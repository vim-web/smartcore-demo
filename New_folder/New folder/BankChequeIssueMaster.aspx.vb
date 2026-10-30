Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Partial Public Class BankChequeIssueMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1020
    Private Shared CommCls As New CommonClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoaddefaultValue()
        End If

    End Sub
    Private Sub LoaddefaultValue()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(DDlBranch)
        DDlBranch.SelectedValue = Session("DefaultBrCode")
        DDlBranch.Enabled = False
        TxtBankName.Enabled = False
        DDlBranch_SelectedIndexChanged(Me.Cmb_AccNo, System.EventArgs.Empty)
        Session("M_EntryMode") = "NEW"
    End Sub

    Protected Sub DDlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlBranch.SelectedIndexChanged
        Try
            Dim M_CommonBL As New BankChequeIssueBL
            Dim dt As New DataTable
            dt = M_CommonBL.GetValues(DDlBranch.SelectedValue)
            Cmb_AccNo.DataTextField = "Ba_AccountNo"
            Cmb_AccNo.DataSource = dt
            Cmb_AccNo.DataBind()
            Cmb_AccNo_SelectedIndexChanged(Me.Cmb_AccNo, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Cmb_AccNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cmb_AccNo.SelectedIndexChanged
        Try
            Dim M_CommonBL As New BankChequeIssueBL
            Dim M_Dt As New DataTable

            M_Dt = M_CommonBL.GetAccount(DDlBranch.SelectedValue, Cmb_AccNo.SelectedValue)
            If M_Dt.Rows.Count >= 1 Then
                TxtBankName.Text = M_Dt.Rows(0)("Acc_Name").ToString
                HddAccID.Value = M_Dt.Rows(0)("ACC_ID").ToString
                Balance()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Balance()
        Try
            Dim M_CommonBL As New BankChequeIssueBL
            Dim B_dt As New DataTable
            B_dt = M_CommonBL.GetBalance(HddAccID.Value, Session("TransDate"))
            If B_dt.Rows.Count >= 1 Then
                Dim CurBal As Double = B_dt.Rows(0)("Cur_Bal").ToString()
                TxtBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")

            End If


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

   
    

   
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_BankChequeIssue As New SMARTCORE_BO.BankChequeIssueBO
        Dim ChqFrom As String
        Dim ChqTo As String
        ChqFrom = TxtFromChqno.Text.Trim()
        ChqTo = TxtToChqNo.Text.Trim()

        If ChqTo <= ChqFrom Then
            ShowAlertMessage("ChqFromNo is Less Than ChqToNo ")
            Exit Sub
        Else


            With M_BankChequeIssue


                .BrCode = DDlBranch.SelectedValue
                .FromChequeNo = TxtFromChqno.Text
                .ToChequeNo = TxtToChqNo.Text
                .BankAccNo = Cmb_AccNo.SelectedValue
                .BankName = TxtBankName.Text

            End With

            Try
                Dim M_BrCode As String
                Dim M_BankChequeIssueBL As New BankChequeIssueBL
                M_BrCode = M_BankChequeIssueBL.Save_Data(M_BankChequeIssue, Session("M_EntryMode"))
                ShowAlertMessage("Saved Successfully")
                M_BankChequeIssueBL = Nothing
            Catch ex As Exception
                ShowAlertMessage("Saving Failed")
            End Try
        End If
    End Sub


    '    Private Sub Locate_Data(ByVal M_BankChequeIssue As SMARTCORE_BO.BankChequeIssueBO)

    '        If M_BankChequeIssue.BrCode = 0 Then
    '            LblEntryMode.Text = "-- NEW --"
    '            Exit Sub
    '        End If
    '        With M_BankChequeIssue



    '=.BrCode
    '=.FromChequeNo
    '=.ToChequeNo
    '=.BankAccNo
    '=.BankName

    '        End With
    '    End Sub



End Class