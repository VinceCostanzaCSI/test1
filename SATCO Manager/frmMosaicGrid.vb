Public Class frmMosaicGrid
    Private SQL As New SQLControl
    Private Sub frmMosaicGrid_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        cmdAdd.Visible = True
        cmdModify.Visible = True

        SQL.RunQuery("Select * From Mosaic")
        If SQL.RecordCount = 0 Then
            MsgBox("No Records Found")
        Else
            Try
                lblTotal.Text = SQL.RecordCount
                DGVData.DataSource = SQL.SQLDataset.Tables(0)

                'DGVData.Rows(0).Selected = True

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
        'Save Updates to the database
        Try
            SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

            'Refresh DataGrid
            DGVData.Refresh()
            'cmdModify.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs)
        DGVData.AllowUserToDeleteRows = True
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        DGVData.AllowUserToAddRows = True
    End Sub

    Private Sub cmdActive_Click(sender As System.Object, e As System.EventArgs) Handles cmdActive.Click
        SQL.RunQuery("Select * From Mosaic where Active = '1'")
        If SQL.RecordCount = 0 Then
            MsgBox("No Records Found")
        Else
            Try
                lblTotal.Text = SQL.RecordCount
                DGVData.DataSource = SQL.SQLDataset.Tables(0)

                'DGVData.Rows(0).Selected = True

                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If

    End Sub

End Class