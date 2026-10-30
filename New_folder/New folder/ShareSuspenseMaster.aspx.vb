Imports SMARTCORE_BL
Partial Public Class ShareSuspenseMaster
    Inherits System.Web.UI.Page

    Public OrnamentTbl As New DataTable("Table")
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Dim FormID As Integer = 1150
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
            Assign_Values()
            Txt_AdmissionDate.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "SHARE")
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

    End Sub

    Private Sub Assign_Values()

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
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
        Dim M_ShareSusMaster As New SMARTCORE_BO.ShareSusMasterBO
        Dim M_BatchNo As String = ""

        If CmbCashTransfer.SelectedIndex = 1 Then M_BatchNo = CmbBatchNo.SelectedItem.Text

        With M_ShareSusMaster
            .AccID = M_AccID
            .SmMemNo = M_AccNo
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .AccCode = M_AccHead
            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
            .SmAdmitDate = IIf(IsDate(Txt_AdmissionDate.Text), Txt_AdmissionDate.Text, "01/01/1900")
            .SmLastUpdate = Txt_AdmissionDate.Text
            .SmNoShr = Val(Txt_ShareNo.Text)
            .SmBalance = Val(Txt_ShareAmt.Text)
            .SmNomNos = IIf(NomDiv.Visible = False, 0, 1)
            .SmClosedDate = "01/01/1900"
            .ShpSharevalue = Val(Txt_ShareAmt.Text)
            .SmOpNoShr = 0
            .SmOpShrAmt = 0
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

            Dim M_ShareSusMasterBL As New ShareSusMasterBL
            Dim M_Dt As DataTable
            Session("M_EntryMode") = "NEW"
            M_Dt = M_ShareSusMasterBL.Save_Data(M_ShareSusMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), M_BatchNo)
            ShowAlertMessage(" SHARE NO : " & M_Dt.Rows(0)("Acc_No").ToString)

            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=SHARE&SCHEME=" & CmbScheme.SelectedValue & "&SEC=SHARE&DRCR=" & "C")

            M_ShareSusMasterBL = Nothing
            Session("NomTbl") = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
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
        Response.Redirect("~/Main.aspx")
    End Sub

    Private Sub CmbBatchNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBatchNo.SelectedIndexChanged
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