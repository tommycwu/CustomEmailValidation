using System;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;


namespace CustomEmailValidation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Redirect("/WebForm2.aspx");
            }

            if (Request.RequestType == "POST" && !Request.IsAuthenticated)
            {
                var sessionToken = Request.Form["sessionToken"]?.ToString();
                var properties = new AuthenticationProperties();
                properties.Dictionary.Add("sessionToken", sessionToken);
                properties.RedirectUri = "/WebForm2.aspx";

                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                        properties,
                        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}