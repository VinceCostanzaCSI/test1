Public Class frmAccessControlView
    Private SQL As New SQLControl
    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

    End Sub

    Private Sub frmAccessControlView_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        Try
            LoadTimer.Enabled = False
            SQL.RunQuery("Select * From AccessControl where DateTime between '" & StartDate & "' and '" & EndDate & "' Order by DateTime desc ")
            If SQL.RecordCount = 0 Then
                MsgBox("No Access Records Found")
            Else
                Try
                    'lblTotal.Text = SQL.RecordCount
                    DGVData.DataSource = SQL.SQLDataset.Tables(0)

                    'DGVData.Rows(0).Selected = True

                    SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class