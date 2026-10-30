Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL

Partial Public Class UserEmployee
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1148
    Shared Obj_UserRights As Object
    Private Shared M_DBConn As New Connection



    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Designation(CmbDesignation)
        M_CommonBL.Fill_Pickup(CmbEmpType, 69, 0)
        M_CommonBL.Fill_User("")
        M_CommonBL = Nothing
        Dim M_Dt As New DataTable
        Dim M_sql As String = "select MAX(cast(Emp_ID as int))+1 AS Emp_Id from User_Employee_M_Tbl where Emp_ID<>'E001'"
        M_DBConn.ExecuteDataTable(M_Dt, M_sql, False)
        TxtEmpID.Text = M_Dt.Rows(0)("Emp_Id")


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
            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

        End If

        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "CUSTOMER" Then
                TxtCustId.Text = Session("SearchResult")
                TxtCustId_TextChanged(Me.TxtCustId, System.EventArgs.Empty)
                TxtCustId.Focus()

            End If
            Session("SearchResult") = ""
        End If

    End Sub
    Private Sub Load_CustomerData()
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(TxtCustId.Text), M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next
        If M_CustMaster.CustId = 0 Then
            TxtCustID.Text = "NEW"
            Exit Sub
        End If

        TxtAddress.Text = M_PresAddress.AddAddress1 + M_PresAddress.AddAddress2 + M_PresAddress.AddAddress3 + M_PresAddress.AddPinCode
        TxtName.Text = M_CustMaster.CustName

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtCustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustId.TextChanged
        Load_CustomerData()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        'Add Right
        If Obj_UserRights.Add_Right = False Then
            ShowAlertMessage("You Have No Permission To Add !!!")
            Exit Sub
        End If



        Dim M_UserEmpBL As New User_EmployeeBL
        Dim M_Dt As New DataTable
        M_Dt = M_UserEmpBL.Locate_Data(TxtEmpID.Text)
        If M_Dt.Rows.Count > 0 Then
            ShowAlertMessage(" This Employee ID is Already Exist.Please enter Another Employee ID.")
            ClearData()
            Exit Sub
        End If


        Dim M_UsrEmp As New User_EmployeeBO
        Dim M_UsrBl As New User_EmployeeBL

        Try
            With M_UsrEmp
                .EmpID = TxtEmpID.Text
                .EmpDesigCode = CmbDesignation.SelectedValue
                .EmpTypeCode = CmbEmpType.SelectedValue
                .BrCode = Session("DefaultBrCode")
                .EmpName = TxtName.Text
                .EmpAddress = TxtAddress.Text
                .EmpPhone = ""
                .CustId = TxtCustId.Text
                .EmpEffectFrom = TxtDOJ.Text
                .EmpRetiredDate = TxtDOR.Text
            End With
            M_UsrBl.Save_Data(M_UsrEmp, "NEW")
            ShowAlertMessage("Saved Successfully")
            ClearData()

        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        TxtAddress.Text = ""
        TxtCustId.Text = ""
        TxtName.Text = ""
        TxtDOJ.Text = Session("TransDate")
        TxtDOR.Text = Session("TransDate")
        TxtEmpID.Text = ""
        TxtEmpID.Focus()


    End Sub

    Protected Sub TxtEmpID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtEmpID.TextChanged
        Try


            Dim M_UserEmpBL As New User_EmployeeBL
            Dim M_Dt As New DataTable
            M_Dt = M_UserEmpBL.Locate_Data(TxtEmpID.Text)
            If M_Dt.Rows.Count > 0 Then
                TxtDOJ.Text = M_Dt.Rows(0).Item("Emp_EffectFrom")
                TxtDOR.Text = M_Dt.Rows(0).Item("Emp_RetiredDate")
                TxtCustId.Text = M_Dt.Rows(0).Item("Cust_Id")
                CmbEmpType.SelectedValue = M_Dt.Rows(0).Item("Emp_Type_Code")
                CmbDesignation.SelectedValue = M_Dt.Rows(0).Item("Emp_Desig_Code")
                Load_CustomerData()
                HdnCustID.Value = 1


            Else
                TxtAddress.Text = ""
                TxtCustId.Text = ""
                TxtName.Text = ""
                TxtDOJ.Text = Session("TransDate")
                TxtDOR.Text = Session("TransDate")
                HdnCustID.Value = 0

            End If
            M_UserEmpBL = Nothing
        Catch ex As Exception

        End Try

    End Sub
End Class