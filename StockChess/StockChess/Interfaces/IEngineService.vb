Public Interface IEngineService
    Sub StartEngine()
    Sub StopEngine()
    Sub SendCommand(ByVal command As String)
    Event EngineMessage(ByVal message As String)
End Interface
