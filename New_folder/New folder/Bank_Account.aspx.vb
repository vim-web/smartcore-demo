Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Partial Public Class Bank_Account
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Load_DefaultValues()
                'Set_Grid()
                Session("SearchType") = "ACCOUNTHEAD"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Dim M_CommonBl As New CommonBL
                M_CommonBl.Fill_Branch(DrpBranch)
               
            End If
            If Session("SearchResult") <> "" Then
                Txt_Acccode.Text = Session("SearchResult")
                Txt_Acccode_TextChanged(Me.Txt_Acccode, System.EventArgs.Empty)
                Session("SearchResult") = ""
                ''cust_grid()



            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Save_BankHead(Txt_BankName.Text, Txt_Acccode.Text, DrpBranch.SelectedValue)
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage(M_Dt.Rows(0).Item("Status").ToString())
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Txt_Acccode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Acccode.TextChanged
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Get_AccountHead(Txt_Acccode.Text)
            If M_Dt.Rows.Count > 0 Then
                Txt_BankName.Text = M_Dt.Rows(0).Item("Acc_Name").ToString()
            End If
            M_Dt = M_CommonBl.Get_BankBranch(Txt_Acccode.Text)
            If M_Dt.Rows.Count > 0 Then
                DrpBranch.SelectedValue = M_Dt.Rows(0).Item("Br_Code").ToString()
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

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class