Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class NPARptRegionwise
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 2037
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                M_CommonBL.Fill_UserWiseRegion(DdlRegion, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DdlArea, Session("UserID"), DdlRegion.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DdlRegion.SelectedValue, DdlArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                'M_CommonBL.Fill_Branch(DdlBranch, True)
                DdlBranch.Enabled = True
                Txt_AsOnDate.Text = Session("TransDate")
                GetSection()
                Load_LoanType()


                DdlProduct.Items.Add("ALL")
                DdlScheme.Items.Add("ALL")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub DdlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlRegion.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DdlArea, Session("UserID"), DdlRegion.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DdlArea_SelectedIndexChanged(Me.DdlArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DdlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DdlRegion.SelectedValue, DdlArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetSection()
        Try
            Dim M_dt As New DataTable()
            Dim S_CommanBL As New CommonBL
            M_dt = S_CommanBL.LoadSection()
            DdlSection.DataTextField = "Sch_Module_Catg"
            DdlSection.DataValueField = "Sch_Module_Catg"
            DdlSection.DataSource = M_dt
            DdlSection.DataBind()
            DdlSection.Items.Add("ALL")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Load_LoanType()

        Try
            Dim M_dt As New DataTable()
            Dim S_CommanBL As New CommonBL
            M_dt = S_CommanBL.LoadLoanType()
            DdlLoanType.DataTextField = "LoanType"
            DdlLoanType.DataValueField = "LoanTypeCode"
            DdlLoanType.DataSource = M_dt
            DdlLoanType.DataBind()
            DdlLoanType.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub LoadProduct()

    '    Try
    '        Dim M_CommonBL As New CommonBL
    '        Dim M_dt As New DataTable
    '        M_dt = M_CommonBL.LoadProduct()
    '        DdlProduct.DataTextField = "Pkc_Description"
    '        DdlProduct.DataValueField = "Sch_PkcCode"
    '        DdlProduct.DataSource = M_dt
    '        DdlProduct.DataBind()
    '        DdlProduct.Items.Add("ALL")
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub LoadScheme()
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_dt As New DataTable
            M_dt = M_CommonBL.LoadScheme(DdlProduct.SelectedValue)
            DdlScheme.DataTextField = "Sch_Name"
            DdlScheme.DataValueField = "Sch_Code"
            DdlScheme.DataSource = M_dt
            DdlScheme.DataBind()
            DdlScheme.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click

        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

        Dim M_Qry As String = ""

        Dim HeaderArr(19, 4) As String
        Dim HeaderWidth(19, 1) As String

        HeaderArr(0, 0) = "SLNO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "Br_Name"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "16"
        HeaderArr(1, 3) = "16"


        HeaderArr(2, 0) = "Sch_Name"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "10"

        HeaderArr(3, 0) = "Acc_No"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "15"

        HeaderArr(4, 0) = "Cust_Name"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "15"

        HeaderArr(5, 0) = "Loan Date"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "Period"
        HeaderArr(6, 1) = "R"
        HeaderArr(6, 2) = "15"
        HeaderArr(6, 3) = "15"

        HeaderArr(7, 0) = "Due Date"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "10"

        HeaderArr(8, 0) = "Loan Amount"
        HeaderArr(8, 1) = "R"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "10"

        HeaderArr(9, 0) = "EMI"
        HeaderArr(9, 1) = "R"
        HeaderArr(9, 2) = "5"
        HeaderArr(9, 3) = "5"

        HeaderArr(10, 0) = "Last_PaidInstDate"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "10"
        HeaderArr(10, 3) = "10"

        HeaderArr(11, 0) = "NPA_Date"
        HeaderArr(11, 1) = "L"
        HeaderArr(11, 2) = "10"
        HeaderArr(11, 3) = "10"

        HeaderArr(12, 0) = "Outstand"
        HeaderArr(12, 1) = "R"
        HeaderArr(12, 2) = "10"
        HeaderArr(12, 3) = "10"

        HeaderArr(13, 0) = "Receivable Priciple"
        HeaderArr(13, 1) = "R"
        HeaderArr(13, 2) = "10"
        HeaderArr(13, 3) = "10"

        HeaderArr(14, 0) = "Receivable Interest"
        HeaderArr(14, 1) = "R"
        HeaderArr(14, 2) = "10"
        HeaderArr(14, 3) = "10"

        HeaderArr(15, 0) = "Total Receivable"
        HeaderArr(15, 1) = "R"
        HeaderArr(15, 2) = "10"
        HeaderArr(15, 3) = "10"

        HeaderArr(16, 0) = "Period Type"
        HeaderArr(16, 1) = "L"
        HeaderArr(16, 2) = "10"
        HeaderArr(16, 3) = "10"

        HeaderArr(17, 0) = "Last Paid InstNo"
        HeaderArr(17, 1) = "R"
        HeaderArr(17, 2) = "8"
        HeaderArr(17, 3) = "8"

        HeaderArr(18, 0) = "ArearInstNo"
        HeaderArr(18, 1) = "R"
        HeaderArr(18, 2) = "10"
        HeaderArr(18, 3) = "10"



        Session("RptHeader") = HeaderArr

        M_Qry = "EXEC [dbo].[Get_NPA_Regionwise_Sp]'" & DdlRegion.SelectedValue & "','" & DdlArea.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & DdlSection.SelectedValue & "','" & DdlProduct.SelectedValue & "','" & DdlLoanType.SelectedValue & "','" & DdlScheme.SelectedValue & "'," & ClsComm.DbDate(Txt_AsOnDate.Text) & "," & Txt_NpaPeriod.Text & "," & Session("UserID") & ""
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Protected Sub Ddlsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlSection.SelectedIndexChanged
        Try

            Dim M_CommonBL As New CommonBL
            Dim M_dt As New DataTable
            Dim LoanType As String = ""
           
            M_dt = M_CommonBL.LoadProduct(DdlSection.SelectedItem.Text)
            DdlProduct.DataTextField = "Pkc_Description"
            DdlProduct.DataValueField = "Sch_PkcCode"
            DdlProduct.DataSource = M_dt
            DdlProduct.DataBind()
            DdlProduct.Items.Remove(DdlProduct.Items.FindByText("ALL"))
            DdlProduct.Items.Add("ALL")


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlRegion.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DdlArea, Session("UserID"), DdlRegion.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DdlArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DdlRegion.SelectedValue, DdlArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub DdlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProduct.SelectedIndexChanged
       
        Dim M_CommonBL As New CommonBL
        Dim M_dt As New DataTable
        M_dt = M_CommonBL.LoadScheme(DdlProduct.SelectedValue)
        DdlScheme.DataTextField = "Sch_Name"
        DdlScheme.DataValueField = "Sch_Code"
        DdlScheme.DataSource = M_dt
        DdlScheme.DataBind()
        DdlScheme.Items.Remove(DdlScheme.Items.FindByText("ALL"))
        DdlScheme.Items.Add("ALL")

    End Sub
End Class