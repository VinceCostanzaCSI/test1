<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderUploadBrenntag
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPercent = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtMaxLoad = New System.Windows.Forms.TextBox()
        Me.txtCSZ = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtAltCode = New System.Windows.Forms.TextBox()
        Me.txtAltPO = New System.Windows.Forms.TextBox()
        Me.txtBOL = New System.Windows.Forms.TextBox()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.txtRelease = New System.Windows.Forms.TextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.txtShipTo = New System.Windows.Forms.TextBox()
        Me.txtDocItem = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtDeliveryDate = New System.Windows.Forms.TextBox()
        Me.txtConsignee = New System.Windows.Forms.TextBox()
        Me.txtMosaicRelease = New System.Windows.Forms.TextBox()
        Me.LoadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.CloseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.txtPercentName = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(40, 655)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(56, 20)
        Me.lblMsg.TabIndex = 15
        Me.lblMsg.Text = "Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(266, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(252, 25)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Order Upload Information"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(172, 57)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(451, 69)
        Me.ListBox1.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPercentName)
        Me.GroupBox1.Controls.Add(Me.txtPercent)
        Me.GroupBox1.Controls.Add(Me.txtNotes)
        Me.GroupBox1.Controls.Add(Me.txtMaxLoad)
        Me.GroupBox1.Controls.Add(Me.txtCSZ)
        Me.GroupBox1.Controls.Add(Me.txtAddress2)
        Me.GroupBox1.Controls.Add(Me.txtAddress1)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.txtAltCode)
        Me.GroupBox1.Controls.Add(Me.txtAltPO)
        Me.GroupBox1.Controls.Add(Me.txtBOL)
        Me.GroupBox1.Controls.Add(Me.txtPO)
        Me.GroupBox1.Controls.Add(Me.txtRelease)
        Me.GroupBox1.Location = New System.Drawing.Point(26, 158)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(355, 480)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Brenntag Fields"
        '
        'txtPercent
        '
        Me.txtPercent.Location = New System.Drawing.Point(33, 444)
        Me.txtPercent.MaxLength = 2
        Me.txtPercent.Name = "txtPercent"
        Me.txtPercent.Size = New System.Drawing.Size(37, 20)
        Me.txtPercent.TabIndex = 11
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(33, 289)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(302, 124)
        Me.txtNotes.TabIndex = 10
        '
        'txtMaxLoad
        '
        Me.txtMaxLoad.Location = New System.Drawing.Point(33, 263)
        Me.txtMaxLoad.Name = "txtMaxLoad"
        Me.txtMaxLoad.Size = New System.Drawing.Size(133, 20)
        Me.txtMaxLoad.TabIndex = 9
        '
        'txtCSZ
        '
        Me.txtCSZ.Location = New System.Drawing.Point(33, 237)
        Me.txtCSZ.Name = "txtCSZ"
        Me.txtCSZ.Size = New System.Drawing.Size(302, 20)
        Me.txtCSZ.TabIndex = 8
        '
        'txtAddress2
        '
        Me.txtAddress2.Location = New System.Drawing.Point(33, 211)
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.Size = New System.Drawing.Size(302, 20)
        Me.txtAddress2.TabIndex = 7
        '
        'txtAddress1
        '
        Me.txtAddress1.Location = New System.Drawing.Point(33, 185)
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.Size = New System.Drawing.Size(302, 20)
        Me.txtAddress1.TabIndex = 6
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(33, 159)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(302, 20)
        Me.txtName.TabIndex = 5
        '
        'txtAltCode
        '
        Me.txtAltCode.Location = New System.Drawing.Point(33, 133)
        Me.txtAltCode.Name = "txtAltCode"
        Me.txtAltCode.Size = New System.Drawing.Size(133, 20)
        Me.txtAltCode.TabIndex = 4
        '
        'txtAltPO
        '
        Me.txtAltPO.Location = New System.Drawing.Point(33, 107)
        Me.txtAltPO.Name = "txtAltPO"
        Me.txtAltPO.Size = New System.Drawing.Size(133, 20)
        Me.txtAltPO.TabIndex = 3
        '
        'txtBOL
        '
        Me.txtBOL.Location = New System.Drawing.Point(33, 81)
        Me.txtBOL.Name = "txtBOL"
        Me.txtBOL.Size = New System.Drawing.Size(133, 20)
        Me.txtBOL.TabIndex = 2
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(33, 55)
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(133, 20)
        Me.txtPO.TabIndex = 1
        '
        'txtRelease
        '
        Me.txtRelease.Location = New System.Drawing.Point(33, 29)
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.Size = New System.Drawing.Size(133, 20)
        Me.txtRelease.TabIndex = 0
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(663, 633)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(113, 42)
        Me.cmdExit.TabIndex = 11
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtQuantity)
        Me.GroupBox2.Controls.Add(Me.txtShipTo)
        Me.GroupBox2.Controls.Add(Me.txtDocItem)
        Me.GroupBox2.Controls.Add(Me.txtProduct)
        Me.GroupBox2.Controls.Add(Me.txtDeliveryDate)
        Me.GroupBox2.Controls.Add(Me.txtConsignee)
        Me.GroupBox2.Controls.Add(Me.txtMosaicRelease)
        Me.GroupBox2.Location = New System.Drawing.Point(411, 167)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(355, 222)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Mosaic Fields"
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(33, 185)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(133, 20)
        Me.txtQuantity.TabIndex = 6
        '
        'txtShipTo
        '
        Me.txtShipTo.Location = New System.Drawing.Point(33, 159)
        Me.txtShipTo.Name = "txtShipTo"
        Me.txtShipTo.Size = New System.Drawing.Size(302, 20)
        Me.txtShipTo.TabIndex = 5
        '
        'txtDocItem
        '
        Me.txtDocItem.Location = New System.Drawing.Point(33, 133)
        Me.txtDocItem.Name = "txtDocItem"
        Me.txtDocItem.Size = New System.Drawing.Size(133, 20)
        Me.txtDocItem.TabIndex = 4
        '
        'txtProduct
        '
        Me.txtProduct.Location = New System.Drawing.Point(33, 107)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(133, 20)
        Me.txtProduct.TabIndex = 3
        '
        'txtDeliveryDate
        '
        Me.txtDeliveryDate.Location = New System.Drawing.Point(33, 81)
        Me.txtDeliveryDate.Name = "txtDeliveryDate"
        Me.txtDeliveryDate.Size = New System.Drawing.Size(133, 20)
        Me.txtDeliveryDate.TabIndex = 2
        '
        'txtConsignee
        '
        Me.txtConsignee.Location = New System.Drawing.Point(33, 55)
        Me.txtConsignee.Name = "txtConsignee"
        Me.txtConsignee.Size = New System.Drawing.Size(293, 20)
        Me.txtConsignee.TabIndex = 1
        '
        'txtMosaicRelease
        '
        Me.txtMosaicRelease.Location = New System.Drawing.Point(33, 29)
        Me.txtMosaicRelease.Name = "txtMosaicRelease"
        Me.txtMosaicRelease.Size = New System.Drawing.Size(133, 20)
        Me.txtMosaicRelease.TabIndex = 0
        '
        'LoadTimer
        '
        '
        'OFD
        '
        Me.OFD.FileName = "OpenFileDialog1"
        '
        'CloseTimer
        '
        Me.CloseTimer.Interval = 5000
        '
        'txtPercentName
        '
        Me.txtPercentName.Location = New System.Drawing.Point(33, 419)
        Me.txtPercentName.MaxLength = 20
        Me.txtPercentName.Name = "txtPercentName"
        Me.txtPercentName.Size = New System.Drawing.Size(302, 20)
        Me.txtPercentName.TabIndex = 12
        '
        'frmOrderUploadBrenntag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 699)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdExit)
        Me.Name = "frmOrderUploadBrenntag"
        Me.Text = "frmOrderUpload"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxLoad As System.Windows.Forms.TextBox
    Friend WithEvents txtCSZ As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtAltCode As System.Windows.Forms.TextBox
    Friend WithEvents txtAltPO As System.Windows.Forms.TextBox
    Friend WithEvents txtBOL As System.Windows.Forms.TextBox
    Friend WithEvents txtPO As System.Windows.Forms.TextBox
    Friend WithEvents txtRelease As System.Windows.Forms.TextBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtShipTo As System.Windows.Forms.TextBox
    Friend WithEvents txtDocItem As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliveryDate As System.Windows.Forms.TextBox
    Friend WithEvents txtConsignee As System.Windows.Forms.TextBox
    Friend WithEvents txtMosaicRelease As System.Windows.Forms.TextBox
    Friend WithEvents LoadTimer As System.Windows.Forms.Timer
    Friend WithEvents OFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CloseTimer As System.Windows.Forms.Timer
    Friend WithEvents txtPercent As TextBox
    Friend WithEvents txtPercentName As TextBox
End Class
