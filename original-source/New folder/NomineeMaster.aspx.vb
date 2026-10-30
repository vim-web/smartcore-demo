Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports System.Data
Imports SMARTCORE_Common.CommonClass
Partial Public Class NomineeMaster


    Inherits System.Web.UI.Page
    Public NomineeTbl As New DataTable("Table")
    Public NomineeGrid As New DataGrid
    Shared PickupDt As DataTable
    Shared M_AccID As Integer
    Shared M_AccNo As String
    Dim FormID As Integer = 1110


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Load_DefaultValues()

        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(DrpBranch)
        M_CommonBL.GetSchemes(DrpScheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(Ddl_Relation, 25, 0)
        M_CommonBL.Fill_Pickup(Ddl_GuardianRelation, 25, 0)
        M_CommonBL.Fill_Pickup(Ddl_Occupation, 4, 0)
        M_CommonBL = Nothing
        'Ddl_Minor.Items.Add("Yes")
        'Ddl_Minor.Items.Add("No")
        If Session("AcOpenBranch").ToString.Length > 0 And Session("AcOpenScheme").ToString.Length > 0 Then
            DrpBranch.SelectedValue = Session("AcOpenBranch")
            DrpScheme.SelectedValue = Session("AcOpenScheme")
        End If
        If Session("M_EntryMode") = "NEW" Then
            TxtAccountNo.Text = 0
            TxtNomineeID.Text = 0
            TxtAccountNo.Enabled = False
            TxtNomineeID.Enabled = False
        End If
        TxtName.Focus()
    End Sub




    Private Sub Locate_Data(ByVal M_Nominee As SMARTCORE_BO.NomineeBO)

        'If M_Nominee.NmNomNo = 0 Then
        '    ' LblEntryMode.Text = "-- NEW --"
        '    Exit Sub
        'End If
        'With M_Nominee



        '    =.NmNomNo
        '    =.BrCode
        '    =.SchCode
        '    =.AccNo
        '    =.NmRelation
        '    =.NmOccupation
        '    =.NmGrelation
        '    =.LogId
        '    =.slno
        '    =.NmDOB
        '    =.NmEffectDate
        '    =.NmAge
        '    =.NmShrPercent
        '    =.NmName
        '    =.NmPhone
        '    =.NmAddress
        '    =.NmGName
        '    =.NmGAddress
        '    =.NmNarration
        '    =.NmGender
        '    =.NmMinor

        'End With
        ''LblEntryMode.Text = "-- VIEW --"
    End Sub




    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        ' ''Dim M_Nominee As New SMARTCORE_BO.NomineeBO
        ' ''Dim M_NmNomNo As Integer

        ' ''With M_Nominee


        ' ''    .NmNomNo = M_NmNomNo
        ' ''    .BrCode = DrpBranch.SelectedValue
        ' ''    .SchCode = DrpScheme.SelectedValue
        ' ''    .AccNo = Val(TxtAccountNo.Text)
        ' ''    .NmRelation = Ddl_Relation.SelectedValue
        ' ''    .NmOccupation = Ddl_Occupation.SelectedValue
        ' ''    .NmGrelation = Ddl_GuardianRelation.SelectedValue
        ' ''    .LogId = 1
        ' ''    .NmDOB = TxtDateOfBirth.Text
        ' ''    .NmEffectDate = Session("TransDate")
        ' ''    .NmAge = Val(TxtAge.Text)
        ' ''    .NmShrPercent = 0
        ' ''    .NmName = TxtName.Text
        ' ''    .NmPhone = TxtPhone.Text
        ' ''    .NmAddress = TxtAddress.Text
        ' ''    .NmGName = TxtGuardian.Text
        ' ''    .NmGAddress = TxtGaurdianAddress.Text
        ' ''    .NmNarration = ""
        ' ''    .NmGender = "M"
        ' ''    .NmMinor = Ddl_Minor.SelectedValue
        ' ''    .slno = 1

        ' ''End With

        ' ''Try
        ' ''    Set_Grid()
        ' ''    Dim Trandt As DataTable = DirectCast(Session("NomTbl"), DataTable)
        ' ''    Dim M_TranSw As New IO.StringWriter
        ' ''    Trandt.WriteXml(M_TranSw)


        ' ''    Dim M_NomineeBL As New NomineeBL

        ' ''    Session("M_EntryMode") = "NEW"
        ' ''    M_NmNomNo = M_NomineeBL.Save_Data(M_Nominee, M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), "", Session("M_EntryMode"))
        ' ''    ShowAlertMessage(" NOMINEE NO : " & M_NmNomNo)
        ' ''    M_NomineeBL = Nothing

        ' ''    ' ''Dim Trandt As DataTable = DirectCast(Session("TransTbl"), DataTable)
        ' ''    ' ''Dim M_TranSw As New IO.StringWriter
        ' ''    ' ''Trandt.WriteXml(M_TranSw)

        ' ''    ' ''Dim M_LoanMasterBL As New LoanMasterBL
        ' ''    ' ''Dim M_Dt As DataTable
        ' ''    ' ''Session("M_EntryMode") = "NEW"
        ' ''    ' ''M_Dt = M_LoanMasterBL.Save_Data(M_LoanMaster, M_AccHolderSw.ToString.Replace("DocumentElement", "NewDataSet"), M_OrnSw.ToString.Replace("DocumentElement", "NewDataSet"), M_TranSw.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))
        ' ''    ' ''ShowAlertMessage(" LOAN NO : " & M_Dt.Rows(0)("Acc_No").ToString)
        ' ''    ' ''M_LoanMasterBL = Nothing
        ' ''    '' ''Response.Redirect("~/AccOpen.aspx")





        ' ''Catch ex As Exception

        ' ''End Try

        Set_Grid()

        ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)


    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
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
            NomineeTbl.Rows.Add("", DrpBranch.SelectedValue, DrpScheme.SelectedValue, Val(TxtAccountNo.Text), TxtName.Text, "M", Ddl_Relation.SelectedValue, TxtPhone.Text, TxtAddress.Text, Ddl_Occupation.SelectedValue, Convert.ToDateTime(TxtDateOfBirth.Text), Val(TxtAge.Text), "N", TxtGuardian.Text, Ddl_GuardianRelation.SelectedValue, TxtGaurdianAddress.Text, "Nil", Session("TransDate"), 0, 0)
            Session("NomTbl") = NomineeTbl
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_Minor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Minor.SelectedIndexChanged
        If Ddl_Minor.SelectedItem.ToString = "YES" Then
            TxtGuardian.Enabled = True
            TxtGaurdianAddress.Enabled = True
            TxtGuardian.Text = ""
            TxtGaurdianAddress.Text = ""
            Ddl_GuardianRelation.Enabled = True
            TxtGuardian.Focus()
        Else
            TxtGuardian.Enabled = False
            TxtGaurdianAddress.Enabled = False
            TxtGuardian.Text = "Nil"
            TxtGaurdianAddress.Text = "Nil"
            Ddl_GuardianRelation.Enabled = False
        End If
    End Sub

    Protected Sub DrpScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpScheme.SelectedIndexChanged

    End Sub


End Class