Module ModuleGlobal
    ' почему-то нельзя использовать глобальные классы форм вне контекста frmBase т.к. создаются какие-то теневые экземпляры
    ' здесь использовать только константы

    Public Const PARAMETER_IS_NOTHING As String = "Отсутствует"
    Public Const VACUUM As String = "Разрежение"
    Public Const PRESSURE As String = "Давление"
    Public Const PROVIDER_JET As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
    Public Const con9999999 As Integer = 9999999
    ''' <summary>
    ''' ColumnIndex_ИспользоватьЛогику
    ''' </summary>
    Public Const ColumnIndex_UseLogical As Integer = 2
    ''' <summary>
    ''' ColumnIndex_ИспользоватьКонстанту
    ''' </summary>
    Public Const ColumnIndex_UseConstant As Integer = 3
    ''' <summary>
    ''' ColumnIndex_ИмяБазовогоПараметра
    ''' </summary>
    Public Const ColumnIndex_NameBaseParameter As Integer = 6

    Public Function BuildCnnStr(ByVal provider As String, ByVal dataBase As String) As String
        'Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source="D:\ПрограммыVBNET\RUD\RUD.NET\bin\Ресурсы\Channels.mdb";Jet OLEDB:Engine Type=5;Provider="Microsoft.Jet.OLEDB.4.0";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1
        'Public Const strProviderJet As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
        Return $"{provider}Data Source={dataBase};"
    End Function
End Module
