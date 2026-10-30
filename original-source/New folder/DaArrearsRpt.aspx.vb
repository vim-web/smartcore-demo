Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class DaArrearsRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1005
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
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Session("SearchType") = "EMPLOYEE"

                Load_DefaultValues()

            End If
            If Session("SearchResult") <> "" Then
                TxtID.Text = Session("SearchResult")
                TxtID_TextChanged(Me.TxtID, System.EventArgs.Empty)
                TxtID.Focus()
                Session("SearchResult") = ""

            End If
            Session("SearchResult") = ""
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Txt_DateFrom.Text = Session("TransDate")
            Txt_DateTo.Text = Session("TransDate")
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Function Locate_Data(ByVal M_Employee As SMARTCORE_BO.EmployeeBO) As Boolean
        If M_Employee.EmpID = 0 Then
            Return False
        End If
        With M_Employee
            TxtName.Text = .EmpName
        End With
        Return True
    End Function

    Protected Sub TxtID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtID.TextChanged
        Try
            Dim M_Employee As New SMARTCORE_BO.EmployeeBO
            Dim M_PayrollBL As New PayrollBL
            M_Employee = M_PayrollBL.Locate_Data(TxtID.Text)
            If Locate_Data(M_Employee) = False Then
                ShowAlertMessage("Invalid Employee ID")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "EMP ID"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "10"

            HeaderArr(1, 0) = "EMPLOYEE NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "30"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "Basic Pay"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "DA PAID"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "NEW DA"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "ARREAR"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "15"
            HeaderArr(6, 3) = "15"

          
            Session("RptHeader") = HeaderArr

            Dim M_Sql As String
            Dim ClsComm As New CommonClass()

            M_Sql = "EXEC  DAArrear_Sp '" & TxtID.Text & "', " & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateTo.Text) & ",'" & TxtRate.Text & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub

        Catch ex As Exception

        End Try
    End Sub
End Class