
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class AccDocumentRpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim M_CommCL As New CommonClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoadDefaultValue()
        End If

    End Sub
    Public Sub LoadDefaultValue()
        M_CommonBL.Fill_Branch(ddl_branch)
        M_CommonBL.GetSchemes(ddl_Scheme, Session("TransDate"))
        txt_DocDate.Text = Session("TransDate")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try


    End Sub
    Protected Sub but_close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_close.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub but_save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_save.Click
        Try
            Dim HeaderArr(5, 4) As String
            HeaderArr(0, 0) = "Acc No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "6"
            HeaderArr(0, 3) = "6"

            HeaderArr(1, 0) = "Name"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "40"
            HeaderArr(1, 3) = "31"

            HeaderArr(2, 0) = "Document Date"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "7"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "Document Type"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "7"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "Document Details"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "40"
            HeaderArr(4, 3) = "31"

           

            Session("RptHeader") = HeaderArr
            Dim Br_Code As String = ddl_branch.SelectedValue.Trim()
            Dim Sch_code As String = ddl_Scheme.SelectedValue.Trim()
            Dim Strdate As String = M_CommCL.DbDate(txt_DocDate.Text)
            Dim RptType As String = Rdo_RptType.SelectedValue.Trim()
            Dim M_qry As String = "Exec AccDocumentRpt_Sp '" + Br_Code + "','" + Sch_code + "'," + Strdate + ",'" + RptType + "'"
            Session("RptQry") = M_qry
            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception
            ShowAlertMessage("Sorry.! Server Error. Try Again.")
        End Try
    End Sub
End Class
