Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class ProcessingFeeWaiver
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("SearchBranch") = Ddl_Branch.SelectedValue
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Session("SearchType") = "ACCOUNTNO"

               
                LoadDefaultValues()
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCOUNTNO" Then
                    Txt_AccNo.Text = Session("SearchResult")
                    Txt_AccNo_TextChanged(Me.Txt_AccNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""
            End If
        

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim ParameterDt As New DataTable
            Dim M_CommonBl As New CommonBL
            ParameterDt = M_CommonBl.Get_CustName(Txt_AccNo.Text)
            If ParameterDt.Rows.Count > 0 Then

                Txt_CustName.Text = ParameterDt.Rows(0)("Cust_Name").ToString()
                Ddl_Branch.SelectedValue = ParameterDt.Rows(0)("Br_Code").ToString()
                Ddl_Scheme.SelectedValue = ParameterDt.Rows(0)("Sch_Code").ToString()
                ''Dim XXXX As String = Ddl_Branch.SelectedValue

                Dim M_Dt As New DataTable
                M_Dt = M_CommonBl.get_porcessingFee(Txt_AccNo.Text, Ddl_Scheme.SelectedValue, Session("TransDate"))
                If M_Dt.Rows.Count = 0 Then
                    ShowAlertMessage("This Customer have no Processing Fee")
                End If
                Grd_processfee.DataSource = M_Dt
                Grd_processfee.DataBind()
            Else
                ShowAlertMessage("Invalid Account Number!!!")

            End If
            Txt_subdate.Attributes.Add("readonly", "readonly")
            Txt_subdate.Enabled = False

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
    Public Sub LoadDefaultValues()
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable

            M_CommonBl.Fill_Branch(Ddl_Branch)
            M_Dt = M_CommonBl.Get_LoanScheme()
            Ddl_Scheme.DataTextField = "Sch_Name"
            Ddl_Scheme.DataValueField = "Sch_Code"
            Ddl_Scheme.DataSource = M_Dt
            Ddl_Scheme.DataBind()
            Txt_subdate.Text = Session("TransDate")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim ParameterDt As New DataTable
            Dim M_CmnBl As New CommonBL
            Dim ParameterAt As New DataTable
            ParameterAt = M_CmnBl.get_porcessingFee(Txt_AccNo.Text, Ddl_Scheme.SelectedValue, Session("TransDate"))
            ParameterDt = M_CmnBl.Get_CustName(Txt_AccNo.Text)

            Dim Acc_no As String = Txt_AccNo.Text
            Dim Sch_Code As String = ParameterDt.Rows(0)("Sch_Code")
            Dim Br_Code As String = ParameterDt.Rows(0)("Br_Code")

            Dim M_commonBl As New CommonBL
            Dim M_Datatable As DataTable
            M_Datatable = M_commonBl.Waive_ProcessFee(Br_Code, Acc_no, Sch_Code, Session("TransDate"), Session("UserID"))
            If ParameterAt.Rows(0).Item("Authorised") = "Y" Then
                ShowAlertMessage("Authorised Loan, Process Not Completed")
            Else
                ShowAlertMessage("Process Completed")
                Grd_processfee.DataSource = ""
                Grd_processfee.DataBind()
                Txt_AccNo.Text = ""
                Txt_CustName.Text = ""
            End If

          
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Btn_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Close.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Try
            Session("SearchBranch") = Ddl_Branch.SelectedValue
        Catch ex As Exception

        End Try
    End Sub
End Class