Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class Branchtransfer_rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            TxtFromDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
        End If

    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged

    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()

        Dim HeaderArr(11, 4) As String
        Dim HeaderWidth(11, 1) As String
        HeaderArr(0, 0) = "ALLOTED USER"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "15"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "ALLOTED USER TO"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "15"
        HeaderArr(1, 3) = "10"

        HeaderArr(2, 0) = "ALLOTED REASON"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "15"
        HeaderArr(2, 3) = "15"

        HeaderArr(3, 0) = " FROM DATE"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "10"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "TO DATE"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "15"

        HeaderArr(5, 0) = "ALLOTED TIME "
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "15"
        HeaderArr(5, 3) = "15"

        HeaderArr(6, 0) = "FROM BRANCH"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "15"

        HeaderArr(7, 0) = "TO BRANCH"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "15"
        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC EmployeeBranchTransfer_rpt_Sp '" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'SA'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class