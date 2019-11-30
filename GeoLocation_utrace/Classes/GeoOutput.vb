Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.IO

Public Class GeoOutput
    Public ReadOnly header As String = "" & vbCrLf &
                            "" & vbCrLf &
                            "                     **********************************************" & vbCrLf &
                            "                              " & MyAppFullName() & " Report" & vbCrLf &
                            "                     **********************************************" & vbCrLf & vbCrLf
    Public ReadOnly footer As String = vbCrLf & vbCrLf &
                            "__________________________________________________________________________________________" & vbCrLf &
                            " Produced by " & MyAppFullName() & " from https://github.com/spreedated/geoip_location"

    Public ReadOnly outputString As String = "---------------------------------------------------------------------------" & vbCrLf &
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
    Public Property ServiceName As String
    Public Property Destination As String = Nothing

    Public Overridable Function Output() As String
        Dim acc As String = String.Format(outputString,
                                          IP,
                                          Host,
                                          ISP,
                                          Organization,
                                          Region,
                                          CountryCode,
                                          Latitude,
                                          Longitude,
                                          Queries)

        Return header & acc & footer
    End Function
End Class

Public Class UTrace : Inherits GeoOutput
    Sub New()
        Me.ServiceName = "UTrace"
    End Sub
    Public Sub Query()
        Try
            Using wc As New WebClient
                Dim getXMLdoc As String = wc.DownloadString("http://xml.utrace.de/?query=" & Me.Destination)
                Dim output As StringBuilder = New StringBuilder()

                Using reader As XmlReader = XmlReader.Create(New StringReader(getXMLdoc))

                    reader.ReadToFollowing("result")
                    reader.MoveToFirstAttribute()

                    reader.ReadToFollowing("ip")
                    Me.IP = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("host")
                    Me.Host = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("isp")
                    Me.ISP = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("org")
                    Me.Organization = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("region")
                    Me.Region = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("countrycode")
                    Me.CountryCode = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("latitude")
                    Me.Latitude = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("longitude")
                    Me.Longitude = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("queries")
                    Me.Queries = reader.ReadElementContentAsString()

                End Using
            End Using
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub

    Public Overrides Function Output() As String
        Return MyBase.Output()
    End Function
End Class
