
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports CrystalDecisions.Shared

Partial Public Class InOperativeDeposit
    Inherits System.Web.UI.Page
    Dim M_Sql As String
    Dim M_ReportBl As New ReportsBL
    Private Shared CommCls As New CommonClass()

    Dim M_CommonDa As New CommonDA
    Shared Company_Name As String
    Dim StrSlf As String
    Dim OpCash As Double
    Dim ClCash As Double
    Dim Dt As DataTable
    Private Shared M_DBConn As New Connection
    Dim FormID As Integer = 1071
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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

                'CmbOutput.Items.Add("Printer")
                'CmbOutput.Items.Add("Screen")
                'CmbOutput.SelectedIndex = 0
                Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()
                Assign_Values()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        CmbSection.SelectedIndex = 0
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
        M_CommonBL = Nothing
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        TxtFromDate.Text = Session("TransDate")
        TxtTodate.Text = Session("TransDate")


    End Sub

    Private Sub Assign_Values()
        Dim M_CommonBL As New CommonBL

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")

        M_CommonBL = Nothing

    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
    End Sub


    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim RptName As String = ""
            Dim HeadingName As String = ""
            Dim M_Qry As String
            Dim ClsComm As New CommonClass()


            If CmbSection.SelectedItem.Text = "DEPOSIT" And CmbType.SelectedItem.Text = "INOPERATIVE LIST" Then


                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String


                HeaderArr(0, 0) = "SL.NO"
                HeaderArr(0, 1) = "R"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "ACCOUNT NO"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "DEPOSIT DATE"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "12"
                HeaderArr(2, 3) = "12"

                HeaderArr(3, 0) = "NAME"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "30"
                HeaderArr(3, 3) = "30"

                HeaderArr(4, 0) = "BALANCE"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "30"
                HeaderArr(4, 3) = "30"

                HeaderArr(5, 0) = "LAST TR.DATE"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "20"
                HeaderArr(5, 3) = "13"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC InOperativeAccountList '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub

            ElseIf CmbSection.SelectedItem.Text = "DEPOSIT" And CmbType.SelectedItem.Text = "FREEZED LIST" Then


                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String


                HeaderArr(0, 0) = "SL.NO"
                HeaderArr(0, 1) = "R"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "ACCOUNT NO"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "DEPOSIT DATE"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "12"
                HeaderArr(2, 3) = "12"

                HeaderArr(3, 0) = "NAME"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "30"
                HeaderArr(3, 3) = "30"

                HeaderArr(4, 0) = "BALANCE"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "30"
                HeaderArr(4, 3) = "30"

                HeaderArr(5, 0) = "REASON"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "25"
                HeaderArr(5, 3) = "15"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC FreezedAccountList '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub




            ElseIf CmbSection.SelectedItem.Text = "LOAN" And CmbType.SelectedItem.Text = "INOPERATIVE LIST" Then
                Dim HeaderArr(6, 4) As String
                Dim HeaderWidth(6, 1) As String


                HeaderArr(0, 0) = "SL.NO"
                HeaderArr(0, 1) = "R"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "10"

                HeaderArr(1, 0) = "ACCOUNT NO"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "LOAN DATE"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "12"
                HeaderArr(2, 3) = "12"

                HeaderArr(3, 0) = "NAME"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "30"
                HeaderArr(3, 3) = "30"

                HeaderArr(4, 0) = "BALANCE"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "30"
                HeaderArr(4, 3) = "30"

                HeaderArr(5, 0) = "LAST TR.DATE"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "20"
                HeaderArr(5, 3) = "13"

                Session("RptHeader") = HeaderArr

                M_Qry = "EXEC InOperativeAccountList '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Qry
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub


                

            End If




        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub


End Class