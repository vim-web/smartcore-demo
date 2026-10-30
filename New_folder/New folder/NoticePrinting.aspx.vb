Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class NoticePrinting
    Inherits System.Web.UI.Page
    ' Public ParmTbl As New DataTable("Table")
    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1112
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
            TxtAsOnDate.Text = Session("TransDate")
            TxtBranch.Attributes.Add("readonly", "readonly")
            TxtAuctionDate.Text = Session("TransDate")
            TxtTime.Text = Session("TransDate")

        End If

        ' If Not IsPostBack Then

        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)
        'DrpPrinters.Items.Add("WeP 800 DX (Copy 1)")

        '""
        ' End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbLoanTyype, Session("TransDate"), "LOAN")
        M_CommonBL.Fill_Pickup(CmbPrintType, 75, 0)
        M_CommonBL.Fill_Pickup(CmbNoticeType, 64, 0)

        M_CommonBL.Fill_Pickup(Lst_ActExcept, 64, 0)
        M_CommonBL.Fill_Pickup(Lst_ActInclude, 64, 0)

        CmbBranch.SelectedValue = Session("DefaultBrCode")

        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call CmbLoanTyype_SelectedIndexChanged(Me.CmbLoanTyype, System.EventArgs.Empty)

        DivPrint.Visible = False
        DivLoanNo.Visible = False
        DivOdDate.Visible = False
        DivODNo.Visible = False
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        CmbBranch.SelectedValue = TxtBranch.Text
        TxtBranch.Text = CmbBranch.SelectedValue
    End Sub

    Protected Sub ChkActionType_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkActionType.CheckedChanged
        If ChkActionType.Checked = True Then
            DivActExcept.Visible = True
            DivActInclude.Visible = True
        Else
            DivActExcept.Visible = False
            DivActInclude.Visible = False
        End If
    End Sub




    'Private Sub Set_Grid()
    '    Dim column As New DataColumn()
    '    ' Add the column to a new DataTable.
    '    ParmTbl.Columns.Add("ActionType", GetType(String))
    '    ParmTbl.Columns.Add("NoticeType", GetType(String))
    '    ParmTbl.Columns.Add("ISLonee", GetType(String))
    '    ParmTbl.Columns.Add("RefNo", GetType(String))
    '    ParmTbl.Columns.Add("NoticeCode", GetType(String))
    '    ParmTbl.Columns.Add("NoticeAmt", GetType(Double))
    '    ParmTbl.Columns.Add("MiscCode", GetType(String))
    '    ParmTbl.Columns.Add("MiscAmt", GetType(Double))
    '    ParmTbl.Columns.Add("IsOdDate", GetType(String))
    '    ParmTbl.Columns.Add("IsLoanNo", GetType(String))
    '    ParmTbl.Columns.Add("IsODNo", GetType(String))
    '    ParmTbl.Columns.Add("OdFromDt", GetType(Date))
    '    ParmTbl.Columns.Add("OdToDt", GetType(Date))
    '    ParmTbl.Columns.Add("FromAccNo", GetType(String))
    '    ParmTbl.Columns.Add("ToAccNo", GetType(String))
    '    ParmTbl.Columns.Add("ODFromNo", GetType(String))
    '    ParmTbl.Columns.Add("ODToNo", GetType(String))
    '    ParmTbl.Columns.Add(column)
    '    ParmTbl.Rows.Add("", "", "", "", "", 0, "", 0, "", "", "", "01/01/1900", "01/01/1900", "", "", "", "")

    '    Session("ParmTbl") = ParmTbl

    'End Sub


    Protected Sub CmbNoticeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNoticeType.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            M_Dt = M_CommonBL.Get_NoticeCodes(CmbLoanTyype.SelectedValue, CmbNoticeType.SelectedValue, Session("TransDate"))

            If M_Dt.Rows.Count > 0 Then
                TxtNoticeAmt.Text = Format(Val(M_Dt.Rows(0)("Amount").ToString), "#0.00")
                TxtMiscAmt.Text = Format(Val(M_Dt.Rows(0)("Misc_Amount").ToString), "#0.00")
                HdnNoticeHead.Value = M_Dt.Rows(0)("Notice_AccCode").ToString
                HdnMiscHead.Value = M_Dt.Rows(0)("Misc_AccCode").ToString
            End If

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

    Protected Sub CmbLoanTyype_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLoanTyype.SelectedIndexChanged
        Call CmbNoticeType_SelectedIndexChanged(Me.CmbNoticeType, System.EventArgs.Empty)
    End Sub

    Private Sub Set_Parameter()
        Try
            Dim SendType As String
            Dim ParmTbl As New DataTable("Table")
            ParmTbl.Columns.Add("SchCode", GetType(String))
            ParmTbl.Columns.Add("ActionType", GetType(String))
            ParmTbl.Columns.Add("SendType", GetType(String))
            ParmTbl.Columns.Add("RefNo", GetType(String))
            ParmTbl.Columns.Add("NoticeCode", GetType(String))
            ParmTbl.Columns.Add("NoticeAmt", GetType(Double))
            ParmTbl.Columns.Add("MiscCode", GetType(String))
            ParmTbl.Columns.Add("MiscAmt", GetType(Double))
            ParmTbl.Columns.Add("IsOdDate", GetType(String))
            ParmTbl.Columns.Add("IsLoanNo", GetType(String))
            ParmTbl.Columns.Add("IsODNo", GetType(String))
            ParmTbl.Columns.Add("OdFromDt", GetType(Date))
            ParmTbl.Columns.Add("OdToDt", GetType(Date))
            ParmTbl.Columns.Add("FromAccNo", GetType(String))
            ParmTbl.Columns.Add("ToAccNo", GetType(String))
            ParmTbl.Columns.Add("ODFromNo", GetType(String))
            ParmTbl.Columns.Add("ODToNo", GetType(String))


            If OptLoanee.Checked Then
                SendType = "L"
            ElseIf OptSurties.Checked Then
                SendType = "S"
            Else
                SendType = "B"
            End If
            ParmTbl.Rows.Add(CmbLoanTyype.SelectedValue, CmbNoticeType.SelectedValue, SendType, TxtRefNo.Text, HdnNoticeHead.Value, Val(TxtNoticeAmt.Text), HdnMiscHead.Value, Val(TxtMiscAmt.Text), IIf(Chk_BetOD_Date.Checked, "Y", "N"), IIf(ChkLoanNo.Checked, "Y", "N"), IIf(ChkOdNo.Checked, "Y", "N"), IIf(Chk_BetOD_Date.Checked, TxtOdDateFrom.Text, "01/01/1900"), IIf(Chk_BetOD_Date.Checked, TxtOdDateTo.Text, "01/01/1900"), TxtLoanNoFrom.Text, TxtLoanNoTo.Text, TxtOdNoFrom.Text, TxtOdNoTo.Text)
            Session("ParmTbl") = ParmTbl
        Catch ex As Exception
            'ShowAlertMesnsage(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub CmdProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdProcess.Click
        Try
            Dim M_ParameterSw As New IO.StringWriter
            Set_Parameter()
            Dim ParaDt As DataTable = DirectCast(Session("ParmTbl"), DataTable)
            ParaDt.WriteXml(M_ParameterSw)


            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable
            M_Dt = M_CommonBL.Process_Notice(CmbBranch.SelectedValue, CmbLoanTyype.SelectedValue, TxtAsOnDate.Text, Session("HostID"), Session("UserID"), M_ParameterSw.ToString.Replace("DocumentElement", "NewDataSet"))
            DgvNotice.DataSource = M_Dt
            DgvNotice.DataBind()

            DgvNoticeNew.DataSource = M_Dt
            DgvNoticeNew.DataBind()


            M_CommonBL = Nothing

        Catch ex As Exception

        End Try
        DivPrint.Visible = True
    End Sub

    Protected Sub DgvNotice_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvNotice.PageIndexChanging
        'DgvNotice.PageIndex = e.NewPageIndex
        'DgvNotice.DataBind()
    End Sub


    Protected Sub DgvNotice_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvNotice.SelectedIndexChanged

    End Sub



    Protected Sub DgvNotice_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DgvNotice.RowCommand
        Try

            Dim M_DT As DataTable
            Dim SendType As String
            If OptLoanee.Checked Then
                SendType = "L"
            ElseIf OptSurties.Checked Then
                SendType = "S"
            Else
                SendType = "B"
            End If

            If e.CommandName.ToUpper = "SELECT" Then
                Dim ACC_NO As String

                Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
                Dim SqlStr As String
                Dim RowIndex As Integer = CInt(e.CommandArgument.ToString())


                ACC_NO = DgvNotice.DataKeys(RowIndex)("Acc_No").ToString()

                SqlStr = "Exec FDBondPrinting_Sp_NEW '" & ACC_NO & "','" & "GOLD NOTICE" & "',''"
                M_DBConn.ExecuteDataTable(M_DT, SqlStr, False)
                HdnText.Value = M_DT.Rows(0)(0).ToString()

 


                SqlStr = "INSERT INTO Loan_ActionTaken_M_Tbl " & _
                                     "(Tran_ID ,Acc_ID,Acc_No,Sch_Code,Br_Code,Action_Type, " & _
                                     "Status,Reg_Date,Reg_No,GL_Code,Amount,SettlmentAmt,StopIntDate, " & _
                                     "Balance,Authorised,MakerID,CheckerID, " & _
                                     "Making_Time,Checking_Time,Remarks) " & _
                                     "SELECT 0,.Dbo.Get_CoreBankingAccID_Fn(O.Br_CODE,O.SCH_CODE,O.Acc_No,'0'), O.Acc_No, '" & CmbLoanTyype.SelectedValue & "', O.Br_Code,'" & CmbNoticeType.SelectedValue & "', " & _
                                     "'A'," & ClsCom.DbDate(Session("TransDate")) & ", '" & "L" & "', '" & "148" & "', " & _
                                     "" & Val(TxtNoticeAmt.Text) & ", " & Val(TxtNoticeAmt.Text) & ", '01/01/1900', " & Val(TxtNoticeAmt.Text) & ", 'Y', " & _
                                     "'" & Session("UserID") & "','" & Session("UserID") & "'," & ClsCom.DbDate(Session("TransDate")) & "," & ClsCom.DbDate(Session("TransDate")) & ",'" & "REF" & " '" & _
                                     " FROM Loan_Outstand_R_Tbl O INNER JOIN " & _
                                     "Loan_Detail_Vw L ON L.Sch_Code = O.Sch_Code AND L.AcNo = O.Acc_No AND L.BrCode = O.Br_Code INNER JOIN " & _
                                     "Gen_SchemeMaster_P_Tbl GS ON O.Shr_Sch_Code = GS.Sch_Code " & _
                                     "WHERE O.Sch_Code = '" & CmbLoanTyype.SelectedValue & "' And (O.Terminal = '" & Session("HostID") & "') And O.UserId = '" & Session("UserID") & "' AND (O.acc_NO='" & ACC_NO & "')"


                M_DBConn.ExecuteScalar(SqlStr, M_DBConn.SQLSource.STOREDPROCEDURE, False)

                M_DBConn = Nothing
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub


    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = DirectCast(DgvNoticeNew.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If chkAll.Checked = True Then
            For Each gvRow As GridViewRow In DgvNoticeNew.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = True
                Dim txtname As TextBox = DirectCast(gvRow.FindControl("TxtCust_Name"), TextBox)
                ' Dim txtlocation As TextBox = DirectCast(gvRow.FindControl("txtLocation"), TextBox)
                txtname.[ReadOnly] = False
                ' txtlocation.[ReadOnly] = False
                txtname.ForeColor = System.Drawing.Color.Black
                'txtlocation.ForeColor = System.Drawing.Color.Black
            Next
        Else
            For Each gvRow As GridViewRow In DgvNoticeNew.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = False
                Dim txtname As TextBox = DirectCast(gvRow.FindControl("TxtCust_Name"), TextBox)
                '  Dim txtlocation As TextBox = DirectCast(gvRow.FindControl("txtLocation"), TextBox)
                txtname.[ReadOnly] = True
                ' txtlocation.[ReadOnly] = True
                txtname.ForeColor = System.Drawing.Color.Blue
                ' txtlocation.ForeColor = System.Drawing.Color.Blue
            Next
        End If
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub


    Protected Sub BtnGeneratePrint_Click(ByVal sender As Object, ByVal e As EventArgs)
      
    End Sub

   

    Protected Sub Chk_BetOD_Date_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chk_BetOD_Date.CheckedChanged
        If Chk_BetOD_Date.Checked Then
            DivOdDate.Visible = True
        Else
            DivOdDate.Visible = False
        End If
    End Sub

    Protected Sub ChkLoanNo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkLoanNo.CheckedChanged
        If ChkLoanNo.Checked Then
            DivLoanNo.Visible = True
        Else
            DivLoanNo.Visible = False
        End If
    End Sub

    Protected Sub ChkOdNo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkOdNo.CheckedChanged
        If ChkOdNo.Checked Then
            DivODNo.Visible = True
        Else
            DivODNo.Visible = False
        End If
    End Sub

    Protected Sub TxtAuctionDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAuctionDate.TextChanged

    End Sub

    Protected Sub BtnGeneratePrint_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGeneratePrint.Click
        Dim Str As String
        Dim SendType As String
        Dim C As Integer
        If OptLoanee.Checked Then
            SendType = "L"
        ElseIf OptSurties.Checked Then
            SendType = "S"
        Else
            SendType = "B"
        End If
        For Each gvRow As GridViewRow In DgvNoticeNew.Rows

            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
            If chkSel.Checked = True Then
                Dim txtAccNo As TextBox = DirectCast(gvRow.FindControl("TxtAcc_No"), TextBox)
                '  Dim txtlocation As TextBox = DirectCast(gvRow.FindControl("txtLocation"), TextBox)
                Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

                Dim M_DT As DataTable
                Dim M_DT1 As DataTable
                Dim ACC_NO As String
                Dim SqlStr As String
                Dim SqlStr1 As String
                ACC_NO = txtAccNo.Text
                C = 0

                M_DBConn.ExecuteDataTable(M_DT1, "Select tran_no from  LoanNotice_T_Tbl where acc_No ='" & ACC_NO & "'", False)
                If M_DT1.Rows.Count >= 1 And SendType = "S" Then ' For Sureties
                    For C = 0 To M_DT1.Rows.Count - 1
                        SqlStr = "Exec [LoanNoticePrinting_Sp] '" & ACC_NO & "','" & "GOLD NOTICE" & "','" & M_DT1.Rows(C)(0).ToString() & "', ''"
                        M_DBConn.ExecuteDataTable(M_DT, SqlStr, False)
                        Str = Str + (M_DT.Rows(0)(0).ToString() + "@@@@")
                    Next
                Else ' For Loanee
                    SqlStr = "Exec [LoanNoticePrinting_Sp] '" & ACC_NO & "','" & "GOLD NOTICE" & "','0', ''"
                    M_DBConn.ExecuteDataTable(M_DT, SqlStr, False)
                    Str = Str + (M_DT.Rows(0)(0).ToString() + "@@@@")
                    C = C + 1
                End If


                SqlStr = " Delete from  Loan_ActionTaken_M_Tbl where acc_No ='" & ACC_NO & "' and  Reg_NO ='" & SendType & "'  and Reg_Date =" & ClsCom.DbDate(Session("TransDate"))


                M_DBConn.ExecuteScalar(SqlStr, M_DBConn.SQLSource.STOREDPROCEDURE, False)
                SqlStr = "INSERT INTO Loan_ActionTaken_M_Tbl " & _
                                     "(Tran_ID ,Acc_ID,Acc_No,Sch_Code,Br_Code,Action_Type, " & _
                                     "Status,Reg_Date,Reg_No,GL_Code,Amount,SettlmentAmt,StopIntDate, " & _
                                     "Balance,Authorised,MakerID,CheckerID, " & _
                                     "Making_Time,Checking_Time,Remarks) " & _
                                     "SELECT 0,.Dbo.Get_CoreBankingAccID_Fn(O.Br_CODE,O.SCH_CODE,O.Acc_No,'0'), O.Acc_No, '" & CmbLoanTyype.SelectedValue & "', O.Br_Code,'" & CmbNoticeType.SelectedValue & "', " & _
                                     "'A'," & ClsCom.DbDate(Session("TransDate")) & ", '" & SendType & "', '" & "148" & "', " & _
                                     "" & Val(TxtNoticeAmt.Text) * C & ", " & Val(TxtNoticeAmt.Text) * C & ", '01/01/1900', " & Val(TxtNoticeAmt.Text) * C & ", 'Y', " & _
                                     "'" & Session("UserID") & "','" & Session("UserID") & "'," & ClsCom.DbDate(Session("TransDate")) & "," & ClsCom.DbDate(Session("TransDate")) & ",'" & "REF" & " '" & _
                                     " FROM Loan_Outstand_R_Tbl O INNER JOIN " & _
                                     "Loan_Detail_Vw L ON L.Sch_Code = O.Sch_Code AND L.AcNo = O.Acc_No AND L.BrCode = O.Br_Code INNER JOIN " & _
                                     "Gen_SchemeMaster_P_Tbl GS ON O.Shr_Sch_Code = GS.Sch_Code " & _
                                     "WHERE O.Sch_Code = '" & CmbLoanTyype.SelectedValue & "' And (O.Terminal = '" & Session("HostID") & "') And O.UserId = '" & Session("UserID") & "' AND (O.acc_NO='" & ACC_NO & "')"

                M_DBConn.ExecuteScalar(SqlStr, M_DBConn.SQLSource.STOREDPROCEDURE, False)
                M_DBConn = Nothing
            End If
            HdnText.Value = Str


        Next
    End Sub

  
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class