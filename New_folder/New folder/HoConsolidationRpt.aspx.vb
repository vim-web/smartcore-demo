
Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class HoConsolidationRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1136
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Txt_FrDate.Text = Session("TransDate")
                Txt_ToDate.Text = Session("TransDate")
                CmbSection.Items.Add("LOAN")
                CmbSection.Items.Add("DEPOSIT")
                CmbSection.Items.Add("GROUP DEPOSIT")


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

    Protected Sub Cmd_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Try
            If CmbSection.SelectedItem.Text = "LOAN" Then


                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String


                HeaderArr(0, 0) = "BRANCH"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "18"

                HeaderArr(1, 0) = "Open No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Open Bal"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "14"

                HeaderArr(3, 0) = "Pay No"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "5"
                HeaderArr(3, 3) = "6"

                HeaderArr(4, 0) = "Pay Amt"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Rct No"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "5"
                HeaderArr(5, 3) = "6"


                HeaderArr(6, 0) = "Rct Amt"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "14"

                HeaderArr(7, 0) = "Cl No"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "5"
                HeaderArr(7, 3) = "6"


                HeaderArr(8, 0) = "Closing Amt"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "10"
                HeaderArr(8, 3) = "14"

                HeaderArr(9, 0) = "OD No"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "5"
                HeaderArr(9, 3) = "6"


                HeaderArr(10, 0) = "OD Amount"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "13"

                HeaderArr(11, 0) = "Int Amt"

                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "10"
                HeaderArr(11, 3) = "13"

                HeaderArr(12, 0) = "OD.Int"
                HeaderArr(12, 1) = "R"
                HeaderArr(12, 2) = "10"
                HeaderArr(12, 3) = "13"

                Session("RptHeader") = HeaderArr
            ElseIf CmbSection.SelectedItem.Text = "DEPOSIT" Then



                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String


                HeaderArr(0, 0) = "BRANCH"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "18"

                HeaderArr(1, 0) = "Open No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Open Bal"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "10"
                HeaderArr(2, 3) = "14"

                HeaderArr(3, 0) = "Pay No"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "5"
                HeaderArr(3, 3) = "6"

                HeaderArr(4, 0) = "Pay Amt"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Rct No"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "5"
                HeaderArr(5, 3) = "6"


                HeaderArr(6, 0) = "Rct Amt"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "14"

                HeaderArr(7, 0) = "Cl No"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "5"
                HeaderArr(7, 3) = "6"


                HeaderArr(8, 0) = "Closing Amt"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "10"
                HeaderArr(8, 3) = "14"

            

                HeaderArr(9, 0) = "Int Amt"

                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "10"
                HeaderArr(9, 3) = "13"

                HeaderArr(10, 0) = "OD.Int"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "13"

                Session("RptHeader") = HeaderArr
            End If

            Dim M_Sql
            M_Sql = "Exec [HO_Loan_Schedule_Sp] '" & CmbSection.SelectedItem.Text & "','<-- ALL -->','ALL','" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged

    End Sub
End Class