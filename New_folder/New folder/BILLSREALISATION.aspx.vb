Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Partial Public Class BILLSREALISATION
    Inherits System.Web.UI.Page
    Public BillTbl As New DataTable("Table")
    Dim FormID As Integer = 1014
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Load_DefaultValues()
                Panel1.Visible = False
                Call cmbBillType_SelectedIndexChanged(Me.cmbBillType, System.EventArgs.Empty)
                If Len(Request.QueryString("TRANID")) > 0 Then
                    GetAuthoriseDtls("BLR", Request.QueryString("TRANID"))
                Else
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("BLR", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(1)
        DgvTransaction.DataSource = M_Dataset.Tables(0)
        Session("TransTbl") = M_Dataset.Tables(0)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        'Calc_TranTotal()
        DgvBill.DataSource = M_Dataset.Tables(2)
        DgvBill.DataBind()
    End Sub
    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)
        DivBillDetails.Visible = True
        cmbBillType.SelectedValue = M_AccTbl.Rows(0).Item("Bill_InwardOutward")
        TxtBillNo.Text = M_AccTbl.Rows(0).Item("Bill_No")
        Panel1.Visible = True
        CmbBillNo.Visible = False
        CmbInstType.SelectedValue = M_AccTbl.Rows(0).Item("Bill_InstrumentType")
        TxtInstNo.Text = M_AccTbl.Rows(0).Item("Bill_InstrumentNo")
        TxtInstDate.Text = M_AccTbl.Rows(0).Item("Bill_InstrumentDate")
        TxtDrawer.Text = M_AccTbl.Rows(0).Item("Bill_Drawer")
        TxtPayeeBank.Text = M_AccTbl.Rows(0).Item("Bill_PayeeBank")
        TxtBillSendDate.Text = M_AccTbl.Rows(0).Item("Bill_SendDate")
        TxtBankBc.Text = M_AccTbl.Rows(0).Item("Bill_BankBc")
        TxtBankBcDate.Text = M_AccTbl.Rows(0).Item("Bill_BankBCDate")
        TxtBillAmt.Text = M_AccTbl.Rows(0).Item("Bill_Amount")
        CmbCollnBank.SelectedValue = M_AccTbl.Rows(0).Item("Bill_ColBank")
        TxtBearer.Text = M_AccTbl.Rows(0).Item("Bill_Bearer")
        TxtPayeeBranch.Text = M_AccTbl.Rows(0).Item("Bill_PayeeBranch")
        TxtRemark.Text = M_AccTbl.Rows(0).Item("Bill_Remarks")
        cmbStatus.SelectedItem.Text = M_AccTbl.Rows(0).Item("Status")
        TxtRemarkStatus.Text = M_AccTbl.Rows(0).Item("Bill_ColRemarks")
        TxtCollectedAmt.Text = M_AccTbl.Rows(0).Item("Bill_CollectionAmt")
        TxtCommAmt.Text = M_AccTbl.Rows(0).Item("Bill_Commission")
        TxtOtherbankCommAmt.Text = M_AccTbl.Rows(0).Item("Bill_OtherBankCommission")
        TxtPostageAmt.Text = M_AccTbl.Rows(0).Item("Bill_Postage")
        AuthoriseEnabled()
    End Sub
    Private Sub AuthoriseEnabled()
        cmbBillType.Enabled = False
        CmbBillNo.Enabled = False
        TxtBillNo.Enabled = False
        CmbInstType.Enabled = False
        TxtInstNo.Enabled = False
        TxtInstDate.Enabled = False
        TxtDrawer.Enabled = False
        TxtPayeeBank.Enabled = False
        TxtBillSendDate.Enabled = False
        TxtBankBc.Enabled = False
        TxtBankBcDate.Enabled = False
        TxtBillAmt.Enabled = False
        CmbCollnBank.Enabled = False
        TxtBearer.Enabled = False
        TxtPayeeBranch.Enabled = False
        TxtRemark.Enabled = False
        cmbStatus.Enabled = False
        TxtRemarkStatus.Enabled = False
        TxtCollectedAmt.Enabled = False
        TxtCommAmt.Enabled = False
        TxtOtherbankCommAmt.Enabled = False
        TxtPostageAmt.Enabled = False
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_BillsBL As New BillBL

            M_CommonBL.Fill_Pickup(CmbInstType, 53, 0)

            M_BillsBL.Fill_BillCollBranch("%", CmbCollnBank)

            cmbStatus.SelectedIndex = 1

            M_BillsBL = Nothing
            Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_Grid()
        Try
            DgvBill.DataSource = Nothing
            BillTbl.Columns.Add("Bill_ID", GetType(Double))
            BillTbl.Columns.Add("TrACC_ID", GetType(Double))
            BillTbl.Columns.Add("Name", GetType(String))
            BillTbl.Columns.Add("Tran_Type", GetType(String))
            BillTbl.Columns.Add("Amount", GetType(Double))
            BillTbl.Columns.Add("Narration", GetType(String))
            BillTbl.Rows.Add("0", "0", "", "", "0", "")
            Session("BillTbl") = BillTbl
            DgvBill.DataSource = BillTbl
            DgvBill.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub cmbBillType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbBillType.SelectedIndexChanged
        Try
            Dim M_BillsBL As New BillBL
            M_BillsBL.Fill_PendingBills(Session("DefaultBrCode"), CmbBillNo, IIf(cmbBillType.SelectedValue = "I", "I", "O"))
            If CmbBillNo.Items.Count > 0 Then
                DivBillDetails.Visible = True
                Call CmbBillNo_SelectedIndexChanged(Me.CmbBillNo, System.EventArgs.Empty)
                Call cmbStatus_SelectedIndexChanged(Me.cmbStatus, System.EventArgs.Empty)
            Else
                DivBillDetails.Visible = False
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



    Protected Sub CmbBillNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBillNo.SelectedIndexChanged
        Try
            Dim M_BillsBL As New BillBL
            Dim BillDs As DataSet
            BillDs = M_BillsBL.Fetch_PendingBills(CmbBillNo.SelectedValue)

            DgvBill.DataSource = BillDs.Tables(1)
            DgvBill.DataBind()

            'M_BillsBL.Fill_BillCollBranch(Session("DefaultBrCode"), CmbCollnBank)

            Session("BIll_ID") = BillDs.Tables(0).Rows(0).Item("Bill_ID").ToString()

            M_BillsBL = Nothing

            CmbInstType.SelectedValue = BillDs.Tables(0).Rows(0).Item("Bill_InstrumentType").ToString()
            TxtInstNo.Text = BillDs.Tables(0).Rows(0).Item("Bill_InstrumentNo").ToString()
            TxtInstDate.Text = Left(BillDs.Tables(0).Rows(0).Item("Bill_InstrumentDate").ToString(), 10)
            TxtBankBc.Text = BillDs.Tables(0).Rows(0).Item("Bill_BankBC").ToString()
            If Left(BillDs.Tables(0).Rows(0).Item("Bill_BankBCDate").ToString(), 10) = "01/01/1900" Then
                TxtBankBcDate.Text = ""
            Else
                TxtBankBcDate.Text = Left(BillDs.Tables(0).Rows(0).Item("Bill_BankBCDate").ToString(), 10)
            End If
            TxtBillAmt.Text = BillDs.Tables(0).Rows(0).Item("Bill_Amount").ToString()
            TxtPayeeBank.Text = BillDs.Tables(0).Rows(0).Item("Bill_PayeeBank").ToString()
            TxtPayeeBranch.Text = BillDs.Tables(0).Rows(0).Item("Bill_PayeeBranch").ToString()
            CmbCollnBank.SelectedValue = BillDs.Tables(0).Rows(0).Item("Bill_ColBank").ToString()
            TxtDrawer.Text = BillDs.Tables(0).Rows(0).Item("Bill_Drawer").ToString()
            TxtBearer.Text = BillDs.Tables(0).Rows(0).Item("Bill_BeareR").ToString()
            TxtRemark.Text = BillDs.Tables(0).Rows(0).Item("Bill_Remarks").ToString()
            TxtBillSendDate.Text = Left(BillDs.Tables(0).Rows(0).Item("Bill_SendDate").ToString(), 10)
            TxtCollectedAmt.Text = TxtBillAmt.Text
            TxtCollectedAmt_TextChanged(Me.TxtCollectedAmt, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")

        ' ShowAlertMessage(translateWholeNumber("100000"))

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_BillBL As New BillBL
            Dim M_Dt As New DataTable
            Dim Tran_No As String
            If Len(Request.QueryString("TRANID")) > 0 Then

                If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx.vb")
                    Exit Sub
                End If
                M_CommonBL = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If
            Dim Trandt As DataTable = DirectCast(Session("BillsCollDt"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)
            M_Dt = M_CommonBL.Update_AccNoTran(Session("DefaultBrCode"), "", Session("TransDate"), Session("DefaultBrCode"), " Bill No : " & CmbBillNo.SelectedItem.ToString() & "-" & CmbInstType.SelectedItem.Text & " :" & TxtInstNo.Text, Session("LoginID"), 0, "", M_TranSw.ToString, , , "BLR", , , , , , , , "BLR", "BLR", "Transfer")
            If M_Dt.Rows.Count > 0 Then
                Tran_No = M_Dt.Rows(0)("Tran_No").ToString()

                M_BillBL.Update_BillsRealisation(Session("BIll_ID"), Tran_No, Session("LoginID"), Session("TransDate"), IIf(cmbStatus.SelectedItem.ToString = "REALISED", "Y", "R"), TxtRemarkStatus.Text)

                ShowAlertMessage("UPDATED")

                Dim SchCode As String = ""
                Dim SchTbl As DataTable = DirectCast(HttpContext.Current.Cache("SchemeTbl"), DataTable)
                If IIf(cmbBillType.SelectedValue = "I", "I", "O") = "O" Then
                    SchTbl = SchTbl.Select("Sch_Module_Catg='OUTWARD'").CopyToDataTable
                    If SchTbl.Rows.Count >= 1 Then
                        SchCode = SchTbl.Rows(0)("Sch_Code").ToString
                    End If
                ElseIf IIf(cmbBillType.SelectedValue = "I", "I", "O") = "I" Then
                    SchTbl = SchTbl.Select("Sch_Module_Catg='INWARD'").CopyToDataTable
                    If SchTbl.Rows.Count >= 1 Then
                        SchCode = SchTbl.Rows(0)("Sch_Code").ToString
                    End If
                End If
                If SchCode.Length > 0 Then Session("AcOpenScheme") = SchCode
                Session("AcOpenBranch") = Session("DefaultBrCode")
                Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbBillNo.SelectedItem.Text & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "BILL_REALIS" & "&SCHEME=" & SchCode & "&SEC=" & "BILLS" & "&DRCR=B" & "&TRANCATG=" & "TRAN")
            Else
                ShowAlertMessage("FAILED")
            End If

            M_BillBL = Nothing
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub cmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try
            'If cmbStatus.SelectedItem.ToString = "REALISED" Then
            '    DivCollDtails.Visible = True
            '    DivCollDtailsReturned.Visible = False
            'Else
            '    DivCollDtails.Visible = False
            '    DivCollDtailsReturned.Visible = True
            'End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub ShowDts()
        Try
            Dim M_BillsBL As New BillBL
            Dim BillDt As DataTable

            BillDt = M_BillsBL.Bills_SetData(Session("TransDate"), CmbBillNo.SelectedValue, cmbBillType.SelectedValue, Val(TxtCollectedAmt.Text), Val(TxtCommAmt.Text), Val(TxtPostageAmt.Text), Val(TxtServiceTaxAmt.Text), IIf(cmbStatus.SelectedItem.ToString = "REALISED", "Y", "R"))
            
            DgvTransaction.DataSource = BillDt
            DgvTransaction.DataBind()
            Session("BillsCollDt") = BillDt
            ' Get_Total()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click

    End Sub

    Protected Sub TxtCollectedAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCollectedAmt.TextChanged
        ShowDts()
    End Sub

    Protected Sub TxtPostageAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPostageAmt.TextChanged
        ShowDts()
    End Sub

    Protected Sub TxtCommAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCommAmt.TextChanged
        ShowDts()
    End Sub
End Class