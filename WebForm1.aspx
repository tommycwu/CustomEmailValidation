<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CustomEmailValidation.WebForm1" %>

<script src="https://global.oktacdn.com/okta-signin-widget/5.1.5/js/okta-sign-in.min.js" type="text/javascript"></script>
<link href="https://global.oktacdn.com/okta-signin-widget/5.1.5/css/okta-sign-in.min.css" type="text/css" rel="stylesheet"/>
<script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="okta-login-container"></div>
        <input type="hidden" name="sessionToken" id="hiddenSessionTokenField" />
<script>
    var signIn = new OktaSignIn({
        baseUrl: '<%= System.Configuration.ConfigurationManager.AppSettings["OktaDomain"].ToString() %>'
    });

    signIn.renderEl({
        el: '#okta-login-container'
    }, function success(res) {
            if (res.status === 'SUCCESS') {
                var sessionTokenField = $("#hiddenSessionTokenField");
                sessionTokenField.val(res.session.token);
                var form = sessionTokenField.parent();
                form.submit();
            console.log('sessionToken', res.session.token);
        } else {
            // The user can be in another authentication state that requires further action.
            // For more information about these states, see:
            //   https://github.com/okta/okta-signin-widget#rendereloptions-success-error
        }
    });
</script>
    </form>
</body>
</html>
