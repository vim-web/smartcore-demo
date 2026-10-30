Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Public Class ChittyAuctionChange
    Inherits System.Web.UI.Page
    Private M_DBConn As New Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            End If

    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBl As New CommonBL
        Dim M_ChittyGroupBL As New ChittyGroupBL
        M_CommonBl.Fill_Branch(CmbBranch)
        M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))
        M_ChittyGroupBL.Get_ChittyGrp(CmbChityGroup, Session("DefaultBrCode"), CmbScheme.SelectedValue)

        'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        'Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbChityGroup_SelectedIndexChanged(Me.CmbChityGroup, System.EventArgs.Empty)
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        'Try
        '    Dim M_ChittyGroupBL As New ChittyGroupBL
        '    M_ChittyGroupBL.Get_ChittyGrp(CmbChityGroup, CmbBranch.SelectedValue, CmbScheme.SelectedValue)
        'Catch ex As Exception
        '    ShowAlertMessage(ex.Message)
        'End Try
    End Sub

   
    Protected Sub CmbChityGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbChityGroup.SelectedIndexChanged
        Try
            Dim M_ChittyAuctnChangeBl As New ChittyAuctionChangeBL
            M_ChittyAuctnChangeBl.fill_auctn(CmbAuctionNo, CmbChityGroup.SelectedValue)
            'Dim M_ChittyGroupBL As New ChittyGroupBL
            'TxtAuctnNo.Text = M_ChittyGroupBL.Get_ChittyNextAucNo(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChityGroup.SelectedValue)
            'M_ChittyGroupBL.Get_ChittyPendingAccounts(CmbAccNo, Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChityGroup.SelectedValue)
            Call CmbAuctionNo_SelectedIndexChanged(Me.CmbAuctionNo, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtNewAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtNewAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dim CustID As String = ""

        Dim M_AccNo As String = TxtNewAccNo.Text
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnNewAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            CustID = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
        Else

            ShowAlertMessage("INVALID ACCOUNT NO")

        End If

        M_CommonBL = Nothing
        If Len(CustID) > 0 Then TxtNewAccNo.Focus()
    End Sub
    Private Sub Clear_Controls()
        TxtNewAccNo.Text = ""
        TxtName.Text = ""
        TxtDiscntAmt.Text = ""
        TxtBankCharge.Text = ""
        TxtAccNo.Text = ""
        TxtAuctnAmt.Text = ""
        TxtAddress.Text = ""
        CmbAuctionNo.Items.Clear()
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable

            Dim M_AccNo As String = TxtAccNo.Text
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            Else

                ShowAlertMessage("INVALID ACCOUNT NO")

            End If

            Dim M_AuctionChange As New SMARTCORE_BO.ChittyAuctionChangeBO
            Dim M_StID As Integer

            With M_AuctionChange
                .GrpID = CmbChityGroup.SelectedValue
                .AuctionNo = CmbAuctionNo.SelectedValue
                .OldAccid = Val(HdnAccId.Value)
                .NewAccid = Val(HdnNewAccId.Value)
                .MakingTime = "1/1/1900"
                .MakerID = Session("Login")
            End With
            Dim M_AuctionChangeBL As New ChittyAuctionChangeBL
            Session("M_EntryMode") = "NEW"
            M_AuctionChangeBL.Save_Data(M_AuctionChange, Session("M_EntryMode"))

            ShowAlertMessage("Successfully Saved")
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub


    Protected Sub CmbAuctionNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAuctionNo.TextChanged
        Dim M_Datatable As New DataTable
        Dim M_Sql As String
        M_Sql = "Select A.Acc_no,AuctionAmt,DiscountAmt,CommissionAmt from GroupDeposit_Auction_M_Tbl G join Accmain_m_tbl A on A.acc_id = G.Acc_id where G.GrpID = '" & CmbChityGroup.SelectedValue & "' And AuctionNo = '" & CmbAuctionNo.SelectedValue & "'"
        M_DBConn.ExecuteDataTable(M_Datatable, M_Sql, False)
        If M_Datatable.Rows.Count > 0 Then
            TxtBankCharge.Text = M_Datatable.Rows(0)("CommissionAmt").ToString()
            TxtDiscntAmt.Text = M_Datatable.Rows(0)("DiscountAmt").ToString()
            TxtAuctnAmt.Text = M_Datatable.Rows(0)("AuctionAmt").ToString()
            TxtAccNo.Text = M_Datatable.Rows(0)("Acc_no").ToString()
        End If
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            'Session("Branch") = CmbBranch.SelectedValue
            Dim M_ChittyGroupBL As New ChittyGroupBL
            M_ChittyGroupBL.Get_ChittyGrp(CmbChityGroup, CmbBranch.SelectedValue, CmbScheme.SelectedValue)
            Call CmbChityGroup_SelectedIndexChanged(Me.CmbChityGroup, System.EventArgs.Empty)
            Clear_Controls()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

End Class