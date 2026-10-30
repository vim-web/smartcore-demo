Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class EmpMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
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
            load_default()
            Session("M_EntryMode") = "NEW"
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Pickup(CmbQualification, 13, 0)
            M_CommonBL.Fill_Pickup(CmbDesign, 85, 0)
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbStafftype.Items.Add("Normal")
            CmbStafftype.Items.Add("Directors")
            'CmbStafftype.Items.Add("Contigent")
            M_CommonBL = Nothing
            TxtDoj.Attributes.Add("readonly", "readonly")
            TxtDob.Attributes.Add("readonly", "readonly")
            TxtFixDate.Attributes.Add("readonly", "readonly")
            TxtAccName.Attributes.Add("readonly", "readonly")
            CmbStatus.Items.Add("Active")
            CmbStatus.Items.Add("In Active")
            CmbStatus.SelectedIndex = 0
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "EMPLOYEE"
            ImgSLAcc.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

            
        End If
        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "EMPLOYEE" Then

                TxtID.Text = Session("SearchResult")
                TxtID_TextChanged(Me.TxtID, System.EventArgs.Empty)
                TxtID.Focus()
                Session("SearchResult") = ""
            Else
                TxtSalAcc.Text = Session("SearchResult")
                TxtSalAcc_TextChanged(Me.TxtID, System.EventArgs.Empty)
                TxtSalAcc.Focus()
                Session("SearchResult") = ""
            End If
        End If




        'If Session("SearchResult") <> "" Then
        '    If Session("SearchType") = "ACCOUNTNO" Then
        '        TxtSalAcc.Text = Session("SearchResult")
        '        TxtSalAcc_TextChanged(Me.TxtSalAcc, System.EventArgs.Empty)
        '    End If
        '    Session("SearchResult") = ""
        'End If


    End Sub



    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Private Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        If Validate_Control() = False Then Exit Sub
        Dim M_Employee As New SMARTCORE_BO.EmployeeBO
        With M_Employee
            .EmpDoj = TxtDoj.Text
            .EmpDob = TxtDob.Text
            .EmpFixDate = TxtFixDate.Text
            .EmpAge = Val(TxtAge.Text)
            .EmpQualification = CmbQualification.SelectedValue
            .EmpDesignation = CmbDesign.SelectedValue
            .EmpSalAcc = HdnAccid.Value
            .EmpStatus = CmbStatus.SelectedIndex
            .EmpFixAmt = Val(TxtFixAmt.Text)
            .EmpID = TxtID.Text
            .EmpName = TxtName.Text
            .EmpRelName = TxtRelName.Text
            .EmpAddress1 = TxtAdd1.Text
            .EmpAddress2 = TxtAdd2.Text
            .EmpAddress3 = TxtAdd3.Text
            .EmpPhoneNo = TxtPhoneNo.Text
            .EmpMobileNo = TxtMblNo.Text
            .EmpBranch = CmbBranch.SelectedValue
            .EmpStaffType = 0
            .EmpCategory = IIf(CmbStafftype.SelectedIndex = 0, "N", "D")


        End With

        Try
            Dim M_PayrollBL As New PayrollBL
            M_PayrollBL.Save_Data(M_Employee, Session("M_EntryMode"))
            Clear_Controls()
            M_PayrollBL = Nothing
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Clear_Controls()
        TxtDoj.Text = Session("TransDate")
        TxtDob.Text = Session("TransDate")
        TxtFixDate.Text = Session("TransDate")
        TxtAge.Text = ""
        TxtSalAcc.Text = ""
        TxtFixAmt.Text = ""
        TxtID.Text = ""
        TxtName.Text = ""
        TxtRelName.Text = ""
        TxtAdd1.Text = ""
        TxtAdd2.Text = ""
        TxtAdd3.Text = ""
        TxtAccName.Text = ""
        TxtPhoneNo.Text = ""
        TxtMblNo.Text = ""
        HdnAccid.Value = 0
    End Sub

    Protected Sub TxtID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtID.TextChanged
        If TxtID.Text.Length = 0 Then
            Call Clear_Controls()
            Session("M_EntryMode") = "NEW"
        Else
            Try
                Dim M_Employee As New SMARTCORE_BO.EmployeeBO
                Dim M_PayrollBL As New PayrollBL
                Dim M_CommonBL As New CommonBL
                M_Employee = M_PayrollBL.Locate_Data(TxtID.Text)
                Locate_Data(M_Employee)
                M_PayrollBL = Nothing

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub Locate_Data(ByVal M_Employee As SMARTCORE_BO.EmployeeBO)
        If M_Employee.EmpID = 0 Then
            Session("M_EntryMode") = "NEW"
            Exit Sub
        End If
        With M_Employee
            TxtDoj.Text = .EmpDoj
            TxtDob.Text = .EmpDob
            TxtFixDate.Text = .EmpFixDate
            TxtAge.Text = .EmpAge
            CmbQualification.SelectedValue = .EmpQualification
            CmbDesign.SelectedValue = .EmpDesignation
            TxtSalAcc.Text = .EmpSalAcc
            CmbStatus.SelectedIndex = .EmpStatus
            TxtFixAmt.Text = .EmpFixAmt
            TxtID.Text = .EmpID
            TxtName.Text = .EmpName
            TxtRelName.Text = .EmpRelName
            TxtAdd1.Text = .EmpAddress1
            TxtAdd2.Text = .EmpAddress2
            TxtAdd3.Text = .EmpAddress3
            TxtPhoneNo.Text = .EmpPhoneNo
            TxtMblNo.Text = .EmpMobileNo
            CmbBranch.SelectedValue = .EmpBranch
            CmbStafftype.SelectedIndex = IIf(.EmpCategory = "N", 0, 1)
            Dim M_CommonBL As New CommonBL
            Dim M_SalAcc As String = ""
            TxtAccName.Text = M_CommonBL.GetAccountCustName(.EmpSalAcc, M_SalAcc)
            TxtSalAcc.Text = M_SalAcc
            TxtSalAcc_TextChanged(Me.TxtID, System.EventArgs.Empty)

        End With
        Session("M_EntryMode") = "VIEW"
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Function Validate_Control() As Boolean
        If Len(Trim(TxtID.Text)) = 0 Then
            TxtID.Focus()
            ShowAlertMessage("INVALID EMPLOYEE ID ENTERED")
            Return False
        End If

        If Len(Trim(TxtName.Text)) = 0 Then
            TxtName.Focus()
            ShowAlertMessage("INVALID EMPLOYEE NAME ENTERED")
            Return False
        End If
        If Len(Trim(TxtSalAcc.Text)) = 0 Then
            TxtSalAcc.Focus()
            ShowAlertMessage("INVALID ACC NO ENTERED")
            Return False
        End If
        If CommonClass.ChkDate(TxtDob.Text) = False Then
            TxtDob.Focus()
            ShowAlertMessage("INVALID DATE OF BIRTH ENTERED")
            Return False
        End If
        If CommonClass.ChkDate(TxtDoj.Text) = False Then
            TxtDoj.Focus()
            ShowAlertMessage("INVALID DATE OF JOINING ENTERED")
            Return False
        End If

        If CommonClass.ChkDate(TxtFixDate.Text) = False Then
            TxtFixDate.Focus()
            ShowAlertMessage("INVALID FIXATION DATE ENTERED")
            Return False
        End If
        Return True
    End Function

    Protected Sub TxtDoj_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDoj.TextChanged

    End Sub

    Protected Sub TxtSalAcc_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSalAcc.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim M_Accid As Integer
        TxtAccName.Text = M_CommonBL.GetAccountCustName(TxtSalAcc.Text, M_Accid)
        HdnAccid.Value = M_Accid
        M_CommonBL = Nothing
    End Sub
    Public Sub load_default()
        Try
            Dim M_datatabl As New DataTable
            Dim M_CommonBL As New CommonBL
            Dim ImgTrAccNo
            M_datatabl = M_CommonBL.Get_EmpCode()
            TxtID.Text = M_datatabl.Rows(0).Item("Emp_ID")


        Catch ex As Exception

        End Try
    End Sub

End Class