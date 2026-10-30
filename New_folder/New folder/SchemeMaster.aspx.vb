Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class SchemeMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object
    Shared AcHeadDt As DataTable

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

                Session("SearchType") = "Scheme"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

                Session("M_EntryMode") = "NEW"
                TxtSchCode.Text = "NEW"
                Load_DefaultValues()
            End If
            If Session("SearchResult") <> "" Then
                TxtSchCode.Text = Session("SearchResult")
                TxtSchCode_TextChanged(Me.TxtSchCode, System.EventArgs.Empty)
                TxtSchCode.Focus()
                Session("SearchResult") = ""
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            CmbSection.Items.Add("SHARE")
            CmbSection.Items.Add("DEPOSIT")
            CmbSection.Items.Add("LOAN")
            CmbSection.Items.Add("SUSPENCE")
            CmbSection.Items.Add("LOCKER")
            CmbSection.Items.Add("BANK")

            CmbActive.Items.Add("Yes")
            CmbActive.Items.Add("No")

            CmbGentype.Items.Add("Separate Number")
            CmbGentype.Items.Add("Linked to Scheme")

            Dim M_CommonBL As New CommonBL
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"))
            Load_Module()
            AcHeadDt = DirectCast(Cache("Achead"), DataTable)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_Module()
        Try
            CmbModule.Items.Clear()
            If CmbSection.SelectedItem.Text = "SHARE" Then
                CmbModule.Items.Add("SHARE")

            ElseIf CmbSection.SelectedItem.Text = "DEPOSIT" Then

                CmbModule.Items.Add("SB")
                CmbModule.Items.Add("CA")
                CmbModule.Items.Add("RD")
                CmbModule.Items.Add("DD")
                CmbModule.Items.Add("FD")

            ElseIf CmbSection.SelectedItem.Text = "LOAN" Then

                CmbModule.Items.Add("GEN.LOAN")
                CmbModule.Items.Add("GOLDLOAN")
                CmbModule.Items.Add("DEP.LOAN")
                CmbModule.Items.Add("OD")

            ElseIf CmbSection.SelectedItem.Text = "SUSPENCE" Then

                CmbModule.Items.Add("SA")
                CmbModule.Items.Add("SL")
            ElseIf CmbSection.SelectedItem.Text = "BANK" Then

                CmbModule.Items.Add("BANK")


            ElseIf CmbSection.SelectedItem.Text = "LOCKER" Then

                CmbModule.Items.Add("LOCKER")



            End If



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Type()
        Try
            Try
                If CmbGentype.SelectedItem.Text = "Separate Number" Then
                    TrScheme.Visible = False
                Else
                    TrScheme.Visible = True
                End If
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
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
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
   Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Try
            If True Then
                Dim Expression As String = "Acc_Name LIKE '" & prefixText & "%'"
                Dim SortingOrder As String = "Acc_Name ASC"
                Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression, SortingOrder)
                Dim CompletionSet = New List(Of String)()
                For Each row As DataRow In drFiltered
                    CompletionSet.Add(row("Acc_Name").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("Acc_Code").ToString())
                Next
                Return CompletionSet.ToArray()
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Function

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Try
            Load_Module()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbGentype_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbGentype.SelectedIndexChanged
        Try
            Type()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            Dim M_SchemeMaster As New SMARTCORE_BO.SchemeMasterBO            With M_SchemeMaster

                '.SchSanctionby = 
                '.SchVouID = 
                .SchImplimentDate = TxtDate.Text
                '.SchDeActiveDate = 
                '.MakingTime = 
                '.CheckingTime = 
                .SchCode = TxtSchCode.Text
                .SchName = TxtSchName.Text
                .SchShortName = TxtShortName.Text
                .AccCode = TxtAccCode.Text
                '.SchDeActiveReason = 
                .SchSectionName = CmbSection.SelectedItem.Text
                .CheckerID = Session("LoginUserID").ToString()
                '.SchBackEntryBranches = 
                '.SCHAGENTCOMMSSION = 
                '.SchType4 = 
                '.SchType5 = 
                '.SchType6 = 
                If CmbGentype.SelectedItem.Text = "Separate Number" Then
                    .SchGenNoScheme = TxtSchCode.Text
                Else
                    .SchGenNoScheme = CmbScheme.SelectedValue
                End If

                '.SchGenNo = 
                .MakerID = Session("LoginUserID").ToString()
                .SchModuleCatg = CmbModule.SelectedItem.Text
                '.SchTermType = 
                '.SchTransferScheme = 
                '.SchType1 = 
                '.SchType2 = 
                '.SchType3 = 
                '.SchImplimented = 
                .SchActive = CmbActive.SelectedItem.Text
                '.SchBackEntry = 
                '.SchMasterAthorisation = 
                '.SchTranAthorisation = 
                '.SchCashReceiptAllowed = 
                '.SchTrasfReceiptAllowed = 
                '.SchClearingfReceiptAllowed = 
                '.SchATMFacility = 
                .SchGenNoType = CmbGentype.SelectedItem.Text
                '.BranchWiseNo = 
                '.NextNOYearWise = 

            End With            Dim M_SchCode As Integer            Try                Dim M_SchemeMasterBL As New SchemeMasterBL                M_SchCode = M_SchemeMasterBL.Save_Data(M_SchemeMaster, Session("M_EntryMode"))                If M_SchCode > 0 Then                    If Session("M_EntryMode") = "NEW" Then                        ShowAlertMessage("Saved Successfully")
                    Else
                        ShowAlertMessage("Updated Successfully")
                    End If
                    Clear()
                Else
                    ShowAlertMessage("Not Saved Successfully")

                End If                M_SchemeMasterBL = Nothing            Catch ex As Exception                ShowAlertMessage(ex.Message)            End Try

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

    Protected Sub TxtAccCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccCode.TextChanged
        Try
            Dim Expression As String = "Acc_Code='" & TxtAccCode.Text & "'"
            Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression)

            If drFiltered.Count > 0 Then
                TxtAccName.Text = drFiltered(0)("Acc_Name")
            Else
                TxtAccName.Text = ""
                ShowAlertMessage("Invalid A/c Head")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            TxtAccName.Text = ""
            TxtAccCode.Text = ""
            TxtDate.Text = ""
            TxtSchCode.Text = ""
            TxtSchName.Text = ""
            TxtShortName.Text = ""
            CmbScheme.SelectedIndex = 0
            CmbSection.SelectedIndex = 0
            CmbGentype.SelectedIndex = 0
            CmbModule.SelectedIndex = 0
            CmbActive.SelectedIndex = 0

            Load_Module()
            Type()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        Try
            Dim M_Common As New CommonBL
            Dim dt As New DataTable
            dt = M_Common.Get_SchemeDtls(TxtSchCode.Text)
            If dt.Rows.Count > 0 Then
                Dim M_Get As New SchemeMasterBL
                Dim M_Scheme As New SMARTCORE_BO.SchemeMasterBO
                M_Scheme = M_Get.Locate_Data(TxtSchCode.Text)
                With M_Scheme
                    TxtSchName.Text = .SchName
                    TxtShortName.Text = .SchShortName
                    TxtAccCode.Text = .AccCode
                    TxtDate.Text = Format(.SchImplimentDate, "dd/MM/yyyy")
                    CmbSection.SelectedItem.Text = .SchSectionName
                    CmbModule.SelectedItem.Text = .SchModuleCatg
                    If .SchActive = "Y" Then
                        CmbActive.SelectedIndex = 0
                    Else
                        CmbActive.SelectedIndex = 1
                    End If
                    If .SchGenNoType = "S" Then
                        CmbGentype.SelectedIndex = 0
                        TrScheme.Visible = False
                    Else
                        CmbGentype.SelectedIndex = 1
                        TrScheme.Visible = True
                        CmbScheme.SelectedValue = .SchGenNoScheme

                    End If
                    Call TxtAccCode_TextChanged(sender, e)

                End With
                Session("M_EntryMode") = "UPDATE"

            Else
                Dim str As String
                str = TxtSchCode.Text
                Clear()
                TxtSchCode.Text = str
                TxtSchCode.Focus()
                Session("M_EntryMode") = "NEW"
            End If
           


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class