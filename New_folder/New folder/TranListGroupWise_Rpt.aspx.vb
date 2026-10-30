Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class TranListGroupWise_Rpt
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL


                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch, True)
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                M_CommonBL.Fill_AccountGroup(CmbGrp)
                DrpTran.Items.Add("Credit")
                DrpTran.Items.Add("Debit")
                DrpTran.Items.Add("Both")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                CmbGrp.Items.Add("ALL")


            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
          
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim TranType As String
            Dim Branch As String

            If DdlBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DdlBranch.SelectedValue
            End If

            If DrpTran.SelectedValue = "Credit" Then
                TranType = "C"
            ElseIf DrpTran.SelectedValue = "Debit" Then
                TranType = "D"
            Else
                TranType = "B"
            End If


            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String



            HeaderArr(0, 0) = "Sl No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "6"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "Branch"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "25"

            HeaderArr(2, 0) = "Tran Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "Head Name"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "25"
            HeaderArr(3, 3) = "25"

            HeaderArr(4, 0) = "Narration"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "35"
            HeaderArr(4, 3) = "35"

            HeaderArr(5, 0) = "Credit"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "Debit"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            Dim DFr As Date
            DFr = DtFrom.Text

            Dim DTo As Date
            DTo = DtTo.Text

            Session("RptHeader") = HeaderArr




            M_Sql = "Exec Get_TranListGrpWise_Sp '" & Branch & "','" & CmbGrp.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & TranType & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class