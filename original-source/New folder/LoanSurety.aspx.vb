
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class LoanSurety
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1034
    Shared Obj_UserRights As Object
    Public AccHolderTbl As New DataTable("Table")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                LoadDefaultValues()
                Set_Grid()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadDefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Branch(CmbSurBranch)
            M_CommonBL.GetSchemes(CmbSurMemType, Session("TransDate"), ("SHARE"))
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("LOAN"), "GEN.LOAN','OD")

        Catch ex As Exception
        End Try
    End Sub
    Private Sub Set_Grid()
        Try
            DgvCustomer.DataSource = Nothing
            Dim AccHolderTbl As New DataTable("Table")
            AccHolderTbl.Columns.Add("MemDtls", GetType(String))
            AccHolderTbl.Columns.Add("MemBrCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSchCode", GetType(String))
            AccHolderTbl.Columns.Add("MemSch", GetType(String))
            AccHolderTbl.Columns.Add("MemNo", GetType(String))
            AccHolderTbl.Columns.Add("CustID", GetType(String))
            AccHolderTbl.Columns.Add("CustName", GetType(String))
            AccHolderTbl.Columns.Add("Relation", GetType(String))
            AccHolderTbl.Columns.Add("CustAdd", GetType(String))
            AccHolderTbl.Columns.Add("CustType", GetType(String))
            AccHolderTbl.Columns.Add("CustCatg", GetType(String))
            AccHolderTbl.Columns.Add("HouseNm", GetType(String))
            AccHolderTbl.Columns.Add("Place", GetType(String))
            AccHolderTbl.Columns.Add("Dist", GetType(String))
            AccHolderTbl.Columns.Add("State", GetType(String))
            AccHolderTbl.Columns.Add("Country", GetType(String))
            AccHolderTbl.Columns.Add("Pincode", GetType(String))
            AccHolderTbl.Columns.Add("MblNo", GetType(String))
            AccHolderTbl.Columns.Add("LandPh", GetType(String))
            AccHolderTbl.Columns.Add("RelType", GetType(String))
            AccHolderTbl.Columns.Add("Dob", GetType(String))
            AccHolderTbl.Columns.Add("Age", GetType(String))

            AccHolderTbl.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            Session("AccHolderTbl") = AccHolderTbl
            DgvCustomer.DataSource = AccHolderTbl
            DgvCustomer.DataBind()
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
    
    Private Sub Load_CustomerData()
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_CustMasterBL As New CustMasterBL

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(HdnSurCustID.Value), M_PresAddress, M_PermAddress)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        Try
            Dim Address As String
            Address = M_PresAddress.AddAddress1 + "," + M_PresAddress.AddAddress2 + "," + M_PresAddress.AddAddress3 + "," + M_PresAddress.AddPinCode + M_PresAddress.AddDist

            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)

            Dim Row As DataRow
            Row = dt.NewRow
            If Len(HdnSurMemNo.Value.Trim) > 0 Then
                Row("MemDtls") = CmbSurMemType.SelectedItem.Text.Trim + " " + HdnSurMemNo.Value.Trim + " (" + CmbSurBranch.SelectedValue + " )"
            End If
            Row("MemBrCode") = CmbSurBranch.SelectedValue
            Row("MemSchCode") = CmbSurMemType.SelectedValue
            Row("MemSch") = CmbSurMemType.SelectedValue
            Row("MemNo") = HdnSurMemNo.Value
            Row("CustID") = HdnSurCustID.Value
            Row("CustName") = M_CustMaster.CustName
            Row("Relation") = ""
            Row("CustAdd") = Address
            Row("CustType") = M_CustMaster.CustTypeCode
            Row("CustCatg") = M_CustMaster.CustCatgCode
            Row("HouseNm") = M_PresAddress.AddAddress1
            Row("Place") = M_PresAddress.AddAddress2
            Row("Dist") = M_PresAddress.AddDist
            Row("State") = M_PresAddress.AddAddress3
            Row("Country") = M_PresAddress.AddCountry
            Row("Pincode") = M_PresAddress.AddPinCode
            Row("MblNo") = M_CustMaster.CustMobile
            Row("LandPh") = M_CustMaster.CustPhone1
            Row("RelType") = M_CustMaster.CustRelTypCode
            If IsDate(M_CustMaster.CustDobInc) = False Then
                Row("Dob") = "01/01/1900"
            Else
                Row("Dob") = M_CustMaster.CustDobInc
            End If
            Row("Age") = Val(M_CustMaster.CustAge)
            Try
                dt.Rows.Add(Row)
                'ClearCustomer()
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try

            Try
                Dim Expression As String = "CustID=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtLoanNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtLoanNo.TextChanged
        Try
            Set_Grid()
            Dim Dt_AcDtlsN As DataTable
            Dim M_CommonBL As New CommonBL
            Dim Dt As New DataTable
            Dim M_LoanApp As New LoanApplicationBL

            If TxtLoanNo.Text.Trim.Length <= 7 Then
                TxtLoanNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtLoanNo.Text, 7)
            End If
            Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtLoanNo.Text, Session("TransDate"))
            If Dt_AcDtlsN.Rows.Count > 0 Then
                If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                    ShowAlertMessage("Account is Closed!")
                Else
                    If CmbScheme.SelectedValue <> Dt_AcDtlsN.Rows(0)("Sch_Code").ToString() Then
                        ShowAlertMessage("Please Enter Correct Loan No")
                        Exit Sub
                    Else
                        CmbScheme.SelectedValue = Dt_AcDtlsN.Rows(0)("Sch_Code").ToString()
                        CmbBranch.SelectedValue = Dt_AcDtlsN.Rows(0)("Br_Code").ToString()
                    End If
                    HdnCustID.Value = Dt_AcDtlsN.Rows(0)("Cust_ID").ToString()
                    TxtName.Text = Dt_AcDtlsN.Rows(0)("Cust_Name").ToString()
                    TxtAddress.Text = Dt_AcDtlsN.Rows(0)("Cust_Address").ToString()
                    HdnAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_id").ToString()
                    TxtMemberno.Text = Dt_AcDtlsN.Rows(0)("CustMemNo").ToString()
                    CheckSureties()

                End If
            Else
                ShowAlertMessage("  Invalid Account No")
                TxtLoanNo.Focus()
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub
    Private Sub CheckSureties()
        'checks()
        Dim M_LoanAppBL As New LoanApplicationBL
        Dim M_Surety As New LoanApplicationBL
        Dim M_CommonBL As New CommonBL
        Dim Msg As String = ""
        Dim Dt As New DataTable
        Dim DS_Security As DataSet
        DS_Security = M_LoanAppBL.Check_Surety(HdnAccid.Value)
        If DS_Security.Tables(1).Rows.Count > 0 Then
            For I = 0 To DS_Security.Tables(1).Rows.Count - 1
                CmbSurBranch.SelectedValue = DS_Security.Tables(1).Rows(I)("Ao_Shr_BrCode").ToString()
                CmbSurMemType.SelectedValue = DS_Security.Tables(1).Rows(I)("Ao_Shr_SchCode").ToString()
                TxtSurMemNo.Text = DS_Security.Tables(1).Rows(I)("Ao_Shr_MemNo").ToString()
                'Call ImgBtnAddLand_Click(Me.TxtSurMemNo, System.EventArgs.Empty, DS_Security.Tables(1).Rows(I)("Ao_Shr_MemNo").ToString())
                Dim M_MemNo As String = TxtSurMemNo.Text
                HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
                If Len(M_MemNo.Trim) > 0 Then
                    HdnSurMemNo.Value = M_MemNo
                End If
                Load_CustomerData()
                TxtSurMemNo.Text = ""
            Next
            Dt = M_Surety.Sureties(TxtLoanNo.Text)
            If Dt.Rows.Count < 2 Then
                ShowAlertMessage("Only One Surety Will Added Please Enter Another!")
            Else
                ShowAlertMessage("Surety Already Added! ")
            End If
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Private Sub clear()
        Set_Grid()
        TxtLoanNo.Text = ""
        TxtMemberno.Text = ""
        TxtName.Text = ""
        TxtAddress.Text = ""
        TxtSurMemNo.Text = ""
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Try
            If DgvCustomer.Rows.Count < 2 Then
                ShowAlertMessage("Please Enter Maximum Two Sureties")
            Else
                Call SaveOtherSuerties()
                ShowAlertMessage("Saved Successfully")
                Call clear()
                '  Response.Redirect("~/LoanApprovalSecurity.aspx")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    
    Private Sub SaveOtherSuerties()
        Try
            Dim M_LoanSecurityBO As New SMARTCORE_BO.LoanAppFormBO
            With M_LoanSecurityBO
                .BrCode = CmbBranch.SelectedValue
                .AccId = HdnAccid.Value
                .SchCode = CmbScheme.SelectedValue
                .LmApplNo = TxtLoanNo.Text
            End With

            Dim Suertiesdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_Suerties As New IO.StringWriter
            Suertiesdt.WriteXml(M_Suerties)
            Dim M_LoanAppBL As New LoanApplicationBL
            'M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, M_Landsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Cropsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Ornamentsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Insurancesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Bondsw.ToString.Replace("DocumentElement", "NewDataSet"), M_Vehiclesw.ToString.Replace("DocumentElement", "NewDataSet"), M_Depositsw.ToString.Replace("DocumentElement", "NewDataSet"))
            M_LoanAppBL.Save_LADetails(M_LoanSecurityBO, "", "", "", "", "", "", "", M_Suerties.ToString.Replace("DocumentElement", "NewDataSet"))
        Catch ex As Exception
            ShowAlertMessage(ex.ToString)
        End Try
    End Sub

    Protected Sub DgvCustomer_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvCustomer.RowDeleting
        Try
            Dim CustCode As String = CInt(DgvCustomer.DataKeys(e.RowIndex).Value)
            DeleteCust(CustCode)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteCust(ByVal CustCode As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            For i = 0 To DgvCustomer.Rows.Count - 1
                If dt.Rows.Item(i).Item(5).ToString = CustCode Then
                    dt.Rows.Item(i).Delete()
                    GoTo otherow
                End If
            Next i
otherow:
            DgvCustomer.DataSource = dt
            DgvCustomer.DataBind()
            Session("AccHolderTbl") = dt


            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
   
    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        clear()
    End Sub

    Protected Sub ImgBtnAddLand_Click(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal AccNo As String = "") Handles ImgBtnAddLand.Click

        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = ""
            Dim Dt_AcDtlsN As DataTable
            Dim M_Common As New LoanApplicationBL
            Dim dt As New DataTable
            TxtSurMemNo.Text = Right("000" + CmbSurBranch.SelectedValue, 3) + Right("000" + CmbSurMemType.SelectedValue, 3) + Right("0000000" + TxtSurMemNo.Text, 7)
            If AccNo.Length > 0 Then
                M_AccNo = AccNo
                TxtSurMemNo.Text = AccNo
            Else
                M_AccNo = TxtSurMemNo.Text
            End If
            dt = M_Common.CheckSur(TxtSurMemNo.Text, CmbScheme.SelectedValue)
            If dt.Rows.Count = 2 Then
                ShowAlertMessage("Already Give Surety Two Times.Exceed Limit!")
                Exit Sub
            Else
                Dt_AcDtlsN = M_CommonBL.GetAccountTranDtls(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, M_AccNo, Session("TransDate"))
                If Dt_AcDtlsN.Rows.Count = 0 Then
                    ShowAlertMessage("Invalid Member No Entered")
                    M_CommonBL = Nothing
                    Exit Sub
                ElseIf Dt_AcDtlsN.Rows.Count > 0 Then
                    If (Dt_AcDtlsN.Rows(0)("Acc_Status").ToString() = "C") Then
                        ShowAlertMessage("Account is Closed!")
                        Exit Sub
                    Else
                        SurMemAccid.Value = Dt_AcDtlsN.Rows(0)("Acc_ID").ToString()
                    End If
                End If
                Dim M_MemNo As String = TxtSurMemNo.Text
                HdnSurCustID.Value = M_CommonBL.GetAccountCustID(CmbSurBranch.SelectedValue, CmbSurMemType.SelectedValue, TxtSurMemNo.Text, M_MemNo)
                If Len(M_MemNo.Trim) > 0 Then
                    HdnSurMemNo.Value = M_MemNo
                End If
                M_CommonBL = Nothing
                If DgvCustomer.Rows.Count = 2 Then
                    ShowAlertMessage("Maximum Two Sureties will Accept!")
                    TxtSurMemNo.Text = ""
                    Exit Sub
                Else
                    Load_CustomerData()
                    TxtSurMemNo.Text = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class