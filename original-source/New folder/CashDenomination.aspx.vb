Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Public Class CashDenomination
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim Closebal As String = Request.QueryString("Closebal")

                Load_DefaultValues()
                Txtcsbal.Text = Closebal
                Session("PreUrl") = Request.UrlReferrer.ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_CommanBL As New CommonBL
            Dim M_Datatable As New DataTable
            Dim Closebal As String = Request.QueryString("Closebal")
            Dim TotalSum As Double = Val(TxtSum.Text)
            ' Dim TotalDecSum As Double = TotalSum.ToString("0.00")
            Dim PageURlD As String = "~/DayBeginEnd.aspx?Status=" + "Y"

            If TotalSum = Closebal Then
                M_Datatable = M_CommanBL.InsertDENOMINATION(DrpBranch0.SelectedValue, TxtFromDate.Text, Session("UserID"), Val(Txt2000.Text), Val(Txt1000.Text), Val(Txt500.Text), Val(Txt200.Text), Val(Txt100.Text), Val(Txt50.Text), Val(Txt20.Text), Val(Txt10.Text), Val(Txt5.Text), Val(Txtcoin5.Text), Val(Txtcoin2.Text), Val(Txtcoin1.Text), Val(Txtother.Text), Val(Closebal), Val(TxtSum.Text))
                ShowAlertMessage("Saved Successfully")
                ClearAll()
                'Response.Redirect(Session("PreUrl"))
                Response.Redirect(PageURlD)
            Else
                ShowAlertMessage("Please Check Entered Amount")
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        ClearAll()

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
            Dim M_CommonBL As New CommonBL
            Dim dt As New DataTable
            M_CommonBL.Fill_Branch(DrpBranch0)
            DrpBranch0.SelectedValue = Session("DefaultBrCode")
            TxtFromDate.Text = Session("TransDate")


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClear.Click
        'Response.Redirect("~/main.aspx")
        'Response.Redirect(Session("PreUrl"))
        Dim PageURlD As String = "~/DayBeginEnd.aspx?Status=" + "N"
        Response.Redirect(PageURlD)

    End Sub

    Protected Sub Txt100_TextChanged(sender As Object, e As EventArgs) Handles Txt100.TextChanged
        Try
            Dim MatAmt As Double

            If Txt100.Text > "0" Then
                MatAmt = 100 * Val(Txt100.Text)
                Txtsum100.Text = MatAmt
            Else
                Txtsum100.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txtcoin2_TextChanged(sender As Object, e As EventArgs) Handles Txtcoin2.TextChanged
        Try
            Dim MatAmt As Double

            If Txtcoin2.Text > "0" Then
                MatAmt = 2 * Val(Txtcoin2.Text)
                Txtsumcoin2.Text = MatAmt
            Else
                Txtsumcoin2.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txtcoin1_TextChanged(sender As Object, e As EventArgs) Handles Txtcoin1.TextChanged
        Try
            Dim MatAmt As Double

            If Txtcoin1.Text > "0" Then
                MatAmt = 1 * Val(Txtcoin1.Text)
                Txtsumcoin1.Text = MatAmt
            Else
                Txtsumcoin1.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt2000_TextChanged(sender As Object, e As EventArgs) Handles Txt2000.TextChanged
        Try
            Dim MatAmt As Double

            If Txt2000.Text > "0" Then
                MatAmt = 2000 * Val(Txt2000.Text)
                Txtsum2000.Text = MatAmt
            Else
                Txtsum2000.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt1000_TextChanged(sender As Object, e As EventArgs) Handles Txt1000.TextChanged
        Try
            Dim MatAmt As Double

            If Txt1000.Text > "0" Then
                MatAmt = 1000 * Val(Txt1000.Text)
                Txtsum1000.Text = MatAmt
            Else
                Txtsum1000.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt500_TextChanged(sender As Object, e As EventArgs) Handles Txt500.TextChanged
        Try
            Dim MatAmt As Double

            If Txt500.Text > "0" Then
                MatAmt = 500 * Val(Txt500.Text)
                Txtsum500.Text = MatAmt
            Else
                Txtsum500.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt200_TextChanged(sender As Object, e As EventArgs) Handles Txt200.TextChanged
        Try
            Dim MatAmt As Double

            If Txt200.Text > "0" Then
                MatAmt = 200 * Val(Txt200.Text)
                Txtsum200.Text = MatAmt
            Else
                Txtsum200.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt50_TextChanged(sender As Object, e As EventArgs) Handles Txt50.TextChanged
        Try
            Dim MatAmt As Double

            If Txt50.Text > "0" Then
                MatAmt = 50 * Val(Txt50.Text)
                Txtsum50.Text = MatAmt
            Else
                Txtsum50.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt20_TextChanged(sender As Object, e As EventArgs) Handles Txt20.TextChanged
        Try
            Dim MatAmt As Double

            If Txt20.Text > "0" Then
                MatAmt = 20 * Val(Txt20.Text)
                Txtsum20.Text = MatAmt
            Else
                Txtsum20.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt10_TextChanged(sender As Object, e As EventArgs) Handles Txt10.TextChanged
        Try
            Dim MatAmt As Double

            If Txt10.Text > "0" Then
                MatAmt = 10 * Val(Txt10.Text)
                Txtsum10.Text = MatAmt
            Else
                Txtsum10.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt5_TextChanged(sender As Object, e As EventArgs) Handles Txt5.TextChanged
        Try
            Dim MatAmt As Double

            If Txt5.Text > "0" Then
                MatAmt = 5 * Val(Txt5.Text)
                Txtsum5.Text = MatAmt
            Else
                Txtsum5.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txtcoin5_TextChanged(sender As Object, e As EventArgs) Handles Txtcoin5.TextChanged
        Try
            Dim MatAmt As Double

            If Txtcoin5.Text > "0" Then
                MatAmt = 5 * Val(Txtcoin5.Text)
                Txtsumcoin5.Text = MatAmt
            Else
                Txtsumcoin5.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtSum_TextChanged(sender As Object, e As EventArgs) Handles TxtSum.TextChanged
        Try
            Dim TotalSum As Double

            TotalSum = Val(Txtsum2000.Text) + Val(Txtsum1000.Text) + Val(Txtsum500.Text) + Val(Txtsum200.Text) + Val(Txtsum100.Text) + Val(Txtsum50.Text) + Val(Txtsum20.Text) + Val(Txtsum10.Text) + Val(Txtsum5.Text) + Val(Txtsumcoin5.Text) + Val(Txtsumcoin2.Text) + Val(Txtsumcoin1.Text) + Val(Txtother.Text)
            If TotalSum = "0" Then
                TxtSum.Text = ""
            Else
                TxtSum.Text = TotalSum
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ClearAll()

        Txt2000.Text = ""
        Txt1000.Text = ""
        Txt500.Text = ""
        Txt200.Text = ""
        Txt100.Text = ""
        Txt50.Text = ""
        Txt20.Text = ""
        Txt10.Text = ""
        Txt5.Text = ""
        Txtcoin5.Text = ""
        Txtcoin2.Text = ""
        Txtcoin1.Text = ""
        Txtsum2000.Text = ""
        Txtsum1000.Text = ""
        Txtsum500.Text = ""
        Txtsum200.Text = ""
        Txtsum100.Text = ""
        Txtsum50.Text = ""
        Txtsum20.Text = ""
        Txtsum10.Text = ""
        Txtsum5.Text = ""
        Txtsumcoin5.Text = ""
        Txtsumcoin2.Text = ""
        Txtsumcoin1.Text = ""
        Txtother.Text = ""
        Txtothers.Text = ""
        DrpBranch0.SelectedValue = Session("DefaultBrCode")
        TxtFromDate.Text = Session("TransDate")

    End Sub

    Protected Sub Txtother_TextChanged(sender As Object, e As EventArgs) Handles Txtother.TextChanged
        Try
            Dim MatAmt As Double

            If Txtother.Text > "0" Then
                MatAmt = Val(Txtother.Text)
                Txtother.Text = MatAmt
            Else
                Txtother.Text = ""
            End If
            TxtSum_TextChanged(Me.TxtSum, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub
End Class