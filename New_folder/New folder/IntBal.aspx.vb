Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Partial Public Class IntBal
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim Cust_ID As String
    Dim FormID As Integer = 1093
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Ddl_Branch)
            M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "DEPOSIT")
            Txt_FromDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable

            If Txt_AccNo.Text.Trim.Length <= 7 Then
                Txt_AccNo.Text = Right("000" + Ddl_Branch.SelectedValue, 3) + Right("000" + Ddl_Scheme.SelectedValue, 3) + Right("0000000" + Txt_AccNo.Text, 7)
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(Ddl_Branch.SelectedValue, Ddl_Scheme.SelectedValue, Txt_AccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                Cust_ID = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()

            Else

                ShowAlertMessage("Invalid Account Number!!!")
                Txt_AccNo.Text = ""
            End If
            M_CommonBL = Nothing


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name As String
            Dim StrSlf As String
            Dim Dt_AcDtls As DataTable

            M_DBConn = New Connection()
            Company_Name = M_CommonDa.GetCompanyDetails()

            Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")
            Dim Sch_Section As String

            Sch_Section = M_CommonDa.GetSchemeSection(Ddl_Scheme.SelectedValue)

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(Ddl_Branch.SelectedValue, Ddl_Scheme.SelectedValue, Txt_AccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                Cust_ID = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
            End If
            If Sch_Section = "DEPOSIT" Then


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "DATE"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "40"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "DEBIT/CREDIT"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "40"
                HeaderArr(1, 3) = "5"

                HeaderArr(2, 0) = "INTEREST AMOUNT"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "40"
                HeaderArr(2, 3) = "30"

                Session("RptHeader") = HeaderArr
                M_Sql = "Exec InterestBalanceRpt_Sp " & Ddl_Branch.SelectedValue & ",'" & Cust_ID & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "','" & Txt_AccNo.Text & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   

End Class