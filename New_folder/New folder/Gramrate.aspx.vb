Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports System.Windows
Public Class Gramrate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txt_date.Text = Session("TransDate")
            Load_DefaultValues()
            bindgrid()
            dropchange()
        End If
    End Sub
    Private Sub bindgrid()
        Dim ds As New DataSet
        Dim db As New Connection
        Try
            Dim m_grdbind As New SMARTCORE_BL.Gold_GramRateBL
            ds = m_grdbind.Get_GramRateDetails(Cmbscheme.SelectedValue, txt_date.Text)
            Dim M_HeaderDt As DataTable
            M_HeaderDt = ds.Tables(1)


            If M_HeaderDt.Rows.Count > 0 Then
                'txt_date.Text = ""

                '  txt_date.Text = M_HeaderDt.Rows(0).Item("EFFECTIVE_DATE")
                txt_rate.Text = M_HeaderDt.Rows(0).Item("GRAM_RATE")
                txtrate.Text = M_HeaderDt.Rows(0).Item("BANK_RATE")
                Txt_LendingRate.Text = M_HeaderDt.Rows(0).Item("LENDING_RATE")
                If M_HeaderDt.Rows(0).Item("BANK_RATE").ToString() = "P" Then
                    cmbrate.SelectedIndex = 0
                Else
                    cmbrate.SelectedIndex = 1
                End If
                txt_weight.Text = M_HeaderDt.Rows(0).Item("MINIMUM_WEIGHT")
                txtweight_rate.Text = M_HeaderDt.Rows(0).Item("MINIMUM_WEIGHTRATE")
                ''RadioButtonList1.Items(1).Selected = True
            End If
            GvRpt.DataSource = M_HeaderDt
            GvRpt.DataBind()
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
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.Items.Add("ALL")
            M_CommonBL.GetSchemes(Cmbscheme, Session("TransDate"), "LOAN", "GOLDLOAN")
            Call Cmbscheme_SelectedIndexChanged(Me.Cmbscheme, System.EventArgs.Empty)
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            Txtbr_code.Text = CmbBranch.SelectedValue
            CmbBranch.SelectedValue = Txtbr_code.Text
            Dim ds As New DataSet
            Dim m_grdbind As New SMARTCORE_BL.Gold_GramRateBL
            ds = m_grdbind.Get_GramRateDetails(Cmbscheme.SelectedValue, txt_date.Text)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Cmbscheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmbscheme.SelectedIndexChanged
        bindgrid()
    End Sub

    Protected Sub Txtbr_code_TextChanged(sender As Object, e As EventArgs) Handles Txtbr_code.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = Txtbr_code.Text
        Txtbr_code.Text = CmbBranch.SelectedValue
        'Dim db As New Connection
        Dim ds As New DataSet
        Dim m_grdbind As New SMARTCORE_BL.Gold_GramRateBL
        ds = m_grdbind.Get_GramRateDetails(Cmbscheme.SelectedValue, txt_date.Text)
    End Sub

    Protected Sub btn_save_Click(sender As Object, e As ImageClickEventArgs) Handles btn_save.Click
        Dim ds As New DataSet
        Dim DS1 As New DataSet
        Dim db As New Connection
        validation()
        Try
            Dim m_grdbind As New SMARTCORE_BL.Gold_GramRateBL
            ds = m_grdbind.Get_GramRateDetails(Cmbscheme.SelectedValue, txt_date.Text)
            DS1 = m_grdbind.Get_EffectDate(Cmbscheme.SelectedValue, txt_date.Text)

           
            Session("EntryMode") = Nothing
            Dim dt As DataTable
            dt = DS1.Tables(0)
            If dt.Rows.Count > 0 Then
                'Dim msg As String
                'Dim title As String
                'Dim style As MsgBoxStyle
                'Dim response As MsgBoxResult
                'msg = " Do You Want To Updated Rate For the Scheme:" & Cmbscheme.SelectedItem.Text & ""
                'style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
                'title = "Confirmation MSG"
                'response = MsgBox(msg, style, title)
                'If response = MsgBoxResult.Yes Then
                Session("EntryMode") = "UPDATE"
                'Else
                '    Exit Sub
                'End If
            Else
                Session("EntryMode") = "NEW"
            End If
            Dim M_GramRate As New SMARTCORE_BO.Gold_GramRateBO
            Dim M_SchCode As String
            Dim d As Date = Date.Parse(txt_date.Text)

            With M_GramRate
                .GgrMinWeight = Integer.Parse(txt_weight.Text)
                .GgrGrmRate = txt_rate.Text
                .GgrBankRatePerc = txtrate.Text
                .GgrMaxEligibility = "100"
                .GgrMinWeightRate = txtweight_rate.Text
                .SchCode = Cmbscheme.SelectedValue.ToString
                .MakerID = Session("UserID")
                .GgrBankRateType = cmbrate.Text
                .GgrEffectDate = txt_date.Text
                .MakingTime = "01/01/1900"
                .Br_Code = Txtbr_code.Text
                .GgrLendingRate = Txt_LendingRate.Text
            End With
            Dim M_Gold_GramRateBL As New Gold_GramRateBL
            M_SchCode = M_Gold_GramRateBL.Save_Data(M_GramRate, Session("EntryMode"))
            M_Gold_GramRateBL = Nothing
            clear()
            txt_date.Text = Session("TransDate")
            bindgrid()

        Catch ex As Exception
        End Try
    End Sub
   
   
    Protected Sub txt_date_TextChanged(sender As Object, e As EventArgs) Handles txt_date.TextChanged
        bindgrid()

    End Sub

    Protected Sub btn_close_Click(sender As Object, e As ImageClickEventArgs) Handles btn_close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub btn_clear_Click(sender As Object, e As ImageClickEventArgs) Handles btn_clear.Click
        clear()
    End Sub
    Private Sub clear()
        txt_rate.Text = ""
        txtrate.Text = ""
        txtweight_rate.Text = ""
        txt_weight.Text = ""
        GvRpt.DataSource = Nothing
        GvRpt.DataBind()

    End Sub
    Private Sub dropchange()
        If cmbrate.SelectedItem.Text = "RATE" Then
            Label1.Text = "Bank Gram Rate"
        ElseIf cmbrate.SelectedItem.Text = "PERCENTAGE" Then
            Label1.Text = "Bank Gram Percentage"
        End If
    End Sub
    Protected Sub cmbrate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbrate.SelectedIndexChanged
        dropchange()
    End Sub

    Protected Sub txt_rate_TextChanged(sender As Object, e As EventArgs) Handles txt_rate.TextChanged
        validation()
    End Sub
    Private Sub validation()

        If cmbrate.SelectedItem.Text = "RATE" Then
            If Val(txtrate.Text) > Val(txt_rate.Text) Then
                ShowAlertMessage("Please Enter Less Than Market Rate")
                txtrate.Text = " "
                txtrate.Focus()


            End If

        ElseIf cmbrate.SelectedItem.Text = "PERCENTAGE" Then
            If Val(txtrate.Text) > 100 Then
                ShowAlertMessage("Please Enter Less Than 100%")
                txtrate.Text = " "
                txtrate.Focus()
           
            End If
        ElseIf Txt_LendingRate.Text.Length <= 0 Then

            ShowAlertMessage("Please Enter Lending Rate")
            Txt_LendingRate.Focus()


        Else

            txtweight_rate.Focus()
        
        End If

    End Sub

    Protected Sub txtrate_TextChanged(sender As Object, e As EventArgs) Handles txtrate.TextChanged
        validation()
    End Sub

    'Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged
    '    Try
    '        If RadioButtonList1.Items(0).Selected Then
    '            Txtbr_code.Visible = False
    '            CmbBranch.Visible = False
    '            LblBr.Visible = False
    '        ElseIf RadioButtonList1.Items(1).Selected Then
    '            Txtbr_code.Visible = True
    '            CmbBranch.Visible = True

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class