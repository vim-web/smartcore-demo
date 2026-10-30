Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO.MemoryStream
Imports System.IO

Public Class VerifyBnkDtls
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 2021
    Shared Obj_UserRights As Object
    Shared FileName As String
    Shared path As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                'M_Comm = Nothing
                'If Obj_UserRights.View_Right = False Then
                '    Response.Redirect("~/UnauthorisedAcces.ASPX")
                'End If

                Load_DefaultValues()

                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Session("SearchType") = "ACCOUNTNO"
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCOUNTNO" Then
                    TxtAccNo.Text = Session("SearchResult")
                    TxtAccNo_TextChanged(Me.TxtAccNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""
            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            Dim dt As New DataTable
            M_CommonBL.Fill_Pickup(CmbRelType, 9, 0)
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("DEPOSIT','LOAN"), "")
            CmbScheme.Items.Insert(0, "Select")


            ' Dgvimg.DataSource = ""
            'Dgvimg.DataBind()
            Session("Tran_Dt") = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub ClearAll()

        CmbScheme.SelectedIndex = 0
        CmbBranch.SelectedValue = Session("DefaultBrCode")

        TxtAccNo.Text = ""
        TxtBnkAccNo.Text = ""
        TxtIfscCode.Text = ""
        Txt_CustId.Text = ""
        Txt_CustName.Text = ""
        Txt_CustAddress.Text = ""
        Txt_BAnkName.Text = ""
        Txt_BAnkBranch.Text = ""
        Txt_BenfName.Text = ""
        ' Dgvimg.DataSource = ""
        '  Dgvimg.DataBind()
        Session("Tran_Dt") = Nothing

    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged

        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable
            Dim Dt_AcDtls1 As DataTable

            If TxtAccNo.Text.Trim.Length <= 7 Then
                TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                Txt_CustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                'CmbSection.Items.FindByText("LOAN").Selected = True

                CmbScheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                CmbBranch.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()

                Dt_AcDtls1 = M_CommonBL.Get_CustbankNo(TxtAccNo.Text)
                If Dt_AcDtls1.Rows.Count > 0 Then
                    TxtBnkAccNo.Text = Dt_AcDtls1.Rows(0)("Bnk_AccNo").ToString()
                    TxtIfscCode.Text = Dt_AcDtls1.Rows(0)("IFSCCode").ToString()
                    Txt_BAnkName.Text = Dt_AcDtls1.Rows(0)("Bnk_Name").ToString()
                    Txt_BAnkBranch.Text = Dt_AcDtls1.Rows(0)("Bnk_Branch").ToString()
                    Txt_BenfName.Text = Dt_AcDtls1.Rows(0)("Benificary_Name").ToString()
                    CmbRelType.SelectedValue = Dt_AcDtls1.Rows(0)("Benificary_Relation").ToString()


                    ''TxtRemarks.Enabled = True
                Else
                    TxtBnkAccNo.Text = ""
                    TxtIfscCode.Text = ""
                    Txt_BAnkName.Text = ""
                    Txt_BAnkBranch.Text = ""
                    Txt_BenfName.Text = ""
                End If
            Else
                ShowAlertMessage("Invalid Account Number!!!")
                TxtAccNo.Text = ""
            End If
            M_CommonBL = Nothing

            'Dgvimg.DataSource = ""
            'Dgvimg.DataBind()
            Session("Tran_Dt") = Nothing


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

    Private Function Validate_Control() As Boolean

        If CmbScheme.SelectedIndex = 0 Then
            CmbScheme.Focus()
            ShowAlertMessage("Please select the scheme")
            Return False
        End If


        If TxtAccNo.Text.Length = 0 Then
            TxtAccNo.Focus()
            ShowAlertMessage("Please Enter the Account No")
            Return False
        End If


        If TxtBnkAccNo.Text.Length = 0 Then
            TxtBnkAccNo.Focus()
            ShowAlertMessage("Please Enter the Bank Account No")
            Return False
        End If


        If TxtIfscCode.Text.Length = 0 Then
            TxtIfscCode.Focus()
            ShowAlertMessage("Please Enter the IFSC Code")
            Return False
        End If

        Return True
    End Function


    Protected Sub CmdClose0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose0.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        ClearAll()
    End Sub

    Protected Sub CmdSave0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave0.Click
        Try

            If Validate_Control() = False Then
                Exit Sub
            End If

            Dim M_CommnBL As New CommonBL
            Dim DtTab As DataTable


            DtTab = M_CommnBL.SaveBank_Details(CmbBranch.SelectedValue, CmbScheme.SelectedValue, Txt_CustId.Text, TxtAccNo.Text, TxtBnkAccNo.Text, TxtIfscCode.Text, Session("TransDate"), Session("UserID"), Txt_BAnkName.Text, Txt_BAnkBranch.Text, CmbRelType.SelectedValue, Txt_BenfName.Text)

            ShowAlertMessage("Saved Successfully")
            ClearAll()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class
