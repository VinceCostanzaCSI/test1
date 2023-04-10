Public Class frmSplash

    Private Sub GroupBox1_Click(sender As Object, e As System.EventArgs) Handles GroupBox1.Click
        Me.Close()
    End Sub

    Private Sub TextBox3_Click(sender As Object, e As System.EventArgs) Handles txtProductName.Click
        Me.Close()
    End Sub

    Private Sub frmSplash_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            txtVersion.Text = "Version " & Version
            'txtVersion.Text = "Version " & FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion
            txtProductName.Text = Application.ProductName
            DataPathInit()
            If DefaultLocation = "T" Then
                DBPath = DBPath1
            Else
                DBPath = DBPath2
            End If
            CenterForm(Me)
            LoadTimer.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        frmMain.Show()
        Me.Close()
    End Sub

    Private Sub LoadTimer_Tick(sender As Object, e As EventArgs) Handles LoadTimer.Tick
        frmMain.Show()
        Me.Close()
    End Sub

    Private Sub txtProductName_TextChanged(sender As Object, e As EventArgs) Handles txtProductName.TextChanged

    End Sub
End Class