''' <summary>
''' Пользовательское событие унаследованное от EventArgs
''' </summary>
''' <remarks></remarks>
Public Class DataErrorEventArgs
    Inherits EventArgs

    Public Sub New(ByVal message As String, ByVal description As String)
        Me.Message = message
        Me.Description = description
    End Sub

    ' Это событие имеет 2 члена -- 
    ''' <summary>
    ''' 1) что за событие
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Message As String
    ''' <summary>
    ''' 2) какова его сущность.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Description As String
End Class
