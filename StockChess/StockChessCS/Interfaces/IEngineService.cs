using System;

namespace StockChessCS.Interfaces
{
    public interface IEngineService
    {
        void StartEngine();
        void StopEngine();
        void SendCommand(string command);
        event Action<string> EngineMessage;        
    }    
}
