Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LoanableFundRpt
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL


                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DtFrom.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim i As Integer
        Dim Str_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()

        If DdlBranch.SelectedValue = "<--ALL-->" Then
            For i = 0 To DdlBranch.Items.Count - 1
                Str_Sql = "Exec Loanable_Fund_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("HostID") & "','" & DdlBranch.Items(i).Value & "', '" & Session("LoginID") & "'"
                M_DBConn.ExecuteDataTable(M_Dt, Str_Sql, False)
            Next
        Else
            Str_Sql = "Exec Loanable_Fund_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("HostID") & "','" & DdlBranch.SelectedItem.Value & "', '" & Session("LoginID") & "'"
            M_DBConn.ExecuteDataTable(M_Dt, Str_Sql, False)
        End If

        Dim Sql_string As String
        Sql_string = "SELECT     G.Sub_Heading,G.Group_ID, SUM(ISNULL(P.Amount1, 0)) AS Amount1 " & _
        "FROM         Rpt_ProcDetails_R_Tbl AS P " & _
        "RIGHT OUTER JOIN Rpt_Generator_R_Tbl AS G ON P.Report_Id = G.Report_ID " & _
        "AND P.Group_Id = G.Group_ID AND P.Head_Id = G.Head_ID AND P.Sub_Head_Id = G.Sub_Head_ID AND "
        Sql_string = Sql_string & "P.Fr_Date = " & ClsComm.DbDate(DtFrom.Text) & " AND P.To_Date = " & ClsComm.DbDate(DtFrom.Text) & " "

        If DdlBranch.SelectedItem.Value <> "<--ALL-->" Then
            Sql_string = Sql_string & " And P.Br_Code='" & DdlBranch.SelectedItem.Value & "' "
        End If

        Sql_string = Sql_string & " Where(G.Report_ID = 10) " & _
            "GROUP BY G.Sub_Heading, G.Sub_Head_Order,G.Group_ID ORDER BY G.Sub_Head_Order"

        Dim str_flf As String
        str_flf = "Company|" & Company_Name & "|Branch|" & IIf(DdlBranch.SelectedValue = "", "Consolidation", DdlBranch.SelectedValue) & "|Heading|Loanable Fund Statement As on " & DtFrom.Text
        str_flf = str_flf & "|User|" & Session("LoginId")


        Session.Add("RptQry", Sql_string)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans\Loanable_Fund.rpt"))

        Session.Add("RptFormula", str_flf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


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