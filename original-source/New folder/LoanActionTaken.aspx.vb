Imports SMARTCORE_BL

Public Class LoanActionTaken
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1081
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


                Load_DefaultValues()
                Set_Grid()
                Set_ActGrid()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
        ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

        Session("SearchType") = "ACCOUNTNO"
        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "ACCOUNTNO" Then
                TxtAccNo.Text = Session("SearchResult")
                TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN")
            M_CommonBL.Fill_Pickup(CmbActionType, 64, 0)

            TxtDate.Enabled = False
            TxtRegnDate.Text = Session("TransDate")
            TxtResDate.Text = Session("TransDate")
            'TxtRegnDate.Attributes.Add("readonly", "readonly")
            TxtResDate.Attributes.Add("readonly", "readonly")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Set_Grid()
        Try
            Dim LoanTbl As New DataTable("Table")
            DgvGl.DataSource = Nothing
            LoanTbl.Columns.Add("Acc_Code", GetType(String))
            LoanTbl.Columns.Add("GlHead", GetType(String))
            LoanTbl.Columns.Add("ChargeAmount", GetType(Double))
            LoanTbl.Rows.Add("", "", 0)
            Session("LoanTbl") = LoanTbl
            DgvGl.DataSource = LoanTbl
            DgvGl.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub ClearControls()
        Try
            TxtRegnDate.Text = Session("TransDate")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
            TxtAccNo.Text = ""
            TxtShortCode.Text = ""
            TxtCid.Text = ""
            TxtCustName.Text = ""
            TxtAddress.Text = ""
            TxtSettlementAmt.Text = "0.00"
            TxtBalance.Text = ""
            TxtRemarks.Text = ""
            TxtChargeAmt0.Text = ""
            Set_ActGrid()
            Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            TxtBranch.Text = CmbBranch.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Dim M_CommonBL As New CommonBL
            M_CommonBL.LocateSchTranHeads(CmbGLHead, CmbScheme.SelectedValue, , 3)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   
    Protected Sub TxtShortCode_TextChanged(sender As Object, e As EventArgs) Handles TxtShortCode.TextChanged
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable()
            Dim M_AccNo As String = TxtAccNo.Text
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            Dim Dt_AcDtls As DataTable
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                If (Dt_AcDtls.Rows(0)("Acc_Status").ToString() = "C") Then
                    ClearControls()
                    ShowAlertMessage("Account is Closed!")
                    Exit Sub

                Else
                    HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                    TxtCid.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                    TxtCustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                    TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                    CmbScheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                    TxtScheme.Text = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                    M_CommonBL.LocateSchTranHeads(CmbGLHead, CmbScheme.SelectedValue, , 3)
                End If
            End If
            Call Fill_ActionTakenDtls()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Set_ActGrid()
        Try
            Dim ActionTbl As New DataTable
            ActionTbl.Columns.Add("Pkc_Description", GetType(String))
            ActionTbl.Columns.Add("Reg_Date", GetType(String))
            ActionTbl.Columns.Add("GLHead", GetType(String))
            ActionTbl.Columns.Add("ChargeAmount", GetType(Double))
            ActionTbl.Rows.Add("", "")
            DgvJointAc.DataSource = ActionTbl
            DgvJointAc.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_LoanAction As New SMARTCORE_BO.LoanActionBO
            With M_LoanAction
                '.TranID = M_Dt.Rows(0).Item("Tran_ID")
                .AccID = Val(HdnAccId.Value)
                .ActionType = CmbActionType.SelectedValue
                .SlNo = 1
                .RegDate = TxtRegnDate.Text
                .TrDate = Session("TransDate")
                If ChkStopInterest.Checked = True Then
                    .StopIntDate = TxtDate.Text
                Else
                    .StopIntDate = "01/01/1900"
                End If

                .RevokeDate = "01/01/1900"
                .SettlmentAmt = Val(TxtSettlementAmt.Text)
                .TranNo = ""
                .AccNo = TxtAccNo.Text
                .SchCode = Val(TxtScheme.Text)
                .BrCode = Val(TxtBranch.Text)
                .RegNo = TxtRegNo.Text
                .GLCode = CmbGLHead.SelectedIndex
                .MakerID = Session("Login")
                .CheckerID = Session("Login")
                .RevokeUser = ""
                .Remarks = TxtRemarks.Text
                .Status = "A"
                .Authorised = "Y"
                .ResNo = TxtResNo.Text
                .ResDate = IIf(TxtResDate.Text.Length = 0, "01/01/1900", TxtResDate.Text)
            End With

            Dim LoanTblDt As DataTable = DirectCast(Session("LoanTbl"), DataTable)
            Dim M_LoanActionsw As New IO.StringWriter
            LoanTblDt.WriteXml(M_LoanActionsw)

            Dim M_LoanActionBl As New SMARTCORE_BL.LoanActionBL
            Session("M_EntryMode") = "NEW"
            If M_LoanActionBl.Save_Data(M_LoanAction, M_LoanActionsw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode")) > 0 Then
                ShowAlertMessage("Loan Action Saving Completed")
                ClearControls()
                TxtShortCode.Focus()
            Else
                ShowAlertMessage("Loan Action Saving Failed")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ChkStopInterest_CheckedChanged(sender As Object, e As EventArgs) Handles ChkStopInterest.CheckedChanged
        Try
            If ChkStopInterest.Checked = True Then
                TxtDate.Enabled = True
            Else
                TxtDate.Enabled = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ImgAdd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAdd.Click
        Try
            Dim dt As DataTable = DirectCast(Session("LoanTbl"), DataTable)
            Dim row As DataRow
            row = dt.NewRow
            row("Acc_Code") = CmbGLHead.SelectedValue
            row("GlHead") = CmbGLHead.SelectedItem.Text
            row("ChargeAmount") = TxtChargeAmt0.Text
            Try
                dt.Rows.Add(row)

            Catch ex As Exception

            End Try

            Try
                Dim Expression As String = "Acc_Code=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
            End Try
            DgvGl.DataSource = dt
            DgvGl.DataBind()
            Session("LoanTbl") = dt
            TxtChargeAmt0.Text = "0.00"
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAddress_TextChanged(sender As Object, e As EventArgs) Handles TxtAddress.TextChanged

    End Sub

    Protected Sub TxtBranch_TextChanged(sender As Object, e As EventArgs) Handles TxtBranch.TextChanged
        Try
            CmbBranch.SelectedValue = TxtBranch.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub TxtScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtScheme.TextChanged
        Try
            CmbScheme.SelectedValue = TxtScheme.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Fill_ActionTakenDtls()
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt As New DataTable
            Dt = M_CommonBL.Get_LoanAction(Val(HdnAccId.Value))
            DgvJointAc.DataSource = Dt
            DgvJointAc.DataBind()
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub DgvJointAc_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvJointAc.PageIndexChanging
        Try
            DgvJointAc.PageIndex = e.NewPageIndex
            Call Fill_ActionTakenDtls()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class