Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Public Class Standing
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Public standingtbl As New DataTable("Table")
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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


                Set_Grid()
                Load_DefaultValues()
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

            M_CommonBL.GetSchemes(CmbSchemeFrom, Session("TransDate"), "DEPOSIT", "FD','SB','DD','CA")
            M_CommonBL.GetSchemes(CmbSchemeTo, Session("TransDate"), "GROUP DEPOSIT','LOAN','DEPOSIT", "RD','SB','OD','GOLDLOAN','GEN.LOAN','DEP.LOAN','LF','LS','CA','GROUP DEPOSIT")
            M_CommonBL.Fill_Pickup(CmbFrequency, 44, 0)

            CmbAmountType.Items.Add("Balance")
            CmbAmountType.Items.Add("Fixed")

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
    Private Sub Set_Grid()
        Try

            DgvStanding.DataSource = Nothing
            standingtbl.Columns.Add("ToBranch", GetType(String))
            standingtbl.Columns.Add("ToScheme", GetType(String))
            standingtbl.Columns.Add("ToAccNo", GetType(String))
            standingtbl.Columns.Add("ToAccID", GetType(Double))
            standingtbl.Columns.Add("ToAmtType", GetType(String))
            standingtbl.Columns.Add("ToAmount", GetType(Double))

            standingtbl.Rows.Add("", "", "", 0, "", 0)
            Session("standingtbl") = standingtbl
            DgvStanding.DataSource = standingtbl
            DgvStanding.DataBind()
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
    Private Function Validate_Control() As Boolean
        Try
            Dim I As Integer
            Dim J As Integer
            For I = 0 To DgvStanding.Rows.Count - 1

                If DgvStanding.Rows(I).Cells(4).Text = "Balance" Then
                    J = J + 1
                End If

            Next

            If J > 0 Then
                ShowAlertMessage("Already Entered")
                Return False

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
        Return True

    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmbSchemeTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchemeTo.SelectedIndexChanged
        Try
            TxtSchCodeTo.Text = CmbSchemeTo.SelectedValue
            Set_SchemeCtrl(False)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub CmbBranchFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranchFrom.SelectedIndexChanged
        TxtAccNoFrom.Text = Right("000" + CmbBranchFrom.SelectedValue, 3) + Right("000" + CmbSchemeFrom.SelectedValue, 3)
    End Sub

    Protected Sub TxtSchCodeFrom_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCodeFrom.TextChanged
        On Error Resume Next
        CmbSchemeFrom.SelectedValue = TxtSchCodeFrom.Text
        TxtSchCodeFrom.Text = CmbSchemeFrom.SelectedValue
        Set_SchemeCtrl(True)
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Protected Sub CmbSchemeFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchemeFrom.SelectedIndexChanged
        Try
            TxtSchCodeFrom.Text = CmbSchemeFrom.SelectedValue
            Set_SchemeCtrl(True)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNoFrom.Text = Right("000" + CmbBranchFrom.SelectedValue, 3) + Right("000" + CmbSchemeFrom.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNoFrom_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtAccNoFrom_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNoFrom.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNoFrom.Text
            Dim DT_Assignedtails As New DataTable
            Dim M_DbConn As New Connection
            Dim M_Date As New DataTable
            Dim M_Sql As String

            M_Sql = "select day(Dm_WithEffDate) from Dep_Master_M_Tbl where Acc_No='" & Trim(TxtAccNoFrom.Text) & "'"
            M_DbConn.ExecuteDataTable(M_Date, M_Sql, False)
            If M_Date.Rows.Count > 0 Then
                TxtExecutionDay.Text = M_Date(0)(0).ToString
            End If

            'Call CmbSchemeFrom_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

       

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranchFrom.SelectedValue, CmbSchemeFrom.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                CmbSchemeFrom.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                TxtSchCodeFrom.Text = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                DT_Assignedtails = M_CommonBL.CheckStanding(CmbBranchFrom.SelectedValue, CmbSchemeFrom.SelectedValue, M_AccNo, Session("TransDate"))
                If DT_Assignedtails.Rows.Count > 0 Then
                    ShowAlertMessage("This Account is Already Standing to  " & DT_Assignedtails.Rows(0)(2) & "  A/c No")
                    TxtAccNoFrom.Focus()
                    Exit Sub
                Else
                    TxtCustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                    Session("CustomerId") = TxtCustId.Text
                    TxtFromName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                    AccIDFrom.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                    TxtFromAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                End If
            Else
                AccIDFrom.Value = ""
                ShowAlertMessage("Invalid A/c No")
                TxtAccNoFrom.Focus()

            End If
            'TxtAccNoFrom.Text = M_AccNo

            M_CommonBL = Nothing
            M_DbConn = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    
    Protected Sub TxtSchCodeTo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCodeTo.TextChanged
        On Error Resume Next
        CmbSchemeTo.SelectedValue = TxtSchCodeTo.Text
        TxtSchCodeTo.Text = CmbSchemeTo.SelectedValue
        Set_SchemeCtrl(False)
        TxtShortCode2.Text = ""
        TxtShortCode2.Focus()
    End Sub

    Protected Sub TxtShortCode2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode2.TextChanged
        TxtAccNoTo.Text = Right("000" + CmbBranchTo.SelectedValue, 3) + Right("000" + CmbSchemeTo.SelectedValue, 3) + Right("0000000" + TxtShortCode2.Text, 7)
        Call TxtAccNoTo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)

    End Sub




    Protected Sub TxtAccNoTo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNoTo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNoTo.Text

            'Call CmbSchemeTo_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranchTo.SelectedValue, CmbSchemeTo.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                CmbBranchTo.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()
                CmbSchemeTo.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                TxtSchCodeTo.Text = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
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

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try


            'If DgvStanding.Rows.Count = 0 Then
            '    ShowAlertMessage("Enter Credit Account Details")
            '    Exit Sub
            'End If


            Dim M_StandingInstruction As New SMARTCORE_BO.StandingInstructionBO
            With M_StandingInstruction

                .StID = Val(TxtStandingId.Text)
                .AccID = AccIDFrom.Value
                .StFreqType = CmbFrequency.SelectedValue
                .StExecutionDay = Val(TxtExecutionDay.Text)
                .StRegDate = Session("TransDate")
                .DeAmount = Val(TxtAmount.Text)
                .DeBrCode = CmbBranchFrom.SelectedValue
                .DeSchCode = Val(TxtSchCodeFrom.Text)
                .DeAccNo = TxtAccNoFrom.Text
                .StApplicationNo = TxtApplnNo.Text
                .StStatus = CmbStatus.SelectedValue
                .ExecutionAmtType = CmbAmountType.SelectedValue

            End With

            Dim StandingtblDt As DataTable = DirectCast(Session("Standingtbl"), DataTable)
            Dim M_Standingsw As New IO.StringWriter
            StandingtblDt.WriteXml(M_Standingsw)

            Dim M_StandingInstructionBL As New StandingInstructionBL
            Dim M_StID As Integer

            Session("M_EntryMode") = "NEW"
            M_StID = M_StandingInstructionBL.Save_Data(M_StandingInstruction, M_Standingsw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))

            If (M_StID > 0) Then
                ShowAlertMessage("STANDING INSTRUCTION SAVING COMPLETED")
                Clear()
            Else
                ShowAlertMessage("STANDING INSTRUCTION SAVING FAILED")
            End If
            Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ImgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgAdd.Click
        Try
            If Validate_Control() = False Then
                Exit Sub

            End If

            Dim dt As DataTable = DirectCast(Session("standingtbl"), DataTable)
            Dim Row As DataRow

            Row = dt.NewRow
            Row("ToBranch") = CmbBranchTo.SelectedItem
            Row("ToScheme") = CmbSchemeTo.SelectedItem
            Row("ToAccNo") = TxtAccNoTo.Text
            Row("ToAccID") = Val(AccIdTo.Value)
            Row("ToAmtType") = CmbAmountType.SelectedItem.Text
            Row("ToAmount") = Val(TxtAmount.Text)

            Try
                dt.Rows.Add(Row)

            Catch ex As Exception

            End Try
            Try
                Dim Expression As String = "ToBranch=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try

            DgvStanding.DataSource = dt
            DgvStanding.DataBind()
            Session("standingtbl") = dt
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear()
    End Sub


    Private Sub Clear()
        Session("standingtbl") = Nothing
        TxtAccNoFrom.Text = ""
        TxtAccNoTo.Text = ""
        TxtCustId.Text = ""
        TxtExecutionDay.Text = ""
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
        DgvStanding.DataSource = ""
        DgvStanding.DataBind()
        AccIDFrom.Value = ""
    End Sub
End Class