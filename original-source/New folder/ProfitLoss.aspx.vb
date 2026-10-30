Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class ProfitLoss

    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1119
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

                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                RdoOption.Items(0).Selected = True

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
            If ChkExcel.Checked = True Then
                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()

                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String



                HeaderArr(0, 0) = "HEAD CODE"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "15"
                HeaderArr(0, 3) = "15"

                HeaderArr(1, 0) = "INCOME"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "35"
                HeaderArr(1, 3) = "35"

                HeaderArr(2, 0) = "AMOUNT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "HEAD CODE"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "15"
                HeaderArr(3, 3) = "15"

                HeaderArr(4, 0) = "EXPENSE"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "35"
                HeaderArr(4, 3) = "35"

                HeaderArr(5, 0) = "AMOUNT"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "15"
                HeaderArr(5, 3) = "15"

                Dim DFr As Date
                DFr = DtFrom.Text

                Dim DTo As Date
                DTo = DtTo.Text

                Session("RptHeader") = HeaderArr
                M_Sql = "Exec ProfitLoss_UniqueHead_Sp_Exl '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

                Session("RptQry") = M_Sql

                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Else


                Dim DFr As Date
                DFr = DtFrom.Text
                Dim DTo As Date
                DTo = DtTo.Text

                Dim M_Sql As String
                Dim StrSlf As String = ""
                Dim ClsComm As New CommonClass()
                Dim M_Dt As DataTable = New DataTable()
                If RdoOption.Items(0).Selected Then
                   
                    M_Sql = "Exec ProfitLoss_Sp '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

                ElseIf RdoOption.Items(2).Selected Then
                    
                    M_Sql = "Exec ProfitLossWithRecPay_Sp '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

                ElseIf RdoOption.Items(3).Selected Then
                   
                    M_Sql = "Exec ProfitLossMonthly_Sp '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & Session("UserID") & "'"
                Else
                    
                    M_Sql = "Exec ProfitLoss_UniqueHead_Sp '" & DdlBranch.SelectedValue & "','" & Format(DFr, "MM/dd/yyyy") & "','" & Format(DTo, "MM/dd/yyyy") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

                End If

                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Profit & Loss Statement from " & DtFrom.Text & " to " & DtTo.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptFormula", StrSlf)
                If RdoOption.Items(3).Selected Then
                    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/PandLMonthly.rpt"))
                Else
                    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/ProfitLossStmt.rpt"))
                End If
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