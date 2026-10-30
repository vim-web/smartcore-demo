
Imports SMARTCORE_BL
Public Class LoanManualEntry
    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1030
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Load_DefaultValues()
            'TxtFrDate.Text = Session("TransDate")

            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        End If
        If Val(LblTotal.Text) > 0 Then
            GetTotal()
        End If
    End Sub
    Private Function Load_DefaultValues()
        Try
            M_CommonBl.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            M_CommonBl.Fill_SangalDetails(CmbSangam, CmbBranch.SelectedValue)
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), ("DEPOSIT"))

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text

    End Sub
    Private Sub Set_SangamDetails()
        Try
            Dim AccDetailsTbl As New DataTable("Table")

            AccDetailsTbl.Columns.Add("Br_Code", GetType(String))
            AccDetailsTbl.Columns.Add("Sch_Code", GetType(String))
            AccDetailsTbl.Columns.Add("SangamId", GetType(String))
            AccDetailsTbl.Columns.Add("AccId", GetType(String))
            AccDetailsTbl.Columns.Add("Amount", GetType(Double))

            For Each gvRow As GridViewRow In DgvSangamDetails.Rows
                Dim txtAccId As TextBox = DirectCast(gvRow.FindControl("TxtAccID"), TextBox)
                Dim txtAmount As TextBox = DirectCast(gvRow.FindControl("TxtAmount"), TextBox)
                AccDetailsTbl.Rows.Add(CmbBranch.SelectedValue, CmbScheme.SelectedValue, CmbSangam.SelectedValue, txtAccId.Text, txtAmount.Text)
            Next

            Session("AccDetailsTbl") = AccDetailsTbl
        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try
    End Sub

    Private Sub GetTotal()
        Dim Total As Double
        Try

            For Each gvRow As GridViewRow In DgvSangamDetails.Rows

                Dim txtAmount As TextBox = DirectCast(gvRow.FindControl("TxtAmount"), TextBox)
                Total += Val(txtAmount.Text)

            Next
            If (Total > 0) Then
                Divtotal.Visible = True
                LblTotal.Text = Total.ToString()
            Else
                Divtotal.Visible = False
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
        Dim SangamDt As DataTable
        Try
            Dim M_CommonBl As New CommonBL
            SangamDt = M_CommonBl.GetSangamDetails(CmbBranch.SelectedValue, CmbScheme.SelectedValue, CmbSangam.SelectedValue, Session("TransDate"), Val(TxtAmt.Text))
            DgvSangamDetails.DataSource = SangamDt
            DgvSangamDetails.DataBind()
            GetTotal()
            M_CommonBl = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Call Set_SangamDetails()
        Dim M_BatchNo As String = ""
        'If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text
        Dim M_AccDetailsSw As New IO.StringWriter

        Dim AccDetailsDt As DataTable = DirectCast(Session("AccDetailsTbl"), DataTable)
        AccDetailsDt.WriteXml(M_AccDetailsSw)


        Dim M_DepositMasterBL As New DepositMasterBL
        Dim M_Dt As New DataTable



        M_Dt = M_DepositMasterBL.Save_SangamCollectionData(M_AccDetailsSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("DefaultBrCode"), CmbScheme.SelectedValue, Session("TransDate"), Session("UserID"), "C", M_BatchNo)
        ShowAlertMessage(" Saved Successfully ")
        Session("AccDetailsTbl") = Nothing
        M_DepositMasterBL = Nothing

    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
    End Sub

    Protected Sub DgvSangamDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvSangamDetails.SelectedIndexChanged
        ' Call GetTotal()
    End Sub

    Protected Sub DgvSangamDetails_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles DgvSangamDetails.SelectedIndexChanging
        ' Call GetTotal()
    End Sub

    Protected Sub DgvSangamDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvSangamDetails.RowCommand
        '  Call GetTotal()
    End Sub
End Class