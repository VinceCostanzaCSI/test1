<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderUploadMosaic
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
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.LoadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DGV1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.txtShipTo = New System.Windows.Forms.TextBox()
        Me.txtDocItem = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtDeliveryDate = New System.Windows.Forms.TextBox()
        Me.txtConsignee = New System.Windows.Forms.TextBox()
        Me.txtRelease = New System.Windows.Forms.TextBox()
        Me.CloseTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(138, 552)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(56, 20)
        Me.lblMsg.TabIndex = 19
        Me.lblMsg.Text = "Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(182, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(267, 25)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Mosiac Upload Information"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(123, 61)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(451, 69)
        Me.ListBox1.TabIndex = 17
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(528, 530)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(113, 42)
        Me.cmdExit.TabIndex = 16
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'LoadTimer
        '
        '
        'DGV1
        '
        Me.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV1.Location = New System.Drawing.Point(9, 157)
        Me.DGV1.Name = "DGV1"
        Me.DGV1.Size = New System.Drawing.Size(701, 147)
        Me.DGV1.TabIndex = 20
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtQuantity)
        Me.GroupBox2.Controls.Add(Me.txtShipTo)
        Me.GroupBox2.Controls.Add(Me.txtDocItem)
        Me.GroupBox2.Controls.Add(Me.txtProduct)
        Me.GroupBox2.Controls.Add(Me.txtDeliveryDate)
        Me.GroupBox2.Controls.Add(Me.txtConsignee)
        Me.GroupBox2.Controls.Add(Me.txtRelease)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 310)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(355, 222)
        Me.GroupBox2.TabIndex = 21
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
        'txtRelease
        '
        Me.txtRelease.Location = New System.Drawing.Point(33, 29)
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.Size = New System.Drawing.Size(133, 20)
        Me.txtRelease.TabIndex = 0
        '
        'CloseTimer
        '
        Me.CloseTimer.Interval = 5000
        '
        'frmOrderUploadMosaic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 598)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DGV1)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.cmdExit)
        Me.Name = "frmOrderUploadMosaic"
        Me.Text = "frmOrderUploadMosaic"
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents LoadTimer As System.Windows.Forms.Timer
    Friend WithEvents DGV1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtShipTo As System.Windows.Forms.TextBox
    Friend WithEvents txtDocItem As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtDeliveryDate As System.Windows.Forms.TextBox
    Friend WithEvents txtConsignee As System.Windows.Forms.TextBox
    Friend WithEvents txtRelease As System.Windows.Forms.TextBox
    Friend WithEvents CloseTimer As System.Windows.Forms.Timer
End Class
