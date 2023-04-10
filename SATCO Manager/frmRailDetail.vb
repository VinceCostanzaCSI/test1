Public Class frmRailDetail
    Private SQL As New SQLControl

    Private Sub frmRailDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterForm(Me)
        If FileControl = 0 Then
            cmdAdd.Visible = False
            cmdDelete.Visible = False
            cmdModify.Visible = False
        End If
        If FileControl = 1 Then
            cmdAdd.Visible = False
            cmdDelete.Visible = False
            cmdModify.Visible = False
        End If
        If FileControl = 2 Then
            cmdAdd.Visible = False
            cmdDelete.Visible = False
            cmdModify.Visible = True
        End If

        SQL.RunQuery("Select * From Rail where Code = '" & NewPrinterCode & "' and Id = '" & NewPrinterID & "' Order by Number asc")
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

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        'DGVData.AllowUserToDeleteRows = True
        Try
            For Each row As DataGridViewRow In DGVData.SelectedRows
                DGVData.Rows.Remove(row)
            Next
            'Save Updates to the database
            SQL.SQLDA.Update(SQL.SQLDataset)

            'Refresh DataGrid
            DGVData.Refresh()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        DGVData.AllowUserToAddRows = True
    End Sub

    Private Sub cmdModify_Click(sender As System.Object, e As System.EventArgs) Handles cmdModify.Click
        Try
            'Save Updates to the database
            SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

            'Refresh DataGrid
            DGVData.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        'NewPrinterCode = DGVData.Rows(SelectedRow).Cells(0).Value
        'NewPrinterID = DGVData.Rows(SelectedRow).Cells(1).Value

        PrintRailTicket()

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


End Class