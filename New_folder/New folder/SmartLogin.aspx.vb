Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports System.Configuration
Imports System.Configuration.ConfigurationManager

Partial Public Class SmartLogin
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1132
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CmdClose.Attributes.Add("OnClick", "window.close();")
            DtpLoginDate.Attributes.Add("readonly", "readonly")
            Session.Add("LoginStatus", "")
            Label1.Text = Request.QueryString("msg")
            Session.Clear()
            Session.Add("SearchType", "Branch")
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "Branch" Then
                TxtBrCode.Text = Session("SearchResult")
                TxtBrCode_TextChanged(Me.TxtBrCode, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If
    End Sub

    Protected Sub CmbLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLogin.Click
        Try
            Dim M_User As New UserBL
            Dim M_UsrBr As String
            If TxtBrCode.Text = "DC" Then
                If M_User.Validate_User(TxtLogin.Text, TxtPswd.Text, M_UsrBr) = True Then
                    Dim DT_UserDetails As DataTable
                    DT_UserDetails = M_User.Get_UserID(TxtLogin.Text, DtpLoginDate.Text)
                    Session.Add("LoginUserID", DT_UserDetails.Rows(0)("User_Emp_ID").ToString)
                    Session.Add("MaxTranLimit", DT_UserDetails.Rows(0)("MaxTranAmt").ToString)

                    Session.Add("LoginID", TxtLogin.Text)
                    Session.Add("UserID", TxtLogin.Text)

                    Session.Add("LoginPswd", TxtPswd.Text)
                    Session.Add("TransDate", DtpLoginDate.Text)
                    Session.Add("DefaultBrCode", TxtBrCode.Text)
                    Session.Add("File_Path", AppSettings("REPORT_FOLDER"))
                    Session("TestLogin") = TxtLogin.Text
                    Dim com As New CommonBL
                    Dim dt As New DataTable
                    dt = com.HO_Open(DtpLoginDate.Text, "O")
                    If dt.Rows.Count > 0 Then
                        Response.Redirect("~/Main.aspx?UsrID=" & TxtLogin.Text)
                    Else
                        Response.Redirect("~/DayOpenProcess.aspx")
                        Session.Add("UserID", "")
                    End If
                Else
                    ShowAlertMessage("Invalid UserName or Password")
                End If
            Else
                Dim M_CommonBL As New CommonBL
                Dim M_Dt As New DataTable
                Dim M_Sql As String
                Dim Message As String
                Dim M_Conn As New Connection
                Dim TDate As Date
                M_Dt = M_CommonBL.Fill_BranchDtls(TxtBrCode.Text)
                If M_Dt.Rows.Count = 0 Then
                    ShowAlertMessage("Invalid Branch Code Entered")
                    Exit Sub
                End If
                Session("DefDist") = M_Dt.Rows(0).Item("Br_DefDist")
                Session("DefState") = M_Dt.Rows(0).Item("Br_DefState")
                Session("DefCountry") = M_Dt.Rows(0).Item("Br_DefCountry")
                Session("GoldLimitPerDayForBr") = M_Dt.Rows(0).Item("Br_GoldLimitPerDayForBr")
                Session("GoldLimitPerDayForCust") = M_Dt.Rows(0).Item("Br_GoldLimitPerDayForCust")
                Session("IsDayOpened") = M_Dt.Rows(0).Item("Br_DayOpenStatus")
                Session("BrRegion") = M_Dt.Rows(0).Item("Br_Region")
                Session("HostID") = M_CommonBL.Get_LoginID(TxtBrCode.Text)
                If M_User.Validate_User(TxtLogin.Text, TxtPswd.Text, M_UsrBr) = True Then
                    If (M_User.Validate_UserAssignedBranch(TxtLogin.Text, TxtBrCode.Text).Rows.Count = 0 And TxtLogin.Text.ToUpper <> "SA") And (M_UsrBr <> "%") Then
                        ShowAlertMessage("User Not Assigned to " & TxtBrCode.Text & " Branch")
                        Exit Sub
                    End If

                    Dim DT_UserDetails As DataTable
                    DT_UserDetails = M_User.Get_UserID(TxtLogin.Text, DtpLoginDate.Text)
                    If DT_UserDetails.Rows(0)("User_Active").ToString <> "Y" Then
                        ShowAlertMessage("Invalid User")
                        TxtLogin.Text = ""
                        TxtPswd.Text = ""
                        TxtBrCode.Text = ""
                        TxtBrName.Text = ""
                        Exit Sub
                    End If
                    Session.Add("LoginUserID", DT_UserDetails.Rows(0)("User_Emp_ID").ToString)
                    Session.Add("MaxTranLimit", DT_UserDetails.Rows(0)("MaxTranAmt").ToString)
                    Session.Add("UserType", DT_UserDetails.Rows(0)("User_Type").ToString)
                    Session.Add("LoginID", TxtLogin.Text)
                    Session.Add("UserID", TxtLogin.Text)

                    Session.Add("LoginPswd", TxtPswd.Text)
                    Session.Add("TransDate", DtpLoginDate.Text)
                    Session.Add("DefaultBrCode", TxtBrCode.Text)
                    Session.Add("File_Path", AppSettings("REPORT_FOLDER"))
                    Session("TestLogin") = TxtLogin.Text
                    Session.Add("ASSIGNEDBRANCH", DT_UserDetails.Rows(0)("Assigned_Branch").ToString)
                    '   Dim M_DtMessage As New DataTable

                    M_Dt = M_CommonBL.Fill_BranchDtls(TxtBrCode.Text)
                    Session("DefDist") = M_Dt.Rows(0).Item("Br_DefDist")
                    Session("DefState") = M_Dt.Rows(0).Item("Br_DefState")
                    Session("DefCountry") = M_Dt.Rows(0).Item("Br_DefCountry")
                    Session("Br_Name") = M_Dt.Rows(0).Item("Br_Name")
                    Session("BrRegion") = M_Dt.Rows(0).Item("Br_Region")

                    Dim AcheadDt As New DataTable
                    AcheadDt = M_CommonBL.Get_AccountHead()
                    HttpContext.Current.Cache("Achead") = AcheadDt
                    M_CommonBL = Nothing
                    Message = ""
                    Dim M_RemainderBl As New RemainderBL
                    Session("RemainderTbl") = M_RemainderBl.Set_GenRemainderGrid
                    Dim M_DtMessage As DataTable = DirectCast(Session("RemainderTbl"), DataTable)
                    If M_DtMessage.Rows.Count > 0 Then
                        For I = 0 To M_DtMessage.Rows.Count - 1
                            If I = 0 Then
                                Message = Message + (M_DtMessage.Rows(I)(2).ToString & " By[" & M_DtMessage.Rows(I)(3).ToString & "] ")
                            Else
                                Message = Message + (" :::: " & M_DtMessage.Rows(I)(2).ToString & " By[" & M_DtMessage.Rows(I)(3).ToString & "] ")
                            End If
                        Next
                        Session.Add("GeneralMessage", Message)
                    Else
                        Session.Add("GeneralMessage", "HAVE A NICE DAY")
                    End If

                    M_CommonBL = Nothing

                    Dim BrDayOpenStatus As Char
                    Dim M_CommonDa As New SMARTCORE_DA.CommonDA
                    M_Dt = M_CommonDa.Fill_BranchDayOpenStatus(TxtBrCode.Text)

                    BrDayOpenStatus = M_Dt.Rows(0).Item("Br_DayEndStatus")
                    TDate = DtpLoginDate.Text
                    If BrDayOpenStatus = "O" Then
                        M_Sql = "select * from Login_Tbl where Username='" & TxtLogin.Text & "' and Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Status='Y' And Br_Code='" & Session("DefaultBrCode") & "'"
                        M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                        If M_Dt.Rows.Count > 0 Then
                            M_Sql = "Update Login_Tbl set Status='Y'  where  Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Username='" & TxtLogin.Text & "'And Br_Code='" & Session("DefaultBrCode") & "'"
                            M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                            Response.Redirect("~/Main.aspx?UsrID=" & TxtLogin.Text)
                        Else
                            M_Sql = "select * from Login_Tbl where Username='" & TxtLogin.Text & "' and Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Status='N' And Br_Code='" & Session("DefaultBrCode") & "'"
                            M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                            If M_Dt.Rows.Count > 0 Then
                                M_Sql = "Update Login_Tbl set Status='Y'  where  Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Username='" & TxtLogin.Text & "'And Br_Code='" & Session("DefaultBrCode") & "'"
                                M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                                Response.Redirect("~/Main.aspx?UsrID=" & TxtLogin.Text)

                            Else
                                M_Sql = "Update Login_Tbl set Status='Y'  where  Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Username='" & TxtLogin.Text & "'And Br_Code='" & Session("DefaultBrCode") & "'"
                                M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                                Save()
                                Response.Redirect("~/Main.aspx?UsrID=" & TxtLogin.Text)
                            End If
                        End If

                    Else
                        M_Sql = "Update Login_Tbl set Status='Y'  where  Tr_Date='" & Format(TDate, "MM/dd/yyyy") & "' and Username='" & TxtLogin.Text & "'And Br_Code='" & Session("DefaultBrCode") & "'"
                        M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
                        Save()
                        Response.Redirect("~/DayOpenProcess.aspx")
                        Session.Add("UserID", "")

                    End If


                Else
                    ShowAlertMessage("Invalid UserName or Password")
                End If
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

    Protected Sub TxtBrCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBrCode.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Dt As New DataTable

            If TxtBrCode.Text = "DC" Then

                M_Dt = M_CommonBL.Fill_GenSettingsDC(TxtBrCode.Text)
                If M_Dt.Rows.Count > 0 Then
                    DtpLoginDate.Text = M_Dt.Rows(0)(0).ToString()
                    CmbLogin.Enabled = True
                Else
                    DtpLoginDate.Text = Date.Now.ToString("dd/MM/yyyy")
                    CmbLogin.Enabled = False
                End If
            Else
                M_Dt = M_CommonBL.Fill_GenSettings(TxtBrCode.Text)
                If M_Dt.Rows.Count > 0 Then
                    Session("DefShareBranch") = M_Dt.Rows(0).Item("Share_HoldingBranch")
                    DtpLoginDate.Text = M_Dt.Rows(0).Item("Trans_Date") 'Date.Now.ToString("dd/MM/yyyy")
                    CmbLogin.Enabled = True
                    TxtLogin.Focus()
                Else
                    DtpLoginDate.Text = Date.Now.ToString("dd/MM/yyyy")
                    CmbLogin.Enabled = True
                End If
            End If
            Dim DtTbl As New DataTable
            Dim commonBL As New CommonBL
            DtTbl = commonBL.get_brname(TxtBrCode.Text)
            TxtBrName.Text = DtTbl.Rows(0)("Br_Name").ToString

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtLogin_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtLogin.TextChanged

    End Sub

    Protected Sub TxtPswd_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPswd.TextChanged

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose.Click
        'Me.Dispose()
        Try
            Response.Write("<script language='javascript'> { self.close() }</script>")
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Save()
        Dim M_Login As New SMARTCORE_BO.LoginBo
        With M_Login
            .BrCode = TxtBrCode.Text
            .Username = TxtLogin.Text
            .Status = "Y"
            .TrDate = DtpLoginDate.Text
        End With
        Dim M_LoginBL As New LoginBL
        Session("M_EntryMode") = "NEW"
        M_LoginBL.Save_Data(M_Login, Session("M_EntryMode"))
        M_LoginBL = Nothing
    End Sub

End Class