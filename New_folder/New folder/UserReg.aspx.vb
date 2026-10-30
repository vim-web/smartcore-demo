Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Partial Public Class UserReg
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1149
    Shared Obj_UserRights As Object



    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbUserType, 69, 0)
        M_CommonBL.Fill_UserEmployee(CmbEmployeeName)
        BindBranch()
        DefaultBranch()
        Call CmbEmployeeName_SelectedIndexChanged(Me.CmbEmployeeName, System.EventArgs.Empty)
        M_CommonBL = Nothing
    End Sub

    Protected Sub BindBranch()
        Dim M_CommonBL As New CommonBL
        Dim M_dt As DataTable
        M_dt = M_CommonBL.Get_BranchAreaRegion()
        If M_dt.Rows.Count > 0 Then
            GvBranchDetails.DataSource = M_dt
            GvBranchDetails.DataBind()
        End If
    End Sub
    Protected Sub DefaultBranch()
        Try
            For i = 0 To GvBranchDetails.Rows.Count - 1
                Dim row As GridViewRow = GvBranchDetails.Rows(i)
                Dim DefBranch As String = row.Cells(1).Text
                If DefBranch = CmbBranch.SelectedValue Then
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = True
                Else
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = False
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub
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
        End If
       

    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

 

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        TxtConPassword.Text = ""
        TxtUsrName.Text = ""
        TxtPassword.Text = ""
        TxtMaxAmt.Text = "0.0"
    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click


        'Add Right
        If Obj_UserRights.Add_Right = False Then
            ShowAlertMessage("You Have No Permission To Add New User!!!")
            Exit Sub
        End If

        'Passing Right
        If Obj_UserRights.Authn_Right = False Then
            ShowAlertMessage("You Have No Permission To Add New User!!!")
            Exit Sub
        End If

        Dim M_UsrRegBO As New UserBO
        Dim M_UsrRegBL As New UserBL
        Dim M_Dt1 As New DataTable

        M_Dt1 = M_UsrRegBL.Locate_UserId(TxtCustId.Text)
        If M_Dt1.Rows.Count > 0 Then
            ShowAlertMessage(" This Cust ID is Already Exist.Please enter Another Cust ID.")
            ClearData()
            Exit Sub
        End If

        If M_UsrRegBL.Validate_UserName(TxtUsrName.Text) Then
            M_UsrRegBL = Nothing
            ShowAlertMessage("User Name " & TxtUsrName.Text & " Is Already Exist.Please Enter another User Name")
            TxtUsrName.Text = ""
            TxtUsrName.Focus()
            Exit Sub
        End If


        Dim Dt_BranchArea As New DataTable
        Dt_BranchArea.TableName = "Table"
        Dt_BranchArea.Columns.Add("BranchCode")
        Dim DtRow As DataRow
        For i As Integer = 0 To GvBranchDetails.Rows.Count - 1

            Dim ChkView As CheckBox = DirectCast(GvBranchDetails.Rows(i).Cells(0).FindControl("ChkSelect"), CheckBox)
            If ChkView.Checked Then
                DtRow = Dt_BranchArea.NewRow()
                DtRow(0) = GvBranchDetails.Rows(i).Cells(1).Text
                Dt_BranchArea.Rows.Add(DtRow)
            End If
        Next
        Dim AreaMapping As New System.IO.StringWriter()
        Dt_BranchArea.WriteXml(AreaMapping)



        Try
            With M_UsrRegBO
                .UserName = TxtUsrName.Text
                .UserType = CmbUserType.SelectedValue
                .UserID = "1"
                .UserEmpID = CmbEmployeeName.SelectedValue
                .UserPwd = M_UsrRegBL.EncodeText(TxtPassword.Text)
                .UserPwdValidUpto = "01/01/1900"
                .UserStatus = IIf(CmbUserStatus.SelectedValue = "ACTIVE", "Y", "N")
                .UserAllotedUser = Session("LoginID")
                .UserAllotedTime = "01/01/1900"
                .UserActive = IIf(CmbUserStatus.SelectedValue = "ACTIVE", "Y", "N")
                .UserStatusChgUser = Session("LoginID")
                .UserStatusChgTime = "01/01/1900"
                .UserTbar = ""
                .AssignedBranch = CmbBranch.SelectedValue
                .UserMaxAmt = Val(TxtMaxAmt.Text)
                .UserOtherBranch = AreaMapping.ToString

            End With
            M_UsrRegBL.Save_Data(M_UsrRegBO, "NEW")
            BindBranch()
            ShowAlertMessage("Saved Successfully")

            ClearData()

        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAll.CheckedChanged
        Try
            If ChkAll.Checked = True Then

                Dim i As Integer
                For i = 0 To GvBranchDetails.Rows.Count - 1
                    Dim row As GridViewRow = GvBranchDetails.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = True
                Next
            ElseIf ChkAll.Checked = False Then
                For i = 0 To GvBranchDetails.Rows.Count - 1
                    Dim row As GridViewRow = GvBranchDetails.Rows(i)
                    Dim DefBranch As String = row.Cells(1).Text
                    If CmbBranch.SelectedValue <> DefBranch Then
                        Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                        ChkSelect.Checked = False
                    End If
                    
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            DefaultBranch()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbEmployeeName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbEmployeeName.SelectedIndexChanged
        Try

            
            Dim Dt_CustIdDtls As DataTable
            Dt_CustIdDtls = M_CommonBL.GetUserDtls(CmbEmployeeName.SelectedValue)
            
            TxtCustId.Text = Dt_CustIdDtls.Rows(0).Item("Cust_Id").ToString()
            TxtCustAddr.Text = Dt_CustIdDtls.Rows(0).Item("full_address").ToString()

            'If Dt_CustIdDtls.Rows(0).Item("User_Active").ToString() = "Y" Then
            '    CmbUserStatus.SelectedValue = "ACTIVE"
            'Else
            '    CmbUserStatus.SelectedValue = "INACTIVE"
            'End If




        Catch ex As Exception

        End Try
    End Sub
End Class