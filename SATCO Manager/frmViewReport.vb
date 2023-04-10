Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmViewReport

    Private Sub rptViewer_Load(sender As System.Object, e As System.EventArgs) Handles rptViewer.Load
        Try
            configureCRYSTALREPORT()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub configureCRYSTALREPORT()
        Try
            Dim myConnectionInfo As New ConnectionInfo()
            myConnectionInfo.ServerName = DBPath 'Environment.MachineName
            myConnectionInfo.DatabaseName = "SATCO"
            myConnectionInfo.UserID = "Adminuser"
            myConnectionInfo.Password = "Tampa08"
            setDBLOGONforREPORT(myConnectionInfo)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub setDBLOGONforREPORT(ByVal myconnectioninfo As ConnectionInfo)
        Dim mytableloginfos As New TableLogOnInfos()

        mytableloginfos = rptViewer.LogOnInfo
        For Each myTableLogOnInfo As TableLogOnInfo In mytableloginfos
            myTableLogOnInfo.ConnectionInfo = myconnectioninfo
        Next

    End Sub

    Private Sub frmViewReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
    End Sub

End Class