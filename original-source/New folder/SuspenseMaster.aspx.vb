Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class SuspenseMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1139
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


            TxtOpDate.Attributes.Add("readonly", "readonly")
            TxtIntRate.Attributes.Add("readonly", "readonly")
            TxtOpDate.Text = Session("TransDate")
            Session("M_EntryMode") = "NEW"
          
            If Len(Request.QueryString("TRANID")) > 0 Then
                Session("AcOpenScheme") = "91"
                Assign_Values()
                GetAuthoriseDtls("SUSPMAS", Request.QueryString("TRANID"))
            Else
                Assign_Values()
            End If

        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("SUSPMAS", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        DgvCustomer.DataSource = M_Dataset.Tables(1)
        DgvCustomer.DataBind()
        DgvTransaction.DataSource = M_Dataset.Tables(2)
        Session("TransTbl") = M_Dataset.Tables(2)
        DgvTransaction.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl)
        PnlAuthorise.Enabled = False

    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        TxtOpDate.Text = M_AccTbl.Rows(0).Item("Admit_date")
        TxtInitialDep.Text = M_AccTbl.Rows(0).Item("Int_Amt")
        CmbCashTransfer.SelectedValue = M_AccTbl.Rows(0).Item("Cash")
        If M_AccTbl.Rows(0).Item("Sch_Type6").ToString = "DEB" Then
            Panel2.Visible = True
            TxtIntRate.Text = M_AccTbl.Rows(0).Item("IntRate")
            TxtMonth.Text = M_AccTbl.Rows(0).Item("Month")
            DrpIntPayMode.SelectedValue = M_AccTbl.Rows(0).Item("IntPayMode")
            DrpCanvas.SelectedValue = Val(M_AccTbl.Rows(0).Item("CanvasedBy"))
            TxtDays.Text = M_AccTbl.Rows(0).Item("Days")

        Else
            Panel2.Visible = False
        End If

    End Sub

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>" & vbLf
        strDisAbleBackButton += "window.history.forward(1);" & vbLf
        strDisAbleBackButton += vbLf & "</script>"
        ClientScript.RegisterClientScriptBlock(Me.Page.[GetType](), "clientScript", strDisAbleBackButton)
    End Sub
    Private Sub Assign_Values()

        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
        CmbBatchNo.SelectedIndex = 0
        M_CommonBL.Fill_Pickup(DrpIntPayMode, 44, 0)
        M_CommonBL.Fill_CanvassedBy(DrpCanvas)

        ''----Bank Details
        M_CommonBL.Fill_Pickup(drpTranferType, 108, 0)
        M_CommonBL.GetBranchBankDetails(drpBank, Session("DefaultBrCode").ToString())
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
        DdlTranType.Items.Add("<--Select-->")
        DdlTranType.Items.Add("Credit")
        DdlTranType.Items.Add("Debit")
        DdlTranType.SelectedIndex = 0
        dvRTGS.Visible = False
        ''----Bank Details

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        DgvCustomer.DataSource = dt
        DgvCustomer.DataBind()
        Dim HeadDt As DataTable
        HeadDt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        Session("locAcHeadTbl") = HeadDt
        DgvTransaction.DataSource = HeadDt
        DgvTransaction.DataBind()
        Session("TransTbl") = HeadDt
        CmbCashTransfer.Items.Add("CASH")
        CmbCashTransfer.Items.Add("TRANSFER")
        CmbCashTransfer.SelectedIndex = 0
        M_Dt = M_CommonBL.Get_SchemeMaster(Session("AcOpenScheme"))
        If Not (M_Dt.Rows(0).Item("Sch_Type6").ToString) Is Nothing Then
            If M_Dt.Rows(0).Item("Sch_Type6").ToString() = "DEB" Then
                Panel2.Visible = True
            End If
        End If
        TxtMonth.Text = 0
        TxtIntRate.Text = 0
        TxtDays.Text = 0
    End Sub
    Private Sub Set_TranAmt()
        Dim dt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
        Dim Sql As String = ""
        If CmbCashTransfer.SelectedIndex = 0 Then
            If Session("MODULE") = "SL" Then
                dt.Rows(0)("RctCash") = Val(TxtInitialDep.Text)
                dt.Rows(0)("PayCash") = 0
            Else
                dt.Rows(0)("RctCash") = 0
                dt.Rows(0)("PayCash") = Val(TxtInitialDep.Text)
            End If

            dt.Rows(0)("RctTransfer") = 0
            dt.Rows(0)("PayTransfer") = 0
        Else
            If Session("MODULE") = "SL" Then
                dt.Rows(0)("RctTransfer") = Val(TxtInitialDep.Text)
                dt.Rows(0)("PayTransfer") = 0
            Else
                dt.Rows(0)("RctTransfer") = 0
                dt.Rows(0)("PayTransfer") = Val(TxtInitialDep.Text)
            End If
            dt.Rows(0)("RctCash") = 0
            dt.Rows(0)("PayCash") = 0
        End If
        DgvTransaction.DataSource = dt
        DgvTransaction.DataBind()
        Session("TransTbl") = dt
        Dim objSum As Object
        If dt.Rows.Count > 0 Then
            objSum = dt.Compute("Sum(RctCash)", "")
            LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = dt.Compute("Sum(RctTransfer)", "")
            LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = dt.Compute("Sum(PayCash)", "")
            LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = dt.Compute("Sum(PayTransfer)", "")
            LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
        End If


    End Sub

    Protected Sub TxtInitialDep_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInitialDep.TextChanged
        Set_TranAmt()
        Dim M_CommonCls As New CommonClass
        If Val(TxtInitialDep.Text) > 0 Then
            LblWords.Text = M_CommonCls.Towords(Val(TxtInitialDep.Text)).ToUpper
            M_CommonCls = Nothing
            TxtInitialDep.Text = Format(Val(TxtInitialDep.Text), "#0.00")
        End If

        
    End Sub

    
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Dim M_CommonBL As New CommonBL
        Dim M_BatchNo As String = ""
        If Not ((Val(Session("MaxTranLimit").ToString) >= (Val(LblRctCashTotal.Text) + Val(LblRctTransferTotal.Text) + Val(LblPayCashTotal.Text) + Val(LblPayTransferTotal.Text)))) Then
            ShowAlertMessage("Maximum Transaction Limit is :" + Session("MaxTranLimit").ToString())
            Exit Sub
        End If
        If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

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


        If ChkBank.Checked = True Then
            If DdlTranType.SelectedIndex = 0 Then
                ShowAlertMessage("Please select Bank Transaction....")
                DdlTranType.Focus()
                Exit Sub
            End If
            If Val(IIf(TxtBankAmnt.Text = "", "0", TxtBankAmnt.Text)) <= 0 Then
                ShowAlertMessage("Please Enter Bank Amnount....")
                TxtBankAmnt.Focus()
                Exit Sub
            End If
        End If

        Dim M_SuspMaster As New SMARTCORE_BO.SuspMasterBO
        With M_SuspMaster
            .AccID = 0
            .AccNo = 0
            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
            .BrCode = CmbBranch.SelectedValue
            .SchCode = CmbScheme.SelectedValue
            .RefAccid = 0
            .ShareRequired = IIf(ChkMember.Checked = True, 1, 0)
            .AdmitDate = TxtOpDate.Text
            .SmLastUpdate = TxtOpDate.Text
            .SmClosedDate = "01/01/1900"
            .MakingTime = DateTime.Now
            .CheckerTime = "01/01/1900"
            .SmOpBalance = 0
            .SmNarration = TxtRemarks.Text
            .TranNo = 0
            .MakerID = Session("UserID")
            .CheckerID = ""
            .SmOpBalType = "C"
            .SmStatus = "Y"
            .Authorised = "N"
            .DebIntayMode = DrpIntPayMode.SelectedValue.ToString
            .DebIntRate = Val(TxtIntRate.Text)
            .DebMonth = TxtMonth.Text
            .DebCanvas = IIf(DrpCanvas.SelectedValue = "<-- Select -->", 0, Val(DrpCanvas.SelectedValue))
            .DebDays = TxtDays.Text
            .OperationMode = Val(Session("AcOpenOpMode"))
        End With
        Try
            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            Dim M_SuspMasterBL As New SuspMasterBL
            Dim M_Dt As DataTable

            M_Dt = M_SuspMasterBL.Save_Data(M_SuspMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), M_BatchNo)
            ShowAlertMessage("SUSPENSE NO : " & M_Dt.Rows(0)("Acc_No").ToString)

            ''*********Bank Details**********
            If CmbCashTransfer.SelectedIndex = 1 And ChkBank.Checked = True Then
                Dim M_LoanMasterBL1 As New LoanMasterBL
                Dim dtTransferDetails As New DataTable
                dtTransferDetails = M_LoanMasterBL1.SaveTransferDetails_Data(M_Dt.Rows(0)("Tran_No").ToString, drpBank.SelectedItem.ToString(), drpbankAccno.SelectedItem.Text.ToString(), TxtCheque.Text.ToString(), txtChequeDate.Text.ToString(), drpTranferType.SelectedValue.ToString(), txtIFSC.Text.ToString(), txtaccno.Text.ToString(), txtCommission.Text.ToString(), Session("TransDate"), Val(TxtBankAmnt.Text), IIf(DdlTranType.SelectedIndex = 1, "C", "D"))
            End If
            ''*********Bank Details**********

            Session("NomTbl") = Nothing
            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=SUSPENSE" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=SUSPENSE" & "&DRCR=" & "C")

            M_SuspMasterBL = Nothing
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
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(drpbankAccno, drpBank.SelectedValue.ToString())
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Set_TranAmt()

        If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
            CmbBatchNo.Enabled = False
            ImgBatch.Enabled = False
            ChkBank.Enabled = False
            ChkBank.Checked = False
            PnlBank.Visible=False
        Else
            CmbBatchNo.Enabled = True
            ImgBatch.Enabled = True
            ChkBank.Enabled = True
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
    End Sub

    Private Sub CmbBatchNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBatchNo.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            If CmbBatchNo.SelectedItem.Text <> "-- NEW --" Then
                M_Dt = M_CommonBL.Get_BatchTotal(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo.SelectedItem.Text)
                If M_Dt.Rows.Count > 0 Then
                    LblDebit.Text = Format(Val(M_Dt.Rows(0)("Debit").ToString), "#0.00") & " Dr"
                    LblCredit.Text = Format(Val(M_Dt.Rows(0)("Credit").ToString), "#0.00") & " Cr"
                End If
            Else
                LblDebit.Text = ""
                LblCredit.Text = ""
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub ChkBank_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkBank.CheckedChanged
        Try
            If ChkBank.Checked = True Then
                PnlBank.Visible = True
            Else
                PnlBank.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtMonth_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMonth.TextChanged
        Try
            Calc_InterestRate()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Calc_InterestRate(Optional ByVal ForSplitAc As Boolean = False)
        Try
            Dim M_CommonBL As New CommonBL
            Dim Deb_Schm As String
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBL.Get_SuspenseShareScheme(CmbScheme.SelectedValue)
            Deb_Schm = M_Dt.Rows(0).Item("Share_Sch_Code").ToString()

            TxtIntRate.Text = Format(M_CommonBL.Get_Intr_Rate(Session("TransDate"), Deb_Schm, "", Val(TxtDays.Text) + Val(TxtMonth.Text), _
                        IIf(Val(TxtMonth.Text) > 0, "M", "D"), Val(TxtInitialDep.Text), CommonBL.Intr_RateTypes.INTR_RATE_NORMAL, "", DrpIntPayMode.SelectedValue.ToString()), "#0.00")

            If Session("AcOpenPrivelage") <> CommonBL.Intr_RateTypes.INTR_RATE_NORMAL Then
                TxtIntRate.Text = Format(Val(TxtIntRate.Text) + M_CommonBL.Get_Intr_Rate(Session("TransDate"), Deb_Schm, "", Val(TxtDays.Text) + Val(TxtMonth.Text), _
                        IIf(Val(TxtMonth.Text) > 0, "M", "D"), Val(TxtInitialDep.Text), Session("AcOpenPrivelage"), "", DrpIntPayMode.SelectedValue.ToString()), "#0.00")
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub TxtDays_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDays.TextChanged
        Try
            Calc_InterestRate()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DrpIntPayMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpIntPayMode.SelectedIndexChanged
        Try
            Calc_InterestRate()
        Catch ex As Exception
        End Try
    End Sub
End Class