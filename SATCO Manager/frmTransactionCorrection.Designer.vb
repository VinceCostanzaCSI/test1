<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransactionCorrection
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
        Me.Status = New System.Windows.Forms.TextBox()
        Me.NetBox = New System.Windows.Forms.TextBox()
        Me.TareBox = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.GrossBox = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtScaleTicket = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtScaleNumber = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtReleaseNumber = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtAdjustment = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtAnalysis = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtTank = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtTankLevel = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboTank = New System.Windows.Forms.ComboBox()
        Me.cboCommodity = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtTrailer = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtTargetWt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.cboCode = New System.Windows.Forms.ComboBox()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.txtConsignee = New System.Windows.Forms.TextBox()
        Me.lblNSF = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboDriverId = New System.Windows.Forms.ComboBox()
        Me.txtCarrier = New System.Windows.Forms.TextBox()
        Me.txtDriverName = New System.Windows.Forms.TextBox()
        Me.txtCardId = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdReprint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtOutTime = New System.Windows.Forms.TextBox()
        Me.txtInTime = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.LoadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.txtSeal1 = New System.Windows.Forms.TextBox()
        Me.txtSeal2 = New System.Windows.Forms.TextBox()
        Me.txtSeal3 = New System.Windows.Forms.TextBox()
        Me.txtSeal4 = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblTankInfo = New System.Windows.Forms.Label()
        Me.lblInfoSA = New System.Windows.Forms.Label()
        Me.txtUsed = New System.Windows.Forms.TextBox()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Status
        '
        Me.Status.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status.Location = New System.Drawing.Point(374, 665)
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(399, 26)
        Me.Status.TabIndex = 85
        '
        'NetBox
        '
        Me.NetBox.Enabled = False
        Me.NetBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NetBox.Location = New System.Drawing.Point(135, 84)
        Me.NetBox.Name = "NetBox"
        Me.NetBox.Size = New System.Drawing.Size(88, 26)
        Me.NetBox.TabIndex = 84
        '
        'TareBox
        '
        Me.TareBox.Enabled = False
        Me.TareBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TareBox.Location = New System.Drawing.Point(135, 52)
        Me.TareBox.Name = "TareBox"
        Me.TareBox.Size = New System.Drawing.Size(88, 26)
        Me.TareBox.TabIndex = 83
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(6, 84)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(103, 20)
        Me.Label26.TabIndex = 82
        Me.Label26.Text = "Net Weight:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(6, 52)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(111, 20)
        Me.Label25.TabIndex = 81
        Me.Label25.Text = "Tare Weight:"
        '
        'GrossBox
        '
        Me.GrossBox.Enabled = False
        Me.GrossBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrossBox.Location = New System.Drawing.Point(135, 20)
        Me.GrossBox.Name = "GrossBox"
        Me.GrossBox.Size = New System.Drawing.Size(88, 26)
        Me.GrossBox.TabIndex = 80
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 20)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(123, 20)
        Me.Label24.TabIndex = 79
        Me.Label24.Text = "Gross Weight:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtScaleTicket)
        Me.GroupBox5.Controls.Add(Me.Label30)
        Me.GroupBox5.Controls.Add(Me.txtScaleNumber)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.txtReleaseNumber)
        Me.GroupBox5.Controls.Add(Me.Label28)
        Me.GroupBox5.Controls.Add(Me.txtAdjustment)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(31, 508)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(420, 151)
        Me.GroupBox5.TabIndex = 76
        Me.GroupBox5.TabStop = False
        '
        'txtScaleTicket
        '
        Me.txtScaleTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScaleTicket.Location = New System.Drawing.Point(211, 110)
        Me.txtScaleTicket.Name = "txtScaleTicket"
        Me.txtScaleTicket.Size = New System.Drawing.Size(60, 26)
        Me.txtScaleTicket.TabIndex = 101
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(35, 113)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(170, 20)
        Me.Label30.TabIndex = 100
        Me.Label30.Text = "Ticket Number (CA):"
        '
        'txtScaleNumber
        '
        Me.txtScaleNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScaleNumber.Location = New System.Drawing.Point(188, 46)
        Me.txtScaleNumber.Name = "txtScaleNumber"
        Me.txtScaleNumber.Size = New System.Drawing.Size(40, 26)
        Me.txtScaleNumber.TabIndex = 99
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(35, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 20)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Scale Number:"
        '
        'txtReleaseNumber
        '
        Me.txtReleaseNumber.Location = New System.Drawing.Point(188, 14)
        Me.txtReleaseNumber.Name = "txtReleaseNumber"
        Me.txtReleaseNumber.Size = New System.Drawing.Size(143, 26)
        Me.txtReleaseNumber.TabIndex = 17
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(35, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(147, 20)
        Me.Label28.TabIndex = 15
        Me.Label28.Text = "Release Number:"
        '
        'txtAdjustment
        '
        Me.txtAdjustment.Location = New System.Drawing.Point(146, 78)
        Me.txtAdjustment.Name = "txtAdjustment"
        Me.txtAdjustment.Size = New System.Drawing.Size(268, 26)
        Me.txtAdjustment.TabIndex = 19
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(35, 81)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(105, 20)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Adjustment:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtAnalysis)
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.txtTank)
        Me.GroupBox4.Controls.Add(Me.txtDescription)
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Controls.Add(Me.txtTankLevel)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cboTank)
        Me.GroupBox4.Controls.Add(Me.cboCommodity)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(31, 377)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(829, 114)
        Me.GroupBox4.TabIndex = 75
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Commodity and Tank"
        '
        'txtAnalysis
        '
        Me.txtAnalysis.Location = New System.Drawing.Point(635, 64)
        Me.txtAnalysis.Name = "txtAnalysis"
        Me.txtAnalysis.Size = New System.Drawing.Size(107, 26)
        Me.txtAnalysis.TabIndex = 32
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(531, 67)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(80, 20)
        Me.Label29.TabIndex = 31
        Me.Label29.Text = "Analysis:"
        '
        'txtTank
        '
        Me.txtTank.Location = New System.Drawing.Point(198, 70)
        Me.txtTank.Name = "txtTank"
        Me.txtTank.Size = New System.Drawing.Size(40, 26)
        Me.txtTank.TabIndex = 30
        Me.txtTank.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.Enabled = False
        Me.txtDescription.Location = New System.Drawing.Point(371, 23)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(371, 26)
        Me.txtDescription.TabIndex = 29
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(260, 26)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(105, 20)
        Me.Label27.TabIndex = 28
        Me.Label27.Text = "Description:"
        '
        'txtTankLevel
        '
        Me.txtTankLevel.Location = New System.Drawing.Point(371, 67)
        Me.txtTankLevel.Name = "txtTankLevel"
        Me.txtTankLevel.Size = New System.Drawing.Size(107, 26)
        Me.txtTankLevel.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(267, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 20)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Level:"
        '
        'cboTank
        '
        Me.cboTank.FormattingEnabled = True
        Me.cboTank.Location = New System.Drawing.Point(108, 67)
        Me.cboTank.Name = "cboTank"
        Me.cboTank.Size = New System.Drawing.Size(84, 28)
        Me.cboTank.TabIndex = 23
        '
        'cboCommodity
        '
        Me.cboCommodity.Enabled = False
        Me.cboCommodity.FormattingEnabled = True
        Me.cboCommodity.Location = New System.Drawing.Point(108, 23)
        Me.cboCommodity.Name = "cboCommodity"
        Me.cboCommodity.Size = New System.Drawing.Size(107, 28)
        Me.cboCommodity.TabIndex = 22
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(35, 70)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 20)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Tank:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(35, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(33, 20)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "ID:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtTrailer)
        Me.GroupBox3.Controls.Add(Me.txtVehicle)
        Me.GroupBox3.Controls.Add(Me.txtTargetWt)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(31, 325)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(829, 46)
        Me.GroupBox3.TabIndex = 78
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Vehicle"
        '
        'txtTrailer
        '
        Me.txtTrailer.Location = New System.Drawing.Point(681, 14)
        Me.txtTrailer.Name = "txtTrailer"
        Me.txtTrailer.Size = New System.Drawing.Size(115, 26)
        Me.txtTrailer.TabIndex = 15
        '
        'txtVehicle
        '
        Me.txtVehicle.Location = New System.Drawing.Point(474, 13)
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(106, 26)
        Me.txtVehicle.TabIndex = 14
        '
        'txtTargetWt
        '
        Me.txtTargetWt.Location = New System.Drawing.Point(136, 13)
        Me.txtTargetWt.Name = "txtTargetWt"
        Me.txtTargetWt.Size = New System.Drawing.Size(79, 26)
        Me.txtTargetWt.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(615, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 20)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Trailer:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(387, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 20)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Vehicle:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(35, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(93, 20)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Target Wt:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtID)
        Me.GroupBox2.Controls.Add(Me.cboCode)
        Me.GroupBox2.Controls.Add(Me.txtDestination)
        Me.GroupBox2.Controls.Add(Me.txtConsignee)
        Me.GroupBox2.Controls.Add(Me.lblNSF)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(31, 198)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(829, 112)
        Me.GroupBox2.TabIndex = 77
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Consignee"
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(249, 28)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(81, 26)
        Me.txtID.TabIndex = 12
        '
        'cboCode
        '
        Me.cboCode.FormattingEnabled = True
        Me.cboCode.Location = New System.Drawing.Point(108, 28)
        Me.cboCode.Name = "cboCode"
        Me.cboCode.Size = New System.Drawing.Size(129, 28)
        Me.cboCode.TabIndex = 11
        '
        'txtDestination
        '
        Me.txtDestination.Location = New System.Drawing.Point(405, 64)
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.ReadOnly = True
        Me.txtDestination.Size = New System.Drawing.Size(391, 26)
        Me.txtDestination.TabIndex = 10
        '
        'txtConsignee
        '
        Me.txtConsignee.Location = New System.Drawing.Point(405, 30)
        Me.txtConsignee.Name = "txtConsignee"
        Me.txtConsignee.ReadOnly = True
        Me.txtConsignee.Size = New System.Drawing.Size(391, 26)
        Me.txtConsignee.TabIndex = 8
        '
        'lblNSF
        '
        Me.lblNSF.AutoSize = True
        Me.lblNSF.Location = New System.Drawing.Point(104, 70)
        Me.lblNSF.Name = "lblNSF"
        Me.lblNSF.Size = New System.Drawing.Size(44, 20)
        Me.lblNSF.TabIndex = 7
        Me.lblNSF.Text = "NSF"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(295, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 20)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Destination:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(339, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 20)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Name:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 20)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Code:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboDriverId)
        Me.GroupBox1.Controls.Add(Me.txtCarrier)
        Me.GroupBox1.Controls.Add(Me.txtDriverName)
        Me.GroupBox1.Controls.Add(Me.txtCardId)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(31, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(829, 110)
        Me.GroupBox1.TabIndex = 74
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Driver"
        '
        'cboDriverId
        '
        Me.cboDriverId.FormattingEnabled = True
        Me.cboDriverId.Location = New System.Drawing.Point(108, 62)
        Me.cboDriverId.Name = "cboDriverId"
        Me.cboDriverId.Size = New System.Drawing.Size(129, 28)
        Me.cboDriverId.TabIndex = 23
        '
        'txtCarrier
        '
        Me.txtCarrier.Location = New System.Drawing.Point(368, 62)
        Me.txtCarrier.Name = "txtCarrier"
        Me.txtCarrier.ReadOnly = True
        Me.txtCarrier.Size = New System.Drawing.Size(428, 26)
        Me.txtCarrier.TabIndex = 7
        '
        'txtDriverName
        '
        Me.txtDriverName.Location = New System.Drawing.Point(368, 22)
        Me.txtDriverName.Name = "txtDriverName"
        Me.txtDriverName.ReadOnly = True
        Me.txtDriverName.Size = New System.Drawing.Size(428, 26)
        Me.txtDriverName.TabIndex = 6
        '
        'txtCardId
        '
        Me.txtCardId.Location = New System.Drawing.Point(108, 22)
        Me.txtCardId.Name = "txtCardId"
        Me.txtCardId.ReadOnly = True
        Me.txtCardId.Size = New System.Drawing.Size(129, 26)
        Me.txtCardId.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(295, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 20)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Carrier:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(295, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Card #:"
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(932, 231)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(85, 50)
        Me.cmdSave.TabIndex = 88
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdReprint
        '
        Me.cmdReprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReprint.Location = New System.Drawing.Point(932, 327)
        Me.cmdReprint.Name = "cmdReprint"
        Me.cmdReprint.Size = New System.Drawing.Size(85, 50)
        Me.cmdReprint.TabIndex = 89
        Me.cmdReprint.Text = "Reprint"
        Me.cmdReprint.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(932, 432)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(85, 50)
        Me.cmdExit.TabIndex = 90
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(288, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(365, 37)
        Me.Label1.TabIndex = 91
        Me.Label1.Text = "Transaction Correction"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.NetBox)
        Me.GroupBox6.Controls.Add(Me.TareBox)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.GrossBox)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Location = New System.Drawing.Point(712, 508)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(238, 127)
        Me.GroupBox6.TabIndex = 92
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Weight"
        '
        'txtOutTime
        '
        Me.txtOutTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutTime.Location = New System.Drawing.Point(994, 91)
        Me.txtOutTime.Name = "txtOutTime"
        Me.txtOutTime.Size = New System.Drawing.Size(88, 26)
        Me.txtOutTime.TabIndex = 98
        '
        'txtInTime
        '
        Me.txtInTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInTime.Location = New System.Drawing.Point(994, 60)
        Me.txtInTime.Name = "txtInTime"
        Me.txtInTime.Size = New System.Drawing.Size(88, 26)
        Me.txtInTime.TabIndex = 97
        '
        'txtDate
        '
        Me.txtDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Location = New System.Drawing.Point(873, 28)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(209, 26)
        Me.txtDate.TabIndex = 96
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(869, 91)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(102, 24)
        Me.Label23.TabIndex = 95
        Me.Label23.Text = "Time Out:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(869, 60)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(86, 24)
        Me.Label22.TabIndex = 94
        Me.Label22.Text = "Time In:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(814, 31)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(58, 24)
        Me.Label21.TabIndex = 93
        Me.Label21.Text = "Date:"
        '
        'LoadTimer
        '
        '
        'txtSeal1
        '
        Me.txtSeal1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeal1.Location = New System.Drawing.Point(77, 17)
        Me.txtSeal1.Name = "txtSeal1"
        Me.txtSeal1.Size = New System.Drawing.Size(155, 26)
        Me.txtSeal1.TabIndex = 28
        '
        'txtSeal2
        '
        Me.txtSeal2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeal2.Location = New System.Drawing.Point(77, 49)
        Me.txtSeal2.Name = "txtSeal2"
        Me.txtSeal2.Size = New System.Drawing.Size(155, 26)
        Me.txtSeal2.TabIndex = 29
        '
        'txtSeal3
        '
        Me.txtSeal3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeal3.Location = New System.Drawing.Point(77, 81)
        Me.txtSeal3.Name = "txtSeal3"
        Me.txtSeal3.Size = New System.Drawing.Size(155, 26)
        Me.txtSeal3.TabIndex = 30
        '
        'txtSeal4
        '
        Me.txtSeal4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeal4.Location = New System.Drawing.Point(77, 113)
        Me.txtSeal4.Name = "txtSeal4"
        Me.txtSeal4.Size = New System.Drawing.Size(155, 26)
        Me.txtSeal4.TabIndex = 31
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtSeal4)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.txtSeal3)
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.txtSeal2)
        Me.GroupBox7.Controls.Add(Me.Label18)
        Me.GroupBox7.Controls.Add(Me.txtSeal1)
        Me.GroupBox7.Controls.Add(Me.Label19)
        Me.GroupBox7.Location = New System.Drawing.Point(457, 508)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(238, 151)
        Me.GroupBox7.TabIndex = 99
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Seals"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(7, 116)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 20)
        Me.Label20.TabIndex = 85
        Me.Label20.Text = "Seal 4:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 20)
        Me.Label11.TabIndex = 82
        Me.Label11.Text = "Seal 3:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 52)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 20)
        Me.Label18.TabIndex = 81
        Me.Label18.Text = "Seal 2:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 20)
        Me.Label19.TabIndex = 79
        Me.Label19.Text = "Seal 1:"
        '
        'lblTankInfo
        '
        Me.lblTankInfo.AutoSize = True
        Me.lblTankInfo.Location = New System.Drawing.Point(136, 492)
        Me.lblTankInfo.Name = "lblTankInfo"
        Me.lblTankInfo.Size = New System.Drawing.Size(91, 13)
        Me.lblTankInfo.TabIndex = 100
        Me.lblTankInfo.Text = "Tank Update Info"
        '
        'lblInfoSA
        '
        Me.lblInfoSA.AutoSize = True
        Me.lblInfoSA.Location = New System.Drawing.Point(136, 313)
        Me.lblInfoSA.Name = "lblInfoSA"
        Me.lblInfoSA.Size = New System.Drawing.Size(80, 13)
        Me.lblInfoSA.TabIndex = 101
        Me.lblInfoSA.Text = "SA Update Info"
        '
        'txtUsed
        '
        Me.txtUsed.Location = New System.Drawing.Point(113, 666)
        Me.txtUsed.Name = "txtUsed"
        Me.txtUsed.Size = New System.Drawing.Size(100, 20)
        Me.txtUsed.TabIndex = 102
        '
        'frmTransactionCorrection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1109, 698)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtUsed)
        Me.Controls.Add(Me.lblInfoSA)
        Me.Controls.Add(Me.lblTankInfo)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.txtOutTime)
        Me.Controls.Add(Me.txtInTime)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdReprint)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmTransactionCorrection"
        Me.Text = "frmTransactionCorrection"
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents NetBox As System.Windows.Forms.TextBox
    Friend WithEvents TareBox As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GrossBox As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtReleaseNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cboTank As System.Windows.Forms.ComboBox
    Friend WithEvents cboCommodity As System.Windows.Forms.ComboBox
    Friend WithEvents txtAdjustment As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTrailer As System.Windows.Forms.TextBox
    Friend WithEvents txtVehicle As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetWt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboCode As System.Windows.Forms.ComboBox
    Friend WithEvents txtDestination As System.Windows.Forms.TextBox
    Friend WithEvents txtConsignee As System.Windows.Forms.TextBox
    Friend WithEvents lblNSF As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboDriverId As System.Windows.Forms.ComboBox
    Friend WithEvents txtCarrier As System.Windows.Forms.TextBox
    Friend WithEvents txtDriverName As System.Windows.Forms.TextBox
    Friend WithEvents txtCardId As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdReprint As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOutTime As System.Windows.Forms.TextBox
    Friend WithEvents txtInTime As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents LoadTimer As System.Windows.Forms.Timer
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtTankLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtScaleNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSeal1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSeal2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSeal3 As System.Windows.Forms.TextBox
    Friend WithEvents txtSeal4 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblTankInfo As System.Windows.Forms.Label
    Friend WithEvents lblInfoSA As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtTank As System.Windows.Forms.TextBox
    Friend WithEvents txtUsed As System.Windows.Forms.TextBox
    Friend WithEvents txtAnalysis As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txtScaleTicket As TextBox
    Friend WithEvents Label30 As Label
End Class
