Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class DueDateExtender
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "ACCOUNTNO"

            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCOUNTNO" Then
                    Txt_AccNo.Text = Session("SearchResult")
                    Txt_AccNo_TextChanged(Me.Txt_AccNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""
            End If
            LoadDefaultValues()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_DataTable As New DataTable
            Dim M_CommonBl As New CommonBL
         
            M_DataTable = M_CommonBl.Update_DueDate(Txt_AccNo.Text, Txt_ExtendDueDate.Text, Session("UserID"))
           
            ShowAlertMessage("Process Completed")
            Dim ParameterDt As New DataTable
            ParameterDt = M_CommonBl.DueDate_CustDetails(Txt_AccNo.Text)
            Grd_DueDate.DataSource = Nothing
            Grd_DueDate.DataBind()
            Grd_DueDate.DataSource = ParameterDt
            Grd_DueDate.DataBind()
            

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim M_CommonBl As New CommonBL
            Dim ParameterDt As New DataTable
            ParameterDt = M_CommonBl.DueDate_CustDetails(Txt_AccNo.Text)
            Txt_CustName.Text = ParameterDt.Rows(0)("NAME").ToString()
            Ddl_Branch.SelectedValue = ParameterDt.Rows(0)("Br_Code").ToString()
            Ddl_Scheme.SelectedValue = ParameterDt.Rows(0)("Sch_Code").ToString()
            Grd_DueDate.DataSource = ParameterDt
            Grd_DueDate.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadDefaultValues()
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            Dim ParameterDt As New DataTable

            M_CommonBl.Fill_Branch(Ddl_Branch)
            M_Dt = M_CommonBl.Get_LoanScheme()
            Ddl_Scheme.DataTextField = "Sch_Name"
            Ddl_Scheme.DataValueField = "Sch_Code"
            Ddl_Scheme.DataSource = M_Dt
            Ddl_Scheme.DataBind()
            ParameterDt = M_CommonBl.DueDate_CustDetails(Txt_AccNo.Text)
            Txt_DueDate.Enabled = False
            Txt_DueDate.Text = ParameterDt.Rows(0)("Due_Date").ToString()
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

    Protected Sub Btn_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Close.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class