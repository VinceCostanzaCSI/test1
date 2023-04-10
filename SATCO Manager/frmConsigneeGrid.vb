Public Class frmConsigneeGrid
    Private SQL As New SQLControl

    Private Sub frmConsigneeGrid_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        If FileControl = 0 Or FileControl = 1 Then
            cmdAdd.Visible = True
            DGVData.AllowUserToDeleteRows = False
            cmdModify.Visible = True
        End If
        If FileControl = 2 Then
            cmdAdd.Visible = True
            DGVData.AllowUserToDeleteRows = True
            cmdModify.Visible = True
        End If

        If SelectedConsignee = "" Then
            SQL.RunQuery("Select * From Consignee")
        Else
            SQL.RunQuery("Select * From Consignee where Code like '" & SelectedConsignee & "%'")
        End If

        If SQL.RecordCount = 0 Then
            MsgBox("No Records Found")
        Else
            Try
                lblTotal.Text = SQL.RecordCount
                DGVData.DataSource = SQL.SQLDataset.Tables(0)

                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub
    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdModify_Click(sender As System.Object, e As System.EventArgs) Handles cmdModify.Click
        'Save Updates to the database
        SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

        'Refresh DataGrid
        DGVData.Refresh()
        'cmdModify.Enabled = False
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs)
        DGVData.AllowUserToDeleteRows = True
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        DGVData.AllowUserToAddRows = True
    End Sub

    Private Sub cmdUpload_Click(sender As System.Object, e As System.EventArgs)

    End Sub


End Class