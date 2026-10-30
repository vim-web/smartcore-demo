Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class DailyLoanReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                
                CmbType.Items.Add("OPEN")
                CmbType.Items.Add("RECEIPT")
                DemandDate.Text = Session("TransDate")
                
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim M_Sql As String


            If CmbType.SelectedIndex = 0 Then
                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "Br_Code"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "13"
                HeaderArr(0, 3) = "13"

                HeaderArr(1, 0) = "Branch"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Section"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Loan Amount"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "Tran_Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                Session("RptHeader") = HeaderArr
                
            Else
                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "Br_Code"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "13"
                HeaderArr(0, 3) = "13"

                HeaderArr(1, 0) = "Branch"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Section"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Principal"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "Interest"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Tran_Date"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "13"
                HeaderArr(5, 3) = "13"

                Session("RptHeader") = HeaderArr

            End If
            M_Sql = "Exec Get_DailyLoanDetails_Sp'" & Format(CDate(DemandDate.Text), "MM/dd/yyyy") & "','" & CmbType.SelectedIndex & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
End Class