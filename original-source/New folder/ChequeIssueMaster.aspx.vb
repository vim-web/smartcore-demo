Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Partial Public Class ChequeIssueMaster
    Inherits System.Web.UI.Page
    Private Shared CommCls As New CommonClass
    Private EntryMode As String
    Private M_CustId As Integer
    Private Str_Sql As String
    Dim FormID As Integer = 1020

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("M_EntryMode") = "NEW"
            Session("SearchType") = "ACCOUNTNO"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Load_DefaultValues()
        Else
            If Session("SearchResult") <> "" Then
                TxtAccNo.Text = Session("SearchResult")
                Dim Dt As New DataTable

                Dim GDr As GridViewRow = DirectCast(Session("SearchDataRow"), GridViewRow)
                Session("SearchResult") = ""

                Call TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            End If
        End If
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable

            M_Dt = M_CommonBL.GetAccountTranDtls(DDlBranch.SelectedValue, DDlScheme.SelectedValue, TxtAccNo.Text.Trim, Session("TransDate"))

            If M_Dt.Rows.Count >= 1 Then
                HddSch.Value = DDlScheme.SelectedValue
                TxtName.Text = M_Dt.Rows(0)("Cust_Name").ToString
                HddAccID.Value = M_Dt.Rows(0)("Acc_ID").ToString
                TxtAddress.Text = M_Dt.Rows(0)("Cust_Address").ToString
                Dim CurBal As Double = M_Dt.Rows(0)("Cur_Bal").ToString()
                TxtBalance.Text = Format(Val(CStr(Math.Abs(CurBal)).ToString), "#0.00") & " " & IIf(CurBal < 0, "Dr", "Cr")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(DDlScheme, Session("TransDate"), , , , , , , , , , True)

        Call DDlScheme_SelectedIndexChanged(Me.DDlScheme, System.EventArgs.Empty)

        M_CommonBL.Fill_Branch(DDlBranch)
        DDlBranch.SelectedValue = Session("DefaultBrCode")
        DDlBranch.Enabled = False
        M_CommonBL = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        If ValidateFields() = False Then
            Exit Sub
        End If

        Dim M_DBConn As New Connection
        Dim ChqSatatus As String = ""
        Dim Err As String = ""
        Dim I As Integer
        Dim FrmChqNo As Integer
        Dim ToChqNo As Integer

        For I = 0 To DDlChqType.SelectedValue - 1

            If I = 0 Then
                ChqSatatus += "1"
            Else
                ChqSatatus += "-1"
            End If

        Next
        FrmChqNo = Val(TxtFromChqno.Text)
        TxtToChqNo.Text = Val(TxtFromChqno.Text) + DDlChqType.SelectedValue
        Try
            ' For I = 0 To DDlChqBookNos.SelectedValue - 1
            'If I = 0 Then
            FrmChqNo = Val(TxtFromChqno.Text)
            ToChqNo = Val(TxtFromChqno.Text) + DDlChqType.SelectedValue

            'Else
            '    FrmChqNo = ToChqNo
            '    ToChqNo = FrmChqNo + DDlChqType.SelectedValue
            'End If

            Dim M_ChequeMasterBO As New SMARTCORE_BO.ChequeMasterBO
            With M_ChequeMasterBO
                .ACCID = HddAccID.Value.ToString()
                .SchCode = HddSch.Value.ToString()
                .AccNo = TxtAccNo.Text
                .BrCode = Session("DefaultBrCode").ToString
                .CmChqNoFrom = FrmChqNo
                .CmChqNoTo = ToChqNo - 1
                .CmLooseBook = "B"
                .CmChqBookType = DDlChqType.SelectedValue.ToString()
                .CmChqDate = Session("TransDate")
                .CmChqStatus = ChqSatatus
                .CmChqBookStatus = "A"
                .CmRemarks = ""
                .Authorised = "N"
                .MakerID = Session("UserID")
                .CheckerID = ""
                .MakingTime = DateTime.Now
                .CheckingTime = DateTime.Now
            End With
            Dim M_ChequeMasterBL As New SMARTCORE_BL.ChequeMasterBL
            M_CustId = M_ChequeMasterBL.Save_Data(M_ChequeMasterBO, Session("M_EntryMode")) ', M_PresAddress, M_PermAddress, M_PhotoIdentity, M_AddIdentity, Session("M_EntryMode"))
            M_ChequeMasterBL = Nothing

            '   Next

            Str_Sql = "Update Gen_ChequeStock_M_Tbl Set Stk_Chq_Status = 'I'"
            Str_Sql = Str_Sql & " Where Grp_Sch_Code='" & Trim(ChqGroup.Value) & "'And "
            Str_Sql = Str_Sql & " Stk_Chq_BookType='" & DDlChqType.SelectedValue & "'And "
            Str_Sql = Str_Sql & " Br_Code='" & DDlBranch.SelectedValue & "' And Stk_Chq_No_From = '" & FrmChqNo & "'And "
            Str_Sql = Str_Sql & " Stk_Chq_No_To='" & ToChqNo - 1 & "'"
            M_DBConn.ExecuteScalar(Str_Sql, Connection.SQLSource.STOREDPROCEDURE, False)




        Catch ex As Exception
            Err = ex.ToString()
            ShowAlertMessage(ex.ToString())
            Exit Sub
        End Try

        If Err.ToString() = "" Then
            ShowAlertMessage("Saved Successfully")
            ClearValues()

        End If


    End Sub

    Protected Sub TxtFromChqno_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtFromChqno.TextChanged
        TxtToChqNo.Text = (Val(TxtFromChqno.Text) + DDlChqType.SelectedValue) - 1
    End Sub

    Private Function Availabity_of_ChequeBooks() As Boolean
        Dim Str_Sql As String
        Str_Sql = " Select Count(Stk_Chq_Status)As StockNo from Gen_ChequeStock_M_Tbl "
        Str_Sql = Str_Sql & " Where Grp_Sch_Code='" & ChqGroup.Value & "' And "
        Str_Sql = Str_Sql & " Br_Code='" & DDlBranch.SelectedValue & "' And Stk_Chq_BookType='" & Trim(DDlChqType.SelectedValue) & "'And "
        Str_Sql = Str_Sql & " Stk_Chq_Status = 'A' And Authorised= 'Y' "

        Dim M_SearchBL As New SearchBL
        Dim M_Dt As New DataTable

        M_Dt = M_SearchBL.ExecuteQuery(Str_Sql)
        M_SearchBL = Nothing
        If M_Dt.Rows.Count >= 1 Then
            Availabity_of_ChequeBooks = True
        Else
            Availabity_of_ChequeBooks = False
        End If


    End Function

    Private Function AvilableStockFn() As Boolean
        Dim Str_Sql As String

        Str_Sql = " Select Top 20 Stk_Chq_No_From As StartingNo,Stk_Chq_No_To As EndingNo From Gen_ChequeStock_M_Tbl "
        Str_Sql = Str_Sql & " Where Grp_Sch_Code='" & Trim(ChqGroup.Value) & "' And "
        Str_Sql = Str_Sql & " Br_Code='" & DDlBranch.SelectedValue & "' And Stk_Chq_BookType = '" & IIf(DDlChqType.SelectedValue = "<--SELECT-->", "0", DDlChqType.SelectedValue) & "' And Stk_Chq_Status = 'A' "
        Str_Sql = Str_Sql & " And Authorised= 'Y' And Stk_Date <= " & CommCls.DbDate(Session("TransDate")) & " Order By Right('000000000000'+ Stk_Chq_No_From,12)"
        Dim M_SearchBL As New SearchBL
        Dim M_Dt As New DataTable

        M_Dt = M_SearchBL.ExecuteQuery(Str_Sql)
        M_SearchBL = Nothing
        If M_Dt.Rows.Count >= 1 Then
            GrViewCheque.DataSource = M_Dt
            GrViewCheque.DataBind()
            AvilableStockFn = True
            LblStock.Visible = True
            LblStock.Text = "Avilable Cheque Books"
            LblStock.ForeColor = Drawing.Color.Blue
            Return True
        Else
            LblStock.Visible = True
            LblStock.ForeColor = Drawing.Color.Red
            LblStock.Text = "Cheque Books Status : Out  Of Stock"
            AvilableStockFn = False
            GrViewCheque.DataSource = Nothing
            GrViewCheque.DataBind()
            M_Dt = Nothing

        End If


    End Function

    Protected Sub DDlChqType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlChqType.SelectedIndexChanged
        AvilableStockFn()
        TxtFromChqno.Focus()
    End Sub
    Private Function ValidateFields() As Boolean
        Dim Status As Boolean

        If TxtAccNo.Text.Length > 0 And Val(TxtBalance.Text) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Account No")
            TxtAccNo.Focus()
            Return False
        End If

        If DDlChqType.SelectedValue = "<--SELECT-->" Then
            ShowAlertMessage("Select Cheque Type")
            DDlChqType.Focus()
            Return False
        End If
        If GrViewCheque.Rows.Count > 0 Then

            If GrViewCheque.Rows(0).Cells(0).Text = TxtFromChqno.Text Then
                Status = True
            Else
                ShowAlertMessage("Starting Cheque No is not Match with Stock")
                Return True
            End If
        Else
            ShowAlertMessage(" Cheque Out Of  Stock")
            Return False
        End If
        Return Status
    End Function
    Protected Sub ClearValues()
        TxtAccNo.Text = ""
        TxtAddress.Text = ""
        TxtBalance.Text = 0
        TxtFromChqno.Text = ""
        TxtToChqNo.Text = ""
        TxtName.Text = ""
        GrViewCheque.DataSource = Nothing
        GrViewCheque.DataBind()
        LblStock.Visible = False
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub DDlScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlScheme.SelectedIndexChanged
        Session("SearchScheme") = DDlScheme.SelectedValue
        Dim M_CommomBL As New CommonBL
        Dim M_Dt = M_CommomBL.GetDepositParameter(DDlScheme.SelectedValue)
        ChqGroup.Value = M_Dt.Rows(0)("Dep_Chq_Grp_Code").ToString
        SchSection.Value = M_CommomBL.GetSchemeSection(DDlScheme.SelectedValue)
    End Sub
End Class