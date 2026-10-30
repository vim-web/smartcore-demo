Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class NewAccOpen
    Inherits System.Web.UI.Page
    Public Shared X As String
    Public VouId As String
    Dim FormID As Integer = 1106
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' CallJScriptPrinter()
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        Dim M_Comm As New CommonBL
        Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
        M_Comm = Nothing
        '   If Not IsPostBack Then

        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)

        '  End If
        Session("locAcHeadTbl") = Nothing

        If Not IsPostBack Then
            ' Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)
            ' ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "err_msg", "Button1_onclick();", True)

            If Request.QueryString("TRANCATG") <> "TRAN" Then
                divacdtls.Visible = True
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(CmbBranch)
                M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
                M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))

                CmbBranch.SelectedValue = Session("AcOpenBranch")
                CmbScheme.SelectedValue = Session("AcOpenScheme")
                CmbOpMode.SelectedValue = Session("AcOpenOpMode")
                TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
                Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
                DgvCustomer.DataSource = dt
                DgvCustomer.DataBind()

            Else
                divacdtls.Visible = False
            End If


            hdnTRANCATG.Value = Request.QueryString("TRANCATG")
            hdnTransID.Value = Request.QueryString("TransID")
            LblAccNo.Text = IIf(hdnTRANCATG.Value <> "TRAN", "New Account No :", "") & Request.QueryString("AccNo")
            hdnPrintBond.Value = Request.QueryString("PrintBond")
            'If hdnPrintBond.Value = "Y" Then
            '    CmdBond.Visible = True
            'Else
            '    CmdBond.Visible = False
            'End If
            hdnSection.Value = Request.QueryString("SECTION")
            hdnSecName.Value = Request.QueryString("SEC")
            hdnSchCode.Value = Request.QueryString("SCHEME")
            hdnDRCR.Value = Request.QueryString("DRCR")

            'If hdnPrintBond.Value = "N" Then CmdPrint.Visible = False
            'If hdnSection.Value = "GOLD" Then ButtonPrint.Visible = False Else ButtonPrint.Visible = True
            ''////////
            If hdnSection.Value = "GOLD" Then
                If Len(Request.QueryString("TransID")) > 0 And Request.QueryString("SaveType") = "N" Then
                    ButtonPrint.Visible = True
                Else
                    ButtonPrint.Visible = False
                End If
            End If
            ''////////
            If hdnPrintBond.Value = "N" Then ButtonPrint.Visible = False



            Dim str1 As String
            Dim strArr() As String
            str1 = Request.QueryString("AccNo")
            strArr = str1.Split(",")

            If strArr.Length > 1 Then
                DivMiltipleAccount.Visible = True
                For Each Str2 As String In strArr
                    Dim LstItem As New ListItem(Str2, Str2)
                    ChkAccNo.Items.Add(LstItem)
                Next
            End If


            Dim Prn As New Print
            Dim DtLoc As New DataTable
            Dim M_DBConn As New SMARTCORE_DAL.Connection
            Dim Qry As String

            Qry = "select * from Transaction_T_Tbl where tran_no='" & hdnTransID.Value & "'"
            M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
            If DtLoc.Rows.Count > 0 Then
                If hdnSecName.Value = "DEPOSIT" And DtLoc.Rows(0)("Tran_Function").ToString = "RN" Then
                    VouId = IIf(DtLoc.Rows(0)("SCH_Code").ToString = "18", "ADBOND", "FDBOND")
                    If DtLoc.Rows(0)("SCH_Code").ToString = "17" Then
                        Call PrintBond_FDNew(hdnTransID.Value, VouId)
                    ElseIf DtLoc.Rows(0)("SCH_Code").ToString = "18" Then
                        Call PrintBond_AD(hdnTransID.Value, VouId)
                    End If
                End If
                Prn.SetUPPrintWODialogue(Session("DefaultBrCode"), hdnSecName.Value, hdnSchCode.Value, hdnTransID.Value, "1", hdnDRCR.Value, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, IIf(hdnSchCode.Value = "LAF", "LAF", DtLoc.Rows(0)("Tran_Function").ToString))
            Else
                Prn.SetUPPrintWODialogue(Session("DefaultBrCode"), hdnSecName.Value, hdnSchCode.Value, hdnTransID.Value, "1", hdnDRCR.Value, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, IIf(hdnSchCode.Value = "LAF", "LAF", ""))
            End If
            Dim Str As [String] = Session("PrintStr").ToString()
            HiddenField1.Value = Str
            ' X = DrpPrinters.SelectedItem.Text
            ' CallJScript()
            '   CallJScriptPrinter()

            ' ''If hdnTRANCATG.Value <> "TRAN" Then

            ' ''    If (CmbScheme.SelectedItem.Text = "SHARE HOLDERS DEPOSIT" Or CmbScheme.SelectedItem.Text = "Directors Relative Deposit") And hdnTRANCATG.Value <> "TRAN" Then
            ' ''        VouId = "FDBOND"
            ' ''        Call PrintBond_FDNew(hdnTransID.Value, VouId)
            ' ''    ElseIf hdnSection.Value = "GOLD" And hdnTRANCATG.Value <> "TRAN" Then
            ' ''        VouId = "GLBOND"
            ' ''        Call PrintBond_Gold(hdnTransID.Value, VouId)
            ' ''    ElseIf CmbScheme.SelectedItem.Text = "ATHULYA DEPOSIT" And hdnTRANCATG.Value <> "TRAN" Then
            ' ''        VouId = "ADBOND"
            ' ''        Call PrintBond_AD(hdnTransID.Value, VouId)
            ' ''    End If
            ' ''End If

            'If CmbScheme.SelectedItem.Text = "FIXED DEPOSIT" And hdnTRANCATG.Value <> "TRAN" Then
            '    VouId = "FDBOND"
            '    Call PrintBond_FDNew(hdnTransID.Value, VouId)
            'ElseIf CmbScheme.SelectedItem.Text = "GOLD LOAN" And hdnTRANCATG.Value <> "TRAN" Then
            '    VouId = "GLBOND"
            '    Call PrintBond_Gold(hdnTransID.Value, VouId)
            'ElseIf CmbScheme.SelectedItem.Text = "ATHULYA DEPOSIT" And hdnTRANCATG.Value <> "TRAN" Then
            '    VouId = "ADBOND"
            '    Call PrintBond_AD(hdnTransID.Value, VouId)
            'End If

            ViewState("PreviousPage") = Request.UrlReferrer
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose.Click
        'Response.Redirect("~/main.aspx")
        If hdnTRANCATG.Value = "TRAN" Then
            Response.Redirect(ViewState("PreviousPage").ToString(), True)
        Else
            Response.Redirect("~/main.aspx")  ' To Do
        End If
    End Sub


    Public Sub PrintBond_FD(ByVal Tran_No As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String
        Dim Dt As DataTable



        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()


        M_Sql = "Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperatorsWithAddres_Fn(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators "
        M_Sql = M_Sql & "From DepBond_Vw B Left Outer Join Transaction_T_Tbl  T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code And "
        M_Sql = M_Sql & "T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code  where B.Tran_No='" & Tran_No & "'"



        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        If VouId = "FDBOND" Then
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FDBOND.rpt"))
        Else
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/SUBORDINATE_BOND.rpt"))
        End If



        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=1", False)

    End Sub


    Public Sub PrintBond_FDNew(ByVal Tran_No As String, ByVal VouId As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String
        Dim Dt As DataTable


        M_Sql = " Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperators(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators From DepBond_Vw B Left Outer Join Transaction_T_Tbl T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code "

        M_Sql = M_Sql + " And T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code WHERE T.Tran_No='" & Tran_No & "'"
        'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        M_Sql = Session("File_Path").ToString()
        If VouId = "FDBOND" Then
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FDBOND.rpt"))
        Else
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/SUBORDINATE_BOND.rpt"))
        End If

        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


        'Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
        'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        'HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
        'Session.Add("RptQry", M_Sql)
        'Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Deposit\FDBOND.rpt")


        ' Session.Add("RptFormula", StrSlf)
        ' Response.Redirect("~/RptPreview.aspx?Outputto=1", False)

    End Sub

    Public Sub PrintBond_AD(ByVal Tran_No As String, ByVal VouId As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String
        Dim Dt As DataTable
        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
        Session.Add("RptQry", M_Sql)
        'Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Deposit\FDBOND.rpt")


        ' Session.Add("RptFormula", StrSlf)
        ' Response.Redirect("~/RptPreview.aspx?Outputto=1", False)

    End Sub


    'Public Sub PrintBond_Gold(ByVal Tran_No As String, ByVal Vouid As String)
    '    'Dim M_ReportBl As New ReportsBL
    '    'Dim M_Sql As String
    '    'Dim M_Dt As New DataTable()
    '    'Dim M_CommonDa As New SMARTCORE_DA.CommonDA
    '    'Dim StrSlf As String
    '    'Dim Dt As DataTable

    '    'Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
    '    'M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "'"
    '    'M_DBConn.ExecuteScalar(M_Sql, , False)

    '    'M_Sql = ""

    '    'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
    '    'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
    '    'HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
    '    'Session.Add("RptQry", M_Sql)

    '    ''M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & Tran_No & "'"
    '    ''M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

    '    ''Session.Add("RptQry", M_Sql)
    '    ''Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Loans\GoldBond.rpt")


    '    ''Session.Add("RptFormula", StrSlf)
    '    ''Response.Redirect("~/RptPreview.aspx?Outputto=1", False)



    '    Dim M_ReportBl As New ReportsBL
    '    Dim M_Sql As String
    '    Dim M_Dt As New DataTable()
    '    Dim M_CommonDa As New SMARTCORE_DA.CommonDA
    '    Dim StrSlf As String
    '    Dim Dt As DataTable

    '    Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
    '    M_Sql = "Exec Loan_Bond_Sp '" & hdnTransID.Value & "'"
    '    M_DBConn.ExecuteScalar(M_Sql, , False)

    '    'M_Sql = ""

    '    'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
    '    'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
    '    'HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
    '    'Session.Add("RptQry", M_Sql)

    '    M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & hdnTransID.Value & "'"
    '    M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

    '    Session.Add("RptQry", M_Sql)
    '    M_Sql = Session("File_Path").ToString()
    '    Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK.rpt"))

    '    Session.Add("RptFormula", StrSlf)
    '    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


    'End Sub

    'Protected Sub CmdBond_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdBond.Click
    '    If hdnSection.Value = "FD" Then
    '        '  PrintBond_FDNew(hdnTransID.Value)
    '    ElseIf hdnSection.Value = "GOLD" Then
    '        'PrintBond_Gold(hdnTransID.Value)
    '    End If
    'End Sub

    Protected Sub CmdPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdPrint.Click
        '' VOU PRINT
        'Dim Prn As New Print
        'Dim DtLoc As New DataTable
        'Dim M_DBConn As New SMARTCORE_DAL.Connection
        'Dim Qry As String

        'Qry = "select * from Transaction_T_Tbl where tran_no='" & hdnTransID.Value & "'"
        'M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
        'Prn.SetUPPrintWODialogue("8", hdnSecName.Value, hdnSchCode.Value, hdnTransID.Value, "1", hdnDRCR.Value, 1, True, Session("TransDate"), Session("LoginID").ToString(), DtLoc)
        'Dim Str As [String] = Session("PrintStr").ToString()
        'HiddenField1.Value = Str
        '' X = DrpPrinters.SelectedItem.Text
        '' CallJScript()
        ''   CallJScriptPrinter()
    End Sub

    Public Shared Sub CallJScript()
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            '[error] = [error].Replace("'", "`")
            '            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "Button2_onclick();", True)
        End If
    End Sub
    Public Shared Sub CallJScriptPrinter()
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            '[error] = [error].Replace("'", "`")
            '            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "abc();", True)
        End If
    End Sub

    Private Sub NewAccount_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ' CallJScript()
    End Sub


    Protected Sub ChkAccNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAccNo.SelectedIndexChanged
        If hdnTRANCATG.Value <> "TRAN" Then
            Dim Prn As New Print
            Dim DtLoc As New DataTable
            Dim M_DBConn As New SMARTCORE_DAL.Connection
            Dim Qry As String
            Dim Acc_NO As String
            Dim CommonBL As New SMARTCORE_BL.CommonBL

            Acc_NO = ChkAccNo.SelectedItem.Text




            Qry = "select tran_no from Transaction_T_Tbl where acc_no='" & Acc_NO & "'"
            M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
            hdnTransID.Value = DtLoc.Rows(0)("tran_no").ToString

            If CmbScheme.SelectedItem.Text = "FIXED DEPOSIT" And hdnTRANCATG.Value <> "TRAN" Then
                VouId = "FDBOND"
                Call PrintBond_FDNew(hdnTransID.Value, VouId)
                MultipleVou()

            End If
        End If
    End Sub

    Protected Sub MultipleVou()
        Dim Prn As New Print
        Dim DtLoc As New DataTable
        Dim M_DBConn As New SMARTCORE_DAL.Connection
        Dim Qry As String

        Qry = "select * from Transaction_T_Tbl where tran_no='" & hdnTransID.Value & "'"
        M_DBConn.ExecuteDataTable(DtLoc, Qry, False)

        If DtLoc.Rows.Count > 0 Then
            Prn.SetUPPrintWODialogue(Session("DefaultBrCode"), hdnSecName.Value, hdnSchCode.Value, hdnTransID.Value, "1", hdnDRCR.Value, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, IIf(hdnSchCode.Value = "LAF", "LAF", DtLoc.Rows(0)("Tran_Function").ToString))
        Else
            Prn.SetUPPrintWODialogue(Session("DefaultBrCode"), hdnSecName.Value, hdnSchCode.Value, hdnTransID.Value, "1", hdnDRCR.Value, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, IIf(hdnSchCode.Value = "LAF", "LAF", ""))
        End If
        Dim Str As [String] = Session("PrintStr").ToString()
        HiddenField1.Value = Str
    End Sub

    Protected Sub CmdBond0_Click(sender As Object, e As EventArgs) Handles CmdBond0.Click

        If hdnTRANCATG.Value <> "TRAN" Then

            If (CmbScheme.SelectedItem.Text = "SHARE HOLDERS DEPOSIT" Or CmbScheme.SelectedItem.Text = "Directors Relative Deposit" Or CmbScheme.SelectedItem.Text = "SUBORDINATE DEBT") And hdnTRANCATG.Value <> "TRAN" Then
                If CmbScheme.SelectedItem.Text = "SUBORDINATE DEBT" Then
                    VouId = "SUBBOND"
                Else
                    VouId = "FDBOND"
                End If

                Call PrintBond_FDNew(hdnTransID.Value, VouId)
            ElseIf hdnSection.Value = "GOLD" And hdnTRANCATG.Value <> "TRAN" Then
                VouId = "GLBOND"
                Call PrintBond_Gold(hdnTransID.Value, VouId)
            ElseIf CmbScheme.SelectedItem.Text = "ATHULYA DEPOSIT" And hdnTRANCATG.Value <> "TRAN" Then
                VouId = "ADBOND"
                Call PrintBond_AD(hdnTransID.Value, VouId)
            End If
        End If

    End Sub

    Protected Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        Try
            'Printing Right
            'If Obj_UserRights.Print_Right = False Then
            '    ShowAlertMessage("You Have No Permission To Print!!!")
            '    Exit Sub
            'End If

            'Session("PrintFromDt") = TxtTranDt.Text

            Dim PrintType As String
            Dim Tran_No As String
            Dim M_DBConn As New Connection()
            Dim SqlStr As String = ""
            Dim VouId As String


            PrintType = hdnSection.Value
            Tran_No = hdnTransID.Value
            'If PrintType = "FDBOND" Or PrintType = "SUBORDINATE BOND" Then
            '    'Call PrintBond_FD(Tran_No)
            '    VouId = "FD"
            '    If CmbPrnType.SelectedItem.Text = "SUBORDINATE BOND" Then
            '        VouId = "SUBORDINATE BOND"
            '    End If
            '    Call PrintBond_FDNew(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "VOUCHER" Then
            '    Call PrintVoucher(Tran_No)
            'ElseIf CmbPrnType.SelectedItem.Text = "GLBOND" Then
            If Session("BrRegion") = 1 Then 'KERALA
                VouId = "KFS"
                Call PrintBond_KFS(Tran_No, VouId)
            ElseIf Session("BrRegion") = 2 Then 'TAMILNADU
                VouId = "GLBOND"
                Call PrintBond_Gold(Tran_No, VouId)
            End If


            'ElseIf CmbPrnType.SelectedItem.Text = "ATHULYA BOND" Then
            'VouId = "ADBOND"
            'Call PrintBond_AD(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "DIRECTOR'S RELATIVE BOND" Then
            'VouId = "DIRECTOR'S RELATIVE BOND"
            'Call PrintBond_FDNew(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "DOUBLE BENEFIT BOND" Then
            'VouId = "DOUBLE BENEFIT BOND"
            'Call PrintBond_FDNew(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "CUMILATIVE SHARE H.DEP" Then
            'VouId = "CUMILATIVE SHARE H.DEP"
            'Call PrintBond_FDNew(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "ICD BOND" Then
            'VouId = "ICD BOND"
            'Call PrintBond_FDNew(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "DEBENTURE" Then
            'VouId = "DEBENTURE"
            'Call PrintBond_Debanture(Tran_No, VouId)
            'ElseIf CmbPrnType.SelectedItem.Text = "UN-SEC DEBENTURE" Then
            'VouId = "UN-SEC"
            'Call PrintBond_Debanture(Tran_No, VouId)
            'End If

            'Call Refresh()
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Public Sub PrintBond_Gold(ByVal Tran_No As String, ByVal Vouid As String)
        Try

            'Dim Prn As New Print
            'Dim M_ReportBl As New ReportsBL
            'Dim M_Sql As String
            'Dim M_Dt As New DataTable()
            'Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            'Dim StrSlf As String
            'Dim Dt As DataTable

            'Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            'M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "'"
            'M_DBConn.ExecuteScalar(M_Sql, , False)

            'M_Sql = ""

            'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & Vouid & "',''"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            'Session("PrintStr") = M_Dt.Rows(0)(0).ToString()
            'Session.Add("RptQry", M_Sql)
            'Prn.PrintBond()

            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Sql1 As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String
            Dim Dt As DataTable

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "','Y'"
            M_DBConn.ExecuteScalar(M_Sql, , False)

            M_Sql1 = "Exec Insert_BondPrint_T_Tbl_Sp '" & Tran_No & "','" & Vouid & "','" & Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy") & "','" & Session("UserID") & "'"
            M_DBConn.ExecuteScalar(M_Sql1, , False)

            'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            'HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
            'Session.Add("RptQry", M_Sql)

            M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & Tran_No & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            M_Sql = Session("File_Path").ToString()
            'If Drp_printlang.SelectedItem.Text = "Malayalam" Then
            'Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK.rpt"))
            'ElseIf Drp_printlang.SelectedItem.Text = "Tamil" Then
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondTamil.rpt"))
            'ElseIf Drp_printlang.SelectedItem.Text = "English" Then
            'Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondEng.rpt"))
            'End If

            Session.Add("RptFormula", StrSlf)
            ''Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

            Dim url As String = "RptPreview.aspx?Outputto=2"
            ShowPrintDialog(url, "_blank", "menubar=0,width=1000,height=1000,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintBond_KFS(ByVal Tran_No As String, ByVal Vouid As String)
        Try

            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Sql1 As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "','Y'"
            M_DBConn.ExecuteScalar(M_Sql, , False)

            M_Sql1 = "Exec Insert_KFSPrint_T_Tbl_Sp '" & Tran_No & "','" & Vouid & "','" & Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy") & "','" & Session("UserID") & "'"
            M_DBConn.ExecuteScalar(M_Sql1, , False)

            'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            'HdnFDValue.Value = M_Dt.Rows(0)(0).ToString()
            'Session.Add("RptQry", M_Sql)

            M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & Tran_No & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            M_Sql = Session("File_Path").ToString()
            'If Drp_printlang.SelectedItem.Text = "Malayalam" Then
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK_KFS.rpt"))
            'ElseIf Drp_printlang.SelectedItem.Text = "Tamil" Then
            '    Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondTamil.rpt"))
            'ElseIf Drp_printlang.SelectedItem.Text = "English" Then
            '    Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondEng.rpt"))
            'End If



            Session.Add("RptFormula", StrSlf)
            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            'Response.Redirect("~/RptView.aspx", False)
            Dim url As String = "RptPreview.aspx?Outputto=2"
            ShowPrintDialog(url, "_blank", "menubar=0,width=1000,height=1000,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub ShowPrintDialog(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
        Try
            Dim context As HttpContext = HttpContext.Current

            If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then

                context.Response.Redirect(url)
            Else
                Dim page As Page = DirectCast(context.Handler, Page)
                If page Is Nothing Then
                    Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
                End If
                url = page.ResolveClientUrl(url)

                Dim script As String
                windowFeatures = windowFeatures & ",scrollbars=yes"
                If Not [String].IsNullOrEmpty(windowFeatures) Then
                    script = "window.open(""{0}"", ""{1}"", ""{2}"", ""{1}"");"
                Else
                    script = "window.open(""{0}"", ""{1}"");"
                End If

                script = [String].Format(script, url, target, windowFeatures)
                ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
    '    Try

    '        Dim M_ReportBl As New ReportsBL
    '        Dim M_Sql As String
    '        Dim ClsComm As New CommonClass()
    '        Dim M_Dt As New DataTable()
    '        Dim M_DBConn As New SMARTCORE_DAL.Connection
    '        Dim Company_Name As String
    '        Dim StrSlf As String
    '        Dim M_TranNo As String
    '        M_DBConn = New Connection()


    '        Company_Name = HttpContext.Current.Cache("CompanyName")
    '        M_Sql = "Exec VoucherPrint_Sp '" & Session("DefaultBrCode") & "','" & hdnTransID.Value & "'"
    '        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
    '        If M_Dt.Rows.Count > 0 Then
    '            Session.Add("RptQry", M_Sql)
    '            Session.Add("RptPath", Server.MapPath("~/Voucher.rpt"))
    '            Response.Redirect("~/RptPreview.aspx?Outputto=2&PRINTTYPE=VOUCHER", False)
    '        End If


    '        'Dim Prn As New Print
    '        'Dim DtLoc As New DataTable
    '        'Dim M_DBConn As New SMARTCORE_DAL.Connection
    '        'Dim Qry As String
    '        'Dim Section As String
    '        'Dim TranFunction As String
    '        'Dim CommonBL As New SMARTCORE_BL.CommonBL
    '        'Dim SchCode As String


    '        'Qry = "select * from Transaction_T_Tbl where tran_no='" & Tran_No & "'"
    '        'M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
    '        'TranFunction = DtLoc.Rows(0)("Tran_Function").ToString
    '        'Section = CommonBL.GetSchemeSection(DtLoc.Rows(0)("Sch_Code").ToString)
    '        'SchCode = DtLoc.Rows(0)("Sch_Code").ToString

    '        'If TranFunction = "AGCO" Or TranFunction = "SAL" Then
    '        '    Section = "GENERAL"
    '        '    SchCode = ""
    '        'ElseIf Section.Length <= 0 Then
    '        '    Section = "GENERAL"
    '        'End If


    '        'Prn.SetUPPrint(Session("DefaultBrCode"), Section, SchCode, Tran_No, "1", DtLoc.Rows(0)("Tran_Type").ToString, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, DtLoc.Rows(0)("Tran_Function").ToString())


    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
