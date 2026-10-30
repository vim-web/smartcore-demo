Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Public Class ChittyAuction
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1025
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Dim M_CommonBL As New CommonBL
                M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))
                TxtAuctionDate.Attributes.Add("readonly", "readonly")
                TxtDiscntAmt.Attributes.Add("readonly", "readonly")
                TxtBankChrg.Attributes.Add("readonly", "readonly")
                TxtBalAmt.Attributes.Add("readonly", "readonly")
                TxtAuctionDate.Text = Session("TransDate")
                Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
                Call CmdChittyGrp_SelectedIndexChanged(Me.CmbChittyGrp, System.EventArgs.Empty)

                If Len(Request.QueryString("TRANID")) > 0 Then
                    GetAuthoriseDtls("GDSAUCT", Request.QueryString("TRANID"))
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("GDSAUCT", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)

        Session("TransTbl") = M_Dataset.Tables(1)

        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        DgvTransaction.DataSource = M_Dataset.Tables(1)
        DgvTransaction.DataBind()

        'Calc_TranTotal()
        'DgvCustomer.DataSource = M_Dataset.Tables(2)
        'DgvCustomer.DataBind()

    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable)

        CmbChittyGrp.SelectedValue = M_AccTbl.Rows(0).Item("GrpID")
        Call CmdChittyGrp_SelectedIndexChanged(Me.CmbChittyGrp, System.EventArgs.Empty)
        CmbAccNo.SelectedItem.Text = M_AccTbl.Rows(0).Item("ACC_NO")
        TxtName.Text = M_AccTbl.Rows(0).Item("Cust_Name")
        TxtAuctionDate.Text = M_AccTbl.Rows(0).Item("Bid_AucDate")
        TxtAuctionNo.Text = M_AccTbl.Rows(0).Item("AuctionNo")
        TxtAuctnAmt.Text = M_AccTbl.Rows(0).Item("AuctionAmt")
        TxtDiscntAmt.Text = M_AccTbl.Rows(0).Item("DiscountAmt")
        TxtBankChrg.Text = M_AccTbl.Rows(0).Item("CommissionAmt")



        'Call CmdChittyGrp_SelectedIndexChanged(Me.CmbChittyGrp, System.EventArgs.Empty)
        'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        'Call TxtMemNo_TextChanged(Me.TxtMemNo, System.EventArgs.Empty)
        'Call TxtApplnNo_TextChanged(TxtApplnNo, System.EventArgs.Empty)

    End Sub


    Protected Sub CmdChittyGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbChittyGrp.SelectedIndexChanged
        Try
            Dim M_ChittyGroupBL As New ChittyGroupBL
            Dim M_Dt As New DataTable
            TxtAuctionNo.Text = M_ChittyGroupBL.Get_ChittyNextAucNo(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChittyGrp.SelectedValue)
            M_ChittyGroupBL.Get_ChittyPendingAccounts(CmbAccNo, Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChittyGrp.SelectedValue)
            If CmbAccNo.Items.Count > 0 Then Call CmbAccNo_SelectedIndexChanged(Me.CmbAccNo, System.EventArgs.Empty)
            M_Dt = M_ChittyGroupBL.Get_ChittyGrpDtls(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChittyGrp.SelectedValue)
            If M_Dt.Rows.Count > 0 Then
                HdnSala.Value = M_Dt.Rows(0)("SalaAmount").ToString()
                HdnChrg.Value = M_Dt.Rows(0)("BankCharge").ToString()
                HdnForeman.Value = M_Dt.Rows(0)("BankFormanAccid").ToString()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            Dim M_ChittyGroupBL As New ChittyGroupBL
            M_ChittyGroupBL.Get_ChittyGrp(CmbChittyGrp, Session("DefaultBrCode"), CmbScheme.SelectedValue)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbAccNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAccNo.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            TxtName.Text = M_CommonBL.GetAccountCustName(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbAccNo.SelectedItem.Text)
            If HdnForeman.Value = CmbAccNo.SelectedValue Then
                TxtAuctnAmt.Text = HdnSala.Value
                TxtBalAmt.Text = HdnSala.Value
                TxtBankChrg.Text = ""
                TxtDiscntAmt.Text = ""
                TxtAuctnAmt.ReadOnly = True

            Else
                TxtAuctnAmt.Text = ""
                TxtBalAmt.Text = ""
                TxtBankChrg.Text = ""
                TxtDiscntAmt.Text = ""
                TxtAuctnAmt.ReadOnly = False
            End If
            TxtAuctnAmt_TextChanged(Me.TxtAuctnAmt, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAuctnAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAuctnAmt.TextChanged
        Try
            If HdnForeman.Value <> CmbAccNo.SelectedValue Then
                TxtBankChrg.Text = HdnChrg.Value
                TxtDiscntAmt.Text = Val(HdnSala.Value) - Val(TxtAuctnAmt.Text) - Val(TxtBankChrg.Text)
            End If

            If Val(TxtAuctionNo.Text) <> 1 And HdnForeman.Value = CmbAccNo.SelectedValue Then
                TxtBankChrg.Text = HdnChrg.Value
                TxtAuctnAmt.Text = Val(HdnSala.Value) - Val(TxtBankChrg.Text)
                TxtDiscntAmt.Text = Val(HdnSala.Value) - Val(TxtAuctnAmt.Text) - Val(TxtBankChrg.Text)
            End If

            TxtBalAmt.Text = Val(TxtAuctnAmt.Text)
            Dim M_ChittyGroupBL As New ChittyGroupBL
            Dim M_Dt As New DataTable
            M_Dt = M_ChittyGroupBL.Set_GroupDepositAuction(Session("DefaultBrCode"), CmbScheme.SelectedValue, CmbChittyGrp.SelectedValue, Val(TxtDiscntAmt.Text))
            DgvTransaction.DataSource = M_Dt

            Session("ChittyAuctionTbl") = M_Dt
            DgvTransaction.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_AuctionID As Integer

            If Len(Request.QueryString("TRANID")) > 0 Then
                Dim M_CommonBL1 As New CommonBL
                If M_CommonBL1.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx.vb")
                    Exit Sub
                End If
                M_CommonBL1 = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If


            Dim M_GroupDepositAuction As New SMARTCORE_BO.GroupDepositAuctionBO

            With M_GroupDepositAuction
                .AuctionID = 0
                .AccID = CmbAccNo.SelectedValue
                .GDCode = CmbChittyGrp.SelectedValue
                .AuctionNo = Val(TxtAuctionNo.Text)
                .GDSSubPurpose = 0
                .GDSPurpose = 0
                .TrDate = Session("TransDate")
                .BidAucDate = TxtAuctionDate.Text
                .AuctionAmt = Val(TxtAuctnAmt.Text)
                .DiscountAmt = Val(TxtDiscntAmt.Text)
                .CommissionAmt = Val(TxtBankChrg.Text)
                .BidType = "A"
                .IsBonusBid = ""
            End With

            Try
                Dim M_GroupDepositAuctionBL As New ChittyGroupBL
                M_AuctionID = M_GroupDepositAuctionBL.Save_Auction(M_GroupDepositAuction, "NEW")
                M_GroupDepositAuctionBL = Nothing
            Catch ex As Exception

            End Try


            Dim M_Dt As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim Trandt As DataTable = DirectCast(Session("ChittyAuctionTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            M_Dt = M_CommonBL.Update_AccNoTran(Session("DefaultBrCode"), CmbScheme.SelectedValue, Session("TransDate"), Session("DefaultBrCode"), "", Session("LoginID"), HdnForeman.Value, "", M_TranSw.ToString, TxtAuctionNo.Text, , , , , , , , , , "GDSAUCT", "GDSAUCT", "Transfer")

            If M_Dt.Rows.Count > 0 Then
                Dim Tran_No As String = M_Dt.Rows(0)("Tran_No").ToString()
                Dim M_DBConn As New Connection
                M_DBConn.ExecuteScalar("update Transaction_T_Tbl  set Inst_No ='" & TxtAuctionNo.Text & "' where Tran_No ='" & Tran_No & "' and Tr_Date ='" & Format(CDate(Session("TransDate")), "MM/dd/yyyy") & "'", Connection.SQLSource.STOREDPROCEDURE, False)
                M_DBConn.ExecuteScalar("Update GroupDeposit_Auction_M_Tbl  set Tran_No ='" & Tran_No & "'  where Auction_ID='" & M_AuctionID & "'", Connection.SQLSource.STOREDPROCEDURE, False)
                M_DBConn = Nothing


                Response.Redirect("~/NewAccount.aspx?AccNo=" & "GENERAL VOUCHER" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & "GROUP DEPOSIT" & "&DRCR=C" & "&TRANCATG=" & "TRAN")
                'Response.Redirect("~/NewAccount.aspx?AccNo=" & "GENERAL VOUCHER" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GEN ERAL" & "&DRCR=C" & "&TRANCATG=" & "TRAN")
            Else
                ShowAlertMessage("FAILED")
            End If

            Session("ChittyAuctionTbl") = Nothing
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

End Class