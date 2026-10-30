Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Public Class WebForm6
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1052
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If



                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(Cmb_Branch)

                M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"), "DEPOSIT")

                Txt_FrDate.Text = Session("TransDate")
                Txt_ToDate.Text = Session("TransDate")
                Dim M_CommonDa As New CommonDA
                Company_Name = M_CommonDa.GetCompanyDetails()
                M_CommonDa = Nothing

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

    Protected Sub Cmd_Show_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        M_DBConn = New Connection()
        M_Sql = Rpt_Qry 

        Dim M_Qry As String = ""
        Dim M_QryFormula As String = ""
        Dim QryVar As String = ""
        Dim QryNo1 As Integer = "0"
        Company_Name = HttpContext.Current.Cache("CompanyName")

        M_Sql = "EXEC [DepositMatruedDueDateRptNew_Sp] '" & Cmb_Branch.SelectedValue & "','" & Cmb_Scheme.SelectedValue & "','" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "','Y','Y','Y','DEPOSIT'"
        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

        

        'M_Qry = "Select O.*,D.Dm_DepAmount,C.Cust_RelTyp_Code +''+C.Cust_RelNm as relation,C.Cust_Name,C.Add_Address1,C.Add_Address2,C.Add_Address3,C.Add_PostOffice ,Br_Phone1" & _
        '        "From Deposit_MaturedList_R_Tbl O" & _
        '        "inner join Dep_Master_M_Tbl D On D.Br_Code=O.Br_Code And D.Sch_Code=O.Sch_Code And D.Acc_No=O.Acc_no" & _
        '        "inner join Cust_Details_Vw C On C.Cust_ID=D.Cust_ID  " & _
        '        "inner join Gen_BranchDetails_P_Tbl B on B.Br_Code=O.Br_Code" & _
        '        "order by right(Space(18) + O.Acc_no,18)"

        M_Qry = "Select O.*,D.Dm_DepAmount,C.Cust_RelTyp_Code +''+C.Cust_RelNm as relation,C.Cust_Name,C.Add_Address1,C.Add_Address2,C.Add_Address3,C.Add_PostOffice ,Br_Phone1 From Deposit_MaturedList_R_Tbl O inner join Dep_Master_M_Tbl D On D.Br_Code=O.Br_Code And D.Sch_Code=O.Sch_Code And D.Acc_No=O.Acc_no inner join Cust_Details_Vw C On C.Cust_ID=D.Cust_ID  inner join Gen_BranchDetails_P_Tbl B on B.Br_Code=O.Br_Code where O.Dm_DueDate between'" & Format(CDate(Txt_FrDate.Text), "MM/dd/yyyy") & "'and'" & Format(CDate(Txt_ToDate.Text), "MM/dd/yyyy") & "'and O.Br_Code='" & Cmb_Branch.SelectedValue & "'and O.Sch_Code='" & Cmb_Scheme.SelectedValue & "'order by right(Space(18) + O.Acc_no,18)"


        M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)
        Session.Add("RptQry", M_Qry)
        'Rpt_Path = "Dep_Intimation.rpt"
        Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Dep_Intimation.rpt"))

        StrSlf = "Company|" & Company_Name & "|Branch|" & Cmb_Branch.SelectedItem.ToString & " - Branch|Heading| Due Date Intimation   As on : " & Txt_FrDate.Text & " |User_ID|" & Session("UserID")

        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"


        Session.Add("RptFormula", StrSlf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    End Sub

    Protected Sub Cmd_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class