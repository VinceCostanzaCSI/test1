Imports System.IO

Module Module1

    Public CtrlPassword As String
    Public OperatorName As String
    Public PrinterConnected As Integer
    Public GateCardReaderConnected As Integer
    Public NewPrinterID As String
    Public NewPrinterSAUC As String
    Public NewPrinterTicket As String
    Public MaintMode As Boolean
    Public FullAccess As Boolean
    Public LoadAccess As Boolean
    Public DriverSearchGrid As String
    Public SASearchGrid As String
    Public YaraSearchGrid As String
    Public BrenntagSearchGrid As String
    Public TankSearch As String
    Public CardStart As Integer
    Public CardLength As Integer
    Public GateCardStart As Integer
    Public GateCardLength As Integer
    Public CardSearchID As String
    Public caldate As String
    Public JagIsActive As Boolean
    Public SharePath As String
    Public RptPath As String
    Public NSFLoad As Boolean
    Public ScaleNumber As Integer = 1
    Public ScaleIsActive As Boolean

    Public OpID As String
    Public EditCOntrol As String
    Public Query As String
    Public ReportQuery As String
    Public DPath As String
    Public StartDate As String
    Public EndDate As String

    ''System Variables
    'Public AccessReaderActive As Boolean
    'Public AccessReaderBaud As Integer
    'Public AccessReaderDataBits As Integer
    'Public AccessReaderLength As Integer
    'Public AccessReaderParity As Integer
    'Public AccessReaderPort As Integer
    'Public AccessReaderSettings As String
    'Public AccessReaderStart As Integer
    'Public AccessReaderStatus As Integer
    'Public AccessReaderStopBits As Integer
    'Public ActiveOperator As String
    'Public Address1 As String
    'Public Address2 As String
    'Public AdminPassword As String
    'Public ArchiveDate As String
    'Public BNCapacityMax As Long
    'Public BNCapacityMin As Long
    'Public BNCOA1 As String
    'Public BNCOA2 As String
    'Public BNCOA3 As String
    'Public BNCOA4 As String
    'Public BNCOA5 As String
    'Public BNCOA6 As String
    'Public BNCOA7 As String
    'Public BNCOA8 As String
    'Public BNCOA9 As String
    'Public BNCOA10 As String
    'Public BNCOA11 As String
    'Public BNCOA12 As String
    'Public BNCOA13 As String
    'Public BNCOA14 As String
    'Public BNGravity As String
    'Public BNNumberOfTickets As Integer
    'Public BNppg As String
    'Public CardReaderActive As Boolean
    'Public CardReaderBaud As Integer
    'Public CardReaderDataBits As Integer
    'Public CardReaderLength As Integer
    'Public CardReaderParity As Integer
    'Public CardReaderPort As Integer
    'Public CardReaderSettings As String
    'Public CardReaderStart As Integer
    'Public CardReaderStatus As Integer
    'Public CardReaderStopBits As Integer
    'Public City As String
    'Public CN1Batch As String
    'Public CN1Calcium As String
    'Public CN1Date As String
    'Public CN1Density As String
    'Public CN1Graivty As String
    'Public CN1Nitrate As String
    'Public CN1pH As String
    'Public CN1Product As String
    'Public CN1Turbidity As String
    'Public CNBatch As String
    'Public CNDate As String
    'Public CNGravity As String
    'Public CNNitrate As String
    'Public CNNumberOfTickets As Integer
    'Public CNpH As String
    'Public CNProduct As String
    'Public CompanyName As String
    'Public CurrentTab As Integer
    'Public DatabasePath As String
    'Public DocumentPath As String
    'Public HazmatActive As Boolean
    'Public LogProcess As Integer
    'Public ManualTicket As Integer
    'Public MaxFillWeight As Long
    'Public ReadDelay As Integer
    'Public ReportPath As String
    'Public SANumberOfTickets As Integer
    'Public State As String
    'Public TareWeightHigh As Long
    'Public TareWeightLow As Long
    'Public UCNumberOfTickets As Integer
    'Public VariableWt As Long
    'Public WatchPath As String
    'Public Zip As String

    'Type of Product
    Public YaraDriver As Boolean
    Public YaraRelease As String
    Public YaraSolution As String

    'Brenntag Variables
    'Public BrenntagPO As String
    'Public BrenntagBOL As String
    'Public BrenntagAltPO As String
    'Public BrenntagName As String
    'Public BrenntagAddress1 As String
    'Public BrenntagCSZ As String

    Public BrenntagDriver As Boolean
    Public BrenntagRelease As String
    Public BrenntagSolution As String
    Public BrenntagMaxLoad As String
    Public BrenntagSplit As Boolean
    Public BrenntagUpdate As Boolean

    Public Sub CenterForm(ByVal frm As Form, Optional ByVal parent As Form = Nothing)
        '' Note: call this from frm's Load event!
        Dim r As Rectangle
        If parent IsNot Nothing Then
            r = parent.RectangleToScreen(parent.ClientRectangle)
        Else
            r = Screen.FromPoint(frm.Location).WorkingArea
        End If

        Dim x = r.Left + (r.Width - frm.Width) \ 2
        Dim y = r.Top + (r.Height - frm.Height) \ 2
        frm.Location = New Point(x, y)
        'Stop
    End Sub

    Public Function Delay(ByVal Wait As Double) As Integer
        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(Wait)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop
        Return True
    End Function

    Public Function AnotherInstance() As Integer
        'Eschew multiple instances of program
        'Get number of processes of you program
        If Process.GetProcessesByName _
          (Process.GetCurrentProcess.ProcessName).Length > 1 Then

            MessageBox.Show _
             ("Another Instance of this process is already running", _
                 "Multiple Instances Forbidden", _
                  MessageBoxButtons.OK, _
                 MessageBoxIcon.Exclamation)
            Application.Exit()
            AnotherInstance = True
        Else
            AnotherInstance = False
        End If

    End Function

    Public Sub AddLogEntry(ByVal Msg As String)
        'Setup log file
        Dim localpath As String

        Dim uriPath As String = System.IO.Path.GetDirectoryName( _
           System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        localpath = New Uri(uriPath).LocalPath
        Dim strFile As String = localpath & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
        File.AppendAllText(strFile, String.Format(Msg & " " & DateTime.Now & vbCrLf))

    End Sub

    Public Sub DataPathInit()
        Dim TPth As String
        Dim XPth As String
        Dim A As String
        Dim B As String
        Dim C As String


        Try
            Dim rdr As StreamReader
            rdr = New StreamReader("c:\SATCO\SetPath.dat")
            Dim parts() As String = rdr.ReadLine().Split(","c)
            TPth = parts(0)
            XPth = parts(1)
            A = parts(2)
            B = parts(3)
            C = parts(4)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Module
