Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data.SqlClient

Partial Public Class TicketMaster
    Inherits System.Web.UI.Page
    Public M_TicketId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            Session("M_EntryMode") = "NEW"
            'hid_CustID.Value = "NEW"

            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

            Load_DefaultValues()

            txtDepDate.Text = DateTime.Now.Date
            'txtRetDate.Text = DateTime.Now.Date
            txtRetDate.Enabled = False

        End If
        If Session("SearchResult") <> "" Then
            If (TxtName.Text <> "" And TxtCustID.Text = "NEW") Then
                If Session("SearchResult") <> "NEW" Then
                    'TxtIntroID.Text = Session("SearchResult").ToString()
                    'Call TxtIntroID_TextChanged(TxtIntroID, System.EventArgs.Empty)
                End If
                Session("SearchResult") = "NEW"
            ElseIf (TxtName.Text <> "" And TxtCustID.Text <> "NEW") Then
                'If TxtIntroID.Text.Length = 0 Then
                '    TxtIntroID.Text = Session("SearchResult").ToString()
                '    Call TxtIntroID_TextChanged(TxtIntroID, System.EventArgs.Empty)
                '    Session("SearchResult") = "NEW"
                'End If
            Else
                TxtCustID.Text = Session("SearchResult")
                TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                TxtCustID.Focus()
                Session("SearchResult") = ""
            End If
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click ''gowtham

        Try
            If Validate_Control() = False Then
                Exit Sub
            End If
            Dim M_Data As New DataTable
            Dim m_TicketMasterBL As New TicketMasterBL
            Dim M_TicketMaster As New SMARTCORE_BO.TicketMasterBO
            Dim TicktHub As String = ""
            M_Data = m_TicketMasterBL.GetTicketHub()
            If M_Data.Rows.Count > 0 Then
                TicktHub = M_Data.Rows(0)("Acc_Code").ToString()
            End If
            ' If Len(M_TicketId) > 0 Then

            Dim Trandt As DataTable = DirectCast(Session("locAcHeadTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable
            Dim M_BatchNo As String = ""
            Dim Tran_No As String = ""
            Dim IsIB As String = "N"
            Trandt.WriteXml(M_TranSw)
          

            M_Dt = M_CommonBL.Update_AccHeadTran(TicktHub, Session("TransDate"), Session("DefaultBrCode"), TxtRemarks.Text, Session("LoginID"), M_TranSw.ToString, "", "TCK", M_BatchNo)
            If M_Dt.Rows.Count > 0 Then
                Tran_No = M_Dt.Rows(0)("Tran_No").ToString()




                With M_TicketMaster


                    .TicketId = 0
                    If TxtCustID.Text = "NEW" Then
                        .CustID = 0
                    Else
                        .CustID = Val(TxtCustID.Text)
                    End If

                    .CustNAME = TxtName.Text
                    .CustAddress1 = TxtAdd1.Text
                    .CustAddress2 = TxtAdd2.Text
                    .CustAddress3 = TxtAdd3.Text
                    .CustPhone = TxtPhoneNo.Text
                    .CustPassport = TxtPassportNo.Text

                    .TravalType = CmbTravelType.SelectedValue
                    .AirlineName = txtNameofAirline.Text
                    .Gender = cmbGender.SelectedValue
                    .TypeofTraval = cmbTravelsection.SelectedValue
                    .TravalAgency = CmbTravlAgncy.SelectedValue
                    .Fromplace = txtFrom.Text
                    .Toplace = txtTo.Text.ToString()
                    .FromDate = txtDepDate.Text
                    If txtRetDate.Text = "" Then
                        .ToDate = "01/01/1900"
                    Else
                        .ToDate = txtRetDate.Text
                    End If

                    ''.ToDate = txtRetDate.Text
                    .NoPassanger = Val(txtNoofPassanger.Text)
                    .Addult = Val(txtAdult.Text)
                    .Children = Val(txtchilfrens.Text)
                    .Infants = Val(txtInfants.Text)
                    .ClassTravel = cmbClassoftravel.SelectedValue.ToString()
                    .PerTicket = Convert.ToDecimal(txtPerticket.Text)
                    .Tax = Convert.ToDecimal(txtTax.Text)
                    .Totalamount = Convert.ToDecimal(txtTotAmt.Text)
                    .Commission = Convert.ToDecimal(txtCommAmt.Text)
                    .TranNo = Tran_No

                End With


                M_TicketId = m_TicketMasterBL.Save_Data(M_TicketMaster)

                ShowAlertMessage("Successfully Saved,  Ticket ID : " & M_TicketId)
                ''Call Clear_Controls()
                M_TicketMaster = Nothing

                ' ShowAlertMessage("UPDATED")
                Response.Redirect("~/NewAccount.aspx?AccNo=" & "GENERAL VOUCHER" & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=" & "C" & "&TRANCATG=" & "TRAN")
            Else
                ShowAlertMessage("FAILED")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try




    End Sub   ''gowtham

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Private Sub Set_TranAmt()

        Try
            ' If Val(AccID.Value) = 0 And CmbTransType.SelectedIndex = 0 Then Exit Sub
            'If CmbTransType.SelectedIndex <> 0 Then Exit Sub ' Blocking For General Voucher
            'AlertDt = DirectCast(Session("AlertDt"), DataTable)
            'AlertDt.Clear()

            Dim Sql As String = ""
            Dim dt As New DataTable("Table")
            Dim dtAlert As New DataTable
            Dim ds As DataSet
            Dim TranType As String
            DivTranGrid.Visible = True
           

            Dim SchModule As String

            If DdlTranType.SelectedValue = "CASH" Then
                TranType = "C"
            Else
                TranType = "T"
            End If




            SchModule = "TICKET" 'SchModuleCatg.Value




            Select Case SchModule
            
                Case "TICKET"
                    Dim M_TicktMstrBL As New TicketMasterBL
                    ds = M_TicktMstrBL.Set_TicketAmt(Session("TransDate"), Val(txtTotAmt.Text), TranType, Session("DefaultBrCode"))
                    If ds.Tables.Count >= 1 Then dt = ds.Tables(0)
                    If ds.Tables.Count >= 2 Then dtAlert = ds.Tables(1)
                    CmdSave.Enabled = True
                    M_TicktMstrBL = Nothing
                    Session("locAcHeadTbl") = dt
                    TxtRemarks.Text = "Ticket"

            End Select

            DgvTransaction.DataSource = dt
            DgvTransaction.DataBind()

            'If CmbSchTrans.SelectedItem.Text <> "Renewal" Then
            '    DgvTransaction.DataSource = dt
            '    DgvTransaction.DataBind()

            '    If SchModuleCatg.Value = "GROUP DEPOSIT" Then
            '        If ds.Tables.Count >= 3 And CmbSchTrans.SelectedIndex = 0 Then dt = ds.Tables(2)
            '    End If
            '    Session("locAcHeadTbl") = dt

            '    Call Calc_TranTotal()
            'End If

            'If dtAlert.Rows.Count > 0 Then
            '    If dtAlert.Rows(0)("AlertMsg").ToString.Length > 0 Then
            '        Dim AlertStr As String
            '        AlertStr = dtAlert.Rows(0)("AlertMsg").ToString
            '        ShowAlertMessage(AlertStr)
            '        If dtAlert.Rows(0)("To_Proceed").ToString = "N" Then
            '            AlertDt.Rows.Add("M", AlertStr)
            '        End If
            '    End If

            '    If dtAlert.Rows(0)("Narrarion").ToString.Length > 0 Then
            '        TxtRemarks.Text = dtAlert.Rows(0)("Narrarion").ToString
            '        Call TxtMemberNo_TextChanged(Me.TxtMemberNo, System.EventArgs.Empty)
            '    Else
            '        TxtRemarks.Text = ""
            '    End If
            '    ' Assinging FD Interest * other closure Amts
            '    If CDbl(dtAlert.Rows(0)("TranAmt").ToString) > 0 And CmbSchTrans.SelectedItem.Text <> "Renewal" Then
            '        TxtTransAmt.Text = Format(CDbl(dtAlert.Rows(0)("TranAmt").ToString), "#0.00")
            '    End If

            '    If dtAlert.Rows(0)("IsChkIssue").ToString.ToUpper = "Y" Then
            '        Lbl_Status.Visible = True
            '    Else
            '        Lbl_Status.Visible = False
            '    End If
            'End If

            '' Enable / Diable Save Button
            'If SchModuleCatg.Value <> "LOCKER" Then
            '    If Val(TxtTransAmt.Text) > 0 Then
            '        Dim M_CommonCls As New CommonClass
            '        LblWords.Text = M_CommonCls.Towords(Val(TxtTransAmt.Text)).ToUpper
            '        M_CommonCls = Nothing
            '        TxtTransAmt.Text = Format(Val(TxtTransAmt.Text), "#0.00")
            '        CmdSave.Enabled = True
            '    End If
            'Else
            '    CmdSave.Enabled = True
            'End If


            'Session("AlertDt") = AlertDt
            'Call AddValidateMsg()

        Catch ex As Exception
            ShowAlertMessage(Err.Description)

        End Try
    End Sub
    Private Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCustID.TextChanged
        Dim CustId As String = TxtCustID.Text
        ' Call Clear_Controls()


        TxtCustID.Text = CustId
        If TxtCustID.Text.Length = 0 Then
            TxtCustID.Text = "NEW"
            Session("M_EntryMode") = "NEW"
            ' hid_CustID.Value = "NEW"
        Else
            Try


                Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
                Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
                Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
                Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
                Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
                Dim M_CustMasterBL As New CustMasterBL




                M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
                If M_CustMaster.CustId > 0 Then

                    Dim M_Dt As New DataTable
                    Dim M_CommonBL As New CommonBL
                    M_Dt = M_CommonBL.LocationDtls(CustId)
                    If (M_Dt.Rows.Count > 0) Then
                        'CmbDist.SelectedValue = M_Dt.Rows(0).Item("District").ToString()
                        'Call CmbDist_SelectedIndexChanged(sender, e)
                    End If


                    Session("ImgSignature") = ""
                    Session("ImgDocument") = ""
                    Session("Imgphoto") = ""

                    Locate_Data(M_CustMaster, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
                    ' TxtIntroNm.Text = M_CustMasterBL.Get_CustomerName(IIf(TxtIntroID.Text = "", 0, TxtIntroID.Text))
                    If M_CustMaster.IsCompleted = "Y" Then
                        '  DivModification.Visible = True
                    End If
                End If
                M_CustMasterBL = Nothing
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Locate_Data(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO, Optional ByVal M_PhotoID As SMARTCORE_BO.CustIdentityBO = Nothing, Optional ByVal M_ADDID As SMARTCORE_BO.CustIdentityBO = Nothing)
        On Error Resume Next

        If M_CustMaster.CustId = 0 Then
            Session("M_EntryMode") = "NEW"
            TxtCustID.Text = "NEW"
            'hid_CustID.Value = "NEW"
            Exit Sub
        End If

        Dim M_Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        M_Dt = M_CommonBL.LocationDtls(TxtCustID.Text)

        With M_CustMaster
            Session("CustomerId") = M_CustMaster.CustId
       
        
            TxtName.Text = .CustName
           
            'TxtEmail.Text = .CustEmail
            TxtPhoneNo.Text = .CustPhone1
         

        End With
        TxtAdd1.Text = M_PresAddress.AddAddress1
        TxtAdd2.Text = M_PresAddress.AddPostoffice
        'TxtPresState.Text = M_PresAddress.AddAddress3
        TxtAdd3.Text = M_PresAddress.AddAddress2
        'TxtPresPin.Text = M_PresAddress.AddPinCode
        'TxtPresHouseNo.Text = M_PresAddress.AddHouseNo
        'TxtPresDist.Text = M_PresAddress.AddDist
        'TxtPresCountry.Text = M_PresAddress.AddCountry
        'txtPresvillage.Text = M_PresAddress.Village
        'TxtPrestaluk.Text = M_PresAddress.Taluk
        'TxtPermHouseName.Text = M_PermAddress.AddAddress1
        'TxtPermPlace.Text = M_PermAddress.AddPostoffice
        'TxtPermState.Text = M_PermAddress.AddAddress3
        'TxtPermLand.Text = M_PermAddress.AddAddress2
        'TxtPermPin.Text = M_PermAddress.AddPinCode
        'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
        'TxtPermDist.Text = M_PresAddress.AddDist
        'TxtPermCountry.Text = M_PresAddress.AddCountry
        'TxtPermtaluk.Text = M_PermAddress.Taluk
        'TxtPermvillage.Text = M_PermAddress.Village



        'CmbPhotoIDProof.SelectedValue = M_PhotoID.CidntCode
      
        'TxtAddProofNo.Text = M_ADDID.CidntNo
        'TxtAddIDProof.Text = M_ADDID.CidntNo
        Session("M_EntryMode") = "VIEW"
        'hid_CustID.Value = "VIEW"

        Exit Sub



    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Sub Clear_Controls()

        TxtCustID.Text = "NEW"
        TxtName.Text = ""
        TxtAdd1.Text = ""
        TxtAdd2.Text = ""
        TxtAdd3.Text = ""
        TxtPhoneNo.Text = ""
        TxtPassportNo.Text = ""


        txtNameofAirline.Text = ""
        cmbGender.SelectedIndex = 0

        CmbTravlAgncy.SelectedIndex = 0
        txtFrom.Text = ""
        txtTo.Text = ""
        
        txtNoofPassanger.Text = ""
        txtAdult.Text = "0"
        txtchilfrens.Text = "0"
        txtInfants.Text = "0"

        txtPerticket.Text = ""
        txtTax.Text = ""
        txtTotAmt.Text = ""
        txtCommAmt.Text = ""

        txtDepDate.Text = DateTime.Now.Date
        txtRetDate.Text = ""
        cmbTravelsection.SelectedIndex = 0
        txtRetDate.Enabled = False
      
 
    End Sub
    Public Shared Sub ErrorMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alertify.error('" & [error] & "');", True)
        End If
    End Sub

    Private Function Validate_Control() As Boolean
        If Len(Trim(TxtName.Text)) = 0 Then
            TxtName.Focus()
            ErrorMessage("INVALID CUSTOMER NAME ENTERED")
            Return False
        End If
   
        If Len(Trim(TxtAdd1.Text)) = 0 Then
            TxtAdd1.Focus()
            ErrorMessage("INVALID CUSTOMER ADDRESS ")
            Return False
        End If
        'If Len(Trim(TxtAdd2.Text)) = 0 Then
        '    TxtAdd2.Focus()
        '    ErrorMessage("INVALID CUSTOMER ADDRESS ")
        '    Return False
        'End If
        'If Len(Trim(TxtAdd3.Text)) = 0 Then
        '    TxtAdd3.Focus()
        '    ErrorMessage("INVALID CUSTOMER ADDRESS ")
        '    Return False
        'End If
        If Len(Trim(TxtPhoneNo.Text)) = 0 Then
            TxtPhoneNo.Focus()
            ErrorMessage("INVALID PHONE NO")
            Return False
        End If
        'If Len(Trim(TxtPassportNo.Text)) = 0 Then
        '    TxtPassportNo.Focus()
        '    ErrorMessage("INVALID PASSPORT NO")
        '    Return False
        'End If
        'If Len(Trim(txtNameofAirline.Text)) = 0 Then
        '    txtNameofAirline.Focus()
        '    ErrorMessage("INVALID NAME OF AIRLINE")
        '    Return False
        'End If

        'If Len(Trim(txtTravelAgency.Text)) = 0 Then
        '    txtTravelAgency.Focus()
        '    ErrorMessage("INVALID TRAVAL AGENCY")
        '    Return False
        'End If
        If Len(Trim(txtFrom.Text)) = 0 Then
            txtFrom.Focus()
            ErrorMessage("INVALID FROM PLACE")
            Return False
        End If
        If Len(Trim(txtTo.Text)) = 0 Then
            txtTo.Focus()
            ErrorMessage("INVALID TO PLACE")
            Return False
        End If

     

        If Len(Trim(txtNoofPassanger.Text)) = 0 Then
            txtNoofPassanger.Focus()
            ErrorMessage("INVALID No of Passengers")
            Return False
        End If

        If Len(Trim(txtPerticket.Text)) = 0 Then
            txtPerticket.Focus()
            ErrorMessage("INVALID PER TICKET PRICE")
            Return False
        End If
   
        If Len(Trim(txtTax.Text)) = 0 Then
            txtTax.Focus()
            ErrorMessage("INVALID TAX")
            Return False
        End If
        If Len(Trim(txtTotAmt.Text)) = 0 Then
            txtTotAmt.Focus()
            ErrorMessage("INVALID TOTAL AMOUNT")
            Return False
        End If
        If Len(Trim(txtCommAmt.Text)) = 0 Then
            txtCommAmt.Focus()
            ErrorMessage("INVALID COMMISSION AMOUNT")
            Return False
        End If
    

        Return True
    End Function

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL

            M_CommonBL.Fill_Pickup(cmbClassoftravel, 106, 0)
            M_CommonBL.Fill_Pickup(cmbTravelsection, 105, 0)
            M_CommonBL.Fill_Pickup(CmbTravelType, 104, 0)
            M_CommonBL.Fill_Pickup(cmbGender, 8, 0)
            M_CommonBL.Fill_Pickup(CmbTravlAgncy, 107, 0)
            DdlTranType.Items.Add("CASH")
            DdlTranType.Items.Add("TRANSFER")

         
       
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   

    Private Sub cmbTravelsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTravelsection.SelectedIndexChanged

        txtRetDate.Text = ""
        txtRetDate.Enabled = False
        If cmbTravelsection.SelectedItem.Text.ToString() = "Round Trip" Then
            txtRetDate.Enabled = True
            txtRetDate.Text = DateTime.Now.Date
        End If
    End Sub

    Protected Sub butClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles butClear.Click
        Clear_Controls()
    End Sub

 

    Protected Sub txtTotAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTotAmt.TextChanged
        Set_TranAmt()
    End Sub

    Protected Sub DdlTranType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlTranType.SelectedIndexChanged
        txtTotAmt_TextChanged(Me.txtTotAmt, System.EventArgs.Empty)
    End Sub
End Class