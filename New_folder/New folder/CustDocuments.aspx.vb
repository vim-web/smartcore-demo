Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports System.IO
Imports System.Web.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration

Imports System.Text.RegularExpressions
Partial Public Class CustDocuments
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1149
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


                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=300,width=500,scrollbars=yes');return false;")
                
                TxtCustID.Focus()
                Session("SearchResult") = ""
                CmbAdmBranch.Attributes.Add("readonly", "readonly")
            End If

            If TxtCustID.Text <> "" Then
                TxtCustID_TextChanged(Me.TxtCustID, System.EventArgs.Empty)
            Else
                TxtCustID.Text = Session("SearchResult")
            End If
         
            If Not Me.IsPostBack Then
                Session("Hexstring") = ""
                If Request.InputStream.Length > 0 Then
                    Using reader As New StreamReader(Request.InputStream)
                        Dim hexString As String = Server.UrlEncode(reader.ReadToEnd())
                        Session("Hexstring") = hexString
                        Dim imageName As String = DateTime.Now.ToString("dd-MM-yy hh-mm-ss")
                        Dim imagePath As String = String.Format("~/Captures/{0}.JPG", imageName)
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString))
                        Session("CapturedImage") = ResolveUrl(imagePath)
                        Session("CapturedImageType") = "Captures/" + imageName + ".JPG"

                    End Using
                End If
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
    Private Sub Load_DefaultValues()
        Try
            CmbPhotoType.Items.Add("Browse")
            CmbPhotoType.Items.Add("Web Cam")
            LoadType()
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbAdmBranch)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Shared Function ConvertHexToBytes(ByVal hex As String) As Byte()
        Dim bytes As Byte() = New Byte(hex.Length / 2 - 1) {}
        For i As Integer = 0 To hex.Length - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Shared Function GetCapturedImage() As String
        Dim url As String = HttpContext.Current.Session("CapturedImage").ToString()
        HttpContext.Current.Session("CapturedImage") = Nothing
        Return url
    End Function

  
    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            SaveImage()
            'AddImageWebCam()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

   

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Try
            Clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub SaveImage()
        Try
            
            UpLoadImageFile(Session("CapturedImageType").ToString())
        Catch ex As Exception
            ShowAlertMessage(ex.Message())
        End Try
    End Sub

    Private Sub UpLoadImageFile(ByVal info As String)

        Dim objConn As SqlConnection
        Dim objCom As SqlCommand
        Try

            Dim converter As New ImageConverter()
            Dim Imge As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(info))
            Dim imgBytes As Byte() = New Byte(-1) {}
            imgBytes = DirectCast(converter.ConvertTo(Imge, imgBytes.[GetType]()), Byte())
            objConn = New SqlConnection(ConfigurationManager.AppSettings("SQL_CONNSTR").ToString())
            Dim ImageDB As String = ConfigurationManager.AppSettings("ImageDB").ToString()

            objCom = New SqlCommand("delete from " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl where Cust_ID='" + TxtCustID.Text + "'", objConn)
            objConn.Open()
            objCom.ExecuteNonQuery()
            objConn.Close()
            Dim InsQry As String
            InsQry = "insert into " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl(Br_Code,Cust_ID,Cust_Type,Cust_Oper_Id,Img_ID,Img_Date,MakerID,CheckerID,Making_Time,Checking_Time,Img_Image) values ('" + CmbAdmBranch.SelectedValue + "','" + TxtCustID.Text + "', "
            InsQry = InsQry + "'I','0','1', "
            InsQry = InsQry + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','" + Session("LoginUserID").ToString() + "','" + Session("LoginUserID").ToString() + "','" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "', "
            InsQry = InsQry + "'" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "','')"

            objCom = New SqlCommand(InsQry, objConn)
            objConn.Open()
            objCom.ExecuteNonQuery()
            objConn.Close()

            objCom = New SqlCommand(" update " + ImageDB + ".dbo.Cust_ImageMaster_M_Tbl set Img_Image =   @Picture where Cust_ID='" + TxtCustID.Text + "'", objConn)

            Dim pictureParameter As SqlParameter = New SqlParameter("@Picture", SqlDbType.Image)
            pictureParameter.Value = imgBytes
            objCom.Parameters.Add(pictureParameter)
            objConn.Open()
            objCom.ExecuteNonQuery()
            objConn.Close()
            ShowAlertMessage("Saved Successfully...")
            Clear()
            Session("CapturedImageType") = ""

        Catch ex As Exception
            Throw New Exception(ex.Message)
            objConn.Close()
        End Try
    End Sub
    
    Protected Sub CmbPhotoType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbPhotoType.SelectedIndexChanged
        Try
            LoadType()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub LoadType()
        Try
            If CmbPhotoType.SelectedIndex = 0 Then
                TxtBrowse.Visible = True
                cam.Visible = False
                imgCapture.Visible = False
                ImgPhoto.Visible = True
                btnCapture.Visible = False
                btnCapture0.Visible = True
            Else
                btnCapture.Visible = True
                btnCapture0.Visible = False
                TxtBrowse.Visible = False
                cam.Visible = True
                imgCapture.Visible = True
                ImgPhoto.Visible = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub btnCapture0_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture0.Click
        Try
            Try
                If CmbPhotoType.SelectedIndex = 0 Then
                    Dim CopyPath As String
                    Dim HomeDir As String
                    Dim FileName As String = ""
                    FileName = DateTime.Now.ToString("MMddyyyyHHmmssms")


                    CopyPath = "~\\Files\\" + DateTime.Now.Second.ToString() + TxtBrowse.Name
                    HomeDir = ConfigurationManager.AppSettings("HomeFolder").ToString()
                    Server.MapPath("CustTempImages//" + FileName + ".jpg")
                    'Dim imageSize As New Byte(TxtBrowse.PostedFile.ContentLength)



                    ViewState("PhotoName") = Server.MapPath("CustTempImages//" + FileName + ".jpg")
                    Dim imageSize As Byte() = New Byte(TxtBrowse.PostedFile.ContentLength - 1) {}

                    Dim uploadedImage As HttpPostedFile = TxtBrowse.PostedFile

                    uploadedImage.InputStream.Read(imageSize, 0, CInt(TxtBrowse.PostedFile.ContentLength))
                    Dim ms As New MemoryStream(imageSize)

                    Dim Img As System.Drawing.Image = Bitmap.FromStream(ms, True)
                    Img.Save(Server.MapPath("CustTempImages/" + FileName + ".jpg"))

                    ImgPhoto.ImageUrl = Server.MapPath("~/CustTempImages/" + FileName + ".jpg")

                    Session("CapturedImageType") = "CustTempImages/" + FileName + ".jpg"
                  




                End If


            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCapture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture.Click
        Try
           

     Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtCustID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCustID.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(TxtCustID.Text, M_PresAddress, M_PermAddress)
            With M_CustMaster

                CmbAdmBranch.SelectedValue = .BrCode
                TxtName.Text = .CustName
                TxtAddress.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode
                'TxtPermLand.Text = 
                'TxtPermPin.Text = 
                'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
                'TxtPermDist.Text = M_PresAddress.AddDist
                'TxtPermCountry.Text = M_PresAddress.AddCountry

            End With




        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            TxtAddress.Text = ""
            TxtCustID.Text = ""
            TxtName.Text = ""
            LoadType()
            imgCapture.Src = ""
            ImgPhoto.ImageUrl = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class