Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class NoticeChittyPrint

    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1111
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
            TxtFrDate.Text = Session("TransDate")
        End If
        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)
    End Sub
    Private Function Load_DefaultValues()
        Try
            M_CommonBl.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))
            M_CommonBl.Fill_Pickup(CmbNoticeType, 64, 0)

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Function


    Protected Sub CmbNoticeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbNoticeType.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As DataTable

            M_Dt = M_CommonBL.Get_NoticeCodes(TxtScheme.Text, CmbNoticeType.SelectedValue, Session("TransDate"))

            If M_Dt.Rows.Count > 0 Then
                TxtNoticeAmt.Text = Format(Val(M_Dt.Rows(0)("Amount").ToString), "#0.00")
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
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
        Try
            Dim M_ChittyGroupBL As New ChittyGroupBL
            M_ChittyGroupBL.Get_ChittyGrp(CmbChitty, CmbBranch.SelectedValue, CmbScheme.SelectedValue)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
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
            M_Dt = M_CommonBL.Process_ChittyNotice(CmbBranch.SelectedValue, TxtScheme.Text, TxtFrDate.Text, Session("HostID"), Session("UserID"), CmbChitty.SelectedValue, CmbNoticeSentTo.SelectedValue, M_ParameterSw.ToString.Replace("DocumentElement", "NewDataSet"))
            DgvNoticeNew.DataSource = M_Dt
            DgvNoticeNew.DataBind()

            M_CommonBL = Nothing

        Catch ex As Exception

        End Try
        DivPrint.Visible = True
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
            ParmTbl.Rows.Add(TxtScheme.Text, CmbNoticeType.SelectedValue, SendType, "", HdnNoticeHead.Value, Val(TxtNoticeAmt.Text), HdnMiscHead.Value, "0", "N", "N", "Y", "01/01/1900", "01/01/1900", "0", "0", TxtOdNoFrom.Text, TxtOdNoTo.Text)

            Session("ParmTbl") = ParmTbl
        Catch ex As Exception
            'ShowAlertMesnsage(ex.Message.ToString)
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

    Protected Sub BtnGeneratePrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGeneratePrint.Click
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
                ACC_NO = txtAccNo.Text
                C = 0
             
                M_DBConn.ExecuteDataTable(M_DT1, "Select tran_no from  LoanNotice_T_Tbl where acc_No ='" & ACC_NO & "'", False)
                If M_DT1.Rows.Count >= 1 And SendType = "S" Then ' For Sureties
                    For C = 0 To M_DT1.Rows.Count - 1

                        SqlStr = "Exec ChittyNoticePrinting_Sp '" & ACC_NO & "','" & "CHITTY NOTICE" & "','" & M_DT1.Rows(C)(0).ToString() & "', ''"
                        M_DBConn.ExecuteDataTable(M_DT, SqlStr, False)

                        Str = Str + (M_DT.Rows(0)(0).ToString() + "@@@@")
                    Next
                Else ' For Loanee
                    SqlStr = "Exec ChittyNoticePrinting_Sp '" & ACC_NO & "','" & "CHITTY NOTICE" & "','0', ''"
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
                                     "SELECT 0,L.ACC_ID, O.Acc_No, '" & TxtScheme.Text & "', L.Br_Code,'" & CmbNoticeType.SelectedValue & "', " & _
                                     "'A'," & ClsCom.DbDate(Session("TransDate")) & ", '" & SendType & "', '" & "148" & "', " & _
                                     "" & Val(TxtNoticeAmt.Text) * C & ", " & Val(TxtNoticeAmt.Text) * C & ", '01/01/1900', " & Val(TxtNoticeAmt.Text) * C & ", 'Y', " & _
                                     "'" & Session("UserID") & "','" & Session("UserID") & "'," & ClsCom.DbDate(Session("TransDate")) & "," & ClsCom.DbDate(Session("TransDate")) & ",'" & "REF" & " '" & _
                                     " FROM Temp_NoticeProcessed_R_Tbl O INNER JOIN " & _
                                     "Account_Detail_Vw L  ON O.acc_ID=L.ACC_ID/* INNER JOIN " & _
                                     "Gen_SchemeMaster_P_Tbl GS ON O.Shr_Sch_Code = GS.Sch_Code*/ " & _
                                     "WHERE /*L.Sch_Code = '" & TxtScheme.Text & "' And*/ (O.Terminal = '" & Session("HostID") & "')/*And O.UserId = '" & Session("UserID") & "'*/ AND (O.acc_NO='" & ACC_NO & "')"


                M_DBConn.ExecuteScalar(SqlStr, M_DBConn.SQLSource.STOREDPROCEDURE, False)
                M_DBConn = Nothing

            End If
            HdnText.Value = Str


        Next

    End Sub

    Protected Sub CmdClose0_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose0.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class