Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class GetEMI_Chart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                Dim M_Comm As New CommonBL
             
                DdlType.Items.Add("FLAT")
                DdlType.Items.Add("DIMINISHING")
                DtFrom.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim CommBL As New CommonBL
            Dim M_Dt As New DataTable

            ' M_Dt = CommBL.Get_EMI_Chart(TxtLoanAmnt.Text, TxtIntRate.Text, TxtPeriod.Text, DtFrom.Text, DdlType.SelectedItem.Text)
            CommBL = Nothing


            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String

            HeaderArr(0, 0) = "Inst No"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "Inst Date"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "12"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "Inst Amount"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "Inst Principal"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "Inst Interest"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Principal Balance"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "10"

            Dim Dt As Date
            Dt = DtFrom.Text
            Session("RptHeader") = HeaderArr

            M_Sql = " Exec Get_EmiChart_Sp  '" & TxtLoanAmnt.Text & "','" & TxtIntRate.Text & "','" & TxtPeriod.Text & "','" & Format(Dt, "MM/dd/yyyy") & "','" & DdlType.SelectedItem.Text & "'"

            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=80", False)


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

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class