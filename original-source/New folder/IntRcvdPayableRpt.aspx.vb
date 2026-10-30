Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class IntRcvdPayableRpt
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 1123

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            Txt_DemandDate.Text = Session("TransDate")
            Dim M_dt As New DataTable
            Dim M_Bl As New CommonBL
            M_Bl.Fill_Branch(DrpBranch)
            DrpBranch.Items.Add("ALL")
            M_Bl.GetSchemes(DrpSection, Session("TransDate"), "LOAN")
            DrpSection.Items.Add("ALL")


        End If

    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim Heading As String = DrpSection.SelectedItem.ToString
            Dim Section As String = DrpSection.SelectedValue


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            'M_Sql = "Exec getLoanApplicationDetailsRpt_Sp '" & LaNo & "'"
            'M_DBConn.ExecuteScalar(M_Sql, , False)

            M_Sql = "Exec Get_IntRecPay_Sp   '" & DrpBranch.SelectedValue & "','" & DrpSection.SelectedValue & "'," & ClsComm.DbDate(Txt_DemandDate.Text) & ",'" & Session("HostID") & "'"

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            'If section = "GEN.LOAN" Then Heading = "GENERAL LOAN"
            'If section = "GOLDLOAN" Then Heading = "GOLD LOAN"

            StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Interest Received Receivable As On : " & Txt_DemandDate.Text & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrSlf)
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/Int_RcvdRcvbl_Rpt.rpt"))


            Session.Add("RptFormula", StrSlf)
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            'Response.Redirect("~/RptPreview.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class