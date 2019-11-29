Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.IO

Public Class GeoOutput
    Private ReadOnly header As String = "" & vbCrLf &
                            "" & vbCrLf &
                            "                                 **********************************************" & vbCrLf &
                            "                                          " & MyAppFullName() & " Report" & vbCrLf &
                            "                                 **********************************************" & vbCrLf & vbCrLf
    Private ReadOnly footer As String = vbCrLf & vbCrLf &
                            "__________________________________________________________________________________________" & vbCrLf &
                            " Produced by " & MyAppFullName() & " from https://github.com/spreedated/geoip_location"

    Const outputString As String = "---------------------------------------------------------------------------" & vbCrLf &
                            "IP:            {0}" & vbCrLf &
                            "Host:          {1}" & vbCrLf &
                            "ISP:           {2}" & vbCrLf &
                            "Organization:  {3}" & vbCrLf &
                            "Region:        {4}" & vbCrLf &
                            "Countrycode:   {5}" & vbCrLf &
                            "Latitude:      {6}" & vbCrLf &
                            "Longitude:     {7}" & vbCrLf &
                            "Queries:       {8}" & vbCrLf &
                            "---------------------------------------------------------------------------"

    Public Property IP As String
    Public Property Host As String
    Public Property ISP As String
    Public Property Organization As String
    Public Property Region As String
    Public Property CountryCode As String
    Public Property Latitude As String
    Public Property Longitude As String
    Public Property Queries As String

    Public Function Output() As String
        Dim acc As String = String.Format(outputString,
                                          _IP,
                                          _Host,
                                          _ISP,
                                          _Organization,
                                          _Region,
                                          _CountryCode,
                                          _Latitude,
                                          _Longitude,
                                          _Queries)

        Return header & outputString & footer
    End Function

    Public Sub UtraceQuery(ByVal destination As String)
        Using wc As New WebClient
            Dim getXMLdoc As String = wc.DownloadString("http://xml.utrace.de/?query=" & destination)
            Dim output As StringBuilder = New StringBuilder()

            Using reader As XmlReader = XmlReader.Create(New StringReader(getXMLdoc))

                reader.ReadToFollowing("result")
                reader.MoveToFirstAttribute()

                reader.ReadToFollowing("ip")
                _IP = reader.ReadElementContentAsString()
                reader.ReadToFollowing("host")
                _Host = reader.ReadElementContentAsString()
                reader.ReadToFollowing("isp")
                _ISP = reader.ReadElementContentAsString()
                reader.ReadToFollowing("org")
                _Organization = reader.ReadElementContentAsString()
                reader.ReadToFollowing("region")
                _Region = reader.ReadElementContentAsString()
                reader.ReadToFollowing("countrycode")
                _CountryCode = reader.ReadElementContentAsString()
                reader.ReadToFollowing("latitude")
                _Latitude = reader.ReadElementContentAsString()
                reader.ReadToFollowing("longitude")
                _Longitude = reader.ReadElementContentAsString()
                reader.ReadToFollowing("queries")
                _Queries = reader.ReadElementContentAsString()

            End Using
        End Using
    End Sub
End Class
