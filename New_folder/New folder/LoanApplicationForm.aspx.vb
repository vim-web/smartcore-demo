Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Public Class LoanApplicationForm
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Dim FormID As Integer = 1083
    Shared Obj_UserRights As Object
    Private Shared Common As New CommonBL

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

            TxtIssueDate.Attributes.Add("readonly", "readonly")
            TxtIssueDate.Text = Session("TransDate")
            Load_DefaultValues()

            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("LA", Request.QueryString("TRANID"))
            End If
            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If

        If Session("SearchResult") <> "" Then

            TxtCustId.Text = Session("SearchResult")
            TxtCustId_TextChanged(Me.TxtCustId, System.EventArgs.Empty)
            TxtCustId.Focus()
            Session("SearchResult") = ""

        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("LA", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        'DgvTransaction.DataSource = M_Dataset.Tables(1)
        Session("TransTbl") = M_Dataset.Tables(1)
        'DgvTransaction.DataBind()
        M_CommonBL = Nothing
        GetAccountDtls(M_Dataset.Tables(0))

        'GetAccountDtls(M_TranTbl, M_MemTbl)
        'PnlAuthorise.Enabled = False
        '  Set_TransactionGrid()
    End Sub


    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        ' CmbMemBranch.SelectedValue = M_AccTbl.Rows(0).Item("MemBrCode")
        ' CmbMemType.SelectedValue = M_AccTbl.Rows(0).Item("MemSchCode")
        TxtRemarks.Text = M_AccTbl.Rows(0).Item("LM_Remarks")
        TxtFormFee.Text = M_AccTbl.Rows(0).Item("Lm_Amt")
        ' TxtMemNo.Text = M_AccTbl.Rows(0).Item("Memberno")

        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        '  Call TxtMemNo_TextChanged(Me.TxtMemNo, System.EventArgs.Empty)
    End Sub
    Private Sub Enabled()
        TxtName.Enabled = False
        TxtAddress.Enabled = False
        TxtIssueDate.Enabled = False
        TxtFormFee.Enabled = False
        TxtRemarks.Enabled = False
    End Sub


    Private Sub Load_DefaultValues()
        '   SetGrid()

        Set_SchemeCtrl()

        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        ' M_CommonBL.Fill_Branch(CmbMemBranch)
        ' M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"), ("SHARE"))





        TxtIssueDate.Attributes.Add("readonly", "readonly")
        M_CommonBL = Nothing
        SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
        '  AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)
        AcHeadDt = DirectCast(Cache("Achead"), DataTable)
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        CmbBranch.Enabled = False
        TxtBranch.Enabled = False


        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)

        'LblRctCashTotal.Text = "0.00"
        'LblRctTransferTotal.Text = "0.00"
        'LblPayCashTotal.Text = "0.00"
        'LblPayTransferTotal.Text = "0.00"


    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    
    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

  
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Function ValidateFields() As Boolean
        Dim Status As Boolean

        'If Val(TxtFormNo.Text.Length) > 0 Then
        '    Status = True
        'Else
        '    ShowAlertMessage("Enter valid Form Number")
        '    TxtFormNo.Focus()
        '    Return False
        'End If

 




        'If Val(HdnAccid.Value.Length) > 0 Then
        '    Status = True
        'Else
        '    ShowAlertMessage("Enter valid Member Number")
        '    TxtMemNo.Focus()
        '    Return False
        'End If



        If Val(TxtIssueDate.Text.Length) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Date")
            TxtIssueDate.Focus()
            Return False
        End If


        'If Val(TxtFormFee.Text.Length) > 0 And Val(TxtFormFee.Text) > 0 Then
        '    Status = True
        'Else
        '    ShowAlertMessage(" Enter Form Fee")
        '    TxtFormFee.Focus()
        '    Return False
        'End If

        If Val(TxtRemarks.Text.Length) = 0 Then
            Status = True
            TxtRemarks.Text = ""
        End If
        Return Status
    End Function

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        If Len(Request.QueryString("TRANID")) > 0 Then
            Dim M_CommonBL1 As New CommonBL
            If M_CommonBL1.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                Response.Redirect("~/ManagerPassing.aspx.vb")
                Exit Sub
            End If
            M_CommonBL1 = Nothing
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If


        If ValidateFields() = False Then
            Exit Sub
        End If
        Dim AccNo As String = ""

        Dim dt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
        Dim M_CommonBL As New CommonBL
        Dim Tran_No As String = ""
        Dim M_Dt As New DataTable
        'Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
        'Dim M_TranSw As New IO.StringWriter
        'Trandt.WriteXml(M_TranSw)

        Dim M_LoanApplication As New SMARTCORE_BO.LoanAppFormBO

        With M_LoanApplication
            .LmApplNo = ""
            .BrCode = CmbBranch.SelectedValue
            .SchCode = "0"
            .LmShrAccID = "0" ' HdnAccid.Value
            .LmDate = Session("TransDate")
            .LmAmt = "0" 'Val(TxtFormFee.Text)
            .LmRemarks = TxtRemarks.Text
            .MakerID = Session("UserID")
            .TranNo = 1
            .CustID = TxtCustId.Text

        End With

        Try
            Dim M_LoanApplicationBL As New LoanAppFormBL
            Dim M_AppDt As DataTable
            Session("M_EntryMode") = "NEW"
            M_AppDt = M_LoanApplicationBL.Save_Data(M_LoanApplication, Session("M_EntryMode"), "")
            M_LoanApplicationBL = Nothing
            Tran_No = M_AppDt.Rows(0)("Tran_No").ToString()
            AccNo = M_AppDt.Rows(0)("Acc_NO").ToString()
            Response.Redirect("~/NewAccount.aspx?AccNo= Loan Application Form No-" & AccNo & "" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=" & "D" & "&TRANCATG=" & "TRAN")
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try




        '===========================================================================



        'Try

        '    Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

        '    M_Dt = M_CommonBL.Update_AccHeadTran(Session("DefaultBrCode"), Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text & "-" & AccNo, Session("LoginID"), M_TranSw.ToString, "", "LA")
        '    If M_Dt.Rows.Count > 0 Then



        '        Dim M_Sql As String
        '        M_Sql = " Update Loan_App_Form_M_Tbl set Tran_No ='" & Tran_No & "' where Lm_ApplNo  ='" & AccNo & "'"
        '        M_DBConn.ExecuteScalar(M_Sql, , False)
        '        ShowAlertMessage("UPDATED")
        '        Response.Redirect("~/NewAccount.aspx?AccNo= Loan Application Form No-" & AccNo & "" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=" & "D" & "&TRANCATG=" & "TRAN")
        '    Else
        '        ShowAlertMessage("FAILED")
        '    End If
        '    M_CommonBL = Nothing
        'Catch ex As Exception

        'End Try


        ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)

    End Sub

  




    Private Sub Set_SchemeCtrl()
        Try

            'Dim locAcHeadTbl As DataTable
            'Dim M_CommonBL As New CommonBL
            'locAcHeadTbl = M_CommonBL.Fill_SchemeHeads("LS", "R")
            '' DgvTransaction.DataSource = locAcHeadTbl
            'Session("locAcHeadTbl") = locAcHeadTbl
            'HddAccCode.Value = locAcHeadTbl.Rows(0)(1).ToString
            '' DgvTransaction.DataBind()
            'Session("SearchScheme") = "84"
            'Dim TempDt As DataTable
            'TempDt = M_CommonBL.Get_SchemeDtls("84")
            'M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub





  


    'Private Sub Calc_TranTotal()
    '    On Error Resume Next
    '    Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)

    '    Dim objSum As Object
    '    If Trandt.Rows.Count > 0 Then
    '        objSum = Trandt.Compute("Sum(RctCash)", "")
    '        LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
    '        objSum = Trandt.Compute("Sum(RctTransfer)", "")
    '        LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
    '        objSum = Trandt.Compute("Sum(PayCash)", "")
    '        LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
    '        objSum = Trandt.Compute("Sum(PayTransfer)", "")
    '        LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
    '    End If

    'End Sub


    'Protected Sub TxtMemNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemNo.TextChanged
    'If TxtMemNo.Text.Length <> 13 Then
    '    TxtMemNo.Text = Right("000" + CmbMemBranch.SelectedValue, 3) + Right("000" + CmbMemType.SelectedValue, 3) + Right("0000000" + TxtMemNo.Text, 7)
    'End If


    'Dim Dt_AcDtlsN As DataTable
    'Dim M_CommonBL As New CommonBL
    'Dim M_AccNo As String = TxtMemNo.Text
    'Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, M_AccNo, Session("TransDate"))
    'If Dt_AcDtlsN.Rows.Count > 0 Then
    '    ' TxtCustID.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
    '    ' Session("CustomerId") = TxtCustID.Text ''
    '    If Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C" Then
    '        ShowAlertMessage("This Account is Closed ")
    '        TxtMemNo.Text = ""
    '        Exit Sub
    '    End If
    '    TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
    '    TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()

    '    HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
    '    SurMemAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
    'End If

    'M_CommonBL = Nothing

    ' End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click

    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Call ClearControls()
        Call Load_DefaultValues()
    End Sub

    Protected Sub TxtFormFee_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtFormFee.TextChanged

        '  Call Set_TransactionGrid()

        'TxtRemarks.Text = "LA For " & RTrim(TxtName.Text) & "(" & TxtMemNo.Text & ")"
        'Calc_TranTotal()
    End Sub


    'Private Sub Set_TransactionGrid()
    '    Dim M_CommonBL As New CommonBL
    '    Dim Dt As DataTable

    '    Dt = M_CommonBL.Set_TransactionLoanAppChargeGrid("84", Session("TransDate"), Val(TxtFormFee.Text), "Y")

    '    DgvTransaction.DataSource = Dt
    '    DgvTransaction.DataBind()
    '    Session("TransTbl") = Dt
    '    'Session("AccHolderTbl") = Dt
    '    Session("locAcHeadTbl") = Dt
    '    M_CommonBL = Nothing

    'End Sub

    Private Sub ClearControls()

        TxtAddress.Text = ""
        TxtFormFee.Text = ""
        TxtRemarks.Text = ""
        TxtName.Text = ""
        LblRctCashTotal.Text = "0.00"
        LblRctTransferTotal.Text = "0.00"
        LblPayCashTotal.Text = "0.00"
        LblPayTransferTotal.Text = "0.00"
    End Sub

    Protected Sub TxtCustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustId.TextChanged
        Try
            Locate_customer()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Locate_customer()
        Try
            Dim M_Dt As New DataTable
            M_Dt = Common.GetCustDetails(TxtCustId.Text)

            If M_Dt.Rows.Count > 0 Then
                TxtName.Text = M_Dt.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = M_Dt.Rows(0)("relname").ToString() + " ,  " + M_Dt.Rows(0)("Full_Address").ToString()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class