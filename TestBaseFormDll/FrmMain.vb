Imports BaseForm

Public Class FrmMain
    Public mBaseForm As BaseForm.FrmBase

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Test()
    End Sub

    ''' <summary>
    ''' Для автономной отладки приложения
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Test()
        mBaseForm = New BaseForm.FrmBase
        mBaseForm.Show() ' 1 Загрузить базовую форму (или наследника)

        'mBaseForm.Manager.PathSettingMdb = "D:\Project\Saturn\Work\SSDNetworkVariable\SSDNetworkVariableFW45Ex\SSDNetworkVariable\bin\Debug\Store\BaseForm.mdb"
        mBaseForm.Manager.PathSettingMdb = "E:\SaturnNew\Доступ к базе\Новая папка\BaseForm.mdb"

        mBaseForm.Manager.ИменаПараметровРегистратора(New String() {"Отсутствует", "One", "Two", "N1", "Вк", "Вб"})
        mBaseForm.Manager.ИндексыПараметровРегистратора(New Integer() {0, 1, 2, 3, 4, 5})

        mBaseForm.FrmBaseLoad() ' 2 Загрузить вкладки с сетками
        mBaseForm.Manager.FillCombo() ' 3 заполнить сетки
    End Sub


End Class
