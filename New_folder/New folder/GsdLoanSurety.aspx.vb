Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class GsdLoanSurety
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private pt_Formula As String
    Private Rpt_ProcessQry As String
    Dim FormID As Integer = 1136
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try

            TxtAsonDate.Attributes.Add("readonly", "readonly")
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            TxtAsonDate.Text = Session("TransDate")
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

            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_CommonDa As New CommonDA
            Dim Brcode As String
            If CmbBranch.SelectedItem.Text = "All" Then
                Brcode = "0"
            Else
                Brcode = CmbBranch.SelectedValue
            End If
            If CmbRpt.SelectedItem.Text = "LOAN" Then
                Dim HeaderArr(7, 4) As String
                Dim HeaderWidth(7, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "AccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Scheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "2"
                HeaderArr(2, 3) = "2"

                HeaderArr(3, 0) = "MemNo"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "LoanAmount"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "2"
                HeaderArr(4, 3) = "2"

                HeaderArr(5, 0) = "Balance"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "13"
                HeaderArr(5, 3) = "13"

                HeaderArr(6, 0) = "OpenDate"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "13"
                HeaderArr(6, 3) = "13"
                Session("RptHeader") = HeaderArr
                M_Sql = "Exec LoanSuretyNotIn_Sp " & ClsComm.DbDate(TxtAsonDate.Text) & ",'" & Session("UserID") & "','" & Brcode & "'"

                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

            ElseIf CmbRpt.SelectedItem.Text = "GSD" Then
                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "AccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Scheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "2"
                HeaderArr(2, 3) = "2"

                HeaderArr(3, 0) = "Balance"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "OpenDate"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "2"
                HeaderArr(4, 3) = "2"


                Session("RptHeader") = HeaderArr
                M_Sql = "Exec GsdSuretyNotIn_Sp " & ClsComm.DbDate(TxtAsonDate.Text) & ",'" & Session("UserID") & "','" & Brcode & "'"

                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class