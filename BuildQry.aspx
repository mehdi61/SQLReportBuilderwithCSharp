<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuildQry.aspx.cs" Inherits="BuildQry" Theme="Default" StylesheetTheme="Default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ساختن پرس و جو</title>
        <style type="text/css">
        .super
        {
            text-decoration: blink;
            text-align: center;
            background-color: Silver;
        }
        .style1
        {
            width: 100%;
        }
        </style>

</head>
<body>
    <form id="form1" runat="server">
    <div dir="rtl" align="right">
        <div class="super">
            <asp:Label ID="lblMSG" runat="server" Font-Bold="True" Font-Size="9pt" Font-Underline="False"
                ForeColor="#CC3300" EnableTheming="False" Font-Italic="False"></asp:Label></div>
                <asp:Panel ID="Panel0" runat="server" GroupingText="مرحله اول | ارتباط با بانک اطلاعاتی">
            <table class="style1">
                <tr>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label26" runat="server" Text="کانکشن استرینگ مبدا"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlConnectionStringSource" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlConnectionStringSource_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:ImageButton ID="imgRefresh0" runat="server" ImageUrl="~/Images/Refresh.png"
                            OnClick="imgRefresh_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblSRCd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <a href="javascript:popUp('CnnStr.aspx')">مدیریت کانکشن استرینگ ها</a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style3" colspan="2">
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="btnStep1" runat="server" OnClick="btnStep1_Click" Text="برقرای ارتباط" />
        <asp:Panel ID="Panel1" runat="server" GroupingText="انتخاب جداول و پرس و جو ها">
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="جداول"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="پرس و جو ها"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:TreeView ID="twTables" runat="server" LineImagesFolder="~/TreeLineImages" 
                            ShowLines="True" ShowCheckBoxes="Leaf" ShowExpandCollapse="False">
                        </asp:TreeView>
                    </td>
                    <td valign="top">
                        <asp:TreeView ID="twViews" runat="server" LineImagesFolder="~/TreeLineImages" 
                            ShowCheckBoxes="Leaf" 
                            ShowExpandCollapse="False" ShowLines="True">
                        </asp:TreeView>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Button ID="btnAddFields" runat="server" onclick="btnAddFields_Click" 
                            Text="ثبت فیلدها" />
                        <br />
                    </td>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <br />
                        <asp:ImageButton ID="imgbUP" runat="server" onclick="imgbUP_Click" 
                            ImageUrl="~/Images/up-icon.gif" />
                        <br />
                        <br />
                        <asp:ImageButton ID="imgbDOWN" runat="server" onclick="imgbDOWN_Click" 
                            ImageUrl="~/Images/down-icon.gif" />
                    </td>
                    <td valign="top">
                        <asp:ListBox ID="lsbFields" runat="server" SkinID="lsbEN"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID="Panel2" runat="server" GroupingText="مدیریت ارتباطات">
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="کلید اصلی"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text="کلید فرعی"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlPK" runat="server" 
                            OnSelectedIndexChanged="ddlConnectionStringSource_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFK" runat="server" 
                            OnSelectedIndexChanged="ddlConnectionStringSource_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAddKey" runat="server" OnClick="btnAddKey_Click" 
                            Text="ایجاد ارتباط" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <asp:CheckBoxList ID="chbKeys" runat="server" CellPadding="4" RepeatColumns="4" 
                            RepeatDirection="Horizontal" Width="100%">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
            <asp:Button ID="btnSaveChange" runat="server" OnClick="btnSaveChange_Click" 
                Text="تهیه خروجی" />
            &nbsp;&nbsp;
        <asp:Label ID="Label30" runat="server" Text="ذخیره پرس و جو تولید شده با نام:"></asp:Label>
&nbsp;<asp:TextBox ID="txtViewName" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnSaveView" runat="server" onclick="btnSaveView_Click" 
            Text="ذخیره پرس و جو" />
            <br />
        <asp:Panel ID="Panel5" runat="server" GroupingText="مشاهده خروجی">
            <asp:Panel ID="PnlSQL" runat="server" GroupingText="اسکریپت اس کیو ال">
                <br />
                <asp:TextBox ID="txtSQL" runat="server" EnableTheming="True" ReadOnly="True" Rows="5"
                    SkinID="EN" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Button ID="btnExecute" runat="server" Text="اجرای اسکریپت تولید شده" 
                    onclick="btnExecute_Click" />
                <asp:GridView ID="gdwExport" runat="server" ShowFooter="True">
                </asp:GridView>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </asp:Panel>
        </asp:Panel>
        <br />
    
    </div>
    </form>
</body>
</html>
