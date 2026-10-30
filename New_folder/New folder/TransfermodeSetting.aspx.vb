Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BO
Partial Public Class TransfermodeSetting
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Load_DefaultValues()
            'Session("TranferDetails") = Nothing
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Try

  
            M_CommonBL.Fill_Pickup(drpTranferType, 108, 0)
            'M_CommonBL.GetSchemes(drpBank, Session("TransDate"), "BANK", "BANK")
            M_CommonBL.GetBranchBankDetails(drpBank, Session("DefaultBrCode").ToString())
            M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
            'M_CommonBL.Fill_BankAccountCheqyeNo(drpCheque, drpbankAccno.SelectedValue.ToString())
            Dim M_Dt As New DataTable
            If DirectCast(Session("TranferDetails"), DataTable).Rows.Count > 0 Then
                M_Dt = Session("TranferDetails")
                If M_Dt.Rows.Count > 0 Then

                    drpBank.SelectedItem.Text = M_Dt.Rows(0).Item("Bank").ToString()
                    drpbankAccno.SelectedItem.Text = M_Dt.Rows(0).Item("BankAccNo").ToString()
                    TxtCheque.Text = M_Dt.Rows(0).Item("ChequeNo").ToString()
                    txtChequeDate.Text = M_Dt.Rows(0).Item("ChequeDate").ToString()
                    drpTranferType.SelectedValue = M_Dt.Rows(0).Item("TranferType").ToString()
                    txtIFSC.Text = M_Dt.Rows(0).Item("IFSC").ToString()
                    txtaccno.Text = M_Dt.Rows(0).Item("AccNo").ToString()
                    txtCommission.Text = M_Dt.Rows(0).Item("CommissionAmt").ToString()
                End If
            End If

         
            dvRTGS.Visible = False
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

    Private Sub drpTranferType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTranferType.SelectedIndexChanged

        If drpTranferType.SelectedItem.Text = "Cheque" Then
            dvRTGS.Visible = False
        Else
            dvRTGS.Visible = True
        End If

    End Sub

    Private Sub drpbankAccno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpbankAccno.SelectedIndexChanged
        'M_CommonBL.Fill_BankAccountCheqyeNo(drpCheque, drpbankAccno.SelectedValue.ToString())
    End Sub

    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles CmdSave.Click
        Dim dtTransfer As New DataTable
        dtTransfer.Columns.Add("Bank", GetType(String))
        dtTransfer.Columns.Add("BankAccNo", GetType(String))
        dtTransfer.Columns.Add("ChequeNo", GetType(String))
        dtTransfer.Columns.Add("ChequeDate", GetType(String))
        dtTransfer.Columns.Add("TranferType", GetType(String))
        dtTransfer.Columns.Add("IFSC", GetType(String))
        dtTransfer.Columns.Add("AccNo", GetType(String))
        dtTransfer.Columns.Add("CommissionAmt", GetType(String))

        If TxtCheque.Text.Length > 0 Then
            dtTransfer.Rows.Add(drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), txtaccno.Text.ToString(), txtCommission.Text.ToString())
            Session("TranferDetails") = dtTransfer
            Session("BankAccNo") = drpbankAccno.SelectedItem.Text.ToString()
        Else
            DirectCast(Session("TranferDetails"), DataTable).Rows.Clear()
            Session("BankAccNo") = Nothing
        End If


        ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)

    End Sub
End Class