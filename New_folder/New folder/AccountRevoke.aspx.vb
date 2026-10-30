Imports SMARTCORE_BL
Imports SMARTCORE_BO

Public Class AccountRevoke
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable
    Dim FormID As Integer = 1004
    Shared Obj_UserRights As Object



    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT")
        'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN", "OD", , , , , , False)
        TxtIntimationDate.Attributes.Add("readonly", "readonly")
        TxtExpiryDate.Attributes.Add("readonly", "readonly")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)

        Call Set_Grid()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Load_DefaultValues()
            CmbTransactionType.Attributes.Add("readonly", "readonly")
            TxtAmount.Attributes.Add("readonly", "readonly")
            TxtIntimationDate.Attributes.Add("readonly", "readonly")
            TxtExpiryDate.Attributes.Add("readonly", "readonly")
            TxtReason.Attributes.Add("readonly", "readonly")
            Call CmbFreezType_SelectedIndexChanged(Me.CmbFreezType, System.EventArgs.Empty)
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_AccountFreezing As New SMARTCORE_BO.AccountFreezingBO
            With M_AccountFreezing
                .FreezType = CmbFreezType.SelectedIndex
                .RegDate = Session("TransDate")
                .AmIntimationDate = TxtIntimationDate.Text
                .AmExpiryDate = IIf(Len(Trim(TxtExpiryDate.Text)) = 0, "01/01/1900", TxtExpiryDate.Text)

                .RevokeDate = "01/01/1900"

                .AmAmount = Val(TxtAmount.Text)
                .FreezID = 0
                .AccID = Val(AccId.Value)
                .CustID = TxtCID.Text
                .AmReason = TxtReason.Text
                .MakerID = Session("LoginId")
                .CheckerID = Session("LoginId")
                .RevokeUser = Session("LoginId")
                .RevokeReason = TxtRevokeReason.Text.Trim()
                .AmStatus = "C" 'Status Close
                .AmTranType = CmbTransactionType.SelectedValue
                .Authorised = "Y"
                .RevokeDate = Session("TransDate")
            End With

            Dim M_AccountFreezingBL As New SMARTCORE_BL.AccountFreezingBL
            Session("M_EntryMode") = "VIEW"

            Dim Freezedt As DataTable = DirectCast(Session("FreezeTbl"), DataTable)
            Dim M_FreezeSw As New IO.StringWriter
            Freezedt.WriteXml(M_FreezeSw)

            If M_AccountFreezingBL.Save_Data(M_AccountFreezing, Session("M_EntryMode"), M_FreezeSw.ToString.Replace("DocumentElement", "NewDataSet")) > 0 Then
                ShowAlertMessage("FREEZE REVOKE ENRY SAVING COMPLETED")
                Call Clear_Controls()
            Else
                ShowAlertMessage("FREEZE REVOKE ENRY SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Function Load_CustomerData() As Boolean
        Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
        Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
        Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
        Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO
        Dim M_CustMasterBL As New CustMasterBL

        M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCID.Text), M_PresAddress, M_PermAddress, M_PhotoID, M_ADDID)

        HdnAlert.Value = ""
        If M_CustMaster.CustId = 0 Then
            ShowAlertMessage("Invalid Customer, Please Enter Customer ID")
            HdnAlert.Value = "Invalid Customer, Please Enter Customer ID"
            TxtCID.Focus()
            Return False
        End If

        TxtName.Text = M_CustMaster.CustName
        With M_PermAddress
            TxtAddress.Text = .AddAddress1 + vbCrLf + .AddAddress2 + vbCrLf + .AddAddress3 + vbCrLf + .AddPostoffice + vbCrLf + .AddDist + vbCrLf + .AddPinCode
        End With

        M_CustMasterBL = Nothing

        Return True
    End Function

    Protected Sub TxtCID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCID.TextChanged
        Try
            TxtCID.ToolTip = TxtCID.Text
            Clear_Controls()
            TxtCID.Text = TxtCID.ToolTip
            TxtCID.ToolTip = ""

            If Load_CustomerData() = True Then
                Dim M_CommonBL As New CommonBL
                Dim M_AccNo As String = TxtAccNo.Text
                Dim I As Integer

                Dim FreezeTbl As New DataTable("Table")
                FreezeTbl = Session("FreezeTbl")

                FreezeTbl.Clear()

                Dt_AcDtls = M_CommonBL.GetCustAccountDtls(Val(TxtCID.Text), Session("TransDate"))

                HdnAlert.Value = ""
                If Dt_AcDtls.Rows.Count = 0 Then
                    Set_Grid()
                    ShowAlertMessage("No Account Exists For This Customer, Please Enter Customer ID")
                    HdnAlert.Value = "No Account Exists For This Customer, Please Enter Customer ID"
                    TxtCID.Focus()
                    Exit Sub
                End If


                For I = 0 To Dt_AcDtls.Rows.Count - 1
                    Dim Bal As Double = Dt_AcDtls.Rows(I)("Cur_Bal").ToString()
                    Dim BrName As String = Dt_AcDtls.Rows(I)("Br_Name").ToString()
                    Dim SchName As String = Dt_AcDtls.Rows(I)("Sch_Name").ToString()

                    FreezeTbl.Rows.Add(BrName, SchName, Dt_AcDtls.Rows(I)("Acc_No").ToString(), _
                        Format(Val(CStr(Math.Abs(Bal)).ToString), "#0.00") & " " & IIf(Bal < 0, "Dr", "Cr"), CInt(Dt_AcDtls.Rows(I)("Acc_ID").ToString()))

                Next I

                Session("FreezeTbl") = FreezeTbl
                DgvAcFreezing.DataSource = FreezeTbl
                DgvAcFreezing.DataBind()

                Locate_FreezeData()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNo.Text

            TxtAccNo.ToolTip = TxtAccNo.Text
            TxtShortCode.ToolTip = TxtShortCode.Text
            Clear_Controls()
            TxtAccNo.Text = TxtAccNo.ToolTip
            TxtAccNo.ToolTip = ""
            TxtShortCode.Text = TxtShortCode.ToolTip
            TxtShortCode.ToolTip = ""

            HdnAlert.Value = ""
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                AccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                TxtCID.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = TxtCID.Text
                TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()

                TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()

                Dim Bal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                Dim BrName As String = Dt_AcDtls.Rows(0)("Br_Name").ToString()
                Dim SchName As String = Dt_AcDtls.Rows(0)("Sch_Name").ToString()

                Dim FreezeTbl As New DataTable("Table")
                FreezeTbl = Session("FreezeTbl")

                FreezeTbl.Clear()

                FreezeTbl.Rows.Add(BrName, SchName, TxtAccNo.Text, _
                    Format(Val(CStr(Math.Abs(Bal)).ToString), "#0.00") & " " & IIf(Bal < 0, "Dr", "Cr"), CInt(AccId.Value))
                Session("FreezeTbl") = FreezeTbl
                DgvAcFreezing.DataSource = FreezeTbl
                DgvAcFreezing.DataBind()
                Locate_FreezeData()
            Else
                ShowAlertMessage("Invalid Account No, Please Check")
                HdnAlert.Value = "Invalid Account No, Please Check"
                TxtShortCode.Focus()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Locate_FreezeData()
        Dim M_FreezeBL As New AccountFreezingBL
        Dim M_FreezeBO As New AccountFreezingBO
        M_FreezeBO = M_FreezeBL.Locate_Data(CmbFreezType.SelectedIndex, IIf(CmbFreezType.SelectedIndex = 0, Val(TxtCID.Text), Val(AccId.Value)))

        With M_FreezeBO
            If .FreezID = 0 Then
                ShowAlertMessage("No Freezed Record Exists, Please Check")
                HdnAlert.Value = "No Freezed Record Exists, Please Check"
                CmbFreezType.Focus()
            Else
                TxtAmount.Text = .AmAmount
                TxtIntimationDate.Text = .AmIntimationDate
                TxtExpiryDate.Text = IIf(.AmExpiryDate <> "01/01/1900", .AmExpiryDate, "")
                TxtReason.Text = .AmReason
                CmbTransactionType.SelectedIndex = IIf(.AmTranType = "B", 0, IIf(.AmTranType = "D", 1, 2))
            End If
        End With
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Sub Clear_Controls()
        TxtAccNo.Text = ""
        TxtShortCode.Text = ""
        TxtCID.Text = ""
        TxtName.Text = ""
        TxtAddress.Text = ""
        TxtAmount.Text = ""
        TxtAmount.Text = ""
        TxtExpiryDate.Text = ""
        TxtIntimationDate.Text = ""
        CmbTransactionType.SelectedIndex = 0
        TxtReason.Text = ""
        TxtRevokeReason.Text = ""
        HdnAlert.Value = "Please Enter Values"
        Call Set_Grid()
        Call Set_SchemeCtrl()
    End Sub

    Private Sub Set_Grid()
        DgvAcFreezing.DataSource = Nothing
        Dim FreezeTbl As New DataTable("Table")
        FreezeTbl.Columns.Add("Br_Name", GetType(String))
        FreezeTbl.Columns.Add("Sch_Name", GetType(String))
        FreezeTbl.Columns.Add("AccountNo", GetType(String))
        FreezeTbl.Columns.Add("Balance", GetType(String))
        FreezeTbl.Columns.Add("AccID", GetType(Double))
        FreezeTbl.Rows.Add("", "", "", 0, 0)
        Session("FreezeTbl") = FreezeTbl
        DgvAcFreezing.DataSource = FreezeTbl
        DgvAcFreezing.DataBind()

    End Sub

    Protected Sub CmbFreezType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbFreezType.SelectedIndexChanged
        Clear_Controls()
        If CmbFreezType.SelectedValue = "Account" Then
            account.Visible = True
            TxtCID.Enabled = False
            TxtName.Enabled = False
            TxtAddress.Enabled = False
        Else
            account.Visible = False
            TxtCID.Enabled = True
            TxtName.Enabled = True
            TxtAddress.Enabled = True
        End If
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub

    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class