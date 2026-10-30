
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class CustomerDiseaseRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
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
                Load_Defaultvalues()
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
    Private Sub Load_Defaultvalues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(ddlBranch)
        ddlBranch.SelectedValue = Session("DefaultBrCode")
    End Sub
    
   
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        Dim M_Qry As String = ""

        Dim HeaderArr(6, 5) As String
        Dim HeaderWidth(3, 1) As String
        HeaderArr(0, 0) = "SLN0"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"


        HeaderArr(1, 0) = "CUSTID"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "4"
        HeaderArr(1, 3) = "4"

        HeaderArr(2, 0) = "CUSTNAME"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"



        HeaderArr(3, 0) = "ADDRESS"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "15"


        HeaderArr(4, 0) = "DEATHDATE"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "15"


        HeaderArr(5, 0) = "INTIMATIONDATE"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "15"
        HeaderArr(5, 3) = "15"


        HeaderArr(6, 0) = "DESCRIPTON"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "15"
        HeaderArr(6, 3) = "15"


        Session("RptHeader") = HeaderArr

        M_Qry = "EXEC [DeathMarkingRpt] '" & ddlBranch.SelectedValue & "','" & Format(CDate(Txtasondate.Text), "MM/dd/yyyy") & "'"
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Exit Sub
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class