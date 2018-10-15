Imports System.Net
Imports System.Xml
Imports System.Text
Imports System.IO

Module Module2
    Public utrace_ip As String = Nothing
    Public utrace_host As String = Nothing
    Public utrace_isp As String = Nothing
    Public utrace_org As String = Nothing
    Public utrace_region As String = Nothing
    Public utrace_countrycode As String = Nothing
    Public utrace_latitude As String = Nothing
    Public utrace_longitude As String = Nothing
    Public utrace_queries As String = Nothing
#Region "GeoIP Output file/clipboard"

    Public Function make_geo_output() As String
        Dim header As String = "" & vbCrLf & _
                            "" & vbCrLf & _
                            "                                 **********************************************" & vbCrLf & _
                            "                                          " & myappfullname & " Report" & vbCrLf & _
                            "                                 **********************************************" & vbCrLf & vbCrLf

        Dim footer As String = vbCrLf & vbCrLf & _
                            "__________________________________________________________________________________________" & vbCrLf & _
                            " Produced by " & myappfullname & " from https://sourceforge.net/projects/geoiplocation1337/"

        Dim output As String = "---------------------------------------------------------------------------" & vbCrLf & _
                            "IP:            " & utrace_ip & vbCrLf & _
                            "Host:          " & utrace_host & vbCrLf & _
                            "ISP:           " & utrace_isp & vbCrLf & _
                            "Organization:  " & utrace_org & vbCrLf & _
                            "Region:        " & utrace_org & vbCrLf & _
                            "Countrycode:   " & utrace_countrycode & vbCrLf & _
                            "Latitude:      " & utrace_latitude & vbCrLf & _
                            "Longitude:     " & utrace_longitude & vbCrLf & _
                            "Queries:       " & utrace_queries & vbCrLf & _
                            "---------------------------------------------------------------------------"
        Return header & output & footer
    End Function
#End Region

#Region "uTrace Query"
    Public Sub utrace_query(ByVal destination As String)
        Dim wc As New WebClient
        Dim getXMLdoc As String = wc.DownloadString("http://xml.utrace.de/?query=" & destination)
        Dim output As StringBuilder = New StringBuilder()

        Using reader As XmlReader = XmlReader.Create(New StringReader(getXMLdoc))

            reader.ReadToFollowing("result")
            reader.MoveToFirstAttribute()

            reader.ReadToFollowing("ip")
            utrace_ip = reader.ReadElementContentAsString()
            reader.ReadToFollowing("host")
            utrace_host = reader.ReadElementContentAsString()
            reader.ReadToFollowing("isp")
            utrace_isp = reader.ReadElementContentAsString()
            reader.ReadToFollowing("org")
            utrace_org = reader.ReadElementContentAsString()
            reader.ReadToFollowing("region")
            utrace_region = reader.ReadElementContentAsString()
            reader.ReadToFollowing("countrycode")
            utrace_countrycode = reader.ReadElementContentAsString()
            reader.ReadToFollowing("latitude")
            utrace_latitude = reader.ReadElementContentAsString()
            reader.ReadToFollowing("longitude")
            utrace_longitude = reader.ReadElementContentAsString()
            reader.ReadToFollowing("queries")
            utrace_queries = reader.ReadElementContentAsString()

        End Using
    End Sub
#End Region
End Module
