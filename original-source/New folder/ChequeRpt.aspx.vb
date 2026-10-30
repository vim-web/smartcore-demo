Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class ChequeRpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1022
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
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
            Txt_Date.Text = Session("TransDate")
            Txt_DateTo.Text = Session("TransDate")
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String


        M_DBConn = New Connection()

        Company_Name = HttpContext.Current.Cache("CompanyName")

        If Opt_ChequeStock.Checked = True Then
            Dim HeaderArr(4, 4) As String
            Dim HeaderWidth(4, 1) As String
            HeaderArr(0, 0) = "CHEQUE NO FROM"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "CHEQUE NO TO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "CHEQUE BOOK TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "40"
            HeaderArr(3, 3) = "13"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Cheque_Stock_sp'" & Ddl_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_Date.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub


        ElseIf Opt_ChequeIssue.Checked = True Then

            Dim HeaderArr(4, 4) As String
            Dim HeaderWidth(4, 1) As String

            HeaderArr(0, 0) = "ACCOUNT NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "CHEQUE NO FROM"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "CHEQUE NO TO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "40"
            HeaderArr(2, 3) = "40"

            HeaderArr(3, 0) = "CHEQUE BOOK TYPE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Cheque_Issue_sp'" & Ddl_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_Date.Text) & "," & CommonClass.DbDate(Txt_DateTo.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
            Exit Sub



        ElseIf Opt_ChqReturn.Checked = True Then
            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String

            HeaderArr(0, 0) = "ACCOUNT NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "13"

            HeaderArr(1, 0) = "NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "CHEQUE NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "CHEQUE TYPE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "STATUS"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "INTIMATION DATE"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "AMOUNT"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Cheque_Return_Sp'" & Ddl_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_Date.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
            Exit Sub


        ElseIf Opt_ChqStopPayment.Checked = True Then
            Dim HeaderArr(4, 4) As String
            Dim HeaderWidth(4, 1) As String

            HeaderArr(0, 0) = "ACCOUNT NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "CHEQUE NO FROM"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "CHEQUE NO TO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "CHEQUE STATUS"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "13"

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Cheque_Stoppayment_sp'" & Ddl_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_Date.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

        End If


    End Sub
    Protected Sub Opt_ChequeIssue_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_ChequeIssue.CheckedChanged
        If Opt_ChequeIssue.Checked = True Then
            Lbl_DateTo.Visible = True
            Txt_DateTo.Visible = True
        End If
    End Sub

    Protected Sub Opt_ChequeStock_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_ChequeStock.CheckedChanged
        If Opt_ChequeStock.Checked = True Then
            Lbl_DateTo.Visible = False
            Txt_DateTo.Visible = False
        End If
    End Sub

    Protected Sub Opt_ChqReturn_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_ChqReturn.CheckedChanged
        If Opt_ChqReturn.Checked = True Then
            Lbl_DateTo.Visible = False
            Txt_DateTo.Visible = False
        End If
    End Sub

    Protected Sub Opt_ChqStopPayment_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_ChqStopPayment.CheckedChanged
        If Opt_ChqStopPayment.Checked = True Then
            Lbl_DateTo.Visible = False
            Txt_DateTo.Visible = False
        End If
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class