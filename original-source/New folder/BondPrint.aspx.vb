Imports System.Data
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Partial Public Class BondPrint
    Inherits System.Web.UI.Page
    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1015
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Dim M_Common As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            If Not Session("PrintFromDt") = Nothing Then
                TxtTranDt.Text = Session("PrintFromDt")
            Else
                TxtTranDt.Text = Session("TransDate")
            End If
            DgvPassing.DataSource = GetTable()
            DgvPassing.DataBind()
            Dim brcode As String
            brcode = Session("DefaultBrCode")
            M_Common.Fill_PrintLang(Drp_printlang, brcode)
            CmbPrnType.Items.Add("VOUCHER")
            CmbPrnType.Items.Add("FDBOND")
            CmbPrnType.Items.Add("GLBOND TAMIL")
            'CmbPrnType.Items.Add("ATHULYA BOND")
            CmbPrnType.Items.Add("SUBORDINATE BOND")
            CmbPrnType.Items.Add("DIRECTOR'S RELATIVE BOND")
            CmbPrnType.Items.Add("DOUBLE BENEFIT BOND")
            CmbPrnType.Items.Add("CUMILATIVE SHARE H.DEP")
            CmbPrnType.Items.Add("ICD BOND")
            CmbPrnType.Items.Add("DEBENTURE")
            CmbPrnType.Items.Add("UN-SEC DEBENTURE")
            CmbPrnType.Items.Add("GLBOND KFS")
            'CmbPrnType.Items.Add("KFS_2")
            CmbPrnType.SelectedIndex = 0
            divPrint_Lang.Visible = False

        End If
    End Sub


    Public Function GetTable() As DataTable
        Dim PassingTbl As DataTable = New DataTable()
        PassingTbl.Columns.Add("Authorised", GetType(Boolean))
        PassingTbl.Columns.Add("VouNo", GetType(String))
        PassingTbl.Columns.Add("Particulars", GetType(String))
        PassingTbl.Columns.Add("RecCash", GetType(Double))
        PassingTbl.Columns.Add("RecTransfer", GetType(Double))
        PassingTbl.Columns.Add("PayCash", GetType(Double))
        PassingTbl.Columns.Add("PayTransfer", GetType(Double))
        PassingTbl.Columns.Add("MakerID", GetType(String))
        PassingTbl.Columns.Add("MakingTime", GetType(Date))
        PassingTbl.Columns.Add("Tran_No", GetType(String))
        PassingTbl.Rows.Add(False, "     ", "", "0", "0", "0", "0", "", "01/01/1900", "")
        Return PassingTbl

    End Function

    Protected Sub DgvPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvPassing.RowCommand


        'Printing Right
        If Obj_UserRights.Print_Right = False Then
            ShowAlertMessage("You Have No Permission To Print!!!")
            Exit Sub
        End If

        Session("PrintFromDt") = TxtTranDt.Text

        Dim Tran_No As String
        Dim M_DBConn As New Connection()
        Dim SqlStr As String = ""
        Dim VouId As String

        If e.CommandName.ToUpper = "SELECT" Then
            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
            Tran_No = DgvPassing.DataKeys(RowIndex)("Tran_No").ToString()
            If CmbPrnType.SelectedItem.Text = "FDBOND" Or CmbPrnType.SelectedItem.Text = "SUBORDINATE BOND" Then
                'Call PrintBond_FD(Tran_No)
                VouId = "FD"
                If CmbPrnType.SelectedItem.Text = "SUBORDINATE BOND" Then
                    VouId = "SUBORDINATE BOND"
                End If
                Call PrintBond_FDNew(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "VOUCHER" Then
                Call PrintVoucher(Tran_No)
            ElseIf CmbPrnType.SelectedItem.Text = "GLBOND TAMIL" Then
                VouId = "GLBOND"
                Call PrintBond_Gold(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "GLBOND KFS" Then
                VouId = "KFS"
                Call PrintBond_KFS(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "KFS_2" Then
                VouId = "KFS_2"
                Call PrintBond_KFS_2(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "ATHULYA BOND" Then
                VouId = "ADBOND"
                Call PrintBond_AD(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "DIRECTOR'S RELATIVE BOND" Then
                VouId = "DIRECTOR'S RELATIVE BOND"
                Call PrintBond_FDNew(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "DOUBLE BENEFIT BOND" Then
                VouId = "DOUBLE BENEFIT BOND"
                Call PrintBond_FDNew(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "CUMILATIVE SHARE H.DEP" Then
                VouId = "CUMILATIVE SHARE H.DEP"
                Call PrintBond_FDNew(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "ICD BOND" Then
                VouId = "ICD BOND"
                Call PrintBond_FDNew(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "DEBENTURE" Then
                VouId = "DEBENTURE"
                Call PrintBond_Debanture(Tran_No, VouId)
            ElseIf CmbPrnType.SelectedItem.Text = "UN-SEC DEBENTURE" Then
                VouId = "UN-SEC"
                Call PrintBond_Debanture(Tran_No, VouId)
            End If

            'Call Refresh()
            'End If
        End If
    End Sub


    Public Sub PrintVoucher(ByVal Tran_No As String)
        Try

            'Dim Prn As New Print
            'Dim DtLoc As New DataTable
            'Dim M_DBConn As New SMARTCORE_DAL.Connection
            'Dim Qry As String
            'Dim Section As String
            'Dim TranFunction As String
            'Dim CommonBL As New SMARTCORE_BL.CommonBL
            'Dim SchCode As String


            'Qry = "select * from Transaction_T_Tbl where tran_no='" & Tran_No & "'and Tran_Function<>'IB'"
            'M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
            'TranFunction = DtLoc.Rows(0)("Tran_Function").ToString
            'Section = CommonBL.GetSchemeSection(DtLoc.Rows(0)("Sch_Code").ToString)
            'SchCode = DtLoc.Rows(0)("Sch_Code").ToString

            'If TranFunction = "AGCO" Or TranFunction = "SAL" Then
            '    Section = "GENERAL"
            '    SchCode = ""
            'ElseIf Section.Length <= 0 Then
            '    Section = "GENERAL"
            'End If

            'Prn.SetUPPrint(Session("DefaultBrCode"), Section, SchCode, Tran_No, "1", DtLoc.Rows(0)("Tran_Type").ToString, 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, DtLoc.Rows(0)("Tran_Function").ToString())

            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_DBConn As New SMARTCORE_DAL.Connection
            Dim Company_Name As String
            Dim StrSlf As String
            Dim M_TranNo As String
            M_DBConn = New Connection()


            Company_Name = HttpContext.Current.Cache("CompanyName")
            M_Sql = "Exec VoucherPrint_Sp '" & Session("DefaultBrCode") & "','" & Tran_No & "'"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            If M_Dt.Rows.Count > 0 Then
                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Voucher.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2&PRINTTYPE=VOUCHER", False)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Public Sub PrintBond_FD(ByVal Tran_No As String)
        Dim M_ReportBl As New SMARTCORE_BL.ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""




        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()


        M_Sql = "Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperatorsWithAddres_Fn(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators "
        M_Sql = M_Sql & "From DepBond_Vw B Left Outer Join Transaction_T_Tbl  T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code And "
        M_Sql = M_Sql & "T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code  where B.Tran_No='" & Tran_No & "'"



        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FDBOND.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=1", False)

    End Sub

    Public Sub PrintBond_Gold(ByVal Tran_No As String)
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
        M_Sql = "Exec Loan_Bond_Sp '" & Tran_No & "'"
        M_DBConn.ExecuteScalar(M_Sql, , False)

        M_Sql = "Select * FROM LoanBond_T_Tbl Where Tran_No='" & Tran_No & "'"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBond.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Public Enum PassingType
        Pending = 0
        Passed
        All
    End Enum

    Public Sub FillPassingData(ByRef Dgv As Object, Optional ByVal FillType As PassingType = PassingType.Pending)
        Try

            Dim SqlString As String = ""
            Dim Dt As DataTable = New DataTable()
            Dim M_DBConn As New Connection()

            'SqlString = "Select Section_Link,Section_SubLink,Sch_Code,Authorisation1,Acc_No,Br_Code," & _
            '"Tran_Type,Tran_No,MakerId,Vr_No, Passing_Display,Narration," & _
            '"CASHRECEIPT = (CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)," & _
            '"TRANSFERRECEIPT = (CASE WHEN TRAN_TYPE = 'C' THEN TRANSFER ELSE 0 END)," & _
            '"CASHPAYMENT = (CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)," & _
            '"TRANSFERPAYMENT = (CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END), "


            SqlString = "Select Cast(Max(Case When Authorised='Y' Then 1 Else 0 End) As Bit) As Authorised,Max(T.Vr_No) As VouNo,CASE WHEN max(T.Sch_Code) ='VOU' THEN Isnull(Max(A.Acc_Name),'')+' - ' + max(Narration+' '+Narration1) ELSE Isnull(Max(S.Sch_Name),'')+'-'+Isnull(Max(M.ACC_No),'') END As Particulars,"
            SqlString = SqlString & " RecCash = Sum((CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)),"
            SqlString = SqlString & " RecTransfer = Sum((CASE WHEN TRAN_TYPE = 'C' THEN TRANSFER ELSE 0 END)),"
            SqlString = SqlString & " PayCash = Sum((CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)),"
            SqlString = SqlString & " PayTransfer = Sum((CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END)),"
            If OptPassed.Checked = True Then
                SqlString = SqlString + " Isnull(dbo.Gen_GetUserName_Fn (Max(T.CheckerID)),'') As MakerID"
            Else
                SqlString = SqlString + " Isnull(dbo.Gen_GetUserName_Fn (Max(T.MakerID)),'') As MakerID"
            End If
            SqlString = SqlString + ",Max(T.Making_Time) As MakingTime, "
            SqlString = SqlString & " T.Tran_No"
            SqlString = SqlString & " From Transaction_T_Tbl T"
            SqlString = SqlString & " Left Outer Join AccMain_M_Tbl M On M.ACC_ID=T.Acc_ID"
            SqlString = SqlString & " Left Outer Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.ACC_SchCode"
            SqlString = SqlString & " Left Outer Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=T.Gl_Code"
            SqlString = SqlString & " Left outer join Cust_Master_M_Tbl C on c.Cust_Id=m.ACC_CustID "
            SqlString = SqlString & " Where T.Cash+T.Transfer>0 "

            SqlString = SqlString + " And Tr_date = " + ClsCom.DbDate(TxtTranDt.Text) & _
            "  And Org_Branch = '" & Session("DefaultBrCode").ToString() & "'"

            If OptPassed.Checked = True Then
                SqlString = SqlString + " And Authorised = 'Y' "
            ElseIf OptPending.Checked = True Then
                SqlString = SqlString + " And Authorised = 'N' "
            End If

            If ChkScheme.Checked = True Then
                SqlString = SqlString + " and T.Sch_Code like '" & TxtScheme.Text & "' "
            End If

            If ChkAccNo.Checked = True Then
                SqlString = SqlString + " and  T.Acc_No  like '%" & TxtAccno00.Text & "%' "
            End If
            If CkName.Checked = True Then
                SqlString = SqlString + " and c.Cust_Name like  '%" & Txt_SearchName.Text & "%' "
            End If
            If CmbPrnType.SelectedItem.Text = "FDBOND" Then
                SqlString = SqlString + " and  S.Sch_Module_Catg  = 'FD'  and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "GLBOND TAMIL" Or CmbPrnType.SelectedItem.Text = "GLBOND KFS" Then
                SqlString = SqlString + " and  S.Sch_Module_Catg  = 'GOLDLOAN'  "
            ElseIf CmbPrnType.SelectedItem.Text = "ATHULYA BOND" Then
                SqlString = SqlString + " and  S.Sch_Module_Catg  = 'RIS' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "SUBORDINATE BOND" Then
                SqlString = SqlString + " and  Sch_Type6='SUB' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "DIRECTOR'S RELATIVE BOND" Then
                SqlString = SqlString + " and  Sch_Type6='DIR' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "DOUBLE BENEFIT BOND" Then
                SqlString = SqlString + " and  Sch_Type6='DBL' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "CUMILATIVE SHARE H.DEP" Then
                SqlString = SqlString + " and  Sch_Type6='FDC' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "ICD BOND" Then
                SqlString = SqlString + " and  Sch_Type6='ICD' and T.Tran_Type='C' "
            ElseIf CmbPrnType.SelectedItem.Text = "DEBENTURE" Then
                SqlString = SqlString + " and  Sch_Type6='DEB' and T.Tran_Type='C' and S.Sch_Module_Catg  = 'FD' AND Sch_Type5 = 'SECURED' "
            ElseIf CmbPrnType.SelectedItem.Text = "UN-SEC DEBENTURE" Then
                SqlString = SqlString + " and  Sch_Type6='DEB' and T.Tran_Type='C' and S.Sch_Module_Catg  = 'FD' AND Sch_Type5 = 'UN SECURED' "
            End If

            If ChkAmount.Checked = True Then
                SqlString = SqlString + " and( T.Cash = " & Val(TxtAmt.Text) & "  or  T.Transfer =" & Val(TxtAmt.Text) & ") "
            End If

            SqlString = SqlString & " Group By T.Tran_No"
            SqlString = SqlString & " Order By Max(T.Making_Time)"

            'If Cashier_Module = True Then
            '    ' For Avoiding Non Cashier Passed Credit Cash Entries
            '    SqlString = SqlString + " And Len(Isnull(Cashier_ID,'')+Case When Cash>0 And Tran_Type='C' Then '' Else MakerId End)>0 "
            'End If

            M_DBConn.ExecuteDataTable(Dt, SqlString, False)

            Dgv.DataSource = Dt
            Dgv.DataBind()

            'If Dt.Rows.Count = 0 Then
            '    PnlStatus.Visible = True
            'Else
            '    PnlStatus.Visible = False
            'End If

            SqlString = "Exec NonTranction_AuthoriseFill_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
            If OptPassed.Checked = True Then
                SqlString = SqlString + ",'Y' "
            ElseIf OptPending.Checked = True Then
                SqlString = SqlString + ",'N' "
            End If
            M_DBConn.ExecuteDataTable(Dt, SqlString, False)
            DgvNonTranPassing.DataSource = Dt
            DgvNonTranPassing.DataBind()

            'If PnlStatus.Visible = True Then
            '    If Dt.Rows.Count = 0 Then
            '        PnlStatus.Visible = True
            '    Else
            '        PnlStatus.Visible = False
            '    End If
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


    Protected Sub DgvPassing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvPassing.PageIndexChanging
        DgvPassing.PageIndex = e.NewPageIndex
        BtnRefresh_Click(Nothing, EventArgs.Empty)
    End Sub

    Protected Sub BtnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRefresh.Click
        If CommonClass.ChkDate(TxtTranDt.Text) = False Then
            ShowAlertMessage("Invalid Date Entered")
            Exit Sub
        End If
        Call Refresh()
    End Sub

    Public Sub Refresh()
        If OptAll.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.All)
        ElseIf OptPassed.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.Passed)
        ElseIf OptPending.Checked = True Then
            Call FillPassingData(DgvPassing, PassingType.Pending)
        End If
    End Sub


    Public Sub PrintBond_FDNew(ByVal Tran_No As String, ByVal VouId As String)
        Try

            'Dim M_ReportBl As New ReportsBL
            'Dim Prn As New Print
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            '  Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String
            Dim Dt As DataTable
            'Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            'Session("PrintStr") = M_Dt.Rows(0)(0).ToString()
            'Session.Add("RptQry", M_Sql)
            'Prn.PrintBond()


            M_Sql = " Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperators(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators From DepBond_Vw B Left Outer Join Transaction_T_Tbl T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code "

            M_Sql = M_Sql + " And T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code WHERE T.Tran_No='" & Tran_No & "'"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            M_Sql = Session("File_Path").ToString()

            If VouId = "FD" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FDBOND.rpt"))
            ElseIf VouId = "SUBORDINATE BOND" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/SUBORDINATE_BOND.rpt"))
            ElseIf VouId = "DIRECTOR'S RELATIVE BOND" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/DIRECTORS_RELATIVE_BOND.rpt"))
            ElseIf VouId = "DOUBLE BENEFIT BOND" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/DOUBLE.rpt"))
            ElseIf VouId = "CUMILATIVE SHARE H.DEP" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/FIXED_CUMILATIVE.rpt"))
            ElseIf VouId = "ICD BOND" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/ICD_Bond.rpt"))
            End If

            ''"DOUBLE BENEFET BOND"
            Session.Add("RptFormula", StrSlf)
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintBond_Debanture(ByVal Tran_No As String, ByVal VouId As String)
        Try

            'Dim M_ReportBl As New ReportsBL
            'Dim Prn As New Print
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            '  Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String
            Dim Dt As DataTable
            'Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            'M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            'Session("PrintStr") = M_Dt.Rows(0)(0).ToString()
            'Session.Add("RptQry", M_Sql)
            'Prn.PrintBond()


            ' M_Sql = " Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperators(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators From DepBond_Vw B Left Outer Join Transaction_T_Tbl T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code "

            M_Sql = " Exec Get_DebentureBond_Sp'" & Tran_No & "'"
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            M_Sql = Session("File_Path").ToString()

            If VouId = "DEBENTURE" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/DEBANTURE.rpt"))
            ElseIf VouId = "UN-SEC" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/UNSEC_DEBANTURE.rpt"))
            End If

            ''"DOUBLE BENEFET BOND"
            Session.Add("RptFormula", StrSlf)
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintBond_AD(ByVal Tran_No As String, ByVal VouId As String)
        Try

            Dim Prn As New Print
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String
            Dim Dt As DataTable
            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            M_Sql = "Exec [FDBondPrinting_Sp] '" & Tran_No & "','" & VouId & "',''"
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            Session("PrintStr") = M_Dt.Rows(0)(0).ToString()
            Session.Add("RptQry", M_Sql)
            Prn.PrintBond()

        Catch ex As Exception

        End Try
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
            If Drp_printlang.SelectedItem.Text = "Malayalam" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "Tamil" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondTamil.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "English" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondEng.rpt"))
            End If



            Session.Add("RptFormula", StrSlf)
            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
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
            Dim StrSlf As String
            Dim Dt As New DataTable

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            M_Sql1 = "Exec CheckEnableToPrintKFS_Sp '" & Tran_No & "','" & Vouid & "','" & Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy") & "','" & Session("UserID") & "'"
            M_DBConn.ExecuteDataTable(Dt, M_Sql1, False)
            If Dt.Rows.Count > 0 Then
                If Dt.Rows(0).Item("EnableToPrintKFS") = "N" Then
                    ShowAlertMessage("You Have No Permission To Print(No. of print exceeds)!!!")
                    Exit Sub
                End If
            End If
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
            If Drp_printlang.SelectedItem.Text = "Malayalam" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK_KFS.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "Tamil" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondTamil.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "English" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondEng.rpt"))
            End If



            Session.Add("RptFormula", StrSlf)
            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            Dim url As String = "RptPreview.aspx?Outputto=2"
            ShowPrintDialog(url, "_blank", "menubar=0,width=1000,height=1000,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub PrintBond_KFS_2(ByVal Tran_No As String, ByVal Vouid As String)
        Try

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
            If Drp_printlang.SelectedItem.Text = "Malayalam" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldBondRK_KFS_2.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "Tamil" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondTamil.rpt"))
            ElseIf Drp_printlang.SelectedItem.Text = "English" Then
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GLBondEng.rpt"))
            End If

            Session.Add("RptFormula", StrSlf)
            'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
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
    Protected Sub DgvNonTranPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvNonTranPassing.RowCommand
        Try
            'PrintingRight
            If Obj_UserRights.Print_Right = False Then
                ShowAlertMessage("You Have No Permission To Print!!!")
                Exit Sub
            End If


            Dim Tran_No As String
            Dim Auth_Type As String
            Dim M_DBConn As New Connection()
            Dim SqlStr As String

            If e.CommandName.ToUpper = "SELECT" Then
                Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
                Tran_No = DgvNonTranPassing.DataKeys(RowIndex)("Tran_No").ToString()
                Dim Prn As New Print
                Dim DtLoc As New DataTable

                Dim Qry As String
                Dim Section As String
                Dim TranFunction As String
                Dim CommonBL As New SMARTCORE_BL.CommonBL
                Dim SchCode As String


                Qry = "SELECT    M.Br_Code,S.Sch_Code , M.Bill_TransNo FROM         Bills_M_Tbl AS M INNER JOIN Gen_SchemeMaster_P_Tbl AS S ON CAST(CASE WHEN M.Bill_InwardOutward = 'O' THEN 'OUTWARD' ELSE 'INWARD' END AS varchar(20)) = S.Sch_Module_Catg WHERE     M.Bill_TransNo = '" & Tran_No & "'"
                M_DBConn.ExecuteDataTable(DtLoc, Qry, False)
                TranFunction = "" 'DtLoc.Rows(0)("Tran_Function").ToString
                Section = CommonBL.GetSchemeSection(DtLoc.Rows(0)("Sch_Code").ToString)
                SchCode = DtLoc.Rows(0)("Sch_Code").ToString
                Prn.SetUPPrint(Session("DefaultBrCode"), "BILLREG", SchCode, Tran_No, "1", "C", 1, True, Session("TransDate"), Session("HostID").ToString(), DtLoc, "")


            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub

    Protected Sub Close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub DgvPassing_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvPassing.SelectedIndexChanged

    End Sub

    Protected Sub CmbPrnType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPrnType.SelectedIndexChanged
        If CmbPrnType.SelectedItem.Text = "GLBOND TAMIL" Then
            divPrint_Lang.Visible = True
        Else
            divPrint_Lang.Visible = False
        End If
        Call Refresh()
    End Sub


End Class