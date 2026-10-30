
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Public Class LoanActionTakenCancel
    Inherits System.Web.UI.Page
    Public LoanActionTbl As New DataTable("Table")
    Dim FormID As Integer = 1150
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            'User(Rights)
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Load_DefaultValues()
            Set_Grid()
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbBranch.Enabled = False
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        'M_CommonBL.Fill_User(CmbUser)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))


        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        'Call CmbRemndrTo_SelectedIndexChanged(Me.CmbRemndrTo, System.EventArgs.Empty)
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        'TxtUserId_TextChanged(Me.TxtUserId, System.EventArgs.Empty)
    End Sub
    Private Sub Clear_Controls()
        TxtAccNo.Text = ""
        TxtAcHoldrAddr.Text = ""
        TxtAcHoldrName.Text = ""

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Set_Grid()
        DgvLoanActionTable.DataSource = Nothing
        LoanActionTbl.Columns.Add("Reg_Date")
        LoanActionTbl.Columns.Add("SendTo")
        LoanActionTbl.Columns.Add("Amount")
        LoanActionTbl.Columns.Add("Acc_Name")
        LoanActionTbl.Columns.Add("Pkc_Description")
        LoanActionTbl.Columns.Add("SL_NO")
        LoanActionTbl.Rows.Add("", "", "", "", "", "")
        Session("LoanActionTbl") = LoanActionTbl
        DgvLoanActionTable.DataSource = Session("LoanActionTbl")
        DgvLoanActionTable.DataBind()
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        'TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As New DataTable
        Dim M_LoanActionTbl As New DataTable
        Dim M_LoanMasterBL As New LoanMasterBL
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            Dim M_AccNo As String = TxtAccNo.Text

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                'Session("CustomerId") = TxtCid.Text
                HdnAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                TxtAcHoldrName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                TxtAcHoldrAddr.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()


                M_LoanActionTbl = M_LoanMasterBL.Get_ActionLoanTable(HdnAccID.Value)
                If M_LoanActionTbl.Rows.Count > 0 Then


                    DgvLoanActionTable.DataSource = M_LoanActionTbl
                    DgvLoanActionTable.DataBind()
                    Session("LoanActionTbl") = M_LoanActionTbl
                End If
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
            End If
            M_CommonBL = Nothing



        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub

    Private Sub DgvReminder_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvLoanActionTable.RowCommand

        'Delete Right
        If Obj_UserRights.Delete_Right = False Then
            ShowAlertMessage("You Have No Permission To Cancel!!!")
            Exit Sub
        End If

        Dim M_Sql As String
        Dim M_DBConn As New Connection
        Dim i As Integer = Convert.ToInt32(e.CommandArgument)
        M_Sql = "Update Loan_ActionTaken_M_Tbl set  SettlmentAmt =0 ,Amount =0,Balance =0   where Sl_No  ='" & DgvLoanActionTable.Rows(i).Cells(0).Text & "'"
        M_DBConn.ExecuteScalar(M_Sql, 0, False)
        ShowAlertMessage("Charge Cancelled   ")
        M_DBConn = Nothing
        Call TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
    End Sub
End Class