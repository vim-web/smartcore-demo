Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class SubsidaryRpt
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Shared Obj_UserRights As Object

    Public M_CommonBL As New CommonBL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                M_DBConn = New Connection



                M_CommonBL.Fill_Branch(DdlBranch)

                DdlBranch.SelectedValue = Session("DefaultBrCode")
                TxtFromDate.Text = Session("TransDate")
                TxtToDate.Text = Session("TransDate")

                CmbSection.Items.Add("SHARE")
                CmbSection.Items.Add("DEPOSIT")
                CmbSection.Items.Add("LOAN")
                CmbSection.Items.Add("GOLD LOAN")
                CmbSection.Items.Add("GROUP DEPOSIT")
                CmbSection.Items.Add("BANK ACCOUNT")
                CmbSection.Items.Add("SUSPENSE")
                CmbSection.Items.Add("INVESTMENT")
                CmbSection.Items.Add("BORROWINGS")
                CmbSection.Items.Add("LOCKER")
                CmbSection.Items.Add("GENERAL VOUCHER")
                CmbSection.Items.Add("BILLS")
                CmbSection.Items.Add("INVENTORY")
                CmbSection.Items.Add("HO TRANSACTIONS")

                CmbHead.Enabled = False


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


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        CmbHead.Enabled = True
        M_CommonBL.Fill_Section(CmbHead, CmbSection.SelectedItem.Text)

    End Sub








    Protected Sub ImgProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcess.Click
        Dim Sql_String As String
        Dim M_Dt As DataTable
        Dim ClsComm As New CommonClass()


        Dim TranType As String

        If Opt_Receipt.Checked = True Then
            TranType = "C"
        ElseIf Opt_Payment.Checked = True Then
            TranType = "D"
        Else
            TranType = ""
        End If
        'If Opt_Scheme.Checked = True Then

        '    Sql_String = "Exec Gen_Subsidiary_Section_Sp '" & CmbSection.SelectedValue & "','" & M_CommonBL.GetAccountCode(CmbHead.SelectedItem.Text) & "','" & DdlBranch.SelectedValue & "'," & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtToDate.Text) & ",'" & TranType & "','" & Session("UserId") & "','" & Session("HostId") & "'"
        'Else

        Sql_String = "Exec Gen_Subsidiary_Sp '" & M_CommonBL.GetAccountCode(CmbHead.SelectedItem.Text) & "','" & DdlBranch.SelectedValue & "'," & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtToDate.Text) & ",'" & TranType & "','" & Session("UserId") & "','" & Session("HostId") & "'"
        'End If
        M_DBConn.ExecuteDataTable(M_Dt, Sql_String, False)
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim i As Integer
        Dim Str_User As String
        Dim Report_Name As String
        Dim Schemes As String
        Dim Str_AccGroup As String
        Dim Sql_Str As String
        Dim Sort_By As String
        

        Str_User = ""

 
        
        If Trim(CmbSection.Text) <> "-- NO SCHEMES --" Then
            If CmbHead.SelectedIndex = 0 Then
                For i = 1 To CmbHead.Items.Count - 1
                    Schemes = Schemes & "'" & CmbHead.SelectedItem.Text & "',"
                Next
                Schemes = Mid(Schemes, 1, Len(Schemes) - 1)
            Else
                Schemes = "'" & CmbHead.SelectedItem.Value & "'"
            End If
        End If


        If Opt_GlHeadWise.Checked = True Then

            Select Case CmbSection.SelectedValue.ToString
                Case "GOLD LOAN"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_GOLD_Pay_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_GOLD_Rec_Tot.rpt"
                    End If
                Case "LOAN"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_Loan_Rec_Tot.rpt"
                    End If
                Case "DEPOSIT"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Deposit_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_Deposit_Tot.rpt"
                    End If
                Case "BANK ACCOUNT"
                    '    Report_Name = "Subsidiary80.rpt"
                    Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Schemes = ""
                Case "HO TRANSACTIONS"
                    '    Report_Name = "Subsidiary80.rpt"
                    Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Schemes = ""
                Case "GENERAL VOUCHER"
                    '    Report_Name = "Subsidiary80.rpt"
                    Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Schemes = ""
                Case "GROUP DEPOSIT"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Rec_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_GDS_Tot.rpt"
                    End If
                Case "BORROWINGS"
                    Report_Name = "Subsidiary_Rec_Tot.rpt"
                Case "INVESTMENT"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_Rec_Tot.rpt"
                    End If
                Case "BILLS"
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_Loan_Rec_Tot.rpt"
                    End If
                Case Else
                    If Opt_Payment.Checked = True Then
                        Report_Name = "Subsidiary_Pay_Tot.rpt"
                    Else
                        Report_Name = "Subsidiary_Rec_Tot.rpt"
                    End If
            End Select
         
        Else
        Report_Name = "Subsidiary80.rpt"
        End If

        Dim Sql_String As String

        Sql_String = "SELECT G.Grp_Name, G.Grp_SeqNo, Temp_Subsidiary_R_Tbl.Acc_Code, Gen_Accountmaster_P_Tbl.Acc_Name, Gen_Accountmaster_P_Tbl.Acc_ShortName, " & _
                "Gen_Accountmaster_P_Tbl.Acc_Grp_Code, Gen_Accountmaster_P_Tbl.Acc_SeqNo, Gen_Accountmaster_P_Tbl.Acc_ALIE, " & _
                "Temp_Subsidiary_R_Tbl.*,Gen_SchemeMaster_P_Tbl.Sch_Name " & _
                "FROM Gen_AccountGrp_P_Tbl G INNER JOIN " & _
                "Gen_Accountmaster_P_Tbl ON G.Grp_Code = Gen_Accountmaster_P_Tbl.Acc_Grp_Code INNER JOIN " & _
                "Temp_Subsidiary_R_Tbl ON Gen_Accountmaster_P_Tbl.Acc_Code = Temp_Subsidiary_R_Tbl.Gl_Code  LEFT OUTER JOIN " & _
                "Gen_SchemeMaster_P_Tbl ON Temp_Subsidiary_R_Tbl.Sch_Code = Gen_SchemeMaster_P_Tbl.Sch_Code " & _
                "LEFT OUTER JOIN Gen_BranchDetails_P_Tbl B ON Temp_Subsidiary_R_Tbl.Org_Branch = B.Br_Code " & _
                "Where Terminal = '" & Session("HostId") & "' and UserId = '" & Session("LoginId") & "'"

        If Len(Trim(DdlBranch.SelectedItem.Value)) > 0 Then
            Sql_String = Sql_String + " And (Temp_Subsidiary_R_Tbl.Br_Code = '" & DdlBranch.SelectedItem.Value & "')"
            'Sql_String = Sql_String + " And (Temp_Subsidiary_R_Tbl.Org_Branch = '" & Br_Code & "')"
        End If

        Dim Tran_Type As String
        If Opt_Receipt.Checked = True Then
            Tran_Type = "C"
        ElseIf Opt_Payment.Checked = True Then
            Tran_Type = "D"
        End If

        If Len(Trim(Schemes)) > 0 Then
            If Opt_Scheme.Checked = True Then
                Sql_String = Sql_String + " And Temp_Subsidiary_R_Tbl.Sch_Code in (" & Trim(Schemes) & ")"
            Else
                Sql_String = Sql_String + " And Temp_Subsidiary_R_Tbl.Acc_Code in (" & Trim(Schemes) & ")"
            End If
        End If

        If Tran_Type = "D" Or Tran_Type = "C" Then
            Sql_String = Sql_String + "And Tran_Type = '" & Tran_Type & "'"
        End If


        Sql_String = Sql_String + " Order by Cast(Temp_Subsidiary_R_Tbl.Vr_No As Integer)"

        Dim Str_Flf As String
        Str_Flf = "Company|" & Company_Name & "|Branch|" & IIf(DdlBranch.SelectedValue = "", "Consolidation", DdlBranch.SelectedValue) & "|Subsidiary From " & TxtFromDate.Text
        Str_Flf = Str_Flf & "|User|" & Session("LoginId")

        If Opt_Scheme.Checked = True And Len(Trim(Schemes)) > 0 Then
          


            Sql_Str = "SELECT DISTINCT Gen_SchemeTranHeads_P_Tbl.Acc_Code, Gen_SchemeTranHeads_P_Tbl.Display, Gen_SchemeTranHeads_P_Tbl.Seq_No " & _
                    "FROM Temp_Subsidiary_R_Tbl INNER JOIN " & _
                    "Gen_SchemeTranHeads_P_Tbl ON Temp_Subsidiary_R_Tbl.Sch_Code = Gen_SchemeTranHeads_P_Tbl.Sch_Code " & _
                    "WHERE (Gen_SchemeTranHeads_P_Tbl.Seq_No <> 1) And Temp_Subsidiary_R_Tbl.Terminal = '" & Session("HostId") & "' And Temp_Subsidiary_R_Tbl.Sch_Code in (" & Schemes & ") "
            If Tran_Type = "C" Then
                Sql_Str = Sql_Str + " And AccGen_Type in ('B','C')"
            ElseIf Tran_Type = "D" Then
                Sql_Str = Sql_Str + " And AccGen_Type in ('B','D')"
            End If
            Sql_Str = Sql_Str + " ORDER BY Gen_SchemeTranHeads_P_Tbl.Seq_No "
         
        End If

        Dim M_Dt As DataTable
        M_DBConn.ExecuteDataTable(M_Dt, Sql_Str, False)

        Dim Temp_Str As String

        Dim J As Integer

        For i = 1 To 12
            Str_Flf = Str_Flf + "|Gl_Code" & i & "|''"
        Next

        For J = 0 To M_Dt.Rows.Count - 1
            If InStr(1, Temp_Str, "|" & M_Dt.Rows(0)("Acc_Code") & "|", vbTextCompare) = 0 Then
                Temp_Str = Temp_Str + "|Gl_Code" & J & "|" & M_Dt.Rows(0)("Acc_Code")
                Temp_Str = Temp_Str + "|Display" & J & "|" & M_Dt.Rows(0)("Display")
            End If

        Next

        Str_Flf = Str_Flf & Temp_Str

        


        Session.Add("RptQry", Sql_String)
        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts\" & Report_Name & ""))

        Session.Add("RptFormula", Str_Flf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

    End Sub
End Class