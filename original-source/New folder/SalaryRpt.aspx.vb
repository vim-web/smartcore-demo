
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Partial Public Class SalaryRpt
    Inherits System.Web.UI.Page

    Dim FormID As Integer = 1142
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


                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim IsAll As Boolean
            Dim M_Sql As String
            Dim M_DBConn As New Connection
            TxtFromDate.Text = Format(Date.Parse(Session("TransDate")), "MM/yyyy")
            TxtToDate.Text = Format(Date.Parse(Session("TransDate")), "MM/yyyy")
            Dim M_CommonBL As New CommonBL

            Dim M_DataTable1 As New DataTable
            Dim M_DisplayMem1 As String = "Emp_name"
            Dim M_ValueMem1 As String = "Emp_ID"
            M_Sql = "select Emp_ID,Emp_name from Employee_M_Tbl "
            M_Sql = M_Sql & " order by Emp_ID "
            M_DBConn.ExecuteDataTable(M_DataTable1, M_Sql, False)
            CommonClass.Fill_Combo(CmbEmployeeName, M_DataTable1, M_DisplayMem1, M_ValueMem1)
            CmbHead.Items.Add("<-- ALL -->")


            CmbEmployeeName.Items.Add("<-- ALL -->")
            Dim M_DataTable As New DataTable

            Dim M_DisplayMem As String = "PayHead_Desc"
            Dim M_ValueMem As String = "PayHead_ID"
            M_Sql = "  select PayHead_Desc,PayHead_ID from PayHead_M_Tbl "
            M_Sql = M_Sql & " order by PayHead_ID "
            M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
            CommonClass.Fill_Combo(CmbHead, M_DataTable, M_DisplayMem, M_ValueMem)
            CmbHead.Items.Add("<-- ALL -->")


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim M_Dt As New DataTable()

            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "MONTH"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "EMPLOYEE ID"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "EMPLOYEE NAME"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "25"
            HeaderArr(2, 3) = "20"

            HeaderArr(3, 0) = "DESIGNATION"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "25"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "EMPLOYEE HEAD AMOUNT"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC SalaryTypeRpt_Sp'" & IIf(CmbHead.SelectedItem.Text = "<-- ALL -->", "%", CmbHead.SelectedValue) & "','" & TxtFromDate.Text & "','" & TxtToDate.Text & "','" & IIf(CmbEmployeeName.SelectedItem.Text = "<-- ALL -->", "%", CmbEmployeeName.SelectedValue) & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class