﻿Imports System.Data.OleDb

Friend Class FormMeasurementParameters

    Private mFormParrent As FrmBase
    Public WriteOnly Property FormParrent() As FrmBase
        Set(ByVal Value As FrmBase)
            mFormParrent = Value
        End Set
    End Property

    Public Sub New(ByVal frmBaseParrent As FrmBase)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        MdiParent = frmBaseParrent
        FormParrent = frmBaseParrent
    End Sub

    ''' <summary>
    ''' Загрузка и конфигурация адаптеров формы.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConfigureTableAdapter()
        ' строку подключения сделал сам т.к. в дизайнере ссылка на строку созданную при создании дизайнера
        MeasurementParametersTableAdapter.Connection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, mFormParrent.Manager.PathSettingMdb))

        ' This line of code loads data into the 'BaseFormDataSet.ИзмеренныеПараметры' table. You can move, or remove it, as needed.
        MeasurementParametersTableAdapter.Fill(BaseFormDataSet.ИзмеренныеПараметры)
    End Sub

    ''' <summary>
    ''' Отслеживать изменения ячеек для установления доступности зависящих ячеек
    ''' и для установки флага перезаписи.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub DataGridViewMeasurement_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridViewMeasurement.CellValueChanged
        If IsHandleCreated Then
            If e.ColumnIndex = ColumnIndex_UseConstant Then
                DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).ReadOnly = Not CBool(DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            End If

            If e.ColumnIndex = ColumnIndex_NameBaseParameter Then
                If DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString = "" Then
                    DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).ReadOnly = True
                    DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = ""
                Else
                    DataGridViewMeasurement.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).ReadOnly = False
                End If
            End If

            mFormParrent.Manager.NeedToRewrite = True
        End If
    End Sub

    Private Sub FormMeasurementParameters_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not mFormParrent.IsWindowClosed Then e.Cancel = True
    End Sub

    Private Sub FormMeasurementParameters_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        mFormParrent = Nothing
    End Sub

    Private Sub TSButtonHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonHelp.Click
        Dim help As String = "Присвоить параметру измерения канал измерительной системы." & Environment.NewLine &
        "При отсутствии канала установить чек <Использовать Константу> и задать её значение." & Environment.NewLine &
        "Если в формуле расчёта используются единицы СИ, то для каждого параметра установить <Входную единицу измерения>," & Environment.NewLine &
        "в противном случае установить <нет>." & Environment.NewLine &
        "Для параметров измеряющих абсолютное давление по разряжению или давлению необходимо указать имя параметра базового давления" & Environment.NewLine &
        "и указать тип давления (разряжение или давление)."

        MessageBox.Show(help, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click
        mFormParrent.Manager.SaveTable()
    End Sub
End Class