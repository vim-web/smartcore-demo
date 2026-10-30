Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Partial Public Class ChittyActionTakerRpt
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
            M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"), "GROUP DEPOSIT")
            M_CommonBL.Fill_Pickup(CmbActionType, 64, 0)
            M_CommonBL.LocateSchTranHeads(Cmb_GLHead, Cmb_Scheme.SelectedValue, , 3)
            Txt_FrDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")
            Cmb_Scheme.Items.Add("All")
            Cmb_Branch.Items.Add("All")

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
        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If
        If Cmb_Scheme.SelectedValue <> "<--ALL-->" Then
            SchemeCode = Cmb_Scheme.SelectedValue
        Else
            SchemeCode = "0"
        End If


        M_DBConn = New Connection()

        Company_Name = HttpContext.Current.Cache("CompanyName")


        Dim HeaderArr(6, 4) As String
        Dim HeaderWidth(6, 1) As String
        HeaderArr(0, 0) = "DATE"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "10"
        HeaderArr(0, 3) = "10"

        HeaderArr(1, 0) = "SCHEME"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "30"
        HeaderArr(1, 3) = "30"

        HeaderArr(2, 0) = "ACCOUNT NO"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "20"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "NAME"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "20"
        HeaderArr(3, 3) = "13"

        HeaderArr(4, 0) = "CHARGE HEAD"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "20"
        HeaderArr(4, 3) = "13"

        HeaderArr(5, 0) = "CHARGE AMOUNT"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "20"
        HeaderArr(5, 3) = "13"
        Session("RptHeader") = HeaderArr
        StrSlf = CmbActionType.SelectedItem.Text

        M_Sql = "EXEC ChittyActionTakenRpt_Sp'" & Cmb_Branch.SelectedValue & "','" & Cmb_Scheme.SelectedValue & "','" & CmbActionType.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ",'" & Cmb_GLHead.SelectedValue & "','" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/PrnView.aspx?PageType=80", False)

    End Sub

    Protected Sub Cmd_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

End Class