<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Ren.CMS.NET_InstallLauncher.launcher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
</head>
<body>
    <script src="//codeorigin.jquery.com/jquery-2.0.3.min.js"></script>
    <script src="//codeorigin.jquery.com/ui/1.10.3/jquery-ui.min.js"></script>
     <script>
         $(function () {
             $.post('?', { Install: 'CAB' });

         });

     </script>

    <form id="form1" runat="server">
    <div runat="server" id="prepare">
            <p> Please wait. Launcher is downloading files...</p>
    </div>
    <div runat="server" visible="false" id="ready" class="container">




    </div>
    </form>
</body>
</html>
