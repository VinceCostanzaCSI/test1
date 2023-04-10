Imports System.Reflection
Imports System.IO

Public Class clsRail
    Dim location = Assembly.GetExecutingAssembly().Location
    Dim appPath = Path.GetDirectoryName(location)       ' C:\Some\Directory
    Dim appName = Path.GetFileName(location)            ' MyLibrary.DLL

    Private SQL As New SQLControl

    'local variable(s) to hold property value(s)
    Private sCode As String             'local copy
    Private iId As Integer              'local copy
    Private iNumber As Integer
    Private sCarPrefix As String
    Private sCar As String
    Private sSeal1 As String
    Private sSeal2 As String
    Private sSeal3 As String
    Private sSeal4 As String
    Private lNetWt As Long
    Private sCurrentId As String    'local copy

    Public Sub UpdateRecord(Code As String, Id As Integer, Number As Integer)
        Try
            Dim UpdateCmd As String = "UPDATE RAIL " &
                                      "SET Code ='" & sCode & "' , " &
                                      "Id ='" & iId & "' , " &
                                      "Number ='" & iNumber & "' , " &
                                      "CarPrefix ='" & sCarPrefix & "' , " &
                                      "Car ='" & sCar & "' , " &
                                      "Seal1 ='" & sSeal1 & "' , " &
                                      "Seal2 ='" & sSeal2 & "' , " &
                                      "Seal3 ='" & sSeal3 & "' , " &
                                      "Seal4 ='" & sSeal4 & "' , " &
                                      "NetWt ='" & lNetWt & "' " &
                                      "WHERE Code = '" & Code & " and Id = '" & Id & " and Number = " & Number & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating Rail File")
            Else
                AddLogEntry("Rail File Updated")
            End If

        Catch ex As Exception
            AddLogEntry("RailUpdateRecord: " & ex.Message)
            'Stop
        End Try
    End Sub

    Public Function AddRecord(Code As String, Id As Integer, Number As Integer)
        Try

            Dim strInsert As String = "INSERT INTO RAIL (Code, Id, Number, CarPrefix, Car, Seal1, Seal2, Seal3, Seal4, NetWt) " &
                                         "VALUES (" &
                                         "'" & sCode & "'," &
                                         "'" & iId & "'," &
                                         "'" & iNumber & "'," &
                                         "'" & sCarPrefix & "'," &
                                         "'" & sCar & "'," &
                                         "'" & sSeal1 & "'," &
                                         "'" & sSeal2 & "'," &
                                         "'" & sSeal3 & "'," &
                                         "'" & sSeal4 & "'," &
                                         "'" & lNetWt & "') "
            SQL.DataUpdate(strInsert)
            AddRecord = True
        Catch ex As Exception
            AddRecord = False
            AddLogEntry("RailAddRecord: " & ex.Message)
            'Stop
        End Try
    End Function

    Public Function FindRecord(Code As String, Id As Integer, Number As Integer)
        Try
            SQL.RunQuery("Select * from RAIL where Code = '" & Code & "' and Id = '" & Id & "' and Number = '" & Number & "' ")
            If SQL.RecordCount = 0 Then
                AddLogEntry("Rail Record " & Code & "-" & Id & " #" & Number & " not found")
                FindRecord = False
            Else
                With SQL.SQLDataset.Tables(0).Rows(0)
                    sCode = .Item("Code")
                    iId = .Item("Id")
                    iNumber = .Item("Number")
                    sCarPrefix = .Item("CarPrefix")
                    sCar = .Item("Car")
                    sSeal1 = .Item("Seal1") & ""
                    sSeal2 = .Item("Seal2") & ""
                    sSeal3 = .Item("Seal3") & ""
                    sSeal4 = .Item("Seal4") & ""
                    lNetWt = .Item("NetWt")
                End With
                FindRecord = True
            End If

        Catch ex As Exception
            AddLogEntry("SAFindRecord: " & ex.Message)
            FindRecord = False
        End Try
    End Function

    Public Property Code() As String
        Get
            Code = sCode
        End Get
        Set(value As String)
            sCode = value
        End Set
    End Property

    Property Id() As Integer
        Get
            Id = iId
        End Get
        Set(value As Integer)
            iId = value
        End Set
    End Property
    Public Property Number() As Integer
        Get
            Number = iNumber
        End Get
        Set(value As Integer)
            iNumber = value
        End Set
    End Property

    Public Property CarPrefix() As String
        Get
            CarPrefix = sCarPrefix
        End Get
        Set(value As String)
            sCarPrefix = value
        End Set
    End Property
    Public Property Car() As String
        Get
            Car = sCar
        End Get
        Set(value As String)
            sCar = value
        End Set
    End Property

    Public Property Seal1() As String
        Get
            Seal1 = sSeal1
        End Get
        Set(value As String)
            sSeal1 = value
        End Set
    End Property
    Public Property Seal2() As String
        Get
            Seal2 = sSeal2
        End Get
        Set(value As String)
            sSeal2 = value
        End Set
    End Property
    Public Property Seal3() As String
        Get
            Seal3 = sSeal3
        End Get
        Set(value As String)
            sSeal3 = value
        End Set
    End Property
    Public Property Seal4() As String
        Get
            Seal4 = sSeal4
        End Get
        Set(value As String)
            sSeal4 = value
        End Set
    End Property
    Public Property NetWt() As Long
        Get
            NetWt = lNetWt
        End Get
        Set(value As Long)
            lNetWt = value
        End Set
    End Property

End Class
