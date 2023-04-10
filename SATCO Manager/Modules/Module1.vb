Imports System.IO
Imports System.Net.Mail
Imports System.Drawing.Printing
Imports Spire.Doc
Imports Spire.Doc.Documents
Imports Spire.Xls
Module Module1

    'Control Variables
    Public Version As String = "3.1.0.28"
    Public OperatorID As String
    Public OperatorName As String
    Public FileMaint As Boolean
    Public FileControl As Integer
    Public Reports As Boolean
    Public Tools As Boolean
    Public AdminRights As Boolean
    Public TransModify As Boolean
    Public FileAdd As Boolean
    Public FileDelete As Boolean
    Public FileModify As Boolean
    Public ControlCenter As Boolean
    Public WatchPath As String
    Public WatchPath1 As String
    Public WatchPath2 As String
    Public WatchPath3 As String
    Public DefaultLocation As String

    Public NewPrinterID As String
    Public NewPrinterCode As String
    Public CardSearchID As String
    Public ScaleIsActive As Boolean
    Public TankSearch As String
    Public DBPath As String
    Public DBPath1 As String
    Public DBPath2 As String
    Public SharePath As String
    Public RptPath As String
    Public ScaleNumber As Integer = 1
    Public ADUResponse As String
    Public OpID As String


    Public Query As String
    Public ReportQuery As String
    Public StartDate As String
    Public EndDate As String
    Public RptStart As String
    Public RptEnd As String
    Public CodeFilter As String
    Public USBDrive As String
    Public TCPReceive As String
    Public FilterDate As Boolean
    Public SelectedRow As Integer
    Public SelectedCode As String
    Public SelectedID As String
    Public SelectedTank As Integer
    Public SelectedConsignee As String
    Public NewTankLevel As Double
    Public SALoadingFlag As Boolean

    Public Const RED_LIGHT = 0
    Public Const GREEN_LIGHT = 1
    Public Const MAX_ATTEMPTS = 3

    Public bControlBoxRemoved As Boolean
    Public lOldStyle As Long

    Public Control As New clsControl

    'SATCO Variables
    Public SAReleaseFlag As Boolean

    'Mosaic Variables
    Public MosaicDriver As Boolean
    Public MosaicRelease As String
    Public MosaicSolution As String
    Public MosaicUpdate As Boolean

    'Brenntag Variables
    Public BrenntagDriver As Boolean
    Public BrenntagRelease As String
    Public BrenntagSolution As String
    Public BrenntagMaxLoad As String
    Public BrenntagSplit As Boolean
    Public BrenntagUpdate As Boolean

    'SA Variables
    Public SADriver As Boolean
    Public SARelease As String
    Public SASolution As String
    Public SAUpdate As Boolean

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

    Public Sub Delay(ByVal interval As Integer)
        'Delays process for number of milliseconds
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub

    Public Function DelayMinutes(ByVal Wait As Double) As Integer
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
        'Dim localpath As String
        Try
            Dim uriPath As String = System.IO.Path.GetDirectoryName( _
               System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
            'localpath = New Uri(uriPath).LocalPath
            Dim strFile As String = "C:\Satco\Logs\" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            File.AppendAllText(strFile, String.Format(Msg & " " & DateTime.Now & "  " & OperatorID & vbCrLf))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub AddOrderEntry(ByVal Msg As String)
        'Setup order log file
        'Dim localpath As String
        Try
            Dim uriPath As String = System.IO.Path.GetDirectoryName( _
               System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
            'localpath = New Uri(uriPath).LocalPath
            Dim strFile As String = "C:\Satco\Logs\Order" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            File.AppendAllText(strFile, String.Format(Msg & " " & DateTime.Now & "  " & OperatorID & vbCrLf))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub AddCardEntry(ByVal Msg As String)
    '    'Setup log file
    '    'Dim localpath As String
    '    Try
    '        Dim uriPath As String = System.IO.Path.GetDirectoryName(
    '           System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
    '        'localpath = New Uri(uriPath).LocalPath
    '        Dim strFile As String = "C:\Satco\Logs\Cards " & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
    '        File.AppendAllText(strFile, String.Format(Msg & vbCrLf))
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Sub DataPathInit()
        Try
            Dim rdr As StreamReader
            rdr = New StreamReader("c:\SATCO\SetPath.txt")
            Dim parts() As String = rdr.ReadLine().Split(","c)
            DBPath1 = parts(0)
            DBPath2 = parts(1)
            DefaultLocation = parts(2)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function Truncate(value As String, length As Integer) As String
        ' If argument is too big, return the original string.
        ' ... Otherwise take a substring from the string's start index.
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function

    Public Sub PrintExcel(Spreadsheet As String)
        'Print out an Hazmat Sheet
        Dim workbook As Workbook = New Workbook()
        Try
            'Load an Excel file
            workbook.LoadFromFile(SharePath & Spreadsheet)

            'Set the print controller to StandardPrintController, which will prevent print process from showing
            workbook.PrintDocument.PrintController = New StandardPrintController()

            workbook.PrintDocument.Print()

        Catch ex As Exception
            AddLogEntry(ex.Message)
        End Try
    End Sub

    Public Sub PrintLastTicket()
        Dim Code As String
        Dim ID As String
        Dim Net As Long
        Dim Tonnage As Single
        Dim Gallons As Single
        Dim Quantity As Single
        'Dim WordObj As Object
        'Dim Doc As Object
        Dim CommID As String
        Dim CommDesc As String
        Dim Freight As String = ""

        Dim Transaction As clsTransaction
        Dim Consignee As clsConsignee
        Dim Commodity As clsCommodity
        Dim Tank As clsTank
        Dim Driver As clsDriver
        Dim Mosaic As clsMosaic
        Dim Brenntag As clsBrenntag
        Dim SysOptions As clsSystem

        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)

        Transaction = New clsTransaction
        Commodity = New clsCommodity
        Driver = New clsDriver
        Consignee = New clsConsignee
        Tank = New clsTank
        SysOptions = New clsSystem
        Mosaic = New clsMosaic
        Brenntag = New clsBrenntag

        Try
            'WordObj = CreateObject("Word.Application")
            Code = NewPrinterCode
            ID = NewPrinterID
            If Code = "" Or ID = "" Then
                Transaction.GetFirstRecord()
                Code = Transaction.Code
                ID = Transaction.Id
            End If
            Transaction.FindRecord(ID, Code)
            Consignee.FindRecord(Code)

            Commodity.FindRecord(Transaction.Commodity)
            'get Commodity ID and Description and save to variables
            CommID = Commodity.ID
            CommDesc = Commodity.Description1


            Tank.FindRecord(Transaction.TankId)
            'Now get Commodity info related to tank info
            Commodity.FindRecord(Tank.Commodity)
            'Doc = ""

            If Mid(CommID, 1, 2) = "SA" Then
                If frmMain.optStockton.Checked = True Then
                    If Consignee.NSF = True Then
                        doc.LoadFromFile(SharePath & "Q172 SA BOL_NSF Stockton.docx")
                    Else
                        doc.LoadFromFile(SharePath & "Q171 SA BOL Stockton.docx")
                    End If
                Else
                    If Consignee.NSF = True Then
                        doc.LoadFromFile(SharePath & "Q095 SA BOL_NSF.docx")
                    Else
                        doc.LoadFromFile(SharePath & "Q091 SA BOL.docx")
                    End If
                End If

            End If
            If Val(CommID) >= 1 Or Mid(CommID, 1, 2) = "UC" Then
                doc.LoadFromFile(SharePath & "Q092 Nutrien Ag Solutions BOL.doc")
            End If
            If Mid(CommID, 1, 2) = "MO" Then
                doc.LoadFromFile(SharePath & "Q110 MO BOL.doc")
            End If
            If Mid(CommID, 1, 2) = "BN" Then
                doc.LoadFromFile(SharePath & "Q108 Brenntag BOL.doc")
            End If

            'Read from database for DriverInfo, MosaicCOA and BrenntagCOA
            SysOptions.GetCurrentRecord()

            'Find the 'bookmark' and add text
            navigator.MoveToBookmark("BOL")
            navigator.InsertText(Trim(Code) & "-" & Trim(ID))


            If Mid(CommID, 1, 2) <> "BN" Then
                navigator.MoveToBookmark("Desc1")
                navigator.InsertText(Mid(Consignee.Consignee, 1, 45))

                navigator.MoveToBookmark("Desc2")
                navigator.InsertText(Mid(Consignee.Destination, 1, 45))

                navigator.MoveToBookmark("Desc3")
                navigator.InsertText(Mid(Consignee.Destination2, 1, 45))
            End If

            If Mid(CommID, 1, 2) = "SA" Then
                navigator.MoveToBookmark("Consignee")
                navigator.InsertText(Code)
                navigator.MoveToBookmark("Strength")
                If Transaction.Analysis = "" Then
                    navigator.InsertText(Commodity.Description2)
                Else
                    navigator.InsertText(Transaction.Analysis)
                End If

                navigator.MoveToBookmark("Gravity")
                If Transaction.Desc3 = "" Then
                    navigator.InsertText(Commodity.Description3)
                Else
                    navigator.InsertText(Transaction.Desc3)
                End If

                navigator.MoveToBookmark("Iron")
                If Transaction.Desc4 = "" Then
                    navigator.InsertText(Commodity.Description4)
                Else
                    navigator.InsertText(Transaction.Desc4)
                End If

                navigator.MoveToBookmark("TankID")
                navigator.InsertText(Transaction.TankId)
                navigator.MoveToBookmark("ScaleID")
                navigator.InsertText(Str(Transaction.ScaleNumber))
                navigator.MoveToBookmark("PO")
                navigator.InsertText(Transaction.PO)
                If Consignee.NSF = True Then
                    navigator.MoveToBookmark("Seal1")
                    navigator.InsertText(Transaction.Seal1)
                    navigator.MoveToBookmark("Seal2")
                    navigator.InsertText(Transaction.Seal2)
                End If
            End If

            If Val(CommID) >= 1 Or Mid(CommID, 1, 2) = "UC" Then  'UC
                navigator.MoveToBookmark("lblMCDC")
                If Val(Tank.Id) = 15 Then
                    navigator.InsertText("DCDS")
                Else
                    navigator.InsertText("MCDS")
                End If
                navigator.MoveToBookmark("Consignee")
                navigator.InsertText(Code)
                navigator.MoveToBookmark("TankID")
                navigator.InsertText(Transaction.TankId)
                navigator.MoveToBookmark("ScaleID")
                navigator.InsertText(Str(Transaction.ScaleNumber))
                navigator.MoveToBookmark("Commodity")
                navigator.InsertText(CommDesc)
                navigator.MoveToBookmark("LabTech")
                If Transaction.Desc5 = "" Then
                    navigator.InsertText(Commodity.Description5)
                Else
                    navigator.InsertText(Transaction.Desc5)
                End If

                navigator.MoveToBookmark("MCDS")
                If Transaction.Desc3 = "" Then
                    navigator.InsertText(Commodity.Description3)
                Else
                    navigator.InsertText(Transaction.Desc3)
                End If

                navigator.MoveToBookmark("ProductionDate")
                If Transaction.Desc2 = "" Then
                    navigator.InsertText(Commodity.Description2)
                Else
                    navigator.InsertText(Transaction.Desc2)
                End If

                navigator.MoveToBookmark("Release")
                navigator.InsertText(Transaction.ReleaseNumber)
            End If

            If Mid(CommID, 1, 2) = "MO" Then
                'If Mosaic.FindRecord(Transaction.ReleaseNumber) = True Then
                navigator.MoveToBookmark("Release")
                navigator.InsertText(Transaction.ReleaseNumber)
                'End If
            End If

            If Mid(CommID, 1, 2) = "BN" Then

                If Brenntag.FindRecord(Transaction.ReleaseNumber) = True Then
                    navigator.MoveToBookmark("PO")
                    navigator.InsertText(Brenntag.PO)
                    navigator.MoveToBookmark("AltPO")
                    navigator.InsertText(Brenntag.AltPO)
                    navigator.MoveToBookmark("AltBOL")
                    navigator.InsertText(Brenntag.BOL)
                    navigator.MoveToBookmark("AltCode")
                    navigator.InsertText(Brenntag.AltCode)
                    navigator.MoveToBookmark("Name")
                    navigator.InsertText(Brenntag.Name)
                    navigator.MoveToBookmark("Address1")
                    navigator.InsertText(Brenntag.Address1)
                    navigator.MoveToBookmark("Address2")
                    navigator.InsertText(Brenntag.Address2)
                    navigator.MoveToBookmark("CSZ")
                    navigator.InsertText(Brenntag.CSZ)

                    navigator.MoveToBookmark("Seal1")
                    navigator.InsertText(Transaction.Seal1 & "")
                    navigator.MoveToBookmark("Seal2")
                    navigator.InsertText(Transaction.Seal2 & "")
                    If Transaction.Seal3 <> "" Then
                        navigator.MoveToBookmark("Seal3")
                        navigator.InsertText(Transaction.Seal3)
                    End If
                    If Transaction.Seal4 <> "" Then
                        navigator.MoveToBookmark("Seal4")
                        navigator.InsertText(Transaction.Seal4)
                    End If
                    navigator.MoveToBookmark("Release")
                    navigator.InsertText(Transaction.ReleaseNumber)
                    If Transaction.Analysis = "25%" Then
                        navigator.MoveToBookmark("NaOH")
                        navigator.InsertText(SysOptions.BTCOA1)
                        navigator.MoveToBookmark("Na2O")
                        navigator.InsertText(SysOptions.BTCOA2)
                        navigator.MoveToBookmark("PCName")
                        navigator.InsertText(Transaction.Adjustment)
                        'Print dillution details
                        navigator.MoveToBookmark("PCWater")
                        navigator.InsertText("H2O   (lbs): " & Transaction.Desc4)
                        navigator.MoveToBookmark("PCCaustic")
                        navigator.InsertText("Na2O (lbs): " & Transaction.Desc5)
                    Else
                        navigator.MoveToBookmark("NaOH")
                        navigator.InsertText(SysOptions.BNCOA1)
                        navigator.MoveToBookmark("Na2O")
                        navigator.InsertText(SysOptions.BNCOA2)
                        navigator.MoveToBookmark("PCName")
                        navigator.InsertText(Transaction.Adjustment)
                    End If
                Else
                    AddLogEntry("Brenntag record exists in Transaction Table but not in Brenntag Table")
                End If
            End If

            Driver.FindRecord(Transaction.DriverId)
            navigator.MoveToBookmark("Carrier")
            navigator.InsertText(Driver.Carrier)
            navigator.MoveToBookmark("VehicleID")
            navigator.InsertText(Transaction.VehicleID)
            navigator.MoveToBookmark("TrailerID")
            navigator.InsertText(Transaction.TrailerID)
            navigator.MoveToBookmark("Date")
            navigator.InsertText(Format(Transaction.TDate, "MM/dd/yyyy"))
            navigator.MoveToBookmark("OutTime")
            navigator.InsertText(Transaction.OutTime)

            If Mid(CommID, 1, 2) <> "MO" Then
                navigator.MoveToBookmark("InTime")
                navigator.InsertText(Transaction.InTime)
                navigator.MoveToBookmark("Driver")
                navigator.InsertText(Driver.Name)
            End If

            If Mid(CommID, 1, 2) = "BN" Then
                navigator.MoveToBookmark("Driver2")
                navigator.InsertText(Driver.Name)
            End If

            If frmMain.optStockton.Checked = True Then
                navigator.MoveToBookmark("ScaleTicket")
                navigator.InsertText("Certified Weigh Ticket # " & Transaction.ScaleTicket)
                navigator.MoveToBookmark("Gross")
                navigator.InsertText(" Gross Wt Lbs):    " & Format(Transaction.Gross, "####0"))
                navigator.MoveToBookmark("Tare")
                navigator.InsertText("Tare Wt (Lbs):    " & Format(Transaction.Tare, "####0"))
                navigator.MoveToBookmark("Net")
                Net = Transaction.Gross - Transaction.Tare
                Tonnage = Net / 2000
                navigator.InsertText("Net Wt (Lbs):    " & Format(Net, "####0"))

                navigator.MoveToBookmark("Tons")
                navigator.InsertText("Net Wt (Tons):    " & Format(Tonnage, "##.##"))
            Else
                navigator.MoveToBookmark("Gross")
                navigator.InsertText(Format(Transaction.Gross, "##,##0"))
                navigator.MoveToBookmark("Tare")
                navigator.InsertText(Format(Transaction.Tare, "##,##0"))
                navigator.MoveToBookmark("Net")
                Net = Transaction.Gross - Transaction.Tare
                Tonnage = Net / 2000
                navigator.InsertText(Format(Net, "##,##0"))
                navigator.MoveToBookmark("Tons")
                navigator.InsertText(Format(Tonnage, "##.000"))
            End If

            If Mid(CommID, 1, 2) = "BN" Then
                navigator.MoveToBookmark("Tons1")
                navigator.InsertText(Format(Tonnage, "##.000"))
            End If

            If Val(CommID) >= 1 Or Mid(CommID, 1, 2) = "UC" Then
                If Val(Transaction.Desc4) = 0 Then
                    Gallons = 0
                Else
                    Gallons = Net / Val(Transaction.Desc4)
                End If
                navigator.MoveToBookmark("Volume")
                navigator.InsertText(Format(Gallons, "#,###.00"))
                Quantity = Gallons * 3.785
                navigator.MoveToBookmark("Quantity")
                navigator.InsertText(Format(Quantity, "#,###.00"))
            End If

            'Now print it out!
            Dim printDoc As PrintDocument = doc.PrintDocument
            printDoc.PrintController = New StandardPrintController()
            printDoc.Print()

            Transaction = Nothing
            Commodity = Nothing
            Driver = Nothing
            Consignee = Nothing
            Tank = Nothing
            SysOptions = Nothing
            Mosaic = Nothing
            Brenntag = Nothing

        Catch ex As Exception
            MsgBox("PrintLastTicket: " & ex.Message)
            AddLogEntry("PrintLastTicket: " & ex.Message)

            Transaction = Nothing
            Commodity = Nothing
            Driver = Nothing
            Consignee = Nothing
            Tank = Nothing
            SysOptions = Nothing
            Mosaic = Nothing
            Brenntag = Nothing
        End Try
    End Sub

    Public Sub PrintRailTicket()
        Dim Code As String
        Dim ID As String
        'Dim Net As Long
        Dim Transaction As clsTransaction
        Dim Rail As clsRail
        Dim Consignee As clsConsignee
        Dim Commodity As clsCommodity
        Dim Tank As clsTank
        Dim Driver As clsDriver
        Dim SysOptions As clsSystem
        Dim Brenntag As clsBrenntag
        Dim CommID As String
        Dim CommDesc As String
        Dim TotalCars As Integer = 0
        Dim Tons As Single
        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)

        Try
            Transaction = New clsTransaction
            Rail = New clsRail
            Commodity = New clsCommodity
            Driver = New clsDriver
            Consignee = New clsConsignee
            Tank = New clsTank
            SysOptions = New clsSystem
            Brenntag = New clsBrenntag
            Code = NewPrinterCode
            ID = NewPrinterID
            If Code = "" Or ID = "" Then
                Transaction.GetFirstRecord()
                Code = Transaction.Code
                ID = Transaction.Id
            End If
            Transaction.FindRecord(ID, Code)
            Consignee.FindRecord(Code)

            Commodity.FindRecord(Transaction.Commodity)
            'get Commodity ID and Description and save to variables
            CommID = Commodity.ID
            CommDesc = Commodity.Description1

            Tank.FindRecord(Transaction.TankId)
            'Now get Commodity info related to tank info
            Commodity.FindRecord(Tank.Commodity)

            If frmMain.optStockton.Checked = True Then
                doc.LoadFromFile(SharePath & "Q182 SA BOL Rail - Stockton.docx")
            Else
                doc.LoadFromFile(SharePath & "Q181 SA BOL Rail - Tampa.docx")
            End If

            'Find the 'bookmark' and add text
            navigator.MoveToBookmark("BOL")
            navigator.InsertText(Trim(Code) & "-" & Trim(ID))

            navigator.MoveToBookmark("Consignee")
            navigator.InsertText(Code)

            If Mid(Code, 1, 2) = "BN" Then
                navigator.MoveToBookmark("Release")
                navigator.InsertText(Transaction.ReleaseNumber)
                'Search for Brenntag Info
                Brenntag.FindRecord(Transaction.ReleaseNumber)
                navigator.MoveToBookmark("Desc1")
                navigator.InsertText(Brenntag.Name)
                navigator.MoveToBookmark("Desc2")
                navigator.InsertText(Brenntag.Address1)
                navigator.MoveToBookmark("Desc3")
                navigator.InsertText(Brenntag.Address2)
                navigator.MoveToBookmark("Desc4")
                navigator.InsertText(Brenntag.CSZ)

            Else

                navigator.MoveToBookmark("Desc1")
                navigator.InsertText(Consignee.Consignee)

                navigator.MoveToBookmark("Desc2")
                navigator.InsertText(Mid(Consignee.Destination, 1, 50))

                navigator.MoveToBookmark("Desc3")
                navigator.InsertText(Consignee.Destination2)

            End If

            navigator.MoveToBookmark("Commodity")
            navigator.InsertText(CommDesc)
            navigator.MoveToBookmark("Strength")
            If Transaction.Desc2 = "" Then
                navigator.InsertText(Commodity.Description2)
            Else
                navigator.InsertText(Transaction.Desc2)
            End If

            navigator.MoveToBookmark("Gravity")
            If Transaction.Desc3 = "" Then
                navigator.InsertText(Commodity.Description3)
            Else
                navigator.InsertText(Transaction.Desc3)
            End If

            navigator.MoveToBookmark("Iron")
            If Transaction.Desc4 = "" Then
                navigator.InsertText(Commodity.Description4)
            Else
                navigator.InsertText(Transaction.Desc4)
            End If

            navigator.MoveToBookmark("TankID")
            navigator.InsertText(Transaction.TankId)

            'Add PO
            navigator.MoveToBookmark("PO")
            navigator.InsertText(Transaction.PO)

            Driver.FindRecord(Transaction.DriverId)

            navigator.MoveToBookmark("Date")
            navigator.InsertText(Format(Transaction.TDate, "MM/dd/yyyy"))
            navigator.MoveToBookmark("Time")
            navigator.InsertText(Transaction.InTime)
            'navigator.MoveToBookmark("OutTime").Select
            'navigator.InsertText Transaction.OutTime
            navigator.MoveToBookmark("Driver")
            navigator.InsertText(Driver.Name)

            'Car1
            If Rail.FindRecord(Code, ID, 1) = True Then
                TotalCars = 1
                navigator.MoveToBookmark("Prefix1")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car1")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net1")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton1")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal11")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal12")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal13")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal14")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car2
            If Rail.FindRecord(Code, ID, 2) = True Then
                TotalCars = 2
                navigator.MoveToBookmark("Prefix2")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car2")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net2")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton2")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal21")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal22")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal23")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal24")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car3
            If Rail.FindRecord(Code, ID, 3) = True Then
                TotalCars = 3
                navigator.MoveToBookmark("Prefix3")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car3")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net3")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton3")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal31")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal32")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal33")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal34")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car4
            If Rail.FindRecord(Code, ID, 4) = True Then
                TotalCars = 4
                navigator.MoveToBookmark("Prefix4")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car4")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net4")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton4")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal41")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal42")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal43")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal44")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car5
            If Rail.FindRecord(Code, ID, 5) = True Then
                TotalCars = 5
                navigator.MoveToBookmark("Prefix5")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car5")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net5")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton5")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal51")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal52")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal53")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal54")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car6
            If Rail.FindRecord(Code, ID, 6) = True Then
                TotalCars = 6
                navigator.MoveToBookmark("Prefix6")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car6")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net6")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton6")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal61")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal62")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal63")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal64")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car7
            If Rail.FindRecord(Code, ID, 7) = True Then
                TotalCars = 7
                navigator.MoveToBookmark("Prefix7")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car7")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net7")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton7")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal71")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal72")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal73")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal74")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car8
            If Rail.FindRecord(Code, ID, 8) = True Then
                TotalCars = 8
                navigator.MoveToBookmark("Prefix8")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car8")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net8")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton8")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal81")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal82")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal83")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal84")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car9
            If Rail.FindRecord(Code, ID, 9) = True Then
                TotalCars = 9
                navigator.MoveToBookmark("Prefix9")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car9")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net9")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton9")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal91")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal92")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal93")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal94")
                navigator.InsertText(Rail.Seal4)
            End If

            'Car10
            If Rail.FindRecord(Code, ID, 10) = True Then
                TotalCars = 10
                navigator.MoveToBookmark("Prefix10")
                navigator.InsertText(Rail.CarPrefix)
                navigator.MoveToBookmark("Car10")
                navigator.InsertText(Rail.Car)
                navigator.MoveToBookmark("Net10")
                navigator.InsertText(Format(Rail.NetWt, "#,###,##0"))
                Tons = Rail.NetWt / 2000
                navigator.MoveToBookmark("Ton10")
                navigator.InsertText(Format(Tons, "####.00"))
                navigator.MoveToBookmark("Seal01")
                navigator.InsertText(Rail.Seal1)
                navigator.MoveToBookmark("Seal02")
                navigator.InsertText(Rail.Seal2)
                navigator.MoveToBookmark("Seal03")
                navigator.InsertText(Rail.Seal3)
                navigator.MoveToBookmark("Seal04")
                navigator.InsertText(Rail.Seal4)
            End If

            navigator.MoveToBookmark("TotalNet")
            navigator.InsertText(Format(Transaction.Net, "#,###,##0"))
            Tons = Transaction.Net / 2000
            navigator.MoveToBookmark("TotalTon")
            navigator.InsertText(Format(Tons, "####.##"))
            navigator.MoveToBookmark("TotalCars")
            navigator.InsertText(Format(TotalCars, "##"))

            'Now print it out!
            Dim printDoc As PrintDocument = doc.PrintDocument
            printDoc.PrintController = New StandardPrintController()
            printDoc.Print()

            Transaction = Nothing
            Commodity = Nothing
            Driver = Nothing
            Consignee = Nothing
            Tank = Nothing
            SysOptions = Nothing
            Brenntag = Nothing

        Catch ex As Exception
            MsgBox("PrintRailTicket: " & ex.Message)
            AddLogEntry("PrintRailTicket: " & ex.Message)
            Transaction = Nothing
            Commodity = Nothing
            Driver = Nothing
            Consignee = Nothing
            Tank = Nothing
            SysOptions = Nothing
            Brenntag = Nothing
        End Try
    End Sub

    Public Sub PrintWeighIn(ID As String)
        Dim Ticket As String
        Dim Net As Long
        Dim Tonnage As Single
        Dim WeighIn As clsWeighIn
        Dim SysOptions As clsSystem
        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)


        WeighIn = New clsWeighIn
        SysOptions = New clsSystem
        WeighIn.FindRecord(ID)

        doc.LoadFromFile(SharePath & "Q093 Weight Ticket.doc")
        'Fill in the blanks

        Ticket = SysOptions.ManualTicket + 1
        SysOptions.ManualTicket = Ticket

        'Find the 'bookmark' and add text
        navigator.MoveToBookmark("Ticket")
        navigator.InsertText(Ticket)

        navigator.MoveToBookmark("VehicleID")
        navigator.InsertText(WeighIn.VehicleId)
        navigator.MoveToBookmark("TrailerID")
        navigator.InsertText(WeighIn.TrailerID)

        navigator.MoveToBookmark("Date")
        navigator.InsertText(Format(WeighIn.DateTime, "MM/dd/yyyy"))
        navigator.MoveToBookmark("Time")
        navigator.InsertText(Format(WeighIn.DateTime, "hh:mm"))

        navigator.MoveToBookmark("Gross")
        navigator.InsertText(Format(WeighIn.Gross, "##,##0"))
        navigator.MoveToBookmark("Tare")
        navigator.InsertText(Format(WeighIn.Tare, "##,##0"))
        navigator.MoveToBookmark("Net")
        Net = WeighIn.Gross - WeighIn.Tare
        Tonnage = Net / 2000
        navigator.InsertText(Format(Net, "##,##0"))
        navigator.MoveToBookmark("Tons")
        navigator.InsertText(Format(Tonnage, "##.##"))

        'Now print it out!
        Dim printDoc As PrintDocument = doc.PrintDocument
        printDoc.PrintController = New StandardPrintController()
        printDoc.Print()

        WeighIn = Nothing
        SysOptions = Nothing

    End Sub

    Public Sub PrintWordDoc(WordDoc As String)
        'Print out a Word Document
        Dim doc As New Document

        doc.LoadFromFile(SharePath & WordDoc)

        'Now print it out!
        Dim printDoc As PrintDocument = doc.PrintDocument
        printDoc.PrintController = New StandardPrintController()
        printDoc.Print()

    End Sub

    Public Sub PrintBNCoA25(WordDoc As String)
        Dim SysOptions As clsSystem
        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)

        SysOptions = New clsSystem
        doc.LoadFromFile(SharePath & WordDoc)

        'Read from database for BrenntagCOA
        SysOptions.GetCurrentRecord()

        navigator.MoveToBookmark("COA1")
        navigator.InsertText(SysOptions.BTCOA1)
        navigator.MoveToBookmark("COA2")
        navigator.InsertText(SysOptions.BTCOA2)
        navigator.MoveToBookmark("COA3")
        navigator.InsertText(SysOptions.BTCOA3)
        navigator.MoveToBookmark("COA4")
        navigator.InsertText(SysOptions.BTCOA4)
        navigator.MoveToBookmark("COA5")
        navigator.InsertText(SysOptions.BTCOA5)
        navigator.MoveToBookmark("COA6")
        navigator.InsertText(SysOptions.BTCOA6)
        navigator.MoveToBookmark("COA7")
        navigator.InsertText(SysOptions.BTCOA7)
        navigator.MoveToBookmark("COA8")
        navigator.InsertText(SysOptions.BTCOA10)
        navigator.MoveToBookmark("COA9")
        navigator.InsertText(SysOptions.BTCOA11)
        navigator.MoveToBookmark("COA10")
        navigator.InsertText(SysOptions.BTCOA12)
        navigator.MoveToBookmark("COA11")
        navigator.InsertText(SysOptions.BTCOA13)
        navigator.MoveToBookmark("COA12")
        navigator.InsertText(SysOptions.BTCOA14)
        'navigator.MoveToBookmark("COA13").Select
        'navigator.InsertText SysOptions.BTCOA13
        'navigator.MoveToBookmark("COA14").Select
        'navigator.InsertText SysOptions.BTCOA14
        navigator.MoveToBookmark("COA0")
        navigator.InsertText(SysOptions.BTGravity)


        'Now print it out!
        Dim printDoc As PrintDocument = doc.PrintDocument
        printDoc.PrintController = New StandardPrintController()
        printDoc.Print()

        SysOptions = Nothing

    End Sub

    Public Sub PrintBNCoA50(WordDoc As String)
        Dim SysOptions As clsSystem
        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)

        SysOptions = New clsSystem
        doc.LoadFromFile(SharePath & WordDoc)

        'Read from database for BrenntagCOA
        SysOptions.GetCurrentRecord()

        navigator.MoveToBookmark("COA1")
        navigator.InsertText(SysOptions.BNCOA1)
        navigator.MoveToBookmark("COA2")
        navigator.InsertText(SysOptions.BNCOA2)
        navigator.MoveToBookmark("COA3")
        navigator.InsertText(SysOptions.BNCOA3)
        navigator.MoveToBookmark("COA4")
        navigator.InsertText(SysOptions.BNCOA4)
        navigator.MoveToBookmark("COA5")
        navigator.InsertText(SysOptions.BNCOA5)
        navigator.MoveToBookmark("COA6")
        navigator.InsertText(SysOptions.BNCOA6)
        navigator.MoveToBookmark("COA7")
        navigator.InsertText(SysOptions.BNCOA7)
        navigator.MoveToBookmark("COA8")
        navigator.InsertText(SysOptions.BNCOA10)
        navigator.MoveToBookmark("COA9")
        navigator.InsertText(SysOptions.BNCOA11)
        navigator.MoveToBookmark("COA10")
        navigator.InsertText(SysOptions.BNCOA12)
        navigator.MoveToBookmark("COA11")
        navigator.InsertText(SysOptions.BNCOA13)
        navigator.MoveToBookmark("COA12")
        navigator.InsertText(SysOptions.BNCOA14)
        'navigator.MoveToBookmark("COA13").Select
        'navigator.InsertText SysOptions.BNCOA13
        'navigator.MoveToBookmark("COA14").Select
        'navigator.InsertText SysOptions.BNCOA14
        navigator.MoveToBookmark("COA0")
        navigator.InsertText(SysOptions.BNGravity)


        'Now print it out!
        Dim printDoc As PrintDocument = doc.PrintDocument
        printDoc.PrintController = New StandardPrintController()
        printDoc.Print()

        SysOptions = Nothing

    End Sub

    Public Sub PrintBNNotes(WordDoc As String)
        Dim Brenntag As clsBrenntag
        'Dim SysOptions As clsSystem
        Dim Transaction As clsTransaction
        Dim Consignee As clsConsignee
        Dim Code As String
        Dim ID As String
        Dim doc As New Document
        Dim navigator As New BookmarksNavigator(doc)

        Transaction = New clsTransaction
        Consignee = New clsConsignee
        Brenntag = New clsBrenntag

        Code = NewPrinterCode
        ID = NewPrinterID
        If Code = "" Or ID = "" Then
            Transaction.GetFirstRecord()
            Code = Transaction.Code
            ID = Transaction.Id
        End If
        Transaction.FindRecord(ID, Code)

        doc.LoadFromFile(SharePath & "Brenntag Notes.doc")

        Brenntag.FindRecord(Transaction.ReleaseNumber)
        navigator.MoveToBookmark("Notes")
        navigator.InsertText(Brenntag.Notes)

        'Now print it out!
        Dim printDoc As PrintDocument = doc.PrintDocument
        printDoc.PrintController = New StandardPrintController()
        printDoc.Print()

    End Sub

End Module
