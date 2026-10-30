Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class GoldStockRegister
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1069
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
            Load_DefaultValues()

        End If
    End Sub

    Public Sub Load_DefaultValues()
        Txt_FromDate0.Attributes.Add("readonly", "readonly")
        Txt_ToDate.Attributes.Add("readonly", "readonly")
        Opt_Detailed.Checked = True
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Ddl_Branch)
        M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "LOAN", "GOLDLOAN")
        Ddl_Scheme.Items.Add("<--ALL-->")
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        If ValidateFields() = False Then
            Exit Sub
        End If



        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String
        Dim StrSlf As String







        M_DBConn = New Connection()
        Company_Name = M_CommonDa.GetCompanyDetails()
        
        Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")

        If Opt_Detailed.Checked = True Then





            M_Sql = "Exec Proc_Gol_Stock_Register '" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_ToDate.Text) & "," & ClsComm.DbDate(Txt_FromDate0.Text) & ",'N','" & Terminal & "'"

            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)


            M_Sql = ""


            M_Sql = "Select  *,(Select Top 1 Ggr_GrmRate from Gold_GramRate_P_Tbl Where Ggr_EffectDate<=" & ClsComm.DbDate(Txt_ToDate.Text) & " Order by Ggr_EffectDate Desc) As Gram_Rate "
            M_Sql = M_Sql & " From Tmp_Gold_Stock_Register Where Br_Code = '" & Ddl_Branch.SelectedValue & "' And Terminal = '" & Terminal & "' Order By GolDate,Sl_no "


            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            '  Server.MapPath("~/REPORTS/Loans/Gold_Stock_Register_Detailed.rpt")
            Session.Add("RptPath", Server.MapPath("~/REPORTS/Loans/Gold_Stock_Register_Detailed.rpt"))



            'M_Sql = "EXEC Process_LoanAccrued_Sp '" & Ddl_Branch.SelectedValue & "','S','" & Ddl_Scheme.SelectedValue & "','','" & ClsComm.DbDate(Txt_ToDate.Text) & "','" & ClsComm.DbDate(Txt_ToDate.Text) & "','N','N','N','Y','" & Terminal & "','SA'"

            'M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)


            'M_Sql = ""


            'M_Sql = "Select L.Sch_Code,L.Sch_Name,O.Acc_No,L.CustName,'' As Orn_Type_Code,L.Lm_LoanDate,L.Lm_DueDate,'' As Orn_Type,"
            'M_Sql = M_Sql & " Cast(Isnull(Sum(Orn_NetWt),0) As Decimal(15,2)) As Orn_NetWt,Cast(Isnull(Sum(Orn_GrossWt),0) As Decimal(15,2)) As Orn_GrossWt,'' As Orn_Remarks "
            'M_Sql = M_Sql & " ,A.Outstanding,Lm_LoanAmount,Lm_ApprValue ,dbo.Get_OrnamentList_Fn(A.Br_Code,A.Sch_Code,A.Acc_No) As Ornament_details From Loan_Detail_Vw L Join "
            'M_Sql = M_Sql & " Loan_Outstand_R_Tbl A On A.Br_Code=L.BrCode And A.Sch_Code=L.Sch_Code And A.Acc_No=L.AcNo And A.Terminal='" & Terminal & "' And A.UserId='SA' And Demand_Date='" & ClsComm.DbDate(Txt_ToDate.Text) & "'  "
            'M_Sql = M_Sql & " Join Gold_Ornaments_M_Tbl O On O.Br_Code=L.BrCode And O.Sch_Code=L.Sch_Code And O.Acc_No=L.AcNo  Join PickupCode_P_Tbl P On P.Pkc_Code=O.Orn_Type "
            'M_Sql = M_Sql & "  Where L.Authorised='Y' And L.Sch_Code='" & Ddl_Scheme.SelectedValue & "' and L.BrCode='" & Ddl_Branch.SelectedValue & "' And L.Lm_LoanDate<='" & ClsComm.DbDate(Txt_ToDate.Text) & "' Group By L.Sch_Code,L.Sch_Name,L.AcNo,O.Acc_no,L.CustName,"
            'M_Sql = M_Sql & " A.Outstanding,Lm_LoanAmount,Lm_ApprValue,A.Br_Code,A.Sch_Code,A.Acc_No,L.Lm_LoanDate,L.Lm_DueDate Order By Right(Space(18)+L.AcNo,18)"


            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            'Session.Add("RptQry", M_Sql)
            'Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Loans\Loan_Ornament_List.rpt")



            StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Gold Stock Register From : " & Txt_ToDate.Text & " To " & Txt_FromDate0.Text & " |User_ID|" & Session("LoginID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
           
            Session.Add("RptFormula", StrSlf)
        ElseIf Opt_Summary.Checked = True Then

            M_Sql = "Exec Proc_Gol_Stock_Register '" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_ToDate.Text) & "," & ClsComm.DbDate(Txt_FromDate0.Text) & ",'N','" & Terminal & "'"

            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)


            M_Sql = ""


            M_Sql = "Select  *,(Select Top 1 Ggr_GrmRate from Gold_GramRate_P_Tbl Where Ggr_EffectDate<=" & ClsComm.DbDate(Txt_ToDate.Text) & " Order by Ggr_EffectDate Desc) As Gram_Rate "
            M_Sql = M_Sql & " From Tmp_Gold_Stock_Register Where Br_Code = '" & Ddl_Branch.SelectedValue & "' And Terminal = '" & Terminal & "' Order By GolDate,Sl_no "


            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/REPORTS/Loans/Gold_Stock_Register_Non_Detailed.rpt"))


            StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Gold Stock Register From : " & Txt_ToDate.Text & " To " & Txt_FromDate0.Text & " |User_ID|" & Session("LoginID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            Session.Add("RptFormula", StrSlf)

        End If


        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        ' Response.Redirect("~/RptView.aspx", False)

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Private Function ValidateFields() As Boolean
        Dim Status As Boolean






        If Val(Txt_FromDate0.Text.Length) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Date")
            Txt_FromDate0.Focus()
            Return False
        End If

        If Val(Txt_ToDate.Text.Length) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Date")
            Txt_ToDate.Focus()
            Return False
        End If



        Return Status


    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

End Class