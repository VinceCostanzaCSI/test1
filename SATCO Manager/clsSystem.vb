Imports System.Reflection
Imports System.IO

Public Class clsSystem
    Const MESSAGE_TXT = "Received, Subject to the Classification and Tariffs in effect on the date of the issue of this RECEIPT"

    Dim Section As String
    Dim location = Assembly.GetExecutingAssembly().Location
    Dim appPath = Path.GetDirectoryName(location)       ' C:\Some\Directory
    Dim appName = Path.GetFileName(location)            ' MyLibrary.DLL

    Property DatabasePath() As String
        Get
            Section = "System\Paths"
            DatabasePath = GetSetting(appName, Section, "DatabasePath", appPath & "\DATA\SATCO.MDB")
        End Get
        Set(value As String)
            Section = "System\Paths"
            SaveSetting(appName, Section, "DatabasePath", value)
        End Set
    End Property
    Property ReportPath() As String
        Get
            Section = "System\Paths"
            ReportPath = GetSetting(appName, Section, "ReportPath", appPath & "\DATA\SATCO.MDB")
        End Get
        Set(value As String)
            Section = "System\Paths"
            SaveSetting(appName, Section, "ReportPath", value)
        End Set
    End Property
    Property DocumentPath() As String
        Get
            Section = "System\Paths"
            DocumentPath = GetSetting(appName, Section, "DocumentPath", appPath & "\DATA\SATCO.MDB")
        End Get
        Set(value As String)
            Section = "System\Paths"
            SaveSetting(appName, Section, "DocumentPath", value)
        End Set
    End Property
    Property WatchPath() As String
        Get
            Section = "System\Paths"
            WatchPath = GetSetting(appName, Section, "WatchPath", appPath & "\DATA\SATCO.MDB")
        End Get
        Set(value As String)
            Section = "System\Paths"
            SaveSetting(appName, Section, "WatchPath", value)
        End Set
    End Property
    Property ScaleIP() As String
        Get
            Section = "System\Scale"
            ScaleIP = GetSetting(appName, Section, "IP", "100.100.100.000")
        End Get
        Set(value As String)
            Section = "System\Scale"
            SaveSetting(appName, Section, "IP", value)
        End Set
    End Property
    Property LogProcess() As Integer
        Get
            Section = "System"
            LogProcess = GetSetting(appName, Section, "Log", 0)
        End Get
        Set(value As Integer)
            Section = "System"
            SaveSetting(appName, Section, "Log", value)
        End Set
    End Property
    Property ScalePort() As Integer
        Get
            Section = "System\Scale"
            ScalePort = GetSetting(appName, Section, "Port", "0")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Port", value)
        End Set
    End Property
    Property ScaleBaud() As Integer
        Get
            Section = "System\Scale"
            ScaleBaud = GetSetting(appName, Section, "Baud", "3")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Baud", value)
        End Set
    End Property
    Public Property ScaleParity() As Integer
        Get
            Section = "System\Scale"
            ScaleParity = GetSetting(appName, Section, "Parity", "0")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Parity", value)
        End Set
    End Property
    Public Property ScaleDataBits() As Integer
        Get
            Section = "System\Scale"
            ScaleDataBits = GetSetting(appName, Section, "DataBits", "1")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "DataBits", value)
        End Set
    End Property
    Public Property ScaleStopBits() As Integer
        Get
            Section = "System\Scale"
            ScaleStopBits = GetSetting(appName, Section, "StopBits", "0")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "StopBits", value)
        End Set
    End Property
    Public Property ScaleNumber() As Integer
        Get
            Section = "System\Scale"
            ScaleNumber = GetSetting(appName, Section, "Number", "1")
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Number", value)
        End Set
    End Property
    Public Property ScaleActive() As Integer
        Get
            Section = "System\Scale"
            ScaleActive = GetSetting(appName, Section, "Active", 0)
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Active", value)
        End Set
    End Property
    Public Property ScaleStatus() As Integer
        Get
            Section = "System\Scale"
            ScaleStatus = GetSetting(appName, Section, "Status", 0)
        End Get
        Set(value As Integer)
            Section = "System\Scale"
            SaveSetting(appName, Section, "Status", value)
        End Set
    End Property
    Public Property CurrentTab() As Integer
        Get
            Section = "System"
            CurrentTab = GetSetting(appName, Section, "Current Tab", "0")
        End Get
        Set(value As Integer)
            Section = "System"
            SaveSetting(appName, Section, "Current Tab", value)
        End Set
    End Property
    Public Property Zip() As String
        Get
            Section = "System\Company"
            Zip = GetSetting(appName, Section, "Zip", "33607")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "Zip", value)
        End Set
    End Property
    Public Property State() As String
        Get
            Section = "System\Company"
            State = GetSetting(appName, Section, "State", "FL")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "State", value)
        End Set
    End Property
    Public Property City() As String
        Get
            Section = "System\Company"
            City = GetSetting(appName, Section, "City", "TAMPA")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "City", value)
        End Set
    End Property
    Public Property Address1() As String
        Get
            Section = "System\Company"
            Address1 = GetSetting(appName, Section, "Address1", "WHATEVER STREET")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "Address1", value)
        End Set
    End Property
    Public Property Address2() As String
        Get
            Section = "System\Company"
            Address2 = GetSetting(appName, Section, "Address2", "WHATEVER STREET")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "Address2", value)
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Section = "System\Company"
            CompanyName = GetSetting(appName, Section, "Company Name", "SATCO")
        End Get
        Set(value As String)
            Section = "System\Company"
            SaveSetting(appName, Section, "Company Name", value)
        End Set
    End Property
    Public Property ActiveOperator() As String
        Get
            Section = "System"
            ActiveOperator = GetSetting(appName, Section, "Active Operator", "")
        End Get
        Set(value As String)
            Section = "System"
            SaveSetting(appName, Section, "Active Operator", value)
        End Set
    End Property
    Public Property ArchiveDate() As String
        Get
            Section = "System"
            ArchiveDate = GetSetting(appName, Section, "Archive Date", Format(Now, "MM/dd/yyyy"))
        End Get
        Set(value As String)
            Section = "System"
            SaveSetting(appName, Section, "Archive Date", value)
        End Set
    End Property
    Public Property TareWeightHigh() As Long
        Get
            Section = "System\Variables"
            TareWeightHigh = GetSetting(appName, Section, "TareWeightHigh", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "TareWeightHigh", value)
        End Set
    End Property
    Public Property TareWeightLow() As Long
        Get
            Section = "System\Variables"
            TareWeightLow = GetSetting(appName, Section, "TareWeightLow", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "TareWeightLow", value)
        End Set
    End Property
    Public Property BNCapacityMin() As Long
        Get
            Section = "System\Variables"
            BNCapacityMin = GetSetting(appName, Section, "BNCapacityMin", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "BNCapacityMin", value)
        End Set
    End Property
    Public Property BNCapacityMax() As Long
        Get
            Section = "System\Variables"
            BNCapacityMax = GetSetting(appName, Section, "BNCapacityMax", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "BNCapacityMax", value)
        End Set
    End Property
    Public Property MaxFillWeight() As Long
        Get
            Section = "System\Variables"
            MaxFillWeight = GetSetting(appName, Section, "MaxFillWeight", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "MaxFillWeight", value)
        End Set
    End Property
    Public Property VariableWt() As Long
        Get
            Section = "System\Variables"
            VariableWt = GetSetting(appName, Section, "VariableWt", 0)
        End Get
        Set(value As Long)
            Section = "System\Variables"
            SaveSetting(appName, Section, "VariableWt", value)
        End Set
    End Property
    Public Property ReadDelay() As Integer
        Get
            Section = "System\Variables"
            ReadDelay = GetSetting(appName, Section, "ReadDelay", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "ReadDelay", value)
        End Set
    End Property
    Public Property SANumberOfTickets() As Integer
        Get
            Section = "System\Variables"
            SANumberOfTickets = GetSetting(appName, Section, "SANumberOfTickets", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "SANumberOfTickets", value)
        End Set
    End Property
    Public Property UCNumberOfTickets() As Integer
        Get
            Section = "System\Variables"
            UCNumberOfTickets = GetSetting(appName, Section, "UCNumberOfTickets", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "UCNumberOfTickets", value)
        End Set
    End Property
    Public Property CNNumberOfTickets() As Integer
        Get
            Section = "System\Variables"
            CNNumberOfTickets = GetSetting(appName, Section, "CNNumberOfTickets", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "CNNumberOfTickets", value)
        End Set
    End Property
    Public Property BNNumberOfTickets() As Integer
        Get
            Section = "System\Variables"
            BNNumberOfTickets = GetSetting(appName, Section, "BNNumberOfTickets", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "BNNumberOfTickets", value)
        End Set
    End Property
    Public Property HazmatActive() As Integer
        Get
            Section = "System\Variables"
            HazmatActive = GetSetting(appName, Section, "HazmatActive", 0)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "HazmatActive", value)
        End Set
    End Property
    Public Property ManualTicket() As Integer
        Get
            Section = "System\Variables"
            ManualTicket = GetSetting(appName, Section, "ManualTicket", 2)
        End Get
        Set(value As Integer)
            Section = "System\Variables"
            SaveSetting(appName, Section, "ManualTicket", value)
        End Set
    End Property
    Public Property CNDate() As String
        Get
            Section = "System\Yara"
            CNDate = GetSetting(appName, Section, "CN Date", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN Date", value)
        End Set
    End Property
    Public Property CNProduct() As String
        Get
            Section = "System\Yara"
            CNProduct = GetSetting(appName, Section, "CN Product", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN Product", value)
        End Set
    End Property
    Public Property CNBatch() As String
        Get
            Section = "System\Yara"
            CNBatch = GetSetting(appName, Section, "CN Batch", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN Batch", value)
        End Set
    End Property
    Public Property CNNitrate() As String
        Get
            Section = "System\Yara"
            CNNitrate = GetSetting(appName, Section, "CN Nitrate", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN Nitrate", value)
        End Set
    End Property
    Public Property CNGravity() As String
        Get
            Section = "System\Yara"
            CNGravity = GetSetting(appName, Section, "CN Gravity", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN Gravity", value)
        End Set
    End Property
    Public Property CNpH() As String
        Get
            Section = "System\Yara"
            CNpH = GetSetting(appName, Section, "CN pH", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN pH", value)
        End Set
    End Property
    Public Property CN1Date() As String
        Get
            Section = "System\Yara"
            CN1Date = GetSetting(appName, Section, "CN1 Date", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Date", value)
        End Set
    End Property
    Public Property CN1Product() As String
        Get
            Section = "System\Yara"
            CN1Product = GetSetting(appName, Section, "CN1 Product", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Product", value)
        End Set
    End Property
    Public Property CN1Batch() As String
        Get
            Section = "System\Yara"
            CN1Batch = GetSetting(appName, Section, "CN1 Batch", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Batch", value)
        End Set
    End Property
    Public Property CN1Nitrate() As String
        Get
            Section = "System\Yara"
            CN1Nitrate = GetSetting(appName, Section, "CN1 Nitrate", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Nitrate", value)
        End Set
    End Property
    Public Property CN1Gravity() As String
        Get
            Section = "System\Yara"
            CN1Gravity = GetSetting(appName, Section, "CN1 Gravity", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Gravity", value)
        End Set
    End Property
    Public Property CN1pH() As String
        Get
            Section = "System\Yara"
            CN1pH = GetSetting(appName, Section, "CN1 pH", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 pH", value)
        End Set
    End Property
    Public Property CN1Calcium() As String
        Get
            Section = "System\Yara"
            CN1Calcium = GetSetting(appName, Section, "CN1 Calcium", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Calcium", value)
        End Set
    End Property
    Public Property CN1Density() As String
        Get
            Section = "System\Yara"
            CN1Density = GetSetting(appName, Section, "CN1 Density", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Density", value)
        End Set
    End Property
    Public Property CN1Turbidity() As String
        Get
            Section = "System\Yara"
            CN1Turbidity = GetSetting(appName, Section, "CN1 Turbidity", "")
        End Get
        Set(value As String)
            Section = "System\Yara"
            SaveSetting(appName, Section, "CN1 Turbidity", value)
        End Set
    End Property
    Public Property BNGravity() As String
        Get
            Section = "System\Brenntag"
            BNGravity = GetSetting(appName, Section, "BN Gravity", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BN Gravity", value)
        End Set
    End Property
    Public Property BNppg() As String
        Get
            Section = "System\Brenntag"
            BNppg = GetSetting(appName, Section, "BN Pounds per Gallon", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BN Pounds per Gallon", value)
        End Set
    End Property
    Public Property BNCOA1() As String
        Get
            Section = "System\Brenntag"
            BNCOA1 = GetSetting(appName, Section, "BNCOA1", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA1", value)
        End Set
    End Property
    Public Property BNCOA2() As String
        Get
            Section = "System\Brenntag"
            BNCOA2 = GetSetting(appName, Section, "BNCOA2", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA2", value)
        End Set
    End Property
    Public Property BNCOA3() As String
        Get
            Section = "System\Brenntag"
            BNCOA3 = GetSetting(appName, Section, "BNCOA3", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA3", value)
        End Set
    End Property
    Public Property BNCOA4() As String
        Get
            Section = "System\Brenntag"
            BNCOA4 = GetSetting(appName, Section, "BNCOA4", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA4", value)
        End Set
    End Property
    Public Property BNCOA5() As String
        Get
            Section = "System\Brenntag"
            BNCOA5 = GetSetting(appName, Section, "BNCOA5", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA5", value)
        End Set
    End Property
    Public Property BNCOA6() As String
        Get
            Section = "System\Brenntag"
            BNCOA6 = GetSetting(appName, Section, "BNCOA6", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA6", value)
        End Set
    End Property
    Public Property BNCOA7() As String
        Get
            Section = "System\Brenntag"
            BNCOA7 = GetSetting(appName, Section, "BNCOA7", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA7", value)
        End Set
    End Property
    Public Property BNCOA8() As String
        Get
            Section = "System\Brenntag"
            BNCOA8 = GetSetting(appName, Section, "BNCOA8", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA8", value)
        End Set
    End Property
    Public Property BNCOA9() As String
        Get
            Section = "System\Brenntag"
            BNCOA9 = GetSetting(appName, Section, "BNCOA9", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA9", value)
        End Set
    End Property
    Public Property BNCOA10() As String
        Get
            Section = "System\Brenntag"
            BNCOA10 = GetSetting(appName, Section, "BNCOA10", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA10", value)
        End Set
    End Property
    Public Property BNCOA11() As String
        Get
            Section = "System\Brenntag"
            BNCOA11 = GetSetting(appName, Section, "BNCOA11", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA11", value)
        End Set
    End Property
    Public Property BNCOA12() As String
        Get
            Section = "System\Brenntag"
            BNCOA12 = GetSetting(appName, Section, "BNCOA12", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA12", value)
        End Set
    End Property
    Public Property BNCOA13() As String
        Get
            Section = "System\Brenntag"
            BNCOA13 = GetSetting(appName, Section, "BNCOA13", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA13", value)
        End Set
    End Property
    Public Property BNCOA14() As String
        Get
            Section = "System\Brenntag"
            BNCOA14 = GetSetting(appName, Section, "BNCOA14", "")
        End Get
        Set(value As String)
            Section = "System\Brenntag"
            SaveSetting(appName, Section, "BNCOA14", value)
        End Set
    End Property
    Public Property CardReaderPort() As Integer
        Get
            Section = "System\CardReader"
            CardReaderPort = GetSetting(appName, Section, "Port", "0")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Port", value)
        End Set
    End Property
    Public Property CardReaderBaud() As Integer
        Get
            Section = "System\CardReader"
            CardReaderBaud = GetSetting(appName, Section, "Baud", "3")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Baud", value)
        End Set
    End Property
    Public Property CardReaderParity() As Integer
        Get
            Section = "System\CardReader"
            CardReaderParity = GetSetting(appName, Section, "Parity", "0")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Parity", value)
        End Set
    End Property
    Public Property CardReaderDataBits() As Integer
        Get
            Section = "System\CardReader"
            CardReaderDataBits = GetSetting(appName, Section, "DataBits", "1")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "DataBits", value)
        End Set
    End Property
    Public Property CardReaderStopBits() As Integer
        Get
            Section = "System\CardReader"
            CardReaderStopBits = GetSetting(appName, Section, "StopBits", "0")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "StopBits", value)
        End Set
    End Property
    Public Property CardReaderStatus() As Integer
        Get
            Section = "System\CardReader"
            CardReaderStatus = GetSetting(appName, Section, "Status", "1")
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Status", value)
        End Set
    End Property
    Public Property CardReaderActive() As Integer
        Get
            Section = "System\CardReader"
            CardReaderActive = GetSetting(appName, Section, "Active", 0)
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Active", value)
        End Set
    End Property
    Public Property CardReaderStart() As Integer
        Get
            Section = "System\CardReader"
            CardReaderStart = GetSetting(appName, Section, "Start", 6)
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Start", value)
        End Set
    End Property
    Public Property CardReaderLength() As Integer
        Get
            Section = "System\CardReader"
            CardReaderLength = GetSetting(appName, Section, "Length", 5)
        End Get
        Set(value As Integer)
            Section = "System\CardReader"
            SaveSetting(appName, Section, "Length", value)
        End Set
    End Property
    Public ReadOnly Property CardReaderSettings() As String
        Get
            Dim SystemOptions As clsSystem

            SystemOptions = New clsSystem

            CardReaderSettings = ""

            Select Case SystemOptions.CardReaderBaud
                Case 1
                    CardReaderSettings = "2400"
                Case 2
                    CardReaderSettings = "4800"
                Case 3
                    CardReaderSettings = "9600"
                Case 4
                    CardReaderSettings = "14400"
                Case 5
                    CardReaderSettings = "19200"
                Case 6
                    CardReaderSettings = "28800"
                Case Else
                    CardReaderSettings = "9600"
            End Select

            Select Case SystemOptions.CardReaderParity
                Case 0
                    CardReaderSettings = CardReaderSettings & ",N"
                Case 1
                    CardReaderSettings = CardReaderSettings & ",E"
                Case 2
                    CardReaderSettings = CardReaderSettings & ",O"
                Case 3
                    CardReaderSettings = CardReaderSettings & ",S"
                Case Else
                    CardReaderSettings = CardReaderSettings & ",N"
            End Select

            Select Case SystemOptions.CardReaderDataBits
                Case 0
                    CardReaderSettings = CardReaderSettings & ",7"
                Case 1
                    CardReaderSettings = CardReaderSettings & ",8"
                Case Else
                    CardReaderSettings = CardReaderSettings & ",8"
            End Select

            Select Case SystemOptions.CardReaderStopBits
                Case 0
                    CardReaderSettings = CardReaderSettings & ",1"
                Case 1
                    CardReaderSettings = CardReaderSettings & ",2"
                Case Else
                    CardReaderSettings = CardReaderSettings & ",1"
            End Select

            SystemOptions = Nothing
        End Get

    End Property

    Public Property AccessReaderPort() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderPort = GetSetting(appName, Section, "Port", "0")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Port", value)
        End Set
    End Property
    Public Property AccessReaderBaud() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderBaud = GetSetting(appName, Section, "Baud", "3")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Baud", value)
        End Set
    End Property
    Public Property AccessReaderParity() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderParity = GetSetting(appName, Section, "Parity", "0")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Parity", value)
        End Set
    End Property
    Public Property AccessReaderDataBits() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderDataBits = GetSetting(appName, Section, "DataBits", "1")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "DataBits", value)
        End Set
    End Property
    Public Property AccessReaderStopBits() As Integer
        Get
            Section = "SystemAccessReader"
            AccessReaderStopBits = GetSetting(appName, Section, "StopBits", "0")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "StopBits", value)
        End Set
    End Property
    Public Property AccessReaderStatus() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderStatus = GetSetting(appName, Section, "Status", "1")
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Status", value)
        End Set
    End Property
    Public Property AccessReaderActive() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderActive = GetSetting(appName, Section, "Active", 0)
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Active", value)
        End Set
    End Property
    Public Property AccessReaderStart() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderStart = GetSetting(appName, Section, "Start", 6)
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Start", value)
        End Set
    End Property
    Public Property AccessReaderLength() As Integer
        Get
            Section = "System\AccessReader"
            AccessReaderLength = GetSetting(appName, Section, "Length", 5)
        End Get
        Set(value As Integer)
            Section = "System\AccessReader"
            SaveSetting(appName, Section, "Length", value)
        End Set
    End Property

    Public ReadOnly Property AccessReaderSettings() As String
        Get
            Dim SystemOptions As clsSystem

            SystemOptions = New clsSystem

            AccessReaderSettings = ""

            Select Case SystemOptions.AccessReaderBaud
                Case 1
                    AccessReaderSettings = "2400"
                Case 2
                    AccessReaderSettings = "4800"
                Case 3
                    AccessReaderSettings = "9600"
                Case 4
                    AccessReaderSettings = "14400"
                Case 5
                    AccessReaderSettings = "19200"
                Case 6
                    AccessReaderSettings = "28800"
                Case Else
                    AccessReaderSettings = "9600"
            End Select

            Select Case SystemOptions.AccessReaderParity
                Case 0
                    AccessReaderSettings = AccessReaderSettings & ",N"
                Case 1
                    AccessReaderSettings = AccessReaderSettings & ",E"
                Case 2
                    AccessReaderSettings = AccessReaderSettings & ",O"
                Case 3
                    AccessReaderSettings = AccessReaderSettings & ",S"
                Case Else
                    AccessReaderSettings = AccessReaderSettings & ",N"
            End Select

            Select Case SystemOptions.CardReaderDataBits
                Case 0
                    AccessReaderSettings = AccessReaderSettings & ",7"
                Case 1
                    AccessReaderSettings = AccessReaderSettings & ",8"
                Case Else
                    AccessReaderSettings = AccessReaderSettings & ",8"
            End Select
            Select Case SystemOptions.CardReaderStopBits
                Case 0
                    AccessReaderSettings = AccessReaderSettings & ",1"
                Case 1
                    AccessReaderSettings = AccessReaderSettings & ",2"
                Case Else
                    AccessReaderSettings = AccessReaderSettings & ",1"
            End Select

            SystemOptions = Nothing
        End Get
    End Property

    'Public Property AdminPassword() As String
    '    Get
    '        Dim LoopCtr As Integer
    '        Dim TmpStr As String
    '        Dim TmpChar As String
    '        Dim AscChar As Integer

    '        Section = "System"
    '        TmpStr = GetSetting(appName, Section, "Admin", "")
    '        If TmpStr <> "TLH" Then  'WEIGHUP
    '            For LoopCtr = 1 To Len(TmpStr)
    '                TmpChar = Mid(TmpStr, LoopCtr, 1)
    '                AscChar = Asc(TmpChar) And 127
    '                AdminPassword = AdminPassword & Chr(AscChar)
    '            Next LoopCtr
    '        Else
    '            AdminPassword = TmpStr
    '        End If
    '    End Get
    '    Set(value As String)
    '        Dim LoopCtr As Integer
    '        Dim TmpStr As String
    '        Dim TmpChar As String
    '        Dim AscChar As Integer

    '        Section = "System"
    '        For LoopCtr = 1 To Len(value)
    '            TmpChar = Mid(value, LoopCtr, 1)
    '            AscChar = Asc(TmpChar) Or 128
    '            TmpStr = TmpStr & Chr(AscChar)
    '        Next LoopCtr
    '        SaveSetting(appName, Section, "Admin", TmpStr)
    '    End Set
    'End Property

End Class
