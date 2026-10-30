Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class GroupDeposit
    Inherits System.Web.UI.Page

    Dim M_CommonBL As New CommonBL
    Private EntryMode As String
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Shared M_AccHead As String
    Shared ParameterDt As DataTable
    Shared NomineeTbl As New DataTable("Table")
    Dim FormID As Integer = 1070
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



            TxtOpDate.Attributes.Add("readonly", "readonly")
            TxtOpDate.Text = Session("TransDate")
            Assign_Values()
        End If
    End Sub
    Private Sub Assign_Values()

        Dim M_CommonBL As New CommonBL

        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbOpMode, 43, 0)
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
        M_CommonBL.Fill_Branch(CmbStandBr)
        M_CommonBL.GetSchemes(CmbStandSch, Session("TransDate"), "DEPOSIT", "SB")

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")
        CmbOpMode.SelectedValue = Session("AcOpenOpMode")

        M_CommonBL.Fill_ChittyGroup(CmbGroupName, CmbScheme.SelectedValue, CmbBranch.SelectedValue)

        M_AccHead = M_CommonBL.GetAccountCode(CmbScheme.SelectedValue)

        TxtNoOfOperator.Text = Val(Session("AcOpenNoOfOper"))
        Dim dt As DataTable = DirectCast(Session("AccHolderTbl"), DataTable)

        HdnMemBrCode.Value = dt.Rows.Item(0)(1).ToString()
        HdnMemType.Value = dt.Rows.Item(0)(2).ToString()
        HdnMemNo.Value = dt.Rows.Item(0)(4).ToString()

        DgvCustomer.DataSource = dt
        DgvCustomer.DataBind()

        Dim HeadDt As DataTable
        HeadDt = M_CommonBL.Fill_SchemeHeads(Session("AcOpenScheme"), "P")
        Session("locAcHeadTbl") = HeadDt
        DgvTransaction.DataSource = HeadDt
        DgvTransaction.DataBind()
        CmbCashTransfer.Items.Add("CASH")
        CmbCashTransfer.Items.Add("TRANSFER")
        CmbCashTransfer.SelectedIndex = 0

        Ddl_SI.SelectedIndex = 0

        CmbNomReq.Items.Add("No")
        CmbNomReq.Items.Add("Yes")
        CmbNomReq.SelectedIndex = 0

        AgentCollection.Value = "N"


    End Sub

    Protected Sub TxtInitialDep_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtInitialDep.TextChanged
        Set_TransactionGrid()
        Dim M_CommonCls As New CommonClass
        LblWords.Text = M_CommonCls.Towords(Val(TxtInitialDep.Text)).ToUpper
        M_CommonCls = Nothing
        TxtInitialDep.Text = Format(Val(TxtInitialDep.Text), "#0.00")
    End Sub

    Protected Sub CmbCashTransfer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCashTransfer.SelectedIndexChanged
        Set_TransactionGrid()
    End Sub

    Protected Sub Ddl_SI_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_SI.SelectedIndexChanged
        If Ddl_SI.SelectedIndex = 1 Then
            StandDiv.Visible = True
        Else
            StandDiv.Visible = False
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

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        If Validate_Control() = False Then
            Exit Sub
        End If

        Dim M_GroupDepositMaster As New SMARTCORE_BO.GroupDepositMasterBO
        With M_GroupDepositMaster
            .AccNo = ""
            .SchCode = CmbScheme.SelectedValue
            .BrCode = CmbBranch.SelectedValue
            .AccCode = M_AccHead
            .AccID = M_AccID
            .CustId = Val(DgvCustomer.Rows(0).Cells(4).Text)
            .GDCode = CmbGroupName.SelectedValue
            .GDShrBrCode = HdnMemBrCode.Value
            .GDShrSchCode = HdnMemType.Value
            .GDMemNo = HdnMemNo.Value
            .GDDepDate = TxtOpDate.Text
            .GDInitialDeposit = Val(TxtInitialDep.Text)
            .GDStatus = "Y"
            .GDOPBalance = 0
            .GDOPBalType = "C"
            .Authorised = "N"
            .TranNo = ""
            .MakerID = Session("UserID")
            .CheckerID = ""
            .MakingTime = DateTime.Now
            .CheckingTime = "01/01/1900"
            .SubAccNo = 0
            .SubInstAmt = 0
            .GDSSubPurpose = 0
            .GDSPurpose = 0
            .GDSSecurityDetails = ""
            .GDNomNos = IIf(NomDiv.Visible = False, 0, 1)
            .GDAgentCode = Val(CmbAgent.SelectedValue)
            If Ddl_SI.SelectedIndex = 0 Then
                .StBrCode = ""
                .StSchCode = ""
                .StAccNo = "'"
            Else
                .StBrCode = CmbStandBr.SelectedValue
                .StSchCode = CmbStandSch.SelectedValue
                .StAccNo = TxtStandAcNo.Text
            End If

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

            Dim M_GroupDepositMasterBL As New GroupDepositMasterBL
            Dim M_Dt As New DataTable

            Session("M_EntryMode") = "NEW"
            M_Dt = M_GroupDepositMasterBL.Save_Data(M_GroupDepositMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), _
                        M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))
            ShowAlertMessage(" GROUP DEPOSIT NO : " & M_Dt.Rows(0)("Acc_No").ToString)
            Session("NomTbl") = Nothing

            Response.Redirect("~/NewAccount.aspx?AccNo=" & M_Dt.Rows(0)("Acc_No").ToString & "&TransId=" & M_Dt.Rows(0)("Tran_No").ToString & "&PrintBond=N" & "&SECTION=GROUP DEPOSIT&SCHEME=" & CmbScheme.SelectedValue & "&SEC=GROUP DEPOSIT&DRCR=" & "C")
            M_GroupDepositMasterBL = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_TransactionGrid()
        Dim M_CommonBL As New CommonBL
        Dim Ds As DataSet
        Ds = M_CommonBL.Set_TransactionGrid(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtOpDate.Text, IIf(CmbCashTransfer.SelectedIndex = 0, "C", "T"), "C", "M", Val(TxtInitialDep.Text))
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


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Function Validate_Control() As Boolean

        If Val(TxtInitialDep.Text) <= 0 Then
            TxtInitialDep.Focus()
            ShowAlertMessage("INVALID DEPOSIT AMOUNT ENTERED")
            Return False
        End If
       
        If AgentCollection.Value = "Y" And CmbAgent.SelectedIndex = CmbAgent.Items.Count - 1 Then
            ShowAlertMessage("INVALID AGENT SELECTED")
            Return False
        End If

        Return True
    End Function
End Class