Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data

Public Class Remainder

    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Dim FormID As Integer = 1121
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

            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Load_DefaultValues()
        End If

        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "CUSTOMER" Then
                TxtCid.Text = Session("SearchResult")
                'Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
                'Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
                'Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO

                'Dim M_CustMasterBL As New CustMasterBL
                'M_CustMaster = M_CustMasterBL.Locate_Data(TxtCid.Text, M_PresAddress, M_PermAddress)
                'Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
                'M_CustMasterBL = Nothing
                TxtCid_TextChanged(Me.TxtCid, System.EventArgs.Empty)
                TxtCid.Focus()
                Session("SearchResult") = ""
            ElseIf Session("SearchType") = "ACCOUNTNO" Then
                TxtAccNo.Text = Session("SearchResult")
                TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If

    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_User(CmbUser)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))


        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbRemndrTo_SelectedIndexChanged(Me.CmbRemndrTo, System.EventArgs.Empty)
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        'TxtUserId_TextChanged(Me.TxtUserId, System.EventArgs.Empty)
    End Sub
    Private Sub Clear_Controls()
        TxtAccNo.Text = ""
        'TxtBranch.Text = ""
        TxtAcHoldrAddr.Text = ""
        TxtAcHoldrName.Text = ""
        TxtCid.Text = ""
        TxtCustAddress.Text = ""
        TxtCustName.Text = ""
        TxtDetails.Text = ""
        'TxtScheme.Text = ""
        TxtSubject.Text = ""
        TxtUserId.Text = ""
        TxtUserName.Text = ""
        HdnCustID.Value = ""
        TxtUserAddress.Text = ""


    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue

    End Sub


    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim RemainderToId As String
        Dim M_StID As Integer

        Try



            If CmbRemndrTo.SelectedIndex = 0 Then
                RemainderToId = HdnCustID.Value
            ElseIf CmbRemndrTo.SelectedIndex = 1 Then
                RemainderToId = CmbUser.SelectedValue
            Else
                RemainderToId = TxtCid.Text
            End If


            Dim M_Remainder As New SMARTCORE_BO.RemainderBO
            With M_Remainder
                .ReminderId = 0
                .ReminderToID = RemainderToId
                .RegisterDate = Session("TransDate")
                .RemainderDate = Session("TransDate")
                .AcknowledgedDatetime = "01/01/1900"
                .ReminderTo = CmbRemndrTo.SelectedValue
                .RemainderTitle = TxtSubject.Text
                .RemainderContent = TxtDetails.Text
                .AssignedBy = ""
                .Priority = "L"
                .AcknoledgementRequired = "Y"
                .Acknowledged = "N"
                .Status = "O"
            End With


            Dim M_RemainderBL As New RemainderBL

            Session("M_EntryMode") = "NEW"
            M_StID = M_RemainderBL.Save_Data(M_Remainder, Session("M_EntryMode"))
            If (M_StID > 0) Then
                ShowAlertMessage("Remainder Successfully Saved")
                Clear_Controls()
            Else
                ShowAlertMessage(" SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbRemndrTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbRemndrTo.SelectedIndexChanged

        If CmbRemndrTo.SelectedItem.Text = "Account" Then
            DivCustomer.Visible = False
            DivUser.Visible = False
            DivAccountNo.Visible = True
            Session("SearchType") = "ACCOUNTNO"
        ElseIf CmbRemndrTo.SelectedItem.Text = "Customer" Then
            DivCustomer.Visible = True
            DivUser.Visible = False
            DivAccountNo.Visible = False
            Session("SearchType") = "CUSTOMER"

        ElseIf CmbRemndrTo.SelectedItem.Text = "User" Then
            DivCustomer.Visible = False
            DivUser.Visible = True
            DivAccountNo.Visible = False
            Call CmbUser_SelectedIndexChanged(Me.CmbUser, System.EventArgs.Empty)


        End If

    End Sub

    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click

    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL

        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            Dim M_AccNo As String = TxtAccNo.Text
        

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                HdnCustID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                Session("CustomerId") = TxtCid.Text
                TxtAcHoldrName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()

                TxtAcHoldrAddr.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                'Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                'Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
                'TxtCurBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
                'TxtShadowBal.Text = Format(Val(CStr(Math.Abs(ShadowBal))), "#0.00") & " " & IIf(ShadowBal < 0, "Dr", "Cr")
                'TxtPendingDr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()), "#0.00")
                'TxtPendingCr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()), "#0.00")
                'txt.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                'For Loading Initial Values
                'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

            Else

                ShowAlertMessage("INVALID ACCOUNT NO")
            End If

            M_CommonBL = Nothing
            If Len(HdnCustID.Value) > 0 Then TxtAccNo.Focus()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub

    Protected Sub TxtCid_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCid.TextChanged
        Load_CustomerData()
    End Sub

    Private Sub Load_CustomerData()
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_CustMasterBL As New CustMasterBL

        M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCid.Text), M_PresAddress, M_PermAddress)
        Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
        M_CustMasterBL = Nothing

    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next
        If M_CustMaster.CustId = 0 Then
            ShowAlertMessage("Not a Valid Customer ID")
            Exit Sub
        End If

        With M_CustMaster
            TxtCustName.Text = .CustName
        End With

        TxtCustAddress.Text = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode

    End Sub



    Protected Sub TxtUserId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtUserId.TextChanged
        UserDetails()
    End Sub


    Private Sub UserDetails()
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_CustMasterBL As New CustMasterBL

        M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtUserId.Text), M_PresAddress, M_PermAddress)

        HiddenField2.Value = ""
        If M_CustMaster.CustId = 0 Then
            ShowAlertMessage("Invalid Customer, Please Enter Customer ID")
            HiddenField2.Value = "Invalid Customer, Please Enter Customer ID"
            TxtCid.Focus()
        End If

        TxtUserName.Text = M_CustMaster.CustName
        With M_PermAddress
            TxtUserAddress.Text = .AddAddress1 + vbCrLf + .AddAddress2 + vbCrLf + .AddAddress3 + vbCrLf + .AddPostoffice + vbCrLf + .AddDist + vbCrLf + .AddPinCode
        End With

        M_CustMasterBL = Nothing
    End Sub

    Protected Sub CmbUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbUser.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        Dim M_AccNo As String = TxtAccNo.Text
        Dt_AcDtls = M_CommonBL.GetUserDetails(CmbUser.SelectedValue)
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnCustID.Value = Dt_AcDtls.Rows(0)("Cust_Id").ToString()
            TxtUserId.Text = Dt_AcDtls.Rows(0)("Cust_Id").ToString()
            Session("CustomerId") = HdnCustID.Value
            TxtUserName.Text = Dt_AcDtls.Rows(0)("Emp_Name").ToString()

            TxtUserAddress.Text = Dt_AcDtls.Rows(0)("Emp_Address").ToString()
            'Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
            'Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
            'TxtCurBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
            'TxtShadowBal.Text = Format(Val(CStr(Math.Abs(ShadowBal))), "#0.00") & " " & IIf(ShadowBal < 0, "Dr", "Cr")
            'TxtPendingDr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()), "#0.00")
            'TxtPendingCr.Text = Format(Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()), "#0.00")
            'txt.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
            'For Loading Initial Values
            'Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

        Else

            ' ShowAlertMessage("INVALID ACCOUNT NO")
        End If

        M_CommonBL = Nothing
        ' If Len(HdnCustID.Value) > 0 Then TxtAccNo.Focus()


    End Sub
End Class