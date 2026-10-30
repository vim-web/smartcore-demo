Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class AttendenanceRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            Load_DefaultValues()
        End If

    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Pickup(CmbLeaveType, 65, 0)
            CmbBranch.Items.Add("ALL")
            CmbLeaveType.Items.Insert("0", "All")
            M_CommonBL = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()

        Dim HeaderArr(2, 3) As String
        Dim HeaderWidth(3, 1) As String
        HeaderArr(0, 0) = "EMP ID"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "EMP NAME"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "10"
        HeaderArr(1, 3) = "10"

        HeaderArr(2, 0) = "STATUS"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "10"

      
        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC AttendenanceRpt_Sp '" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & CmbLeaveType.SelectedValue & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=60", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

End Class