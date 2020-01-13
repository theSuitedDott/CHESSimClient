Public Class UciCommands
    ' Commands to engine
    ' ==================
    Public Const uci As String = "uci"
    Public Const isready As String = "isready"
    Public Const ucinewgame As String = "ucinewgame"
    Public Const position As String = "position startpos moves"
    Public Const go_movetime As String = "go movetime"
    Public Const [stop] As String = "stop"

    ' Commands from engine
    ' ====================
    Public Const uciok As String = "uciok"
    Public Const readyok As String = "readyok"
    Public Const bestmove As String = "bestmove"
End Class
