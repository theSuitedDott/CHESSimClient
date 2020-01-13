Class MainWindow
    Private isBoardFlipped As Boolean
    Private boardRotateTx As New RotateTransform
    Private boardTemplate As DataTemplate

    Private Sub FlipBoard(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If Not isBoardFlipped Then
            boardRotateTx.Angle = 180
            boardTemplate = CType(FindResource("FlippedBoardTemplate"), DataTemplate)
            isBoardFlipped = True
        Else
            boardRotateTx.Angle = 0
            boardTemplate = CType(FindResource("BoardTemplate"), DataTemplate)
            isBoardFlipped = False
        End If
        BoardListBox.RenderTransform = boardRotateTx
        BoardListBox.ItemTemplate = boardTemplate
    End Sub

    Private Sub ItemChecked(sender As Object, e As RoutedEventArgs)
        Dim item = CType(sender, MenuItem)
        Dim itemParent = CType(item.Parent, MenuItem)
        itemParent.Items.OfType(Of MenuItem).Where(Function(i) i.Name <> item.Name).
            ToList.ForEach(Sub(m) m.IsChecked = False)
    End Sub
End Class
