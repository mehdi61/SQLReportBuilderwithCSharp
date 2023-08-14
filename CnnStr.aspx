<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CnnStr.aspx.cs" Inherits="CnnStr" Theme="Default" StylesheetTheme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>مدیریت کانکشن استرینگ ها</title>
    <style type="text/css">
        .super {text-decoration: blink; text-align: center; background-color:Silver}

        .style5
        {
            height: 14px;
        }
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div dir="rtl">
    <div class="super">
        <asp:Label ID="lblMSG" runat="server" Font-Bold="True" Font-Size="9pt" 
            Font-Underline="False" ForeColor="#CC3300" EnableTheming="False" 
            Font-Italic="False"></asp:Label></div>
    
            <table class="style1">
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label1" runat="server" Text="سرور مبدا"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlServers" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:LinkButton ID="lbtnNewServer" runat="server" onclick="lbtnNewServer_Click" 
                            ValidationGroup="vgp1">افزودن 
                        سرور جدید</asp:LinkButton>
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label5" runat="server" Text="سرور جدید" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewServer" runat="server" Visible="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtNewServer" ErrorMessage="الزامی" 
                            ValidationGroup="vgpAdd"></asp:RequiredFieldValidator>
&nbsp; <asp:Button ID="btnAddServer" runat="server" onclick="btnAddServer_Click" 
                            Text="ایجاد" Visible="False" ValidationGroup="vgpAdd" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label2" runat="server" Text="نام کاربری"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtUsername" ErrorMessage="الزامی" 
                            ValidationGroup="vgpDB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label3" runat="server" Text="رمزعبور"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" ValidationGroup="vgpDB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="الزامی" 
                            ValidationGroup="vgpDB"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="2">
                        <asp:Button ID="BtnShowDBs" runat="server" onclick="BtnShowDBs_Click" 
                            Text="مشاهده بانک های اطلاعاتی" ValidationGroup="vgpDB" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label4" runat="server" Text="بانک اطلاعاتی"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDatabase" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style5" colspan="2">
                        <asp:Button ID="btnConnect" runat="server" onclick="btnConnect_Click" 
                            Text="ساخت کانکشن استرینگ" style="height: 22px" 
                            ValidationGroup="vgpCreate" />
                    </td>
                </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
