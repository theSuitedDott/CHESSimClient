Imports Microsoft.Practices.Unity

Public Class ViewModelLocator
    Private _container As UnityContainer

    Public Sub New()
        _container = New UnityContainer
        _container.RegisterType(Of IEngineService, StockfishService)()
    End Sub

    Public ReadOnly Property ChessVM As ChessViewModel
        Get
            Return _container.Resolve(Of ChessViewModel)()
        End Get
    End Property
End Class
