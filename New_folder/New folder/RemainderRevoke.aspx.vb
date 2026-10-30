Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Public Class RemainderRevoke
    Inherits System.Web.UI.Page
    Public RemTbl As New DataTable("Table")
    'Dim FormID As Integer = 1150
    'Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            'User(Rights)
            'Dim M_Comm As New CommonBL
            'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            'M_Comm = Nothing
            'If Obj_UserRights.View_Right = False Then
            '    Response.Redirect("~/UnauthorisedAcces.ASPX")
            'End If
            Load_DefaultValues()
            Set_Grid()
            CmbBranch.SelectedValue = Session("DefaultBrCode")
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        'M_CommonBL.Fill_User(CmbUser)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))


        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        'Call CmbRemndrTo_SelectedIndexChanged(Me.CmbRemndrTo, System.EventArgs.Empty)
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        'TxtUserId_TextChanged(Me.TxtUserId, System.EventArgs.Empty)
    End Sub
    Private Sub Clear_Controls()
        TxtAccNo.Text = ""
        'TxtBranch.Text = ""
        TxtAcHoldrAddr.Text = ""
        TxtAcHoldrName.Text = ""

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Set_Grid()
        DgvReminder.DataSource = Nothing
        RemTbl.Columns.Add("Reminder_ID")
        RemTbl.Columns.Add("RemainderDate")
        RemTbl.Columns.Add("Remainder_Content")
        RemTbl.Rows.Add("", "", "")
        Session("RemTbl") = RemTbl
        DgvReminder.DataSource = Session("RemTbl")
        DgvReminder.DataBind()
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        'TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As New DataTable
        Dim M_RemTbl As New DataTable
        Dim M_ReminderBL As New RemainderBL
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            Dim M_AccNo As String = TxtAccNo.Text

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                'Session("CustomerId") = TxtCid.Text
                HdnAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                TxtAcHoldrName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                TxtAcHoldrAddr.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()


                M_RemTbl = M_ReminderBL.ReminderRevokeGrid(HdnAccID.Value)
                If M_RemTbl.Rows.Count > 0 Then

                   
                    DgvReminder.DataSource = M_RemTbl
                    DgvReminder.DataBind()
                    Session("RemTbl") = M_RemTbl
                End If
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
            End If
            M_CommonBL = Nothing

            
            'If Len(HdnCustID.Value) > 0 Then TxtAccNo.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub

    Private Sub DgvReminder_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvReminder.RowCommand
        Dim M_Sql As String
        Dim M_DBConn As New Connection

        Dim i As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim GvRow As GridViewRow = DgvReminder.Rows(i)
        
        M_Sql = "update Gen_Remainder_M_Tbl set status ='C' where Reminder_Id ='" & DgvReminder.Rows(i).Cells(0).Text & "'"
        M_DBConn.ExecuteScalar(M_Sql, 0, False)
        ShowAlertMessage("Reminder for  " + TxtAcHoldrName.Text + "Revoked")
        M_DBConn = Nothing
    End Sub
End Class