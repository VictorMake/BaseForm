Public Class IntegerTextBox
    Inherits RegExTextBox

    ''' <summary>
    ''' Установить по умолчанию ValidationExpression. Tаким образом
    ''' этот контрол будет проверять содержимое TextBox в соответствии с шаблоном:
    ''' ^\s*[+-]?\d+\s*$" 'integer
    ''' "^\s*\d+\s*$"  'integer полож
    ''' </summary>
    Private Sub SetValidation()
        ValidationExpression = "^\s*\d+\s*$"
        ErrorMessage = "Значение поля должно быть цифровым!"
    End Sub
End Class
