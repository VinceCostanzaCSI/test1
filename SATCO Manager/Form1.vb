Imports System.Net.Sockets
Imports System.Text
Imports System.IO
Imports System.Deployment.Application
Imports OpenPop.Pop3
Imports OpenPop.Mime
Imports System.Net

Public Class frmMain

    Private SQL As New SQLControl
    Dim CurrentRecord As Integer = 0
    Dim Gate1TCPClientz As TcpClient
    Dim Gate1TCPClientStream As NetworkStream
    Dim Gate2TCPClientz As TcpClient
    Dim Gate2TCPClientStream As NetworkStream
    Dim TCPClient2 As TcpClient
    Dim TCPClientStream2 As NetworkStream
    Dim TCPControl As String
    Dim GateActive As Boolean
    Dim GateCardId As String
    Dim Flag1 As Boolean
    Dim Card1 As String
    Dim CardStart As Integer
    Dim CardLength As Integer
    Dim GateRead As String
    Dim GateControl As String
    Dim ProcessingOrders As Boolean
    Dim Gate1ComFlag As Boolean = False
    Dim Gate2ComFlag As Boolean = False
    Dim IPRelayFlag As Boolean = False
    Dim Gate1IP As String
    Dim Gate2IP As String
    Dim RelayIP As String


    '***** This area for detection of USB driver insertion ******
    Private WM_DEVICECHANGE As Integer = &H219

    Public Enum WM_DEVICECHANGE_WPPARAMS As Integer
        DBT_CONFIGCHANGECANCELED = &H19
        DBT_CONFIGCHANGED = &H18
        DBT_CUSTOMEVENT = &H8006
        DBT_DEVICEARRIVAL = &H8000
        DBT_DEVICEQUERYREMOVE = &H8001
        DBT_DEVICEQUERYREMOVEFAILED = &H8002
        DBT_DEVICEREMOVECOMPLETE = &H8004
        DBT_DEVICEREMOVEPENDING = &H8003
        DBT_DEVICETYPESPECIFIC = &H8005
        DBT_DEVNODES_CHANGED = &H7
        DBT_QUERYCHANGECONFIG = &H17
        DBT_USERDEFINED = &HFFFF
    End Enum

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_DEVICECHANGE Then
            Select Case m.WParam
                Case WM_DEVICECHANGE_WPPARAMS.DBT_DEVICEARRIVAL
                    If ControlCenter = True Then
                        Call ProcessUSB()
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub
    Private Sub ProcessUSB()
        Try
            For Each ThisDrive As System.IO.DriveInfo In My.Computer.FileSystem.Drives
                If ThisDrive.DriveType.ToString = "Removable" Then
                    'Got the Drive... now check for your file and copy it
                    txtStatus.Text = "Found USB Drive: " & ThisDrive.Name
                    AddLogEntry("Found USB Drive: " & ThisDrive.Name)
                    'AddOrderEntry("Found USB Drive: " & ThisDrive.Name)
                    USBDrive = ThisDrive.Name
                    If MsgBox("Thumbdrive detected." & vbCrLf & "Do you wish to process files ?", vbYesNo, "Thumbdrive detected") = vbYes Then
                        'Copy files to watch folder
                        'Create the target directory if necessary
                        Dim toPath = WatchPath1
                        Dim fromPath = USBDrive
                        Dim toPathInfo = New DirectoryInfo(toPath)
                        If (Not toPathInfo.Exists) Then
                            toPathInfo.Create()
                        End If
                        Dim fromPathInfo = New DirectoryInfo(fromPath)
                        'copy all files 
                        For Each file As FileInfo In fromPathInfo.GetFiles()
                            file.CopyTo(Path.Combine(toPath, file.Name), True)
                        Next
                        AddLogEntry("Thumbdrive detected: Transferring files to Watch folder")
                        'AddOrderEntry("Thumbdrive detected: Transferring files to Watch folder")
                    Else
                        AddLogEntry("Thumbdrive detected: Transfer cancelled")
                        'AddOrderEntry("Thumbdrive detected: Transfer cancelled")
                    End If

                End If
            Next
        Catch ex As Exception
            AddLogEntry(ex.Message)
            AddOrderEntry(ex.Message)
        End Try
    End Sub
    '********** End of section for USB Thumbdrive detection and processing ********

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuExit.Click
        AddLogEntry(OperatorName & " Successfully logged out ")
        'If ControlCenter = True Then
        '    fsw1.Path = WatchPath
        '    fsw1.EnableRaisingEvents = True
        'Else
        '    fsw1.EnableRaisingEvents = False
        'End If
        Login()

    End Sub

    Public WithEvents NonActivityTimer As New Timer With {.Interval = 600000} '10 minutes

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        ControlCenter = False       'If True then this app will take orders, read gate, have database locally installed
        LoadTimer.Enabled = True

    End Sub


    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick

        Try
            LoadTimer.Enabled = False
            'Path Initilization
            Dim SysOptions = New clsSystem
            'DBPath = SysOptions.DatabasePath
            SharePath = SysOptions.DocumentPath
            RptPath = SysOptions.ReportPath
            WatchPath1 = SysOptions.WatchPath1
            WatchPath2 = SysOptions.WatchPath2
            WatchPath3 = SysOptions.WatchPath3

            optBrenntag.Checked = True

            AddLogEntry("*** Satco Manager Application Starting ****")
            AddOrderEntry("*** Satco Manager Application Starting ****")

            Dim dt As String = Format(Now, "MM-dd-yyyy")
            StartDatePicker.Format = DateTimePickerFormat.Custom
            StartDatePicker.CustomFormat = "MM-dd-yyyy"
            EndDatePicker.Format = DateTimePickerFormat.Custom
            EndDatePicker.CustomFormat = "MM-dd-yyyy"

            StartDatePicker.Text = Mid(dt, 1, 10)
            EndDatePicker.Text = dt
            txtStartTime.Text = "00:00:00"
            txtEndTime.Text = "23:59:59"

            If SQL.HasConnection = True Then
                txtStatus.Text = "Connection Successful to SQL Database"
                AddLogEntry("Connection Successful to SQL Database")
                'Else
                '    txtStatus.Text = "Connection to SQL Database failed - attempting Backup"
                '    If SQL.HasConnection2 = True Then
                '        txtStatus.Text = "Connection Successful to Backup SQL Database"
                '    Else
                '        txtStatus.Text = "Connection to Backup and Primary SQL Database failed - Contact SATCO Representative"
                '        Me.Close()
                '    End If
            End If
            lblVersion.Text = "Version " & Version

            'See if Network is available
            If NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = False Then
                txtStatus.Text = "No Network Connection"
                AddLogEntry("No Network Connection")
            End If

            optSA.Checked = True

            LoadCombos()

            If SysOptions.Control = 1 Then
                ControlCenter = True

                fsw1.Path = WatchPath1
                fsw1.EnableRaisingEvents = True

                If WatchPath2 <> "" Then
                    fsw2.Path = WatchPath2
                    fsw2.EnableRaisingEvents = True
                End If

                If WatchPath3 <> "" Then
                    fsw3.Path = WatchPath3
                    fsw3.EnableRaisingEvents = True
                End If

                If DefaultLocation = "T" Then
                    optTampa.Checked = True
                    txtControlCenter.Text = "Tampa Control Center"
                Else
                    optStockton.Checked = True
                    txtControlCenter.Text = "Stockton Control Center"
                    cmdOpenGate2.Visible = True
                    lblGate2Status.Visible = True
                    'Turn off status for loading on 2,3,4
                    lblLoading1.Visible = False
                    lblLoading2.Visible = False
                    lblLoading3.Visible = False
                    lblStatus1.Visible = False
                    lblStatus2.Visible = False
                    lblStatus3.Visible = False
                    lblTank1.Visible = False
                    lblTank2.Visible = False
                    lblTank3.Visible = False
                    lblScale1.Visible = False
                    lblScale2.Visible = False
                    lblScale3.Visible = False
                    'Menu Option views
                    mnuReportBrenntag.Visible = False
                    MosaicToolStripMenuItem.Visible = False
                    MosaicToolStripMenuItem1.Visible = False
                    UCToolStripMenuItem.Visible = False
                    mnuReportMosaic.Visible = False
                    SAMosaicToolStripMenuItem.Visible = False
                    mnuReportUC.Visible = False
                    mnuOrderBrenntag.Visible = False
                    mnuOrderMosaic.Visible = False
                    AllConsigneesToolStripMenuItem.Visible = False
                    BrenntagToolStripMenuItem.Visible = False
                    MosaicToolStripMenuItem2.Visible = False
                    UCToolStripMenuItem1.Visible = False
                    GroupBox3.Visible = False

                End If

                'Turn on load status box
                DisplayLoadStatusToolStripMenuItem.Checked = True
                LoadStatusGroup.Visible = True
                LoadStatusTimer.Enabled = True

                GroupBox2.Visible = False

                'Setup Gate Communications
                CardStart = SysOptions.AccessReaderStart
                CardLength = SysOptions.AccessReaderLength
                cmdOpenGate.Visible = True
                lblGateStatus.Visible = True

                If SysOptions.AccessReaderIP = "" Then
                    GateRead = "Serial"
                    AddLogEntry("Setting up Gate for serial communications")
                    GateSetupSerial(SysOptions.AccessReaderSettings)
                    '---Open connection to ADU ----
                    'Control.OpenADU()
                Else
                    Gate1IP = SysOptions.AccessReaderIP
                    Gate2IP = SysOptions.AccessReaderIP2
                    GateRead = "TCP"
                    AddLogEntry("Setting up Gate for TCP communications")
                    Gate1SetupTCP()
                    If Gate2IP <> "" Then
                        AddLogEntry("Setting up Gate 2 for TCP communications")
                        Gate2SetupTCP()
                    End If
                End If

                If SysOptions.AccessRelayIP = "" Then
                    GateControl = "ADU"
                    AddLogEntry("Setting up Gate control for serial ADU")
                    '---Open connection to ADU ----
                    Control.OpenADU()
                Else
                    GateControl = "IP"
                    RelayIP = SysOptions.AccessRelayIP
                    AddLogEntry("Setting up Gate control for IPRelay")
                    IPRelayFlag = True
                End If

            Else
                If DefaultLocation = "T" Then
                    txtControlCenter.Text = "Tampa"
                    optTampa.Checked = True
                Else
                    txtControlCenter.Text = "Stockton"
                    optStockton.Checked = True

                    'Menu Option views
                    mnuReportBrenntag.Visible = False
                    MosaicToolStripMenuItem.Visible = False
                    MosaicToolStripMenuItem1.Visible = False
                    UCToolStripMenuItem.Visible = False
                    mnuReportMosaic.Visible = False
                    SAMosaicToolStripMenuItem.Visible = False
                    mnuReportUC.Visible = False
                    mnuOrderBrenntag.Visible = False
                    mnuOrderMosaic.Visible = False
                    AllConsigneesToolStripMenuItem.Visible = False
                    BrenntagToolStripMenuItem.Visible = False
                    MosaicToolStripMenuItem2.Visible = False
                    UCToolStripMenuItem1.Visible = False
                    GroupBox3.Visible = False

                End If

                'Check for update availability 
                InstallUpdateSyncWithInfo()

            End If

            AddActivityHandler(Me)
            AddHandler Me.MouseDown, AddressOf NonActivityTimerStop
            AddHandler Me.KeyDown, AddressOf NonActivityTimerStop
            NonActivityTimer.Start()

            cmdPrint.Enabled = False
            SysOptions = Nothing

            Login()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Login()

        FileToolStripMenuItem.Visible = False
        TransToolStripMenuItem.Visible = False
        TicketsToolStripMenuItem.Visible = False
        AdministrationToolStripMenuItem.Visible = False
        TransToolStripMenuItem.Visible = False
        ReportToolStripMenuItem.Visible = False
        If frmLogon.Visible = False Then
            txtStatus.Text = ""
            txtOperator.Text = ""
            AddLogEntry("Waiting for Operator Login")
            frmLogon.ShowDialog()
            'check for operator ID
            txtOperator.Text = OperatorID
            OpID = txtOperator.Text
            txtStatus.Text = "Welcome " & OperatorName
            AddLogEntry(OperatorName & " Successfully logged in ")

            'Start Timer to logoff operator when no activity
            LoginTimer.Enabled = True
        End If
    End Sub

    Private Sub GateSetupSerial(Gate As String)
        Dim parts As String() = Gate.Split(","c)

        GateCom.PortName = parts(0)
        GateCom.BaudRate = parts(1)
        Select Case parts(2)
            Case "N"
                GateCom.Parity = Ports.Parity.None
            Case "E"
                GateCom.Parity = Ports.Parity.Even
            Case "O"
                GateCom.Parity = Ports.Parity.Odd
            Case "S"
                GateCom.Parity = Ports.Parity.Space
            Case "M"
                GateCom.Parity = Ports.Parity.Mark
        End Select

        GateCom.DataBits = parts(3)

        Select Case parts(4)
            Case 1
                GateCom.StopBits = Ports.StopBits.One
            Case 2
                GateCom.StopBits = Ports.StopBits.Two
            Case Else
                GateCom.StopBits = Ports.StopBits.One
        End Select

        GateCom.Open()

        lblGateStatus.Text = "Gate Card Status: Ready"
        GateReadTimer.Enabled = True
    End Sub

    Private Sub Gate1SetupTCP()
        Try
            'Check if Main Gate is reachable
            If My.Computer.Network.Ping(Gate1IP) Then   'Connection ok
                'Create TCP Client connection
                Dim SerialServerPort As String = "2300"
                AddLogEntry("Connecting to Serial Server")
                Gate1TCPClientz = New TcpClient(Gate1IP, SerialServerPort)  'RS232 Serial Server
                GateReadTimer.Enabled = True
                Gate1TCPClientStream = Gate1TCPClientz.GetStream()
                lblGateStatus.Text = "Gate Card Status: Ready"
                Gate1ComFlag = True
            Else
                lblGateStatus.Text = "Gate Card Status: No Connection"
                AddLogEntry("No Connection to Main Gate")
                Gate1ComFlag = False
            End If

        Catch ex As Exception
            AddLogEntry("Gate1SetupTCP: " & ex.Message)
            lblGateStatus.Text = "Gate Card Status: Connection Error"
        End Try
    End Sub

    Private Sub Gate2SetupTCP()
        Try
            'Check if Main Gate is reachable
            If My.Computer.Network.Ping(Gate2IP) Then   'Connection ok
                'Create TCP Client connection
                Dim SerialServerPort As String = "2300"
                AddLogEntry("Connecting to Serial Server")
                Gate2TCPClientz = New TcpClient(Gate2IP, SerialServerPort)  'RS232 Serial Server
                GateReadTimer2.Enabled = True
                Gate2TCPClientStream = Gate2TCPClientz.GetStream()
                lblGate2Status.Text = "Gate Card Status: Ready"
                Gate2ComFlag = True
            Else
                lblGate2Status.Text = "Gate Card Status: No Connection"
                AddLogEntry("No Connection to Walk-in Gate")
                Gate2ComFlag = False
            End If

        Catch ex As Exception
            AddLogEntry("Gate2SetupTCP: " & ex.Message)
            lblGate2Status.Text = "Gate Card Status: Connection Error"
        End Try
    End Sub

    Private Sub LoadCombos()
        Try
            SQL.RunQuery("Select * from Commodity")
            cboCommodity.Items.Clear()
            If SQL.RecordCount <> 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboCommodity.Items.Add(r("Id"))
                Next
            End If

            SQL.RunQuery("Select * from Tank")
            cboTank.Items.Clear()
            If SQL.RecordCount <> 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboTank.Items.Add(r("Id"))
                Next
            End If

            SQL.RunQuery("Select * from Driver")
            cboDriver.Items.Clear()
            If SQL.RecordCount <> 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboDriver.Items.Add(r("Id"))
                Next
            End If

        Catch ex As Exception
            AddLogEntry("LoadCombos: " & ex.Message)
            txtStatus.Text = "SQL Connection Error"
        End Try
    End Sub

    Private Sub fsw1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsw1.Created
        Try
            Dim Datecheck As String = Format(Now, "MMddyy")
            If ProcessingOrders = False Then
                If ControlCenter = True Then
                    WatchPath = WatchPath1
                    If e.Name.Contains(".csv") Then
                        OrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process"
                        AddLogEntry("Filewatcher1: .csv file found - Starting order upload process")
                        'AddOrderEntry("Filewatcher1: .csv file found - Starting order upload process")
                    ElseIf e.Name.Contains(Datecheck) Then
                        SAOrderTimer.Enabled = True
                        ProcessingOrders = True
                        txtStatus.Text = "Starting order upload process for SA"
                        AddLogEntry("Filewatcher1: .xlsx file found - Starting order upload process for SA")
                        'AddOrderEntry("Filewatcher1: .xlsx file found - Starting order upload process for SA")
                    ElseIf e.Name.Contains(".xlsx") Then
                        MosaicOrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process for Mosaic"
                        AddLogEntry("Filewatcher1: .xlsx file found - Starting order upload process for Mosaic")
                        'AddOrderEntry("Filewatcher1: .xlsx file found - Starting order upload process for Mosaic")
                    Else
                        'AddOrderEntry("Filewatcher1: Incompatible file found - Moving file to Failed Folder")
                        MoveTimer.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            AddLogEntry("Filewatcher1: " & ex.Message)
            AddOrderEntry("Filewatcher1: " & ex.Message)
        End Try
    End Sub

    Private Sub OrderTimer_Tick(sender As System.Object, e As System.EventArgs) Handles OrderTimer.Tick
        OrderTimer.Enabled = False
        frmOrderUploadBrenntag.ShowDialog()
    End Sub

    Private Sub MosaicOrderTimer_Tick(sender As System.Object, e As System.EventArgs) Handles MosaicOrderTimer.Tick
        MosaicOrderTimer.Enabled = False
        frmOrderUploadMosaic.ShowDialog()
    End Sub

    Private Sub SAOrderTimer_Tick(sender As Object, e As EventArgs) Handles SAOrderTimer.Tick
        SAOrderTimer.Enabled = False
        ProcessingOrders = True
        frmOrderUploadSA.Show()
        ProcessingOrders = False
        txtStatus.Text = "SA Order Processing Complete"
    End Sub

    Private Sub MoveTimer_Tick(sender As System.Object, e As System.EventArgs) Handles MoveTimer.Tick
        Try
            MoveTimer.Enabled = False
            'Look at watch folder and move non csv or xlsx files to failed
            For Each fn As String In IO.Directory.GetFiles(WatchPath1, "*.*")
                Dim filename As String = Path.GetFileName(fn)
                If fn.Contains(".csv") Or fn.Contains(".xlsx") Then
                    'Do nothing
                Else
                    File.Copy(fn, "C:\Satco\Failed\" & filename, True)
                    File.Delete(fn)
                    AddLogEntry("Moving non .csv or .xlsx file to failed folder: " & filename)
                    AddOrderEntry("Moving non .csv or .xlsx file to failed folder: " & filename)
                End If
            Next
        Catch ex As Exception
            AddLogEntry("MoveTimer: " & ex.Message)
            AddOrderEntry("MoveTimer: " & ex.Message)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles mnuFileExit.Click
        If ControlCenter = True Then
            Dim result As Integer = MessageBox.Show("Are you sure you want to close SATCO Maint?" & vbCrLf & "This will close gate monitoring and Watch control", "Warning !", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                AddLogEntry("Closing Down Satco Manager by Request after warning")
                Me.Close()
            End If
        Else
            AddLogEntry("Closing Down Satco Manager")
            Me.Close()
        End If

    End Sub

    Private Sub TicketsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TicketsToolStripMenuItem.Click
        'frmTicket.ShowDialog()
    End Sub

    Private Sub AboutReporterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuAbout.Click
        'frmSplash.ShowDialog()
    End Sub

    Private Sub cmdLast_Click(sender As System.Object, e As System.EventArgs) Handles cmdLast.Click
        Try
            SQL.RunQuery("Select * from Trans where Left(Code,2) = '" & CodeFilter & "' Order by TDate desc")

            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                cboConsignee.Text = .Item("Code")
                txtID.Text = .Item("ID")
                txtRelease.Text = .Item("ReleaseNumber")
                txtDriver.Text = .Item("DriverId")
                txtDate.Text = Format(.Item("TDate"), "MM-dd-yyyy")
                txtTime.Text = Format(.Item("TDate"), "hh:mm")
                cboTank.Text = .Item("TankId")
                cboCommodity.Text = .Item("CommodityId")
                txtTons.Text = Val(.Item("NetWt")) / 2000
            End With


            cmdPrint.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub StartDatePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles StartDatePicker.ValueChanged
        StartDate = StartDatePicker.Text
        If txtStartTime.Text = "" Then txtStartTime.Text = "00:00:00"
        RptStart = CDate(StartDatePicker.Text).ToString("yyyy-MM-dd") & " " & txtStartTime.Text

    End Sub

    Private Sub EndDatePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles EndDatePicker.ValueChanged
        EndDate = EndDatePicker.Text
        If txtEndTime.Text = "" Then txtEndTime.Text = "23:59:59"

        RptEnd = CDate(EndDatePicker.Text).ToString("yyyy-MM-dd") & " " & txtEndTime.Text

    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        cboConsignee.Text = ""
        txtID.Text = ""
        txtTime.Text = ""
        txtDate.Text = ""
        txtDriver.Text = ""
        cboCommodity.Text = ""
        txtTons.Text = ""
        'txtOperator.Text = ""
        txtRelease.Text = ""
        cmdPrint.Enabled = False
        cboTank.Text = ""
        cboDriver.Text = ""
        txtStatus.Text = ""

    End Sub


    Private Sub optBrenntag_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optBrenntag.CheckedChanged
        Try

            If optBrenntag.Checked = True Then
            txtStatus.Text = " Filter for Brenntag"
            CodeFilter = "BN"

                'Load Consignee Combobox
                SQL.RunQuery("Select * from Consignee where Code like '" & CodeFilter & "%'")
                If SQL.RecordCount <> 0 Then
                    cboConsignee.Items.Clear()
                    For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                        cboConsignee.Items.Add(r("Code"))
                    Next
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub optSA_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optSA.CheckedChanged
        If optSA.Checked = True Then
            txtStatus.Text = " Filter for SA"
            CodeFilter = "SA"

            'Load Consignee Combobox
            SQL.RunQuery("Select * from Consignee where Code like '" & CodeFilter & "%'")
            If SQL.RecordCount <> 0 Then
                cboConsignee.Items.Clear()
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboConsignee.Items.Add(r("Code"))
                Next
            End If

        End If
    End Sub

    Private Sub optUC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optUC.CheckedChanged
        If optUC.Checked = True Then
            txtStatus.Text = " Filter for UC"
            CodeFilter = "UC"

            'Load Consignee Combobox
            SQL.RunQuery("Select * from Consignee where Code like '" & CodeFilter & "%'")
            If SQL.RecordCount <> 0 Then
                cboConsignee.Items.Clear()
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboConsignee.Items.Add(r("Code"))
                Next
            End If

        End If
    End Sub

    Private Sub optMosaic_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optMosaic.CheckedChanged
        If optMosaic.Checked = True Then
            txtStatus.Text = " Filter for Mosaic"
            CodeFilter = "MO"

            'Load Consignee Combobox
            SQL.RunQuery("Select * from Consignee where Code like '" & CodeFilter & "%'")
            cboConsignee.Items.Clear()
            For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                cboConsignee.Items.Add(r("Code"))
            Next
        End If
    End Sub

    Private Sub mnuFileCommodity_Click(sender As System.Object, e As System.EventArgs) Handles mnuFileCommodity.Click
        frmCommodityMaint.ShowDialog()
    End Sub

    Private Sub mnuReportBrenntag_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportBrenntag.Click
        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and CommodityId = 'BN03' order by Tdate asc")
            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula4 ='50% Caustic - Na2O' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
                MsgBox("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.CommodityId} = 'BN03' and {Trans.ReleaseNUmber} <> 'VOID'"
            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            frmViewReport.rptViewer.ReportSource = RptPath & "Brenntag-SQL.rpt"

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuReportDriver_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportDriver.Click
        Try
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            If SQL.DataUpdate("Update ReportInfo SET Formula='" & Query & "'") = 0 Then
                MsgBox("Error updating Formula File")

            Else
                txtStatus.Text = "Formula File Updated"
            End If
            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Driver-SQL-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Driver-SQL.rpt"
            End If

            frmViewReport.Show()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuReportTank_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportTank.Click
        Try
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            If SQL.DataUpdate("Update ReportInfo SET Formula='" & Query & "'") = 0 Then
                MsgBox("Error updating Formula File")

            Else
                txtStatus.Text = "Formula File Updated"
            End If
            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Tank-SQL-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Tank-SQL.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuReportCustomer_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportCustomer.Click
        Try
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            If SQL.DataUpdate("Update ReportInfo SET Formula='" & Query & "'") = 0 Then
                MsgBox("Error updating Formula File")
                'Stop
            Else
                txtStatus.Text = "Formula File Updated"
            End If
            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Customer-SQL-CA.rpt"
                frmViewReport.Show()
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Customer-SQL.rpt"
                frmViewReport.Show()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuReportMosaic_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportMosaic.Click

        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                     "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code LIKE 'MO%' order by Tdate asc")

            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} startswith 'MO' and {Trans.ReleaseNUmber} <> 'VOID'"

            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            frmViewReport.rptViewer.ReportSource = RptPath & "Mosaic-SQL.rpt"
            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub mnuToolsSetup_Click(sender As System.Object, e As System.EventArgs) Handles mnuToolsSetup.Click
        frmSetup.ShowDialog()
    End Sub

    Private Sub mnuToolsFileTransfer_Click(sender As System.Object, e As System.EventArgs)
        frmFileXfer.ShowDialog()
    End Sub

    Private Sub mnuTansManual_Click(sender As System.Object, e As System.EventArgs) Handles mnuTansManual.Click
        frmTransManual.ShowDialog()
    End Sub

    Private Sub mnuTransMaunalRail_Click(sender As System.Object, e As System.EventArgs) Handles mnuTransMaunalRail.Click
        frmRail.ShowDialog()
    End Sub

    Private Sub AccessToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmAccessControlView.ShowDialog()
    End Sub

    Private Sub mnuReportAccess_Click(sender As System.Object, e As System.EventArgs) Handles mnuReportAccess.Click

    End Sub

    Private Sub txtStartTime_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtStartTime.TextChanged
        RptStart = CDate(StartDatePicker.Text).ToString("yyyy-MM-dd ") & txtStartTime.Text
    End Sub

    Private Sub txtEndTime_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEndTime.TextChanged
        RptEnd = CDate(EndDatePicker.Text).ToString("yyyy-MM-dd ") & txtEndTime.Text
    End Sub

    Private Sub mnuFileDriver_Click(sender As System.Object, e As System.EventArgs) Handles mnuFileDriver.Click
        frmDriverMaint.ShowDialog()
    End Sub

    Private Sub mnuFileTank_Click(sender As System.Object, e As System.EventArgs) Handles mnuFileTank.Click
        'frmTankMaint.ShowDialog()
    End Sub

    Private Sub mnuTransWeighIn_Click(sender As System.Object, e As System.EventArgs) Handles mnuTransWeighIn.Click
        frmWeighIn.Show()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        frmTankInventory.Show()
    End Sub

    Private Sub InventoryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InventoryToolStripMenuItem.Click
        frmTankInventory.ShowDialog()
    End Sub

    Private Sub GridToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GridToolStripMenuItem.Click
        TankSearch = ""
        frmTankContents.ShowDialog()
    End Sub

    Private Sub FormToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FormToolStripMenuItem.Click
        frmTankMaint.ShowDialog()
    End Sub

    Private Sub cmdPrevious_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrevious.Click
        Try
            SQL.RunQuery("Select * from Trans where Left(Code,2) = '" & CodeFilter & "'  Order by ID desc")
            cmdNext.Enabled = True
            CurrentRecord = CurrentRecord + 1

            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                cboConsignee.Text = .Item("Code")
                txtID.Text = .Item("ID")
                txtRelease.Text = .Item("ReleaseNumber")
                txtDriver.Text = .Item("DriverId")
                txtDate.Text = Format(.Item("TDate"), "MM-dd-yyyy")
                txtTime.Text = Format(.Item("TDate"), "hh:mm")
                cboTank.Text = .Item("TankId")
                cboCommodity.Text = .Item("CommodityId")
                txtTons.Text = Val(.Item("NetWt")) / 2000
            End With

            cmdPrint.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
            cmdPrevious.Enabled() = False
        End Try
    End Sub

    Private Sub cmdNext_Click(sender As System.Object, e As System.EventArgs) Handles cmdNext.Click
        Try
            SQL.RunQuery("Select * from Trans where Left(Code,2) = '" & CodeFilter & "' Order by ID desc")

            cmdPrevious.Enabled = True
            If CurrentRecord <= 0 Then
                cmdNext.Enabled = False
                CurrentRecord = 0
            Else
                CurrentRecord = CurrentRecord - 1
            End If

            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                cboConsignee.Text = .Item("Code")
                txtID.Text = .Item("ID")
                txtRelease.Text = .Item("ReleaseNumber")
                txtDriver.Text = .Item("DriverId")
                txtDate.Text = Format(.Item("TDate"), "MM-dd-yyyy")
                txtTime.Text = Format(.Item("TDate"), "hh:mm")
                cboTank.Text = .Item("TankId")
                cboCommodity.Text = .Item("CommodityId")
                txtTons.Text = Val(.Item("NetWt")) / 2000
            End With

            cmdPrint.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
            cmdNext.Enabled = False
        End Try
    End Sub

    Private Sub wait(ByVal interval As Integer)
        'Delays process for number of milliseconds
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            SQL.RunQuery("Select * from Trans where Left(Code,2) = '" & CodeFilter & "' Order by ID desc")
            CurrentRecord = SQL.RecordCount - 1
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                cboConsignee.Text = .Item("Code")
                txtID.Text = .Item("ID")
                txtRelease.Text = .Item("ReleaseNumber")
                txtDriver.Text = .Item("DriverId")
                txtDate.Text = Format(.Item("TDate"), "MM-dd-yyyy")
                txtTime.Text = Format(.Item("TDate"), "hh:mm")
                cboTank.Text = .Item("TankId")
                cboCommodity.Text = .Item("CommodityId")
                txtTons.Text = Val(.Item("NetWt")) / 2000
            End With

            cmdPrint.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TicketPrintToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TicketPrintToolStripMenuItem.Click
        If cboDriver.Text <> "" Then
            CardSearchID = cboDriver.Text
        End If

        frmTicketPrint.ShowDialog()
    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AllToolStripMenuItem.Click
        Query = "Select * From Trans"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub LastLoadPerConsigneeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LastLoadPerConsigneeToolStripMenuItem.Click
        Query = "Select Code, max(ID) from Trans group by Code"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        NewPrinterCode = cboConsignee.Text
        NewPrinterID = txtID.Text
        If NewPrinterCode <> "" And NewPrinterID <> "" Then
            PrintLastTicket()
        End If
    End Sub

    Private Sub GateTimer_Tick(sender As System.Object, e As System.EventArgs) Handles GateTimer.Tick
        GateTimer.Enabled = False
        Control.CloseEntryGate()
    End Sub

    Private Sub SetupCom()
        Dim SysOptions = New clsSystem
        Try
            If SysOptions.ScaleActive = 1 Then

                'Monitor AccessCom port for activity
                Dim Card As String = SysOptions.CardReaderSettings
                Dim parts As String() = Card.Split(","c)

                GateCom.PortName = parts(0)
                GateCom.BaudRate = parts(1)
                Select Case parts(2)
                    Case "N"
                        GateCom.Parity = Ports.Parity.None
                    Case "E"
                        GateCom.Parity = Ports.Parity.Even
                    Case "O"
                        GateCom.Parity = Ports.Parity.Odd
                    Case "S"
                        GateCom.Parity = Ports.Parity.Space
                    Case "M"
                        GateCom.Parity = Ports.Parity.Mark
                End Select

                GateCom.DataBits = parts(3)

                Select Case parts(4)
                    Case 1
                        GateCom.StopBits = Ports.StopBits.One
                    Case 2
                        GateCom.StopBits = Ports.StopBits.Two
                    Case Else
                        GateCom.StopBits = Ports.StopBits.One
                End Select
                GateCom.Open()
                GateCom.DiscardInBuffer()
                GateCom.DiscardOutBuffer()

            End If
            SysOptions = Nothing
        Catch ex As Exception
            txtStatus.Text = ex.Message
            AddLogEntry("SetupCom: " & ex.Message)
        End Try

    End Sub

    Private Sub GateReadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles GateReadTimer.Tick

        GateReadTimer.Enabled = False
        Dim Driver As clsDriver
        Dim Result As Integer
        Dim Access As clsAccessControl
        Dim msg As String
        Dim TDate As Date

        Dim CardChar As String = ""
        Dim CardStr As String = ""

        Try
            If GateRead = "Serial" Then
                CardStr = GateCom.ReadExisting
                If Len(CardStr) < 7 Then
                    If Len(CardStr) <> 0 Then
                        AddLogEntry("Bad RFID Card Read: Length = " & Str(Len(CardStr)))
                    End If

                    GateReadTimer.Enabled = True
                    Exit Sub
                End If
            Else
                If My.Computer.Network.Ping(Gate1IP) Then
                    If Gate1ComFlag = False Then
                        Gate1SetupTCP()
                        Gate1ComFlag = True
                    End If
                    If Gate1TCPClientStream.DataAvailable = True Then
                        Dim rcvbytes(Gate1TCPClientz.ReceiveBufferSize) As Byte
                        Gate1TCPClientStream.Read(rcvbytes, 0, CInt(Gate1TCPClientz.ReceiveBufferSize))
                        CardStr = System.Text.Encoding.ASCII.GetString(rcvbytes)
                        'CardStr = cleanString(CardStr)
                        'Stop
                        If Len(CardStr) < Len(CardLength) Then
                            GateReadTimer.Enabled = True
                            Exit Sub
                        End If
                    Else
                        GateReadTimer.Enabled = True
                        Exit Sub
                    End If
                Else
                    If Gate1ComFlag = True Then   'This is the first time failure
                        lblGateStatus.Text = "No Connection to Gate 1"
                        AddLogEntry("No Connection to Gate 1")
                        Gate1ComFlag = False
                    End If
                    GateReadTimer.Enabled = True
                    Exit Sub
                End If
            End If

            If CardStart = 0 And CardLength = 0 Then
                'Stop
                'Decode card data from SecureAkey encoding
                GateCardId = DecodeCard(CardStr)
                If GateCardId = "" Then
                    GateReadTimer.Enabled = True
                    Exit Sub
                End If
            ElseIf CardStart = 1 And CardLength = 1 Then
                'This is an RS232 RFID Reader that outputs Ascii representation of Hex values
                GateCardId = DecodeRS232Card(CardStr)
            Else
                GateCardId = Mid(CardStr, CardStart, CardLength)
            End If

            AddLogEntry("Card read at gate = " & GateCardId)

            ' ----- Verify the card id -----
            Driver = New clsDriver

            If Driver.Search("CardId", GateCardId) = False Then
                Result = 0
                msg = "Invalid Card at gate - " & GateCardId
                lblGateStatus.Text = msg
                AddLogEntry("Card at gate INVALID")
                'Stop
            Else

                TDate = Format(Driver.Twic, "MM/dd/yyyy")
                If TDate < Now And txtControlCenter.Text <> "Stockton Control Center" Then
                    AddLogEntry("Card at gate - Driver Twic is expired")
                    Result = 0
                    msg = "Driver Twic is expired at gate - " & GateCardId
                    lblGateStatus.Text = msg

                ElseIf Driver.Active = False Then
                    AddLogEntry("Driver at gate is INACTIVE")
                    Result = 0
                    msg = "Driver at gate is INACTIVE - " & GateCardId
                    lblGateStatus.Text = msg

                Else
                    Result = 1
                    msg = "Valid Card at gate 1 - " & GateCardId
                    lblGateStatus.Text = msg

                    If txtControlCenter.Text = "Stockton Control Center" Then

                        If Trim(lblStatus0.Text) <> "Filling Cycle Complete" Then
                            lblGateStatus.Text = "Filling Cycle NOT Complete to open gate"
                            AddLogEntry("Card Valid at Gate but Filling Cycle NOT Complete")
                            Driver = Nothing
                            GateReadTimer.Enabled = True  'Turn GateReader back on to check
                            Exit Sub
                        End If
                        If Val(lblScale0.Text) > 250 Then
                            lblGateStatus.Text = "Weight NOT in zero band to open gate"
                            AddLogEntry("Card Valid at Gate but Weight NOT in zero band")
                            Driver = Nothing
                            GateReadTimer.Enabled = True  'Turn GateReader back on to check
                            Exit Sub
                        End If
                        LoadStatus(1, "Main Gate Open")
                    End If

                    OpentheGate()

                    'If GateRead = "Serial" Then
                    '    ' ----- Signal ADU to Open Gate -----
                    '    AddLogEntry("Card at gate VALID... Signaling ADU to open gate")
                    '    Control.OpenEntryGate()
                    '    GateTimer.Enabled = True    'Delay and then close gate
                    'Else
                    '    Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA1")
                    '    Gate1TCPClientz.Client.Send(sendbytes)
                    '    TCPGateTimer.Enabled = True     'Delay and then close gate
                    'End If


                    ' ----- Write the access record -----
                    Access = New clsAccessControl
                    Access.DateTime = CDate(Format(Now, "MM/dd/yyyy") & " " & Format(Now, "HH:mm:ss"))
                    Access.CardId = GateCardId
                    Access.DriverId = Driver.ID
                    Access.Valid = Result
                    Access.AddRecord()

                    Access = Nothing
                    AddLogEntry("Access Record for card " & GateCardId & " added to database")
                End If

            End If

            Driver = Nothing
            GateReadTimer.Enabled = True  'Turn GateReader back on to check

        Catch ex As Exception
            AddLogEntry("GateReadTimer-Tick: " & ex.Message)
            If GateRead = "Serial" Then
                If GateCom.IsOpen = False Then
                    AddLogEntry("GateReadTimer-Tick: Attempting to reOpen port")
                    GateCom.Open()
                End If
                GateReadTimer.Enabled = True  'Turn GateReader back on to check
            End If

        End Try

    End Sub

    Private Function cleanString(ByVal strEdit As String) As String
        Try

            Dim strBuilder As New StringBuilder(strEdit)
            strBuilder.Replace("&nbsp;", "")    'cleans the string of &nbsp;
            strBuilder.Replace(" ", "")    'cleans the string of  spaces
            'if length  of strBuilder is zero then no iterations are performed in the 
            'while loop below. 
            Dim count As Integer = 0
            Dim len As Integer = strBuilder.Length

            strBuilder.Replace(" ", "")  'cleans the string of spaces
            strBuilder.Replace(vbCr, "")  'cleans the string of CR
            strBuilder.Replace(vbNullChar, "")  'cleans the string of vbNullChar
            Return strBuilder.ToString
        Catch ex As Exception
            AddLogEntry("CleanString: " & ex.Message)
            Return ""
        End Try

    End Function
    Private Function DecodeRS232Card(ByVal CardRead As String) As String
        Try

            'This is an RS232 RFID Reader that outputs Ascii representation of Hex values
            DecodeRS232Card = Convert.ToInt32(Mid(CardRead, 2, 8), 16)

            'Some readers have an extra ADA number that needs to be stripped
            'DecodeRS232Card = Convert.ToInt32(Mid(CardRead, 4, 8), 16)
        Catch ex As Exception
            DecodeRS232Card = ""
            AddLogEntry("DecodeRS232Card: " & ex.Message)

        End Try
    End Function
    Private Function DecodeCard(ByVal CardRead As String) As String

        Try
            Dim TrimCard As String = Mid(CardRead, 19, 8)
            'Mask MSB and LSB
            Dim msb As String = Mid(TrimCard, 1, 2)
            Dim lsb As String = Mid(TrimCard, 7, 2)
            Dim msbmask As Integer = Val("&H" & msb)
            Dim lsbmask As Integer = Val("&H" & lsb)
            msbmask = msbmask And &H7F
            lsbmask = lsbmask And &HFE

            msb = Hex(msbmask).PadLeft(2, "0"c)
            lsb = Hex(lsbmask).PadLeft(2, "0"c)

            Dim TrimCard2 As String = msb & Mid(TrimCard, 3, 4) & lsb

            ''Now shift bits to the right by one
            Dim MyShift = Convert.ToInt32(TrimCard2, 16) >> 1

            Dim TrimCard3 As String = Hex(MyShift)
            TrimCard3 = TrimCard3.PadLeft(8, "0"c)

            Dim TrimCard4 As String = Mid(TrimCard3, 1, 4)
            Dim TrimCard5 As String = Mid(TrimCard3, 5, 4)

            Dim intfc As Integer = Convert.ToInt32(TrimCard4, 16)
            Dim intcn As Integer = Convert.ToInt32(TrimCard5, 16)

            TrimCard4 = intfc.ToString("00000")
            TrimCard5 = intcn.ToString("00000")
            Dim TrimCard6 As String = TrimCard4 & TrimCard5
            DecodeCard = Mid(TrimCard6, 6, 5)

        Catch ex As Exception
            DecodeCard = ""
            AddLogEntry("DecodeCard: " & ex.Message)
        End Try

    End Function

    Private Sub OpentheGate()
        Try
            If GateControl = "ADU" Then
                AddLogEntry("Signaling ADU to open gate")
                ' ----- Signal ADU to Open Gate -----
                Control.OpenEntryGate()
                GateTimer.Enabled = True    'Delay and then close gate
            Else
                If txtControlCenter.Text = "Tampa Control Center" Then
                    'Send command to IP Relay
                    AddLogEntry("Signaling IP Relay to open gate 1")
                    RelayControl(1)
                    GateRelayTimer.Enabled = True
                Else
                    If Gate1ComFlag = True Then
                        AddLogEntry("Signaling TCP to open gate 1")
                        'Open gate by sending Weigand board commands
                        Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA1")
                        Gate1TCPClientz.Client.Send(sendbytes)
                        TCPGateTimer.Enabled = True     'Delay to close gate
                        'lblGateStatus.Text = "Manually opened"
                        If txtControlCenter.Text = "Stockton Control Center" And Trim(lblStatus0.Text) = "Filling Cycle Complete" Then
                            LoadStatus(1, "Main Gate Open")
                        End If
                    Else
                        Gate1SetupTCP()  'Try to communicate
                    End If
                End If


            End If

        Catch ex As Exception
            AddLogEntry("OpentheGate: " & ex.Message)
            lblGateStatus.Text = "Error connecting to gate 1"
        End Try
    End Sub

    Private Sub LoadStatus(ByVal ID As Integer, Status As String)
        Try
            SQL.RunQuery("Update Loader set Status = '" & Status & "' where ID = '" & ID & "'")
        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try

    End Sub

    Private Sub LogoffToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogoffToolStripMenuItem.Click
        AddLogEntry(OperatorName & " Successfully logged out ")

        'If ControlCenter = True Then
        '    fsw1.Path = WatchPath
        '    fsw1.EnableRaisingEvents = True
        'Else
        '    fsw1.EnableRaisingEvents = False
        'End If

        Login()

    End Sub

    Private Sub AddActivityHandler(ByVal ThisParent As Control)
        For Each ctr As Control In ThisParent.Controls
            AddHandler ctr.MouseDown, AddressOf NonActivityTimerStop
            AddHandler ctr.MouseMove, AddressOf NonActivityTimerStop
            AddHandler ctr.KeyDown, AddressOf NonActivityTimerStop

            AddActivityHandler(ctr)
        Next
    End Sub

    Private Sub NonActivityTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NonActivityTimer.Tick
        NonActivityTimer.Stop()
        If frmLogon.Visible = False Then
            AddLogEntry(OperatorName & " No Activity - Successfully logged out ")
            'If ControlCenter = True Then
            '    fsw1.Path = WatchPath
            '    fsw1.EnableRaisingEvents = True
            'Else
            '    fsw1.EnableRaisingEvents = False
            'End If

            Login()

        End If
        'NonActivityTimer.Start()
    End Sub
    Private Sub NonActivityTimerStop(ByVal sender As Object, ByVal e As System.EventArgs)
        With NonActivityTimer
            .Stop()
            .Start()
        End With
    End Sub


    Private Sub cmdTimeSet1_Click(sender As System.Object, e As System.EventArgs) Handles cmdTimeSet1.Click
        txtStartTime.Text = "00:00:00"
        txtEndTime.Text = "23:59:59"
    End Sub

    Private Sub cmdTimeSet2_Click(sender As System.Object, e As System.EventArgs) Handles cmdTimeSet2.Click
        txtStartTime.Text = "06:00:00"
        txtEndTime.Text = "05:59:59"
    End Sub

    Private Sub GateCom_DataReceived(sender As System.Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles GateCom.DataReceived

    End Sub

    Private Sub SAMosaicToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SAMosaicToolStripMenuItem.Click
        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            If cboTank.Text = "" And cboConsignee.Text = "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and CommodityId LIKE 'SA%' order by Tdate asc")
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            Else
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' order by Tdate asc")
            End If

            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" And cboConsignee.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.CommodityId} startswith 'SA' and {Trans.ReleaseNUmber} <> 'VOID'"
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            Else
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            frmViewReport.rptViewer.ReportSource = RptPath & "Transaction-Mosaic-SQL.rpt"
            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DriverControlToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DriverControlToolStripMenuItem.Click
        frmDriverControl.ShowDialog()
    End Sub

    Private Sub AllToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles AllToolStripMenuItem1.Click
        SelectedConsignee = ""
        frmConsigneeGrid.ShowDialog()
    End Sub

    Private Sub MosaicToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MosaicToolStripMenuItem.Click
        SelectedConsignee = "BN"
        frmConsigneeMaint.ShowDialog()
    End Sub

    Private Sub MosaicToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles MosaicToolStripMenuItem1.Click
        SelectedConsignee = "MO"
        frmConsigneeMaint.ShowDialog()
    End Sub

    Private Sub SAToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SAToolStripMenuItem.Click
        SelectedConsignee = "SA"
        frmConsigneeMaint.ShowDialog()
    End Sub

    Private Sub UCToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UCToolStripMenuItem.Click
        SelectedConsignee = "UC"
        frmConsigneeMaint.ShowDialog()
    End Sub

    Private Sub AdministrationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AdministrationToolStripMenuItem.Click
        frmAdministration.ShowDialog()
    End Sub



    Private Sub ListBox1_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox1.DoubleClick
        ListBox1.Visible = False
    End Sub

    Private Sub cmdOpenGate_Click(sender As Object, e As EventArgs) Handles cmdOpenGate.Click
        Try
            AddLogEntry("Gate 1 Manually opened")
            OpentheGate()

        Catch ex As Exception
            AddLogEntry("cmdOpenGate: " & ex.Message)
            lblGateStatus.Text = "Error connecting to gate 1"
        End Try
    End Sub

    Private Sub cmdOpenGate2_Click(sender As Object, e As EventArgs) Handles cmdOpenGate2.Click
        Try
            If Gate2ComFlag = True Then
                AddLogEntry("Manual Open.. Signaling TCP to open gate 2")
                'Open gate by sending Weigand board commands
                Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA1")
                Gate2TCPClientz.Client.Send(sendbytes)
                TCPGateTimer2.Enabled = True     'Delay to close gate
                AddLogEntry("Gate 2 Manually opened")
            End If

        Catch ex As Exception
            AddLogEntry("cmdOpenGate2: " & ex.Message)
            lblGate2Status.Text = "Error connecting to gate 2"
        End Try
    End Sub

    Public Sub RelayControl(Action As Boolean)
        Dim RelayStat As String
        Try
            If IPRelayFlag = True Then
                OpenIPRelay()   'Try opening TCPCLient

                If IPRelayFlag = True Then

                    If TCPClientStream2.DataAvailable = True Then
                        Dim rcvbytes(TCPClient2.ReceiveBufferSize) As Byte
                        TCPClientStream2.Read(rcvbytes, 0, CInt(TCPClient2.ReceiveBufferSize))
                        RelayStat = System.Text.Encoding.ASCII.GetString(rcvbytes)
                    End If

                    'Turn on relay for Red - Off for Green
                    If Action Then
                        Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("11")
                        TCPClient2.Client.Send(sendbytes)
                    Else
                        Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("21")
                        TCPClient2.Client.Send(sendbytes)
                    End If
                    TCPClient2.Close()
                Else
                    AddLogEntry("Not able to open IP Relay connection")
                End If

            End If

        Catch ex As Exception
            AddLogEntry("RelayControl: " & ex.Message)
            lblGateStatus.Text = "Error connecting to IP Relay"
            TCPClient2.Close()
            'OpenIPRelay()

        End Try
    End Sub

    Private Sub OpenIPRelay()
        Try
            'Check if Serial Server is reachable
            If My.Computer.Network.Ping(RelayIP) Then
                'Create TCP Client connection
                'AddLogEntry("Connecting to IP Relay")

                TCPClient2 = New TcpClient(RelayIP, "6722")  'IP Relay
                TCPClientStream2 = TCPClient2.GetStream()
                IPRelayFlag = True
            Else
                lblGateStatus.Text = "No Connection to IP Relay"
                AddLogEntry("No Connection to IP Relay")
                IPRelayFlag = False
            End If

        Catch ex As Exception
            AddLogEntry("OpenIPRelay: " & ex.Message)
        End Try
    End Sub

    Private Sub AllConsigneesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllConsigneesToolStripMenuItem.Click
        Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
            "' and '" & EndDate & " " & txtEndTime.Text & "' order by TDate asc"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub BrenntagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrenntagToolStripMenuItem.Click
        Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
            "' and '" & EndDate & " " & txtEndTime.Text & "' and Code like 'BN%' order by TDate asc"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub MosaicToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles MosaicToolStripMenuItem2.Click
        Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
            "' and '" & EndDate & " " & txtEndTime.Text & "' and Code like 'MO%' order by TDate asc"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub SAToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SAToolStripMenuItem1.Click
        If cboTank.Text <> "" Then
            Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
                       "' and '" & EndDate & " " & txtEndTime.Text & "' and Code like 'SA%' and TankId = '" & cboTank.Text & "' order by TDate asc"
        Else
            Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
                        "' and '" & EndDate & " " & txtEndTime.Text & "' and Code like 'SA%' order by TDate asc"
        End If

        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub UCToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UCToolStripMenuItem1.Click
        Query = "Select * From Trans where TDate between '" & StartDate & " " & txtStartTime.Text &
            "' and '" & EndDate & " " & txtEndTime.Text & "' and Code like 'UC%' order by TDate asc"
        frmTransactionMaint.ShowDialog()
    End Sub

    Private Sub mnuReportUC_Click(sender As Object, e As EventArgs) Handles mnuReportUC.Click
        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            If cboTank.Text = "" And cboConsignee.Text = "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code LIKE 'UC%' order by Tdate asc")
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            Else
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' order by Tdate asc")
            End If

            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" And cboConsignee.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} <> 'VOID' and {Trans.Code} like 'UC*'"
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            Else
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            frmViewReport.rptViewer.ReportSource = RptPath & "UC_Transaction-SQL.rpt"
            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuReportSA_Click(sender As Object, e As EventArgs) Handles mnuReportSA.Click
        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            If cboTank.Text = "" And cboConsignee.Text = "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code LIKE 'SA%' order by Tdate asc")
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            Else
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' order by Tdate asc")
            End If

            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                Dim SACode As String = SQL.SQLDataset.Tables(0).Rows(0).Item("Code")
                If SACode = "SA00001" Or SACode = "SA00002" Or SACode = "SA00003" Then
                    BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") - NetWeight / 2000
                Else
                    BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                End If

                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" And cboConsignee.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} <> 'VOID' and {Trans.Code} like 'SA*'"
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            Else
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            If cboTank.Text = "" Then
                If optStockton.Checked = True Then
                    frmViewReport.rptViewer.ReportSource = RptPath & "Transaction-SQL-No-TL-CA.rpt"
                Else
                    frmViewReport.rptViewer.ReportSource = RptPath & "Transaction-SQL-No-TL.rpt"
                End If
            Else
                If optStockton.Checked = True Then
                    frmViewReport.rptViewer.ReportSource = RptPath & "Transaction-SQL-CA.rpt"
                Else
                    frmViewReport.rptViewer.ReportSource = RptPath & "Transaction-SQL.rpt"
                End If
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DailyInventoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DailyInventoryToolStripMenuItem.Click
        Try
            frmTankReport.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CorrectionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorrectionsToolStripMenuItem.Click
        Query = "Select * From Corrections"
        frmCorrectionGrid.ShowDialog()
    End Sub

    Private Sub LoadStatusTimer_Tick(sender As Object, e As EventArgs) Handles LoadStatusTimer.Tick
        Try
            SQL.RunQuery("Select * from Loader")
            If SQL.RecordCount <> 0 Then
                With SQL.SQLDataset.Tables(0).Rows(0)
                    lblLoading0.Text = .Item("Loading")
                    lblStatus0.Text = .Item("Status")
                    lblTank0.Text = .Item("Tank")
                    lblScale0.Text = .Item("Scale")
                End With
                With SQL.SQLDataset.Tables(0).Rows(1)
                    lblLoading1.Text = .Item("Loading")
                    lblStatus1.Text = .Item("Status")
                    lblTank1.Text = .Item("Tank")
                    lblScale1.Text = .Item("Scale")
                End With
                With SQL.SQLDataset.Tables(0).Rows(2)
                    lblLoading2.Text = .Item("Loading")
                    lblStatus2.Text = .Item("Status")
                    lblTank2.Text = .Item("Tank")
                    lblScale2.Text = .Item("Scale")
                End With
                With SQL.SQLDataset.Tables(0).Rows(3)
                    lblLoading3.Text = .Item("Loading")
                    lblStatus3.Text = .Item("Status")
                    lblTank3.Text = .Item("Tank")
                    lblScale3.Text = .Item("Scale")
                End With

                'Check to see if loading is in process
                If lblLoading0.Text = "True" Or lblLoading1.Text = "True" Then
                    SALoadingFlag = True
                Else
                    SALoadingFlag = False
                End If

            End If
        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try
    End Sub

    Private Sub DisplayLoadStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayLoadStatusToolStripMenuItem.Click
        If DisplayLoadStatusToolStripMenuItem.Checked = False Then
            DisplayLoadStatusToolStripMenuItem.Checked = True
            LoadStatusGroup.Visible = True
            LoadStatusTimer.Enabled = True
        Else
            DisplayLoadStatusToolStripMenuItem.Checked = False
            LoadStatusGroup.Visible = False
            LoadStatusTimer.Enabled = False
        End If
    End Sub

    Private Sub optTampa_CheckedChanged(sender As Object, e As EventArgs) Handles optTampa.CheckedChanged
        DBPath = DBPath1
        txtStatus.Text = "Location and Database set to Tampa"
        txtControlCenter.Text = "Tampa"
        PictureBox1.Visible = False
        PictureBox2.Visible = True
        PictureBox3.Visible = False

        'Menu Option views
        mnuReportBrenntag.Visible = True
        MosaicToolStripMenuItem.Visible = True
        MosaicToolStripMenuItem1.Visible = True
        UCToolStripMenuItem.Visible = True
        mnuReportMosaic.Visible = True
        SAMosaicToolStripMenuItem.Visible = True
        mnuReportUC.Visible = True
        mnuOrderBrenntag.Visible = True
        mnuOrderMosaic.Visible = True
        AllConsigneesToolStripMenuItem.Visible = True
        BrenntagToolStripMenuItem.Visible = True
        MosaicToolStripMenuItem2.Visible = True
        UCToolStripMenuItem1.Visible = True
        GroupBox3.Visible = True

        SQL.Reconnect()

        If SQL.HasConnection = True Then
            txtStatus.Text = "Connection Successful to Tampa SQL Database"
            AddLogEntry("Connection Successful to SQL Database")
            SQL.RunQuery("Select * From tank")
        Else
            txtStatus.Text = "Connection to Tampa SQL Database failed"
            AddLogEntry("Connection to SQL Database failed")
        End If

        LoadCombos()

    End Sub

    Private Sub optStockton_CheckedChanged(sender As Object, e As EventArgs) Handles optStockton.CheckedChanged
        DBPath = DBPath2
        txtStatus.Text = "Location and Database set to Stockton"
        txtControlCenter.Text = "Stockton"
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        PictureBox3.Visible = True

        'Menu Option views
        mnuReportBrenntag.Visible = False
        MosaicToolStripMenuItem.Visible = False
        MosaicToolStripMenuItem1.Visible = False
        UCToolStripMenuItem.Visible = False
        mnuReportMosaic.Visible = False
        SAMosaicToolStripMenuItem.Visible = False
        mnuReportUC.Visible = False
        mnuOrderBrenntag.Visible = False
        mnuOrderMosaic.Visible = False
        AllConsigneesToolStripMenuItem.Visible = False
        BrenntagToolStripMenuItem.Visible = False
        MosaicToolStripMenuItem2.Visible = False
        UCToolStripMenuItem1.Visible = False
        GroupBox3.Visible = False

        SQL.Reconnect()
        If SQL.HasConnection = True Then
            txtStatus.Text = "Connection Successful to Stockton SQL Database"
            AddLogEntry("Connection Successful to SQL Database")
            SQL.RunQuery("Select * From tank")
        Else
            txtStatus.Text = "Connection to Stockton SQL Database failed"
            AddLogEntry("Connection to SQL Database failed")
        End If

        LoadCombos()

    End Sub

    Private Sub CarrierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CarrierToolStripMenuItem.Click
        frmCarrierMaint.ShowDialog()
    End Sub

    Private Sub OrderUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderUploadToolStripMenuItem.Click
        Try

            ReportQuery = "{SA.DeliveryDate} in '" & RptStart & "' to '" & RptEnd & "'"

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.SelectionFormula = ReportQuery
                frmViewReport.rptViewer.ReportSource = RptPath & "SAOrder-SQL-CA.rpt"
            Else
                frmViewReport.rptViewer.SelectionFormula = ReportQuery
                frmViewReport.rptViewer.ReportSource = RptPath & "SAOrder-SQL.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuOrderBrenntag_Click(sender As Object, e As EventArgs) Handles mnuOrderBrenntag.Click
        frmBrenntagMaint.ShowDialog()
    End Sub

    Private Sub mnuOrderMosaic_Click(sender As Object, e As EventArgs) Handles mnuOrderMosaic.Click
        frmMOMaint.ShowDialog()
    End Sub

    Private Sub mnuOrderSA_Click(sender As Object, e As EventArgs) Handles mnuOrderSA.Click
        frmSAMaint.ShowDialog()
    End Sub

    Private Sub UpdateDocuments()
        Try
            If MsgBox("Do you wish to copy Report files " & vbCrLf & "from the server to the local machine?", vbYesNo, "Update Files") = vbYes Then
                'Copy Report files from SATCOMAINT to local Reports Path
                'Create the target directory if necessary
                Dim toPath = RptPath
                Dim fromPath = "\\" & DBPath & "\Satco\Reports\"
                Dim toPathInfo = New DirectoryInfo(toPath)
                If (Not toPathInfo.Exists) Then
                    toPathInfo.Create()
                End If
                Dim fromPathInfo = New DirectoryInfo(fromPath)
                'copy all files 
                For Each file As FileInfo In fromPathInfo.GetFiles()
                    file.CopyTo(Path.Combine(toPath, file.Name), True)
                Next
                AddLogEntry("Transferring files to Report folder")
            Else
                AddLogEntry("Report file update cancelled")
            End If

            If MsgBox("Do you wish to copy Document files " & vbCrLf & "from the server to the local machine?", vbYesNo, "Update Files") = vbYes Then
                'Copy BOL files from SATCOMAINT to local Documents Path
                'Create the target directory if necessary
                Dim toPath = SharePath
                Dim fromPath = "\\" & DBPath & "\Satco\Documents\"
                Dim toPathInfo = New DirectoryInfo(toPath)
                If (Not toPathInfo.Exists) Then
                    toPathInfo.Create()
                End If
                Dim fromPathInfo = New DirectoryInfo(fromPath)
                'copy all files 
                For Each file As FileInfo In fromPathInfo.GetFiles()
                    file.CopyTo(Path.Combine(toPath, file.Name), True)
                Next
                AddLogEntry("Transferring files to Document folder")
                MsgBox("Files have been updated")
            Else
                AddLogEntry("Document file update cancelled")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateDocumentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateDocumentsToolStripMenuItem.Click
        UpdateDocuments()
    End Sub

    Private Sub TCPGateTimer_Tick(sender As Object, e As EventArgs) Handles TCPGateTimer.Tick
        Try
            TCPGateTimer.Enabled = False
            Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA0")
            Gate1TCPClientz.Client.Send(sendbytes)
        Catch ex As Exception
            AddLogEntry("TCPGateTimer: " & ex.Message)
        End Try
    End Sub

    Private Sub TCPGateTimer2_Tick(sender As Object, e As EventArgs) Handles TCPGateTimer2.Tick
        Try
            TCPGateTimer2.Enabled = False
            Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA0")
            Gate2TCPClientz.Client.Send(sendbytes)
        Catch ex As Exception
            AddLogEntry("TCPGateTimer2: " & ex.Message)
        End Try
    End Sub

    Private Sub GateReadTimer2_Tick(sender As Object, e As EventArgs) Handles GateReadTimer2.Tick
        GateReadTimer2.Enabled = False
        Dim Driver As clsDriver
        Dim Result As Integer
        Dim Access As clsAccessControl
        Dim msg As String
        Dim TDate As Date

        Dim CardChar As String = ""
        Dim CardStr As String = ""

        Try
            If My.Computer.Network.Ping(Gate2IP) Then
                If Gate2ComFlag = False Then
                    Gate2SetupTCP()
                    Gate2ComFlag = True
                End If
                If Gate2TCPClientStream.DataAvailable = True Then
                    Dim rcvbytes(Gate2TCPClientz.ReceiveBufferSize) As Byte
                    Gate2TCPClientStream.Read(rcvbytes, 0, CInt(Gate2TCPClientz.ReceiveBufferSize))
                    CardStr = System.Text.Encoding.ASCII.GetString(rcvbytes)
                    CardStr = cleanString(CardStr)
                    'Stop
                    If Len(CardStr) < Len(CardLength) Then
                        GateReadTimer2.Enabled = True
                        Exit Sub
                    End If
                Else
                    GateReadTimer2.Enabled = True
                    Exit Sub
                End If
            Else
                If Gate2ComFlag = True Then   'This is the first time failure
                    lblGate2Status.Text = "No Connection to Gate 2"
                    AddLogEntry("No Connection to Gate 2")
                    Gate2ComFlag = False
                End If
                GateReadTimer2.Enabled = True
                Exit Sub
            End If

            If CardStart = 0 And CardLength = 0 Then
                'Decode card data from SecureAkey encoding
                GateCardId = DecodeCard(CardStr)
                If GateCardId = "" Then
                    GateReadTimer2.Enabled = True
                    Exit Sub
                End If
            Else
                GateCardId = Mid(CardStr, CardStart, CardLength)
            End If

            AddLogEntry("Card read at gate 2 = " & GateCardId)

            ' ----- Verify the card id -----
            Driver = New clsDriver

            If Driver.Search("CardId", GateCardId) = False Then
                Result = 0
                msg = "Invalid Card at gate 2 - " & GateCardId
                lblGate2Status.Text = msg
                AddLogEntry("Card at gate 2 INVALID")
                'Stop
            Else

                TDate = Format(Driver.Twic, "MM/dd/yyyy")
                If TDate < Now And txtControlCenter.Text <> "Stockton Control Center" Then
                    AddLogEntry("Card at gate 2 - Driver Twic is expired")
                    Result = 0
                    msg = "Driver Twic is expired at gate 2 - " & GateCardId
                    lblGate2Status.Text = msg

                ElseIf Driver.Active = False Then
                    AddLogEntry("Driver at gate 2 is INACTIVE")
                    Result = 0
                    msg = "Driver at gate 2 is INACTIVE - " & GateCardId
                    lblGate2Status.Text = msg

                Else
                    Result = 1
                    msg = "Valid Card at gate 2 - " & GateCardId
                    lblGate2Status.Text = msg
                    Dim sendbytes() As Byte = System.Text.Encoding.ASCII.GetBytes("OA1")
                    Gate2TCPClientz.Client.Send(sendbytes)
                    TCPGateTimer2.Enabled = True     'Delay and then close gate

                    ' ----- Write the access record -----
                    Access = New clsAccessControl
                    Access.DateTime = CDate(Format(Now, "MM/dd/yyyy") & " " & Format(Now, "HH:mm:ss"))
                    Access.CardId = GateCardId
                    Access.DriverId = Driver.ID
                    Access.Valid = Result
                    Access.AddRecord()

                    Access = Nothing
                    AddLogEntry("Access Record for card " & GateCardId & " added to database")
                End If

            End If

            Driver = Nothing
            GateReadTimer2.Enabled = True  'Turn GateReader back on to check

        Catch ex As Exception
            AddLogEntry("GateReadTimer2-Tick: " & ex.Message)
            'GateReadTimer2.Enabled = True  'Turn GateReader back on to check
        End Try
    End Sub

    Private Sub InstallUpdateSyncWithInfo()
        Dim info As UpdateCheckInfo = Nothing

        If (ApplicationDeployment.IsNetworkDeployed) Then
            Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

            Try
                info = AD.CheckForDetailedUpdate()
            Catch dde As DeploymentDownloadException
                MessageBox.Show("The new version of the application cannot be downloaded at this time. " + ControlChars.Lf & ControlChars.Lf & "Please check your network connection, or try again later. Error: " + dde.Message)
                Return
            Catch ioe As InvalidOperationException
                MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " & ioe.Message)
                Return
            End Try

            If (info.UpdateAvailable) Then
                Dim doUpdate As Boolean = True

                If (Not info.IsUpdateRequired) Then
                    Dim dr As DialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel)
                    If (Not System.Windows.Forms.DialogResult.OK = dr) Then
                        doUpdate = False
                    End If
                Else
                    ' Display a message that the app MUST reboot. Display the minimum required version.
                    MessageBox.Show("This application has detected a mandatory update from your current " &
                        "version to version " & info.MinimumRequiredVersion.ToString() &
                        ". The application will now install the update and restart.",
                        "Update Available", MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If

                If (doUpdate) Then
                    Try
                        AD.Update()
                        MessageBox.Show("The application has been upgraded, and will now restart.")
                        Application.Restart()
                    Catch dde As DeploymentDownloadException
                        If ControlCenter = True Then
                            AddLogEntry("Cannot install the latest version of the application. ")
                            AddLogEntry("Please check your network connection, or try again later.")
                        Else
                            MessageBox.Show("Cannot install the latest version of the application. " & ControlChars.Lf & ControlChars.Lf & "Please check your network connection, or try again later.")
                        End If

                        Return
                    End Try
                End If
            Else
                If ControlCenter = True Then
                    AddLogEntry("Version is up to date")
                Else
                    txtStatus.Text = "Version is up to date"
                End If
            End If
        End If
    End Sub

    Private Sub CheckForUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdateToolStripMenuItem.Click
        InstallUpdateSyncWithInfo()
    End Sub

    Private Sub CarrierToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CarrierToolStripMenuItem1.Click
        Try
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            If SQL.DataUpdate("Update ReportInfo SET Formula='" & Query & "'") = 0 Then
                MsgBox("Error updating Formula File")

            Else
                txtStatus.Text = "Formula File Updated"
            End If
            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Carrier-SQL-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Carrier-SQL.rpt"
            End If

            frmViewReport.Show()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GridToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GridToolStripMenuItem1.Click
        frmTankGrid.ShowDialog()
    End Sub

    Private Sub GateAccessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GateAccessToolStripMenuItem.Click
        Try

            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            If SQL.DataUpdate("Update ReportInfo SET Formula='" & Query & "'") = 0 Then
                MsgBox("Error updating Formula File")
            Else
                txtStatus.Text = "Formula File Updated"
            End If

            If cboDriver.Text = "" Then
                ReportQuery = "{AccessControl.DateTime} in '" & RptStart & "' to '" & RptEnd & "'"
            Else
                ReportQuery = "{AccessControl.DateTime} in '" & RptStart & "' to '" & RptEnd & "' and {AccessControl.DriverId} = '" & cboDriver.Text & "'"
            End If



            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            frmViewReport.rptViewer.ReportSource = RptPath & "Access-SQL.rpt"

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadAccessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadAccessToolStripMenuItem.Click
        Dim BeginLevel As Double
        Dim EndLevel As Double

        Try
            'Get beginning and ending tank levels
            If cboTank.Text = "" And cboConsignee.Text = "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code LIKE 'SA%' order by Tdate asc")
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' and Code = '" & cboConsignee.Text & "' order by Tdate asc")
            Else
                SQL.RunQuery("Select * from Trans where TDate between '" & StartDatePicker.Text & " " & txtStartTime.Text &
                                         "' and '" & EndDatePicker.Text & " " & txtEndTime.Text & "' and TankId = '" & cboTank.Text & "' order by Tdate asc")
            End If

            If SQL.RecordCount > 0 Then
                Dim NetWeight As Long = SQL.SQLDataset.Tables(0).Rows(0).Item("GrossWt") - SQL.SQLDataset.Tables(0).Rows(0).Item("TareWt")
                Dim SACode As String = SQL.SQLDataset.Tables(0).Rows(0).Item("Code")
                If SACode = "SA00001" Or SACode = "SA00002" Or SACode = "SA00003" Then
                    BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") - NetWeight / 2000
                Else
                    BeginLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("TankLevel") + NetWeight / 2000
                End If

                EndLevel = SQL.SQLDataset.Tables(0).Rows(SQL.RecordCount - 1).Item("TankLevel")

            End If
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "' , " &
                                      "Formula5 ='" & BeginLevel & "' , " &
                                      "Formula6 ='" & EndLevel & "' "
            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" And cboConsignee.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} <> 'VOID' and {Trans.Code} like 'SA*'"
            ElseIf cboTank.Text = "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            ElseIf cboTank.Text <> "" And cboConsignee.Text <> "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.Code} = '" & cboConsignee.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            Else
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.TankId} = '" & cboTank.Text & "' and {Trans.ReleaseNUmber} <> 'VOID'"
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery
            If cboTank.Text = "" Then
                If optStockton.Checked = True Then
                    frmViewReport.rptViewer.ReportSource = RptPath & "TransactionTime-SQL-No-TL-CA.rpt"
                Else
                    frmViewReport.rptViewer.ReportSource = RptPath & "TransactionTime-SQL-Local.rpt"
                End If
            Else
                If optStockton.Checked = True Then
                    frmViewReport.rptViewer.ReportSource = RptPath & "TransactionTIme-SQL-CA.rpt"
                Else
                    frmViewReport.rptViewer.ReportSource = RptPath & "TransactionTime-SQL-Local.rpt"
                End If
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TruckToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TruckToolStripMenuItem.Click

        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNumber} <> 'VOID' and {Trans.ReleaseNumber} <> 'RAIL' " &
                                "and {Trans.ReleaseNumber} <> 'INV'"
            Else
                If cboTank.Text = "01" Or cboTank.Text = "02" Or cboTank.Text = "05" Then  'get all SA
                    ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} Like 'SA*' and {Trans.ReleaseNUmber} <> 'VOID' " &
                                "and {Trans.ReleaseNUmber} <> 'RAIL' and {Trans.ReleaseNumber} <> 'INV'"
                Else
                    ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} <> 'VOID' and {Trans.ReleaseNUmber} <> 'RAIL' " &
                                "and {Trans.TankID} = '" & cboTank.Text & "' "
                End If
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Truck-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Truck.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RailToolStripMenuItem.Click
        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            If cboTank.Text = "" Then
                ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNumber} = 'RAIL' "
            Else
                If cboTank.Text = "01" Or cboTank.Text = "02" Or cboTank.Text = "05" Then  'get all SA
                    ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} = 'RAIL' " &
                                "and {Trans.Code} Like 'SA*' "
                Else
                    ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.ReleaseNUmber} = 'RAIL' " &
                                "and {Trans.TankID} = '" & cboTank.Text & "' "
                End If
            End If

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Rail-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Rail.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub VesselToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VesselToolStripMenuItem.Click
        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = 'SA00003' "

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Vessel-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Vessel.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AdjustmentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentsToolStripMenuItem.Click
        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If


            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = 'SA00002' "

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Adjustments-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Adjustments.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DilutionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DilutionsToolStripMenuItem.Click
        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If


            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = 'SA00001' "

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Dilutions-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Dilutions.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtNotes_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged
        'If removing Trailer2Id from database - adjust cell number in frmTransactionMaint for BOL Printing and Rail details

        '02-08-21 Added Month End Reports
        '06-24-22 Railcar Inventory item and report added
        '07-20-22 Added two additional watch folders to eliminate email system
        '08-10-22 Changed compile to 64 bit - change ADUHID.dll to ADUHID64.dll in ADU208 module and install on PC
        '01-27-23 Removed email stuff
        '03-22-23 Gate card info Start  Length  (Port number is 2300 on serial server)
        '         Old Tampa cards  6    5
        '         Newer Tampa cards 0   0
        '         Weigand Stockton  1   7
        '         RS232 at Tampa    1   1


    End Sub

    Private Sub RailcarSAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RailcarSAToolStripMenuItem.Click
        Try
            'Update Formulas in ReportInfo Table
            Query = "From " & StartDatePicker.Text & " to " & EndDatePicker.Text
            Dim TQuery As String = "From " & txtStartTime.Text & " to " & txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='" & TQuery & "'"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else
                AddLogEntry("ReportInfo File Updated")
            End If

            ReportQuery = "{Trans.TDate} in '" & RptStart & "' to '" & RptEnd & "' and {Trans.Code} = 'SA00004' "

            frmViewReport.rptViewer.SelectionFormula = ReportQuery

            If optStockton.Checked = True Then
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Railcar-CA.rpt"
            Else
                frmViewReport.rptViewer.ReportSource = RptPath & "Monthly Railcar.rpt"
            End If

            frmViewReport.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub fsw2_Created(sender As Object, e As FileSystemEventArgs) Handles fsw2.Created
        Try
            Dim Datecheck As String = Format(Now, "MMddyy")
            If ProcessingOrders = False Then
                If ControlCenter = True Then
                    WatchPath = WatchPath2
                    If e.Name.Contains(".csv") Then
                        OrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process"
                        AddLogEntry("Filewatcher2: .csv file found - Starting order upload process")
                        'AddOrderEntry("Filewatcher2: .csv file found - Starting order upload process")
                    ElseIf e.Name.Contains(Datecheck) Then
                        SAOrderTimer.Enabled = True
                        ProcessingOrders = True
                        txtStatus.Text = "Starting order upload process for SA"
                        AddLogEntry("Filewatcher2: .xlsx file found - Starting order upload process for SA")
                        'AddOrderEntry("Filewatcher: .xlsx file found - Starting order upload process for SA")
                    ElseIf e.Name.Contains(".xlsx") Then
                        MosaicOrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process for Mosaic"
                        AddLogEntry("Filewatcher2: .xlsx file found - Starting order upload process for Mosaic")
                        'AddOrderEntry("Filewatcher2: .xlsx file found - Starting order upload process for Mosaic")
                    Else
                        'AddOrderEntry("Filewatcher2: Incompatible file found - Moving file to Failed Folder")
                        MoveTimer.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            AddLogEntry("Filewatcher2: " & ex.Message)
            AddOrderEntry("Filewatcher2: " & ex.Message)
        End Try
    End Sub

    Private Sub fsw3_Created(sender As Object, e As FileSystemEventArgs) Handles fsw3.Created
        Try
            Dim Datecheck As String = Format(Now, "MMddyy")
            If ProcessingOrders = False Then
                If ControlCenter = True Then
                    WatchPath = WatchPath3
                    If e.Name.Contains(".csv") Then
                        OrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process"
                        AddLogEntry("Filewatcher3: .csv file found - Starting order upload process")
                        'AddOrderEntry("Filewatcher3: .csv file found - Starting order upload process")
                    ElseIf e.Name.Contains(Datecheck) Then
                        SAOrderTimer.Enabled = True
                        ProcessingOrders = True
                        txtStatus.Text = "Starting order upload process for SA"
                        AddLogEntry("Filewatcher3: .xlsx file found - Starting order upload process for SA")
                        'AddOrderEntry("Filewatcher3: .xlsx file found - Starting order upload process for SA")
                    ElseIf e.Name.Contains(".xlsx") Then
                        MosaicOrderTimer.Enabled = True
                        txtStatus.Text = "Starting order upload process for Mosaic"
                        AddLogEntry("Filewatcher3: .xlsx file found - Starting order upload process for Mosaic")
                        'AddOrderEntry("Filewatcher3: .xlsx file found - Starting order upload process for Mosaic")
                    Else
                        'AddOrderEntry("Filewatcher3: Incompatible file found - Moving file to Failed Folder")
                        MoveTimer.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            AddLogEntry("Filewatcher3: " & ex.Message)
            AddOrderEntry("Filewatcher3: " & ex.Message)
        End Try
    End Sub

    Private Sub GateRelayTimer_Tick(sender As Object, e As EventArgs) Handles GateRelayTimer.Tick
        Try
            GateRelayTimer.Enabled = False
            RelayControl(0)
        Catch ex As Exception
            AddLogEntry("GateRelayTimer: " & ex.Message)
        End Try
    End Sub
End Class
