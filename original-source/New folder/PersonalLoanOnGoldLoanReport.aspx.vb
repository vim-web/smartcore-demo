Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Public Class PersonalLoanOnGoldLoanReport
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1006
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Txt_FrDate.Text = Session("TransDate")
            If Session("DC_RPTS_BRANCH") IsNot Nothing Then
                Txt_FrDate.Text = Session("DC_RPTS_FROM_DATE")               
            End If
        End If
    End Sub

    Protected Sub ImageButton2_Click1(sender As Object, e As ImageClickEventArgs) Handles BtnShow.Click
        'Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

        If Not IsDate(Txt_FrDate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        'If Not IsDate(DtTo.Text) Then
        '    ShowAlertMessage("Enter Valid To Date")
        '    Exit Sub
        'End If
        Dim M_Qry As String = ""

        Dim HeaderArr(11, 4) As String
        Dim HeaderWidth(11, 1) As String

        HeaderArr(0, 0) = "Branch Name"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"

        HeaderArr(1, 0) = "Customer Name"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "5"
        HeaderArr(1, 3) = "5"

        HeaderArr(2, 0) = "Account No"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "Net Weight"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "5"
        HeaderArr(3, 3) = "5"

        HeaderArr(4, 0) = "Bank Rate"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "13"
        HeaderArr(4, 3) = "13"

        HeaderArr(5, 0) = "Loan Date"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "13"
        HeaderArr(5, 3) = "13"

        HeaderArr(6, 0) = "Loan Amount"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "15"
        HeaderArr(6, 3) = "15"

        HeaderArr(7, 0) = "OutStanding"
        HeaderArr(7, 1) = "R"
        HeaderArr(7, 2) = "12"
        HeaderArr(7, 3) = "12"

        HeaderArr(8, 0) = "Account No"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "10"

        HeaderArr(9, 0) = "Account Open Date"
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "10"
        HeaderArr(9, 3) = "10"

        HeaderArr(10, 0) = "Account Open Amount"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "10"
        HeaderArr(10, 3) = "10"

        HeaderArr(11, 0) = "OutStanding"
        HeaderArr(11, 1) = "R"
        HeaderArr(11, 2) = "12"
        HeaderArr(11, 3) = "12"


        Session("RptHeader") = HeaderArr
        M_Qry = "Exec PLOn_GoldLoan_Sp '" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "'"
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As ImageClickEventArgs) Handles BtnClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
End Class