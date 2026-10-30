Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports CrystalDecisions.Shared
Public Class CanvassedBy_Rpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'CmbOutput.Items.Add("Printer")
                'CmbOutput.Items.Add("Screen")
                'CmbOutput.SelectedIndex = 1
                'Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        CmbSection.SelectedIndex = 1
        M_CommonBL.Fill_Branch(CmbBranch)
        CmbBranch.Items.Add("<-- ALL -->")
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        ' CmbBranch.SelectedValue = Session("DefaultBrCode")
        'Dim br_code As String = Session("DefaultBrCode")
        'CmbBranch.SelectedValue = br_code
        ''CmbBranch.Items.FindByValue(Session("DefaultBrCode")).Selected = True
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
        CmbScheme.Items.Add("<-- ALL -->")
        TxtFromDate.Text = Session("TransDate")
        TxtTodate.Text = Session("TransDate")
        M_CommonBL = Nothing

    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        Dim Rpt_Qry As String

        'If CmbSection.SelectedItem.Text = "DEPOSIT" Then

        If Not IsDate(TxtFromDate.Text) Then
            ShowAlertMessage("Enter Valid From Date")
            Exit Sub
        End If

        If Not IsDate(TxtTodate.Text) Then
            ShowAlertMessage("Enter Valid To Date")
            Exit Sub
        End If

        Dim Br_Code As String
        Dim Sch_Code As String


        Br_Code = CmbBranch.SelectedValue
        Sch_Code = CmbScheme.SelectedValue

        ' M_Sql = Rpt_Qry '.ToUpper()
        ' Formula From Sql Query Output 

        Dim M_Qry As String = ""

        Dim HeaderArr(9, 4) As String
        Dim HeaderWidth(3, 1) As String


        HeaderArr(0, 0) = "SLN0"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"


        HeaderArr(1, 0) = "DEP_DATE"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "10"
        HeaderArr(1, 3) = "10"

        HeaderArr(2, 0) = "ACC_NO"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"



        HeaderArr(3, 0) = "CUST_NAME"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "15"

        HeaderArr(4, 0) = "SCH_NAME"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "15"


        HeaderArr(5, 0) = "DEP_AMOUNT"
        HeaderArr(5, 1) = "R"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "CUST_ID"
        HeaderArr(6, 1) = "R"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "10"

        HeaderArr(7, 0) = "CANVASSER_NAME"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "15"
        HeaderArr(7, 3) = "15"

        HeaderArr(8, 0) = "CANVASSER_MOB"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "15"
        HeaderArr(8, 3) = "15"




        Session("RptHeader") = HeaderArr

        M_Qry = "EXEC [CanvasedByRpt_sp] '" & Br_Code & "','" & Sch_Code & "','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "'"
        Session("RptQry") = M_Qry
        Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Exit Sub
        'End If
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
        CmbScheme.Items.Add("<-- ALL -->")

    End Sub
End Class