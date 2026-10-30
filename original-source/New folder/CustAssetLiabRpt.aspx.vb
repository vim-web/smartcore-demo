Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports System.Data

Public Class CustAssetLiabRpt
    Inherits System.Web.UI.Page

    Private Shared Common As New CommonBL
    Private Shared ClsCommon As New CommonClass
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1038
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Call FillCombo()
                Txt_DueDate.Text = Session("TransDate")
                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

            End If

            If Session("SearchResult") <> "" Then
                
                TxtCustId.Text = Session("SearchResult")
                TxtCustId_TextChanged(Me.TxtCustId, System.EventArgs.Empty)
                TxtCustId.Focus()
                Session("SearchResult") = ""
                Session("SearchBranch") = ""
            End If
            
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillCombo()
        Try
            Common.Fill_Branch(CmbBranch)
            'Common.GetSchemes(CmbMembershipType, Session("TransDate"), "SHARE")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Session("SearchBranch") = Session("DefaultBrCode").ToString()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    'Protected Sub TxtMemberNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtMemberNo.TextChanged
    '    Try
    '        TxtMemberNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbMembershipType.SelectedValue, 3) + Right("0000000" + TxtMemberNo.Text, 7)

    '        Dim M_Dt As New DataTable
    '        M_Dt = Common.GetAccountDetails(CmbBranch.SelectedValue, CmbMembershipType.SelectedValue, TxtMemberNo.Text)

    '        If M_Dt.Rows.Count > 0 Then
    '            TxtCustId.Text = M_Dt.Rows(0)("Cust_ID").ToString()
    '            Locate_customer()
    '        Else
    '            ShowAlertMessage("INVALID ACCOUNT NO")
    '        End If
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub Locate_customer(ByVal Cust_Id As String)
        Try
            Dim M_Dt As New DataTable
            M_Dt = Common.GetCustDetails(Cust_Id)

            If M_Dt.Rows.Count > 0 Then
                TxtCustName.Text = M_Dt.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = M_Dt.Rows(0)("relname").ToString() + " ,  " + M_Dt.Rows(0)("Full_Address").ToString()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtCustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustId.TextChanged
        Try
            Locate_customer(TxtCustId.Text)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim SqlStr As String
            Dim Str_flf As String
            SqlStr = "Exec Get_CustAssetLby_Sp " & ClsCommon.DbDate(Txt_DueDate.Text) & ",'" & TxtCustId.Text & _
            "','" & CmbBranch.SelectedValue & "','" & Session("HostID") & "','" & "Y" & _
            "','','','' "

            M_DBConn = New Connection()
            M_DBConn.ExecuteScalar(SqlStr, , False)

            If Opt_LiabilityRegister.Checked = True Then
                SqlStr = " Select C.*,B.Br_Name,case when cast(CONVERT(VARCHAR(10),AM.ACC_ClosedDate ,103) as varchar(10))='01/01/1900' then '' else cast(CONVERT(VARCHAR(10),AM.ACC_ClosedDate ,103) as varchar(10))   end ACC_ClosedDate From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code join AccMain_M_Tbl AM on AM.ACC_No=C.Acc_No" & _
                        "   Where Terminal ='" & Session("HostID") & "'"
                If ChkClosedOnly.Checked = True Then
                    SqlStr = SqlStr & " And Balance=0 "
                ElseIf ChkIncludeClosed.Checked = False Then
                    SqlStr = SqlStr & " And Balance<>0 "
                End If
                If RadAsset.Checked = True Then
                    SqlStr = SqlStr & " And Ast_Liab='A' "
                ElseIf RadLiability.Checked = True Then
                    SqlStr = SqlStr & " And Ast_Liab='L' "
                End If
            ElseIf Opt_LiabilityCertificate.Checked = True Then
                'If Chk_NonCustomer.Value = vbUnchecked Then
                SqlStr = " Select Top 1 C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,M.Sch_Code As Lm_Shr_Sch_Code,S.Sch_Name As Member_Type,AM.ACC_ClosedDate , " & _
                    "M.Sm_MemNo As Lm_MemNo,0 As Lm_LoanAmount,S.Sch_Section_Name " & _
                    "From CustAsstLiab_R_Tbl C " & _
                    "Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                    "Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID  " & _
                    "Join Share_Master_M_Tbl M On M.Br_Code=C.Br_Code And M.Sch_Code=C.Sch_Code And M.Sm_MemNo=C.Acc_No " & _
                    "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.Sch_Code join AccMain_M_Tbl AM on AM.ACC_No=C.Acc_No " & _
                    " Where Terminal ='" & Session("HostID") & "' Union All " & Chr(13)

                SqlStr = SqlStr & " Select C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,L.Lm_Shr_Sch_Code,S.Sch_Name As Member_Type,AM.ACC_ClosedDate,L.Lm_MemNo,L.Lm_LoanAmount,LS.Sch_Section_Name " & _
                        " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        " Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID Join Loan_Master_M_Tbl L On L.Br_Code=C.Br_Code And L.Sch_Code=C.Sch_Code And L.Acc_No=C.Acc_No " & _
                        " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=L.Lm_Shr_Sch_Code " & _
                        " Join Gen_SchemeMaster_P_Tbl LS On LS.Sch_Code=L.Sch_Code join AccMain_M_Tbl AM on AM.ACC_No=C.Acc_No" & _
                        " Where  C.Bal_Type='D' And Terminal ='" & Session("HostID") & "' "
                'Else ' Non Customer
                '    Str_Sql = "Select C.*,B.Br_Name,'' As Cust_RelTyp_Code,'' As Cust_RelNm,'' As Lm_Shr_Sch_Code,'' As Sch_Name,'' As Member_Type, '' As Lm_MemNo,0 As Lm_LoanAmount,'SHARE' As Sch_Section_Name " & _
                '            " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                '            " Where Terminal ='" & System_Name & "' "
            ElseIf Opt_NonLiabilityCertificate.Checked = True Then
                'If Chk_NonCustomer.Value = vbUnchecked Then
                Dim Dt As DataTable
                SqlStr = " Select C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,L.Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, L.Lm_MemNo,L.Lm_LoanAmount,LS.Sch_Section_Name " & _
                        " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        " Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID Join Loan_Master_M_Tbl L On L.Br_Code=C.Br_Code And L.Sch_Code=C.Sch_Code And L.Acc_No=C.Acc_No " & _
                        " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=L.Lm_Shr_Sch_Code " & _
                        " Join Gen_SchemeMaster_P_Tbl LS On LS.Sch_Code=L.Sch_Code " & _
                        " Where  C.Bal_Type='D'  and C. balance > 0 And Terminal ='" & Session("HostID") & "' "
                M_DBConn.ExecuteDataTable(Dt, SqlStr, False)

                If Dt.Rows.Count > 0 Then
                    ShowAlertMessage(" Non Liability Certificate is not able to Issue this Member .")
                    Exit Sub
                End If


                SqlStr = " Select Top 1 C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,M.Sch_Code As Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, " & _
                        "M.Sm_MemNo As Lm_MemNo,0 As Lm_LoanAmount,S.Sch_Section_Name " & _
                        "From CustAsstLiab_R_Tbl C " & _
                        "Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        "Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID  " & _
                        "Join Share_Master_M_Tbl M On M.Br_Code=C.Br_Code And M.Sch_Code=C.Sch_Code And M.Sm_MemNo=C.Acc_No " & _
                        "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.Sch_Code  " & _
                        " Where Terminal ='" & Session("HostID") & "' "
            End If

            If Opt_LiabilityRegister.Checked = True Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_AssLibLatest2.rpt"))
            ElseIf Opt_LiabilityCertificate.Checked Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_AssLib_Certificate.rpt"))
            ElseIf Opt_NonLiabilityCertificate.Checked Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_NonLib_Certificate.rpt"))
            End If
            Session.Add("RptQry", SqlStr)
            Session.Add("SubRptNames", "")

            Str_flf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Date|  " & Txt_DueDate.Text & "|Branch|" & CmbBranch.SelectedItem.Text & " |User|" & Session("LoginID") & "|Heading|Customer " & "Asset Liability" & IIf(Opt_LiabilityCertificate.Checked = True, "Register", "Certificate") & "" & ""
            Str_flf = Str_flf + "|Footer1|Clerk/Acct|Footer2|Chief Acct/Asst.Secretary|Footer3|Secretary|Incude_Closed|" & "N"
            Session.Add("RptFormula", Str_flf)



            Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
            '        Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub xx()
        Try
            Dim SqlStr As String
            Dim Str_flf As String
            SqlStr = "Exec Get_CustAssetLby_Sp " & ClsCommon.DbDate(Txt_DueDate.Text) & ",'" & TxtCustId.Text & _
            "','" & CmbBranch.SelectedValue & "','" & Session("HostID") & "','" & "Y" & _
            "','','','' "

            M_DBConn = New Connection()
            M_DBConn.ExecuteScalar(SqlStr, , False)
            Dim Dt As DataTable

            If Opt_LiabilityRegister.Checked = True Then
                SqlStr = " Select C.*,B.Br_Name From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code" & _
                        "   Where Terminal ='" & Session("HostID") & "'"
                If ChkClosedOnly.Checked = True Then
                    SqlStr = SqlStr & " And Balance=0 "
                ElseIf ChkIncludeClosed.Checked = False Then
                    SqlStr = SqlStr & " And Balance<>0 "
                End If
            ElseIf Opt_NonLiabilityCertificate.Checked = True Then
                'If Chk_NonCustomer.Value = vbUnchecked Then
                SqlStr = " Select C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,L.Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, L.Lm_MemNo,L.Lm_LoanAmount,LS.Sch_Section_Name " & _
                        " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        " Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID Join Loan_Master_M_Tbl L On L.Br_Code=C.Br_Code And L.Sch_Code=C.Sch_Code And L.Acc_No=C.Acc_No " & _
                        " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=L.Lm_Shr_Sch_Code " & _
                        " Join Gen_SchemeMaster_P_Tbl LS On LS.Sch_Code=L.Sch_Code " & _
                        " Where  C.Bal_Type='D' And Terminal ='" & Session("HostID") & "' "
                M_DBConn.ExecuteDataTable(Dt, SqlStr, False)

                If Dt.Rows.Count > 0 Then
                    ShowAlertMessage("")
                    Exit Sub
                End If

                'Else ' Non Customer
                '    Str_Sql = "Select C.*,B.Br_Name,'' As Cust_RelTyp_Code,'' As Cust_RelNm,'' As Lm_Shr_Sch_Code,'' As Sch_Name,'' As Member_Type, '' As Lm_MemNo,0 As Lm_LoanAmount,'SHARE' As Sch_Section_Name " & _
                '            " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                '            " Where Terminal ='" & System_Name & "' "

                SqlStr = " Select Top 1 C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,M.Sch_Code As Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, " & _
                        "M.Sm_MemNo As Lm_MemNo,0 As Lm_LoanAmount,S.Sch_Section_Name " & _
                        "From CustAsstLiab_R_Tbl C " & _
                        "Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        "Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID  " & _
                        "Join Share_Master_M_Tbl M On M.Br_Code=C.Br_Code And M.Sch_Code=C.Sch_Code And M.Sm_MemNo=C.Acc_No " & _
                        "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.Sch_Code  " & _
                        " Where Terminal ='" & Session("HostID") & "' "


            ElseIf Opt_LiabilityCertificate.Checked = True Then
                'If Chk_NonCustomer.Value = vbUnchecked Then
                SqlStr = " Select Top 1 C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,M.Sch_Code As Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, " & _
                    "M.Sm_MemNo As Lm_MemNo,0 As Lm_LoanAmount,S.Sch_Section_Name " & _
                    "From CustAsstLiab_R_Tbl C " & _
                    "Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                    "Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID  " & _
                    "Join Share_Master_M_Tbl M On M.Br_Code=C.Br_Code And M.Sch_Code=C.Sch_Code And M.Sm_MemNo=C.Acc_No " & _
                    "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.Sch_Code  " & _
                    " Where Terminal ='" & Session("HostID") & "' and ast_lib='L' and balance >0 Union All " & Chr(13)

                SqlStr = SqlStr & " Select C.*,B.Br_Name,V.Cust_RelTyp_Code,V.Cust_RelNm,L.Lm_Shr_Sch_Code,S.Sch_Name As Member_Type, L.Lm_MemNo,L.Lm_LoanAmount,LS.Sch_Section_Name " & _
                        " From CustAsstLiab_R_Tbl C Join Gen_BranchDetails_P_Tbl B On B.Br_Code=C.Br_Code " & _
                        " Join Cust_Details_Vw V On V.Cust_Id=C.Cust_ID Join Loan_Master_M_Tbl L On L.Br_Code=C.Br_Code And L.Sch_Code=C.Sch_Code And L.Acc_No=C.Acc_No " & _
                        " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=L.Lm_Shr_Sch_Code " & _
                        " Join Gen_SchemeMaster_P_Tbl LS On LS.Sch_Code=L.Sch_Code " & _
                        " Where  C.Bal_Type='D' And Terminal ='" & Session("HostID") & "' and ast_lib='L'  and balance >0"





            End If

            If Opt_LiabilityRegister.Checked = True Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_AssLibLatest2.rpt"))
            ElseIf Opt_LiabilityCertificate.Checked Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_AssLib_Certificate.rpt"))
            ElseIf Opt_NonLiabilityCertificate.Checked Then
                Session.Add("RptPath", Server.MapPath("~/Reports/CUSTOMER/Cust_NonLib_Certificate.rpt"))
            End If
            Session.Add("RptQry", SqlStr)
            Session.Add("SubRptNames", "")

            Str_flf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Date|  " & Txt_DueDate.Text & "|Branch|" & CmbBranch.SelectedItem.Text & " |User|" & Session("LoginID") & "|Heading|Customer " & "Asset Liability" & IIf(Opt_LiabilityCertificate.Checked = True, "Register", "Certificate") & "" & ""
            Str_flf = Str_flf + "|Footer1|Clerk/Acct|Footer2|Chief Acct/Asst.Secretary|Footer3|Secretary|Incude_Closed|" & "N"
            Session.Add("RptFormula", Str_flf)



            Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
            '        Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub



    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Session("SearchBranch") = CmbBranch.SelectedValue.ToString
    End Sub
End Class