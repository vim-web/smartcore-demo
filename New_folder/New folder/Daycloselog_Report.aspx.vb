Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL


Partial Public Class Daycloselog_Report
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

                M_CommonBL.Fill_Branch(DdlBranch)

                DdlBranch.SelectedValue = Session("DefaultBrCode")



                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")


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

        Dim Br_Code As String


        Br_Code = DdlBranch.SelectedValue

        M_Sql = Rpt_Qry '.ToUpper()
        ' Formula From Sql Query Output 

        Dim M_Qry As String = ""
        If rbtnOpening.Checked = True Then
            Dim HeaderArr(3, 4) As String
            Dim HeaderWidth(3, 1) As String
            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"


            HeaderArr(1, 0) = "Date"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "4"
            HeaderArr(1, 3) = "4"

            HeaderArr(2, 0) = "USERID"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "TIME"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"


            





            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [dayendlog_sp] '" & Br_Code & "',  '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','o'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub

        ElseIf rbtnClosing.Checked = True Then

            Dim HeaderArr(3, 4) As String
            Dim HeaderWidth(3, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"


            HeaderArr(1, 0) = "Date"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "4"
            HeaderArr(1, 3) = "4"

            HeaderArr(2, 0) = "USERID"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"



            HeaderArr(3, 0) = "TIME"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"


          







            Session("RptHeader") = HeaderArr

            M_Qry = "EXEC [dayendlog_sp] '" & Br_Code & "',  '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','c'"
            Session("RptQry") = M_Qry
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub

        End If

    End Sub

    Protected Sub Img_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Img_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class