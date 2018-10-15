﻿Imports System.IO
Public Class main

#Region "Resize window"
    Private windowStatus As Boolean = False
    Private meHeight As Integer = 0
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button1.Click
        meHeight = Me.Size.Height
        Button1.Enabled = False
        Button2.Visible = False
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case windowStatus
            Case False
                meHeight += 15
                Me.Size = New Point(571, meHeight)
                If meHeight >= 747 Then
                    Me.Size = New Point(571, 747)
                    Timer1.Stop()
                    windowStatus = True
                    Button1.Enabled = True
                    Button1.Text = "«« &Small window"
                    Button2.Location = New Point(487, 686)
                    Button2.Visible = True
                End If
            Case True
                meHeight -= 15
                Me.Size = New Point(571, meHeight)
                If meHeight <= 371 Then
                    Me.Size = New Point(571, 371)
                    Timer1.Stop()
                    windowStatus = False
                    Button1.Enabled = True
                    Button1.Text = "&Show region on GMaps »»"
                    Button2.Location = New Point(487, 310)
                    Button2.Visible = True
                End If
        End Select
    End Sub
#End Region

#Region "Copy clipboard & save to file buttons"
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(make_geo_output)
            ToolStripStatusLabel1.Text = "[+] Copied to clipboard!"
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
        Catch ex As Exception
            ToolStripStatusLabel1.Text = "[-] Clipboard error, check permissions"
        End Try

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                File.WriteAllText(SaveFileDialog1.FileName, make_geo_output)
                ToolStripStatusLabel1.Text = "[+] File saved! @ " & SaveFileDialog1.FileName
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
            Catch ex As Exception
                ToolStripStatusLabel1.Text = "[-] Error saving file, check permissions"
            End Try
        End If
    End Sub
#End Region

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text.Length <= 3 Or TextBox1.Text.Contains("ex.") Then
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            ToolStripStatusLabel1.Text = "[+] Input too short"
            Exit Sub
        End If

        ToolStripStatusLabel1.Text = "[-] Parsing..."
        Try
            Button1.Enabled = False
            Button3.Enabled = False
            TextBox1.Enabled = False

            utrace_query(TextBox1.Text)

            Label10.Text = utrace_ip
            Label9.Text = utrace_host
            Label8.Text = utrace_isp
            Label7.Text = utrace_org
            Label6.Text = utrace_region
            Label11.Text = utrace_countrycode
            Label13.Text = utrace_latitude
            Label15.Text = utrace_longitude
            Label17.Text = utrace_queries

            ToolStripStatusLabel1.Text = "[+] Ready - results for " & TextBox1.Text
            Try
                WebBrowser1.Navigate("http://maps.googleapis.com/maps/api/staticmap?center=" & utrace_latitude & "," & utrace_longitude & "&zoom=6&size=525x312&maptype=roadmap&markers=color:green%7Clabel:GeoIP%7C" & utrace_latitude & "," & utrace_longitude)
            Catch ex As Exception
            End Try
            Button1.Enabled = True
            Button3.Enabled = True
            TextBox1.Enabled = True
            TrackBar1.Enabled = True
            'Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
        Catch ex As Exception
            ToolStripStatusLabel1.Text = "[+] Error! - uTrace service unreachable or wrong input"
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            Button1.Enabled = False
            Button3.Enabled = True
            TextBox1.Enabled = True
            Label10.Text = Nothing
            Label9.Text = Nothing
            Label8.Text = Nothing
            Label7.Text = Nothing
            Label6.Text = Nothing
            Label11.Text = Nothing
            Label13.Text = Nothing
            Label15.Text = Nothing
            Label17.Text = Nothing
            TrackBar1.Enabled = False
            WebBrowser1.Navigate("")
            'Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
        End Try

    End Sub
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = myappfullname
        Me.Size = New Point(571, 371)
        Button2.Location = New Point(487, 310)
        ToolStripStatusLabel1.Text = "[+] Ready"
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        For Each u In GroupBox1.Controls
            If u.ToString.Contains("###") And u.ToString.ToLower.Contains("label") Then
                u.Text = Nothing
            End If
        Next

    End Sub
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        If TextBox1.Text.Contains("ex.") Then
            TextBox1.Text = Nothing
            TextBox1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text.Length <= 1 Then
            TextBox1.Text = "ex. 89.0.2.169"
            TextBox1.ForeColor = SystemColors.ControlDark
        End If
    End Sub
    Private Sub TextBox1_MouseEnter(sender As Object, e As EventArgs) Handles TextBox1.MouseEnter
        ToolTip1.Active = True
        ToolTip1.SetToolTip(TextBox1, "Accepted inputs:" & vbCrLf & vbCrLf & _
                            "IPv4: 173.194.112.119" & vbCrLf & _
                            "Domain: google.de" & vbCrLf & _
                            "")
    End Sub
    Private Sub TextBox1_MouseLeave(sender As Object, e As EventArgs) Handles TextBox1.MouseLeave
        ToolTip1.Active = False
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Dim u As String = WebBrowser1.Url.ToString
        Dim i As Integer = 0
        If u.Contains("zoom=") Then
            u = u.Substring(u.IndexOf("zoom=") + 5)
            u = u.Substring(0, u.IndexOf("&"))
            Try
                i = CInt(u)
                TrackBar1.Enabled = True
                TrackBar1.Value = i
            Catch ex As Exception
                TrackBar1.Enabled = False
            End Try
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Dim u As String = WebBrowser1.Url.ToString.Substring(0, WebBrowser1.Url.ToString.IndexOf("zoom=") + 5)
        Dim o As String = WebBrowser1.Url.ToString.Substring(WebBrowser1.Url.ToString.IndexOf("zoom=") + 5)
        Dim oi As String = o.Substring(o.IndexOf("&"))
        Dim ui As String = o.Substring(0, o.IndexOf("&"))
        Dim i As Integer = sender.Value
        Dim fullURL As String = u & CStr(i) & oi

        WebBrowser1.Navigate(fullURL)
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        infobox.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        whois_form.ShowDialog()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button3.PerformClick()
            TextBox1.Focus()
        End If
    End Sub
End Class
