Imports SMARTCORE_BL
Imports SMARTCORE_Common
Public Class GuarantorCustomerDetails_Rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'CmbOutput.Items.Add("Printer")
                'CmbOutput.Items.Add("Screen")
                'CmbOutput.SelectedIndex = 1
                'Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL

        M_CommonBL.Fill_Branch(CmbBranch)
        CmbBranch.Items.Add("ALL")
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        ' CmbBranch.SelectedValue = Session("DefaultBrCode")
        'Dim br_code As String = Session("DefaultBrCode")
        'CmbBranch.SelectedValue = br_code
        ''CmbBranch.Items.FindByValue(Session("DefaultBrCode")).Selected = True
      
        TxtAsOnDate.Text = Session("TransDate")

        M_CommonBL = Nothing

    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        Dim Rpt_Qry As String

      

            Dim Br_Code As String



            Br_Code = CmbBranch.SelectedValue

            ' M_Sql = Rpt_Qry '.ToUpper()
            ' Formula From Sql Query Output 

            Dim M_Qry As String = ""

         





        M_Qry = "EXEC Get_GuarantorCustomer_Details_Rpt '" & IIf(Br_Code = "ALL", "%", Br_Code) & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=130", False)

       
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

End Class