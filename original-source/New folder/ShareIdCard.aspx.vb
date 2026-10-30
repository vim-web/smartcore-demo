Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports CrystalDecisions.Shared
Public Class ShareIdCard
    Inherits System.Web.UI.Page
    Dim M_Sql As String
    Dim M_ReportBl As New ReportsBL
    Private Shared CommCls As New CommonClass()

    Dim M_CommonDa As New CommonDA
    Shared Company_Name As String
    Dim StrSlf As String
    Dim Dt As DataTable
    Private Shared M_DBConn As New Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "SHARE")
        M_CommonBL = Nothing

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        'Dim M_Sql As String
        'Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        M_DBConn = New Connection()
        'M_Sql = Rpt_Qry

        Dim M_Qry As String = ""
        Dim M_QryFormula As String = ""
        Dim QryVar As String = ""
        Dim QryNo1 As Integer = "0"
        Company_Name = HttpContext.Current.Cache("CompanyName")

        

        M_Qry = "Select M.Sm_MemNo,M.Sm_AdmitDate,I.Img_Image,C.*,S.Sch_Name,S.Sch_ShortName From Share_Master_M_Tbl M "
        M_Qry = M_Qry & "  Join Cust_Details_Vw C On C.Cust_Id=M.Cust_ID"
        M_Qry = M_Qry & "  Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code = M.Sch_Code"
        M_Qry = M_Qry & "   Left Outer Join Smart_ImageMaster.Dbo.Image_Vw I On I.Cust_ID=M.Cust_ID And Upper(I.Is_ImageName)='PHOTO'"
        M_Qry = M_Qry & "   Where M.Sch_Code= '" & CmbScheme.SelectedValue & "' and M.Sm_AdmitDate between " & CommonClass.DbDate(TxtFromDate.Text) & "and" & CommonClass.DbDate(TxtTodate.Text) & ""


        M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)
        Session.Add("RptQry", M_Qry)
        'Rpt_Path = "Dep_Intimation.rpt"
        Session.Add("RptPath", Server.MapPath("~/Reports/Share/Share_ID_Print.rpt"))

        StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading| Due Date Intimation   As on : " & TxtFromDate.Text & " |User_ID|" & Session("UserID")

        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"


        Session.Add("RptFormula", StrSlf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class