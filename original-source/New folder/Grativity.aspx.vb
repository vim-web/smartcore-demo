
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Partial Public Class Grativity
    Inherits System.Web.UI.Page

    Dim FormID As Integer = 1142
    Shared Obj_UserRights As Object
    Dim M_Sql As String
    Dim M_DBConn As New Connection
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




            Dim M_CommonBL As New CommonBL

            Dim M_DataTable1 As New DataTable
            Dim M_DisplayMem1 As String = "Emp_name"
            Dim M_ValueMem1 As String = "Emp_ID"
            M_Sql = "select Emp_ID,Emp_name from Employee_M_Tbl "
            M_Sql = M_Sql & " order by Emp_ID "
            M_DBConn.ExecuteDataTable(M_DataTable1, M_Sql, False)
            CommonClass.Fill_Combo(CmbEmployeeName, M_DataTable1, M_DisplayMem1, M_ValueMem1)





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
   

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtGratuvity.TextChanged

    End Sub

    Protected Sub TextBox5_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txtyrs.TextChanged

    End Sub

    Protected Sub Basic_txt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBasic.TextChanged

    End Sub

    Protected Sub CmbEmployeeName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbEmployeeName.SelectedIndexChanged
        Dim M_PayrollBl As New PayrollBL
        Dim M_Dt As DataTable
        M_Dt = M_PayrollBl.Locate_GrativityDtls(CmbEmployeeName.SelectedValue, Session("TransDate"))
        If M_Dt.Rows.Count > 0 Then
            TxtBasic.Text = M_Dt.Rows(0).Item("BasisSal")
            TxtDA.Text = M_Dt.Rows(0).Item("DA")
            TxtYrs.Text = M_Dt.Rows(0).Item("ServiceYrs")
            TxtMths.Text = M_Dt.Rows(0).Item("ServiceMths")
        End If
        Dim NoOfYrs As Integer

        NoOfYrs = Val(TxtYrs.Text)
        If Val(TxtMths.Text) > 0 Then NoOfYrs = NoOfYrs + 1
        TxtGratuvity.Text = CInt(((Val(TxtBasic.Text) + Val(TxtDA.Text)) * 15 / 26) * NoOfYrs)

    End Sub
End Class