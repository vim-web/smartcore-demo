Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Public Class FluidResRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1059
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            TxtFrDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
        End If
       
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA

        'M_DBConn = New Connection()

        'Company_Name = HttpContext.Current.Cache("CompanyName")


        Dim HeaderArr(14, 4) As String
        Dim HeaderWidth(14, 4) As String
        HeaderArr(0, 0) = "DATE"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "2"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "5"
        HeaderArr(1, 3) = "40"

        HeaderArr(2, 0) = "3"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "4"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "10"
        HeaderArr(3, 3) = "13"

        HeaderArr(4, 0) = "5"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "13"

        HeaderArr(5, 0) = "6"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "13"

        HeaderArr(6, 0) = "7"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "13"

        HeaderArr(7, 0) = "8"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "13"

        HeaderArr(8, 0) = "9"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "13"

        HeaderArr(9, 0) = "10"
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "10"
        HeaderArr(9, 3) = "13"

        HeaderArr(10, 0) = "11"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "10"
        HeaderArr(10, 3) = "13"

        HeaderArr(11, 0) = "12"
        HeaderArr(11, 1) = "L"
        HeaderArr(11, 2) = "10"
        HeaderArr(11, 3) = "13"

        HeaderArr(12, 0) = "13"
        HeaderArr(12, 1) = "L"
        HeaderArr(12, 2) = "10"
        HeaderArr(12, 3) = "13"

        HeaderArr(13, 0) = "14"
        HeaderArr(13, 1) = "L"
        HeaderArr(13, 2) = "5"
        HeaderArr(13, 3) = "13"


        Session("RptHeader") = HeaderArr
        M_Sql = "EXEC FluidResource_Rpt" & CommonClass.DbDate(TxtFrDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=130", False)

    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class