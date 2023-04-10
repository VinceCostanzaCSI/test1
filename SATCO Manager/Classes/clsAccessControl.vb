Public Class clsAccessControl
    Private SQL As New SQLControl

    'local variable(s) to hold property value(s)
    Private sId As String               'local copy
    Private sCardId As String
    Private sDriverId As String         'local copy
    Private dDateTime As Date           'local copy
    Private bValid As Boolean

    Private sCurrentId As String        'local copy
    Private bEOF As Boolean
    Private bBOF As Boolean

    Public Sub UpdateRecord(CurrentId As String)

        Try
            Dim UpdateCmd As String = "UPDATE AccessControl " & _
                                      "SET Id ='" & sId & "' , " & _
                                      "CardId ='" & sCardId & "' , " & _
                                      "DriverId ='" & sDriverId & "' , " & _
                                      "DateTime ='" & dDateTime & "' , " & _
                                      "Valid ='" & bValid & "' " & _
                                      "WHERE ID = '" & CurrentId & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating AccessControl File")
                'Stop
            Else
                AddLogEntry("AccessControl File Updated")
                'Stop
            End If

        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try

    End Sub

    Public Function AddRecord()
        Try
            'sId = CurentId
            Dim strInsert As String = "INSERT INTO AccessControl (CardId, DriverId, DateTime, Valid) " & _
                                         "VALUES (" & _
                                         "'" & sCardId & "'," & _
                                         "'" & sDriverId & "'," & _
                                         "'" & dDateTime & "'," & _
                                         "'" & bValid & "') "
            SQL.DataUpdate(strInsert)
            AddRecord = True
        Catch ex As Exception
            AddRecord = False
        End Try
    End Function

    Public Sub DeleteRecord(ByVal CurrentId As String)
        Try
            SQL.RunQuery("Select * from AccessControl where Id = '" & CurrentId & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM AccessControl where Id ='" & CurrentId & "' ")

            End If
        Catch ex As Exception
            AddLogEntry("DeleteAccessControl: " & ex.Message)
        End Try
    End Sub

    Public Function FindRecord(ByVal CurrentId As String)
        Try
            SQL.RunQuery("Select * from AccessControl where Id = '" & CurrentId & "' ")
            If SQL.RecordCount = 0 Then
                AddLogEntry("AccessControl Record not found")

            Else
                With SQL.SQLDataset.Tables(0).Rows(0)

                    sId = .Item("ID")
                    sCardId = .Item("CardId")
                    sDriverId = .Item("DriverId")
                    dDateTime = .Item("DateTime")
                    bValid = .Item("Valid")

                End With
            End If
            FindRecord = True
        Catch ex As Exception
            FindRecord = False
        End Try
    End Function

    Property BOF() As Boolean
        Get
            BOF = bBOF
        End Get
        Set(value As Boolean)
            bBOF = value
        End Set
    End Property

    Property EOF() As Boolean
        Get
            EOF = bEOF
        End Get
        Set(value As Boolean)
            bEOF = value
        End Set
    End Property

    Property Id() As String
        Get
            Id = sId
        End Get
        Set(value As String)
            sId = value
        End Set
    End Property

    Property CardId() As String
        Get
            CardId = sCardId
        End Get
        Set(value As String)
            sCardId = value
        End Set
    End Property
    Property DriverId() As String
        Get
            DriverId = sDriverId
        End Get
        Set(value As String)
            sDriverId = value
        End Set
    End Property
    Property DateTime() As Date
        Get
            DateTime = dDateTime
        End Get
        Set(value As Date)
            dDateTime = value
        End Set
    End Property
    Property Valid() As Boolean
        Get
            Valid = bValid
        End Get
        Set(value As Boolean)
            bValid = value
        End Set
    End Property

End Class
