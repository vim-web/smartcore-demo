Imports SMARTCORE_BL
Imports System.Data.SqlClient
Partial Public Class CustImage
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1040

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hdnAccid.Value = Request.QueryString("CustID")
                Load_Image()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_Image()
        Try
            Dim connStr As String = ConfigurationManager.AppSettings("SQL_CONNSTR1").ToString
            Dim Cust_Id As String
            Cust_Id = hdnAccid.Value
            Dim table As New DataTable()
            Using conn As New SqlConnection(connStr)
                Dim sql As String = "SELECT Cust_ID,Img_ID,Is_ImageName as Img_Name, Img_Image FROM Cust_ImageMaster_M_Tbl,Cust_ImageDetails_P_Tbl where Cust_ID ='" & Cust_Id & "' and Is_ImageID = Img_ID "
                Using cmd As New SqlCommand(sql, conn)
                    Using ad As New SqlDataAdapter(cmd)
                        conn.Open()
                        ad.Fill(table)
                        conn.Close()
                    End Using
                End Using
            End Using

            DgvImage.DataSource = table
            DgvImage.DataBind()
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

End Class