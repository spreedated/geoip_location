Imports System.Net
Public Class Frm_Whois
    Public Property GOutput As GeoOutput

    Private Sub Whois_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = myappfullname & " WhoIs"
        Using wc As New WebClient
            Dim getHTMLdoc As String = wc.DownloadString("http://www.utrace.de/whois/" & GOutput.IP)
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
        End Using
    End Sub
End Class