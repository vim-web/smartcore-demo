
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL


Partial Public Class GoldLoanOpenClose_Report_aspx
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1093
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
                'M_CommonBL.Fill_Branch(DdlBranch)

                DdlBranch.SelectedValue = Session("DefaultBrCode")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")


                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

                DdlBranch.Items.Add("<-- ALL -->")
                DdlScheme.Items.Add("<-- ALL -->")
                DdlBranch.Enabled = True
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

    Protected Sub rbtnOpening_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbtnOpening.CheckedChanged
        If rbtnOpening.Checked = True Then

            rbtnClosing.Checked = False
        ElseIf rbtnClosing.Checked = True Then
            rbtnOpening.Checked = False
        End If
    End Sub

    Protected Sub rbtnClosing_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbtnClosing.CheckedChanged
        If rbtnClosing.Checked = True Then

            rbtnOpening.Checked = False
        ElseIf rbtnOpening.Checked = True Then
            rbtnClosing.Checked = False
        End If
    End Sub


    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

        If Not IsDate(DtFrom.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(DtTo.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If

        Dim Brcode As String
        Dim SchemeCode As String

        Brcode = DdlBranch.SelectedValue
        SchemeCode = DdlScheme.SelectedValue

        M_Sql = "EXEC Proc_GoldLoan_OpCloRegister_Rpt '" & IIf(Brcode = "<-- ALL -->", "0", Brcode) & "', '" & IIf(SchemeCode = "<-- ALL -->", "0", SchemeCode) & "', '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & IIf(rbtnClosing.Checked, "C", "O") & "'"
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & IIf(Brcode = "<-- ALL -->", " ALL ", DdlBranch.SelectedItem.ToString) & "|Heading|Gold " & IIf(rbtnClosing.Checked, "Closing ", "Opening") & " Register from " & DtFrom.Text & " to " & DtTo.Text & "|User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        If rbtnClosing.Checked Then
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldLoan_ClosingRegister_SchWise.rpt"))
        Else
            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/GoldLoan_Opening Register_SchWise.rpt"))
        End If

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

    End Sub

    Protected Sub Img_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Close.Click
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
            DdlBranch.Items.Add("<-- ALL -->")
        Catch ex As Exception

        End Try
    End Sub
End Class