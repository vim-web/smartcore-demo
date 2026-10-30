Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Partial Public Class RptOutstanding
    Inherits System.Web.UI.Page
    Dim M_Sql As String
    Dim M_ReportBl As New ReportsBL
    Private Shared CommCls As New CommonClass()
    Private Shared Rpt_Qry As String
    Dim M_CommonDa As New CommonDA
    Private Shared Company_Name As String
    Dim StrSlf As String
    Dim OpCash As Double
    Dim ClCash As Double
    Dim Dt As DataTable
    Private Shared M_DBConn As New Connection
    Dim FormID As Integer = 1123
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/SmartLogin.aspx")
        End If
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Dim M_CommonDa As New CommonDA
            Company_Name = M_CommonDa.GetCompanyDetails()
            M_CommonDa = Nothing
            Load_DefaultValues()
            TxtAsOnDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
            Call RdoOutstanding_SelectedIndexChanged(Me.RdoOutstanding, System.EventArgs.Empty)

       

            RdoOutstanding_SelectedIndexChanged(sender, e)

        End If

    End Sub

    Protected Sub BtnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPro.Click
        Try
            If RdoOutstanding.Items(0).Selected Then
                ProcessOutstanding()
            ElseIf RdoOutstanding.Items(1).Selected Then
                ProcessOutstandingSchedule()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub ProcessOutstanding()
        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable()

        Dim M_RptTblSw As New IO.StringWriter
        Dim RptTbl As New DataTable("Table")
        RptTbl.Columns.Add("Company", GetType(String))
        RptTbl.Columns.Add("Br_Name", GetType(String))
        RptTbl.Columns.Add("UserID", GetType(String))
        RptTbl.Columns.Add("IsDetailed", GetType(String))
        RptTbl.Columns.Add("WithInt", GetType(String))
        RptTbl.Columns.Add("WithAgent", GetType(String))

        Dim IsDetailed As String = IIf(RDoDetailed.Items(0).Selected = True, "N", "Y")
        Dim WithInt As String = IIf(ChkAccrued.Checked = True, "Y", "N")
        Dim WithAgent As String = IIf(ChkAgent.Checked = True, "Y", "N")

        Dim Dr As DataRow
        Dr = RptTbl.Rows.Add
        Dr("Company") = IIf(Company_Name = Nothing, "", Company_Name)
        Dr("Br_Name") = DDlBranch.SelectedItem.Text
        Dr("UserID") = Trim(Session("UserID").ToString)
        Dr("IsDetailed") = IsDetailed
        Dr("WithInt") = WithInt
        Dr("WithAgent") = WithAgent

        If DDlSection.Text = "SHARE" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "SHARE DIVIDEND" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If

        ElseIf DDlSection.Text = "LOAN" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "SUSPENSE" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "INVESTMENT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "BORROWINGS" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "BANKACCOUNT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & DDlType.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "GROUP DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "GOVT SECURITY" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Process_Outstand_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y','" & Session("UserID") & "'"
            End If
            ''ElseIf DDlType.Text = "PROPERTY LOAN" Then
            ''    RptTbl.WriteXml(M_RptTblSw)

            ''    If RdoOutstanding.Items(0).Selected = True Then
            ''        M_Sql = "EXEC Outstand_Section_Wise_Sp '" + DDlType.SelectedValue + "','" + CommCls.DbDate(TxtAsOnDate.Text) + "','" + DDlBranch.SelectedValue + "'"
            ''    End If

        End If
        'DepositOutStandingSchedule()
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
    End Sub


    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        Dim M_dt As New DataTable
        M_dt = M_CommonBL.Get_Pkc_Outstand()
        DDlType.DataTextField = "Pkc_Description"
        DDlType.DataValueField = "Pkc_Code"
        DDlType.DataSource = M_dt
        DDlType.DataBind()
        DDlType.Items.Add("ALL")
        M_CommonBL.Fill_Section(DDlSection)
        DDlSection.Items.Add("SHARE DIVIDEND")
        If DDlSection.Items.Count > 0 Then DDlSection.SelectedIndex = 0
        Call DDlSection_SelectedIndexChanged(Me.DDlSection, System.EventArgs.Empty)
        M_CommonBL.GetSchemes(DDlScheme, Session("TransDate"))
        If DDlScheme.Items.Count > 0 Then DDlScheme.SelectedIndex = 0
        'M_CommonBL.Fill_Branch(DDlBranch, True)
        ''Region Wise
        M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
        M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
        M_CommonBL.Fill_UserWiseBranch(DDlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
        ''Region Wise
        DDlBranch.Items.Remove(DDlBranch.Items.FindByText("ALL"))
        DDlBranch.Items.Add("<-- ALL -->")
        DDlBranch.SelectedValue = Session("DefaultBrCode")

        M_CommonBL.Fill_Pickup(ddl_IndivInst, 6, 0)
        ddl_IndivInst.Items.Add("-- ALL --")
        ddl_IndivInst.SelectedIndex = ddl_IndivInst.Items.Count - 1

        M_CommonBL.Fill_Pickup(ddl_Gender, 8, 0)
        ddl_Gender.Items.Add("-- ALL --")
        ddl_Gender.SelectedIndex = ddl_Gender.Items.Count - 1

        M_CommonBL.Fill_Pickup(ddl_Religion, 1, 0)
        ddl_Religion.Items.Add("-- ALL --")
        ddl_Religion.SelectedIndex = ddl_Religion.Items.Count - 1
        Call ddl_Religion_SelectedIndexChanged(Me.ddl_Religion, System.EventArgs.Empty)

        M_CommonBL.Fill_Pickup(ddl_Occupation, 4, 0)
        ddl_Occupation.Items.Add("-- ALL --")
        ddl_Occupation.SelectedIndex = ddl_Occupation.Items.Count - 1

        M_CommonBL.Fill_Pickup(ddl_Amsom, 81, 0)
        ddl_Amsom.Items.Add("-- ALL --")
        ddl_Amsom.SelectedIndex = ddl_Amsom.Items.Count - 1

        M_CommonBL.Fill_Pickup(ddl_desom, 82, 0)
        ddl_desom.Items.Add("-- ALL --")
        ddl_desom.SelectedIndex = ddl_desom.Items.Count - 1
        M_CommonBL.Fill_SangalDetails(ddl_Sangam, DDlBranch.SelectedValue)
        ddl_Sangam.Items.Add("-- ALL --")
        ddl_Sangam.SelectedIndex = ddl_Sangam.Items.Count - 1

        M_CommonBL.Fill_Agent(DDlAgent, CommonBL.AgentType.DDAgent, DDlBranch.SelectedValue)
        M_CommonBL = Nothing
        RDoDetailed.Items(1).Selected = True
        RdoOutstanding.Items(0).Selected = True

    End Sub

    Protected Sub BtnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Show.Click
        Try
            'Dim Type As String = DDlType.SelectedValue
            'Dim As_On_Date As String = TxtAsOnDate.Text
            'Dim Br_Code As String = DDlBranch.SelectedValue

            'Dim M_CommanBL As New CommonBL
            'Dim M_Datatable As New DataTable()
            'M_Datatable = M_CommanBL.Get_OutstandRpt(Type, As_On_Date, Br_Code)

            If ValidateFields() = False Then
                Exit Sub
            End If

            If RdoOutstanding.Items(0).Selected Then
                ShowOutstanding()
            ElseIf RdoOutstanding.Items(1).Selected Then
                ShowOutandingSchedule()
            Else If RdoOutstanding.Items(2).Selected Then
                PrintDocument()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
     


    Private Sub ShowOutstanding()
        Dim M_DBConn As New Connection
        Dim M_Ds As New DataSet
        Dim CommonCls As New CommonClass

        Dim M_RptTblSw As New IO.StringWriter
        Dim RptTbl As New DataTable("Table")
        RptTbl.Columns.Add("Company", GetType(String))
        RptTbl.Columns.Add("Br_Name", GetType(String))
        RptTbl.Columns.Add("UserID", GetType(String))
        RptTbl.Columns.Add("IsDetailed", GetType(String))
        RptTbl.Columns.Add("WithInt", GetType(String))
        RptTbl.Columns.Add("WithAgent", GetType(String))
        RptTbl.Columns.Add("WithAddress", GetType(String))
        RptTbl.Columns.Add("AgentID", GetType(Double))
        RptTbl.Columns.Add("CustType", GetType(Double))
        RptTbl.Columns.Add("Gender", GetType(Double))
        RptTbl.Columns.Add("Religion", GetType(Double))
        RptTbl.Columns.Add("Caste", GetType(Double))
        RptTbl.Columns.Add("BalanceFrom", GetType(Double))
        RptTbl.Columns.Add("BalanceTo", GetType(Double))
        RptTbl.Columns.Add("AdmissionDateFrom", GetType(Date))
        RptTbl.Columns.Add("AdmissionDateTo", GetType(Date))
        RptTbl.Columns.Add("Occupation", GetType(Double))
        RptTbl.Columns.Add("Amsom", GetType(Double))
        RptTbl.Columns.Add("Desom", GetType(Double))
        RptTbl.Columns.Add("RptWith", GetType(String))
        RptTbl.Columns.Add("Sangam", GetType(String))

        Dim IsDetailed As String = IIf(RDoDetailed.Items(0).Selected = True, "N", "Y")
        Dim WithInt As String = IIf(ChkAccrued.Checked = True, "Y", "N")
        Dim WithAgent As String = IIf(ChkAgent.Checked = True, "Y", "N")
        Dim WithAddress As String = IIf(ChkWithAddress.Checked = True, "Y", "N")

        Dim Dr As DataRow
        Dr = RptTbl.Rows.Add
        Dr("Company") = IIf(Company_Name = Nothing, "", Company_Name)
        Dr("Br_Name") = DDlBranch.SelectedItem.Text
        Dr("UserID") = Trim(Session("UserID").ToString)
        Dr("IsDetailed") = IsDetailed
        Dr("WithInt") = WithInt
        Dr("WithAgent") = WithAgent
        Dr("WithAddress") = WithAddress
        Dr("AgentID") = Val(DDlAgent.SelectedValue)
        Dr("CustType") = Val(ddl_IndivInst.SelectedValue)
        Dr("Gender") = Val(ddl_Gender.SelectedValue)
        Dr("Religion") = Val(ddl_Religion.SelectedValue)
        Dr("Caste") = Val(ddl_Caste.SelectedValue)
        Dr("BalanceFrom") = Val(txt_BalAmtFr.Text)
        Dr("BalanceTo") = Val(txt_BalAmtTo.Text)
        Dr("AdmissionDateFrom") = IIf(Ce_AddmFrDate.Text.Trim.Length = 0, "01/01/1900", Ce_AddmFrDate.Text)
        Dr("AdmissionDateTo") = IIf(Ce_AddmToDate.Text.Trim.Length = 0, "01/01/1900", Ce_AddmToDate.Text)
        Dr("Occupation") = Val(ddl_Occupation.SelectedValue)
        Dr("Amsom") = Val(ddl_Amsom.SelectedValue)
        Dr("Desom") = Val(ddl_desom.SelectedValue)
        If CmbRptWith.Items.Count = 0 Then
            Dr("RptWith") = ""
        Else
            Dr("RptWith") = CmbRptWith.SelectedItem.Text
        End If

        Dr("Sangam") = Val(ddl_Sangam.SelectedValue)

        If DDlSection.Text = "SHARE" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "SHARE DIVIDEND" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding


                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"


            End If
        ElseIf DDlSection.Text = "LOAN" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "SUSPENSE" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "INVESTMENT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "BORROWINGS" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "BANKACCOUNT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "GROUP DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
        ElseIf DDlSection.Text = "GOVT SECURITY" Then
            RptTbl.WriteXml(M_RptTblSw)

            If RdoOutstanding.Items(0).Selected = True Then ' Outstanding
                M_Sql = "Exec Show_OutstandRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
            End If
            ''ElseIf DDlType.Text = "PROPERTY LOAN" Then
            ''    RptTbl.WriteXml(M_RptTblSw)

            ''    If RdoOutstanding.Items(0).Selected = True Then
            ''        M_Sql = "EXEC Outstand_Section_Wise_Sp '" + DDlType.SelectedValue + "','" + CommCls.DbDate(TxtAsOnDate.Text) + "','" + DDlBranch.SelectedValue + "'"
            ''    End If
        End If

        M_DBConn.ExecuteDataSet(M_Ds, M_Sql, False)

        Dim HeaderArr(20, 4) As String
        Dim I As Integer

        If M_Ds.Tables.Count >= 2 Then
            For I = 0 To M_Ds.Tables(1).Rows.Count - 1
                HeaderArr(I, 0) = M_Ds.Tables(1).Rows(I)("HeadStr").ToString
                HeaderArr(I, 1) = M_Ds.Tables(1).Rows(I)("HeadAlign").ToString
                HeaderArr(I, 2) = M_Ds.Tables(1).Rows(I)("HeadGrWdith").ToString
                HeaderArr(I, 3) = M_Ds.Tables(1).Rows(I)("HeadPrWdith").ToString
            Next

            Session("RptHeader") = HeaderArr

            Session("RptQry") = M_Ds.Tables(0).Rows(0)("RptQry").ToString
            Response.Redirect("~/PrnView.aspx?PageType=" & M_Ds.Tables(0).Rows(0)("RptPaperSize").ToString, False)

        End If

        'If M_Dt.Rows.Count > 0 Then
        '    Session.Add("RptPath", Server.MapPath("~/Reports/" & M_Dt.Rows(0)("RptName").ToString))
        '    Session.Add("RptQry", M_Dt.Rows(0)("RptQry").ToString)
        '    Session.Add("RptFormula", M_Dt.Rows(0)("StrFormula").ToString)

        '    ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")

        '    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'End If
    End Sub

    Private Sub ShowOutandingSchedule()
        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable
        Dim CommonCls As New CommonClass

        Dim M_RptTblSw As New IO.StringWriter
        Dim RptTbl As New DataTable("Table")
        RptTbl.Columns.Add("Company", GetType(String))
        RptTbl.Columns.Add("Br_Name", GetType(String))
        RptTbl.Columns.Add("UserID", GetType(String))
        RptTbl.Columns.Add("IsDetailed", GetType(String))
        RptTbl.Columns.Add("WithInt", GetType(String))
        RptTbl.Columns.Add("WithAgent", GetType(String))
        RptTbl.Columns.Add("WithAddress", GetType(String))
        RptTbl.Columns.Add("AgentID", GetType(Double))
        RptTbl.Columns.Add("CustType", GetType(Double))
        RptTbl.Columns.Add("Gender", GetType(Double))
        RptTbl.Columns.Add("Religion", GetType(Double))
        RptTbl.Columns.Add("Caste", GetType(Double))
        RptTbl.Columns.Add("BalanceFrom", GetType(Double))
        RptTbl.Columns.Add("BalanceTo", GetType(Double))
        RptTbl.Columns.Add("AdmissionDateFrom", GetType(Date))
        RptTbl.Columns.Add("AdmissionDateTo", GetType(Date))
        RptTbl.Columns.Add("Occupation", GetType(Double))
        RptTbl.Columns.Add("Amsom", GetType(Double))
        RptTbl.Columns.Add("Desom", GetType(Double))
        RptTbl.Columns.Add("RptWith", GetType(String))


        Dim IsDetailed As String = IIf(RDoDetailed.Items(0).Selected = True, "N", "Y")
        Dim WithInt As String = IIf(ChkAccrued.Checked = True, "Y", "N")
        Dim WithAgent As String = IIf(ChkAgent.Checked = True, "Y", "N")
        Dim WithAddress As String = IIf(ChkWithAddress.Checked = True, "Y", "N")

        Dim Dr As DataRow
        Dr = RptTbl.Rows.Add
        Dr("Company") = IIf(Company_Name = Nothing, "", Company_Name)
        Dr("Br_Name") = DDlBranch.SelectedItem.Text
        Dr("UserID") = Trim(Session("UserID").ToString)
        Dr("IsDetailed") = IsDetailed
        Dr("WithInt") = WithInt
        Dr("WithAgent") = WithAgent
        Dr("WithAddress") = WithAddress
        Dr("AgentID") = Val(DDlAgent.SelectedValue)
        Dr("CustType") = Val(ddl_IndivInst.SelectedValue)
        Dr("Gender") = Val(ddl_Gender.SelectedValue)
        Dr("Religion") = Val(ddl_Religion.SelectedValue)
        Dr("Caste") = Val(ddl_Caste.SelectedValue)
        Dr("BalanceFrom") = Val(txt_BalAmtFr.Text)
        Dr("BalanceTo") = Val(txt_BalAmtTo.Text)
        Dr("AdmissionDateFrom") = IIf(Ce_AddmFrDate.Text.Trim.Length = 0, "01/01/1900", Ce_AddmFrDate.Text)
        Dr("AdmissionDateTo") = IIf(Ce_AddmToDate.Text.Trim.Length = 0, "01/01/1900", Ce_AddmToDate.Text)
        Dr("Occupation") = Val(ddl_Occupation.SelectedValue)
        Dr("Amsom") = Val(ddl_Amsom.SelectedValue)
        Dr("Desom") = Val(ddl_desom.SelectedValue)
        If CmbRptWith.Items.Count = 0 Then
            Dr("RptWith") = ""
        Else
            Dr("RptWith") = CmbRptWith.SelectedItem.Text
        End If

        If DDlSection.Text = "SHARE" Then

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "SCHEME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "20"

            HeaderArr(1, 0) = "Open Nos"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Open Bal"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "14"

            HeaderArr(3, 0) = "Rct No"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "6"

            HeaderArr(4, 0) = "Rct Amount"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "14"

            HeaderArr(5, 0) = "Pay No"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "7"


            HeaderArr(6, 0) = "Pay Amount"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "14"

            HeaderArr(7, 0) = "Cl Nos"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "7"


            HeaderArr(8, 0) = "Cl Amount"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "14"

            Session("RptHeader") = HeaderArr
            Dim M_Sql1 As String
            M_Sql1 = "Exec [Share_Schedule_Sp] '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sql1
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

            Exit Sub

        ElseIf DDlSection.Text = "SUSPENSE" Then

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "SCHEME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "20"

            HeaderArr(1, 0) = "Open Nos"
            HeaderArr(1, 1) = "R"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Open Bal"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "14"

            HeaderArr(3, 0) = "Rct No"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "6"

            HeaderArr(4, 0) = "Rct Amount"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "14"

            HeaderArr(5, 0) = "Pay No"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "7"


            HeaderArr(6, 0) = "Pay Amount"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "14"

            HeaderArr(7, 0) = "Cl Nos"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "7"


            HeaderArr(8, 0) = "Cl Amount"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "14"

            Session("RptHeader") = HeaderArr
            Dim M_Sqls As String
            M_Sqls = "Exec Suspense_Schedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sqls
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

            Exit Sub

        ElseIf DDlSection.Text = "GROUP DEPOSIT" Then
            If RDoDetailed.Items(0).Selected = False Then
                If CmbRptWith.SelectedIndex = 0 Or CmbRptWith.SelectedIndex = 1 Then
                    Dim HeaderArr(10, 4) As String
                    Dim HeaderWidth(10, 1) As String


                    HeaderArr(0, 0) = "Scheme Name"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "10"
                    HeaderArr(0, 3) = "10"

                    HeaderArr(1, 0) = "Acc No"
                    HeaderArr(1, 1) = "L"
                    HeaderArr(1, 2) = "10"
                    HeaderArr(1, 3) = "10"

                    HeaderArr(2, 0) = "Receipt Uptoyear"
                    HeaderArr(2, 1) = "R"
                    HeaderArr(2, 2) = "10"
                    HeaderArr(2, 3) = "14"

                    HeaderArr(3, 0) = "Receipt DuringYear"
                    HeaderArr(3, 1) = "R"
                    HeaderArr(3, 2) = "5"
                    HeaderArr(3, 3) = "6"

                    HeaderArr(4, 0) = "Total"
                    HeaderArr(4, 1) = "R"
                    HeaderArr(4, 2) = "10"
                    HeaderArr(4, 3) = "14"

                    HeaderArr(5, 0) = "Payment Uptoyear"
                    HeaderArr(5, 1) = "R"
                    HeaderArr(5, 2) = "5"
                    HeaderArr(5, 3) = "7"


                    HeaderArr(6, 0) = "Payment DuringAmount"
                    HeaderArr(6, 1) = "R"
                    HeaderArr(6, 2) = "10"
                    HeaderArr(6, 3) = "14"

                    HeaderArr(7, 0) = "Total"
                    HeaderArr(7, 1) = "R"
                    HeaderArr(7, 2) = "5"
                    HeaderArr(7, 3) = "7"


                    HeaderArr(8, 0) = "Asset"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "10"
                    HeaderArr(8, 3) = "14"

                    HeaderArr(9, 0) = "Liability"
                    HeaderArr(9, 1) = "R"
                    HeaderArr(9, 2) = "10"
                    HeaderArr(9, 3) = "14"

                    Session("RptHeader") = HeaderArr
                    Dim M_Sql As String
                    M_Sql = "Exec GroupdepositSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("HostId") & "'," & CmbRptWith.SelectedIndex & ",'" & Session("UserID") & "'"
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)

                ElseIf CmbRptWith.SelectedIndex = 2 Then
                    Dim HeaderArr(7, 4) As String
                    Dim HeaderWidth(7, 1) As String
                    'HeaderArr(0, 0) = "Slno"
                    'HeaderArr(0, 1) = "L"
                    'HeaderArr(0, 2) = "10"
                    'HeaderArr(0, 3) = "20"

                    HeaderArr(0, 0) = "Branch Name"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "10"
                    HeaderArr(0, 3) = "10"

                    HeaderArr(1, 0) = "Receipt Uptoyear"
                    HeaderArr(1, 1) = "R"
                    HeaderArr(1, 2) = "10"
                    HeaderArr(1, 3) = "14"

                    HeaderArr(2, 0) = "Receipt DuringYear"
                    HeaderArr(2, 1) = "R"
                    HeaderArr(2, 2) = "5"
                    HeaderArr(2, 3) = "6"

                    HeaderArr(3, 0) = "Total"
                    HeaderArr(3, 1) = "R"
                    HeaderArr(3, 2) = "10"
                    HeaderArr(3, 3) = "14"

                    HeaderArr(4, 0) = "Payment UptoYear"
                    HeaderArr(4, 1) = "R"
                    HeaderArr(4, 2) = "5"
                    HeaderArr(4, 3) = "7"


                    HeaderArr(5, 0) = "Payment DuringAmount"
                    HeaderArr(5, 1) = "R"
                    HeaderArr(5, 2) = "10"
                    HeaderArr(5, 3) = "14"

                    HeaderArr(6, 0) = "T"
                    HeaderArr(6, 1) = "R"
                    HeaderArr(6, 2) = "5"
                    HeaderArr(6, 3) = "7"



                    Session("RptHeader") = HeaderArr
                    Dim M_Sql As String

                    M_Sql = "Exec GroupdepositSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "'," & CmbRptWith.SelectedIndex & ",'" & Session("UserID") & "'"
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)
                End If
            Else
                If CmbRptWith.SelectedIndex = 0 Or CmbRptWith.SelectedIndex = 1 Then
                    Dim HeaderArr(10, 4) As String
                    Dim HeaderWidth(10, 1) As String


                    HeaderArr(0, 0) = "Scheme Name"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "10"
                    HeaderArr(0, 3) = "10"

                    HeaderArr(1, 0) = "Receipt Uptoyear"
                    HeaderArr(1, 1) = "R"
                    HeaderArr(1, 2) = "10"
                    HeaderArr(1, 3) = "10"

                    HeaderArr(2, 0) = "Receipt DuringYear"
                    HeaderArr(2, 1) = "R"
                    HeaderArr(2, 2) = "10"
                    HeaderArr(2, 3) = "14"

                    HeaderArr(3, 0) = "Total"
                    HeaderArr(3, 1) = "R"
                    HeaderArr(3, 2) = "5"
                    HeaderArr(3, 3) = "6"

                    HeaderArr(4, 0) = "Payment Uptoyear"
                    HeaderArr(4, 1) = "R"
                    HeaderArr(4, 2) = "10"
                    HeaderArr(4, 3) = "14"

                    HeaderArr(5, 0) = "Payment DuringAmount"
                    HeaderArr(5, 1) = "R"
                    HeaderArr(5, 2) = "5"
                    HeaderArr(5, 3) = "7"


                    HeaderArr(6, 0) = "Total"
                    HeaderArr(6, 1) = "R"
                    HeaderArr(6, 2) = "10"
                    HeaderArr(6, 3) = "14"

                    HeaderArr(7, 0) = "Asset"
                    HeaderArr(7, 1) = "R"
                    HeaderArr(7, 2) = "5"
                    HeaderArr(7, 3) = "7"


                    HeaderArr(8, 0) = "Liability"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "10"
                    HeaderArr(8, 3) = "14"


                    Session("RptHeader") = HeaderArr
                    Dim M_Sql As String
                    M_Sql = "Exec GroupdepositScheduleSummary_Sp '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("HostId") & "'," & CmbRptWith.SelectedIndex & ",'" & Session("UserID") & "'"
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)

                ElseIf CmbRptWith.SelectedIndex = 2 Then
                    Dim HeaderArr(7, 4) As String
                    Dim HeaderWidth(7, 1) As String
                    'HeaderArr(0, 0) = "Slno"
                    'HeaderArr(0, 1) = "L"
                    'HeaderArr(0, 2) = "10"
                    'HeaderArr(0, 3) = "20"

                    HeaderArr(0, 0) = "Branch Name"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "10"
                    HeaderArr(0, 3) = "10"

                    HeaderArr(1, 0) = "Receipt Uptoyear"
                    HeaderArr(1, 1) = "R"
                    HeaderArr(1, 2) = "10"
                    HeaderArr(1, 3) = "14"

                    HeaderArr(2, 0) = "Receipt DuringYear"
                    HeaderArr(2, 1) = "R"
                    HeaderArr(2, 2) = "5"
                    HeaderArr(2, 3) = "6"

                    HeaderArr(3, 0) = "Total"
                    HeaderArr(3, 1) = "R"
                    HeaderArr(3, 2) = "10"
                    HeaderArr(3, 3) = "14"

                    HeaderArr(4, 0) = "Payment UptoYear"
                    HeaderArr(4, 1) = "R"
                    HeaderArr(4, 2) = "5"
                    HeaderArr(4, 3) = "7"


                    HeaderArr(5, 0) = "Payment DuringAmount"
                    HeaderArr(5, 1) = "R"
                    HeaderArr(5, 2) = "10"
                    HeaderArr(5, 3) = "14"

                    HeaderArr(6, 0) = "T"
                    HeaderArr(6, 1) = "R"
                    HeaderArr(6, 2) = "5"
                    HeaderArr(6, 3) = "7"



                    Session("RptHeader") = HeaderArr
                    Dim M_Sql As String

                    M_Sql = "Exec GroupdepositScheduleSummary_Sp'" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "'," & CmbRptWith.SelectedIndex & ",'" & Session("UserID") & "'"
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)

                End If
            End If
            Exit Sub


            'RptTbl.WriteXml(M_RptTblSw)

            'M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
            '        CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "'"
        ElseIf DDlSection.Text = "DEPOSIT" Then



            Dim HeaderArr(12, 4) As String
            Dim HeaderWidth(12, 1) As String
            HeaderArr(0, 0) = "SCHEME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "20"

            HeaderArr(1, 0) = "Open Nos"
            HeaderArr(1, 1) = "R"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Open Bal"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "14"

            HeaderArr(3, 0) = "Rct No"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "6"

            HeaderArr(4, 0) = "Rct Amount"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "14"

            HeaderArr(5, 0) = "Pay No"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "7"


            HeaderArr(6, 0) = "Pay Amount"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "14"

            HeaderArr(7, 0) = "Cl Nos"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "7"


            HeaderArr(8, 0) = "Cl Amount"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "14"


            If CmbRptWith.SelectedIndex = 1 Then

                HeaderArr(9, 0) = "No(+/-)"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "5"
                HeaderArr(9, 3) = "7"

                HeaderArr(10, 0) = "Amt(+/-)"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "14"

                HeaderArr(11, 0) = "Int Amount"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "10"
                HeaderArr(11, 3) = "14"

                HeaderArr(12, 0) = "Mat Amount"
                HeaderArr(12, 1) = "R"
                HeaderArr(12, 2) = "10"
                HeaderArr(12, 3) = "14"
            Else
                HeaderArr(9, 0) = "Int Amount"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "10"
                HeaderArr(9, 3) = "14"

                HeaderArr(10, 0) = "Mat Amount"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "14"
            End If

            Session("RptHeader") = HeaderArr
            Dim M_Sql As String
            M_Sql = "Exec [Deposit_Schedule_Sp] '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'," & CmbRptWith.SelectedIndex & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

            Exit Sub




            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "LOAN" Then

            Dim HeaderArr(13, 4) As String
            Dim HeaderWidth(13, 1) As String
            HeaderArr(0, 0) = "SCHEME"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "18"

            HeaderArr(1, 0) = "Open No"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Open Bal"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "14"

            HeaderArr(3, 0) = "Pay No"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "6"

            HeaderArr(4, 0) = "Pay Amt"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "13"

            HeaderArr(5, 0) = "Rct No"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "5"
            HeaderArr(5, 3) = "6"


            HeaderArr(6, 0) = "Rct Amt"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "14"

            HeaderArr(7, 0) = "Cl No"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "6"


            HeaderArr(8, 0) = "Closing Amt"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "14"

            HeaderArr(9, 0) = "OD No"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "5"
            HeaderArr(9, 3) = "6"


            HeaderArr(10, 0) = "OD Amount"
            HeaderArr(10, 1) = "R"
            HeaderArr(10, 2) = "10"
            HeaderArr(10, 3) = "13"

            HeaderArr(11, 0) = "Demand"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "10"
            HeaderArr(11, 3) = "13"

            HeaderArr(12, 0) = "Int Amt"
            HeaderArr(12, 1) = "R"
            HeaderArr(12, 2) = "10"
            HeaderArr(12, 3) = "13"

            HeaderArr(13, 0) = "OD.Int"
            HeaderArr(13, 1) = "R"
            HeaderArr(13, 2) = "10"
            HeaderArr(13, 3) = "13"

            Session("RptHeader") = HeaderArr
            Dim M_Sql
            M_Sql = "Exec [Loan_Schedule_Sp] '" & DDlBranch.SelectedValue & "','" & DDlScheme.SelectedValue & "','" & Format(CDate(TxtAsOnDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'," & CmbRptWith.SelectedIndex & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

            Exit Sub

            'RptTbl.WriteXml(M_RptTblSw)
            'M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
            '       CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "'"
        ElseIf DDlSection.Text = "SUSPENSE" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "INVESTMENT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "BORROWINGS" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "BANKACCOUNT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "GROUP DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "GOVT SECURITY" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Show_OutstandScheduleRpt_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                    CommonCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','" & Session("UserID") & "'"
        End If

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        If M_Dt.Rows.Count > 0 Then
            Session.Add("RptPath", Server.MapPath("~/Reports/" & M_Dt.Rows(0)("RptName").ToString))
            Session.Add("RptQry", M_Dt.Rows(0)("RptQry").ToString)
            Session.Add("RptFormula", M_Dt.Rows(0)("StrFormula").ToString)

            ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        End If
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub DDlSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        If DDlSection.SelectedItem.Text = "GROUP DEPOSIT" Then
            M_CommonBL.Fill_ChittyGroup(DDlScheme, , DDlBranch.SelectedValue, True)
        ElseIf DDlSection.SelectedItem.Text = "SHARE DIVIDEND" Then
            M_CommonBL.Fill_Section(DDlScheme, "SHARE")
        Else
            If DDlSection.SelectedItem.Text = "LOAN" Then
                LblType.Visible = True
                DDlType.Visible = True
                M_CommonBL.Fill_Section(DDlScheme, IIf(DDlSection.SelectedItem.Text = "-- ALL --", "0", DDlSection.SelectedItem.Text), DDlType.SelectedValue)
            Else
                M_CommonBL.Fill_Section(DDlScheme, IIf(DDlSection.SelectedItem.Text = "-- ALL --", "0", DDlSection.SelectedItem.Text))
            End If

        End If
        M_CommonBL = Nothing

        FillReportOptions(DDlSection.SelectedItem.Text)
    End Sub

    Private Sub FillReportOptions(ByVal Section As String)
        CmbRptWith.Items.Clear()
        If RdoOutstanding.Items(1).Selected Then  ' Schedule
            If Section = "GROUP DEPOSIT" Then
                CmbRptWith.Items.Add("Installment No Wise")
                CmbRptWith.Items.Add("Amount Wise")
                CmbRptWith.Items.Add("BranchWise Consolidation")
                RptType.Visible = True
            ElseIf Section = "DEPOSIT" Then
                'CmbRptWith.Items.Add("Mobilisation")
                'CmbRptWith.Items.Add("Branch Wise")
                'CmbRptWith.Items.Add("Grand Summary")

                CmbRptWith.Items.Add("Schedule")
                CmbRptWith.Items.Add("Mobilisation")
                RptType.Visible = True
            ElseIf Section = "LOAN" Then
                CmbRptWith.Items.Add("Schedule with Full OD")
                CmbRptWith.Items.Add("Schedule with Actual OD")
                CmbRptWith.Items.Add("Audit Loan Transaction List")
                CmbRptWith.Items.Add("Schedule Term wise")
                RptType.Visible = True
            Else
                RptType.Visible = False
            End If
        End If
    End Sub

    Protected Sub RDoDetailed_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RDoDetailed.SelectedIndexChanged

    End Sub


    Protected Sub DepositOutStandingSchedule()

        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable()
        Dim Count As Integer
        Dim IsDel As String = "Y"

        M_DBConn.ExecuteScalar("Delete From Deposit_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)



        If DDlScheme.SelectedValue = "<-- ALL -->" Then
            For Count = 0 To DDlScheme.Items.Count - 1
                '  M_Sql = "Exec [Deposit_Outstand_Sp] 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", "0", DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & ",'Y','" & Session("LoginID") & "','4784','N','Y'"
                M_Sql = "Deposit_Outstand_Shedule_Sp 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'N','" & Session("LoginID") & "','4784','" & IsDel & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                IsDel = "N"
            Next


        Else
            M_Sql = "Deposit_Outstand_Shedule_Sp 'N','" & DDlBranch.SelectedValue & "', 'S','" & DDlScheme.SelectedValue.ToString & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & IIf(ChkAccrued.Checked, "Y", "N") & "','" & Session("LoginID") & "','4784','" & IsDel & "'"
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
        End If


        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
        M_DBConn = Nothing
    End Sub

    Protected Sub ChkAccrued_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAccrued.CheckedChanged

    End Sub

    Protected Sub RdoOutstanding_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdoOutstanding.SelectedIndexChanged
        If RdoOutstanding.Items(0).Selected Then
            lblToDate.Visible = False
            TxtToDate.Visible = False
            BtnPro.Visible = True
            DivShare.Visible = True
            DDlSection.Visible = True
            DDlScheme.Visible = True
            DivCon.Visible = True
            lblAsonDate.Text = "As On Date"
            DDlType.Visible = False
            LblType.Visible = False
        ElseIf RdoOutstanding.Items(1).Selected Then
            ''BtnPro.Visible = False
            BtnPro.Visible = True
            lblToDate.Visible = True
            TxtToDate.Visible = True
            DivShare.Visible = False
            DivCon.Visible = False
            lblAsonDate.Text = "From Date"
            DDlType.Visible = False
            LblType.Visible = False
        ElseIf RdoOutstanding.Items(2).Selected Then
            lblAsonDate.Text = "As On Date"
            TxtToDate.Visible = False
            BtnPro.Visible = False
            DivCon.Visible = False
            DivShare.Visible = False
            DDlScheme.Visible = False
            lblToDate.Visible = False
            DDlType.Visible = True
        End If
        FillReportOptions(DDlSection.SelectedItem.Text)
    End Sub


    'Protected Sub Show_DepositOutstanding_Show()
    '    Dim RptName As String
    '    ' Rpt_Name = "\Reports\Deposit\DepositOutstandWithName.rpt"
    '    If RdoOutstanding.Items(0).Selected = True Then

    '        If DDlScheme.SelectedValue = "<-- ALL -->" Then

    '            M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            M_Sql = "SELECT B.Br_Name+'['+B.Br_Code + ']' AS Branch,S.Sch_Name AS Scheme,Count(*) AS OutstandingNo,Sum(Outstanding * Case When Bal_Type='C' Then 1 Else -1 End) AS Outstanding_Amt,Isnull(Sum(Accrued),0) AS Int_Amt, 0 AS OverdueNos,0 AS Overdue_Amt,0 AS Overdue_IntAmt,Sum(Isnull(Accrued,0)) As  Accrued FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code WHERE Terminal = '" & "4784" & "' GROUP BY S.Sch_Code,S.Sch_Name,B.Br_Name,B.Br_Code"
    '            Session.Add("RptQry", M_Sql)
    '            ' Rpt_Name = "\Reports\Deposit\Dep_OverueCompact.rpt"
    '            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Dep_OverueCompact.rpt")
    '            StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Transfer List As on: " & TxtAsOnDate.Text & " |User_ID| Sa"
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        Else
    '            M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784') AND M.Dm_Agent_Code LIKE '" & IIf(ChkAgent.Checked, IIf(DDlAgent.SelectedValue = "<-- ALL -->", "%%", DDlAgent.SelectedValue), "%%") & "' AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            If RDoDetailed.Items(1).Selected Then
    '                M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784')  AND M.Dm_Agent_Code LIKE '" & IIf(ChkAgent.Checked, IIf(DDlAgent.SelectedValue = "<-- ALL -->", "%%", DDlAgent.SelectedValue), "%%") & "' AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            Else
    '                M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784')  AND M.Dm_Agent_Code LIKE '" & IIf(ChkAgent.Checked, IIf(DDlAgent.SelectedValue = "<-- ALL -->", "%%", DDlAgent.SelectedValue), "%%") & "' AND(Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            End If

    '            Session.Add("RptQry", M_Sql)
    '            ' StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Transfer List As on: " & TxtAsOnDate.Text & " |User_ID| Sa"
    '            StrSlf = "Company|" & Company_Name & "|Chkadd1|" & "0" & "|Chkadd2|" & "0" & "|ChkPost|" & "0" & "|Branch|" & DDlBranch.SelectedItem.Text & " - Branch|Heading|Deposit Outstanding As on  " & TxtAsOnDate.Text & "" & " |User|" & Session("LoginID")
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
    '            Session.Add("RptFormula", StrSlf)
    '            If RDoDetailed.Items(1).Selected Then
    '                'Session.Add("RptPath", Session("File_Path") & "\" & "Reports\Deposit\DepositOutstandWithName.rpt")
    '                Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/DepositOutstandWithInt.rpt")
    '            Else
    '                Session.Add("RptPath", Session("File_Path") & "\" & "Reports\Deposit\DepositOutstandWithInt.rpt")
    '            End If



    '        End If
    '    Else

    '        If DDlScheme.SelectedValue = "<-- ALL -->" Then

    '            M_Sql = "SELECT M.Sch_Code,S.Sch_Name AS Sch_Name,Sch_Section_Name,M.Br_Code,OpNo ,OpAmt ,Rec_No ,Rec_Amt, Pay_No,Pay_Amt,Interest,Include_Int,B.Br_Name from " & _
    '        "(select Sch_Code,Br_Code,Sum(Case When Share_Capital>0 Then 1 Else 0 End) As OpNo," & _
    '        "Sum(Share_Capital) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Receipt) As Rec_Amt," & _
    '        "Sum(Closed_Between) As Pay_No,Sum(Payment) As Pay_Amt,0 As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int " & _
    '        "From Share_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '        "Group By Sch_Code,Br_Code " & _
    '        "Union " & _
    '        "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo," & _
    '        "Sum(Outstanding*Case When Bal_Type='C' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Dep_Receipt) As Rec_Amt," & _
    '        "Sum(Closed_Between) As Pay_No,Sum(Dep_Payment) As Pay_Amt,Sum(Accrued) As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & _
    '        "From Deposit_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '        "Group By Sch_Code,Br_Code " & _
    '        "Union " & _
    '        "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo, " & _
    '        "Sum(Outstanding*Case When BalType='D' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Loan_Receipt) As Rec_Amt," & _
    '        "Sum(Closed_Between) As Pay_No,Sum(Loan_Disburse) As Pay_Amt,Sum(Accrude_IntAmt) As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & _
    '        "From Loan_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '        "Group By Sch_Code,Br_Code "
    '            M_Sql = M_Sql & "Union " & Chr(13) & _
    '                    "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo, " & Chr(13) & _
    '                    "Sum(Outstanding*Case When Bal_Type='C' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Susp_Receipt) As Rec_Amt," & Chr(13) & _
    '                    "Sum(Closed_Between) As Pay_No,Sum(Susp_Payment) As Pay_Amt,0 As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & Chr(13) & _
    '                    "From Suspense_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtToDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & Chr(13) & _
    '                    "Group By Sch_Code,Br_Code) as M INNER JOIN Gen_SchemeMaster_P_Tbl S ON S.Sch_Code=M.Sch_Code " & Chr(13) & _
    '                    " Join Gen_BranchDetails_P_Tbl B On B.Br_Code=M.Br_Code "



    '            StrSlf = "Company|" & Company_Name & "|Branch|" & Trim(DDlBranch.SelectedItem.ToString) & " - Branch|Heading|Shedule Outstanding Between  " & TxtAsOnDate.Text & " And " & TxtToDate.Text & " " & " |User|" & Session("LoginID")
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
    '            Session.Add("RptFormula", StrSlf)
    '            Session.Add("RptQry", M_Sql)
    '            Session.Add("RptPath", Session("File_Path") & "\Reports\Shedule_Outstand_WithName.rpt")

    '            'Shedule_Outstand_WithName()
    '            'StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Gold Stock Register As On    Dated : " & TxtToDate.Text & " |User_ID| Sa"
    '            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '            'Session.Add("RptFormula", StrSlf)

    '        Else

    '            M_Sql = "SELECT Gen_SchemeMaster_P_Tbl.Sch_Name , Dep_Master_M_Tbl.Base_AccNo,Dep_Master_M_Tbl.Dm_Days,Dep_Master_M_Tbl.Dm_Months,Dep_Master_M_Tbl.Dm_IntRate ,Dep_Master_M_Tbl.Dm_DueDate, Deposit_Outstand_R_Tbl.*"
    '            M_Sql = M_Sql & " FROM Deposit_Outstand_R_Tbl INNER JOIN"
    '            M_Sql = M_Sql & " Dep_Master_M_Tbl ON Deposit_Outstand_R_Tbl.Sch_Code = Dep_Master_M_Tbl.Sch_Code AND"
    '            M_Sql = M_Sql & " Deposit_Outstand_R_Tbl.Acc_No = Dep_Master_M_Tbl.Acc_No AND Deposit_Outstand_R_Tbl.Br_Code = Dep_Master_M_Tbl.Br_Code INNER JOIN"
    '            M_Sql = M_Sql & " Gen_SchemeMaster_P_Tbl ON Deposit_Outstand_R_Tbl.Sch_Code = Gen_SchemeMaster_P_Tbl.Sch_Code INNER JOIN"
    '            M_Sql = M_Sql & " Cust_Master_M_Tbl ON Deposit_Outstand_R_Tbl.Cust_Id = Cust_Master_M_Tbl.Cust_Id "
    '            M_Sql = M_Sql & " WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ")"
    '            M_Sql = M_Sql & " AND (Deposit_Outstand_R_Tbl.Sch_Code = '" & DDlScheme.SelectedValue.ToString() & "')"
    '            M_Sql = M_Sql & " ORDER BY  Dm_DepDate, Right(Space(18)+Dep_Master_M_Tbl.Base_AccNo,18)"


    '            StrSlf = "Company|" & Company_Name & "|Branch|" & Trim(DDlBranch.SelectedItem.ToString) & " - Branch|Heading|Shedule Outstanding Between  " & TxtAsOnDate.Text & " And " & TxtToDate.Text & " " & " |User|" & Session("LoginID")
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
    '            Session.Add("RptFormula", StrSlf)
    '            Session.Add("RptQry", M_Sql)

    '            If ChkAccrued.Checked Then
    '                Session.Add("RptPath", Session("File_Path") & "\Reports\DEPOSIT\DepositOutstand_Shedule_with_int.rpt")
    '            Else
    '                Session.Add("RptPath", Session("File_Path") & "\Reports\DEPOSIT\DepositOutstand_Shedule_Detail.rpt")
    '            End If
    '        End If
    '    End If
    'End Sub

    'Protected Sub Show_LoanOutstanding_Show()
    '    Dim RptName As String
    '    ' Rpt_Name = "\Reports\Deposit\DepositOutstandWithName.rpt"
    '    If RdoOutstanding.Items(0).Selected = True Then

    '        If DDlScheme.SelectedValue = "<-- ALL -->" Then
    '            M_Sql = "SELECT B.Br_Name+'['+B.Br_Code + ']' AS Branch,S.Sch_Name AS Scheme,Count(*) AS OutstandingNo,Sum(Outstanding * Case When BalType='D' Then 1 Else -1 End) AS Outstanding_Amt,SUM(Isnull(NOD_IntAmt,0)) AS Int_Amt,"
    '            M_Sql = M_Sql & " SUM(Case When Od_Amt>0 Then 1 Else 0 End) AS OverdueNos,sum(Isnull(Od_Amt,0)) AS Overdue_Amt,sum(Isnull(OD_INTAMT,0)) AS Overdue_IntAmt,SUM(M.Lm_LoanAmount) as LoanAmount "
    '        Else
    '            M_Sql = "SELECT R.*,S.Sch_Name,MP.Pkc_Description As Purpose,SP.Pkc_Description As SubPurpose,M.Lm_DueDate "
    '        End If

    '        M_Sql = M_Sql & " FROM Gen_SchemeMaster_P_Tbl S INNER JOIN"
    '        M_Sql = M_Sql & " Loan_Outstand_R_Tbl R ON S.Sch_Code = R.Sch_Code INNER JOIN"
    '        M_Sql = M_Sql & " Loan_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND"
    '        M_Sql = M_Sql & " R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code INNER JOIN"
    '        M_Sql = M_Sql & " Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code INNER JOIN "
    '        M_Sql = M_Sql & " Cust_Master_M_Tbl CM ON R.Cust_Id = CM.Cust_Id "
    '        M_Sql = M_Sql & " Left Outer Join PickupCode_P_Tbl MP On M.Lm_Mainpurpose=MP.Pkc_Code "
    '        M_Sql = M_Sql & " Left Outer Join PickupCode_P_Tbl SP On M.Lm_Subpurpose=SP.Pkc_Code "


    '        If DDlScheme.SelectedValue = "<-- ALL -->" Then
    '            M_Sql = M_Sql & " WHERE Terminal = '" & "4784" & "'"
    '            M_Sql = M_Sql & " GROUP BY S.Sch_Code,S.Sch_Name,B.Br_Name,B.Br_Code"
    '        Else
    '            M_Sql = M_Sql & " WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "') "
    '            M_Sql = M_Sql & " AND R.Sch_Code = '" & DDlScheme.SelectedValue.ToString() & "'"
    '            M_Sql = M_Sql & " Order By  Right(Space(18) + R.Acc_No,18)"

    '        End If




    '        If DDlScheme.SelectedValue <> "<-- ALL -->" Then


    '            Session.Add("RptQry", M_Sql)
    '            ' Rpt_Name = "\Reports\Deposit\Dep_OverueCompact.rpt"
    '            Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\LoanOutStandWithNameAndIntOD_Type1.rpt")
    '            StrSlf = "Company|" & Company_Name & "|Chkadd1|" & "0" & "|Chkadd2|" & "0" & "|ChkPost|" & "0" & "|Branch|" & DDlBranch.SelectedItem.Text & " - Branch|Heading|Loan Outstanding As on  " & TxtAsOnDate.Text & "" & " |User|" & Session("LoginID") & "|CheckIsGrouparea|" & "0"
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
    '            Session.Add("RptFormula", StrSlf)
    '            ' Call DisplayCrystalReport(File_Path & "\Reports\Loans\LoanOutStandWithNameAndIntOD_Type1.rpt", Rst_Print, StrSlf, True, , , , , Str_GroupHead)
    '            'Session.Add("RptPath", Session("File_Path") & "\Reports\Deposit\Dep_OverueCompact.rpt")

    '            ' StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Transfer List As on: " & TxtAsOnDate.Text & " |User_ID| Sa"
    '            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        Else
    '            Session.Add("RptQry", M_Sql)
    '            If DDlBranch.SelectedValue = "<-- ALL -->" Then
    '                Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\Loan_OverueCompact_Consolidation.rpt")

    '            Else
    '                Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\Loan_OverueCompact.rpt")
    '            End If
    '            ' Rpt_Name = "\Reports\Deposit\Dep_OverueCompact.rpt"
    '            Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\Loan_OverueCompact.rpt")
    '            StrSlf = "Company|" & Company_Name & "|Chkadd1|" & "0" & "|Chkadd2|" & "0" & "|ChkPost|" & "0" & "|Branch|" & DDlBranch.SelectedItem.Text & " - Branch|Heading|Loan Outstanding As on  " & TxtAsOnDate.Text & "" & " |User|" & Session("LoginID") & "|CheckIsGrouparea|" & "0"
    '            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
    '            Session.Add("RptFormula", StrSlf)

    '            'M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            'If RDoDetailed.Items(1).Selected Then
    '            '    M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            'Else
    '            '    M_Sql = "SELECT S.Sch_Name,M.Base_AccNo,M.Dm_DueDate,M.Dm_Days,M.Dm_Months,R.*,C.Add_Address1 as Cust_Addr1,Add_Address2 as Cust_Addr2,Add_Address3 as Cust_Addr3, Add_Postoffice as Cust_Postoffice,Isnull(LOC.Pkc_Description,'') As AreaName FROM Deposit_Outstand_R_Tbl R INNER JOIN Dep_Master_M_Tbl M ON R.Sch_Code = M.Sch_Code AND R.Acc_No = M.Acc_No AND R.Br_Code = M.Br_Code  INNER JOIN Gen_BranchDetails_P_Tbl AS B ON B.Br_Code = M.Br_Code  INNER JOIN Gen_SchemeMaster_P_Tbl S ON R.Sch_Code = S.Sch_Code INNER JOIN Cust_Details_Vw C ON R.Cust_Id = C.Cust_Id  Left Outer Join PickupCode_P_Tbl LOC On LOC.Pkc_Code=C.Cust_Location_Code  WHERE (Terminal = '4784') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And (R.Br_Code='" & DDlBranch.SelectedValue & "')  AND (R.Sch_Code = '" & DDlScheme.SelectedValue & "' ) Order By  Right(Space(18)+LTRIM(RTRIM(M.Base_AccNo)),18)"
    '            'End If

    '            'Session.Add("RptQry", M_Sql)
    '            'Session.Add("RptPath", Session("File_Path") & "\" & "Reports\Deposit\DepositOutstandWithInt.rpt")
    '            'StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Transfer List As on: " & TxtAsOnDate.Text & " |User_ID| Sa"
    '            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        End If
    '    Else


    '    End If
    'End Sub

    'Protected Sub Show_LoanOutstanding_Schedule_Show()
    '    If DDlScheme.SelectedValue = "<-- ALL -->" Then

    '        M_Sql = "SELECT M.Sch_Code,S.Sch_Name AS Sch_Name,Sch_Section_Name,M.Br_Code,OpNo ,OpAmt ,Rec_No ,Rec_Amt, Pay_No,Pay_Amt,Interest,Include_Int,B.Br_Name from " & _
    '    "(select Sch_Code,Br_Code,Sum(Case When Share_Capital>0 Then 1 Else 0 End) As OpNo," & _
    '    "Sum(Share_Capital) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Receipt) As Rec_Amt," & _
    '    "Sum(Closed_Between) As Pay_No,Sum(Payment) As Pay_Amt,0 As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int " & _
    '    "From Share_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '    "Group By Sch_Code,Br_Code " & _
    '    "Union " & _
    '    "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo," & _
    '    "Sum(Outstanding*Case When Bal_Type='C' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Dep_Receipt) As Rec_Amt," & _
    '    "Sum(Closed_Between) As Pay_No,Sum(Dep_Payment) As Pay_Amt,Sum(Accrued) As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & _
    '    "From Deposit_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '    "Group By Sch_Code,Br_Code " & _
    '    "Union " & _
    '    "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo, " & _
    '    "Sum(Outstanding*Case When BalType='D' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Loan_Receipt) As Rec_Amt," & _
    '    "Sum(Closed_Between) As Pay_No,Sum(Loan_Disburse) As Pay_Amt,Sum(Accrude_IntAmt) As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & _
    '    "From Loan_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & _
    '    "Group By Sch_Code,Br_Code "
    '        M_Sql = M_Sql & "Union " & Chr(13) & _
    '                "select Sch_Code,Br_Code,Sum(Case When Outstanding>0 Then 1 Else 0 End) As OpNo, " & Chr(13) & _
    '                "Sum(Outstanding*Case When Bal_Type='C' Then 1 Else -1 End) AS OpAmt,Sum(Opened_Between) As Rec_No,Sum(Susp_Receipt) As Rec_Amt," & Chr(13) & _
    '                "Sum(Closed_Between) As Pay_No,Sum(Susp_Payment) As Pay_Amt,0 As Interest,'" & IIf(ChkAccrued.Checked = True, "Y", "N") & "' As Include_Int  " & Chr(13) & _
    '                "From Suspense_Outstand_R_Tbl WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtToDate.Text) & ") AND USERID='" & Session("LoginID") & "'" & Chr(13) & _
    '                "Group By Sch_Code,Br_Code) as M INNER JOIN Gen_SchemeMaster_P_Tbl S ON S.Sch_Code=M.Sch_Code " & Chr(13) & _
    '                " Join Gen_BranchDetails_P_Tbl B On B.Br_Code=M.Br_Code "



    '        StrSlf = "Company|" & Company_Name & "|Branch|" & Trim(DDlBranch.SelectedItem.ToString) & " - Branch|Heading|Shedule Outstanding Between  " & TxtAsOnDate.Text & " And " & TxtToDate.Text & " " & " |User|" & Session("LoginID")
    '        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
    '        Session.Add("RptFormula", StrSlf)
    '        Session.Add("RptQry", M_Sql)


    '        Session.Add("RptPath", Session("File_Path") & "\Reports\Shedule_Outstand_WithName.rpt")

    '        'Shedule_Outstand_WithName()
    '        'StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & " - Branch|Heading|Gold Stock Register As On    Dated : " & TxtToDate.Text & " |User_ID| Sa"
    '        'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        'Session.Add("RptFormula", StrSlf)

    '    Else

    '        M_Sql = "SELECT Loan_Outstand_R_Tbl.*, Gen_SchemeMaster_P_Tbl.Sch_Name"
    '        M_Sql = M_Sql & " FROM Gen_SchemeMaster_P_Tbl INNER JOIN"
    '        M_Sql = M_Sql & " Loan_Outstand_R_Tbl ON Gen_SchemeMaster_P_Tbl.Sch_Code = Loan_Outstand_R_Tbl.Sch_Code INNER JOIN"
    '        M_Sql = M_Sql & " Loan_Master_M_Tbl ON Loan_Outstand_R_Tbl.Sch_Code = Loan_Master_M_Tbl.Sch_Code AND"
    '        M_Sql = M_Sql & " Loan_Outstand_R_Tbl.Acc_No = Loan_Master_M_Tbl.Acc_No AND"
    '        M_Sql = M_Sql & " Loan_Outstand_R_Tbl.Br_Code = Loan_Master_M_Tbl.Br_Code INNER JOIN"
    '        M_Sql = M_Sql & " Cust_Master_M_Tbl ON Loan_Outstand_R_Tbl.Cust_Id = Cust_Master_M_Tbl.Cust_Id "
    '        M_Sql = M_Sql & " WHERE (Terminal = '" & "4784" & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ")"
    '        M_Sql = M_Sql & " AND (Loan_Outstand_R_Tbl.Sch_Code = '" & DDlScheme.SelectedValue.ToString() & "')"
    '        M_Sql = M_Sql & " Order By lm_loandate, Right(Space(18) + Loan_Outstand_R_Tbl.Acc_No, 18)" '@@@


    '        StrSlf = "Company|" & Company_Name & "|Branch|" & Trim(DDlBranch.SelectedItem.ToString) & " - Branch|Heading|Shedule Outstanding Between  " & TxtAsOnDate.Text & " And " & TxtToDate.Text & " " & " |User|" & Session("LoginID")
    '        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
    '        Session.Add("RptFormula", StrSlf)
    '        Session.Add("RptQry", M_Sql)

    '        If RDoDetailed.Items(0).Selected = True Then
    '            Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\LoanOutstand_Shedule_Detailed_WithInt.rpt")
    '        Else
    '            Session.Add("RptPath", Session("File_Path") & "\Reports\Loans\LoanOutstand_Shedule.rpt")
    '        End If






    '        'If ChkAccrued.Checked Then
    '        '    Session.Add("RptPath", Session("File_Path") & "\Reports\DEPOSIT\DepositOutstand_Shedule_with_int.rpt")
    '        'Else
    '        '    Session.Add("RptPath", Session("File_Path") & "\Reports\DEPOSIT\DepositOutstand_Shedule_Detail.rpt")
    '        'End If
    '    End If
    'End Sub



    Protected Sub Process_Loan_Outstanding()

        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable()
        Dim Count As Integer
        Dim IsDel As String = "Y"

        M_DBConn.ExecuteScalar("Delete From Loan_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)



        If DDlScheme.SelectedValue = "<-- ALL -->" Then
            For Count = 0 To DDlScheme.Items.Count - 1
                ' M_Sql = "Exec [Process_LoanAccrued_Sp] '" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtAsOnDate.Text) & ",'N','Y','Y','Y','4784','" & Session("LoginID") & "','N','N','N','N'"
                M_Sql = "Exec [Process_LoanAccrued_Sp] '" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtAsOnDate.Text) & ",'N','Y','Y','Y','4784','" & Session("LoginID") & "','N','N','N','N'"
                '  M_Sql = "Exec [Deposit_Outstand_Sp] 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", "0", DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & ",'Y','" & Session("LoginID") & "','4784','N','Y'"
                'M_Sql = "Deposit_Outstand_Shedule_Sp 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'N','" & Session("LoginID") & "','4784','" & IsDel & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                IsDel = "N"
            Next


        Else
            If DDlScheme.SelectedValue = "<-- ALL -->" Then

            Else
                'If DDlScheme.SelectedValue <> "<-- ALL -->" And ChkAccrued.Checked = True Then
                'Else

                'End If
                M_Sql = "Exec [Process_LoanAccrued_Sp] '" & DDlBranch.SelectedValue & "', 'S','" & DDlScheme.SelectedValue.ToString & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtAsOnDate.Text) & ",'N','Y','Y','Y','4784','" & Session("LoginID") & "','N','N','N','N'"


            End If




            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
        End If


        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
        M_DBConn = Nothing
    End Sub

    Protected Sub Process_Loan_Outstanding_Schedule()

        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable()
        Dim Count As Integer
        Dim IsDel As String = "Y"

        M_DBConn.ExecuteScalar("Delete From Deposit_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)
        M_DBConn.ExecuteScalar("Delete From Loan_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)
        M_DBConn.ExecuteScalar("Delete From Share_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)
        M_DBConn.ExecuteScalar("Delete From Suspense_Outstand_R_Tbl Where Terminal='4784'", Connection.SQLSource.STOREDPROCEDURE, False)



        If DDlScheme.SelectedValue = "<-- ALL -->" Then
            For Count = 0 To DDlScheme.Items.Count - 1
                ' M_Sql = "Exec [Process_LoanAccrued_Sp] '" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtAsOnDate.Text) & ",'N','Y','Y','Y','4784','" & Session("LoginID") & "','N','N','N','N'"
                ' M_Sql = "Exec [Process_LoanAccrued_Sp] '" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtAsOnDate.Text) & ",'N','Y','Y','Y','4784','" & Session("LoginID") & "','N','N','N','N'"

                M_Sql = "Exec Process_LoanShedule_Sp '" & DDlBranch.SelectedValue & "','" & "S" & "','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", "0", DDlScheme.Items(Count).Value.ToString) & "',''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'Y','Y','Y','" & "4784" & "','" & Session("LoginID") & "','N','" & IsDel & "'"
                '  M_Sql = "Exec [Deposit_Outstand_Sp] 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", "0", DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & ",'Y','" & Session("LoginID") & "','4784','N','Y'"
                'M_Sql = "Deposit_Outstand_Shedule_Sp 'N','" & DDlBranch.SelectedValue & "', 'S','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", DDlScheme.Items(Count).Value.ToString, DDlScheme.Items(Count).Value.ToString) & "' ,''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'N','" & Session("LoginID") & "','4784','" & IsDel & "'"
                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                IsDel = "N"
            Next


        Else



            'M_Sql = "Exec Process_LoanShedule_Sp '" & DDlBranch.SelectedValue & "','" & "S" & "','" & IIf(DDlScheme.Items(Count).Value.ToString = "<-- ALL -->", "0", DDlScheme.Items(Count).Value.ToString) & "',''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'Y','Y','Y','" & "4784" & "','" & Session("LoginID") & "','N','" & IsDel & "'"
            M_Sql = "Exec Process_LoanShedule_Sp '" & DDlBranch.SelectedValue & "','" & "S" & "','" & IIf(DDlScheme.SelectedItem.ToString = "<-- ALL -->", "0", DDlScheme.SelectedValue.ToString) & "',''," & CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'Y','Y','Y','" & "4784" & "','" & Session("LoginID") & "','N','" & IsDel & "'"

            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
        End If


        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
        M_DBConn = Nothing
    End Sub
    Private Function ValidateFields() As Boolean
        Dim Status As Boolean



        If RdoOutstanding.Items(0).Selected = True Then


            If Val(TxtAsOnDate.Text.Length) > 0 Then
                Status = True
            Else
                ShowAlertMessage(" Invalid Date")
                TxtAsOnDate.Focus()
                Return False
            End If

        Else
            If Val(TxtAsOnDate.Text.Length) > 0 Then
                Status = True
            Else
                ShowAlertMessage(" Invalid Date")
                TxtAsOnDate.Focus()
                Return False
            End If

            If Val(TxtToDate.Text.Length) > 0 Then
                Status = True
            Else
                ShowAlertMessage(" Invalid Date")
                TxtToDate.Focus()
                Return False
            End If


        End If

        'If TxtAccNo.Text.Length > 0 And Val(TxtBalance.Text) > 0 Then
        '    Status = True
        'Else
        '    ShowAlertMessage(" Invalid Account No")
        '    TxtAccNo.Focus()
        '    Return False
        'End If

        'If DDlChqType.SelectedValue = "<--SELECT-->" Then
        '    ShowAlertMessage("Select Cheque Type")
        '    DDlChqType.Focus()
        '    Return False
        'End If
        'If GrViewCheque.Rows.Count > 0 Then

        '    If GrViewCheque.Rows(0).Cells(0).Text = TxtFromChqno.Text Then
        '        Status = True
        '    Else
        '        ShowAlertMessage("Starting Cheque No is not Match with Stock")
        '        Return False
        '    End If
        'Else
        '    ShowAlertMessage("Starting Cheque No is not Match with Stock")
        '    Return False
        'End If

        If DDlSection.SelectedItem.ToString <> "<-- ALL -->" Then
            Status = True
        Else
            ShowAlertMessage(" Select Section ")
            DDlSection.Focus()
            Return False
        End If
        Return Status


    End Function


    Protected Sub ChkAgent_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAgent.CheckedChanged
        If ChkAgent.Checked Then
            DDlAgent.Enabled = True
        Else
            DDlAgent.Enabled = False
        End If



    End Sub

    'Protected Sub Share_Outstand_And_Shedule()
    '    Dim Terminal = System.Environment.MachineName
    '    Dim RptName As String = ""
    '    Dim HeadingName As String = ""


    '    If ValidateFields() = False Then
    '        Exit Sub
    '    End If

    '    ' == Share
    '    If DDlSection.SelectedItem.Text = "SHARE" Then
    '        If RdoOutstanding.Items(0).Selected = True Then

    '            If DDlScheme.SelectedValue = "<-- ALL -->" Then
    '                For Count = 0 To DDlScheme.Items.Count - 1
    '                    M_DBConn.ExecuteScalar("Delete From Share_Outstand_R_Tbl Where Terminal='" & Terminal & "' AND Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text), Connection.SQLSource.STOREDPROCEDURE, False)

    '                    M_Sql = "EXEC Share_Outstand_Sp 'N','" & DDlScheme.Items(Count).Value & "','" & DDlBranch.SelectedItem.Value & "'," & _
    '                    " '" & CommCls.DbDate(TxtAsOnDate.Text) & "','" & Session("UserID") & "','N','" & Terminal & "'"
    '                    M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
    '                Next

    '                ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
    '            Else

    '                M_Sql = "EXEC Share_Outstand_Sp 'N','" & DDlScheme.SelectedItem.Value & "','" & DDlBranch.SelectedItem.Value & "'," & _
    '                        CommCls.DbDate(TxtAsOnDate.Text) & ",'" & Session("UserID") & "','Y','" & Terminal & "'"
    '                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

    '                M_Sql = "SELECT *, Gen_SchemeMaster_P_Tbl.Sch_Name AS Sch_Name FROM Share_Outstand_R_Tbl " & _
    '                        " INNER JOIN Gen_SchemeMaster_P_Tbl ON Share_Outstand_R_Tbl.Sch_Code = Gen_SchemeMaster_P_Tbl.Sch_Code " & _
    '                        "  INNER JOIN PickupCode_P_Tbl  ON PickupCode_P_Tbl  .Pkc_Code = Share_Outstand_R_Tbl.Cust_Location_Code " & _
    '                        " WHERE (Terminal = '" & Terminal & "') AND (Demand_Date = " & CommCls.DbDate(TxtAsOnDate.Text) & ") And" & _
    '                        " (Share_Outstand_R_Tbl.Br_Code='" & DDlBranch.SelectedItem.Value & "')  " & _
    '                        " AND (Share_Outstand_R_Tbl.Sch_Code = '" & DDlScheme.SelectedItem.Value & "') " & _
    '                        " Order By Right(Space(18)+Share_Outstand_R_Tbl.Mem_No,18)"

    '                HeadingName = "OUTSTANDING"
    '                RptName = "ShareOutstanding.rpt"


    '                ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")


    '            End If
    '        Else

    '        End If

    '        StrSlf = "Company|" & Company_Name & "|Branch|" & DDlBranch.SelectedItem.ToString & "|Heading|" & DDlSection.SelectedItem.Text & " " & HeadingName & " REPORT AS ON " & TxtAsOnDate.Text & " |User_ID| " & Session("UserID") & ""
    '        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        Session.Add("RptQry", M_Sql)
    '        Session.Add("RptPath", Session("File_Path") & "\" & "Reports\Share\" & RptName)


    '        Session.Add("RptFormula", StrSlf)
    '        'If CmbOutput.SelectedIndex = 0 Then
    '        '    Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
    '        'Else
    '        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    '        'End If

    '    End If

    '    '===
    'End Sub

    Protected Sub ddl_Religion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_Religion.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL

        If ddl_Religion.SelectedItem.Text = "-- ALL --" Then
            M_CommonBL.Fill_Pickup(ddl_Caste, 2, 0)
        Else
            M_CommonBL.Fill_Pickup(ddl_Caste, 2, ddl_Religion.SelectedValue)
        End If
        ddl_Caste.Items.Add("-- ALL --")
        ddl_Caste.SelectedIndex = ddl_Caste.Items.Count - 1
    End Sub

    Private Sub ProcessOutstandingSchedule()
        Dim M_DBConn As New Connection
        Dim M_Dt As New DataTable()

        Dim M_RptTblSw As New IO.StringWriter
        Dim RptTbl As New DataTable("Table")
        RptTbl.Columns.Add("Company", GetType(String))
        RptTbl.Columns.Add("Br_Name", GetType(String))
        RptTbl.Columns.Add("UserID", GetType(String))
        RptTbl.Columns.Add("IsDetailed", GetType(String))
        RptTbl.Columns.Add("WithInt", GetType(String))
        RptTbl.Columns.Add("WithAgent", GetType(String))

        Dim IsDetailed As String = IIf(RDoDetailed.Items(0).Selected = True, "N", "Y")
        Dim WithInt As String = IIf(ChkAccrued.Checked = True, "Y", "N")
        Dim WithAgent As String = IIf(ChkAgent.Checked = True, "Y", "N")

        Dim Dr As DataRow
        Dr = RptTbl.Rows.Add
        Dr("Company") = IIf(Company_Name = Nothing, "", Company_Name)
        Dr("Br_Name") = DDlBranch.SelectedItem.Text
        Dr("UserID") = Trim(Session("UserID").ToString)
        Dr("IsDetailed") = IsDetailed
        Dr("WithInt") = WithInt
        Dr("WithAgent") = WithAgent

        If DDlSection.Text = "SHARE" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
            CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            Dim Br_Code As String
            Dim Sch_Code As String
            Dim M_Ds As New DataSet
            Dim J As Integer
            Dim I As Integer

            'Dim Lbl As Label = DirectCast(UpdateProgress1.FindControl("Lbl_Status"), Label)
            'Lbl.Text = ""

            M_Sql = "Delete From Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'"
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

            For J = 0 To DDlBranch.Items.Count - 1
                If DDlBranch.SelectedValue <> "<-- ALL -->" Then
                    Br_Code = DDlBranch.SelectedValue
                Else
                    'If DDlBranch.Items(J).Text <> "<-- ALL -->" Then J = J + 1
                    Br_Code = DDlBranch.Items(J).Value
                End If

                For I = 0 To DDlScheme.Items.Count - 1
                    If DDlScheme.SelectedValue <> "<-- ALL -->" Then
                        Sch_Code = DDlScheme.SelectedValue
                        'Lbl.Text = "Pocessing  " & DdlScheme.Text & " - " & Br_Code
                    Else
                        Sch_Code = DDlScheme.Items(I).Value
                        'Lbl.Text = "Pocessing  " & DdlScheme.Items(J).Text & " - " & Br_Code
                    End If

                    If Sch_Code <> "<-- ALL -->" And Br_Code <> "<-- ALL -->" And DDlSection.Text <> "LOAN" Then
                        M_Sql = "Exec Process_OutstandSchedule_Sp '" & Br_Code & "','" & DDlSection.Text & "','" & Sch_Code & "'," & _
                            CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','N' ,'" & Session("UserID") & "'"
                        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                    End If

                    If DDlScheme.SelectedValue <> "<-- ALL -->" Then Exit For
                Next I
                If DDlBranch.SelectedValue <> "<-- ALL -->" Then Exit For
            Next J

        ElseIf DDlSection.Text = "LOAN" Then
            RptTbl.WriteXml(M_RptTblSw)

            '  M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
            '     CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y'"
        ElseIf DDlSection.Text = "SUSPENSE" Then
            RptTbl.WriteXml(M_RptTblSw)
            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "INVESTMENT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "BORROWINGS" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "BANKACCOUNT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "GROUP DEPOSIT" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        ElseIf DDlSection.Text = "GOVT SECURITY" Then
            RptTbl.WriteXml(M_RptTblSw)

            M_Sql = "Exec Process_OutstandSchedule_Sp '" & DDlBranch.SelectedValue & "','" & DDlSection.Text & "','" & DDlScheme.SelectedValue & "'," & _
                CommCls.DbDate(TxtAsOnDate.Text) & "," & CommCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & M_RptTblSw.ToString.Replace("DocumentElement", "NewDataSet") & "','Y' ,'" & Session("UserID") & "'"
        End If

        If DDlSection.Text <> "DEPOSIT" And DDlSection.Text <> "LOAN" Then M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
    End Sub

    Protected Sub DDlAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlAgent.SelectedIndexChanged

    End Sub

    Protected Sub DDlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlBranch.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Agent(DDlAgent, CommonBL.AgentType.DDAgent, DDlBranch.SelectedValue)
        M_CommonBL = Nothing
    End Sub

    Protected Sub Btn_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btn_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Public Function Get_OutstandRpt(ByVal Type As String, ByVal As_On_Date As String, ByVal Br_code As String) As DataTable
        Try
            Dim Ddl_Type As String = DDlType.SelectedValue
            Dim AsOnDate As String = TxtAsOnDate.Text
            Dim Brcode As String = DDlBranch.SelectedValue

        Catch ex As Exception

        End Try
    End Function
    Public Sub PrintDocument()
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA
        Dim StrSlf As String = ""
        Dim clscomm As New CommonClass

        Dim Type As String = DDlType.SelectedValue
        Dim As_On_Date As String = TxtAsOnDate.Text
        Dim Br_Code As String = DDlBranch.SelectedValue
        Dim User_Id As String = Session("UserID")


        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()


        M_Sql = "Exec Outstand_Section_Wise_Sp '" + Type + "','" + Convert.ToDateTime(As_On_Date).ToString("MM/dd/yyyy") + "','" + Br_Code + "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        'StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Session("BranchName").ToString & "|Heading|  " & Session("BranchName").ToString & "|User_ID|" & Session("UserID")
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch :" & DDlBranch.SelectedItem.ToString & " |Heading|Outstanding Statement As On " & As_On_Date & "|User_ID|" & User_Id
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/Loans/OutStandRpt.rpt"))


        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'Response.Redirect("~/RptPreview.aspx", False)

    End Sub

    Protected Sub DDlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlType.SelectedIndexChanged
        Try
            Call DDlSection_SelectedIndexChanged(Me.DDlSection, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DDlBranch.Items.Remove(DDlBranch.Items.FindByText("ALL"))
            DDlBranch.Items.Add("<-- ALL -->")
        Catch ex As Exception

        End Try
    End Sub
End Class
