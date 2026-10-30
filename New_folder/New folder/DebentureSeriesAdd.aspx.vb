Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Public Class DebentureSeriesAdd
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1153
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

                Dim M_CommonBL As New CommonBL
                M_CommonBL.GetSchemes(CmbSch, Session("TransDate"), "DEPOSIT")
                TxtEffdate.Text = Session("TransDate")

                TxtSeriesCode.Text = "NEW"
                Session("SearchType") = "DEBENTURESERIES"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

            End If

            If Session("SearchResult") <> "" Then
                TxtSeriesCode.Text = Session("SearchResult")
                TxtSeriesCode_TextChanged(Me.TxtSeriesCode, System.EventArgs.Empty)
                TxtSeries.Focus()
                Session("SearchResult") = ""
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

    Protected Sub CmdClear0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Private Sub clear()
        Try
            TxtMaxNo.Text = ""
            TxtSeries.Text = ""
            TxtEffdate.Text = Session("TransDate")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
             Try
            Dim Sch_code As String = CmbSch.SelectedValue
            Dim Series_Name As String = TxtSeries.Text
            Dim EffDate As String = TxtEffdate.Text
            Dim MaxNo As String = TxtMaxNo.Text
            Dim M_CommanBL As New CommonBL
            Dim M_Datatable As New DataTable()


            Dim maker_id As String = Session("UserID")
            Dim M_CommonBL As New CommonBL
            Dim Dt_SerDtls As DataTable

         
            M_Datatable = M_CommanBL.DebentureseriesAdd(Sch_code, TxtSeriesCode.Text, Series_Name, EffDate, MaxNo)
            ShowAlertMessage(M_Datatable.Rows(0).Item("Status").ToString())
            clear()

      

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtSeries_TextChanged(sender As Object, e As EventArgs) Handles TxtSeries.TextChanged
        
    End Sub

    Protected Sub TxtSeriesCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSeriesCode.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_SerDtls As DataTable

            Dt_SerDtls = M_CommonBL.GetDeBentureSeries(CmbSch.SelectedValue, TxtSeriesCode.Text)
            If Dt_SerDtls.Rows.Count > 0 Then

                CmbSch.SelectedValue = Dt_SerDtls.Rows(0)("SchCode").ToString()
                TxtEffdate.Text = Dt_SerDtls.Rows(0)("Effect_Date").ToString()
                TxtMaxNo.Text = Dt_SerDtls.Rows(0)("MaxNo").ToString()
                TxtSeries.Text = Dt_SerDtls.Rows(0)("SeriesName").ToString()

                'Else

                '    TxtMaxNo.Text = ""
                '    TxtSeries.Text = ""
            End If
            M_CommonBL = Nothing


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class