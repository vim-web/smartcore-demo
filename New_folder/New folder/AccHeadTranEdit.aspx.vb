
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Public Class AccHeadTranEdit
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared BranchDt As DataTable
    Shared Dt_AcDtls As DataTable
    Shared drParam As DataRow()
    Dim AlertDt As DataTable
    Dim FormID As Integer = 1144
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("UserID") = "" Then
                    Response.Redirect("~/SmartLogin.aspx")
                End If
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Load_DefaultValues()
                Dim TranNo As String = Request.QueryString("TRANID")
                If Len(TranNo) > 0 Then
                    GetAuthoriseDtls(TranNo)
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)

        End Try
    End Sub

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Try
            If True Then
                Dim Expression As String = "Acc_Name LIKE '" & prefixText & "%'"
                Dim SortingOrder As String = "Acc_Name ASC"
                Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression, SortingOrder)
                Dim CompletionSet = New List(Of String)()
                For Each row As DataRow In drFiltered
                    CompletionSet.Add(row("Acc_Name").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("Acc_Code").ToString())
                Next
                Return CompletionSet.ToArray()
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Function
    Private Sub Load_DefaultValues()
        Try
            Session("BatchNo") = ""
            Session("Tran_No") = ""
            Session("Acc_No") = ""
            Dim M_CommonBL As New CommonBL



            AlertDt = New DataTable
            AlertDt.Columns.Add("Type", GetType(String))
            AlertDt.Columns.Add("Msg", GetType(String))
            Session("AlertDt") = AlertDt


            M_CommonBL.Fill_Branch(DrpToBrnch, False, True)

            DrpToBrnch.SelectedValue = Session("DefaultBrCode")

            CmbAcheadTran.Items.Add("Credit")
            CmbAcheadTran.Items.Add("Debit")
            CmbAcheadTran.SelectedIndex = 0

            M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
            CmbBatchNo.SelectedIndex = 0


            AcHeadDt = DirectCast(Cache("Achead"), DataTable)

            'LblRctCashTotal.Text = "0.00"
            'LblRctTransferTotal.Text = "0.00"
            'LblPayCashTotal.Text = "0.00"
            'LblPayTransferTotal.Text = "0.00"
            Session("SearchBranch") = Session("DefaultBrCode")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

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

    Private Sub GetAuthoriseDtls(ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        Dim M_TranTbl As DataTable
        Dim M_SecTbl As DataTable

        M_Dataset = M_CommonBL.GetAuthoriseDtls("TRAN", TransID)
        M_CommonBL = Nothing

        M_TranTbl = M_Dataset.Tables(0)

        If M_TranTbl.Rows(0).Item("SchVou") = "VOU" Then
            DgvTransaction.DataSource = M_Dataset.Tables(1)
            Session("locAcHeadTbl") = M_Dataset.Tables(1)
            DgvTransaction.DataBind()
            TxtRemarks.Text = M_TranTbl.Rows(0).Item("Narration")
            TxtRemarks1.Text = M_TranTbl.Rows(0).Item("Narration1")
            M_CommonBL = Nothing

        Else
            TxtRemarks.Text = M_TranTbl.Rows(0).Item("Narration")
            TxtRemarks1.Text = M_TranTbl.Rows(0).Item("Narration1")

            DgvTransaction.DataSource = M_Dataset.Tables(1)
            Session("locAcHeadTbl") = M_Dataset.Tables(1)
            DgvTransaction.DataBind()
            M_CommonBL = Nothing
            CmdSave.Enabled = True
            CmbBatchNo.Enabled = False
            CmdClear.Enabled = True
            CmdClear0.Enabled = False

        End If
    End Sub

    
    Protected Sub CmdOrnAdd_Click(sender As Object, e As EventArgs) Handles CmdOrnAdd.Click
        Try

            If Len(TxtAccCode.Text) = 0 Then
                ShowAlertMessage("INVALID ACCOUNT CODE")
                TxtAccCode.Focus()
                Exit Sub
            End If
            ''''''''Check Negative balance
            If TxtAcHeadTransAmt.Text.Length > 0 And (CmbAcheadTran.SelectedIndex = 1) Then
                Dim M_Dt As New DataTable
                Dim M_CommBL As New CommonBL
                M_Dt = M_CommBL.Check_Cash_Balance(TxtAcHeadTransAmt.Text, IIf(CmbAcheadTran.SelectedIndex = 0, "C", "D"), Session("TransDate"), Session("DefaultBrCode"))
                If M_Dt.Rows.Count > 0 Then
                    If M_Dt.Rows(0).Item("Status") = "Y" Then
                        ShowAlertMessage("Cash Balance Is Not Enough")
                        Exit Sub
                    End If
                End If
            End If
            ''''''''Check Negative balance
            Dim dt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim Row As DataRow
            Row = dt.NewRow
            Row("AccCode") = TxtAccCode.Text
            Row("AccHead") = TxtAccName.Text.Replace(".", "")
            If CmbAcheadTran.SelectedIndex = 0 Then
                Row("RctCash") = Val(TxtAcHeadTransAmt.Text)
                Row("RctTransfer") = Val(TxtTransfer.Text)
                Row("PayCash") = 0
                Row("PayTransfer") = 0

            Else
                Row("RctCash") = 0
                Row("RctTransfer") = 0
                Row("PayCash") = Val(TxtAcHeadTransAmt.Text)
                Row("PayTransfer") = Val(TxtTransfer.Text)

            End If
            Row("Seq_No") = dt.Rows.Count
            Try
                dt.Rows.Add(Row)
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try

            Try
                Dim Expression As String = "AccCode=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try
            DgvTransaction.DataSource = dt
            DgvTransaction.DataBind()
            Session("locAcHeadTbl") = dt
            ClearAcHead()
            TxtRemarks.Focus()
            Dim M_CommonBL As New CommonBL
            AcHeadDt = M_CommonBL.Fill_AcHead()
            M_CommonBL = Nothing
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub

    Private Sub ClearAcHead()
        Try
            TxtAccCode.Text = ""
            TxtAccName.Text = ""
            TxtAcHeadTransAmt.Text = ""
            TxtTransfer.Text = ""
            LblWords.Text = ""
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub
    Private Sub SaveData(ByRef Tran_No As String)
        Try
            Dim RenewTbl As New DataTable("Table1")
            Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable
            Dim M_BatchNo As String = ""



            RenewTbl.Columns.Add("AccCode", GetType(String))
            RenewTbl.Columns.Add("AccHead", GetType(String))
            RenewTbl.Columns.Add("RctCash", GetType(String))
            RenewTbl.Columns.Add("RctTransfer", GetType(String))
            RenewTbl.Columns.Add("PayCash", GetType(String))
            RenewTbl.Columns.Add("PayTransfer", GetType(String))
            RenewTbl.Columns.Add("Seq_No", GetType(String))
            Dim Dt As DataRow


            For Each row As GridViewRow In DgvTransaction.Rows
                Dt = RenewTbl.NewRow
                Dim TxtAccCodeVal As String = _
                CType(row.FindControl("TxtAccCodeNew"), TextBox).Text
                Dt("AccCode") = TxtAccCodeVal
                Dim TxtAccHeadVal As String = _
                CType(row.FindControl("TxtAccNameNew"), TextBox).Text
                Dt("AccHead") = Val(TxtAccHeadVal)
                Dim TxtRctCashVal As String = _
                CType(row.FindControl("txtRctCash"), TextBox).Text
                Dt("RctCash") = Val(TxtRctCashVal)
                Dim TxtRctTransfer As String = _
                CType(row.FindControl("txtRctTransfer"), TextBox).Text
                Dt("RctTransfer") = Val(TxtRctTransfer)
                Dim TxtPayCashVal As String = _
                CType(row.FindControl("txtPayCash"), TextBox).Text
                Dt("PayCash") = Val(TxtPayCashVal)
                Dim TxtPayTransferVal As String = _
                CType(row.FindControl("txtPayTransfer"), TextBox).Text
                Dt("PayTransfer") = Val(TxtPayTransferVal)
                Dim TxtSeq_NoVal As String = _
                CType(row.FindControl("txtSeq_No"), TextBox).Text
                Dt("Seq_No") = Val(TxtSeq_NoVal)
                RenewTbl.Rows.Add(Dt)
            Next


            RenewTbl.WriteXml(M_TranSw)

            M_Dt = M_CommonBL.Edit_AccHeadTran(DrpToBrnch.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), M_TranSw.ToString, TxtRemarks1.Text, , M_BatchNo, Tran_No)
            If M_Dt.Rows.Count > 0 Then
                Tran_No = M_Dt.Rows(0)("Tran_No").ToString()
                ShowAlertMessage("UPDATED")
            Else
                ShowAlertMessage("FAILED")
            End If

            'Print_Voucher(Tran_No)
            Session("BatchNo") = M_BatchNo
            M_CommonBL = Nothing
            Clear_Text()
            Response.Redirect("~/ManagerPassingEdit.aspx")
            'Clear_Controls()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            Dim M_CommonBL As New CommonBL
            If Len(Request.QueryString("TRANID")) > 0 Then
                SaveData(Request.QueryString("TRANID"))
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
            LoggError(ex)
        End Try

    End Sub


    Protected Sub DgvTransaction_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvTransaction.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'DgvTransaction.Columns(0).ItemStyle.Width = 20
                'DgvTransaction.Columns(1).ItemStyle.Width = 200       


            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub DgvTransaction_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DgvTransaction.RowEditing

    End Sub

    Protected Sub DgvTransaction_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles DgvTransaction.RowUpdating

    End Sub


    Private Sub Clear_Text()
        Try
            TxtAccCode.Text = ""
            TxtAccName.Text = ""
            TxtAcHeadTransAmt.Text = ""
            TxtTransfer.Text = ""
            TxtRemarks.Text = ""
            TxtTockenNo.Text = ""
            DgvTransaction.DataSource = Nothing
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub

    Protected Sub CmdClear0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            Clear_Text()
        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub

    Protected Sub DgvTransaction_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvTransaction.RowDeleting

    End Sub
    Private Sub DeleteRow(ByVal Code As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim view As DataView = New DataView(dt)
            view.RowFilter = "Seq_No = '" + Code + "'"
            For Each row As DataRowView In view
                row.Delete()
            Next
            GoTo otherow

otherow:
            dt = ReverseRowsInDataTable(view.Table)
            DgvTransaction.DataSource = view.Table
            DgvTransaction.DataBind()
            Session("locAcHeadTbl") = view.Table
            If dt.Rows.Count <= 0 Then DgvTransaction.DataSource = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccCode_TextChanged(sender As Object, e As EventArgs) Handles TxtAccCode.TextChanged

        Try
            Dim Expression As String = "Acc_Code='" & TxtAccCode.Text & "'"
            Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression)

            If drFiltered.Count > 0 Then
                TxtAccName.Text = drFiltered(0)("Acc_Name")
            Else
                TxtAccName.Text = ""
                ShowAlertMessage("Invalid A/c Head")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub TxtAcHeadTransAmt_TextChanged(sender As Object, e As EventArgs) Handles TxtAcHeadTransAmt.TextChanged
        Dim M_CommonCls As New CommonClass
        LblWords.Text = M_CommonCls.Towords(Val(TxtAcHeadTransAmt.Text) + Val(TxtTransfer.Text)).ToUpper
        M_CommonCls = Nothing
    End Sub

    Protected Sub DgvTransaction_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvTransaction.RowCommand
        Try
            If e.CommandName = "Delete" Then


                Dim Code As String = e.CommandArgument.ToString()
                DeleteRow(Code)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub TxtAccCodeNew_TextChanged(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In DgvTransaction.Rows
                Dim TxtAccCodeVal As String = _
                 CType(row.FindControl("TxtAccCodeNew"), TextBox).Text

                Dim TxtAccHeadVal As String = _
                 CType(row.FindControl("TxtAccNameNew"), TextBox).Text
                Dim Expression As String = "Acc_Code='" & TxtAccCodeVal & "'"
                Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression)
                If drFiltered.Count > 0 Then
                     _
                    CType(row.FindControl("TxtAccNameNew"), TextBox).Text = drFiltered(0)("Acc_Name")
                Else
                     _
                    CType(row.FindControl("TxtAccNameNew"), TextBox).Text = ""
                    ShowAlertMessage("Invalid A/c Head")
                End If
            Next
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Function ReverseRowsInDataTable(ByVal inputTable As DataTable)

        Dim outputTable As DataTable = inputTable.Clone()
        Dim i As Int16
        For i = inputTable.Rows.Count - 1 To 0
            outputTable.ImportRow(inputTable.Rows(i))
        Next
        Return outputTable
    End Function

End Class