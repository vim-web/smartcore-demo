Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class Locker_Rpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1097
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If



                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Cmb_Branch)


            Txt_FrDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")



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
   
    Protected Sub Cmd_Show_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String

        Dim Brcode As String

        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If
       
        M_DBConn = New Connection()

        Company_Name = HttpContext.Current.Cache("CompanyName")

        If Opt_LkrAvailability.Checked = True Then
            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "LOCKER NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "LOCKER CODE"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "LOCKER TYPE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "ROW NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "COLUMN NO"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            
            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC LkrAvailability_Sp'" & Cmb_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        ElseIf Opt_LockerClose.Checked = True Then

            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String
            HeaderArr(0, 0) = "ACC NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "KEY NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LOCKER NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "OPEN DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "DUE DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "20"
            HeaderArr(5, 3) = "13"



            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC [LkrClosedReg_Sp] '" & Cmb_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        ElseIf Opt_LockerIssue.Checked = True Then
            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String
            HeaderArr(0, 0) = "ACC NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "40"

            HeaderArr(2, 0) = "KEY NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "LOCKER NO"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "OPEN DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "DUE DATE"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "20"
            HeaderArr(5, 3) = "13"


            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC LkrIssueClosedReg_Sp'" & Cmb_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        ElseIf Opt_LkrVisitorsList.Checked = True Then
            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String
            HeaderArr(0, 0) = "DATE"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "LOCKER NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "KEY NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "NAME"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "ADDRESS"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "ACC NO"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "TIME IN"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "TIME OUT"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "DUE DATE"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "13"
            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC LkrVisitorList_Sp'" & Cmb_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        ElseIf Opt_LkrOutstanding.Checked = True Then
            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String
            HeaderArr(0, 0) = "Sl NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Account NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "Name"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "27"

            HeaderArr(3, 0) = "Address"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "30"

            HeaderArr(4, 0) = "Phone No"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "20"

            HeaderArr(5, 0) = "Open Date"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "Due Date"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"



            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC Locker_Outstanding'" & Cmb_Branch.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        End If

    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub Opt_LkrAvailability_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_LkrAvailability.CheckedChanged
        If Opt_LkrAvailability.Checked = True Then
            Lbl_DateTo.Visible = False
            Txt_ToDate.Visible = False
        End If
    End Sub

    Protected Sub Opt_LockerIssue_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_LockerIssue.CheckedChanged
        If Opt_LockerIssue.Checked Then
            Txt_ToDate.Visible = True
            Lbl_DateTo.Visible = True
        End If
    End Sub

    Protected Sub Opt_LkrOutstanding_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_LkrOutstanding.CheckedChanged
        If Opt_LkrOutstanding.Checked = True Then
            Lbl_DateTo.Visible = False
            Txt_ToDate.Visible = False
        End If
    End Sub

    Protected Sub Opt_LockerClose_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_LockerClose.CheckedChanged
        If Opt_LockerClose.Checked Then
            Txt_ToDate.Visible = True
            Lbl_DateTo.Visible = True
        End If
    End Sub
End Class