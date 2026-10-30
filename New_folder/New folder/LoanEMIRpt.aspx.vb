Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Partial Public Class LoanEMIRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1086
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
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                'TxtDate.Attributes.Add("readonly", "readonly")
                TxtCustName.Attributes.Add("readonly", "readonly")
                TxtAddress.Attributes.Add("readonly", "readonly")
                TxtDate.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try

            TxtAccNo.Text = Right("000" + DdlBranch.SelectedValue, 3) + Right("000" + DdlScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(DdlBranch.SelectedValue, DdlScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtCustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                HdnAccid.Value = Dt_AcDtls.Rows(0)("Acc_id").ToString()
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim M_Sql As String
            Dim StrSlf As String
            Dim BrCode As String = ""
            Dim Cust_ID As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_DBConn As New Connection
            Dim M_Ds As New DataSet()

            M_Sql = "EXEC  EMIChartRpt_Sp '" & HdnAccid.Value & "'"
            M_DBConn.ExecuteDataSet(M_Ds, M_Sql, False)
            Dim CustName As String
            Dim CentreName As String
            Dim MailingAddress As String
            Dim MblNO As String
            Dim BranchName As String
            Dim Product As String
            Dim LoanAmt As String
            Dim LoanDate As String
            Dim Tenure As String
            Dim DueDate As String
            Dim RepayMode As String
            Dim Status As String
            Dim M_Dt As DataTable = M_Ds.Tables(1)
            Dim M_DtChart As DataTable = M_Ds.Tables(0)

            CustName = M_Dt.Rows(0).Item("CustName")
            CentreName = M_Dt.Rows(0).Item("CentreName")
            MailingAddress = M_Dt.Rows(0).Item("MailingAddress")
            MblNO = M_Dt.Rows(0).Item("MblNO")
            BranchName = M_Dt.Rows(0).Item("BranchName")
            Product = M_Dt.Rows(0).Item("Product")
            LoanAmt = M_Dt.Rows(0).Item("LoanAmt")
            LoanDate = M_Dt.Rows(0).Item("LoanDate")
            Tenure = M_Dt.Rows(0).Item("Tenure")
            DueDate = M_Dt.Rows(0).Item("DueDate")
            RepayMode = M_Dt.Rows(0).Item("RepayMode")
            Status = M_Dt.Rows(0).Item("Status")
            Cust_ID = M_Dt.Rows(0).Item("CustID")

            StrSlf = "CustName|" & CustName & "|CentreName|" & CentreName
            StrSlf = StrSlf & "|MailingAddress|" & MailingAddress & "|MblNO|" & MblNO
            StrSlf = StrSlf & "|BranchName|" & BranchName & "|Product|" & Product
            StrSlf = StrSlf & "|LoanAmt|" & LoanAmt & "|LoanDate|" & LoanDate
            StrSlf = StrSlf & "|Tenure|" & Tenure & "|DueDate|" & DueDate
            StrSlf = StrSlf & "|RepayMode|" & RepayMode & "|Status|" & Status
            StrSlf = StrSlf & "|CustID|" & Cust_ID & "|UserID|" & Session("UserID")
            Session.Add("RptFormula", StrSlf)
            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/REPORTS/Loans/EmiChart.rpt"))

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class