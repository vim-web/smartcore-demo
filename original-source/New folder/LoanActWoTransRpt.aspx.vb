Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class LoanActWoTransRpt
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1078
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
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Cmb_Branch)
            M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"), ("GROUP DEPOSIT','LOAN"), ("GROUP DEPOSIT','GEN.LOAN','OD"))
            M_CommonBL.Fill_Pickup(CmbActionType, 64, 0)
            Txt_FrDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")
            Cmb_Scheme.Items.Add("<--ALL-->")
            Cmb_Branch.Items.Add("<--ALL-->")
            CmbActionType.Items.Add("<--ALL-->")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub Cmd_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim StrSlf As String
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String

        Dim Brcode As String
        Dim SchemeCode As String
        Dim ActionType As String
        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "%"
        End If
        If Cmb_Scheme.SelectedValue <> "<--ALL-->" Then
            SchemeCode = Cmb_Scheme.SelectedValue
        Else
            SchemeCode = "%"
        End If
        If CmbActionType.SelectedValue <> "<--ALL-->" Then
            ActionType = CmbActionType.SelectedValue
        Else
            ActionType = "%"
        End If




        M_DBConn = New Connection()

        Company_Name = HttpContext.Current.Cache("CompanyName")


        Dim HeaderArr(7, 4) As String
        Dim HeaderWidth(7, 1) As String
        HeaderArr(0, 0) = "SLNO"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "5"

        HeaderArr(1, 0) = "SCHEME"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "20"
        HeaderArr(1, 3) = "25"

        HeaderArr(2, 0) = "ACCOUNT NO"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "15"

        HeaderArr(3, 0) = "NAME"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "30"

        HeaderArr(4, 0) = "MEMBER.NO"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "13"

        HeaderArr(5, 0) = "REG. DATE"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "ACTION TYPE"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "20"
        HeaderArr(6, 3) = "13"
        Session("RptHeader") = HeaderArr
        StrSlf = CmbActionType.SelectedItem.Text

        M_Sql = "EXEC LOAN_WOACTION_Sp'" & Brcode & "','" & SchemeCode & "','" & ActionType & "'," & CommonClass.DbDate(Txt_FrDate.Text) & ",'" & Session("LoginID") & "'"
        Session("RptQry") = M_Sql
        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/PrnView.aspx?PageType=130", False)

    End Sub

    Protected Sub Cmd_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class