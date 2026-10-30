Imports SMARTCORE_BL

Partial Public Class ShareMaster
    Inherits System.Web.UI.Page

    Public OrnamentTbl As New DataTable("Table")
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1102
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


            Load_DefaultValues()

            Txt_AdmissionDate.Attributes.Add("readonly", "readonly")



            If Len(Request.QueryString("TRANID")) > 0 Then
                GetAuthoriseDtls("SHAREMAS", Request.QueryString("TRANID"))
            Else
                Assign_Values()
            End If

        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("SHAREMAS", TransID)
        M_CommonBL = Nothing
        Dim M_Nominee As DataTable
        Dim M_TranTbl As DataTable
        M_TranTbl = M_Dataset.Tables(0)
        DgvCustomer.DataSource = M_Dataset.Tables(1)
        M_Nominee = M_Dataset.Tables(2)
        DgvTransaction.DataSource = M_Dataset.Tables(3)
        Session("TransTbl") = M_Dataset.Tables(1)
        DgvTransaction.DataBind()
        DgvCustomer.DataBind()
        M_CommonBL = Nothing

        GetAccountDtls(M_TranTbl, M_Nominee)
        PnlAuthorise.Enabled = False
        'Calc_TranTotal()
        Set_TransactionGrid()
    End Sub

    Private Sub GetAccountDtls(ByVal M_AccTbl As DataTable, ByVal M_Nominee As DataTable)
        CmbBranch.SelectedValue = M_AccTbl.Rows(0).Item("Br_Code")
        CmbScheme.SelectedValue = M_AccTbl.Rows(0).Item("Sch_Code")
        TxtNoOfOperator.Text = M_AccTbl.Rows(0).Item("NoOfOp")
        Txt_AdmissionDate.Text = M_AccTbl.Rows(0).Item("Sm_AdmitDate")
        Txt_ReslNo.Text = M_AccTbl.Rows(0).Item("Sm_ResNo")
        Txt_ReslDate.Text = M_AccTbl.Rows(0).Item("Sm_ResDate")
        Txt_IdNumber.Text = M_AccTbl.Rows(0).Item("Sm_ID_No")
        Txt_ShareNo.Text = M_AccTbl.Rows(0).Item("Sm_NoShr")
        Txt_ShareAmt.Text = M_AccTbl.Rows(0).Item("Sm_Balance")
        CmbNomReq.SelectedItem.Text = M_AccTbl.Rows(0).Item("Nominee")
        CmbCashTransfer.SelectedIndex = M_AccTbl.Rows(0).Item("Cash")
        If (CmbNomReq.SelectedItem.Text = "Yes") Then
            NomDiv.Visible = True
            Txt_NomName.Text = M_Nominee.Rows(0).Item("Nm_Name")
            Txt_NomAdd.Text = M_Nominee.Rows(0).Item("Nm_Address")
            Txt_NomPh.Text = M_Nominee.Rows(0).Item("Nm_Phone")
            CmbNomRel.SelectedIndex = M_Nominee.Rows(0).Item("Nm_Relation")
        Else
            NomDiv.Visible = False
            Txt_NomName.Text = Nothing
            Txt_NomAdd.Text = Nothing
            Txt_NomPh.Text = Nothing
            CmbNomRel.SelectedIndex = Nothing
        End If

       
    End Sub


    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
        M_CommonBL.Fill_Pickup(CmbAmsom, 82, 0)
        M_CommonBL.Fill_Pickup(CmbDesam, 81, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "SHARE")

        M_CommonBL.Fill_BatchNo(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo)
        CmbBatchNo.SelectedIndex = 0

        DgvTransaction.DataSource = Nothing
        Dim Dt As DataTable
        Dt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        PickupDt = DirectCast(Cache("PickupTbl"), DataTable)
        M_CommonBL = Nothing
        CmbCashTransfer.Items.Add("CASH")
        CmbCashTransfer.Items.Add("TRANSFER")
        CmbCashTransfer.SelectedIndex = 0

        CmbNomReq.Items.Add("No")
        CmbNomReq.Items.Add("Yes")
        CmbNomReq.SelectedIndex = 0
        cmdDelete.Visible = False
    End Sub

    Private Sub Assign_Values()

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")
        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
        DgvCustomer.DataSource = dt
        DgvCustomer.DataBind()

        Dim M_CommonBL As New CommonBL
        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)
        ParameterDt = M_CommonBL.GetShareParameter(CmbScheme.SelectedValue)
        HdnShareValue.Value = ParameterDt.Rows(0).Item("Shp_Sharevalue")
        Txt_AdmissionDate.Text = Session("TransDate")
        M_CommonBL = Nothing

    End Sub


    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_ShareMaster As New SMARTCORE_BO.ShareMasterBO
        Dim M_CommonBL As New CommonBL
        Dim M_BatchNo As String = ""
        If Not ((Val(Session("MaxTranLimit").ToString) >= (Val(LblRctCashTotal.Text) + Val(LblRctTransferTotal.Text) + Val(LblPayCashTotal.Text) + Val(LblPayTransferTotal.Text)))) Then
            ShowAlertMessage("Maximum Transaction Limit is :" + Session("MaxTranLimit").ToString())
            Exit Sub
        End If
        If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

        If Len(Request.QueryString("TRANID")) > 0 Then

            If Len(Request.QueryString("DEL")) > 0 Then

                CmdSave.Visible = False
                cmdDelete.Visible = True


            End If

            If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                ShowAlertMessage("Authotisation Failed")
                Response.Redirect("~/ManagerPassing.aspx")
                Exit Sub
            End If
            M_CommonBL = Nothing
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
            Exit Sub
        End If


        With M_ShareMaster
            .AccID = M_AccID
            .SmMemNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .AccCode = M_AccHead
            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
            .SmAdmitDate = IIf(IsDate(Txt_AdmissionDate.Text), Txt_AdmissionDate.Text, "01/01/1900")
            .SmResNo = Txt_ReslNo.Text
            .SmResDate = IIf(IsDate(Txt_ReslDate.Text), Txt_ReslDate.Text, "01/01/1900")
            .SmIDNo = Txt_IdNumber.Text
            .SmFolioNo = Txt_ReslNo.Text
            .SmLastUpdate = Txt_AdmissionDate.Text
            .SmNoShr = Val(Txt_ShareNo.Text)
            .SmNomNos = IIf(NomDiv.Visible = False, 0, 1)
            .SmClosedDate = "01/01/1900"
            .ShpSharevalue = Val(Txt_ShareAmt.Text)
            .SmOpNoShr = 0
            .SmOpShrAmt = Val(Txt_ShareAmt.Text)
            .SmStatus = "Y"
            .Authorised = "N"
            .LogID = "0"
            .TranNo = ""
            .MakerID = Session("UserID")
            .CheckerID = ""
            .MakingTime = Date.Now
            .CashierTime = "01/01/1900"
            .CheckingTime = "01/01/1900"
            .CashierID = ""
            .BankAccNo = 0
            .SmRemarks = ""
            .CustAmsom = CmbAmsom.SelectedValue
            .CustDesom = CmbDesam.SelectedValue
            .OrgBranch = Session("DefaultBrCode")
        End With

        Try
            Dim AccHolderdt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)
            Dim M_AccHolderSw As New IO.StringWriter
            AccHolderdt.WriteXml(M_AccHolderSw)

            Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
            Dim M_TranSw As New IO.StringWriter
            Trandt.WriteXml(M_TranSw)

            Dim M_NomineeSw As New IO.StringWriter
            If NomDiv.Visible = False Then
                Session("NomTbl") = ""
            Else
                Set_Nominee()
                Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
                NomineeDt.WriteXml(M_NomineeSw)
            End If

            Dim M_ShareMasterBL As New ShareMasterBL
            Dim M_Dt As DataTable
            Session("M_EntryMode") = "NEW"
            M_Dt = M_ShareMasterBL.Save_Data(M_ShareMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), M_BatchNo)
            ShowAlertMessage(" SHARE NO : " & M_Dt.Rows(0)("Acc_No").ToString)

            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=SHARE&SCHEME=" & CmbScheme.SelectedValue & "&SEC=SHARE&DRCR=" & "C")

            M_ShareMasterBL = Nothing
            Session("NomTbl") = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdDelete.Click
        Try


            Dim Tran_No As String
            Dim M_DBConn As New SMARTCORE_DAL.Connection()
            Dim ClsCom As New SMARTCORE_Common.CommonClass
            Dim SqlStr As String
            Dim DeleteBranch As String
            DeleteBranch = Request.QueryString("DelBranch").ToString()
            Tran_No = Request.QueryString("TRANID").ToString()

            SqlStr = "Exec Transaction_Delete_Sp '" & Tran_No & "'," & ClsCom.DbDate(CDate(Session("TransDate").ToString())) & _
                ",'" & Session("UserID") & "','" & Session("DefaultBrCode").ToString() & "'"
            M_DBConn.ExecuteScalar(SqlStr, , False)

            Response.Redirect("~/trancancellation.ASPX")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_Nominee()
        Try
            Dim NomineeTbl As New DataTable("Table")
            NomineeTbl.Columns.Add("Nm_Nom_No", GetType(String))
            NomineeTbl.Columns.Add("Br_Code", GetType(String))
            NomineeTbl.Columns.Add("Sch_Code", GetType(String))
            NomineeTbl.Columns.Add("Acc_No", GetType(String))
            NomineeTbl.Columns.Add("Nm_Name", GetType(String))
            NomineeTbl.Columns.Add("Nm_Gender", GetType(String))
            NomineeTbl.Columns.Add("Nm_Relation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Phone", GetType(String))
            NomineeTbl.Columns.Add("Nm_Address", GetType(String))
            NomineeTbl.Columns.Add("Nm_Occupation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_DOB", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Age", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Minor", GetType(String))
            NomineeTbl.Columns.Add("Nm_GName", GetType(String))
            NomineeTbl.Columns.Add("Nm_Grelation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_GAddress", GetType(String))
            NomineeTbl.Columns.Add("Nm_Narration", GetType(String))
            NomineeTbl.Columns.Add("Nm_EffectDate", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Shr_Percent", GetType(Double))
            NomineeTbl.Columns.Add("Log_Id", GetType(Double))
            NomineeTbl.Rows.Add("", CmbBranch.SelectedValue, CmbScheme.SelectedValue, "", Txt_NomName.Text, "M", CmbNomRel.SelectedValue, Txt_NomPh.Text, Txt_NomAdd.Text, 0, "01/01/1900", 0, "N", "", 0, "", "Nil", Session("TransDate"), 0, 0)
            Session("NomTbl") = NomineeTbl

        Catch ex As Exception
            ShowAlertMessage(ex.Message.ToString)
        End Try
    End Sub


    Private Sub Set_TransactionGrid()
        Dim M_CommonBL As New CommonBL
        Dim Ds As DataSet
        Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Txt_AdmissionDate.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "C", "M", Val(Txt_ShareAmt.Text))
        Dim Dt As DataTable = Ds.Tables(0)
        DgvTransaction.DataSource = Dt
        DgvTransaction.DataBind()
        Session("TransTbl") = Dt

        Dim objSum As Object
        If Dt.Rows.Count > 0 Then
            objSum = Dt.Compute("Sum(RctCash)", "")
            LblRctCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Dt.Compute("Sum(RctTransfer)", "")
            LblRctTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Dt.Compute("Sum(PayCash)", "")
            LblPayCashTotal.Text = Format(Val(objSum.ToString()), "#0.00")
            objSum = Dt.Compute("Sum(PayTransfer)", "")
            LblPayTransferTotal.Text = Format(Val(objSum.ToString()), "#0.00")
        End If

        If Ds.Tables(1).Rows.Count > 0 Then
            If Ds.Tables(1).Rows(0)("AlertMsg").ToString.Length > 0 Then
                Dim AlertStr As String
                AlertStr = Ds.Tables(1).Rows(0)("AlertMsg").ToString
                ShowAlertMessage(AlertStr)
            End If
        End If
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub Txt_ShareAmt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_ShareAmt.TextChanged
        Set_TransactionGrid()
    End Sub

    Protected Sub Txt_ShareNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_ShareNo.TextChanged
        Dim ShareValue As Double
        ShareValue = Val(Txt_ShareNo.Text) * Val(HdnShareValue.Value)
        Txt_ShareAmt.Text = Format(ShareValue, "#0.00")

        Set_TransactionGrid()
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Set_TransactionGrid()

        If CmbCashTransfer.SelectedIndex = 0 Then ' Cash
            CmbBatchNo.Enabled = False
            ImgBatch.Enabled = False
        Else
            CmbBatchNo.Enabled = True
            ImgBatch.Enabled = True
        End If
    End Sub

    Protected Sub CmbNomReq_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNomReq.SelectedIndexChanged
        If CmbNomReq.SelectedIndex = 0 Then
            Txt_NomName.Text = ""
            NomDiv.Visible = False
        Else
            If Txt_NomAdd.Text = "" Then
                Txt_NomAdd.Text = DgvCustomer.Rows(0).Cells(7).Text
            End If
            NomDiv.Visible = True
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        If Len(Request.QueryString("TRANID")) > 0 Then
            Response.Redirect("~/ManagerPassing.aspx?Passing=y")
        Else
            Response.Redirect("~/Main.aspx")
        End If
    End Sub

    Protected Sub CmbBatchNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBatchNo.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            If CmbBatchNo.SelectedItem.Text <> "-- NEW --" Then
                M_Dt = M_CommonBL.Get_BatchTotal(Session("DefaultBrCode"), Session("TransDate"), CmbBatchNo.SelectedItem.Text)
                If M_Dt.Rows.Count > 0 Then
                    LblDebit.Text = Format(Val(M_Dt.Rows(0)("Debit").ToString), "#0.00") & " Dr"
                    LblCredit.Text = Format(Val(M_Dt.Rows(0)("Credit").ToString), "#0.00") & " Cr"
                End If
            Else
                LblDebit.Text = ""
                LblCredit.Text = ""
            End If

            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
End Class