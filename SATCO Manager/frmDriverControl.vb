Public Class frmDriverControl
    Private SQL As New SQLControl

    Private Sub frmDriverControl_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)

        SQL.RunQuery("Select Top 1* From DriverInfo")
        If Sql.RecordCount = 0 Then
            MsgBox("No Record Found")
        Else
            Try
                DGVData.DataSource = SQL.SQLDataset.Tables(0)
                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

    End Sub

    Private Sub cmdModify_Click(sender As System.Object, e As System.EventArgs) Handles cmdModify.Click
        SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

        'Refresh DataGrid
        DGVData.Refresh()
    End Sub
End Class