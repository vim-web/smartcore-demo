Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class BulkTransactionEntry
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
    Shared Obj_UserRights As Object
    Public BulkTran As New DataTable("Table")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            LoadDefaultValues()
            Set_Grid()
            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("BULKENTRY", Request.QueryString("TRANID"))
            End If
            
        End If
    End Sub
    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("BULKENTRY", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As New DataTable
        M_TranTbl = M_Dataset.Tables(2)
        Session("TransTbl") = M_Dataset.Tables(1)

        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        DgvTransaction.DataSource = M_Dataset.Tables(1)
        DgvTransaction.DataBind()
    End Sub
    Private Sub GetAccountDtls(ByVal Dtbl As DataTable)

        CmbBranch.SelectedValue = Dtbl.Rows(0).Item("Br_Code").ToString()
        Cmbscheme.SelectedValue = Dtbl.Rows(0).Item("Sch_Code").ToString()
        Txtshortcode.Text = Dtbl.Rows(0).Item("Sch_Code").ToString()
        TxtAccno.Text = Dtbl.Rows(0).Item("Acc_No").ToString()
        TxtName.Text = Dtbl.Rows(0).Item("Cust_Name").ToString()
        TxtRemarks.Text = Dtbl.Rows(0).Item("Narration").ToString()
        CmbBranch.Enabled = False
        Cmbscheme.Enabled = False
        TxtAccno.Enabled = False
        CmbTranType.Enabled = False
        CmbCashTransfer.Enabled = False
        TxtRemarks.Enabled = False
        TxtName.Enabled = False
    End Sub

    Private Sub LoadDefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            'M_CommonBL.GetSchemes(Cmbscheme, Session("TransDate"))
            M_CommonBL.GetSchemes(Cmbscheme, Session("TransDate"), "LOAN", "OD")
            CmbCashTransfer.Items.Add("Cash")
            CmbCashTransfer.Items.Add("Transfer")
            CmbTranType.Items.Add("Credit")
            CmbTranType.Items.Add("Debit")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
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
            Dim Dt As New DataTable
            Dim Bulkdt As DataTable = DirectCast(Session("BulkTran"), DataTable)
            Dim M_BulkTransaction As New IO.StringWriter
            Bulkdt.WriteXml(M_BulkTransaction)
            Dim M_CommonBL As New CommonBL
            Session("M_EntryMode") = "NEW"
            Dt = M_CommonBL.SaveBulk(CmbBranch.SelectedValue, Cmbscheme.SelectedValue, Session("TransDate"), Session("UserId"), M_BulkTransaction.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))
            ShowAlertMessage("Saved Successfully")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub Txtshortcode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txtshortcode.TextChanged
        Try
            TxtAccno.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + Cmbscheme.SelectedValue, 3) + Right("0000000" + Txtshortcode.Text, 7)
            Call TxtAccno_TextChanged(Me.Txtshortcode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccno_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccno.TextChanged
        Dim Dt_AcDtls As DataTable
        Dim M_CommonBL As New CommonBL
        If TxtAccno.Text.Trim.Length <= 7 Then
            TxtAccno.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + Cmbscheme.SelectedValue, 3) + Right("0000000" + TxtAccno.Text, 7)
        End If
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, Cmbscheme.SelectedValue, TxtAccno.Text, Session("TransDate"))

        If (Dt_AcDtls.Rows(0)("Acc_Status").ToString() = "C") Then
            ShowAlertMessage("Account is Closed!")
            Exit Sub
        Else
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                HidAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            End If
        End If
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

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdOrnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdOrnAdd.Click
        Try
            Dim AcDtls As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim dt As DataTable = DirectCast(Session("BulkTran"), DataTable)
            Dim Row As DataRow
            Row = dt.NewRow
            Row("AccNo") = TxtAccno.Text
            Row("AccId") = HidAccId.Value
            Row("AccCode") = Cmbscheme.SelectedValue
            Row("Tranno") = 0
            Row("Narration") = TxtRemarks.Text
            If CmbCashTransfer.SelectedIndex = 0 Then
                If CmbTranType.SelectedIndex = 0 Then
                    Row("RctCash") = Val(TxtAmt.Text)
                    Row("RctTransfer") = 0
                    Row("PayCash") = 0
                    Row("PayTransfer") = 0
                Else
                    Row("RctCash") = 0
                    Row("RctTransfer") = 0
                    Row("PayCash") = Val(TxtAmt.Text)
                    Row("PayTransfer") = 0
                End If
            Else
                If CmbTranType.SelectedIndex = 0 Then
                    Row("RctCash") = 0
                    Row("RctTransfer") = Val(TxtAmt.Text)
                    Row("PayCash") = 0
                    Row("PayTransfer") = 0
                Else
                    Row("RctCash") = 0
                    Row("RctTransfer") = 0
                    Row("PayCash") = 0
                    Row("PayTransfer") = Val(TxtAmt.Text)
                End If
            End If

            Try
                dt.Rows.Add(Row)
                Dim Expression As String = "AccId=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try
            AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, Cmbscheme.SelectedValue, TxtAccno.Text, Session("TransDate"))
            If (AcDtls.Rows(0)("Acc_Status").ToString() = "C") Then
                ShowAlertMessage("Account is Closed!")
                Exit Sub
            Else
                DgvTransaction.DataSource = dt
                DgvTransaction.DataBind()
                Session("BulkTran") = dt
                Calc_TranTotal()
                Txtshortcode.Text = ""
                TxtAmt.Text = ""
                TxtAccno.Text = ""
                TxtRemarks.Text = ""

            End If
        Catch ex As Exception

        End Try
        
    End Sub
    Private Sub Set_Grid()
        Try
            DgvTransaction.DataSource = Nothing
            BulkTran.Columns.Add("AccNo", GetType(String))
            BulkTran.Columns.Add("AccId", GetType(String))
            BulkTran.Columns.Add("AccCode", GetType(String))
            BulkTran.Columns.Add("RctCash", GetType(String))
            BulkTran.Columns.Add("RctTransfer", GetType(String))
            BulkTran.Columns.Add("PayCash", GetType(String))
            BulkTran.Columns.Add("PayTransfer", GetType(String))
            BulkTran.Columns.Add("Tranno", GetType(String))
            BulkTran.Columns.Add("Narration", GetType(String))

            BulkTran.Rows.Add(" ", " ", " ", " ", " ", " ", "")
            Session("BulkTran") = BulkTran
            DgvTransaction.DataSource = BulkTran
            DgvTransaction.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
       
           
    End Sub

    Protected Sub DgvTransaction_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvTransaction.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("BulkTran"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvTransaction.DataSource = dt
            DgvTransaction.DataBind()
            Session("BulkTran") = dt
            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Txtshortcode.Text = ""
        TxtAmt.Text = ""
        TxtAccno.Text = ""
        TxtRemarks.Text = ""
        LblPayCashTotal.Text = " "
        LblPayTransferTotal.Text = " "
        LblRctCashTotal.Text = " "
        LblRctTransferTotal.Text = " "
        Set_Grid()
    End Sub

    Private Sub Calc_TranTotal()
        Try
            LblRctCashTotal.Text = "0.00"
            LblRctTransferTotal.Text = "0.00"
            LblPayCashTotal.Text = "0.00"
            LblPayTransferTotal.Text = "0.00"
            Dim j As Double = 0
            Dim s As Double = 0
            Dim s1 As Double = 0
            Dim s2 As Double = 0
            Dim s3 As Double = 0
            For i As Double = j To DgvTransaction.Rows.Count - 1
                s += Val(DgvTransaction.Rows(i).Cells(3).Text)
                s1 += Val(DgvTransaction.Rows(i).Cells(4).Text)
                s2 += Val(DgvTransaction.Rows(i).Cells(5).Text)
                s3 += Val(DgvTransaction.Rows(i).Cells(6).Text)
                j = i + 1
            Next
            LblRctCashTotal.Text = s.ToString()
            LblRctTransferTotal.Text = s1.ToString()
            LblPayCashTotal.Text = s2.ToString()
            LblPayTransferTotal.Text = s3.ToString()
        Catch ex As Exception
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
End Class