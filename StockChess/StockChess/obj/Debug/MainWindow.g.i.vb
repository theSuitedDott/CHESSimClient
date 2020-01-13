﻿#ExternalChecksum("..\..\MainWindow.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","861E31EBC31E79A143442B29E848E78C6D3098B7")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports MahApps.Metro.Controls
Imports StockChess
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Interactivity
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell


'''<summary>
'''MainWindow
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class MainWindow
    Inherits MahApps.Metro.Controls.MetroWindow
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\MainWindow.xaml",24)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\MainWindow.xaml",48)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ColorMenuItem As System.Windows.Controls.MenuItem
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\MainWindow.xaml",61)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents WhiteMenuItem As System.Windows.Controls.MenuItem
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\MainWindow.xaml",64)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents BlackMenuItem As System.Windows.Controls.MenuItem
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\MainWindow.xaml",75)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents BoardListBox As System.Windows.Controls.ListBox
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/StockChess;component/mainwindow.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\MainWindow.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.LayoutRoot = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 2) Then
            
            #ExternalSource("..\..\MainWindow.xaml",45)
            AddHandler CType(target,System.Windows.Controls.MenuItem).Click, New System.Windows.RoutedEventHandler(AddressOf Me.FlipBoard)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.ColorMenuItem = CType(target,System.Windows.Controls.MenuItem)
            Return
        End If
        If (connectionId = 4) Then
            Me.WhiteMenuItem = CType(target,System.Windows.Controls.MenuItem)
            
            #ExternalSource("..\..\MainWindow.xaml",62)
            AddHandler Me.WhiteMenuItem.Checked, New System.Windows.RoutedEventHandler(AddressOf Me.ItemChecked)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            Me.BlackMenuItem = CType(target,System.Windows.Controls.MenuItem)
            
            #ExternalSource("..\..\MainWindow.xaml",65)
            AddHandler Me.BlackMenuItem.Checked, New System.Windows.RoutedEventHandler(AddressOf Me.ItemChecked)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.BoardListBox = CType(target,System.Windows.Controls.ListBox)
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

