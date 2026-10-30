
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Imports SMARTCORE_DAL

Partial Public Class ChequeReturn
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    Dim M_Sql As String
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1021
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()

            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            Session("SearchType") = "ACCOUNTNO"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If

        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "ACCOUNTNO" Then
                TxtAccNo.Text = Session("SearchResult")
                TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim Str As String
            Dim Str1 As String
            Dim StrStatus As String
            Str = ""
            For Each li As ListItem In ChkStChequeNo.Items
                '  li.Selected = True
                If li.Selected And DDlStstusType.SelectedItem.Text = "Stop Payment" And li.Value.ToString <> "Withdrawed" And li.Value.ToString <> "Stop" And li.Value.ToString <> "Returned" Then
                    StrStatus = "S"
                ElseIf li.Selected And DDlStstusType.SelectedItem.Text = "Stop Payment Revoke" And li.Value.ToString = "Stop" And li.Value.ToString <> "Withdrawed" And li.Value.ToString <> "Returned" Then
                    StrStatus = "1"
                ElseIf li.Value.ToString = "Returned" Then
                    StrStatus = "R"
                ElseIf DDlStstusType.SelectedItem.Text = "Returned" And li.Selected Then
                    StrStatus = "R"
                ElseIf li.Value.ToString = "Withdrawed" Then
                    StrStatus = "W"

                ElseIf li.Value.ToString = "Stop" Then
                    StrStatus = "S"
                Else
                    StrStatus = "1"
                End If
                'IIf(li.Selected, "S", "1")

                '            Str1 = Str1 + li.Value.ToString() + li.Attributes.Item(li.Text)
                Str = Str + IIf(Str = "", "", "-") + StrStatus
            Next



            M_DBConn = New Connection()
            M_Sql = " UPDATE  Gen_ChequeMaster_M_Tbl SET Cm_Chq_Status= '" & Str & "' FROM  Gen_ChequeMaster_M_Tbl where  acc_ID= '" & HddAccID.Value & "' and Cm_Chq_BookType= '" & DDlChqType.SelectedValue.ToString & "' AND Cm_Chq_No_From = " & HddStChequeNo.Value & ""
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
            M_DBConn = Nothing
            TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
        Catch ex As Exception

        End Try








    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim M_AccNo As String = TxtAccNo.Text
        GrViewCheque.DataSource = Nothing
        GrViewCheque.DataBind()
        ChkStChequeNo.Items.Clear()
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
            HddAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()

            TxtBalance.Text = Format(Val(Dt_AcDtls.Rows(0)("Cur_Bal").ToString()), "#0.00")


        End If

        TxtAccNo.Text = M_AccNo

        M_CommonBL = Nothing

        If HddAccID.Value > 0 Then
            Dim M_DBConn1 As New Connection
            Dim Dt_AcChequeDetails As New DataTable

            M_DBConn1.ExecuteDataTable(Dt_AcChequeDetails, " SELECT  ACC_ID, Tran_No, Sch_Code, Acc_No, Br_Code, Cm_Chq_No_From, Cm_Chq_No_To,  Cm_Chq_BookType, Cm_Chq_Date,Cm_Chq_Status FROM  Gen_ChequeMaster_M_Tbl where  acc_ID= '" & HddAccID.Value & "' and Cm_Chq_BookType= '" & DDlChqType.SelectedValue.ToString & "'", False)
            GrViewCheque.DataSource = Dt_AcChequeDetails
            GrViewCheque.DataBind()

            M_DBConn1 = Nothing
        Else
            GrViewCheque.DataSource = Nothing
            GrViewCheque.DataBind()
        End If


        'If HddAccID.Value > 0 Then
        '    Dim M_DBConn1 As New Connection
        '    Dim Dt_AcChequeDetails As New DataTable
        '    M_DBConn1.ExecuteDataTable(Dt_AcChequeDetails, " SELECT  ACC_ID, Tran_No, Sch_Code, Acc_No, Br_Code, Cm_Chq_No_From, Cm_Chq_No_To,  Cm_Chq_BookType, Cm_Chq_Date FROM  Gen_ChequeMaster_M_Tbl where  acc_ID= '" & HddAccID.Value & "'", False)

        '    M_DBConn1 = Nothing
        'Else
        '    GrViewCheque.DataSource = Nothing
        '    GrViewCheque.DataBind()
        'End If




    End Sub

    Private Sub Load_DefaultValues()


        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        CmbBranch.Enabled = False
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        M_CommonBL = Nothing
        SchemeDt = DirectCast(Cache("SchemeTbl"), DataTable)
        AcHeadDt = DirectCast(Session("AcHeadTbl"), DataTable)

    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtSchCode.Text = CmbScheme.SelectedValue
            Set_SchemeCtrl()
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Private Sub Set_SchemeCtrl()
        Try
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
      
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Sub
    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub DDlChqType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlChqType.SelectedIndexChanged
        'Dim M_CommonBL As New CommonBL

        TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)


    End Sub



    Protected Sub GrViewCheque_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrViewCheque.RowCommand

    End Sub

    Protected Sub GrViewCheque_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrViewCheque.SelectedIndexChanged
        Dim GDr As GridViewRow = GrViewCheque.SelectedRow
        FillChequeBook(GDr.Cells(1).Text, GDr.Cells(8).Text)
        HddStChequeNo.Value = GDr.Cells(1).Text
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click

    End Sub
    Public Function FillChequeBook(ByVal StartingNo As Integer, ByVal StatusStr As String) As String
        Dim Status As String()
        Dim I As Integer = 1
        Dim StartingChqNo As Integer
        Dim EndingChequeNo As Integer
        ChkStChequeNo.Items.Clear()
        Status = Split(StatusStr, "-")
        For Each Str As String In Status 'StatusStr

            If Str = "1" Then
                'DDLChangeStatus.Items(I).Text = StartingNo.ToString + " " + "Avilable"
                '  DDLChangeStatus.Items(I).Value = StartingNo.ToString
                'DDLChangeStatus.Items.Add(StartingNo.ToString + " " + "Available")

                ChkStChequeNo.Items.Add(New ListItem(StartingNo.ToString, "A"))

                ' X.Attributes.Add(("readonly", "readonly"))

                ' CheckBoxList1.Items.Add(New ListItem("A", "A"))
                ' cblMulti.Items.Add(New ListItem("Items 3", "Items 3"))

            ElseIf Str = "R" Then
                Dim LstItem As New ListItem(StartingNo.ToString + " " + "Returned", "Returned")
                LstItem.Enabled = False
                ChkStChequeNo.Items.Add(LstItem)
                'DDLChangeStatus.Items(I).Text = StartingNo.ToString + " " + "Withdrawed"
                ' DDLChangeStatus.Items.Add(StartingNo.ToString + " " + "Withdrawed")
            ElseIf Str = "W" Then
                Dim LstItem As New ListItem(StartingNo.ToString + " " + "Withdrawed", "Withdrawed")
                LstItem.Enabled = False
                ChkStChequeNo.Items.Add(LstItem)
                ''''ChkStChequeNo.Items.Add(New ListItem("Withdrawed", "Withdrawed"))
                'DDLChangeStatus.Items(I).Text = StartingNo.ToString + " " + "Withdrawed"
                ' DDLChangeStatus.Items.Add(StartingNo.ToString + " " + "Withdrawed")
            ElseIf Str = "S" Then
                Dim LstItem As New ListItem(StartingNo.ToString + " " + "Stop", "Stop")
                ChkStChequeNo.Items.Add(LstItem)
                '''' ChkStChequeNo.Items.Add(New ListItem("Stop", "Stop"))
                'DDLChangeStatus.Items(I).Text = StartingNo.ToString + " " + "Withdrawed"
                ' DDLChangeStatus.Items.Add(StartingNo.ToString + " " + "Withdrawed")
            End If


            StartingNo += 1
            I += 1
        Next
        Return ""


    End Function


    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        On Error Resume Next
        CmbScheme.SelectedValue = TxtSchCode.Text
        TxtSchCode.Text = CmbScheme.SelectedValue
        Set_SchemeCtrl()
        TxtShortCode.Text = ""
        TxtShortCode.Focus()
    End Sub



    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
    '    Dim Str As String
    '    Str = ""
    '    For Each li As ListItem In ChkStChequeNo.Items
    '        '  li.Selected = True
    '        Str = Str + IIf(Str = "", "", "-") + IIf(li.Selected, "S", "1")
    '    Next
    '    Button2.Text = Str
    'End Sub



    Protected Sub DDlStstusType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDlStstusType.SelectedIndexChanged

    End Sub

    Protected Sub ChkStChequeNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkStChequeNo.SelectedIndexChanged

    End Sub
End Class