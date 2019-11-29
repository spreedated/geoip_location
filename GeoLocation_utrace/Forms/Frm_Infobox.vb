Public Class Frm_Infobox
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Dim fadephase As Integer = 1
    Private Sub Infobox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim transval As Integer
        Label2.Text = Current_copyright("2014-2019", True, MyAppFullName)
        Me.Size = New Size(525, 415)
        transval = (Me.Opacity * 100)
        HScrollBar1.Value = transval
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "More infoz »" Then
            Button2.Text = "« Less infoz"
            Me.Size = New Size(525, 415)
        Else
            Button2.Text = "More infoz »"
            Me.Size = New Size(525, 738)
        End If
    End Sub
    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        Me.Opacity = HScrollBar1.Value.ToString / 100
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=V38AKB69RQY6L&source=url")
    End Sub
    Private Sub Me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, Label2.MouseDown, PictureBox1.MouseDown, RichTextBox1.MouseDown, Label1.MouseDown, Button1.MouseDown, Button2.MouseDown
        If e.Button = MouseButtons.Left Then

            IsFormBeingDragged = True

            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub
    Private Sub Me_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp, Label2.MouseUp, PictureBox1.MouseUp, RichTextBox1.MouseUp, Label1.MouseUp, Button1.MouseUp, Button2.MouseUp
        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Label2.MouseMove, PictureBox1.MouseMove, RichTextBox1.MouseMove, Label1.MouseMove, Button1.MouseMove, Button2.MouseMove
        If IsFormBeingDragged Then
            Dim temp As Point = New Point With {
                .X = Me.Location.X + (e.X - MouseDownX),
                .Y = Me.Location.Y + (e.Y - MouseDownY)
            }
            Me.Location = temp
        End If
    End Sub

#Region "FadeTimer"
    Private WithEvents FadeTimer As Timers.Timer = New Timers.Timer
    Private Sub FadeTimer_Tick(sender As Object, e As EventArgs) Handles FadeTimer.Elapsed
        Dim k = Me.Opacity.ToString
        Dim transval As Integer
        If fadephase = 1 Then
            If k >= 1 Then
                fadephase = 0
            End If
            Me.Opacity = k + 0.01
        End If
        If fadephase = 0 Then
            If k <= 0.8 Then
                fadephase = 1
            End If
            Me.Opacity = k - 0.01
        End If
        transval = (Me.Opacity * 100)
        HScrollBar1.Value = transval
    End Sub
#End Region

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Select Case CheckBox1.CheckState
            Case CheckState.Checked
                FadeTimer.Start()
            Case CheckState.Unchecked
                FadeTimer.Stop()
                Me.Opacity = 1D
        End Select
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(LinkLabel1.Text)
    End Sub
End Class