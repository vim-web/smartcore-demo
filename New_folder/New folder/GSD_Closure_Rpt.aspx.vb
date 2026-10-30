Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class GSD_Closure_Rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_Branch(CmbBranch)
            TxtFrDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
        End If
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()

        Dim HeaderArr(4, 4) As String
        Dim HeaderWidth(4, 1) As String
        HeaderArr(0, 0) = "ACC NO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "20"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "OPEN DATE"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "20"
        HeaderArr(1, 3) = "40"

        HeaderArr(2, 0) = "CLOSED DATE"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "20"
        HeaderArr(2, 3) = "13"


        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC GSD_Closure_Rpt_Sp'" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFrDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Exit Sub
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class