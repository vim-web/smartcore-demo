Imports SMARTCORE_BL

Public Class LockerVisitorEntry
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()

        End If
    End Sub
    Private Sub Set_SchemeCtrl()
        'Try
        '    TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        '    'TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
        'Catch ex As Exception
        '    ShowAlertMessage(Err.Description)
        'End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOCKER")
        '  M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOCKER")
        TxtCheckIn.Text = DateTime.Now.ToString("hh:mm tt")
        TxtCheckOut.Text = DateTime.Now.ToString("hh:mm tt")
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
    End Sub
    Private Sub Clear_Controls()
        ' TxtAccNo.Text = ""
        ' TxtBranch.Text = ""
        TxtCheckin.Text = DateTime.Now.ToString("hh:mm tt")
        TxtCheckOut.Text = DateTime.Now.ToString("hh:mm tt")
        'TxtShortCode.Text = ""
        TxtRemark.Text = ""

        ' TxtAccNo.Focus()
        ChkVisitor.Items.Clear()

    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
            'Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        'Set_SchemeCtrl()
        'TxtShortCode.Text = ""
        'TxtShortCode.Focus()
    End Sub

    'Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
    '    On Error Resume Next
    '    Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
    '    Set_SchemeCtrl()
    'End Sub


    'Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged


    'End Sub


    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_LockerVisitorEntry As New SMARTCORE_BO.LockerVisitorEntryBO

        Dim M_StID As Integer

        With M_LockerVisitorEntry
            .LrvVisitorID = 0
            .LrvAccID = Val(AccId.Value)
            .LrvVisitorCount = 0
            .TransDate = Session("TransDate")
            .LrvCheckIn = TxtCheckIn.Text
            .LrvCheckOut = TxtCheckOut.Text
            .LrvCustId = Session("CustomerId")
            .LrvRemarks = TxtRemark.Text

        End With

        Dim M_LockerVisitorEntryBL As New LockerVisitorEntryBL

        Session("M_EntryMode") = "NEW"
        M_StID = M_LockerVisitorEntryBL.Save_Data(M_LockerVisitorEntry, Session("M_EntryMode"))

        If (M_StID > 0) Then
            ShowAlertMessage("Successfully Saved")
            Clear_Controls()
        Else
            ShowAlertMessage(" SAVING FAILED")
        End If
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
        'TxtCheckin.Text = DateTime.Now.ToString("hh:mm tt")
        'TxtCheckOut.Text = DateTime.Now.ToString("hh:mm tt")

    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    'Protected Sub TxtLockerNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtLockerNo.TextChanged
    '    Try
    '        Dim M_CommonBL As New CommonBL
    '        Dim M_AccNo As String = TxtAccNo.Text
    '        ChkVisitor.Items.Clear()
    '        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
    '        If Dt_AcDtls.Rows.Count > 0 Then
    '            Cust_Id.Value = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
    '            Session("CustomerId") = Cust_Id.Value
    '            AccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
    '            ChkVisitor.Items.Insert(0, Dt_AcDtls.Rows(0)("Cust_Name").ToString())
    '            'TxtAcHoldrName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
    '            'TxtAcHoldrAddr.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
    '            'Else
    '        End If
    '        'ShowAlertMessage("INVALID ACCOUNT NO")
    '        'Clear_Controls()


    '        M_CommonBL = Nothing
    '        '  If Len(Cust_Id) = 0 Then TxtAccNo.Focus()
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '    End Try
    'End Sub

    
    'Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
    '    TxtLockerNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TextBox2.Text, 7)
    '    Call TxtLockerNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
    'End Sub

    'Protected Sub TxtShortCode_TextChanged(sender As Object, e As EventArgs) Handles TxtShortCode.TextChanged
    '    TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
    '    Call TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
    'End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNo.Text
            ChkVisitor.Items.Clear()
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                Cust_Id.Value = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = Cust_Id.Value
                AccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                ChkVisitor.Items.Insert(0, Dt_AcDtls.Rows(0)("Cust_Name").ToString())
                TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
                Clear_Controls()
            End If

            M_CommonBL = Nothing
            '  If Len(Cust_Id) = 0 Then TxtAccNo.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub ChkVisitor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkVisitor.SelectedIndexChanged
        TxtRemark.Text = ChkVisitor.SelectedValue

    End Sub
End Class