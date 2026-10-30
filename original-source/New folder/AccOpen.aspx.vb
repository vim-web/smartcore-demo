Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BO
Partial Public Class AccOpen
    Inherits System.Web.UI.Page
    Public AccHolderTbl As New DataTable("Table")
    Shared SchemeDt As DataTable
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1001
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            If Not IsPostBack Then
                Load_DefaultValues()
                Set_Grid()
                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                ImgMemSearch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "CUSTOMER" Then
                    TxtCustID.Text = Session("SearchResult")
                    'Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
                    'Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
                    'Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO

                    'Dim M_CustMasterBL As New CustMasterBL
                    'M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress)
                    'Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
                    'M_CustMasterBL = Nothing
                    TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                    TxtCustID.Focus()
                    Session("SearchResult") = ""
                ElseIf Session("SearchType") = "ACCOUNTNO" Then
                    TxtMemberNo.Text = Session("SearchResult")
                    TxtMemberNo_TextChanged(Me.TxtMemberNo, System.EventArgs.Empty)
                ElseIf Session("SearchType") = "GOLDACCNO" Then
                    TxtMemberNo.Text = Session("SearchResult")
                    TxtMemberNo_TextChanged(Me.TxtMemberNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""

            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Pickup(CmbCustomerType, 6, 0)
            M_CommonBL.Fill_Pickup(CmbCustomerCatg, 7, CmbCustomerType.SelectedValue)
            M_CommonBL.Fill_Pickup(CmbPrivelage, 42, 0)
            M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
            M_CommonBL.Fill_Pickup(CmbRelType, 9, 0)
            M_CommonBL.Fill_Pickup(DrpLoanType, 102, 0)

            M_CommonBL.Fill_Section(Ddl_Section)
            If Ddl_Section.Items.Count > 0 Then Ddl_Section.SelectedIndex = 3
            Call Ddl_Section_SelectedIndexChanged(Me.Ddl_Section, System.EventArgs.Empty)


            'MODIFIED 
            'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), Ddl_Section.SelectedItem.ToString)
            'If Ddl_Section.Items.Count > 0 Then CmbScheme.SelectedIndex = 0
            'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))

            M_CommonBL.Fill_Branch(CmbMemBranch)
            M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"), "SHARE", "SHARE")
            M_CommonBL.GetSchemes(CmbLoanAppType, Session("TransDate"), ("LOAN','LOAN"), "LF','LS")
            M_CommonBL = Nothing
            CmbCustMem.Items.Add("CUSTOMER")
            CmbCustMem.Items.Add("MEMBER")
            CmbCustMem.Items.Add("ACCOUNT")
            CmbCustMem.SelectedIndex = 0
            SchemeDt = DirectCast(Cache("SchemeTbl"), DataTable)
            Session("MODULE") = "FD"
            Session("SECTION") = "DEPOSIT"
            Session("Lm_LoanAmount") = Nothing
            TxtNoOfOperator.Text = "1"
            Session("Tran") = "NEW"
            TxtPresDist.Text = Session("DefDist")
            TxtPresState.Text = Session("DefState")
            TxtPresCountry.Text = Session("DefCountry")

            ' CmbScheme.SelectedValue = "17"
            CmbPrivelage.SelectedValue = "417"
            TxtSchCode.Text = CmbScheme.SelectedValue
            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbOpMode_SelectedIndexChanged(Me.CmbOpMode, System.EventArgs.Empty)
            Session("SearchBranch") = ""
       
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Fill_Pickup(ByRef Cmbbx As Object, ByVal M_PkcType As Integer, ByVal M_PkcParentID As Integer)
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(Cmbbx, M_PkcType, M_PkcParentID)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbCustMem_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCustMem.SelectedIndexChanged
        Try
            Dim I As Integer

            Session("SearchScheme") = ""
            If CmbCustMem.SelectedIndex = 0 Then
                MemberIDDiv.Visible = False
                Session("SearchType") = "CUSTOMER"
            ElseIf CmbCustMem.SelectedIndex = 1 Then
                MemberIDDiv.Visible = True
                Session("SearchType") = "ACCOUNTNO"
                Session("SearchScheme") = CmbMemType.SelectedValue
                CmbMemType_SelectedIndexChanged(Me.CmbMemType, System.EventArgs.Empty)
                If CmbCustMem.Items(1).Text = "MEMBER" Then
                    For I = 0 To CmbMemBranch.Items.Count - 1
                        If Session("DefShareBranch") = CmbMemBranch.Items(I).Value Then
                            CmbMemBranch.SelectedValue = Session("DefShareBranch")
                            Exit For
                        End If
                    Next
                Else
                    CmbMemBranch.SelectedValue = Session("DefaultBrCode")
                End If
                Session("SearchBranch") = CmbMemBranch.SelectedValue
            ElseIf CmbCustMem.SelectedIndex = 2 Then
                MemberIDDiv.Visible = True
                Session("SearchType") = "ACCOUNTNO"
                Dim M_CommonBL As New CommonBL
                M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"))
                M_CommonBL = Nothing
                CmbMemType_SelectedIndexChanged(Me.CmbMemType, System.EventArgs.Empty)
                CmbMemBranch.SelectedValue = Session("DefaultBrCode")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next
        If M_CustMaster.CustId = 0 Then
            TxtCustID.Text = "NEW"
            Exit Sub
        End If
        If M_CustMaster.CustStatus = "I" Or M_CustMaster.CustStatus = "D" Then
            ShowAlertMessage("CUSTOMER IS NOT ACTIVE")
            Exit Sub
        End If

        With M_CustMaster
            CmbCustomerType.SelectedValue = .CustTypeCode
            Fill_Pickup(CmbCustomerCatg, 7, CmbCustomerType.SelectedValue)
            TxtCustName.Text = .CustName
            TxtCustID.Enabled = False
            CmbCustomerCatg.SelectedValue = 133 '.CustCatgCode
            TxtAge.Text = .CustAge
            TxtRelName.Text = .CustRelNm
            CmbRelType.SelectedValue = .CustRelTypCode
            TxtDob.Text = .CustDobInc
            TxtLandPhone.Text = .CustPhone1
            TxtMblNo.Text = .CustMobile
        End With

        TxtPresHouseName.Enabled = False
        TxtPresPlace.Enabled = False
        TxtPresState.Enabled = False
        TxtPresPin.Enabled = False
        TxtPresDist.Enabled = False
        TxtPresCountry.Enabled = False
        CmbCustomerType.Enabled = False
        CmbCustomerCatg.Enabled = False
        TxtRelName.Enabled = False
        CmbRelType.Enabled = False
        TxtDob.Enabled = False
        TxtCustName.Enabled = False
        TxtAge.Enabled = False
        TxtMblNo.Enabled = False
        TxtLandPhone.Enabled = False

        TxtPresHouseName.Text = M_PresAddress.AddAddress1
        TxtPresPlace.Text = M_PresAddress.AddAddress2
        TxtPresState.Text = M_PresAddress.AddAddress3
        TxtPresPin.Text = M_PresAddress.AddPinCode
        TxtPresDist.Text = M_PresAddress.AddDist
        TxtPresCountry.Text = M_PresAddress.AddCountry

    End Sub

    Protected Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustID.TextChanged
        If Ddl_Section.SelectedItem.Text = "SUSPENSE" Then
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable

            M_Dt = M_CommonBl.Check_SuspenseAccount(CmbBranch.SelectedValue, TxtCustID.Text, CmbScheme.SelectedValue)
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage("Already Account Opened On " + M_Dt.Rows(0).Item("ACC_OpenDate").ToString() + ", Account No Is : " + M_Dt.Rows(0).Item("ACC_No").ToString())
                TxtCustID.Text = ""
                Exit Sub
            End If
        End If

        Load_CustomerData()

    End Sub


    Private Sub Load_CustomerData()
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
            Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCustID.Text), M_PresAddress, M_PermAddress, M_PhotoID, M_ADDID)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_Grid()
        Try
            DgvCustomer.DataSource = Nothing
            AccHolderTbl.Columns.Add("MemDtls", GetType(String))
            AccHolderTbl.Columns.Add("MemBrCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSchCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSch", GetType(String))
            AccHolderTbl.Columns.Add("MemNo", GetType(String))
            AccHolderTbl.Columns.Add("CustID", GetType(String))
            AccHolderTbl.Columns.Add("CustName", GetType(String))
            AccHolderTbl.Columns.Add("Relation", GetType(String))
            AccHolderTbl.Columns.Add("CustAdd", GetType(String))
            AccHolderTbl.Columns.Add("CustType", GetType(String))
            AccHolderTbl.Columns.Add("CustCatg", GetType(String))
            AccHolderTbl.Columns.Add("HouseNm", GetType(String))
            AccHolderTbl.Columns.Add("Place", GetType(String))
            AccHolderTbl.Columns.Add("Dist", GetType(String))
            AccHolderTbl.Columns.Add("State", GetType(String))
            AccHolderTbl.Columns.Add("Country", GetType(String))
            AccHolderTbl.Columns.Add("Pincode", GetType(String))
            AccHolderTbl.Columns.Add("MblNo", GetType(String))
            AccHolderTbl.Columns.Add("LandPh", GetType(String))
            AccHolderTbl.Columns.Add("RelType", GetType(String))
            AccHolderTbl.Columns.Add("Dob", GetType(String))
            AccHolderTbl.Columns.Add("Age", GetType(String))

            AccHolderTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("AccHolderTbl") = Nothing
            Session("AccHolderTbl") = AccHolderTbl
            DgvCustomer.DataSource = AccHolderTbl
            DgvCustomer.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub ClearCustomer()
        Try
            TxtMemberNo.Text = ""
            TxtCustID.Text = ""
            TxtCustName.Text = ""
            TxtPresHouseName.Text = ""
            TxtPresPlace.Text = ""
            TxtPresDist.Text = ""
            'TxtPresState.Text = ""
            'TxtPresCountry.Text = ""
            TxtPresPin.Text = ""
            TxtRelName.Text = ""
            TxtMblNo.Text = ""
            TxtLandPhone.Text = ""
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Try
            Dim Expression As String

            If Val(TxtCustID.Text) = 0 Then TxtCustID.Text = "NEW"
            If Not (TxtCustID.Text = "NEW" Or Len(TxtCustID.Text) > 0) Then
                TxtCustID.Focus()
                ShowAlertMessage("INVALID CUSTOMER ID ENTERED")
                Exit Sub
            End If

            If Len(TxtCustName.Text) = 0 Then
                TxtCustName.Focus()
                ShowAlertMessage("INVALID CUSTOMER NAME ENTERED")
                Exit Sub
            End If

            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            If (dt.Rows.Count > 0) And Session("MODULE") <> "DEP.LOAN" Then
                If dt.Rows.Count = Val(TxtNoOfOperator.Text) And dt.Rows(0).Item(1).ToString <> "" Then
                    ShowAlertMessage("MAX NO OF CUSTOMER EXCEEDED")
                    Exit Sub
                End If

                If dt.Rows.Count >= Val(TxtNoOfOperator.Text) And dt.Rows(0).Item(1).ToString <> "" Then
                    ShowAlertMessage("MAX NO OF CUSTOMER EXCEEDED")
                    Exit Sub
                End If

            End If

            If Session("MODULE") = "DEP.LOAN" Then
                If TxtCustID.Text.Trim.Length > 0 And TxtCustID.Text <> "NEW" Then
                    Expression = "MemNo='" & TxtMemberNo.Text.Trim & "'"
                    If dt.[Select](Expression).Count > 0 Then
                        ShowAlertMessage("A/c No ALREADY ADDED")
                        Exit Sub
                    End If
                End If
            Else
                If TxtCustID.Text.Trim.Length > 0 And TxtCustID.Text <> "NEW" Then
                    Expression = "CustID='" & TxtCustID.Text.Trim & "'"
                    If dt.[Select](Expression).Count > 0 Then
                        ShowAlertMessage("CUSTOMER ALREADY ADDED")
                        Exit Sub
                    End If
                End If
            End If

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(TxtMemberNo.Text.Trim) > 0 Then
                Row("MemDtls") = CmbMemType.SelectedItem.Text.Trim + " " + TxtMemberNo.Text.Trim + " (" + CmbMemBranch.SelectedValue + " )"
            End If
            Row("MemBrCode") = CmbMemBranch.SelectedValue
            Row("MemSchCode") = CmbMemType.SelectedValue
            Row("MemSch") = CmbMemType.Text
            Row("MemNo") = TxtMemberNo.Text
            Row("CustID") = TxtCustID.Text
            Row("CustName") = TxtCustName.Text.ToUpper
            Row("Relation") = TxtRelName.Text.ToUpper
            Row("CustAdd") = TxtPresHouseName.Text.ToUpper + " " + TxtPresPlace.Text.ToUpper + " " + TxtPresDist.Text.ToUpper + " " + TxtPresState.Text.ToUpper + " " + TxtPresCountry.Text.ToUpper + " " + TxtPresPin.Text.ToUpper + " " + TxtMblNo.Text.ToUpper + " " + TxtLandPhone.Text.ToUpper
            Row("CustType") = CmbCustomerType.SelectedValue
            Row("CustCatg") = CmbCustomerCatg.SelectedValue
            Row("HouseNm") = TxtPresHouseName.Text.ToUpper
            Row("Place") = TxtPresPlace.Text.ToUpper
            Row("Dist") = TxtPresDist.Text.ToUpper
            Row("State") = TxtPresState.Text.ToUpper
            Row("Country") = TxtPresCountry.Text.ToUpper
            Row("Pincode") = TxtPresPin.Text.ToUpper
            Row("MblNo") = TxtMblNo.Text.ToUpper
            Row("LandPh") = TxtLandPhone.Text.ToUpper
            Row("RelType") = CmbRelType.SelectedValue
            If IsDate(TxtDob.Text) = False Then
                Row("Dob") = "01/01/1900"
            Else
                Row("Dob") = TxtDob.Text
            End If
            Row("Age") = Val(TxtAge.Text)
           
            Try
                If Session("Tran") = "Edit" Then
                    UpdateCustomerdetails()
                    dt.Rows.Add(Row)
                    ClearCustomer()
                Else
                    dt.Rows.Add(Row)
                    ClearCustomer()
                End If
               
            Catch ex As Exception

            End Try

            Try
                Expression = "CustID=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt

            TxtPresHouseName.Enabled = True
            TxtPresPlace.Enabled = True
            TxtPresState.Enabled = True
            TxtPresPin.Enabled = True
            TxtPresDist.Enabled = True
            TxtPresCountry.Enabled = True
            CmbCustomerType.Enabled = True
            CmbCustomerCatg.Enabled = True
            TxtCustID.Enabled = True
            TxtRelName.Enabled = True
            CmbRelType.Enabled = True
            TxtDob.Enabled = True
            TxtCustName.Enabled = True
            TxtAge.Enabled = True
            TxtMblNo.Enabled = True
            TxtLandPhone.Enabled = True

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

    Protected Sub ImgNext_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNext.Click
        Try
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            If dt.Rows.Count = 1 And dt.Rows(0)("CustID").ToString() = "" Then
                If CmbCustMem.SelectedIndex = 0 Then
                    ShowAlertMessage("INVALID CUSTOMER ENTERED")
                Else
                    ShowAlertMessage("INVALID MEMBER ENTERED")
                End If
                Exit Sub

                If (dt.Rows.Count > 0) And Session("MODULE") <> "DEP.LOAN" Then
                    If dt.Rows.Count = Val(TxtNoOfOperator.Text) And dt.Rows(0).Item(1).ToString <> "" Then
                        ShowAlertMessage("MAX NO OF CUSTOMER EXCEEDED")
                        Exit Sub
                    End If

                    If dt.Rows.Count >= Val(TxtNoOfOperator.Text) And dt.Rows(0).Item(1).ToString <> "" Then
                        ShowAlertMessage("MAX NO OF CUSTOMER EXCEEDED")
                        Exit Sub
                    End If
                End If

                If CmbOpMode.SelectedValue = 399 Then
                    If Val(TxtNoOfOperator.Text) <= 1 Then
                        ShowAlertMessage("INVALID NO OF CUSTOMER ENTERED")
                        Exit Sub
                    End If
                End If

            End If

            If dt.Rows.Count < Val(TxtNoOfOperator.Text) Then
                ShowAlertMessage("INCORRECT NO OF CUSTOMER ENTERED")
                Exit Sub
            End If

            If CmbOpMode.SelectedValue <> 399 Then
                If Val(TxtNoOfOperator.Text) <= 1 Then
                    ShowAlertMessage("INVALID NO OF CUSTOMER ENTERED")
                    Exit Sub
                End If
            End If
            Session("AcOpenBranch") = CmbBranch.SelectedValue
            Session("AcOpenScheme") = CmbScheme.SelectedValue
            Session("AcOpenPrivelage") = CmbPrivelage.SelectedValue
            Session("AcOpenOpMode") = CmbOpMode.SelectedValue
            Session("AcOpenNoOfOper") = Val(TxtNoOfOperator.Text)

            If Session("MODULE") = "GEN.LOAN" Or Session("MODULE") = "OD" Or Session("MODULE") = "CC" Then
                Session("LoanAppNo") = TxtApplnNo.Text
            Else
                Session("LoanAppNo") = Nothing
            End If

            Dim M_CommonBL As New CommonBL
            If Session("MODULE") = "GOLDLOAN" Then
                Response.Redirect("~/GoldOpen.aspx")
            ElseIf Session("SECTION") = "DEPOSIT" Then
                'Session("MODULE") = "FD" Or Session("MODULE") = "RD" Or Session("MODULE") = "SB" Or Session("MODULE") = "DD" Or Session("MODULE") = "RIS" Then
                Response.Redirect("~/DepositMaster.aspx")
            ElseIf Session("SECTION") = "INVESTMENT" Then
                'Session("MODULE") = "FD" Or Session("MODULE") = "RD" Or Session("MODULE") = "SB" Or Session("MODULE") = "DD" Or Session("MODULE") = "RIS" Then
                Response.Redirect("~/InvestmentMaster.aspx")
            ElseIf Session("MODULE") = "SHARE" Then
                Response.Redirect("~/ShareMaster.aspx")
            ElseIf Session("MODULE") = "OD" Then
                Response.Redirect("~/LoanMaster.aspx")
            ElseIf Session("MODULE") = "GEN.LOAN" Then
                Response.Redirect("~/LoanMaster.aspx")
            ElseIf Session("MODULE") = "DEP.LOAN" Then
                Response.Redirect("~/Depositloan.aspx")
            ElseIf Session("MODULE") = "GROUP DEPOSIT" Then
                Response.Redirect("~/GroupDeposit.aspx")
            ElseIf Session("MODULE") = "BANK" Then
                Response.Redirect("~/BankAccountMaster.aspx")
            ElseIf Session("MODULE") = "LOCKER" Then
                Response.Redirect("~/LockerMaster.aspx")
            ElseIf Session("MODULE") = "SL" Or Session("MODULE") = "SA" Or Session("MODULE") = "SUSPENSE" Or Session("MODULE") = "TT" Then
                Response.Redirect("~/SuspenseMaster.aspx")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Set_SchemeCtrl()
        Dim I As Integer

        On Error Resume Next
        Dim M_CommonBL As New CommonBL
        Session("MODULE") = ""
        Dim Expression As String = "Sch_Code = " & CmbScheme.SelectedValue
        Dim SortingOrder As String = "Sch_Name ASC"
        Dim drFiltered As DataRow() = SchemeDt.[Select](Expression, SortingOrder)
        For Each row As DataRow In drFiltered
            Session("MODULE") = row("Sch_Module_Catg").ToString()
            Session("SECTION") = row("Sch_Section_Name").ToString()
        Next
        If Session("MODULE") = "" Then
            Session("MODULE") = M_CommonBL.GetSchemeModule(CmbScheme.SelectedValue)
            Session("SECTION") = M_CommonBL.GetSchemeSection(CmbScheme.SelectedValue)
        End If
        'Set_Grid()

        'Fixing Default Values
        CmbCustMem.Items(1).Text = "MEMBER"  ' TO DO JAISON
        LblNoofOperator.Text = "No of Operators"
        LblMember.Text = "Member Branch &amp; Scheme"
        CmbMemBranch.Enabled = True
        LblMemNo.Text = "Member No"
        TxtNoOfOperator.Enabled = True
        CmbOpMode.Enabled = True
        CmbPrivelage.Enabled = True

        If Session("MODULE") = "GOLDLOAN" Or Session("MODULE") = "LOAN" Or Session("MODULE") = "DEP.LOAN" Or Session("MODULE") = "GEN.LOAN" Or Session("MODULE") = "OD" Or Session("MODULE") = "CC" Then
            ParameterDt = M_CommonBL.GetLoanParameter(CmbScheme.SelectedValue)
            If ParameterDt.Rows(0)("Lnp_ShareRequired").ToString() = "Y" Or Session("MODULE") = "DEP.LOAN" Then
                CmbCustMem.SelectedIndex = 0
                CmbCustMem.Enabled = True
                TxtCustID.Enabled = True
                'MemberIDDiv.Visible = True
                ImgSrch.Visible = True
                CmbMemBranch.Focus()
                If ParameterDt.Rows(0)("Lnp_ShareRequired").ToString() = "Y" Then
                    M_CommonBL.Fill_LinkAccounts(CmbMemType, CommonBL.LinkedAccounts.SHARE, CmbScheme.SelectedValue)
                End If

                If Session("MODULE") = "GOLDLOAN" Then
                    TxtNoOfOperator.Text = "1"
                    CmbOpMode.SelectedValue = 399
                    TxtNoOfOperator.Enabled = False
                    CmbOpMode.Enabled = False
                    CmbPrivelage.SelectedValue = 417
                    CmbPrivelage.Enabled = False
                ElseIf Session("MODULE") = "DEP.LOAN" Then
                    CmbOpMode.SelectedValue = 399
                    CmbPrivelage.SelectedValue = 417
                    CmbPrivelage.Enabled = False
                    CmbOpMode.Enabled = False
                    CmbCustMem.Enabled = False
                    TxtCustID.Enabled = False
                    ImgSrch.Visible = False
                    MemberIDDiv.Visible = True
                    CmbCustMem.Items(1).Text = "DEPOSIT"  ' TO DO JAISON
                    LblNoofOperator.Text = "No of Accounts"
                    LblMember.Text = "Deposit Branch &amp; Scheme"
                    ' CmbMemBranch.Enabled = False
                    LblMemNo.Text = "Deposit No"
                    M_CommonBL.Fill_LinkAccounts(CmbMemType, CommonBL.LinkedAccounts.SECURITY, CmbScheme.SelectedValue)
                    M_CommonBL.GetSchemes(CmbMemType, Session("TransDate"), "DEPOSIT")
                ElseIf (Session("MODULE") = "GEN.LOAN" Or Session("MODULE") = "OD" Or Session("MODULE") = "CC") And (ParameterDt.Rows(0)("Lnp_App_type").ToString() = "LS" Or ParameterDt.Rows(0)("Lnp_App_type").ToString() = "LF") Then
                    TxtNoOfOperator.Text = "1"
                    CmbOpMode.SelectedValue = 399
                    TxtNoOfOperator.Enabled = True
                    CmbOpMode.Enabled = True
                    CmbPrivelage.SelectedValue = 417
                    CmbPrivelage.Enabled = False
                    DivLoanApp.Visible = True
                    CmbLoanAppType.SelectedValue = ParameterDt.Rows(0)("Lnp_App_type").ToString()
                    CmbLoanAppType.Enabled = False
                    MemberIDDiv.Visible = False
                    TxtApplnNo.Focus()
                ElseIf Session("MODULE") = "GEN.LOAN" And ParameterDt.Rows(0)("GoldSecurityReq").ToString() = "Y" Then
                    MemberIDDiv.Visible = True
                    CmbCustMem.SelectedValue = "ACCOUNT"
                    LblMemNo.Text = "Gold Loan"
                    Session("SearchType") = "GOLDACCNO"
                    CmbMemBranch.SelectedValue = Session("DefaultBrCode")
                    CmbCustMem.Enabled = False
                    Call CmbMemBranch_SelectedIndexChanged(Me.CmbMemBranch, System.EventArgs.Empty)
                    M_CommonBL.Fill_LinkAccounts(CmbMemType, CommonBL.LinkedAccounts.GOLD, CmbScheme.SelectedValue)
                    Session("SearchScheme") = CmbMemType.SelectedValue
                    TxtApplnNo.Enabled = False
                    TxtMemberNo.Focus()
                    TxtCustID.Enabled = False
                    ImgSrch.Visible = False
                    ClearCustomer()
                End If
        Else
            TxtNoOfOperator.Enabled = True
            CmbOpMode.Enabled = True
            CmbCustMem.SelectedIndex = 0
            CmbCustMem.Enabled = True
            MemberIDDiv.Visible = False
            DivLoanApp.Visible = False
        End If
        ElseIf Session("MODULE") = "SHARE" Then
            TxtNoOfOperator.Text = "1"
            CmbOpMode.SelectedValue = 399
            TxtNoOfOperator.Enabled = False
            CmbOpMode.Enabled = False
            CmbPrivelage.SelectedValue = 417
            CmbPrivelage.Enabled = False
            CmbCustMem.SelectedIndex = 0
            CmbCustMem.Enabled = False
        Else
            TxtCustID.Enabled = True
            CmbCustMem.Enabled = True
            TxtNoOfOperator.Enabled = True
            CmbOpMode.Enabled = True
            CmbCustMem.SelectedIndex = 0
            MemberIDDiv.Visible = False
            DivLoanApp.Visible = False
            CmbPrivelage.Enabled = True
        End If

        CmbOpMode_SelectedIndexChanged(Me.CmbOpMode, System.EventArgs.Empty)
        'CmbCustMem_SelectedIndexChanged(CmbCustMem, System.EventArgs.Empty)
    End Sub
    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtSchCode.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        If Session("MODULE") = "DEP.LOAN" Then
            Session("SearchType") = "ACCOUNTNO"
        End If
    End Sub

    Protected Sub CmbOpMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbOpMode.SelectedIndexChanged
        If CmbOpMode.SelectedValue = 399 Then
            TxtNoOfOperator.Text = "1"
            TxtNoOfOperator.ReadOnly = True
        Else
            TxtNoOfOperator.Text = ""
            TxtNoOfOperator.ReadOnly = False
        End If
    End Sub

    Protected Sub TxtMemberNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemberNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_MemNo As String = ""
            Dim Dt As New DataTable
            Dim Status As String

            If TxtMemberNo.Text.Trim.Length <= 7 Then
                TxtMemberNo.Text = Right("000" + CmbMemBranch.SelectedValue, 3) + Right("000" + CmbMemType.SelectedValue, 3) + Right("0000000" + TxtMemberNo.Text, 7)
            End If

            If Session("MODULE") = "DEP.LOAN" Then 'By
                'Dt = M_CommonBL.GetAccountDetails(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, TxtMemberNo.Text)
                Dt = M_CommonBL.Get_DepositDetails(TxtMemberNo.Text)
                CmbMemBranch.SelectedValue = Dt.Rows(0)("Br_Code").ToString()
                CmbMemType.SelectedValue = Dt.Rows(0)("Sch_Code").ToString()
                If Dt.Rows.Count > 0 Then
                    Status = Dt.Rows(0).Item("Dm_Status").ToString()
                    If Status = "L" Then
                        ShowAlertMessage("This  Account is Pledged for Another Deposit Loan. ")
                        TxtMemberNo.Focus()
                        Exit Sub
                    End If
                End If
            End If


            If Session("MODULE") = "OD" Or Session("MODULE") = "GOLDLOAN" Or Session("MODULE") = "GEN.LOAN" Then 'By
                Dt = M_CommonBL.GetAccountDetails(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, TxtMemberNo.Text)
                If Dt.Rows.Count > 0 Then
                    Status = Dt.Rows(0).Item("Lm_Status").ToString()
                    If Status = "C" Then
                        ShowAlertMessage("This  Account is Closed. ")
                        TxtMemberNo.Focus()
                        Exit Sub
                    End If
                End If
                Dt = Nothing
                Dt = M_CommonBL.GetAccountDetails(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, TxtMemberNo.Text, True, Session("TransDate"))
                If Dt.Rows.Count > 0 Then
                    Dim I As Integer
                    Dim Msg As String
                    For I = 0 To Dt.Rows.Count - 1
                        Msg = Msg + Dt.Rows(0).Item("Details").ToString()
                    Next
                    Msg = " Today This Member is Opened for the following  Loans  " + Msg
                    ShowAlertMessage(Msg)
                End If

            End If


            Dim M_LoanCount As New DataTable 'LCount
            M_LoanCount = M_CommonBL.PL_ApprovalPending(TxtMemberNo.Text)
            If M_LoanCount.Rows.Count > 0 Then
                If Val(M_LoanCount.Rows(0).Item("LCount")) > 0 Then
                    ShowAlertMessage("Security Loan Approval Pending")
                    Exit Sub
                End If
            End If





            TxtCustID.Text = M_CommonBL.GetAccountCustID(CmbMemBranch.SelectedValue, CmbMemType.SelectedValue, TxtMemberNo.Text)

            If Session("MODULE") = "DEP.LOAN" And TxtCustID.Text = 0 Then ' By 
                ShowAlertMessage("Invalid  Account Number ")
                TxtMemberNo.Focus()
                Exit Sub
            End If

            If Len(M_MemNo.Trim) > 0 Then
                TxtMemberNo.Text = M_MemNo
            End If
            M_CommonBL = Nothing
            Load_CustomerData()
            If Len(TxtCustID.Text) > 0 Then CmdAdd.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub ImgNext0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNext0.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdCustClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdCustClear.Click
        Try
            TxtCustID.Enabled = True
            CmbCustMem.Enabled = True
            TxtNoOfOperator.Enabled = True
            CmbOpMode.Enabled = True
            CmbCustMem.SelectedIndex = 0
            MemberIDDiv.Visible = False
            CmbPrivelage.Enabled = True

            TxtPresHouseName.Enabled = True
            TxtPresPlace.Enabled = True
            TxtPresState.Enabled = True
            TxtPresPin.Enabled = True
            TxtPresDist.Enabled = True
            TxtPresCountry.Enabled = True
            CmbCustomerType.Enabled = True
            CmbCustomerCatg.Enabled = True
            TxtCustID.Enabled = True
            TxtRelName.Enabled = True
            CmbRelType.Enabled = True
            TxtDob.Enabled = True
            TxtCustName.Enabled = True
            TxtAge.Enabled = True
            TxtMblNo.Enabled = True
            TxtLandPhone.Enabled = True

            TxtCustID.Text = ""
            TxtNoOfOperator.Text = ""
            TxtPresHouseName.Text = ""
            TxtPresPlace.Text = ""
            TxtPresState.Text = ""
            TxtPresPin.Text = ""
            TxtPresDist.Text = ""
            TxtPresCountry.Text = ""
            TxtCustID.Text = ""
            TxtRelName.Text = ""
            TxtDob.Text = ""
            TxtCustName.Text = ""
            TxtAge.Text = ""
            TxtMblNo.Text = ""
            TxtLandPhone.Text = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   

    Private Sub DgvCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvCustomer.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvCustomer.DataKeys(e.RowIndex).Value)
            DeleteCust(CustCode)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeleteCust(ByVal CustCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            For i = 0 To DgvCustomer.Rows.Count - 1
                If dt.Rows.Item(i).Item(5).ToString = CustCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt

            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub DgvCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvCustomer.SelectedIndexChanged

    End Sub

    Protected Sub CmbMemType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbMemType.SelectedIndexChanged
        Session("SearchScheme") = CmbMemType.SelectedValue
    End Sub

   


    Protected Sub TxtApplnNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtApplnNo.TextChanged
        Try
            Dim Dt_AcDtlsN As DataTable
            Dim Dt_AccStatus As DataTable
            Dim Dt_MemDtls As DataTable
            Dim M_loanAppBL As New LoanApplicationBL
            Dim M_CommonBL As New CommonBL
            Dt_AccStatus = M_loanAppBL.CheckAppFormStatus("", TxtApplnNo.Text)

            If Dt_AccStatus.Rows.Count > 0 Then
                '  Call ClearMemberDetails()
                If Dt_AccStatus.Rows(0)("LM_Status").ToString() = "A" Then
                    'ShowAlertMessage("This Form No is  Registered with " & Dt_AccStatus.Rows(0)("LM_AccNo").ToString() & " Application No.")
                    'Exit Sub
                ElseIf Dt_AccStatus.Rows(0)("LM_Status").ToString() = "C" Then
                    TxtApplnNo.Text = ""
                    ShowAlertMessage("This Loan Application is Closed")
                    Exit Sub
                Else
                    TxtApplnNo.Text = ""
                    ShowAlertMessage("This Loan Application is Not Approved")
                    Exit Sub
                End If

               
            End If



            '  Dim M_AccNo As String = TxtMemNo.Text

            Dt_AcDtlsN = M_CommonBL.GetLoanApplicationDtls(CmbBranch.SelectedValue, TxtApplnNo.Text, IIf(CmbLoanAppType.SelectedValue = "LS", "S", "O"), Session("TransDate")) 'M_loanAppBL.GetLoanApplicationFormDtls(TxtApplnNo.Text)
            If Dt_AcDtlsN.Rows.Count > 0 Then
                ' HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
                ' Session("CustomerId") = TxtCustID.Text
                'TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                'TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                'TxtAmsam.Text = Dt_AcDtlsN.Rows(0)("Cust_Amsam").ToString()
                ' TxtDesam.Text = Dt_AcDtlsN.Rows(0)("Cust_Desam").ToString()

                '  HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Shr_Accid").ToString()
                TxtMemberNo.Text = Dt_AcDtlsN.Rows(0)("Acc_NO").ToString()
                'CmbDirector.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_ID").ToString()
                CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Lm_LoanSchCode").ToString()
                Dt_MemDtls = M_CommonBL.Get_MemberDetails(Dt_AcDtlsN.Rows(0)("Lm_ID").ToString())
                CmbMemBranch.SelectedValue = Dt_MemDtls.Rows(0)("Br_Code").ToString()
                CmbMemType.SelectedValue = Dt_MemDtls.Rows(0)("scheme").ToString()
                TxtCustID.Text = Dt_MemDtls.Rows(0)("CUSTID").ToString()
                TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                Session("Lm_LoanAmount") = Dt_AcDtlsN.Rows(0)("Disbursement").ToString()
            Else
                'Call ClearMemberDetails()
                ShowAlertMessage("Invalid Loan Application Number ")
                Session("Lm_LoanAmount") = Nothing
            End If

            M_loanAppBL = Nothing
            M_CommonBL = Nothing


        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmbLoanAppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanAppType.SelectedIndexChanged

    End Sub

    Protected Sub CmbMemBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbMemBranch.SelectedIndexChanged
        Session("SearchBranch") = CmbMemBranch.SelectedValue
    End Sub

    Protected Sub Ddl_Section_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Section.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        If Ddl_Section.Text = "GROUP DEPOSIT" Then
            CmbScheme.Enabled = False
        Else
            Try
                M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), Ddl_Section.SelectedItem.ToString, , , , , , , , , , , IIf(Ddl_Section.SelectedItem.ToString = "LOAN", DrpLoanType.SelectedValue, ""), "N", Session("DefaultBrCode").ToString())
                TxtSchCode.Text = CmbScheme.SelectedValue
                If Ddl_Section.SelectedItem.ToString = "LOAN" Then
                    DivLoanType.Visible = True
                Else
                    DivLoanType.Visible = False
                End If
                Set_SchemeCtrl()
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

        End If

         
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtSchCode.Text
        TxtSchCode.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Edit.Click
        Try
            TxtCustID.Enabled = True
            CmbCustMem.Enabled = True
            TxtNoOfOperator.Enabled = True
            CmbOpMode.Enabled = True
            CmbCustMem.SelectedIndex = 0
            MemberIDDiv.Visible = False
            CmbPrivelage.Enabled = True
            Session("Tran") = "Edit"
            TxtPresHouseName.Enabled = True
            TxtPresPlace.Enabled = True
            TxtPresState.Enabled = True
            TxtPresPin.Enabled = True
            TxtPresDist.Enabled = True
            TxtPresCountry.Enabled = True
            CmbCustomerType.Enabled = True
            CmbCustomerCatg.Enabled = True
            TxtCustID.Enabled = True
            TxtRelName.Enabled = True
            CmbRelType.Enabled = True
            TxtDob.Enabled = True
            TxtCustName.Enabled = True
            TxtAge.Enabled = True
            TxtMblNo.Enabled = True
            TxtLandPhone.Enabled = True
            TxtCustID.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub UpdateCustomerdetails()
        Try
            If IsDate(TxtDob.Text) = False Then
                TxtDob.Text = "01/01/1900"
            End If

            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_custBL As New CustMasterBL
            M_CustMaster.CustId = TxtCustID.Text.Trim()
            M_CustMaster.CustName = TxtCustName.Text.Trim()
            M_CustMaster.CustRelNm = TxtRelName.Text.Trim()
            M_CustMaster.CustDobInc = IIf(IsDate(TxtDob.Text), TxtDob.Text, "01/01/1900")
            M_CustMaster.CustAge = TxtAge.Text.Trim()
            M_CustMaster.CustMobile = TxtMblNo.Text.Trim()
            M_CustMaster.CustPhone1 = TxtLandPhone.Text.Trim()
            M_PresAddress.AddAddress1 = TxtPresHouseName.Text.Trim()
            M_PresAddress.AddAddress2 = TxtPresPlace.Text.Trim()
            M_PresAddress.AddAddress3 = TxtPresState.Text.Trim()
            M_PresAddress.AddDist = TxtPresDist.Text.Trim()
            M_PresAddress.AddPinCode = TxtPresPin.Text.Trim()
            M_PresAddress.AddCountry = TxtPresCountry.Text.Trim()
            Dim i As Integer = M_custBL.Cust_Updatedata(M_CustMaster, M_PresAddress)
            'If (i > 0) Then
            '    ShowAlertMessage("Successful Update")
            'Else
            '    ShowAlertMessage("Server Error Try Again")
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpLoanType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpLoanType.SelectedIndexChanged
        Call Ddl_Section_SelectedIndexChanged(Me.Ddl_Section, System.EventArgs.Empty)
    End Sub
End Class