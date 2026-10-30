Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports System.IO
Imports System.Web.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Public Class ShareTransfer
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1213
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                ''USer Rights
                'Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If
                Load_DefaultValues()


                Session("SearchType") = "CUSTOMERREGISTER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx?SrType=CUSTOMER1','MyWindow','height=300,width=500,scrollbars=yes');return false;")
                Imgcust.Attributes.Add("OnClick", "window.open('Srch.aspx?SrType=CUSTOMER2','MyWindow','height=300,width=500,scrollbars=yes');return false;")
                ImgJointcust.Attributes.Add("OnClick", "window.open('Srch.aspx?SrType=CUSTOMER3','MyWindow','height=300,width=500,scrollbars=yes');return false;")

                TxtCustID.Focus()
                TxtTranCustId.Focus()

                If Len(Request.QueryString("TRANID")) > 0 Then
                    GetAuthoriseDtls(Request.QueryString("TRANID"))
                    Pnlauthorise1.Enabled = False
                    PnlAuthorise2.Enabled = False
                End If


                'CmbAdmBranch.Attributes.Add("readonly", "readonly")
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType_1") = "CUSTOMER2" Then
                    TxtTranCustId.Text = Session("SearchResult")
                    TxtTranCustId_TextChanged(Me.TxtTranCustId, System.EventArgs.Empty)
                    Session("SearchResult") = ""
                End If
                If Session("SearchType_1") = "CUSTOMER1" Then
                    TxtCustID.Text = Session("SearchResult")
                    TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
                    Session("SearchResult") = ""
                End If
                If Session("SearchType_1") = "CUSTOMER3" Then
                    Txt_JointCustId.Text = Session("SearchResult")
                    Txt_JointCustId_TextChanged(Me.Txt_JointCustId, System.EventArgs.Empty)
                    Session("SearchResult") = ""
                End If
            End If

           


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub GetAuthoriseDtls(ByVal Tran_No As String)
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Ds As New DataSet
            M_Ds = M_CommonBL.Get_ShareTran_Authorization_Sp(Tran_No, "SHRTR")
            Dim M_Dt1 As New DataTable
            Dim M_Dt2 As New DataTable
            Dim M_Dt3 As New DataTable
            M_Dt1 = M_Ds.Tables(0)
            M_Dt2 = M_Ds.Tables(1)
            M_Dt3 = M_Ds.Tables(2)

            If M_Dt1.Rows.Count > 0 Then
                DrpBranch.SelectedValue = M_Dt1.Rows(0).Item("Br_Code")

                CmbShareType.SelectedValue = M_Dt1.Rows(0).Item("Share_Type")
                CmbShareType_SelectedIndexChanged(Me.CmbShareType, System.EventArgs.Empty)
                CmbShare.SelectedValue = M_Dt1.Rows(0).Item("Sch_Code")
                TxtCustID.Text = M_Dt1.Rows(0).Item("FromCust_ID")
                TxtName.Text = M_Dt1.Rows(0).Item("Cust_Name")
                TxtAddress.Text = M_Dt1.Rows(0).Item("Full_Address")
                TxtResoluNo.Text = M_Dt1.Rows(0).Item("Res_No")
                TxtResDate.Text = M_Dt1.Rows(0).Item("Res_Date")
            End If

            If M_Dt2.Rows.Count > 0 Then
                DgvShareDetails.DataSource = M_Dt2
                DgvShareDetails.DataBind()
            End If

            If M_Dt3.Rows.Count > 0 Then
                GridTrans.DataSource = M_Dt3
                GridTrans.DataBind()
            End If

        Catch ex As Exception

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
            'CmbPhotoType.Items.Add("Browse")
            'CmbPhotoType.Items.Add("Web Cam")
            'LoadType()
            'Dim M_CommonBL As New CommonBL
           
            TxtTranDate.Text = Session("TransDate")
            TxtResDate.Text = Session("TransDate")
            TxtDate_Tran.Text = Session("TransDate")
            ''   M_CommonBL.Fill_ShareType(CmbShareType)
            M_CommonBL.Fill_ShareDetails(CmbShare)
            M_CommonBL.Fill_Branch(DrpBranch)
            M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
            DrpBranch.SelectedValue = Session("DefaultBrCode")
            Blank_Grid()
            Blank_CustJointGrid()
            'Blank_CustGrid()
            GridTrans.Rows(0).Cells(7).Visible = False
            GridTrans.Rows(0).Cells(8).Visible = False

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustID.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster

                'CmbAdmBranch.SelectedValue = .BrCode

                TxtName.Text = .CustName
                TxtAddress.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode

                'TxtPermPin.Text = 
                'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
                'TxtPermDist.Text = M_PresAddress.AddDist
                'TxtPermCountry.Text = M_PresAddress.AddCountry
                'cust_show.cust_grid()
            End With
            'CmdShow_Click(Me.CmdShow, System.EventArgs.Empty)
            cust_grid()
            CalcShareBal()

            M_CustMaster = Nothing
            M_PresAddress = Nothing
            M_PermAddress = Nothing
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub Blank_CustJointGrid()
        Try
            Dim CustJointDetails As New DataTable
            GridJointTranDetails.DataSource = Nothing
            CustJointDetails.Columns.Add("Cust_Id", GetType(String))
            CustJointDetails.Columns.Add("Cust_Name", GetType(String))
            CustJointDetails.Columns.Add("Cust_Addr", GetType(String))


            CustJointDetails.Rows.Add("", "", "")
            Session("CustJointDetails") = CustJointDetails
            GridJointTranDetails.DataSource = CustJointDetails
            GridJointTranDetails.DataBind()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub cust_grid()
        Try
            Dim cust_id As String = TxtCustID.Text

            'Dim dt As DataTable = DirectCast(Session("CustShareDetails"), DataTable)
            

            'Dim Add_Address1 As String = Txt_CustAddress.Text
            'Dim Add_HouseNo As String = Txt_CustAddress.Text
            'Dim Add_Address2 As String = Txt_CustAddress.Text
            'Dim Add_Taluk As String = Txt_CustAddress.Text
            'Dim Add_Address3 As String = Txt_CustAddress.Text

            Dim M_CommanBL As New CommonBL()
            Dim M_Datatable As New DataTable()
           

            M_Datatable = M_CommanBL.get_custSharegrid(cust_id, CmbShare.SelectedValue)

           
                If M_Datatable.Rows.Count > 0 Then

                    DgvShareDetails.DataSource = M_Datatable
                    DgvShareDetails.DataBind()
                Session("CustShareDetails") = M_Datatable

                'Dim M_Dt As DataTable = DirectCast(Session("CustShareDetails"), DataTable)
                Dim sum As Double = Convert.ToDouble(M_Datatable.Compute("SUM(Share)", String.Empty))
                LblShareBal.Text = sum
                Else
                    ShowAlertMessage("There is no share taken by this customer")
                    TxtCustID.Text = ""
                    TxtName.Text = ""
                    TxtAddress.Text = ""
                DgvShareDetails.DataSource = Nothing
                DgvShareDetails.DataBind()

                End If

            'M_CommanBL = Nothing


           
            'DgvShareDetails.DataSource = Session("CustShareDetails")
            'DgvShareDetails.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CalcShareBal()
        Try
            Dim Dt1 As DataTable = DirectCast(Session("CustShareDetails"), DataTable)
            Dim Dt2 As DataTable = DirectCast(Session("ShareDetails"), DataTable)
            'Dim 
            'objSum = Trandt.Compute("Sum(RctCash)", "")
            'LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")

            Dim TotalShare As Double = Convert.ToDouble(Val(Dt1.Compute("SUM(Share)", String.Empty)))
            Dim AddedShare As Double = Convert.ToDouble(Val(Dt2.Compute("SUM(TranShare)", String.Empty)))
            LblShareBal.Text = Format(Val((TotalShare - AddedShare)), "#0.00")

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub TxtTranCustId_TextChanged(sender As Object, e As EventArgs) Handles TxtTranCustId.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtTranCustId.Text), M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster

                If TxtTranCustId.Text = TxtCustID.Text Then
                    ShowAlertMessage("The customer id is same as that of transfre details")
                Else
                    TxtTranCustName.Text = .CustName
                    TxtTranCustAddr.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode
                    cust_Bindgrid()
                End If

            End With

            'CmdShow_Click(Me.CmdShow, System.EventArgs.Empty)


            M_CustMaster = Nothing
            M_PresAddress = Nothing
            M_PermAddress = Nothing
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub cust_Bindgrid()
        Try
            Dim cust_id As String = TxtTranCustId.Text


            'Dim Add_Address1 As String = Txt_CustAddress.Text
            'Dim Add_HouseNo As String = Txt_CustAddress.Text
            'Dim Add_Address2 As String = Txt_CustAddress.Text
            'Dim Add_Taluk As String = Txt_CustAddress.Text
            'Dim Add_Address3 As String = Txt_CustAddress.Text

            Dim M_CommanBL As New CommonBL()
            Dim M_Datatable As New DataTable()

            M_Datatable = M_CommanBL.get_custShareDetailsgrid(cust_id)

            M_CommanBL = Nothing
            If M_Datatable.Rows.Count > 0 Then
                PnlTranDetails.Visible = True
                'TranfShrDetails.Visible = False
                GridTranDetails.DataSource = M_Datatable
                GridTranDetails.DataBind()
            Else
                PnlTranDetails.Visible = False
                ''ShowAlertMessage("There is no share taken by this customer")
                'TxtTranCustId.Text = ""
                'TxtTranCustName.Text = ""
                ''TxtTranCustAddr.Text = ""
                'GridTranDetails.DataSource = Nothing
                'GridTranDetails.DataBind()
                ''TranfShrDetails.Visible = True
            End If
           

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbShareType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbShareType.SelectedIndexChanged
        Try
            If TxtCustID.Text <> "" Then
                cust_grid()
            ElseIf TxtTranCustId.Text <> "" Then
                cust_Bindgrid()
            End If
            M_CommonBL.Fill_ShareDetails(CmbShare)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Blank_CustGrid()
        Try
            Dim CustShareDetails As New DataTable
            DgvShareDetails.DataSource = Nothing
            CustShareDetails.Columns.Add("Acc_id", GetType(String))
            CustShareDetails.Columns.Add("ACC_No", GetType(String))
            CustShareDetails.Columns.Add("SeriesNo", GetType(String))
            CustShareDetails.Columns.Add("Date_Allot", GetType(String))
            CustShareDetails.Columns.Add("Share", GetType(String))
            CustShareDetails.Columns.Add("MinDistNo", GetType(String))
            CustShareDetails.Columns.Add("MaxDistNo", GetType(String))
            CustShareDetails.Columns.Add("DistFrom", GetType(String))
            CustShareDetails.Columns.Add("Distinctive_No", GetType(String))


            CustShareDetails.Rows.Add("", "", "", "", "", "", "", "", "")
            Session("CustShareDetails") = CustShareDetails
            DgvShareDetails.DataSource = CustShareDetails
            DgvShareDetails.DataBind()


        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub Blank_Grid()
        Try
            Dim ShareDetails As New DataTable
            GridTrans.DataSource = Nothing
            ShareDetails.Columns.Add("SeriesNo", GetType(String))
            ShareDetails.Columns.Add("CustId", GetType(String))
            ShareDetails.Columns.Add("CustName", GetType(String))
            ShareDetails.Columns.Add("FolioNo", GetType(String))
            ShareDetails.Columns.Add("Share", GetType(String))
            ShareDetails.Columns.Add("TranShare", GetType(Double))
            ShareDetails.Columns.Add("DistNo", GetType(String))
            ShareDetails.Columns.Add("DistFrom", GetType(String))
            ShareDetails.Columns.Add("DistTo", GetType(String))
            ShareDetails.Columns.Add("Transfre", GetType(String))
            ShareDetails.Columns.Add("ACC_No", GetType(String))
            ShareDetails.Columns.Add("ACC_ID", GetType(String))
            ShareDetails.Columns.Add("Batch_No", GetType(String))
            ShareDetails.Columns.Add("TransfereeAcc_ID", GetType(String))
            ShareDetails.Columns.Add("Mode_of_opn", GetType(String))
            ShareDetails.Columns.Add("Mode_of_opn_ID", GetType(String))
            ShareDetails.Columns.Add("Operators", GetType(String))
            ShareDetails.Columns.Add("Operators_Id", GetType(String))
            ShareDetails.Columns.Add("NoOf_Oper", GetType(String))
            ShareDetails.Rows.Add("", "", "", "", "", 0, "", 0, 0, "", "", "", "", "0", "", "", "", "", "0")
            Session("ShareDetails") = ShareDetails
            GridTrans.DataSource = ShareDetails
            GridTrans.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Try


            Dim dt As DataTable = DirectCast(Session("ShareDetails"), DataTable)

            Dim Row As DataRow

            If TxtTranCustAddr.Text.Length = 0 Then
                ShowAlertMessage("Invalid Cust Address")
                TxtTranCustAddr.Focus()
                Exit Sub
            End If
            If TxtTranCustId.Text.Length = 0 Then
                ShowAlertMessage("Invalid Cust Id")
                TxtTranCustId.Focus()
                Exit Sub
            End If
            If TxtTranCustName.Text.Length = 0 Then
                ShowAlertMessage("Invalid Customer Name")
                TxtTranCustName.Focus()
                Exit Sub
            End If
            If TxtTranCustShare.Text.Length = 0 Then
                ShowAlertMessage("Invalid Share")
                TxtTranCustShare.Focus()
                Exit Sub
            End If
            If TxtTranDisFrom.Text.Length = 0 Then
                ShowAlertMessage("Invalid Distinctive No")
                TxtTranDisFrom.Focus()
                Exit Sub
            End If
            If TxtTranDisTo.Text.Length = 0 Then
                ShowAlertMessage("Invalid Distinctive No")
                TxtTranDisTo.Focus()
                Exit Sub
            End If
            If TxtBatch_No.Text.Length = 0 Then
                ShowAlertMessage("Invalid Serial No:")
                TxtBatch_No.Focus()
                Exit Sub
            End If
            Dim Status As String = "N"
            Dim SlNo As String
            Dim batch1 As String
            'Dim DistNo As String
            Dim DistFrom As String
            Dim DistTo As String
            'Dim Acc_id As String
            Dim TransferorAccID As String
            Dim BatchId As String
            batch1 = TxtBatch_No.Text
            For j As Integer = 0 To DgvShareDetails.Rows.Count - 1

                
                    DistFrom = DgvShareDetails.Rows(j).Cells(6).Text
                    DistTo = DgvShareDetails.Rows(j).Cells(7).Text
                SlNo = DgvShareDetails.Rows(j).Cells(1).Text

                    If SlNo = batch1 Then
                    Session("Acc_id") = DgvShareDetails.Rows(j).Cells(0).Text
                    Session("BatchId") = DgvShareDetails.Rows(j).Cells(3).Text
                    TransferorAccID = DgvShareDetails.Rows(j).Cells(0).Text

                    BatchId = DgvShareDetails.Rows(j).Cells(3).Text
                    If (TxtTranDisFrom.Text >= Val(DistFrom) And TxtTranDisFrom.Text <= Val(DistTo)) And (TxtTranDisTo.Text >= Val(DistFrom) And TxtTranDisTo.Text <= Val(DistTo)) Then

                        For i As Integer = 0 To GridTrans.Rows.Count - 1

                            If (TxtTranDisFrom.Text >= GridTrans.Rows(i).Cells(7).Text And TxtTranDisFrom.Text <= GridTrans.Rows(i).Cells(8).Text) Or (TxtTranDisTo.Text >= GridTrans.Rows(i).Cells(7).Text And TxtTranDisTo.Text <= GridTrans.Rows(i).Cells(8).Text) Then
                                ShowAlertMessage("Please check the Distinctive No you entered")
                                Exit Sub
                            End If


                        Next
                    Else
                        ShowAlertMessage("Invalid Disctinctive number..Please Check")
                        Exit Sub
                    End If
                    
                    End If
                   
            Next

            Dim M_CommanBL As New CommonBL()
            Dim M_Datatable As New DataTable()
            Dim TransfereeAccId As String = ""
            Dim TranfrAccNo As String = ""
            If PnlTranDetails.Visible = True Then
                For i As Integer = 0 To GridTranDetails.Rows.Count - 1
                    Dim tb As CheckBox = DirectCast(GridTranDetails.Rows(i).FindControl("ChkView"), CheckBox)
                    If tb.Checked Then
                        If GridTranDetails.Rows(i).Cells(2).Text().Length > 0 Then
                            TranfrAccNo = GridTranDetails.Rows(i).Cells(2).Text()
                            TransfereeAccId = GridTranDetails.Rows(i).Cells(1).Text()
                        Else
                            TranfrAccNo = "0"
                        End If
                    End If
                Next
            Else
                TranfrAccNo = "0"
            End If
            'For i As Integer = 0 To GridTranDetails.Rows.Count - 1
            '    Session("TranfrAccNo") = 0
            '    Dim ChkView As CheckBox = DirectCast(GridTranDetails.Rows(i).Cells(0).FindControl("ChkView"), CheckBox)
            '    If ChkView.Checked = True Then
            '        TranfrAccNo = GridTranDetails.Rows(i).Cells(1).Text()
            '        If TranfrAccNo.Length > 0 Then
            '            TransferrAccId = GridTranDetails.Rows(i).Cells(0).Text()
            '        End If
            '    End If
            'Next

            M_Datatable = M_CommanBL.get_custShareDtlBatchno(Session("Acc_id"), TxtCustID.Text, Session("BatchId"))
            If M_Datatable.Rows.Count > 0 Then

                Dim share As String = M_Datatable.Rows(0)("share").ToString()
                Dim FoiloNo As String = M_Datatable.Rows(0)("Folio_No").ToString()
                Dim Accno As String = M_Datatable.Rows(0)("ACC_No").ToString()
                Dim AccId As String = M_Datatable.Rows(0)("ACC_ID").ToString()
                Dim Mode_op_opn As String = CmbOpMode.SelectedItem.Text()
                Dim Mode_op_opn_Id As String = CmbOpMode.SelectedValue

                Dim joint_cust_id_All As String = ""
                Dim joint_cust_Name_All As String = ""



                If Chk_Operators.Checked = True Then

                    For j As Integer = 0 To GridJointTranDetails.Rows.Count - 1
                        Dim joint_cust_id As String = GridJointTranDetails.Rows(j).Cells(0).Text()
                        joint_cust_id_All += joint_cust_id + "|"


                        Dim joint_cust_Name As String = GridJointTranDetails.Rows(j).Cells(1).Text()
                        joint_cust_Name_All += joint_cust_Name + "|"

                    Next
                End If

                'If UnitValSum > Val(TxtTranAmnt.Text) Then
                '    ShowAlertMessage("Unit Value Greater Than Transaction Amount...")
                '    Exit Sub
                'End If

                'If Validate() = False Then
                '    Exit Sub
                'End If

                ' dt = DirectCast(Session("ShareDetails"), DataTable)
                Row = dt.NewRow
                Row("SeriesNo") = TxtBatch_No.Text
                Row("CustId") = TxtTranCustId.Text
                Row("CustName") = TxtTranCustName.Text
                Row("FolioNo") = FoiloNo
                Row("Share") = share
                Row("TranShare") = Val(TxtTranCustShare.Text)
                Row("DistNo") = TxtTranDisFrom.Text + "-" + TxtTranDisTo.Text
                Row("DistFrom") = TxtTranDisFrom.Text
                Row("DistTo") = TxtTranDisTo.Text
                Row("Transfre") = TranfrAccNo
                Row("ACC_No") = Accno
                Row("ACC_ID") = AccId
                Row("Batch_No") = BatchId
                Row("TransfereeAcc_ID") = TransfereeAccId
                Row("Mode_of_opn") = CmbOpMode.SelectedItem.Text

                Row("Mode_of_opn_ID") = CmbOpMode.SelectedValue
                Row("Operators") = joint_cust_Name_All
                Row("Operators_Id") = joint_cust_id_All
                Row("NoOf_Oper") = Val(TxtNoofoperators.Text)

                Session("DistFrom") = TxtTranDisFrom.Text
                Session("DistTo") = TxtTranDisTo.Text

             
                Try
                    dt.Rows.Add(Row)

                Catch ex As Exception

                End Try
            End If
            Try
                Dim Expression As String = "CustId=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try

            GridTrans.DataSource = dt
            GridTrans.DataBind()
            Session("ShareDetails") = dt

            CalcShareBal()
            'Dim sum As Double = Convert.ToDouble(dt.Compute("SUM(TranShare)", String.Empty))
            'LblShareBal.Text = Format(Val(sum), "#0.00")

            'LblShareBal.Text = Convert.ToDouble(LblShareBal.Text) - Convert.ToDouble(sum)
            TxtTranCustId.Text = ""
            TxtTranCustAddr.Text = ""
            TxtTranCustName.Text = ""
            GridTranDetails.DataSource = Nothing
            GridTranDetails.DataBind()
            TxtTranDisFrom.Text = ""
            TxtTranDisTo.Text = ""
            TxtTranCustShare.Text = ""
            Session.Remove("joint_cust_id_All")
            Session.Remove("joint_cust_Name_All")
            Txt_JointCustId.Text = ""
            Txt_JointCustName.Text = ""
            Txt_JointCustAddr.Text = ""
            Blank_CustJointGrid()
            GridJointTranDetails.DataSource = Nothing
            GridJointTranDetails.DataBind()

            'If dt.Rows.Count > 0 Then
            '    Dim total As Double
            '    For i As Integer = 0 To GridTranDetails.Rows.Count - 1
            '        total = total + GridTranDetails.Rows(i).Cells(3).Text
            '        ShowAlertMessage(total)
            '        'Change the number 2 to your column index number (The first column has a 0 index column)
            '        'In this example the column index of Price is 2
            '    Next

            'End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub
    Protected Sub CmbOpMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbOpMode.SelectedIndexChanged
        If CmbOpMode.SelectedValue = 399 Then
            TxtNoofoperators.Text = "1"
            TxtNoofoperators.ReadOnly = True
            PnlTranDetails.Visible = True
            PnlJointOperators.Visible = False
            Txt_JointCustId.Visible = False
            Txt_JointCustName.Visible = False
            Txt_JointCustAddr.Visible = False
        Else
            TxtNoofoperators.Text = ""
            TxtNoofoperators.ReadOnly = False
            PnlTranDetails.Visible = False
            PnlJointOperators.Visible = True
            Txt_JointCustId.Visible = True
            Txt_JointCustName.Visible = True
            Txt_JointCustAddr.Visible = True
        End If
    End Sub
    Protected Sub CmdAddOperator_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAddOperator.Click
        Try


            Dim dt As DataTable = DirectCast(Session("CustJointDetails"), DataTable)

            Dim Row As DataRow

            If TxtNoofoperators.Text.Length = 0 Then
                ShowAlertMessage("Invalid Operators")
                TxtTranCustAddr.Focus()
                Exit Sub
            End If

            If Txt_JointCustId.Text.Length = 0 Then
                ShowAlertMessage("Invalid CustId")
                TxtTranCustAddr.Focus()
                Exit Sub
            End If
            If dt.Rows.Count >= Val(TxtNoofoperators.Text) And dt.Rows(0).Item(1).ToString <> "" Then
                ShowAlertMessage("MAX NO OF CUSTOMER EXCEEDED")
                Exit Sub
            End If

            Dim Status As String = "N"
            Dim SlNo As String
            Dim batch1 As String
            Dim DistNo As String
            Dim DistFrom As String
            Dim DistTo As String
            Dim Acc_id As String
            Dim TransferorAccID As String
            Dim BatchId As String
            batch1 = TxtBatch_No.Text

            Dim M_CommanBL As New CommonBL()
            Dim M_Datatable As New DataTable()



            M_Datatable = M_CommanBL.get_custJointDtlBatchno(Txt_JointCustId.Text)
            If M_Datatable.Rows.Count > 0 Then


                Dim CustId As String = M_Datatable.Rows(0)("Cust_id").ToString()
                Dim CustName As String = M_Datatable.Rows(0)("Cust_Name").ToString()

                Dim CustAddre As String = M_Datatable.Rows(0)("Add_Address1").ToString() + "," + M_Datatable.Rows(0)("Add_Address2").ToString() + "," + M_Datatable.Rows(0)("Add_Address3").ToString() + "," + M_Datatable.Rows(0)("Add_Postoffice").ToString()


                Row = dt.NewRow
                Row("Cust_Id") = CustId
                Row("Cust_Name") = CustName

                Row("Cust_Addr") = CustAddre






                Try
                    dt.Rows.Add(Row)

                Catch ex As Exception

                End Try
            End If
            Try
                Dim Expression As String = "Cust_Id=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try

            GridJointTranDetails.DataSource = dt
            GridJointTranDetails.DataBind()
            Session("CustJointDetails") = dt
            Txt_JointCustId.Text = ""
            Txt_JointCustName.Text = ""
            Txt_JointCustAddr.Text = ""





        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try

    End Sub
    Protected Sub GridJointTranDetails_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridJointTranDetails.RowDeleting
        Try
            Dim M_dt As New DataTable
            M_dt = DirectCast(Session("CustJointDetails"), DataTable)
            M_dt.Rows(e.RowIndex).Delete()
            M_dt.AcceptChanges()
            Session("CustJointDetails") = M_dt
            GridJointTranDetails.DataSource = M_dt
            GridJointTranDetails.DataBind()

            If M_dt.Rows.Count = 0 Then
                Blank_CustJointGrid()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridTrans_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridTrans.RowDeleting
        Try
            Dim M_dt As New DataTable
            M_dt = DirectCast(Session("ShareDetails"), DataTable)
            M_dt.Rows(e.RowIndex).Delete()
            M_dt.AcceptChanges()
            Session("ShareDetails") = M_dt
            GridTrans.DataSource = M_dt
            GridTrans.DataBind()

            If M_dt.Rows.Count = 0 Then
                Blank_Grid()
            End If
            CalcShareBal()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridTrans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridTrans.SelectedIndexChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BatchNo As TableCell = e.Row.Cells(0)
            Dim Custid As TableCell = e.Row.Cells(1)
            Dim FoiloNo As TableCell = e.Row.Cells(3)
            Dim Share As TableCell = e.Row.Cells(4)
            Dim TransShare As TableCell = e.Row.Cells(5)
            Dim DistnNo As TableCell = e.Row.Cells(6)
            Dim DistnNoFrom As TableCell = e.Row.Cells(7)
            Dim DistnNoTo As TableCell = e.Row.Cells(8)
            For J As Integer = 1 To GridTrans.Rows.Count - 1
                If Session("DistFrom") > GridTrans.Rows(J).Cells(7).Text And Session("DistFrom") < GridTrans.Rows(J).Cells(8).Text Then
                    If Session("DistTo") > GridTrans.Rows(J).Cells(7).Text And Session("DistTo") < GridTrans.Rows(J).Cells(8).Text Then

                        ShowAlertMessage("RIGHT")

                    End If
                Else
                    ShowAlertMessage("WRONG")
                End If

            Next

            ShowAlertMessage(DistnNoFrom.Text)
            'If (Session("DistNoFrom")
            '    Session("DistNoFrom") = TxtTranDisTo.Text


        End If
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As ImageClickEventArgs) Handles CmdSave.Click
        Try
            'Dim Acc_id As String
            'For j As Integer = 0 To DgvShareDetails.Rows.Count - 1
            '    Acc_id = DgvShareDetails.Rows(j).Cells(0).Text
            '    ShowAlertMessage(Acc_id)
            'Next
            'Br_Code ,  Sch_Code , Tr_Date, Share_Type ,  FromCustID ,  TransferorXML ,  TransfereeXML  ,  ResNo  ,  ResDate  ,  MakerID  

            Dim M_CommonBL As New CommonBL
            If Len(Request.QueryString("TRANID")) > 0 Then
                If M_CommonBL.Share_Tran_Authorise(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx")
                    Exit Sub
                End If
                M_CommonBL = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If

            Dim M_Dt As DataTable
            Dim M_ShareMasterBL As New ShareMasterBL
            Dim M_TransferorDt As DataTable = DirectCast(Session("CustShareDetails"), DataTable)
            Dim M_TransfereeDt As DataTable = DirectCast(Session("ShareDetails"), DataTable)
            M_TransfereeDt.TableName = "Table"
            M_TransferorDt.TableName = "Table"
            Dim M_TransferorSw As New IO.StringWriter
            Dim M_TransfereeSw As New IO.StringWriter
     
            M_TransferorDt.WriteXml(M_TransferorSw)
            M_TransfereeDt.WriteXml(M_TransfereeSw)

            Try
                Dim Expression As String = "CustId=''"
                If M_TransfereeDt.[Select](Expression).Count > 0 Then M_TransfereeDt.Rows.RemoveAt(M_TransfereeDt.Rows.IndexOf(M_TransfereeDt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try

            If M_TransfereeDt.Rows.Count <= 0 Then
                ShowAlertMessage("Please Add the Transfer Data")
                TxtTranCustId.Focus()
                Exit Sub
            End If

            M_Dt = M_ShareMasterBL.Share_Transfer_Folio(DrpBranch.SelectedValue, CmbShare.SelectedValue, Session("TransDate"), CmbShareType.SelectedValue, TxtCustID.Text, M_TransferorSw.ToString(), M_TransfereeSw.ToString(), TxtResoluNo.Text, TxtResDate.Text, Session("UserID"))
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage("Saved...")
                Clear_Me()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Clear_Me()
        Try
            TxtCustID.Text = ""
            TxtAddress.Text = ""
            TxtResDate.Text = Session("TransDate")
            TxtResoluNo.Text = ""
            TxtTranCustId.Text = ""
            TxtTranCustName.Text = ""
            TxtTranCustAddr.Text = ""
            TxtBatch_No.Text = ""
            TxtTranDisFrom.Text = ""
            TxtTranDisTo.Text = ""
            TxtTranDate.Text = Session("TransDate")
            TxtTranCustShare.Text = ""
            GridTranDetails.DataSource = Nothing
            GridTranDetails.DataBind()
            Blank_CustGrid()
            Blank_Grid()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Chk_Operators_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chk_Operators.CheckedChanged
        Try
            If Chk_Operators.checked = True Then
                DivOperators.Visible = True
                PnlTranDetails.Visible = False
                PnlJointOperators.Visible = True
                'JntCustDetails.Visible = True
            Else
                DivOperators.Visible = False
                PnlJointOperators.Visible = False
                PnlTranDetails.Visible = True
                'JntCustDetails.Visible = False

            End If
            If CmbOpMode.SelectedValue = 399 Then
                TxtNoofoperators.Text = "1"
                TxtNoofoperators.ReadOnly = True
                'JntCustDetails.Visible = False
            Else
                TxtNoofoperators.Text = ""
                TxtNoofoperators.ReadOnly = False
                'JntCustDetails.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_JointCustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_JointCustId.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Txt_JointCustId.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster

                'CmbAdmBranch.SelectedValue = .BrCode

                Txt_JointCustName.Text = .CustName
                Txt_JointCustAddr.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode

                'TxtPermPin.Text = 
                'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
                'TxtPermDist.Text = M_PresAddress.AddDist
                'TxtPermCountry.Text = M_PresAddress.AddCountry
                'cust_show.cust_grid()
            End With
            'CmdShow_Click(Me.CmdShow, System.EventArgs.Empty)



            M_CustMaster = Nothing
            M_PresAddress = Nothing
            M_PermAddress = Nothing
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class