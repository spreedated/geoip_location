Imports System.Net
Public Class whois_form

    Private Sub whois_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = myappfullname & " WhoIs"
        Dim wc As New WebClient
        Dim getHTMLdoc As String = wc.DownloadString("http://www.utrace.de/whois/" & utrace_ip)
        ' Dim getHTMLdoc As String = RichTextBox2.Text

        Dim o As String = getHTMLdoc.Substring(getHTMLdoc.IndexOf("<br><b>Details"))
        Dim i As String = o.Substring(o.IndexOf("<pre>") + 5)
        Dim u As String = i.Substring(0, i.LastIndexOf("</pre>"))

        If u.Contains("</pre>") Then
            Dim z As String = u.Substring(0, u.IndexOf("</pre>"))
            Dim p As String = u.Substring(u.IndexOf("<pre>") + 5)
            RichTextBox1.Text = z & p
        Else
            RichTextBox1.Text = u
        End If

        Debug.Print(utrace_ip)

    End Sub
End Class