Imports System.Data
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports SMARTCORE_BL

Partial Public Class TranCancellation
    Inherits System.Web.UI.Page

    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1141
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_Branch(DrpBranch)
            DrpBranch.SelectedValue = Session("DefaultBrCode")
            DgvPassing.DataSource = GetTable()
            DgvPassing.DataBind()
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
        Dim Tran_No As String
        Dim M_DBConn As New Connection()
        Dim SqlStr As String
        Dim IsDelete As Boolean = False

        'Delete Right
        If Obj_UserRights.Delete_Right = False Then
            ShowAlertMessage("You Have No Permission To Delete!!!")
            Exit Sub
        End If
        Dim M_CommonBL As New CommonBL


        If e.CommandName.ToUpper = "SELECT" Then
            Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())
            IsDelete = DgvPassing.DataKeys(RowIndex)("Authorised").ToString()

            'If IsDelete = True And Session("LoginID").ToString.ToUpper() <> "SA" Then
            '    ShowAlertMessage("Unable to Delete Transaction.Transaction is Authorised!!!")
            '    Exit Sub
            'End If

            If IsDelete = True Then
                'Session("LoginID").ToString.ToUpper() <> "SA" 
                Dim dtLogin As DataTable
                dtLogin = M_CommonBL.ValLoginIdAllowedDeleteTran(Session("LoginID").ToString)
                If dtLogin.Rows.Count > 0 Then
                    If dtLogin.Rows(0).Item("allowed") = "N" Then
                        ShowAlertMessage("Unable to Delete Transaction.Transaction is Authorised!!!")
                        Exit Sub
                    End If
                End If
            End If

            'If DgvPassing.DataKeys(RowIndex)("Authorised").ToString() = False Then
            Tran_No = DgvPassing.DataKeys(RowIndex)("Tran_No").ToString()
            'cancel ----------------------------------------------------------

            Dim FromNm As String
            FromNm = M_CommonBL.Get_TranFrom(Tran_No)

            Select Case FromNm
                Case "GOLD"
                    Response.Redirect("~/GoldOpen.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "DEPMAS"
                    Response.Redirect("~/DepositMaster.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "SHAREMAS"
                    Response.Redirect("~/ShareMaster.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "MLOAN"
                    Response.Redirect("~/LoanMaster.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "SUSPMAS", "TOURMAS"
                    Response.Redirect("~/SuspenseMaster.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "TRAN"
                    Response.Redirect("~/Transaction.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
                Case "BANKTR"
                    Response.Redirect("~/CashTr.aspx?TRANID=" & Tran_No & "&DEL='1'&DelBranch=" & DrpBranch.SelectedValue.ToString() & "")
                    Exit Sub
            End Select
            SqlStr = "Exec Transaction_Delete_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
                ",'" & Session("UserID") & "','" & DrpBranch.SelectedValue.ToString() & "'"
            M_DBConn.ExecuteScalar(SqlStr, , False)
            Call Refresh()
            'End If
        End If
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Public Enum PassingType
        Pending = 0
        Passed
        All
    End Enum

    Public Sub FillPassingData(ByRef Dgv As Object, Optional ByVal FillType As PassingType = PassingType.Pending)
        Dim SqlString As String = ""
        Dim Dt As DataTable = New DataTable()
        Dim M_DBConn As New Connection()

        SqlString = "Select Cast(Max(Case When Authorised='Y' Then 1 Else 0 End) As Bit) As Authorised,Max(T.Vr_No) As VouNo,CASE WHEN max(T.Sch_Code) ='VOU' THEN Isnull(Max(A.Acc_Name),'')+' - ' + max(Narration+' '+Narration1) ELSE Isnull(Max(S.Sch_Name),'')+'-'+Isnull(Max(M.ACC_No),'') END As Particulars,"
        SqlString = SqlString & " RecCash = Sum((CASE WHEN TRAN_TYPE = 'C' THEN CASH ELSE 0 END)),"
        SqlString = SqlString & " RecTransfer = Sum((CASE WHEN TRAN_TYPE = 'C' THEN TRANSFER ELSE 0 END)),"
        SqlString = SqlString & " PayCash = Sum((CASE WHEN TRAN_TYPE = 'D' THEN CASH ELSE 0 END)),"
        SqlString = SqlString & " PayTransfer = Sum((CASE WHEN TRAN_TYPE = 'D' THEN TRANSFER ELSE 0 END)),"
        If OptPassed.Checked = True Then
            SqlString = SqlString + " Max(Isnull(CU.Emp_Name,'')) As MakerID"
        Else
            SqlString = SqlString + " Max(Isnull(MU.Emp_Name,'')) As MakerID"
        End If
        SqlString = SqlString + ",Max(T.Making_Time) As MakingTime, "
        SqlString = SqlString & " T.Tran_No"
        SqlString = SqlString & " From Transaction_T_Tbl T"
        SqlString = SqlString & " Left Outer Join AccMain_M_Tbl M On M.ACC_ID=T.Acc_ID"
        SqlString = SqlString & " Left Outer Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=M.ACC_SchCode"
        SqlString = SqlString & " Left Outer Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=T.Gl_Code"
        SqlString = SqlString & " Left Outer Join User_Details_Vw MU On MU.User_Name=T.MakerID"
        SqlString = SqlString & " Left Outer Join User_Details_Vw CU On CU.User_Name=T.CheckerID"
        SqlString = SqlString & " Where T.Cash+T.Transfer>0 "
        SqlString = SqlString + " And Tr_date = " + ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
        "  And Org_Branch = '" & DrpBranch.SelectedValue.ToString() & "'"  ''Changed By Gowtham For Delete Other Branch Transaction


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


        If Dt.Rows.Count = 0 Then
            PnlStatus.Visible = True
        Else
            PnlStatus.Visible = False
        End If

        SqlString = "Exec NonTranction_AuthoriseFill_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
        If OptPassed.Checked = True Then
            SqlString = SqlString + ",'Y' "
        ElseIf OptPending.Checked = True Then
            SqlString = SqlString + ",'N' "
        End If
        M_DBConn.ExecuteDataTable(Dt, SqlString, False)
        DgvNonTranPassing.DataSource = Dt
        DgvNonTranPassing.DataBind()

        If PnlStatus.Visible = True Then
            If Dt.Rows.Count = 0 Then
                PnlStatus.Visible = True
            Else
                PnlStatus.Visible = False
            End If
        End If

    End Sub

    Protected Sub BtnAuthorise_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAuthorise.Click

    End Sub

    Protected Sub DgvPassing_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvPassing.SelectedIndexChanged

    End Sub

    Protected Sub DgvPassing_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvPassing.PageIndexChanging
        DgvPassing.PageIndex = e.NewPageIndex
        BtnRefresh_Click(Nothing, EventArgs.Empty)
    End Sub

    Protected Sub BtnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnRefresh.Click
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


    Protected Sub DgvNonTranPassing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvNonTranPassing.RowCommand
        Try


            'Delete Right
            If Obj_UserRights.Delete_Right = False Then
                ShowAlertMessage("You Have No Permission To Delete!!!")
                Exit Sub
            End If




            Dim Tran_No As String
            Dim Auth_Type As String
            Dim M_DBConn As New Connection()
            Dim SqlStr As String

            If e.CommandName.ToUpper = "SELECT" Then
                Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())

                'If DgvPassing.DataKeys(RowIndex)("Authorised").ToString() = False Then
                Tran_No = DgvNonTranPassing.DataKeys(RowIndex)("Tran_No").ToString()
                Auth_Type = DgvNonTranPassing.DataKeys(RowIndex)("Auth_Type").ToString()
                '     SqlStr = "Exec NonTranction_Authorise_Sp '" & Session("DefaultBrCode").ToString() & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString()))
                '    SqlStr = SqlStr & ",'" & Auth_Type & "','" & Tran_No & "','" & Session("UserID") & "'"

                SqlStr = "Exec Transaction_Delete_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
                    ",'" & Session("UserID") & "','" & Session("DefaultBrCode").ToString() & "','','" & Auth_Type & "'"



                M_DBConn.ExecuteScalar(SqlStr, , False)
                Call Refresh()
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub Close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class