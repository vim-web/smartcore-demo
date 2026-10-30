Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL


Partial Public Class LoanOpenClose_Report
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
                M_DBConn = New Connection

                ''**********************************************************************************
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")


                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")

                'DdlBranch.Items.Add("<-- ALL -->")
                DdlScheme.Items.Add("<-- ALL -->")
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
            RbtSummery.Checked = False
            DdlScheme.Visible = True
        ElseIf rbtnClosing.Checked = True Then
            rbtnOpening.Checked = False

        End If
    End Sub

    Protected Sub rbtnClosing_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbtnClosing.CheckedChanged
        If rbtnClosing.Checked = True Then

            rbtnOpening.Checked = False
            RbtSummery.Checked = False
            DdlScheme.Visible = True
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
        M_Sql = Rpt_Qry '.ToUpper()
        'Formula From Sql Query Output

        Dim M_Qry As String = ""
        If rbtnOpening.Checked = True Then
            Dim HeaderArr(12, 4) As String
            Dim HeaderWidth(12, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "CustID"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "Cust Ad Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "BrName"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "SchName"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "13"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "Section"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "13"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "Acc No"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "13"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "Mem No"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "13"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "Name"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "15"

            HeaderArr(9, 0) = "LoanAmt"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "12"

            HeaderArr(10, 0) = "Canvasser Name"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "15"
            HeaderArr(10, 3) = "15"

            HeaderArr(11, 0) = "LoanDate"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "10"
            HeaderArr(11, 3) = "10"

            HeaderArr(12, 0) = "RegNo"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "10"
            HeaderArr(12, 3) = "10"

         

            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [LoanOpeningClosingRpt_Sp] '" & Brcode & "', '" & IIf(SchemeCode = "<-- ALL -->", "<-- ALL -->", SchemeCode) & "', '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','O','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub

        ElseIf rbtnClosing.Checked = True Then

            Dim HeaderArr(13, 4) As String
            Dim HeaderWidth(14, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "CustID"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "Cust Ad Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "BrName"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "SchName"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "13"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "Section"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "13"
            HeaderArr(5, 3) = "13"

            HeaderArr(6, 0) = "Acc No"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "13"
            HeaderArr(6, 3) = "13"

            HeaderArr(7, 0) = "Mem No"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "13"
            HeaderArr(7, 3) = "13"

            HeaderArr(8, 0) = "Name"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "15"

            HeaderArr(9, 0) = "LoanAmt"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "12"


            HeaderArr(10, 0) = "Canvasser Name"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "15"
            HeaderArr(10, 3) = "15"

            HeaderArr(11, 0) = "LoanDate"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "10"
            HeaderArr(11, 3) = "10"

            HeaderArr(12, 0) = "CloseDate"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "10"
            HeaderArr(12, 3) = "10"


            HeaderArr(13, 0) = "RegNo"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "10"
            HeaderArr(13, 3) = "10"






            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [LoanOpeningClosingRpt_Sp] '" & Brcode & "', '" & IIf(SchemeCode = "<-- ALL -->", "<-- ALL -->", SchemeCode) & "', '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','C','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub
        ElseIf RbtSummery.Checked = True Then

            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String

         

            HeaderArr(0, 0) = "Branch"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "Loan Type"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "No Of Opened"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "Open LoanAmt"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "12"
            HeaderArr(3, 3) = "12"

            HeaderArr(4, 0) = "No Of Closed"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Closed LoanAmt"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"



            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [Get_Loan_OpenClose_Summery_Sp] '" & Brcode & "',  '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        End If

    End Sub

    Protected Sub Img_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RbtSummery.CheckedChanged
        If RbtSummery.Checked = True Then
            rbtnClosing.Checked = False
            rbtnOpening.Checked = False
            DdlScheme.Visible = False
        Else
            RbtSummery.Checked = False
            DdlScheme.Visible = True
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