Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data

Partial Public Class NamineeChange
    Inherits System.Web.UI.Page

    Public NomineeTbl As New DataTable("Table")
    Public NomineeGrid As New DataGrid
    Shared PickupDt As DataTable
    Shared Dt_AcDtls As DataTable


    Shared M_AccID As Integer
    Shared M_AccNo As String
    Dim FormID As Integer = 1109
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
        End If
    End Sub



    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(CmbNomRel, 25, 0)
        M_CommonBL.Fill_Pickup(Ddl_GuardianRelation, 25, 0)
        ' M_CommonBL.Fill_Pickup(Ddl_Occupation, 4, 0)
        M_CommonBL = Nothing
        'Ddl_Minor.Items.Add("Yes")
        'Ddl_Minor.Items.Add("No")
        'If Session("AcOpenBranch").ToString.Length > 0 And Session("AcOpenScheme").ToString.Length > 0 Then
        '    'CmbScheme.SelectedValue = Session("AcOpenBranch")
        '    'CmbScheme.SelectedValue = Session("AcOpenScheme")
        'End If
        'If Session("M_EntryMode") = "NEW" Then
        '    TxtAccountNo.Text = 0
        '    TxtNomineeID.Text = 0
        '    TxtAccountNo.Enabled = False
        '    TxtNomineeID.Enabled = False
        'End If
        CmbBranch.SelectedValue = Session("DefaultBrCode")
        Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        TxtShortCode.Focus()
    End Sub
    Private Sub Set_Grid()
        Try
            NomineeTbl.Columns.Add("Nm_Nom_No", GetType(String))
            NomineeTbl.Columns.Add("Br_Code", GetType(String))
            NomineeTbl.Columns.Add("Sch_Code", GetType(String))
            NomineeTbl.Columns.Add("Acc_No", GetType(String))
            NomineeTbl.Columns.Add("Nm_Name", GetType(String))
            NomineeTbl.Columns.Add("Nm_Gender", GetType(String))
            NomineeTbl.Columns.Add("Nm_Relation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Phone", GetType(String))
            NomineeTbl.Columns.Add("Nm_Address", GetType(String))
            NomineeTbl.Columns.Add("Nm_Occupation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_DOB", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Age", GetType(Double))
            NomineeTbl.Columns.Add("Nm_Minor", GetType(String))
            NomineeTbl.Columns.Add("Nm_GName", GetType(String))
            NomineeTbl.Columns.Add("Nm_Grelation", GetType(Double))
            NomineeTbl.Columns.Add("Nm_GAddress", GetType(String))
            NomineeTbl.Columns.Add("Nm_Narration", GetType(String))
            NomineeTbl.Columns.Add("Nm_EffectDate", GetType(Date))
            NomineeTbl.Columns.Add("Nm_Shr_Percent", GetType(Double))
            NomineeTbl.Columns.Add("Log_Id", GetType(Double))
            NomineeTbl.Rows.Add("", CmbBranch.SelectedValue, CmbScheme.SelectedValue, Val(TxtAccNo.Text), Txt_NomName.Text, "M", CmbNomRel.SelectedValue, Txt_NomPh.Text, Txt_NomAdd.Text, "0", Convert.ToDateTime("01/01/1900"), Val(TxtAge.Text), "N", TxtGuardian.Text, Ddl_GuardianRelation.SelectedValue, TxtGaurdianAddress.Text, "Nil", Session("TransDate"), 0, 0)
            Session("NomTbl") = NomineeTbl
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub



    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try


            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtAccNo.Text
            Dim Dt_AcNomineeDtls As DataTable
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then




                Dim M_NomineeBL As New NomineeBL
                Dt_AcNomineeDtls = M_NomineeBL.GetNomineeDtls(Dt_AcDtls.Rows(0)("Acc_ID").ToString())
                HdnAccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()


                DgvCustomer.DataSource = Dt_AcNomineeDtls

                DgvCustomer.DataBind()

                'TxtCustID.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                'Session("CustomerId") = TxtCustID.Text
                'TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                'TxtMblNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                'TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                'Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                'Dim ShadowBal As Double = Dt_AcDtls.Rows(0)("Shadow_Bal").ToString()
                DivGridNominee.Visible = True
                DivNewNominee.Visible = True
                NomDiv.Visible = True
            Else

                ShowAlertMessage("INVALID ACCOUNT NO")
            End If

            M_CommonBL = Nothing
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub Ddl_Minor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Minor.SelectedIndexChanged
    '    If Ddl_Minor.SelectedItem.ToString = "YES" Then
    '        TxtGuardian.Enabled = True
    '        TxtGaurdianAddress.Enabled = True
    '        TxtGuardian.Text = ""
    '        TxtGaurdianAddress.Text = ""
    '        Ddl_GuardianRelation.Enabled = True
    '        TxtGuardian.Focus()
    '    Else
    '        TxtGuardian.Enabled = False
    '        TxtGaurdianAddress.Enabled = False
    '        TxtGuardian.Text = "Nil"
    '        TxtGaurdianAddress.Text = "Nil"
    '        Ddl_GuardianRelation.Enabled = False
    '    End If
    'End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try




            Set_Grid()

            Dim M_NomineeSw As New IO.StringWriter
            Dim NomineeDt As DataTable = DirectCast(Session("NomTbl"), DataTable)
            NomineeDt.WriteXml(M_NomineeSw)
            Dim M_NomineeBL As New NomineeBL

            Session("M_EntryMode") = "NEW"
            M_NomineeBL.Save_Nominee(M_NomineeSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"), CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, HdnAccID.Value, Session("UserID"))

            Session("NomTbl") = Nothing


            ShowAlertMessage("Nominee Changed Successfully")
            ' Response.Redirect("~/NamineeChange.aspx")
            Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)

            Call ClearNominee()


        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
        Call TxtAccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
    End Sub

    Protected Sub DgvCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DgvCustomer.SelectedIndexChanged

    End Sub

    Private Sub ClearNominee()
        Txt_NomName.Text = ""
        Txt_NomPh.Text = ""
        Txt_NomAdd.Text = ""
        DivNewNominee.Visible = False
        NomDiv.Visible = False
    End Sub


    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3)
    End Sub
End Class