<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="CustomEmailValidation.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">

* {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}
  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    -webkit-box-shadow: none !important;
    box-shadow: none !important;
  }
  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    -webkit-box-shadow: none !important;
    box-shadow: none !important;
  }
  * {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}
  </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ID Token</div>
        <div>
            <asp:GridView ID="GridViewID" runat="server" CssClass="mGrid" OnRowDataBound="GridViewID_OnRowDataBound">
            </asp:GridView>
        </div>
        <br />
        <div>
            Access Token</div>
        <div>
            <asp:GridView ID="GridViewAccess" runat="server" CssClass="mGrid" OnRowDataBound="GridViewAccess_RowDataBound">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
