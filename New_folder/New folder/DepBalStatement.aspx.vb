Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class DepBalStatement
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1086

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "DEPOSIT")
                TxtDate.Attributes.Add("readonly", "readonly")
                TxtCustName.Attributes.Add("readonly", "readonly")
                TxtAddress.Attributes.Add("readonly", "readonly")
                TxtDate.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                Dim M_CommonDa As New CommonDA
                Company_Name = M_CommonDa.GetCompanyDetails()
                M_CommonDa = Nothing
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(DdlBranch.SelectedValue, DdlScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            TxtCustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            HdnAccid.Value = Dt_AcDtls.Rows(0)("Acc_id").ToString()
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
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

        M_Sql = "EXEC Deposit_Outstand_Sp 'N', '" & DdlBranch.SelectedValue & "','S','" & DdlScheme.SelectedValue & "','" & TxtAccNo.Text & "','" & Format(CDate(TxtDate.Text), "MM/dd/yyyy") & "','Y','" & Session("LoginID") & "','" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)


        M_Qry = "Select O.*,D.Dm_DepAmount,C.Cust_RelTyp_Code +''+C.Cust_RelNm as relation,C.Cust_Name,C.Add_Address1,C.Add_Address2,C.Add_Address3,C.Add_PostOffice ,Br_Phone1 From Deposit_Outstand_R_Tbl O inner join Dep_Master_M_Tbl D On D.Br_Code=O.Br_Code And D.Sch_Code=O.Sch_Code And D.Acc_No=O.Acc_no inner join Cust_Details_Vw C On C.Cust_ID=D.Cust_ID  inner join Gen_BranchDetails_P_Tbl B on B.Br_Code=O.Br_Code where  O.Br_Code='" & DdlBranch.SelectedValue & "'and O.Sch_Code='" & DdlScheme.SelectedValue & "' and O.Terminal='" & Session("HostID") & "'  order by right(Space(18) + O.Acc_no,18)"


        M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)
        Session.Add("RptQry", M_Qry)
        'Rpt_Path = "Dep_Intimation.rpt"
        Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Dep_balance.rpt"))

        StrSlf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.ToString & " - Branch|Heading| Due Date Intimation   As on : " & TxtDate.Text & " |User_ID|" & Session("UserID")

        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"


        Session.Add("RptFormula", StrSlf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class