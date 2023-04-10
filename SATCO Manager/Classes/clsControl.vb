
Public Class clsControl
    Const TIMEOUT_VALUE = 3
    Dim aduhandle As Integer
    Dim iRC As Integer
    Dim iBytesWritten As Integer


    'K7	Gate Open	Gate Close

    Public Sub OpenADU()
        Try
            aduhandle = OpenAduDevice(1)
        Catch ex As Exception
            MsgBox(ex.Message)
            AddLogEntry("Couldn't Open ADU Device")
        End Try
    End Sub

    Public Sub OpenEntryGate()
        Try
            iRC = WriteAduDevice(aduhandle, "SK7", 3, 0, 500)
        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try
    End Sub

    Public Sub CloseEntryGate()
        Try
            iRC = WriteAduDevice(aduhandle, "RK7", 3, 0, 500)
        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try
    End Sub

End Class
