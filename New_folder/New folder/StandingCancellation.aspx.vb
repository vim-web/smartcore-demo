Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Public Class StandingCancellation
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public standingtbl As New DataTable("Table")
    Dim FormID As Integer = 1134
    'Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                'Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If

                Session("SearchType") = "StID"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Load_DefaultValues()
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "StID" Then
                    TxtStandingId.Text = Session("SearchResult")
                    Call TxtStandingId_TextChanged(Me.TxtStandingId, System.EventArgs.Empty)
                    Call TxtAccNoFrom_TextChanged(Me.TxtAccNoFrom, System.EventArgs.Empty)
                    Call TxtAccNoTo_TextChanged(Me.TxtAccNoTo, System.EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranchFrom)
            M_CommonBL.Fill_Branch(CmbBranchTo)
            CmbBranchFrom.SelectedValue = Session("DefaultBrCode")
            CmbBranchTo.SelectedValue = Session("DefaultBrCode")

            M_CommonBL.GetSchemes(CmbSchemeFrom, Session("TransDate"), "DEPOSIT", "FD','SB','CA")
            M_CommonBL.GetSchemes(CmbSchemeTo, Session("TransDate"), "DEPOSIT", "RD','SB','CA")


            Call CmbSchemeFrom_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)
            M_CommonBL = Nothing
            SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
            AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)
            Call CmbSchemeTo_SelectedIndexChanged(Me.CmbSchemeTo, System.EventArgs.Empty)
            M_CommonBL = Nothing
            SchemeDt = DirectCast(Session("SchemeTbl"), DataTable)
            AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_SchemeCtrl(ByVal IsFr As Boolean)
        Try
            If IsFr = True Then
                TxtAccNoFrom.Text = Right("000" + CmbBranchFrom.SelectedValue, 3) + Right("000" + CmbSchemeFrom.SelectedValue, 3)
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
        Try
            If IsFr = False Then
                TxtAccNoTo.Text = Right("000" + CmbBranchTo.SelectedValue, 3) + Right("000" + CmbSchemeTo.SelectedValue, 3)
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try

    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtSchCodeFrom_TextChanged(sender As Object, e As EventArgs) Handles TxtSchCodeFrom.TextChanged
        On Error Resume Next
        CmbSchemeFrom.SelectedValue = TxtSchCodeFrom.Text
        TxtSchCodeFrom.Text = CmbSchemeFrom.SelectedValue
        Set_SchemeCtrl(True)
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub CmbSchemeFrom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSchemeFrom.SelectedIndexChanged
        Try
            TxtSchCodeTo.Text = CmbSchemeTo.SelectedValue
            Set_SchemeCtrl(False)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(sender As Object, e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNoFrom.Text = Right("000" + CmbBranchFrom.SelectedValue, 3) + Right("000" + CmbSchemeFrom.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNoFrom_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNoFrom_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNoFrom.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNoFrom.Text
            'Dim DT_Assignedtails As New DataTable
            Dim M_DbConn As New Connection
            'Dim M_Date As New DataTable
            'Dim M_Sql As String

            'M_Sql = "select day(Dm_WithEffDate) from Dep_Master_M_Tbl where Acc_No='" & TxtAccNoFrom.Text & "'"
            'M_DbConn.ExecuteDataTable(M_Date, M_Sql, False)
            'If M_Date.Rows.Count > 0 Then
            '    TxtExecutionDay.Text = M_Date(0)(0).ToString
            'End If
            'Call CmbSchemeFrom_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

            'DT_Assignedtails = M_CommonBL.CheckStanding(CmbBranchFrom.SelectedValue, CmbSchemeFrom.SelectedValue, M_AccNo, Session("TransDate"))



            'If DT_Assignedtails.Rows.Count > 0 Then
            '    ShowAlertMessage("This Account is Already Standing to  " & DT_Assignedtails.Rows(0)(2) & "  A/c No")
            '    TxtAccNoFrom.Focus()
            '    Exit Sub
            'End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranchFrom.SelectedValue, CmbSchemeFrom.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtCustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = TxtCustId.Text
                TxtFromName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                AccIDFrom.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                TxtFromAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()


            Else
                ShowAlertMessage("Invalid A/c No")
                TxtAccNoFrom.Focus()
            End If

            'TxtAccNoFrom.Text = M_AccNo

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtSchCodeTo_TextChanged(sender As Object, e As EventArgs) Handles TxtSchCodeTo.TextChanged
        On Error Resume Next
        CmbSchemeTo.SelectedValue = TxtSchCodeTo.Text
        TxtSchCodeTo.Text = CmbSchemeTo.SelectedValue
        Set_SchemeCtrl(False)
        TxtShortCode2.Text = ""
        TxtShortCode2.Focus()
    End Sub

    Protected Sub CmbSchemeTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSchemeTo.SelectedIndexChanged
        Try
            TxtSchCodeTo.Text = CmbSchemeTo.SelectedValue
            Set_SchemeCtrl(False)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode2_TextChanged(sender As Object, e As EventArgs) Handles TxtShortCode2.TextChanged
        TxtAccNoTo.Text = Right("000" + CmbBranchTo.SelectedValue, 3) + Right("000" + CmbSchemeTo.SelectedValue, 3) + Right("0000000" + TxtShortCode2.Text, 7)
        Call TxtAccNoTo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNoTo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNoTo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNoTo.Text

            'Call CmbSchemeTo_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranchTo.SelectedValue, CmbSchemeTo.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtToCustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = TxtToCustId.Text
                TxtToName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                'TxtMblNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                TxtToAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                AccIdTo.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                'Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                'Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
                'TxtCurBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
                'TxtShadowBal.Text = Format(Val(CStr(Math.Abs(ShadowBal))), "#0.00") & " " & IIf(ShadowBal < 0, "Dr", "Cr")
                'TxtPendingDr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()), "#0.00")
                'TxtPendingCr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()), "#0.00")
                'AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            Else
                ShowAlertMessage("Invalid A/c No")
                TxtAccNoTo.Focus()
            End If

            'TxtAccNoTo.Text = M_AccNo

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtStandingId_TextChanged(sender As Object, e As EventArgs) Handles TxtStandingId.TextChanged
        Dim M_DbConn As New Connection
        Dim M_Dt As New DataTable
        Dim M_Sql As String
        M_Sql = "select A.De_Acc_No, A.De_Br_Code,A.De_Sch_Code,B.Cr_Acc_No,B.Cr_Br_Code,B.Cr_Sch_Code,A.De_Amount,B.Cr_Amount,A.St_FreqType,A.St_Status from Gen_StandingInstruction_M_Tbl A inner join Gen_StandingInstructionCr_M_Tbl B on A.St_ID=B.St_ID where A.St_ID='" & TxtStandingId.Text & "'"
        M_DbConn.ExecuteDataTable(M_Dt, M_Sql, False)
        If M_Dt.Rows.Count > 0 Then
            TxtAccNoFrom.Text = M_Dt.Rows(0)("De_Acc_No")
            CmbBranchFrom.SelectedValue = M_Dt.Rows(0)("De_Br_Code")
            TxtSchCodeFrom.Text = M_Dt.Rows(0)("De_Sch_Code")
            CrAmount.Value = M_Dt.Rows(0)("Cr_Amount")
            DrAmount.Value = M_Dt.Rows(0)("De_Amount")
            FreqType.Value = M_Dt.Rows(0)("St_FreqType")
            TxtAccNoTo.Text = M_Dt.Rows(0)("Cr_Acc_No")
            TxtSchCodeTo.Text = M_Dt.Rows(0)("Cr_Sch_Code")
            CmbBranchTo.SelectedValue = M_Dt.Rows(0)("Cr_Br_Code")
            CmbSchemeFrom.SelectedValue = M_Dt.Rows(0)("De_Sch_Code")
            CmbSchemeTo.SelectedValue = M_Dt.Rows(0)("Cr_Sch_Code")
            CmbStatus.SelectedValue = M_Dt.Rows(0)("St_Status")
        Else
            ShowAlertMessage("Invalid Standing Id")
        End If
        Call TxtAccNoFrom_TextChanged(Me.TxtAccNoFrom, System.EventArgs.Empty)
        Call TxtAccNoTo_TextChanged(Me.TxtAccNoTo, System.EventArgs.Empty)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_Sql As String
            Dim M_DBConn As New Connection
            'Dim M_qry As String
            'Dim M_qry2 As String
            'Dim M_Qry3 As String

            ''If CmbStatus.SelectedIndex = 1 Then
            ''    M_Sql = "Update Gen_StandingInstruction_M_Tbl set St_Status='N'  where Acc_ID='" & AccIDFrom.Value & "'" 
            ''    M_DBConn.ExecuteScalar(M_Sql, 0, False)
            ''End If
            'M_Sql = "Insert Into Gen_StandingInstructionCr_Deleted_M_Tbl(St_ID,Cr_Br_Code,Cr_Sch_Code,Cr_Acc_No,Cr_Amount,Cr_AmtTyp,Cr_Status,Sub_AccNo) values( '" & TxtStandingId.Text & "','" & CmbBranchTo.SelectedValue & "','" & CmbSchemeTo.SelectedValue & "','" & TxtAccNoTo.Text & "','" & CrAmount.Value & "','" & "F" & "','" & "A" & "','" & "1" & "')"
            'M_DBConn.ExecuteScalar(M_Sql, 0, False)
            'M_qry = "Insert into Gen_StandingInstruction_Deleted_M_Tbl(St_ID,De_Br_Code,De_Sch_Code,De_Acc_No,De_Amount,Sub_AccNo,Del_UsrID) values('" & TxtStandingId.Text & "','" & CmbBranchFrom.SelectedValue & "','" & CmbSchemeFrom.SelectedValue & "','" & TxtAccNoFrom.Text & "','" & DrAmount.Value & "','" & "1" & "','" & Session("UserId") & "')"
            'M_DBConn.ExecuteScalar(M_qry, 0, False)
            'M_qry2 = "delete from Gen_StandingInstruction_M_Tbl where St_ID='" & TxtStandingId.Text & "'"
            'M_DBConn.ExecuteScalar(M_qry2, 0, False)
            'M_Qry3 = "delete from Gen_StandingInstructionCr_M_Tbl where St_ID='" & TxtStandingId.Text & "'"
            'M_DBConn.ExecuteScalar(M_Qry3, 0, False)
            M_Sql = "EXEC StandingCancel_Sp '" & Session("UserId") & "','" & TxtStandingId.Text & "','" & CmbBranchFrom.SelectedValue & "','" & CmbBranchTo.SelectedValue & "','" & CmbSchemeFrom.SelectedValue & "','" & CmbSchemeTo.SelectedValue & "','" & TxtAccNoFrom.Text & "','" & TxtAccNoTo.Text & "','" & DrAmount.Value & "','" & CrAmount.Value & "','" & FreqType.Value & "'"
            M_DBConn.ExecuteScalar(M_Sql, 0, False)
            ShowAlertMessage("Standing Cancelled")
            Clear()
        Catch ex As Exception
            ShowAlertMessage("Standing Cancellation Failed")
        End Try
        
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear()
    End Sub
    Private Sub Clear()
        TxtAccNoFrom.Text = ""
        TxtAccNoTo.Text = ""
        TxtCustId.Text = ""
        TxtFromAddress.Text = ""
        TxtFromName.Text = ""
        TxtSchCodeFrom.Text = ""
        TxtSchCodeTo.Text = ""
        TxtShortCode.Text = ""
        TxtShortCode2.Text = ""
        TxtStandingId.Text = ""
        TxtToAddress.Text = ""
        TxtToCustId.Text = ""
        TxtToName.Text = ""
        
    End Sub

    
End Class