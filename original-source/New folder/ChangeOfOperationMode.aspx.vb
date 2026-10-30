Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL

Public Class ChangeOfOerationMode
    Inherits System.Web.UI.Page
    Public NewCustID As String
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1017
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
            Load_DefaultValues()
            SetGrid()
            TxtName.Attributes.Add("readonly", "readonly")
            TxtAddress.Attributes.Add("readonly", "readonly")
            TxtCustNo.Attributes.Add("readonly", "readonly")
            TxtNewScheme.Attributes.Add("readonly", "readonly")
            TxtNewSchCode.Attributes.Add("readonly", "readonly")
            TxtNewAccNo.Attributes.Add("readonly", "readonly")
            TxtBranch.Text = Session("DefaultBrCode")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            TxtEffDate.Text = Session("TransDate")

            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

        End If
        If Session("SearchResult") <> "" Then
            TxtNewCustNo.Text = Session("SearchResult")
            TxtNewCustNo_TextChanged(Me.TxtNewCustNo, System.EventArgs.Empty)
            TxtNewCustNo.Focus()
            Session("SearchResult") = ""
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.GetSchemes(CmbNewScheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(CmbOprnMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbNewOprnMode, 43, 0)
        M_CommonBL.Fill_Pickup(CmbRelntype, 25, 0)
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbNewScheme_SelectedIndexChanged(Me.CmbNewScheme, System.EventArgs.Empty)
    End Sub
    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
            TxtNewAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
            TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            TxtNewAccNo_TextChanged(Me.TxtNewAccNo, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub SetGrid()
        DgvPresentMode.DataSource = Nothing
        Dim PresentModeTbl As New DataTable("Table")
        Dim NewModeTbl As New DataTable("Table")
        PresentModeTbl.Columns.Add("Cust_Name", GetType(String))
        PresentModeTbl.Columns.Add("Cust_Id", GetType(String))
        'PresentModeTbl.Columns.Add("Relation", GetType(String))
        'PresentModeTbl.Columns.Add("AuthOpratr", GetType(String))
        'PresentModeTbl.Columns.Add("RelnType", GetType(String))
        PresentModeTbl.Rows.Add("", 0)
        Session("PresentModeTbl") = PresentModeTbl
        DgvPresentMode.DataSource = PresentModeTbl

        DgvPresentMode.DataBind()
        DgvNewMode.DataSource = Nothing
        NewModeTbl.Columns.Add("CustName", GetType(String))
        NewModeTbl.Columns.Add("CID", GetType(String))
        NewModeTbl.Columns.Add("Relation", GetType(String))
        NewModeTbl.Columns.Add("AuthOpratr", GetType(String))
        NewModeTbl.Columns.Add("RelnType", GetType(String))
        NewModeTbl.Rows.Add("", 0, "", "", "")
        Session("NewModeTbl") = NewModeTbl
        DgvNewMode.DataSource = NewModeTbl
        DgvNewMode.DataBind()
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        TxtNewScheme.Text = CmbScheme.SelectedValue
        CmbNewScheme.SelectedValue = CmbScheme.SelectedValue
        'TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
        'TxtNewAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtBranch_TextChanged(sender As Object, e As EventArgs) Handles TxtBranch.TextChanged
        On Error Resume Next
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub TxtAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dim M_Dt As New DataTable()
        Dim M_Sql As String
        Dim M_CommonDa As New CommonDA
        M_DBConn = New Connection()
        Dim M_OpDt As New DataTable
        Dim M_AccNo As String = TxtAccNo.Text
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString
            TxtCustNo.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
            Session("CustomerId") = TxtCustNo.Text
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            TxtNewName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtNewAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            TxtNewCustNo.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()

        End If
        M_OpDt = Get_Operator(HdnAccId.Value)
        If M_OpDt.Rows.Count > 0 Then
            CmbOprnMode.SelectedValue = M_OpDt.Rows(0)("ACC_ModeOfOpearation").ToString
        End If

        If HdnAccId.Value <> 0 Then
            M_Sql = "Select C.Cust_Name,C.Cust_Id,R.Pkc_Description As Relation,O.Ao_AuthrSign As AuthoriedOperator,O.Ao_RelationType From Gen_AccountOperators_M_Tbl O "
            M_Sql = M_Sql & "Join Cust_Master_M_Tbl C On C.Cust_Id=O.Cust_Id "
            M_Sql = M_Sql & "Join PickupCode_P_Tbl R On R.Pkc_Code=O.Ao_RelationType Where Acc_ID='" & HdnAccId.Value & "'"


        End If
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        DgvPresentMode.DataSource = M_Dt
        'DgvNewMode.DataSource = M_Dt
        'DgvNewMode.DataBind()
        DgvPresentMode.DataBind()

        If M_Dt.Rows.Count < 1 Then
            SetGrid()
        End If
        'Dim dt As New DataTable
        'dt = Session("PresentModeTbl")
        'Dim Row As DataRow
        'Row = dt.NewRow
        'If M_Dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To M_Dt.Rows.Count - 1
        '        Row(i)("Cust_Name") = M_Dt.Rows(i)("Cust_Name").ToString()
        '        Row(i)("Cust_Id") = M_Dt.Rows(i)("Cust_ID").ToString()
        '    Next

        '    Try
        '        dt.Rows.Add(Row)

        '    Catch ex As Exception

        '    End Try
        '    Try
        '        Dim Expression As String = "Cust_Name=''"
        '        dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))

        '    Catch ex As Exception

        '    End Try
        '    DgvPresentMode.DataSource = dt
        '    DgvPresentMode.DataBind()
        '    Session("PresentModeTbl") = dt

        '    M_CommonBL = Nothing
        'End If
        
        
    End Sub
    Private Function Get_Operator(ByVal M_Accid As Integer)
        Dim M_Qry As String
        Dim M_Dbconn As New Connection
        Dim M_Dt As New DataTable
        M_Qry = "select ACC_ModeOfOpearation from accmain_m_tbl where ACC_ID='" & M_Accid & "'"
        M_Dbconn.ExecuteDataTable(M_Dt, M_Qry, False)
        Return M_Dt
    End Function

    Protected Sub TxtSchCode_TextChanged(sender As Object, e As EventArgs) Handles TxtSchCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
        TxtNewAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtSchCode.Text, 7)
        TxtNewSchCode.Text = TxtSchCode.Text
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Set_SchemeCtrl()
    End Sub

    Protected Sub TxtScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtScheme.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
        TxtNewScheme.Text = CmbScheme.SelectedValue
        CmbNewScheme.SelectedValue = TxtScheme.Text
        Set_SchemeCtrl()
    End Sub

    Private Function Validate_Save() As Boolean
        Dim I As Integer
        Dim J As Integer
        For I = 0 To DgvNewMode.Rows.Count - 1


            If DgvNewMode.Rows(I).Cells(3).Text = "Main" Then
                J = J + 1
            End If

        Next

        If J = 0 Then
            ShowAlertMessage("Main Customer not entered")
            Return False
        End If

        Return True

    End Function


    Private Function Validate_Control() As Boolean
        Dim I As Integer
        Dim J As Integer
        For I = 0 To DgvNewMode.Rows.Count - 1


            If DgvNewMode.Rows(I).Cells(3).Text = "Main" Then
                J = J + 1

            End If

        Next

        If J > 0 And CheckBoxList1.SelectedValue = "Main" Then
            ShowAlertMessage("Already Entered")
            Return False
        End If

        If Len(Trim(TxtNewCustNo.Text)) = 0 Then
            ShowAlertMessage("Invalid Customer ID Entered")
            Return False
        End If


        Return True

    End Function

    Protected Sub TxtNewAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtNewAccNo.TextChanged

    End Sub

    Protected Sub CmbNewScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNewScheme.SelectedIndexChanged

    End Sub

    Protected Sub CmdAdd_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        If Validate_Control() = False Then
            Exit Sub

        End If
        If CheckBoxList1.SelectedValue = "Main" Then NewCustID = TxtNewCustNo.Text
        Dim dt As New DataTable
        dt = Session("NewModeTbl")
        Dim Row As DataRow
        Row = dt.NewRow
        Row("CustName") = TxtNewName.Text
        Row("CID") = TxtNewCustNo.Text
        Row("AuthOpratr") = CheckBoxList1.SelectedValue
        Row("RelnType") = CmbRelntype.SelectedValue
        Try
            dt.Rows.Add(Row)

        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "CustName=''"
            dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try
        DgvNewMode.DataSource = dt
        DgvNewMode.DataBind()
        Session("NewModeTbl") = dt
        TxtNewAddress.Text = ""
        TxtNewName.Text = ""
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click


        If Validate_Save() = False Then Exit Sub
        Dim M_OpModeBO As New SMARTCORE_BO.ChangeOpModeBO
        With M_OpModeBO
            .AccID = HdnAccId.Value
            .PreOpMode = CmbOprnMode.SelectedValue
            .NewOpMode = CmbNewOprnMode.SelectedValue
            .EffectDate = TxtEffDate.Text
            .Authorised = CheckBoxList1.SelectedValue
            .MakerID = Session("LoginID")
            .CheckerID = Session("LoginID")
            .BrCode = TxtBranch.Text
            .AccNo = TxtAccNo.Text
            .CustNo = TxtCustNo.Text
            .NewCustNo = TxtNewCustNo.Text
            .SchCode = CmbNewScheme.SelectedValue
        End With
        Dim OpModeTblDt As DataTable = DirectCast(Session("NewModeTbl"), DataTable)
        Dim OpModesw As New IO.StringWriter
        OpModeTblDt.WriteXml(OpModesw)

        Dim M_OpModeBl As New CommonBL

        M_OpModeBl.Save_Change_OpMode(M_OpModeBO, OpModesw.ToString.Replace("DocumentElement", "NewDataSet"))

        ShowAlertMessage("Operation Mode Changed")
        clear()
    End Sub

    Protected Sub TxtNewCustNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtNewCustNo.TextChanged
        Dim M_CommonBL As New CommonBL
        TxtNewName.Text = M_CommonBL.GetCustomerName(TxtNewCustNo.Text)
        TxtNewAddress.Text = M_CommonBL.GetCustomerAddress(TxtNewCustNo.Text)
        M_CommonBL = Nothing
    End Sub
    Private Sub clear()
        TxtSchCode.Text = ""
        TxtNewSchCode.Text = ""
        TxtAddress.Text = ""
        TxtCustNo.Text = ""
        TxtName.Text = ""
        TxtNewAddress.Text = ""
        TxtNewCustNo.Text = ""
        TxtNewName.Text = ""
        'DgvNewMode.DataSource = Session("NewModeTbl")
        'DgvPresentMode.DataSource = Session("PresentModeTbl")
        
        SetGrid()
    End Sub

    Private Sub DgvNewMode_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvNewMode.RowDeleting
        Dim dt As DataTable = DirectCast(Session("NewModeTbl"), DataTable)
        dt.Rows(e.RowIndex).Delete()

        DgvNewMode.DataSource = dt
        DgvNewMode.DataBind()
        Session("NewModeTbl") = dt

        If dt.Rows.Count <= 0 Then Call SetGrid()
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        clear()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class