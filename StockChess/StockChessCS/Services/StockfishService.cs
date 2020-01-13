using System;
using System.IO;
using System.Reactive.Linq;
using System.Diagnostics;
using StockChessCS.Helpers;
using StockChessCS.Interfaces;

namespace StockChessCS.Services
{
    public class StockfishService : IEngineService
    {

        private StreamReader strmReader;
        private StreamWriter strmWriter;
        private Process engineProcess;

        private IDisposable engineListener;
        public event Action<string> EngineMessage;	

        public void SendCommand(string command)
        {
            if (strmWriter != null && command != UciCommands.uci)
            {
                strmWriter.WriteLine(command);
            }
        }

        public void StopEngine()
        {
            if (engineProcess != null & !engineProcess.HasExited)
            {
                engineListener.Dispose();
                strmReader.Close();
                strmWriter.Close();                
            }
        }

        public void StartEngine()
        {
            FileInfo engine = new FileInfo(Path.Combine(Environment.CurrentDirectory, "stockfish_8_x64.exe"));
            if (engine.Exists && engine.Extension == ".exe")
            {
                engineProcess = new Process();
                engineProcess.StartInfo.FileName = engine.FullName;
                engineProcess.StartInfo.UseShellExecute = false;
                engineProcess.StartInfo.RedirectStandardInput = true;
                engineProcess.StartInfo.RedirectStandardOutput = true;
                engineProcess.StartInfo.RedirectStandardError = true;
                engineProcess.StartInfo.CreateNoWindow = true;

                engineProcess.Start();

                strmWriter = engineProcess.StandardInput;
                strmReader = engineProcess.StandardOutput;

                engineListener = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1)).Subscribe(s => ReadEngineMessages());

                strmWriter.WriteLine(UciCommands.uci);
                strmWriter.WriteLine(UciCommands.isready);
                strmWriter.WriteLine(UciCommands.ucinewgame);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private void ReadEngineMessages()
        {
            var message = strmReader.ReadLine();
            if (message != string.Empty)
            {
                EngineMessage?.Invoke(message);
            }
        }
    }
}
