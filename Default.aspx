<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    Theme="Default" StylesheetTheme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Builder</title>
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
        .style3
        {
        }
    </style>

    <script language="JavaScript" type="text/javascript">

        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=250,left = 390,top = 275');");
        }
    </script>

</head>
<body>
    <div id="M1e9h8d2i-main">
        <div id="M1e9h8d2i-Sheet">
            <div id="M1e9h8d2i-Sheet-cc">
            </div>
            <div id="M1e9h8d2i-Sheet-body">
                <div id="M1e9h8d2i-Header">
                    <div id="M1e9h8d2i-Header-jpeg">
                    </div>
                </div>
                <div id="M1e9h8d2i-contentLayout">
                    <div id="M1e9h8d2i-content">
                        <div id="M1e9h8d2i-Post">
                            <div id="M1e9h8d2i-Post-body">
                                <div id="M1e9h8d2i-Post-inner">
                                    <div id="M1e9h8d2i-PostContent">
                                        <form id="form1" runat="server">
                                        <div style="background-color:White" dir="rtl" align="right">
                                            <div class="super">
                                                <asp:Label ID="lblMSG" runat="server" Font-Bold="True" Font-Size="9pt" Font-Underline="False"
                                                    ForeColor="#CC3300" EnableTheming="False" Font-Italic="False"></asp:Label></div>
                                            <br />
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
                                                </table><asp:Button ID="btnStep1" runat="server" OnClick="btnStep1_Click" Text="برقرای ارتباط" />
                                            </asp:Panel>
                                            
                                            <br />
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="جداول و پرس و جو ها / فیلدها">
                                                <table class="style1">
                                                    <tr>
                                                        <td valign="top">
                                                            &nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            &nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="Label31" runat="server" Text="جداول:"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="Label30" runat="server" Text="پرس و جو ها:"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Label ID="lblFields" runat="server" Text="فیلدها:" Visible="False"></asp:Label>
                                                            &nbsp;
                                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" OnClick="lbtnSelectAll_Click" Visible="False">انتخاب کلی</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:ListBox ID="lsbTables" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lsbTables_SelectedIndexChanged"
                                                                Rows="8"></asp:ListBox>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:ListBox ID="lsbQueries" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lsbQueries_SelectedIndexChanged"
                                                                Rows="8"></asp:ListBox>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:CheckBoxList ID="chbFields" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table><asp:Button ID="btnStep2" runat="server" Text="مشاهده جزئیات" OnClick="btnStep2_Click" />
                                            </asp:Panel>
                                            
                                            <asp:Panel ID="Panel2" runat="server" GroupingText="تعریف شروط">
                                                <asp:GridView ID="gdwConditions" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ترتیب مرتب سازی">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSortRank" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="مرتب سازی">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlSortType" runat="server" SkinID="FA" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlSortType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="ASC">الف - ی</asp:ListItem>
                                                                    <asp:ListItem Value="DESC">ی - الف</asp:ListItem>
                                                                    <asp:ListItem Value="Unsorted" Selected="True">نامرتب</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="خروجی">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chbExport" runat="server" Checked="True" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نام مستعار">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:Panel ID="Panel3" runat="server" GroupingText="فیلتر">
                                                    <asp:DropDownList ID="ddlColumns" runat="server" SkinID="FA">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:DropDownList ID="ddlConditionType" runat="server" SkinID="FA">
                                                        <asp:ListItem Value="BETWEEN">ما بین</asp:ListItem>
                                                        <asp:ListItem Value="LIKE">در بر داشتن</asp:ListItem>
                                                        <asp:ListItem Value="NOT LIKE">در بر نداشتن</asp:ListItem>
                                                        <asp:ListItem Value="LIKE">ختم شده به</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="=">برابر با</asp:ListItem>
                                                        <asp:ListItem Value="&gt;">بزرگتر از</asp:ListItem>
                                                        <asp:ListItem Value="&gt;=">بزرگتر یا مساوی با</asp:ListItem>
                                                        <asp:ListItem Value="IS NULL">در صورت خالی بودن</asp:ListItem>
                                                        <asp:ListItem Value="&lt;">کوچکتر از</asp:ListItem>
                                                        <asp:ListItem Value="&lt;=">کوچکتر یا مساوی با</asp:ListItem>
                                                        <asp:ListItem Value="NOT BETWEEN">ما بین نبودن</asp:ListItem>
                                                        <asp:ListItem Value="&lt;&gt;">نا مساوی بودن</asp:ListItem>
                                                        <asp:ListItem Value="IS NOT NULL">در صورت خالی نبودن</asp:ListItem>
                                                        <asp:ListItem Value="LIKE">شروع شده با</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:RadioButtonList ID="rblAndOr" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                        <asp:ListItem Value="AND">و</asp:ListItem>
                                                        <asp:ListItem Value="OR" Selected="True">یا</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnAddFilter" runat="server" Text="افزودن فیلتر" OnClick="btnAddFilter_Click" />
                                                    <br />
                                                    <table class="style1">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFilters" runat="server" Text="فیلترهای موجود:" Visible="False"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBoxList ID="chbFilters" runat="server" CellPadding="4" RepeatColumns="4"
                                                                    RepeatDirection="Horizontal" Width="100%">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </asp:Panel>
                                                <br />
                                                <asp:Panel ID="Panel4" runat="server" GroupingText="توابع عملیاتی">
                                                    <asp:Panel ID="Panel6" runat="server" GroupingText="افزودن ردیف">
                                                        <asp:CheckBox ID="chbSequential" runat="server" Text="افزودن ستون ردیف به گزارش بر اساس ستون" />
                                                        &nbsp;<asp:DropDownList ID="ddlColumns2" runat="server" SkinID="FA">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel7" runat="server" GroupingText="افزودن جمع / میانگین /تعداد و...">
                                                        <asp:CheckBox ID="chbAgreegate" runat="server" Text="افزودن تابع" />
                                                        &nbsp;<asp:RadioButtonList ID="rblFunctions" runat="server" RepeatDirection="Horizontal"
                                                            RepeatLayout="Flow">
                                                            <asp:ListItem Value="SUM">جمع کل</asp:ListItem>
                                                            <asp:ListItem Value="COUNT">تعداد</asp:ListItem>
                                                            <asp:ListItem Value="AVG">میانگین</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        &nbsp;به انتهای ستون
                                                        <asp:DropDownList ID="ddlColumns3" runat="server" SkinID="FA">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                    <br />
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="btnSaveChange" runat="server" OnClick="btnSaveChange_Click" Text="تهیه خروجی" />
                                                <br />
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="Panel5" runat="server" GroupingText="مشاهده خروجی">
                                                <asp:Panel ID="PnlSQL" runat="server" GroupingText="اسکریپت اس کیو ال">
                                                    <br />
                                                    <asp:TextBox ID="txtSQL" runat="server" EnableTheming="True" ReadOnly="True" Rows="5"
                                                        SkinID="EN" TextMode="MultiLine"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:GridView ID="gdwExport" runat="server" ShowFooter="True" OnRowDataBound="gdwExport_RowDataBound">
                                                    </asp:GridView>
                                                    <br />
                                                    نام گزارش:
                                                    <asp:TextBox ID="txtReportName" runat="server"></asp:TextBox>
                                                    <br />
                                                    <asp:LinkButton ID="lbtnExcel" runat="server" OnClick="lbtnExcel_Click">خروجی اکسل</asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnSaveQry" runat="server" 
                                                        onclick="lbtnSaveQry_Click">ذخیره گزارش</asp:LinkButton>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <br />
                                        </div>
                                        </form>
                                    </div>
                                    <div id="cleared">
                                    </div>
                                </div>
                                <div id="cleared">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="cleared">
                </div>
                <div id="M1e9h8d2i-Footer">
                    <div id="M1e9h8d2i-Footer-inner">
                        <div id="M1e9h8d2i-Footer-text">
                            <p>
                                <a href="#">Contact Us</a> | <a href="#">Terms of Use</a> | <a href="#">Trademarks</a>
                                | <a href="#">Privacy Statement</a><br />
                                Copyright &copy; 2010 M1e9h8d2i. All Rights Reserved.</p>
                        </div>
                    </div>
                    <div id="M1e9h8d2i-Footer-background">
                    </div>
                </div>
                <div id="cleared">
                </div>
            </div>
        </div>
        <div id="cleared">
        </div>
        <p id="M1e9h8d2i-page-footer">
            &nbsp;</p>
    </div>
</body>
</html>
