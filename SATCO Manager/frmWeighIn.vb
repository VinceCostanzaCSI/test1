Public Class frmWeighIn

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtGross_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtGross.TextChanged
        txtNet.Text = Val(txtGross.Text) - Val(txtTare.Text)
    End Sub

    Private Sub txtNet_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNet.TextChanged
        txtTons.Text = Val(txtNet.Text) / 2000
    End Sub

    Private Sub txtTare_Click(sender As Object, e As System.EventArgs) Handles txtTare.Click
        'EditActiveControlPad(Me)
    End Sub

    Private Sub txtTare_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTare.TextChanged
        txtNet.Text = Val(txtGross.Text) - Val(txtTare.Text)
    End Sub

    Private Sub txtVehicle_Click(sender As Object, e As System.EventArgs) Handles txtVehicle.Click
        'EditActiveControl(Me)
    End Sub

    Private Sub txtVehicle_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVehicle.TextChanged

    End Sub

    Private Sub txtTrailer_Click(sender As Object, e As System.EventArgs) Handles txtTrailer.Click
        'EditActiveControl(Me)
    End Sub

    Private Sub txtTrailer_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTrailer.TextChanged

    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        'First update record then print
        Dim WeighIn As clsWeighIn
        WeighIn = New clsWeighIn

        If txtVehicle.Text <> "" Then
            WeighIn.VehicleId = txtVehicle.Text & ""
            WeighIn.TrailerID = txtTrailer.Text & ""
            WeighIn.Gross = txtGross.Text & ""
            WeighIn.Tare = txtTare.Text & ""
            WeighIn.Net = txtNet.Text & ""
            WeighIn.DateTime = Now
            WeighIn.UpdateRecord(txtVehicle.Text)
            'Stop
            PrintWeighIn(txtVehicle.Text)
        Else
            MsgBox("Please enter a Vehicle ID and weight first")
        End If

        WeighIn = Nothing
    End Sub

    Private Sub frmWeighIn_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
    End Sub

End Class