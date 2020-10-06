Imports System.Data.OleDb

Friend Class FormTuningParameters

    Private mFormParrent As FrmBase
    Public WriteOnly Property FormParrent() As FrmBase
        Set(ByVal Value As FrmBase)
            mFormParrent = Value
        End Set
    End Property

    Sub New(ByVal frmBaseParrent As FrmBase)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        MdiParent = frmBaseParrent
        FormParrent = frmBaseParrent
    End Sub

    ''' <summary>
    ''' Загрузка и конфигурация адаптеров формы.
    ''' Аналог событию frmНастроечныеПараметры_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConfigureTableAdapter()
        ' строка подключения сделал сам т.к. в дизайнере ссылка на строку созданную при создании дизайнера
        TuningParametersTableAdapter.Connection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, mFormParrent.Manager.PathSettingMdb))

        ' This line of code loads data into the 'BaseFormDataSet.НастроечныеПараметры' table. You can move, or remove it, as needed.
        TuningParametersTableAdapter.Fill(BaseFormDataSet.НастроечныеПараметры)
    End Sub

    Public Sub DataGridViewTuning_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridViewTuning.CellValueChanged
        If IsHandleCreated Then
            If e.ColumnIndex = ColumnIndex_UseLogical Then
                ' для двух следующих полей определить ReadOnly
                DataGridViewTuning.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).ReadOnly = CBool(DataGridViewTuning.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                DataGridViewTuning.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).ReadOnly = Not CBool(DataGridViewTuning.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            End If

            mFormParrent.Manager.NeedToRewrite = True
        End If
    End Sub

    Private Sub DataGridViewTuning_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not mFormParrent.IsWindowClosed Then e.Cancel = True
    End Sub

    Private Sub DataGridViewTuning_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        mFormParrent = Nothing
    End Sub

    Private Sub TSButtonHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonHelp.Click
        MessageBox.Show($"Присвоить значение настроечного параметра.{Environment.NewLine}Для логического параметра установить чек, а для цифрового ввести необходимое значение.",
                        "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click
        mFormParrent.Manager.SaveTable()
    End Sub
End Class