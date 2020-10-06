''' <summary>
''' Интерфейс пользовательского расчётного класса (плагина).
''' </summary>
''' <remarks></remarks>
Public Interface IClassCalculation

    ''' <summary>
    ''' Служебный класс инкапсулирующий бизнес модель.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Manager() As ProjectManager

    ''' <summary>
    ''' Расчёт выходных параметров.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Calculate()
End Interface


