using Newtonsoft.Json;
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

namespace CustomEmailValidation
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public string ValidateToken(string baseUrl, string apiKey, string token)
        {
            var fullUrl = baseUrl + "/api/v1/authn";
            try
            {
                dynamic bodyObj = new
                {
                    token,
                };
                var bodyJson = JsonConvert.SerializeObject(bodyObj);

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(fullUrl)
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("SSWS", apiKey);
                    request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                    var response = client.SendAsync(request);
                    var resultStr = response.Result.Content.ReadAsStringAsync().Result;
                    dynamic resultJson = JsonConvert.DeserializeObject(resultStr);
                    return resultJson._embedded.user.id.ToString();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetResetUrl(string baseUrl, string apiKey, string userId)
        {
            var fullUrl = baseUrl + "/api/v1/users/"+ userId + "/lifecycle/reset_password?sendEmail=false";
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(fullUrl)
                    };
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("SSWS", apiKey);
                    request.Content = new StringContent("", Encoding.UTF8, "application/json");
                    var response = client.SendAsync(request);
                    var resultStr = response.Result.Content.ReadAsStringAsync().Result;
                    dynamic resultJson = JsonConvert.DeserializeObject(resultStr);
                    return resultJson.resetPasswordUrl;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string tokenParam = HttpUtility.ParseQueryString(HttpContext.Current.Request.RawUrl).Get(0);//pull the token from the url
            string baseUrl = ConfigurationManager.AppSettings["OktaDomain"].ToString();
            string apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            string retUserId = ValidateToken(baseUrl, apiKey, tokenParam);//check to see if the token is still valid
            string retUrl = ConfigurationManager.AppSettings["HandleErrorPage"];//default to error handling if the results are not expected
            if (retUserId.Length > 0)
            {
                retUrl = GetResetUrl(baseUrl, apiKey, retUserId);//if the token is good,then get the url to finish setting up the user
            }
            Response.Redirect(retUrl);
        }
    }
}