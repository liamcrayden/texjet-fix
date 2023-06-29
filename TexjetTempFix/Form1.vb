Imports System.Runtime
Imports System.Threading

Public Class Form1

    Dim tEchoThread As New Thread(AddressOf EchoFixProcedure)
    Dim tShorteeThread As New Thread(AddressOf ShorteeFixProcedure)
    Dim maxTimeout As Integer = 200000
    Dim ProgressBarDelegate As New setProgressBarValue(AddressOf updateProgressBar)
    Dim system32Path = System.Environment.ExpandEnvironmentVariables("%windir%\system32")
    Dim pnpProcessInfo As New ProcessStartInfo(system32Path + "\pnputil.exe")
    Dim cmdProcessInfo As New ProcessStartInfo("cmd.exe")


    Private Sub btnEcho_Click(sender As Object, e As EventArgs) Handles btnEcho.Click
        tEchoThread.Start()
    End Sub

    Private Sub btnShortee_Click(sender As Object, e As EventArgs) Handles btnShortee.Click
        tShorteeThread.Start()
    End Sub

    Private Sub enableButtons()
        btnEcho.Enabled = True
        btnShortee.Enabled = True
    End Sub

    Private Sub disableButtons()
        btnEcho.Enabled = False
        btnShortee.Enabled = False
    End Sub

    Private Delegate Sub setProgressBarValue(value)

    Private Sub updateProgressBar(value)
        ProgressBar1.Value = value
    End Sub

    Private Sub ShorteeFixProcedure()
        Me.Invoke(AddressOf disableButtons)
        Me.Invoke(ProgressBarDelegate, 10)

        ' Driver removal and reinstallation, if the box is ticked
        If CheckBox1.Checked = True Then
            ' Remove all instances of the old driver
            pnpProcessInfo.Arguments = "/remove-device /deviceid USBPRINT\EPSONSC-P600_Series730C"
            Dim removeDriver As Process = Process.Start(pnpProcessInfo)
            If Not removeDriver.WaitForExit(maxTimeout) Then
                MsgBox("Could not remove old device driver. Aborting.", MsgBoxStyle.Critical, "Error")
                Me.Invoke(ProgressBarDelegate, 0)
                Me.Invoke(AddressOf enableButtons)
                Return
            Else
                Me.Invoke(ProgressBarDelegate, 20)
            End If

            ' Install replacement bundled driver - TODO: Get application path to correct folder
            cmdProcessInfo.Arguments = "/add-driver " + Application.StartupPath() + "P600\e_3f21ge.inf /install"
            Dim addDriver As Process = Process.Start(pnpProcessInfo)
            If Not addDriver.WaitForExit(maxTimeout) Then
                MsgBox("Could not install replacement device driver. Aborting.", MsgBoxStyle.Critical, "Error")
                Me.Invoke(ProgressBarDelegate, 0)
                Me.Invoke(AddressOf enableButtons)
                Return
            Else
                Me.Invoke(ProgressBarDelegate, 30)
            End If

            MsgBox("Unplug the printer from the USB port, then plug it back in to continue.", MsgBoxStyle.Information, "Reconnect")

        End If

        ' Stop the print spooler service
        cmdProcessInfo.Arguments = "/c net stop spooler"
        Dim stopSpoolerService As Process = Process.Start(cmdProcessInfo)
        If Not stopSpoolerService.WaitForExit(maxTimeout) Then
            MsgBox("Could not stop the Print Spooler service. Aborting.", MsgBoxStyle.Critical, "Error")
            Me.Invoke(ProgressBarDelegate, 0)
            Me.Invoke(AddressOf enableButtons)
            Return
        Else
            Me.Invoke(ProgressBarDelegate, 60)
        End If

        ' Start the print spooler service
        cmdProcessInfo.Arguments = "/c net start spooler"
        Dim startSpoolerService As Process = Process.Start(cmdProcessInfo)
        If Not startSpoolerService.WaitForExit(maxTimeout) Then
            MsgBox("Could not stop the Print Spooler service. Aborting.", MsgBoxStyle.Critical, "Error")
            Me.Invoke(ProgressBarDelegate, 0)
            Me.Invoke(AddressOf enableButtons)
            Return
        Else
            Me.Invoke(ProgressBarDelegate, 80)
        End If


        ' Complete
        Me.Invoke(ProgressBarDelegate, 100)
        MsgBox("Fix completed successfully!", MsgBoxStyle.OkOnly, "Success")
        Me.Invoke(AddressOf enableButtons)
        Application.Exit()

    End Sub
    Private Sub EchoFixProcedure()
        Me.Invoke(AddressOf disableButtons)
        Me.Invoke(ProgressBarDelegate, 10)

        ' Driver removal and reinstallation, if the box is ticked
        If CheckBox1.Checked = True Then
            ' Remove all instances of the old driver
            pnpProcessInfo.Arguments = "/remove-device /deviceid USBPRINT\EPSONSC-P800_Series846D"
            Dim removeDriver As Process = Process.Start(pnpProcessInfo)
            If Not removeDriver.WaitForExit(maxTimeout) Then
                MsgBox("Could not remove old device driver. Aborting.", MsgBoxStyle.Critical, "Error")
                Me.Invoke(ProgressBarDelegate, 0)
                Me.Invoke(AddressOf enableButtons)
                Return
            Else
                Me.Invoke(ProgressBarDelegate, 20)
            End If

            ' Install replacement bundled driver - TODO: Get application path to correct folder
            cmdProcessInfo.Arguments = "/add-driver " + Application.StartupPath() + "P800\e_3f21fe.inf /install"
            Dim addDriver As Process = Process.Start(pnpProcessInfo)
            If Not addDriver.WaitForExit(maxTimeout) Then
                MsgBox("Could not install replacement device driver. Aborting.", MsgBoxStyle.Critical, "Error")
                Me.Invoke(ProgressBarDelegate, 0)
                Me.Invoke(AddressOf enableButtons)
                Return
            Else
                Me.Invoke(ProgressBarDelegate, 30)
            End If

            MsgBox("Unplug the printer from the USB port, then plug it back in to continue.", MsgBoxStyle.Information, "Reconnect")

        End If

        ' Stop the print spooler service
        cmdProcessInfo.Arguments = "/c net stop spooler"
        Dim stopSpoolerService As Process = Process.Start(cmdProcessInfo)
        If Not stopSpoolerService.WaitForExit(maxTimeout) Then
            MsgBox("Could not stop the Print Spooler service. Aborting.", MsgBoxStyle.Critical, "Error")
            Me.Invoke(ProgressBarDelegate, 0)
            Me.Invoke(AddressOf enableButtons)
            Return
        Else
            Me.Invoke(ProgressBarDelegate, 60)
        End If

        ' Start the print spooler service
        cmdProcessInfo.Arguments = "/c net start spooler"
        Dim startSpoolerService As Process = Process.Start(cmdProcessInfo)
        If Not startSpoolerService.WaitForExit(maxTimeout) Then
            MsgBox("Could not stop the Print Spooler service. Aborting.", MsgBoxStyle.Critical, "Error")
            Me.Invoke(ProgressBarDelegate, 0)
            Me.Invoke(AddressOf enableButtons)
            Return
        Else
            Me.Invoke(ProgressBarDelegate, 80)
        End If


        ' Complete
        Me.Invoke(ProgressBarDelegate, 100)
        MsgBox("Fix completed successfully!", MsgBoxStyle.OkOnly, "Success")
        Me.Invoke(AddressOf enableButtons)
        Application.Exit()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmdProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
        cmdProcessInfo.CreateNoWindow = True

        pnpProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
        pnpProcessInfo.CreateNoWindow = True
    End Sub
End Class
