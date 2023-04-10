Public Class frmAdministration
    Private SQL As New SQLControl
    Dim UpdatePending As Boolean = False

    Private Sub frmAdministration_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        Try
            SQL.RunQuery("Select * From Control Order by UserName desc")
            If SQL.RecordCount = 0 Then
                MsgBox("No Records Found")
            Else
                DGVData.DataSource = SQL.SQLDataset.Tables(0)
                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Try
            If UpdatePending = True Then
                Dim result As Integer = MessageBox.Show("Changes have been made. Do you want to Save them?", "Warning", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                ElseIf result = DialogResult.Yes Then
                    'Save Updates to the database
                    SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation
                End If
            End If
            Me.Close()
        Catch ex As Exception
            AddLogEntry("Update Driver: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click

        Try
            DGVData.AllowUserToDeleteRows = True
            For Each row As DataGridViewRow In DGVData.SelectedRows
                DGVData.Rows.Remove(row)
            Next
            UpdatePending = True

            ''Save Updates to the database
            'SQL.SQLDA.Update(SQL.SQLDataset)

            ''Refresh DataGrid
            'DGVData.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        DGVData.AllowUserToAddRows = True
    End Sub

    Private Sub cmdModify_Click(sender As System.Object, e As System.EventArgs) Handles cmdModify.Click
        Try
            SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation
            DGVData.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class