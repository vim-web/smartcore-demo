Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Partial Public Class LkrDisplay
    Inherits System.Web.UI.Page

    Dim dt1 As New DataTable
    Dim str1 As String
    Dim v As Integer
    Dim H As Integer
    Dim FormID As Integer = 1142
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                VALUE()
                codeload()
                LOADVALUE()
                CmbBranch.SelectedValue = Session("DefaultBrCode")
            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Sub VALUE()

        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Call DDLCODE_SelectedIndexChanged(Me.DDLCODE, System.EventArgs.Empty)
    End Sub
    Public Sub LOADVALUE()
        Dim lkrbl As New Locker_detailsbl
        Dim ds As New DataSet
        Dim dt, dt2 As DataTable
        Dim header As String

        Try
            ds = lkrbl.ExecuteQuery(CmbBranch.SelectedValue.ToString, DDLCODE.SelectedValue.ToString)
            dt = ds.Tables(0)
            dt2 = ds.Tables(1)
            Dim R, c As Integer


            For i As Integer = 0 To dt.Rows.Count - 1

                header = dt.Rows(i)(0).ToString

                If header.Length > 0 Then

                    R = Integer.Parse(dt.Rows(i)(0).ToString)

                    c = Integer.Parse(dt.Rows(i)(1).ToString)

                    check(R, c)

                End If
            Next


        Catch ex As Exception

        End Try

    End Sub
    Public Sub button(ByVal i As String, ByVal j As String)

        Dim myButton As New Label
        myButton.Width = 150
        myButton.Height = 30
        If j = "C" Then
            myButton.BackColor = Drawing.Color.Yellow
            myButton.BorderWidth = 1
            myButton.BorderColor = Drawing.Color.WhiteSmoke
        Else
            myButton.BackColor = Drawing.Color.Red
            myButton.BorderWidth = 1
            myButton.BorderColor = Drawing.Color.WhiteSmoke
        End If
        Panel1.Controls.Add(myButton)

        myButton.Text = i

    End Sub


    Public Sub check(ByVal r As Integer, ByVal c As Integer)


        Dim i As Integer
        Dim J As Integer
        Dim k As Integer = 0
        Dim lkrbl As New Locker_detailsbl
        Dim ds As New DataSet
        Dim dt2 As DataTable
        Dim n As Integer = 1
        Dim str1, header1, str2 As String
        Try
            ds = lkrbl.ExecuteQuery(CmbBranch.SelectedValue.ToString, DDLCODE.SelectedValue.ToString)
            dt2 = ds.Tables(1)
            For i = 1 To r
                For J = 1 To c
                    For m As Integer = n To 1
                        header1 = dt2.Rows(m)(0).ToString()

                        If header1.Length > 0 Then

                            str1 = dt2.Rows(k)(0).ToString()
                            str2 = dt2.Rows(k)(1).ToString()
                            button(str1, str2)

                        End If

                    Next
                    k = k + 1
                Next

            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub codeload()
        Dim lkrbl As New Locker_detailsbl
        Dim dt As DataTable
        Dim s As String
        dt = lkrbl.ExecuteQuery1(CmbBranch.SelectedValue.ToString)

        DDLCODE.DataSource = dt
        DDLCODE.DataTextField = "Lrd_Type"
        DDLCODE.DataValueField = "Lrd_Code"
        DDLCODE.DataBind()

    End Sub
    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        codeload()
        LOADVALUE()
    End Sub

    Protected Sub DDLCODE_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLCODE.SelectedIndexChanged

        LOADVALUE()

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class