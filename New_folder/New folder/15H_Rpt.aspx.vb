Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class _15H_Rpt
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Txt_DemandDate.Text = Session("TransDate")
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
 
        Dim ClsComm As New CommonClass()
        Dim M_Sql As String
        Dim M_Dt As DataTable = New DataTable()
        Dim StrSlf As String = ""
        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

        M_Sql = "Exec custtds_dtls  '" & Convert.ToDateTime(Txt_DemandDate.Text).ToString("MM/dd/yyyy") & "'"

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

  

        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|15-G Submition Report As On: " & Txt_DemandDate.Text & " |User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/tdsdetails.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)




      

    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class