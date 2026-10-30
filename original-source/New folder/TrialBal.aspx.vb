Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class TrialBal

    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1146
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                ''Region Wise
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")


                DtFrom.Text = Session("TransDate")

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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            If ChkExcl.Checked = True Then
                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()

                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String

                HeaderArr(0, 0) = "ACCOUNT HEAD"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "40"
                HeaderArr(0, 3) = "40"

                HeaderArr(1, 0) = "DEBIT"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "15"
                HeaderArr(1, 3) = "15"

                HeaderArr(2, 0) = "CREDIT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                Dim Dt As Date
                Dt = DtFrom.Text
                Session("RptHeader") = HeaderArr
                'If DdlBranch.SelectedItem.Text = "<-- ALL -->" Then
                '    M_Sql = "Exec TrialBalance_Sp_ToExl 'ALL','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                'Else
                M_Sql = "Exec TrialBalance_Sp_ToExl '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                'End If

                Session("RptQry") = M_Sql

                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Else


                Dim Dt As Date
                Dt = DtFrom.Text
                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()
                
                M_Sql = "Exec TrialBalance_Sp '" & DdlBranch.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Region|Region : " & DrpRegn.SelectedItem.ToString & "|Area|Area : " & DrpArea.SelectedItem.ToString & "|Heading|Trial Balance As " & DtFrom.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptFormula", StrSlf)
                Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/TrialBals.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
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