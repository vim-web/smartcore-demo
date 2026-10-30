Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Public Class GDSDetails
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1060
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then

            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Load_DefaultValues()
            Txt_FrDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Cmb_Branch)
        Txt_FrDate.Text = Session("TransDate")
        Txt_ToDate.Text = Session("TransDate")
        Cmb_Branch.Items.Add("ALL")
    End Sub

    Protected Sub Cmd_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        M_DBConn = New Connection()

        Dim Brcode As String

        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If

        If Opt_GdsNo.Checked = True Then
            Dim HeaderArr(2, 4) As String
            Dim HeaderWidth(2, 1) As String
            HeaderArr(0, 0) = "GDS PARTICULARS"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "40"

            HeaderArr(1, 0) = "COUNT"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "40"
            HeaderArr(1, 3) = "20"

            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC GdsParticulars_rpt'" & Brcode & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        ElseIf Opt_GdsDetais.Checked = True Then
            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String
            HeaderArr(0, 0) = "BRANCH"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "ACC NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "BALANCE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "START DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "END DATE"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "CLASSIFICATION OF OD"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"


            HeaderArr(6, 0) = "OD MONTH"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "15"
            HeaderArr(6, 3) = "10"
            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC GdsBranchParticulars_rpt'" & Brcode & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        ElseIf Opt_GdsDuedet.Checked = True Then
            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "GDS NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "START DATE"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "TOTAL SALA"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "INSTALMENT"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "NO OF INSTALMENT"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC GdsDetails_rpt'" & Brcode & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Exit Sub
        End If



    End Sub

    

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class