Public Class frmTankGrid
    Private SQL As New SQLControl
    Private Sub frmTankGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterForm(Me)
        If FileControl = 0 Then
            cmdModify.Visible = False
        End If

        If FileControl = 1 Then
            cmdModify.Visible = False
        End If

        If FileControl = 2 Then
            cmdModify.Visible = True
        End If

        SQL.RunQuery("Select * From Tank Order by Id asc")
        If SQL.RecordCount = 0 Then
            MsgBox("No Rail Records Found")
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
    End Sub

    Private Sub cmdModify_Click(sender As Object, e As EventArgs) Handles cmdModify.Click
        Try
            'Save Updates to the database
            SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

            'Refresh DataGrid
            DGVData.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub
End Class