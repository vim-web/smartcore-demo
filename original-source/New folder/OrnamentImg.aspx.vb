Imports SMARTCORE_BL
Public Class OrnamentImg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnAccid.Value = Request.QueryString("AccID")
        Load_Img(hdnAccid.Value)
    End Sub
    Private Sub Load_Img(AccId)
        Dim M_Qry As String
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataSet
        Try
            M_Dt = M_CommonBL.Get_OrnamentImgDet("", AccId)

            If M_Dt.Tables.Count > 0 Then
                Dim Tbl1 As DataTable = M_Dt.Tables(0)
                Dim Tbl2 As DataTable = M_Dt.Tables(1)
                If Tbl1.Rows.Count > 0 Then

                    'Dim dt As DataTable = DirectCast(Session("OrnImgTbl"), DataTable)
                    Dim bytes As Byte() = DirectCast(Tbl1.Rows(0).Item("ImageDetails"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    ImgPhoto.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
                End If

                If Tbl2.Rows.Count > 0 Then
                    Dim bytes2 As Byte() = DirectCast(Tbl2.Rows(0).Item("ImageDetails"), Byte())
                    Dim base64String2 As String = Convert.ToBase64String(bytes2, 0, bytes2.Length)
                    ImgPhoto2.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String2
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class