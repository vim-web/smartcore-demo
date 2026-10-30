Imports System.Data.SqlClient
Partial Public Class CustImg
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1041

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim connStr As String = "Data Source=SANOJ\SQL2008;Initial Catalog=Smart_ImageMaster;User ID=sa;Password=Safe2Safe"
        Dim connStr As String = ConfigurationManager.AppSettings("SQL_CONNSTR1").ToString
        Dim Cust_Id As String
        Cust_Id = Session("CustomerId")
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

        GridView1.DataSource = table
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class