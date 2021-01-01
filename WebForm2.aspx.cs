using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomEmailValidation
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string itokenStr = string.Empty;
            string atokenStr = string.Empty;

            if (Request.IsAuthenticated)
            {
                var claimsList = HttpContext.Current.GetOwinContext().Authentication.User.Claims.ToList();
                foreach (var claimItem in claimsList)
                {
                    if (claimItem.Type == "id_token")
                    {
                        itokenStr = claimItem.Value;
                    }
                    else if (claimItem.Type == "access_token")
                    {
                        atokenStr = claimItem.Value;
                    }

                    if ((itokenStr.Length > 0) && (atokenStr.Length > 0))
                    {
                        break;
                    }
                }

                var ihandler = new JwtSecurityTokenHandler();
                var ijsonToken = ihandler.ReadToken(itokenStr);
                var itokenS = ihandler.ReadToken(itokenStr) as JwtSecurityToken;

                GridViewID.DataSource = itokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                GridViewID.DataBind();

                var ahandler = new JwtSecurityTokenHandler();
                var ajsonToken = ahandler.ReadToken(atokenStr);
                var atokenS = ahandler.ReadToken(atokenStr) as JwtSecurityToken;

                GridViewAccess.DataSource = atokenS.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                GridViewAccess.DataBind();

            }
        }

        protected void GridViewID_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewID.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }

        protected void GridViewAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewAccess.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }
    }
}