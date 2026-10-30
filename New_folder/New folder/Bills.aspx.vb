Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class Bills
    Inherits System.Web.UI.Page
    Public BillTbl As New DataTable("Table")
    Dim FormID As Integer = 1011
    Shared Obj_UserRights As Object

 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("BILLRE", Request.QueryString("TRANID"))
            End If

            Load_DefaultValues()
            TxtDate.Text = Session("TransDate")
            TxtDate.Attributes.Add("readonly", "readonly")
           
        End If
        Session("SearchType") = "ACCOUNTNO"
        Session("SearchBranch") = CmbBranch.SelectedValue
        Session("SearchScheme") = CmbScheme.SelectedValue
        ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        If Session("SearchResult") <> "" Then
            TxtAccNo.Text = Session("SearchResult")
            TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)

            Session("SearchResult") = ""
        End If
    End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("BILLRE", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As New DataTable
        M_TranTbl = M_Dataset.Tables(1)

        Session("TransTbl") = M_Dataset.Tables(0)


        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        DgvBill.DataSource = M_Dataset.Tables(0)
        DgvBill.DataBind()

    End Sub

    Private Sub GetAccountDtls(ByVal Dtbl As DataTable)

        RdoBillType.SelectedValue = Dtbl.Rows(0).Item("Bill_InwardOutward")
        cmbBillType.SelectedValue = Dtbl.Rows(0).Item("Bill_Type")
        CmbInstType.SelectedValue = Dtbl.Rows(0).Item("Bill_InstrumentType")
        TxtBankBc.Text = Dtbl.Rows(0).Item("Bill_BankBC")
        'TxtBankCode.Text = Dtbl.Rows(0).Item("")
        TxtPayeeBank.Text = Dtbl.Rows(0).Item("Bill_PayeeBank")
        TxtDrawer.Text = Dtbl.Rows(0).Item("Bill_Drawer")
        CmbBranch.SelectedValue = Dtbl.Rows(0).Item("Br_Code")
        CmbScheme.SelectedValue = Dtbl.Rows(0).Item("ACC_SchCode")
        TxtAccNo.Text = Dtbl.Rows(0).Item("ACC_No")
        TxtDate.Text = Dtbl.Rows(0).Item("Making_Time")
        TxtInstNo.Text = Dtbl.Rows(0).Item("Bill_InstrumentNo")
        TxtBankBcDate.Text = Dtbl.Rows(0).Item("Bill_BankBCDate")
        'CmbBankName.SelectedValue = Dtbl.Rows(0).Item("Bill_PayeeBank")
        TxtPayeeBranch.Text = Dtbl.Rows(0).Item("Bill_PayeeBranch")
        TxtBearer.Text = Dtbl.Rows(0).Item("Bill_Bearer")
        TxtBillAmt.Text = Dtbl.Rows(0).Item("Bill_Amount")
        TxtInstDate.Text = Dtbl.Rows(0).Item("Bill_InstrumentDate")
        'CmbBankBranch.SelectedValue = Dtbl.Rows(0).Item("")
        CmbCollnBank.SelectedValue = Dtbl.Rows(0).Item("Bill_ColBank")
        TxtRemark.Text = Dtbl.Rows(0).Item("Bill_Remarks")
        TxtAmount.Text = Dtbl.Rows(0).Item("Bill_CollectionAmt")
        TxtRemarks.Text = Dtbl.Rows(0).Item("Bill_ColRemarks")
        cmbBillType.Enabled = False
        CmbInstType.Enabled = False
        TxtBankBc.Enabled = False
        'TxtBankCode.Enabled = False
        TxtPayeeBank.Enabled = False
        TxtDrawer.Enabled = False
        CmbBranch.Enabled = False
        CmbScheme.Enabled = False
        TxtAccNo.Enabled = False
        TxtDate.Enabled = False
        TxtInstNo.Enabled = False
        TxtBankBcDate.Enabled = False
        TxtPayeeBranch.Enabled = False
        TxtBearer.Enabled = False
        TxtBillAmt.Enabled = False
        TxtInstDate.Enabled = False
        CmbCollnBank.Enabled = False
        TxtRemark.Enabled = False
        TxtAccDetls.Enabled = False
        TxtAmount.Enabled = False
        TxtRemarks.Enabled = False
        TxtBranch.Enabled = False
        TxtScheme.Enabled = False
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)


    End Sub


    Private Sub Set_Grid()
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

    End Sub
    Private Sub Clear_Controls()
        TxtAccDetls.Text = ""
        TxtAccNo.Text = ""
        TxtAmount.Text = ""
        TxtBankBc.Text = ""
        TxtBankBcDate.Text = ""
        TxtBankCode.Text = ""
        TxtBearer.Text = ""
        TxtBillAmt.Text = ""
        'TxtBranch.Text = ""
        TxtDate.Text = ""
        TxtDrawer.Text = ""
        TxtInstDate.Text = ""
        TxtInstNo.Text = ""
        TxtPayeeBranch.Text = ""
        TxtPayeeBank.Text = ""
        TxtRemark.Text = ""
        TxtRemarks.Text = ""
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
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), , "SB','CA','OD','SL','SA")
        M_CommonBL.Fill_Pickup(cmbBillType, 55, 0)
        M_CommonBL.Fill_Pickup(CmbInstType, 53, 0)
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        'M_CommonBL.FillBankAccount(CmbCollnBank)

        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call cmbBillType_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbInstType_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call Set_Grid()

    End Sub


    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            If Len(Request.QueryString("TRANID")) > 0 Then
                Dim M_CommonBL1 As New CommonBL
                Dim clscom As New CommonClass
                Dim M_DBConn As New Connection()
                Dim SqlStr As String
                SqlStr = "Exec NonTranction_Authorise_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
                SqlStr = SqlStr & ",'BILLS LODGING','" & Request.QueryString("TRANID") & "','" & Session("UserID") & "'"
                M_DBConn.ExecuteScalar(SqlStr, , False)
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If
            If Obj_UserRights.Add_Right = False Then
                ShowAlertMessage("You Have No Permission To Add New Transaction!!!")
                Exit Sub
            End If

            Dim TType As String
            Dim Bills_Tbl As DataTable
            Dim M_Bill As New BillBO

            If RdoBillType.SelectedValue = "I" Then
                TType = "D"
            ElseIf RdoBillType.SelectedValue = "O" Then
                TType = "C"
            ElseIf RdoBillType.SelectedValue = "D" Then
                TType = "D"
            End If

            Bills_Tbl = DirectCast(Session("BillTbl"), DataTable)

            If Bills_Tbl.Rows.Count = 1 And Val(Bills_Tbl.Rows(0)("TrACC_ID").ToString()) = 0 Then
                ShowAlertMessage("Account No Not Added, Please Check")
                GoTo ExitPoint
            End If

            Dim M_BillsAcSw As New IO.StringWriter
            Bills_Tbl.WriteXml(M_BillsAcSw)

            With M_Bill
                .SLno = "0"
                .BillPresentedDate = TxtDate.Text
                .BillInstrumentDate = TxtInstDate.Text 'Format(, "MM/dd/yyyy")
                .BillSendDate = "01/01/1900" ' Session("TransDate")
                .BillRealisedRtnDate = "01/01/1900" 'M_Dt.Rows(0).Item("Bill_Realised_RtnDate")
                .MakingTime = Session("TransDate")
                .CheckingTime = Session("TransDate")
                .BillBankBCDate = IIf(TxtBankBcDate.Text.Trim.Length > 0, TxtBankBcDate.Text, "01/01/1900")
                .BillAmount = TxtBillAmt.Text
                .BillCollectionAmt = TxtAmount.Text
                .BillCommission = "0"
                .BillOtherBankCommission = "0" 'M_Dt.Rows(0).Item("Bill_OtherBankCommission")
                .BillPostage = "0" 'M_Dt.Rows(0).Item("Bill_Postage")
                .BrCode = Session("DefaultBrCode")
                .SchCode = TxtScheme.Text
                .BillType = cmbBillType.SelectedValue
                .BillNo = "0"
                .BillInstrumentType = CmbInstType.SelectedValue
                .BillTransNo = "0"
                .BillColBankBranch = CmbBankBranch.SelectedValue
                .BillColRemarks = "Nil" 'M_Dt.Rows(0).Item("Bill_ColRemarks")
                .BillDrawer = TxtDrawer.Text
                .BillRemarks = TxtRemark.Text
                .BillRealesedRemarks = "N" 'M_Dt.Rows(0).Item("Bill_RealesedRemarks")
                .MakerID = Session("LoginId")
                .CheckerID = Session("LoginId")
                .BillColBank = CmbCollnBank.SelectedValue
                .BillInstrumentNo = TxtInstNo.Text
                .BillBankBC = TxtBankBc.Text
                .BillOtherBank = "Nil" 'M_Dt.Rows(0).Item("Bill_OtherBank")
                .BillCollectBankBranch = "0"
                .BillPayeeBank = TxtPayeeBank.Text.Trim
                .BillPayeeBranch = TxtPayeeBranch.Text.Trim()
                .BillBearer = TxtBearer.Text
                .BillInwardOutward = RdoBillType.SelectedValue.ToString()
                .BillStatus = "A"
                .Authorised = "Y"
                .BillReturned = "N"

            End With

            Dim M_BillBL As New BillBL
            Dim BillNo As String
            Dim TranNo As String
            Dim SchCode As String = ""
            Dim M_Dt As New DataTable

            Session("M_EntryMode") = "NEW"
            M_Dt = M_BillBL.Save_Data(M_Bill, Session("M_EntryMode"), M_BillsAcSw.ToString.Replace("DocumentElement", "NewDataSet"))
           
            If M_Dt.Rows.Count >= 1 Then
                BillNo = M_Dt.Rows(0)("Acc_No").ToString
                TranNo = M_Dt.Rows(0)("Tran_No").ToString
                If Val(BillNo) > 0 Then
                    ShowAlertMessage("Bill No : " & BillNo & " Saved Successfully")
                    Dim SchTbl As DataTable = DirectCast(HttpContext.Current.Cache("SchemeTbl"), DataTable)
                    If Left(RdoBillType.SelectedValue.ToString(), 1) = "O" Then
                        SchTbl = SchTbl.Select("Sch_Module_Catg='OUTWARD'").CopyToDataTable
                        If SchTbl.Rows.Count >= 1 Then
                            SchCode = SchTbl.Rows(0)("Sch_Code").ToString
                        End If
                    ElseIf Left(RdoBillType.SelectedValue.ToString(), 1) = "I" Then
                        SchTbl = SchTbl.Select("Sch_Module_Catg='INWARD'").CopyToDataTable
                        If SchTbl.Rows.Count >= 1 Then
                            SchCode = SchTbl.Rows(0)("Sch_Code").ToString
                        End If
                    End If
                    If SchCode.Length > 0 Then Session("AcOpenScheme") = SchCode
                    Response.Redirect("~/NewAccount.aspx?AccNo=" & BillNo & "&TransId=" & TranNo & "&PrintBond=N" & "&SECTION=BILLREG" & "&SCHEME=" & SchCode & " &SEC=BILLREG" & "&DRCR=" & "C")

                End If
                Clear_Controls()
                Set_Grid()
            End If
