Imports SMARTCORE_BL
Imports SMARTCORE_BO

Public Class AccountFreezing
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable
    Dim FormID As Integer = 1002

    Shared Obj_UserRights As Object


 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Load_DefaultValues()
            Call CmbFreezType_SelectedIndexChanged(Me.CmbFreezType, System.EventArgs.Empty)
        End If
    End Sub
    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT")
            'M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN", "OD", , , , , , False)
            TxtIntimationDate.Attributes.Add("readonly", "readonly")
            TxtExpiryDate.Attributes.Add("readonly", "readonly")
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)

            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)

            HdnTrDate.Value = Session("TransDate")

            Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Set_Grid()
        Try
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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
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
                .RevokeUser = ""
                .RevokeReason = ""
                .AmStatus = "O" 'Status Open
                .AmTranType = CmbTransactionType.SelectedValue
                .Authorised = "Y"
            End With

            Dim M_AccountFreezingBL As New SMARTCORE_BL.AccountFreezingBL
            Session("M_EntryMode") = "NEW"

            Dim Freezedt As DataTable = DirectCast(Session("FreezeTbl"), DataTable)
            Dim M_FreezeSw As New IO.StringWriter
            Freezedt.WriteXml(M_FreezeSw)

            If M_AccountFreezingBL.Save_Data(M_AccountFreezing, Session("M_EntryMode"), M_FreezeSw.ToString.Replace("DocumentElement", "NewDataSet")) > 0 Then
                ShowAlertMessage("FREEZE ENTRY SAVING COMPLETED")
                Call Clear_Controls()
            Else
                ShowAlertMessage("FREEZE ENTRY SAVING FAILED")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

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

                    Dim PendingDrAmt As Double = Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()
                    Dim PendingCrAmt As Double = Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()

                    If PendingDrAmt + PendingCrAmt > 0 Then
                        HdnAlert.Value = "UnAuthorised Entry Exists, Please Check"
                        ShowAlertMessage(HdnAlert.Value)
                        TxtShortCode.Focus()
                    End If

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


    Private Function Load_CustomerData() As Boolean
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_CustMasterBL As New CustMasterBL
            Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
            Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO
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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
        Return True
    End Function
    


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

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
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

                Dim PendingDrAmt As Double = Dt_AcDtls.Rows(0)("Pending_DrAmt").ToString()
                Dim PendingCrAmt As Double = Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()

                If PendingDrAmt + PendingCrAmt > 0 Then
                    HdnAlert.Value = "UnAuthorised Entry Exists, Please Check"
                    ShowAlertMessage(HdnAlert.Value)
                    TxtShortCode.Focus()
                End If

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
                HdnAlert.Value = "Invalid Account No, Please Check"
                ShowAlertMessage(HdnAlert.Value)
                TxtShortCode.Focus()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear_Controls()
        Try
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
            HdnAlert.Value = "Please Enter Values"
            Call Set_Grid()
            Call Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Controls()

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Private Sub Locate_FreezeData()
        Try
            Dim M_FreezeBL As New AccountFreezingBL
            Dim M_FreezeBO As New AccountFreezingBO
            M_FreezeBO = M_FreezeBL.Locate_Data(CmbFreezType.SelectedIndex, IIf(CmbFreezType.SelectedIndex = 0, Val(TxtCID.Text), Val(AccId.Value)))

            With M_FreezeBO
                If .FreezID > 0 Then ' 
                    If .RevokeReason.Length = 0 Then
                        ShowAlertMessage("Already Freezed Record Exists, Please Check")
                        HdnAlert.Value = "Already Freezed Record Exists, Please Check"
                        CmbFreezType.Focus()
                    End If
                End If
            End With
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class