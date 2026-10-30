Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class BatchTran
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadBatch()
        End If
    End Sub

    Private Sub LoadBatch()
        Dim M_CommonBL As New CommonBL
        Dim M_Ds As DataSet
        Dim CrBatchNo As String = ""
        Dim DrBatchNo As String = ""

        If OptBatched.Checked = True Then
            M_Ds = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), "", "BC", "M", Val(TxtAmtFr.Text), Val(TxtAmtTo.Text), CrBatchNo, DrBatchNo)
        ElseIf OptUnBatched.Checked = True Then
            M_Ds = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), "", "BC", "U", Val(TxtAmtFr.Text), Val(TxtAmtTo.Text), CrBatchNo, DrBatchNo)
        Else
            M_Ds = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), "", "BC", "A", Val(TxtAmtFr.Text), Val(TxtAmtTo.Text), CrBatchNo, DrBatchNo)
        End If

        CmbBatchDr.Items.Clear()
        CmbBatchDr.SelectedIndex = -1
        CmbBatchDr.DataTextField = "Batch_No"
        CmbBatchDr.DataValueField = "Batch_No"
        CmbBatchDr.DataSource = M_Ds.Tables(0)
        CmbBatchDr.DataBind()
        CmbBatchDr.Items.Add("Select")
        CmbBatchDr.SelectedIndex = CmbBatchDr.Items.Count - 1

        CmbBatchDr_SelectedIndexChanged(Me.CmbBatchDr, System.EventArgs.Empty)

        'For i As Int16 = 0 To CmbBatchDr.Items.Count - 2
        '    For j As Int16 = CmbBatchDr.Items.Count - 1 To i + 1 Step -1
        '        If CmbBatchDr.Items(i).ToString = CmbBatchDr.Items(j).ToString Then
        '            CmbBatchDr.Items.RemoveAt(j)
        '        End If
        '    Next
        'Next

        CmbBatchCr.Items.Clear()
        CmbBatchCr.SelectedIndex = -1
        CmbBatchCr.DataTextField = "Batch_No"
        CmbBatchCr.DataValueField = "Batch_No"
        CmbBatchCr.DataSource = M_Ds.Tables(0)
        CmbBatchCr.DataBind()
        CmbBatchCr.Items.Add("Select")
        CmbBatchCr.SelectedIndex = CmbBatchCr.Items.Count - 1

        CmbBatchCr_SelectedIndexChanged(Me.CmbBatchCr, System.EventArgs.Empty)

        M_CommonBL = Nothing
    End Sub

    Protected Sub OptBatched_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles OptBatched.CheckedChanged
        If CmbBatchCr.Items.Count > 0 Then CmbBatchCr.SelectedIndex = CmbBatchCr.Items.Count - 1
        If CmbBatchDr.Items.Count > 0 Then CmbBatchDr.SelectedIndex = CmbBatchDr.Items.Count - 1
        LoadBatch()
    End Sub


    Protected Sub TxtAmtFrTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAmtFr.TextChanged
        LoadBatch()
    End Sub

    Protected Sub TxtAmtToTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAmtTo.TextChanged
        LoadBatch()
    End Sub

    Protected Sub OptAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles OptAll.CheckedChanged
        If CmbBatchCr.Items.Count > 0 Then CmbBatchCr.SelectedIndex = CmbBatchCr.Items.Count - 1
        If CmbBatchDr.Items.Count > 0 Then CmbBatchDr.SelectedIndex = CmbBatchDr.Items.Count - 1
        LoadBatch()
    End Sub

    Protected Sub OptUnBatched_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles OptUnBatched.CheckedChanged
        If CmbBatchCr.Items.Count > 0 Then CmbBatchCr.SelectedIndex = CmbBatchCr.Items.Count - 1
        If CmbBatchDr.Items.Count > 0 Then CmbBatchDr.SelectedIndex = CmbBatchDr.Items.Count - 1
        LoadBatch()
    End Sub

    Protected Sub CmbBatchDr_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBatchDr.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As DataTable = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), CmbBatchDr.SelectedValue, "TR", "M")
        DgvBatchFrom.DataSource = M_Dt
        DgvBatchFrom.DataBind()

        Dim objSum As Object
        If M_Dt.Rows.Count > 0 Then
            objSum = Val(M_Dt.Compute("Sum(DrAmt)", "")) - Val(M_Dt.Compute("Sum(CrAmt)", ""))
            LblDiffFr.Text = Format(Val(objSum.ToString()) / 2, "#0.00")

            If Val(LblDiffFr.Text) > 0 Then
                LblDiffFr.Text = "Difference : " & LblDiffFr.Text + " Cr"
            Else
                LblDiffFr.Text = "Difference : " & Format(Math.Abs(Val(LblDiffFr.Text)), "#0.00") + " Cr"
            End If
        End If

        M_CommonBL = Nothing
    End Sub

    Protected Sub CmbBatchCr_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBatchCr.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As DataTable = M_CommonBL.Get_BatchDetails(Session("DefaultBrCode"), Session("TransDate"), CmbBatchCr.SelectedValue, "TR", "M")
        DgvBatchTo.DataSource = M_Dt
        DgvBatchTo.DataBind()

        Dim objSum As Object
        If M_Dt.Rows.Count > 0 Then
            objSum = Val(M_Dt.Compute("Sum(DrAmt)", "")) - Val(M_Dt.Compute("Sum(CrAmt)", ""))
            LblDiffTo.Text = Format(Val(objSum.ToString()) / 2, "#0.00")

            If Val(LblDiffFr.Text) > 0 Then
                LblDiffTo.Text = "Difference : " & LblDiffTo.Text + " Cr"
            Else
                LblDiffTo.Text = "Difference : " & Format(Math.Abs(Val(LblDiffTo.Text)), "#0.00") + " Dr"
            End If
        End If

        M_CommonBL = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_CommonBL As New CommonBL

        If M_CommonBL.Batch_Save(Session("DefaultBrCode"), Session("TransDate"), CmbBatchDr.SelectedValue, CmbBatchCr.SelectedValue) = True Then
            ShowAlertMessage("UPDATED")
            OptBatched_CheckedChanged(Me.OptBatched, System.EventArgs.Empty)
        Else
            ShowAlertMessage("FAILED")
        End If

        M_CommonBL = Nothing
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

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        OptBatched_CheckedChanged(Me.OptBatched, System.EventArgs.Empty)
    End Sub
End Class