Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class LoanArbitration
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

        
            Dim HeaderArr(8, 4) As String

            HeaderArr(0, 0) = "MEMBERNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "12"
            HeaderArr(0, 3) = "10"

            HeaderArr(1, 0) = "CUSTOMERNAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "12"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "12"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "PAYMENT"
            HeaderArr(3, 1) = "r"
            HeaderArr(3, 2) = "16"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "RECEIPT"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "16"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "INTEREST"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "16"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "CHARGESPAID"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "12"
            HeaderArr(6, 3) = "15"


            HeaderArr(7, 0) = "CHARGES"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "12"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "BALANCE"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "16"
            HeaderArr(8, 3) = "15"


            Session("RptHeader") = HeaderArr
            Dim Dt As Date
            Dim M_Sql As String
            Dt = TxtDate.Text
            M_Sql = "Exec Loan_ArbitrationLedger_Sp '" & HdnAccid.Value & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
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