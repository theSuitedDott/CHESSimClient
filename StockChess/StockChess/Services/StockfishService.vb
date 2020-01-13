Imports System.IO
Imports System.Reactive.Linq

Public Class StockfishService
    Implements IEngineService

    Private strmReader As StreamReader
    Private strmWriter As StreamWriter
    Private engineProcess As Process
    Private engineListener As IDisposable

    Public Event EngineMessage(message As String) Implements IEngineService.EngineMessage

    Public Sub SendCommand(command As String) Implements IEngineService.SendCommand
        If strmWriter IsNot Nothing AndAlso command <> UciCommands.uci Then
            strmWriter.WriteLine(command)
        End If
    End Sub

    Public Sub StopEngine() Implements IEngineService.StopEngine
        If engineProcess IsNot Nothing And Not engineProcess.HasExited Then
            engineListener.Dispose()
            strmReader.Close()
            strmWriter.Close()
        End If
    End Sub

    Public Sub StartEngine() Implements IEngineService.StartEngine
        Dim engine As New FileInfo(Path.Combine(Environment.CurrentDirectory, "stockfish_8_x64.exe"))
        If engine.Exists AndAlso engine.Extension = ".exe" Then
            engineProcess = New Process
            engineProcess.StartInfo.FileName = engine.FullName
            engineProcess.StartInfo.UseShellExecute = False
            engineProcess.StartInfo.RedirectStandardInput = True
            engineProcess.StartInfo.RedirectStandardOutput = True
            engineProcess.StartInfo.RedirectStandardError = True
            engineProcess.StartInfo.CreateNoWindow = True

            engineProcess.Start()

            strmWriter = engineProcess.StandardInput
            strmReader = engineProcess.StandardOutput

            engineListener = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1)).Subscribe(Sub() ReadEngineMessages())

            strmWriter.WriteLine(UciCommands.uci)
            strmWriter.WriteLine(UciCommands.isready)
            strmWriter.WriteLine(UciCommands.ucinewgame)
        Else
            Throw New FileNotFoundException
        End If
    End Sub

    Private Sub ReadEngineMessages()
        Dim message = strmReader.ReadLine()
        If message <> String.Empty Then
            RaiseEvent EngineMessage(message)
        End If
    End Sub
End Class