Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class GsdStandingRpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private pt_Formula As String
    Private Rpt_ProcessQry As String
    Dim FormID As Integer = 1136
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
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try

            Txt_FrDate.Attributes.Add("readonly", "readonly")
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Ddl_Branch)
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
            Txt_FrDate.Text = Session("TransDate")
           
            Lbl_ToDt.Visible = False
            Txt_ToDate.Visible = False

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

    Protected Sub BtnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name As String
            Dim StrSlf As String
            Dim Dt As DataTable

            If (ChkGsd.Checked = False) Then



                Dim HeaderArr(7, 4) As String
                Dim HeaderWidth(7, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "DrAccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "DrScheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "2"
                HeaderArr(2, 3) = "2"

                HeaderArr(3, 0) = "CrAccNo"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "CrScheme"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "2"
                HeaderArr(4, 3) = "2"

                HeaderArr(5, 0) = "Amount"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "13"
                HeaderArr(5, 3) = "13"
                Session("RptHeader") = HeaderArr
            Else


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "DrAccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "15"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "DrScheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "Amount"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "15"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "CrAccNo"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "15"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "CrScheme"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "15"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Amount"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "15"
                HeaderArr(6, 3) = "10"
                Session("RptHeader") = HeaderArr
            End If



            If ChkFailure.Checked Then
                M_Sql = "Exec GsDStandingFailedList_Sp " & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            Else If ChkFail.Checked then
                M_Sql = "Exec LoanStandingFailedList_Sp" & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            ElseIf ChkGsd.Checked Then
                M_Sql = "Exec GSDStandingInstRpt_Sp " & ClsComm.DbDate(Txt_FrDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            ElseIf ChkLoan.Checked Then
                M_Sql = "Exec LoanStandingInstRpt_Sp " & ClsComm.DbDate(Txt_FrDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'"
            End If

            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub ChkFailure_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkFailure.CheckedChanged
        If ChkFailure.Checked Then
            Lbl_ToDt.Visible = True
            Txt_ToDate.Visible = True
        Else
            Lbl_ToDt.Visible = False
            Txt_ToDate.Visible = False
        End If
    End Sub

    Protected Sub ChkFail_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkFail.CheckedChanged
           If ChkFail.Checked Then
            Lbl_ToDt.Visible = True
            Txt_ToDate.Visible = True
        Else
            Lbl_ToDt.Visible = False
            Txt_ToDate.Visible = False
        End If
    End Sub

    Protected Sub ChkGsd_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkGsd.CheckedChanged

    End Sub

    Protected Sub ChkLoan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkLoan.CheckedChanged

    End Sub
End Class