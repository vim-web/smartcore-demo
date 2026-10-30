Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Public Class LienRevokedTo
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1034
    Shared Obj_UserRights As Object
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            LoadDefaulrValues()
        End If
    End Sub
    Private Sub LoadDefaulrValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        CmbLoantype.Visible = False
        LoanType.Visible = False
        'M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "DEPOSIT", "FD','RIS','RD")
        M_CommonBL.GetSchemes(CmbLoantype, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdShow.Click
        AccountDetails()
    End Sub
    Private Sub LienReleased()

        Try
            Dim M_Lien As New LienRevokedToBL
            dt = M_Lien.LienReleased(TxtAccNo.Text)
            DgvRevoke.DataSource = dt
            DgvRevoke.DataBind()
        Catch ex As ArithmeticException
        End Try

    End Sub
    
    Private Sub AccountDetails()
        Try
            Dim Acc_Dtls As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = Trim(TxtAccNo.Text)
           
            If CmbOption.SelectedItem.Text = "LOAN APPLICATION" Then
                Acc_Dtls = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, Trim(TxtAccNo.Text), IIf(CmbLoantype.SelectedValue = "LS", "S", "O"), Session("TransDate"))
                If Acc_Dtls.Rows.Count = 0 Then
                    ShowAlertMessage("Invalid Loan Application No")
                    TxtAccNo.Focus()
                Else
                    CmbBranch.SelectedValue = Acc_Dtls.Rows(0)("Br_Code").ToString()
                    TxtAccNo.Text = M_AccNo
                    LienReleased()
                End If

            Else
                Acc_Dtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbOption.SelectedValue, Trim(TxtAccNo.Text), Session("TransDate"))

                If Acc_Dtls.Rows.Count = 0 Then
                    ShowAlertMessage("Invalid Account No")
                    TxtAccNo.Focus()
                ElseIf Acc_Dtls.Rows.Count > 0 Then
                    If (CmbOption.SelectedValue <> Acc_Dtls.Rows(0)("Sch_Code").ToString() Or CmbBranch.SelectedValue <> Acc_Dtls.Rows(0)("Br_Code").ToString()) Then
                        ShowAlertMessage("Please Enter Correct GSD Account No With Correct Branch!")
                        Exit Sub
                    Else
                        CmbOption.SelectedValue = Acc_Dtls.Rows(0)("Sch_Code").ToString()
                        CmbBranch.SelectedValue = Acc_Dtls.Rows(0)("Br_Code").ToString()
                        TxtAccNo.Text = M_AccNo
                        LienReleased()
                    End If
                End If
                    End If
            DgvRevoke.DataSource = dt
            DgvRevoke.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

 

    Protected Sub DgvRevoke_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DgvRevoke.RowCommand
        Dim Sec_id As String
        Dim Details As String
        Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
        'GsdAccNo = DgvRevoke.DataKeys(RowIndex)("Id").ToString()
        Sec_id = DgvRevoke.DataKeys(RowIndex)("Id").ToString()
        Details = DgvRevoke.DataKeys(RowIndex)("Type").ToString()
        LienRevoked(TxtAccNo.Text, Sec_id, Details)
        LienReleased()
    End Sub
    Private Sub LienRevoked(ByVal SecAcc As String, ByVal sec_id As String, ByVal Details As String)
        Dim dt As New DataTable
        Try
            Dim M_LienRevoke As New LienRevokedToBL
            dt = M_LienRevoke.LienRevoked(SecAcc, sec_id, Details)
            ShowAlertMessage("Lien Released")

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

    
   
    Protected Sub CmbOption_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbOption.SelectedIndexChanged
        If CmbOption.SelectedItem.Text = "LOAN APPLICATION" Then
            CmbLoantype.Visible = True
            LoanType.Visible = True
        Else
            CmbLoantype.Visible = False
            LoanType.Visible = False
        End If
    End Sub

    'Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
    '    AccountDetails()
    'End Sub
End Class