Public Class frmCorrectionGrid
    Private SQL As New SQLControl

    Private Sub frmCorrectionGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CenterForm(Me)
            LoadDataGrid()

        Catch ex As Exception
            MsgBox("Load Form: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadDataGrid()

        Try
            SQL.RunQuery(Query)

            If SQL.RecordCount = 0 Then
                MsgBox("No Records Found")
            Else
                lblTotal.Text = SQL.RecordCount
                DGVData.DataSource = SQL.SQLDataset.Tables(0)
                'DGVData.Rows(0).Selected = True
                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand
            End If

        Catch ex As Exception
            If InStr(ex.Message, "key column") <> 0 Then
                'Do nothing
            Else
                MsgBox("LoadDataGrid: " & ex.Message)
            End If

        End Try

    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub
End Class