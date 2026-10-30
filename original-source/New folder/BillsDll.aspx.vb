Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class Bills_Dll
    Inherits System.Web.UI.Page
    Public BillTbl As New DataTable("Table")
    Dim FormID As Integer = 1013

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Load_DefaultValues()
           
        End If
       
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
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(cmbBillType, 55, 0)
        M_CommonBL.Fill_Pickup(CmbInstType, 53, 0)
        'M_CommonBL.FillBankAccount(CmbCollnBank)

        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call cmbBillType_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbInstType_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call Set_Grid()
    End Sub


    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
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
        Try


            Bills_Tbl = DirectCast(Session("BillTbl"), DataTable)
            Dim M_BillsAcSw As New IO.StringWriter
            Bills_Tbl.WriteXml(M_BillsAcSw)

            With M_Bill
                .SLno = "0"
                .BillPresentedDate = "01/01/1900"
                .BillInstrumentDate = TxtInstDate.Text 'Format(, "MM/dd/yyyy")
                .BillSendDate = Session("TransDate")
                .BillRealisedRtnDate = "01/01/1900" 'M_Dt.Rows(0).Item("Bill_Realised_RtnDate")
                .MakingTime = Session("TransDate")
                .CheckingTime = Session("TransDate")
                .BillBankBCDate = TxtBankBcDate.Text
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
                .MakerID = Session("Login")
                .CheckerID = Session("Login")
                .BillColBank = CmbCollnBank.SelectedValue
                .BillInstrumentNo = TxtInstNo.Text
                .BillBankBC = TxtBankBc.Text
                .BillOtherBank = "Nil" 'M_Dt.Rows(0).Item("Bill_OtherBank")
                .BillCollectBankBranch = "0"
                .BillPayeeBank = TxtPayeeBank.Text
                .BillBearer = TxtBearer.Text
                .BillInwardOutward = RdoBillType.SelectedValue.ToString()
                .BillStatus = "A"
                .Authorised = "Y"
                .BillReturned = "N"

            End With

            Dim M_BillBL As New BillBL
            Dim BillNo As String
            Session("M_EntryMode") = "NEW"
            'BillNo = M_BillBL.Save_Data(M_Bill, Session("M_EntryMode"), M_BillsAcSw.ToString.Replace("DocumentElement", "NewDataSet"))
            If Val(BillNo) > 0 Then
                ShowAlertMessage("Bill No : " & BillNo & " Saved Successfully")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbInstType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbInstType.SelectedIndexChanged

    End Sub

    Public Function GetAccountTranDtls(ByVal BrCode As String, ByVal SchCode As String, ByVal AccNo As String, ByVal AsonDate As Date) As DataTable
        Dim M_CommonDA As New CommonDA
        Return M_CommonDA.GetAccountTranDtls(BrCode, SchCode, AccNo, AsonDate)
    End Function

    Protected Sub cmbBillType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbBillType.SelectedIndexChanged

        Dim M_BillsBL As New BillBL
        M_BillsBL.Fill_BillCollBranch(Session("DefaultBrCode"), CmbCollnBank)

        M_BillsBL = Nothing




        'Dim M_CommonBL As New CommonBL
        'Dim M_DBConn As New Connection
        'Dim M_Dt As New DataTable()
        'Dim Sql_String As String
        'If cmbBillType.SelectedItem.ToString() = "" Then Exit Sub
        'If cmbBillType.SelectedItem.ToString() <> "B" Then
        '    Sql_String = "SELECT PickupCode_P_Tbl.Pkc_Description , Gen_BankMaster_M_Tbl.Bank_Name, Gen_BankMaster_M_Tbl.Bank_Parent_Id,Gen_BankMaster_M_Tbl.Bank_AccCode, " & _
        '                 "Gen_BankMaster_M_Tbl.Bank_Code FROM Gen_BankMaster_M_Tbl INNER JOIN " & _
        '                 "PickupCode_P_Tbl ON Gen_BankMaster_M_Tbl.Bank_Type = PickupCode_P_Tbl.Pkc_Code " & _
        '                "WHERE (Gen_BankMaster_M_Tbl.Bank_Type = " & cmbBillType.SelectedValue.ToString() & ") AND (Gen_BankMaster_M_Tbl.Bank_Bk_Type = 'B')"
        '    M_DBConn.ExecuteDataTable(M_Dt, Sql_String, False)
        '    If M_Dt.Rows.Count > 0 Then


        '        Fill_Combo(CmbCollnBank, M_Dt, "Bank_Name", "Bank_AccCode")

        '    End If
        'Else
        '    M_CommonBL.Fill_Branch(CmbBankBranch)
        'End If



        ' ''Sql_String = " SELECT    ACC_ID, Ba_BankBranch+'-' + Ba_AccountNo as AccountDetails,Ba_AccountNo FROM         BankAccount_Master_M_Tbl   where Br_Code ='" & Session("DefaultBrCode") & "'"
        ' ''M_DBConn.ExecuteDataTable(M_Dt, Sql_String, False)
        ' ''If M_Dt.Rows.Count > 0 Then


        ' ''    Fill_Combo(CmbCollnBank, M_Dt, "AccountDetails", "ACC_ID")
        ' ''Else
        ' ''    ShowAlertMessage("Account Code Not Set..")
        ' ''End If

















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

    Protected Sub TxtBranch_TextChanged(sender As Object, e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbCollnBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCollnBank.SelectedIndexChanged

    End Sub

    Protected Sub TxtBillAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBillAmt.TextChanged
        TxtAmount.Text = Val(TxtBillAmt.Text)
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAdd.Click
        Dim TType As String
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
End Class