ExitPoint:
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
        Set_Grid()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbInstType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbInstType.SelectedIndexChanged

    End Sub

    Public Function GetAccountTranDtls(ByVal BrCode As String, ByVal SchCode As String, ByVal AccNo As String, ByVal AsonDate As Date) As DataTable
        Dim M_CommonDA As New CommonDA
        Return M_CommonDA.GetAccountTranDtls(BrCode, SchCode, AccNo, AsonDate)
    End Function

    Protected Sub cmbBillType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbBillType.SelectedIndexChanged
        Try
            Dim M_BillsBL As New BillBL
            M_BillsBL.Fill_BillCollBranch("%", CmbCollnBank)

            M_BillsBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dim M_AccNo As String = TxtAccNo.Text
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            ' TxtCustID.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
            Session("BillAccID") = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            HdnCustName.Value = Dt_AcDtls.Rows(0)("Cust_Name").ToString() & "(" & M_AccNo & ")"
            TxtAccDetls.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString() & Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            'TxtMblNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString() & Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            'TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            'Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
            'Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
            'TxtCurBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
            'TxtShadowBal.Text = Format(Val(CStr(Math.Abs(ShadowBal))), "#0.00") & " " & IIf(ShadowBal < 0, "Dr", "Cr")
            'TxtPendingDr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()), "#0.00")
            'TxtPendingCr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()), "#0.00")
            'AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            'For Loading Initial Values
            'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

        Else

            ShowAlertMessage("INVALID ACCOUNT NO")
        End If

        M_CommonBL = Nothing
        'If Session("CustomerId").ToString().Length > 0 Then CmbTransType.Focus()
    End Sub
    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtBillAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBillAmt.TextChanged
        TxtAmount.Text = Val(TxtBillAmt.Text)
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAdd.Click
        Dim TType As String = ""
        If RdoBillType.SelectedValue = "I" Then
            TType = "D"
        ElseIf RdoBillType.SelectedValue = "O" Then
            TType = "C"
        ElseIf RdoBillType.SelectedValue = "D" Then
            TType = "D"
        End If
        Dim BillTbl As DataTable = DirectCast(Session("BillTbl"), DataTable)
        Dim Row As DataRow

        Row = BillTbl.NewRow
        Row("Bill_ID") = "0"
        Row("TrACC_ID") = Session("BillAccID")
        Row("Name") = HdnCustName.Value
        Row("Tran_Type") = TType
        Row("Amount") = Val(TxtAmount.Text)
        Row("Narration") = TxtRemarks.Text

        Try

            Dim Expression As String = "Amount=0"
            If BillTbl.[Select](Expression).Count > 0 Then BillTbl.Rows.RemoveAt(BillTbl.Rows.IndexOf(BillTbl.[Select](Expression)(0)))

        Catch ex As Exception

        End Try
        BillTbl.Rows.Add(Row)
        DgvBill.DataSource = BillTbl
        DgvBill.DataBind()
        Session("BillTbl") = BillTbl
        ClearDepDtls()

    End Sub



    Private Sub ClearDepDtls()
        TxtAccNo.Text = ""
    End Sub

    'Private Sub Set_Grid()

    '    MultipleDepTbl.Columns.Add("Dep_Mth", GetType(Double))
    '    MultipleDepTbl.Columns.Add("Dep_Days", GetType(Double))
    '    MultipleDepTbl.Columns.Add("Dep_DueDt", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_Amt", GetType(Double))
    '    MultipleDepTbl.Columns.Add("Dep_IntRate", GetType(Double))
    '    MultipleDepTbl.Columns.Add("Dep_IntPayMode", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_NomNm", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_NomRel", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_NomAdd", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_Standing", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_IntPayModeCode", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_StndBr", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_StndSch", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_StndAccNo", GetType(String))
    '    MultipleDepTbl.Columns.Add("Dep_NomRelCode", GetType(Double))
    '    MultipleDepTbl.Columns.Add("Dep_NomPh", GetType(String))
    '    MultipleDepTbl.Rows.Add(0, 0, "01/01/1900", 0, 0, "", "", "", "", "", "", "", "", "", 0, "")

    '    Session("BillTbl") = MultipleDepTbl
    '    DgvDepDtls.DataSource = MultipleDepTbl
    '    DgvDepDtls.DataBind()

    '    'End Sub

    Protected Sub TxtInstDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInstDate.TextChanged

    End Sub

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList_PayeeBank(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()

        If True Then
            Dim PickupDt As DataTable = DirectCast(System.Web.HttpRuntime.Cache("PickupTbl"), DataTable)
            Dim Expression As String = "Pkc_Type = 88 and Pkc_Description LIKE '" & prefixText & "%'"
            Dim SortingOrder As String = "Pkc_Description ASC"
            Dim drFiltered As DataRow() = PickupDt.[Select](Expression, SortingOrder)
            Dim CompletionSet = New List(Of String)()
            For Each row As DataRow In drFiltered
                CompletionSet.Add(row("Pkc_Description").ToString())
            Next
            Return CompletionSet.ToArray()
        End If

    End Function

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList_PayeeBranch(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()

        If True Then
            Dim PickupDt As DataTable = DirectCast(System.Web.HttpRuntime.Cache("PickupTbl"), DataTable)
            Dim Expression As String = "Pkc_Type = 83 and Pkc_Description LIKE '" & prefixText & "%'"
            Dim SortingOrder As String = "Pkc_Description ASC"
            Dim drFiltered As DataRow() = PickupDt.[Select](Expression, SortingOrder)
            Dim CompletionSet = New List(Of String)()
            For Each row As DataRow In drFiltered
                CompletionSet.Add(row("Pkc_Description").ToString())
            Next
            Return CompletionSet.ToArray()
        End If

    End Function

End Class