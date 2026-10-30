Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class InterestDueList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_Branch(DdlBranch)
            Dtfrom.Text = Session("TransDate")
            DtTo.Text = Session("TransDate")
            M_CommonBl.GetSchemes(DdlScheme, Session("TransDate"), "LOAN", "GOLDLOAN")
            DdlScheme.Items.Add("ALL")

            M_CommonBl.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            ''Region Wise
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
            Dim M_dt As New DataTable
            M_dt = M_CommonBl.Get_Pkc_Outstand()
            Ddlsection.DataTextField = "Pkc_Description"
            Ddlsection.DataValueField = "Pkc_Code"
            Ddlsection.DataSource = M_dt
            Ddlsection.DataBind()
            Ddlsection.Items.Add("ALL")
            ChkOther_CheckedChanged(Me.ChkOther, System.EventArgs.Empty)
            DdlBranch.Enabled = True
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass

            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim To_Date As String = DtTo.Text
            Dim User_Id As String = Session("UserID")
            Dim Sch_code As String = DdlScheme.SelectedValue
          

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            If ChkOther.Checked = False Then
                M_Sql = "Exec Demand_List_Sp '" + Br_code + "','" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(To_Date).ToString("MM/dd/yyyy") + "','" & Sch_code & "','" & Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Else
                M_Sql = " Exec Get_OtherLoanDemand_sp '" & Br_code & "','" & Ddlsection.SelectedValue & "','" + Convert.ToDateTime(To_Date).ToString("MM/dd/yyyy") + "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            End If


            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading| Demand List From " & Dtfrom.Text & " To " & DtTo.Text & " |User_ID|" & User_Id
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrSlf)

            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/DemandList_Rpt.rpt"))
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

    Protected Sub ChkOther_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkOther.CheckedChanged
        If ChkOther.Checked = True Then
            DdlScheme.Visible = False
            LblScheme.Visible = False
            Ddlsection.Visible = True
            LblSection.Visible = True
        Else
            DdlScheme.Visible = True
            LblScheme.Visible = True
            Ddlsection.Visible = False
            LblSection.Visible = False
        End If

    End Sub
    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
End Class