


Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient


Public Class ShowImage
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If context.Request.QueryString("Cust_Id") Is Nothing Then
            Return
        End If


        ' Dim connStr As String = "Data Source=sanoj\sql2008;Initial Catalog=Smart_ImageMaster;User ID=sa;Password=Safe2Safe"
        Dim connStr As String = ConfigurationManager.AppSettings("SQL_CONNSTR1").ToString

        Dim CustId As String = context.Request.QueryString("Cust_Id")
        Dim pictureId As String = context.Request.QueryString("Img_Id")
        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand("SELECT  Img_Image FROm Cust_ImageMaster_M_Tbl WHERE Cust_Id = '" & Trim(CustId) & "' and Img_Id = " & pictureId, conn)
                'cmd.Parameters.Add(New SqlParameter("@CustomerId", CustId))
                'cmd.Parameters.Add(New SqlParameter("@ImgId", pictureId))
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                    reader.Read()

                    context.Response.BinaryWrite(DirectCast(reader(reader.GetOrdinal("Img_Image")), [Byte]()))
                    reader.Close()
                End Using
            End Using
        End Using

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class