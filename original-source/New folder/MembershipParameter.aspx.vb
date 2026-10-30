Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class MembershipParameter
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Try

            Dim M_CommonBL As New CommonBL
            M_CommonBL.GetSchemes(CmbSchemeFrom, Session("TransDate"), "SHARE", "SHARE")
            CmbDivPayment.Items.Add("Yes")
            CmbDivPayment.Items.Add("No")

            CmbShareSus.Items.Add("Yes")
            CmbShareSus.Items.Add("No")

            CmbVotingRights.Items.Add("Yes")
            CmbVotingRights.Items.Add("No")

            CmbDistinctive.Items.Add("Yes")
            CmbDistinctive.Items.Add("No")


            CmbLoanEligibility.Items.Add("Yes")
            CmbLoanEligibility.Items.Add("No")

    
        bindgrid()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub bindgrid()
        Try
            Dim dt As New DataTable("Table1")
            'dt.Columns.Add("Shp_Effective_Date")
            'dt.Columns.Add("Shp_Sharevalue")
            'dt.Columns.Add("Shp_MinShrAmt")
            'dt.Columns.Add("Shp_MaxShrAmt")
            'dt.Columns.Add("Shp_DistintiveShare")
            'dt.Columns.Add("Shp_Period")
            'dt.Columns.Add("Shp_DivApplicable")
            'dt.Columns.Add("Shp_VotingRight")
            'dt.Columns.Add("Shp_LoanEligibilty")
            'dt.Columns.Add("Shp_SupAccountNeeded")
            
            Dim Param As New Share_ParameterBL
            dt = Param.Show_Data(CmbSchemeFrom.SelectedValue)

            If dt.Rows.Count > 0 Then
                Session("Param") = dt
                DgvParam.DataSource = dt
                DgvParam.DataBind()
            Else

                dt.Rows.Add("", "", "", "", "", "", "", "", "", "")
                Session("Param") = dt
                DgvParam.DataSource = dt
                DgvParam.DataBind()
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

 

  


    Private Sub Clear()
        Try
            TxtDate.Text = ""
            TxtSharevalue.Text = ""
            TxtMinshare.Text = ""
            TxtMaxshare.Text = ""
            CmbDistinctive.SelectedIndex = 0
            TxtSharePeriod.Text = ""
            CmbDivPayment.SelectedIndex = 0
            CmbVotingRights.SelectedIndex = 0
            CmbLoanEligibility.SelectedIndex = 0
            CmbShareSus.SelectedIndex = 0

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim Param As New Share_ParameterBO
            With Param


                .ShpPeriod = TxtSharePeriod.Text
                '.ShpEntFee = 
                '.ShpMinShrCandidate = 
                '.ShpMaxNoofNom = 
                '.ShpMaxEntFee = 
                '.ShpMaxDivPrd = 
                '.ShpDefaultNoOfShare = 
                .ShpEffectiveDate = TxtDate.Text
                '.MakingTime = 
                '.CheckingTime = 
                .ShpSharevalue = TxtSharevalue.Text
                .ShpMinShrAmt = TxtMinshare.Text
                .ShpMaxShrAmt = TxtMaxshare.Text
                '.ShpDRFAMT = 
                .SchCode = CmbSchemeFrom.SelectedValue
                '.ShpAccNumGenType = 
                .MakerID = Session("LoginUserID").ToString()
                .CheckerID = Session("LoginUserID").ToString()
                '.ShpDivCode = 
                '.ShpDRFCODE = 
                '.ShpPeriodType = 
                .ShpLoanEligibilty = CmbLoanEligibility.SelectedItem.Text
                '.ShpTDSApplicable = 
                .ShpVotingRight = CmbVotingRights.SelectedItem.Text
                .ShpDivApplicable = CmbDivPayment.SelectedItem.Text
                '.ShpDivBasicShare = 
                '.ShpCandidateRight = 
                .ShpDistintiveShare = CmbDivPayment.SelectedItem.Text

                '.ShpCustMultipleShrAllowed = 
                '.ShpResolution = 
                .ShpSupAccountNeeded = CmbShareSus.SelectedItem.Text
                '.ShpNominalShare = 

            End With
            Dim M_Share_ParameterBL As New Share_ParameterBL
            Dim i As Integer
            i = M_Share_ParameterBL.Save_Data(Param, "NEW")
            If i > 0 Then
                ShowAlertMessage("Saved Successfully")
                Clear()
                bindgrid()
            Else
                ShowAlertMessage("Not Saved Successfully")
            End If
            M_Share_ParameterBL = Nothing


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            Clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbSchemeFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchemeFrom.SelectedIndexChanged
        Try
bindgrid()
     Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class