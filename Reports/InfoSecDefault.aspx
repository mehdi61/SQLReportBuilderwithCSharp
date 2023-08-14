<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoSecDefault.aspx.cs" Inherits="Reports_Default" Theme="Default" StylesheetTheme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div dir="rtl" align="center">
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" 
            ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ID" HeaderText="شناسه" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="f_Nam" HeaderText="نام" SortExpression="f_Nam" />
                <asp:BoundField DataField="f_Lastname" HeaderText="شهرت" 
                    SortExpression="f_Lastname" />
                <asp:BoundField DataField="f_CompanyName" HeaderText="شرکت" 
                    SortExpression="f_CompanyName" />
                <asp:BoundField DataField="f_UnitName" HeaderText="واحد" 
                    SortExpression="f_UnitName" />
                <asp:BoundField DataField="f_SMT" HeaderText="سمت" SortExpression="f_SMT" />
                <asp:BoundField DataField="f_Termemployment" HeaderText="مدت اشتغال" 
                    SortExpression="f_Termemployment" />
                <asp:BoundField DataField="f_Phone" HeaderText="تلفن" 
                    SortExpression="f_Phone" />
                <asp:BoundField DataField="f_Fax" HeaderText="فکس" SortExpression="f_Fax" />
                <asp:BoundField DataField="f_Email" HeaderText="ایمیل" 
                    SortExpression="f_Email" />
                <asp:BoundField DataField="f_Address" HeaderText="آدرس" 
                    SortExpression="f_Address" />
                <asp:BoundField DataField="f_Corporate" HeaderText="شرکت دعوت کننده" 
                    SortExpression="f_Corporate" />
                <asp:BoundField DataField="f_Description" HeaderText="توضیحات" 
                    SortExpression="f_Description" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="Data Source=64.130.216.129;Initial Catalog=SeminarDB;User ID=SeminarUser; Password=1qaz!QAZ" 
            ProviderName="System.Data.SqlClient" 
            
            SelectCommand="SELECT * FROM [tbl_participateinseminar] ORDER BY [ID] DESC" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [tbl_participateinseminar] WHERE [ID] = @original_ID;" 
            InsertCommand="INSERT INTO [tbl_participateinseminar] ([f_Nam], [f_Lastname], [f_CompanyName], [f_UnitName], [f_SMT], [f_Termemployment], [f_Phone], [f_Fax], [f_Email], [f_Address], [f_Corporate], [f_Description]) VALUES (@f_Nam, @f_Lastname, @f_CompanyName, @f_UnitName, @f_SMT, @f_Termemployment, @f_Phone, @f_Fax, @f_Email, @f_Address, @f_Corporate, @f_Description)" 
            OldValuesParameterFormatString="original_{0}" 
            UpdateCommand="UPDATE [tbl_participateinseminar] SET [f_Nam] = @f_Nam, [f_Lastname] = @f_Lastname, [f_CompanyName] = @f_CompanyName, [f_UnitName] = @f_UnitName, [f_SMT] = @f_SMT, [f_Termemployment] = @f_Termemployment, [f_Phone] = @f_Phone, [f_Fax] = @f_Fax, [f_Email] = @f_Email, [f_Address] = @f_Address, [f_Corporate] = @f_Corporate, [f_Description] = @f_Description WHERE [ID] = @original_ID AND (([f_Nam] = @original_f_Nam) OR ([f_Nam] IS NULL AND @original_f_Nam IS NULL)) AND (([f_Lastname] = @original_f_Lastname) OR ([f_Lastname] IS NULL AND @original_f_Lastname IS NULL)) AND (([f_CompanyName] = @original_f_CompanyName) OR ([f_CompanyName] IS NULL AND @original_f_CompanyName IS NULL)) AND (([f_UnitName] = @original_f_UnitName) OR ([f_UnitName] IS NULL AND @original_f_UnitName IS NULL)) AND (([f_SMT] = @original_f_SMT) OR ([f_SMT] IS NULL AND @original_f_SMT IS NULL)) AND (([f_Termemployment] = @original_f_Termemployment) OR ([f_Termemployment] IS NULL AND @original_f_Termemployment IS NULL)) AND (([f_Phone] = @original_f_Phone) OR ([f_Phone] IS NULL AND @original_f_Phone IS NULL)) AND (([f_Fax] = @original_f_Fax) OR ([f_Fax] IS NULL AND @original_f_Fax IS NULL)) AND (([f_Email] = @original_f_Email) OR ([f_Email] IS NULL AND @original_f_Email IS NULL)) AND (([f_Address] = @original_f_Address) OR ([f_Address] IS NULL AND @original_f_Address IS NULL)) AND (([f_Corporate] = @original_f_Corporate) OR ([f_Corporate] IS NULL AND @original_f_Corporate IS NULL)) AND (([f_Description] = @original_f_Description) OR ([f_Description] IS NULL AND @original_f_Description IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_f_Nam" Type="String" />
                <asp:Parameter Name="original_f_Lastname" Type="String" />
                <asp:Parameter Name="original_f_CompanyName" Type="String" />
                <asp:Parameter Name="original_f_UnitName" Type="String" />
                <asp:Parameter Name="original_f_SMT" Type="String" />
                <asp:Parameter Name="original_f_Termemployment" Type="String" />
                <asp:Parameter Name="original_f_Phone" Type="String" />
                <asp:Parameter Name="original_f_Fax" Type="String" />
                <asp:Parameter Name="original_f_Email" Type="String" />
                <asp:Parameter Name="original_f_Address" Type="String" />
                <asp:Parameter Name="original_f_Corporate" Type="String" />
                <asp:Parameter Name="original_f_Description" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="f_Nam" Type="String" />
                <asp:Parameter Name="f_Lastname" Type="String" />
                <asp:Parameter Name="f_CompanyName" Type="String" />
                <asp:Parameter Name="f_UnitName" Type="String" />
                <asp:Parameter Name="f_SMT" Type="String" />
                <asp:Parameter Name="f_Termemployment" Type="String" />
                <asp:Parameter Name="f_Phone" Type="String" />
                <asp:Parameter Name="f_Fax" Type="String" />
                <asp:Parameter Name="f_Email" Type="String" />
                <asp:Parameter Name="f_Address" Type="String" />
                <asp:Parameter Name="f_Corporate" Type="String" />
                <asp:Parameter Name="f_Description" Type="String" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_f_Nam" Type="String" />
                <asp:Parameter Name="original_f_Lastname" Type="String" />
                <asp:Parameter Name="original_f_CompanyName" Type="String" />
                <asp:Parameter Name="original_f_UnitName" Type="String" />
                <asp:Parameter Name="original_f_SMT" Type="String" />
                <asp:Parameter Name="original_f_Termemployment" Type="String" />
                <asp:Parameter Name="original_f_Phone" Type="String" />
                <asp:Parameter Name="original_f_Fax" Type="String" />
                <asp:Parameter Name="original_f_Email" Type="String" />
                <asp:Parameter Name="original_f_Address" Type="String" />
                <asp:Parameter Name="original_f_Corporate" Type="String" />
                <asp:Parameter Name="original_f_Description" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="f_Nam" Type="String" />
                <asp:Parameter Name="f_Lastname" Type="String" />
                <asp:Parameter Name="f_CompanyName" Type="String" />
                <asp:Parameter Name="f_UnitName" Type="String" />
                <asp:Parameter Name="f_SMT" Type="String" />
                <asp:Parameter Name="f_Termemployment" Type="String" />
                <asp:Parameter Name="f_Phone" Type="String" />
                <asp:Parameter Name="f_Fax" Type="String" />
                <asp:Parameter Name="f_Email" Type="String" />
                <asp:Parameter Name="f_Address" Type="String" />
                <asp:Parameter Name="f_Corporate" Type="String" />
                <asp:Parameter Name="f_Description" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
