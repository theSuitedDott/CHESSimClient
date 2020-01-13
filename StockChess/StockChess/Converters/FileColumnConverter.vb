Imports System.Globalization

Public Class FileColumnConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim file = CChar(value)
        Dim files = ("abcdefgh").ToArray()
        Dim column = files.ToList.IndexOf(file)
        Return column
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Binding.DoNothing
    End Function
End Class
