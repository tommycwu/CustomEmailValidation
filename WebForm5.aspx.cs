using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace CustomEmailValidation
{

    public partial class WebForm5 : System.Web.UI.Page
    {
        private string userParam;

        protected void Page_Load(object sender, EventArgs e)
        {
            userParam = Request.QueryString["userName"];
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //initialize sdk client and user
            var sdkClient = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = ConfigurationManager.AppSettings["OktaDomain"].ToString(),
                Token = ConfigurationManager.AppSettings["ApiKey"].ToString(),
            });
            var oktaUser = await sdkClient.Users.GetUserAsync(userParam);

            //update email
            oktaUser.Profile["email"] = TextBox1.Text;
            await oktaUser.UpdateAsync();

            //update login
            oktaUser.Profile["login"] = userParam;
            await oktaUser.UpdateAsync();

            //send email
            await oktaUser.AddFactorAsync(new AddEmailFactorOptions
            {
                Email = TextBox1.Text,
            });
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            //initialize sdk client and user
            var sdkClient = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = ConfigurationManager.AppSettings["OktaDomain"].ToString(),
                Token = ConfigurationManager.AppSettings["ApiKey"].ToString(),
            });
            var user = await sdkClient.Users.GetUserAsync(userParam);

            //get email factor id
            var emailFactor = await user.Factors.FirstOrDefaultAsync(x => x.FactorType == FactorType.Email);

            //ask user for otp
            var factorRequest = new ActivateFactorRequest()
            {
                PassCode = TextBox2.Text,
            };
            //verify otp
            var factorResponse = await sdkClient.UserFactors.ActivateFactorAsync(factorRequest, user.Id, emailFactor.Id);


            if (factorResponse.Status == "ACTIVE")
            {
                Response.Redirect("WebForm2.aspx" + "?email=" + TextBox1.Text);
            }
            else
            {
                Label1.Text = factorResponse.Status;
            }
        }
    }
}