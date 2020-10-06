Imports System.Resources

' Внимание!!! При переопределении BaseFormDataSet.xsd для полей <ИмяБазовогоПараметра> и <ТипДавления> в дизайенере установить свойство {NullValue} в значение (Empty)
' Расчетные параметры являются выходом класса(типа DLL) реализующего интерфейс (визуально наследующий форму BaseForm) через Parent.Manager.РасчетныеПараметры 
' Эти классы типа DLL и база параметров с тем же именем хранятся в специальном каталоге "Ресурсы\МодулиРасчета". 
' Программа при старте составляет список имен этих классов
' и сверяет их состав с содержимым в конфигурационном файлом предыдущего запуска.
' Присутствующие добавляются в коллекцию, просто подключает при совпадении.
' В итоге есть коллекция DLL последней конфигурацией, готовая для расчетов сразу после запуска формы регистратора
' и первоначальный список DLL всех находящихся в каталоге DLL.

' 1. Каждая унаследованная DLL (форма) имеет описание Description для чего предназначена форма.
' В первой форме настроек имеется выпадающий список составом DLL.
' В листе отмечаются DLL, заново обновляется конфигурационный файл и заново заполняется коллекция.

' 2. в цикле по коллекции производится проверка соответствия входных каналов входным расчетным параметрам.
' Если найдены отсутствующие каналы, то сообщение, что неполное соответствие.
' Если тест не пройден, то запуск отменяется и предлагается загрузить редактор для приведения в соответствие каналов.
' DLL имеет свойство видимости - невидима, если простой расчет и видима для расчётов с настройками.
' При редактировании, сли DLL невидима, то вывести ее на передний фон. После окончания редактирования невидимые DLL скрываются.
' Перед закрытием проверка флага, что не произведено сохранение изменений (fMainFormBase.Manager.blnНадоПерезаписать).
' В видимой форме вкладки редактирования невидимы после окончания редактирования, а при редактиролвании видимы (свойство).

' При ЗАПУСКЕ в видимой форме вкладки настроек становятся невидимыми. Невидимая форма при запуске скрывается,
' т.е. при ЗАПУСКЕ в цикле по коллекции, если DLL видима вкладки с настройками скрыть, а если DLL невидима скрыть всю DLL.

' Если при проверке DLL была ошибка, то сообщение и на выбор оператору или оставить и отредактировать или исключить.
' Если ошибка в расчете то Dll пишет LOG файл(медленная операция) или ALARM прокручиваемый список и вывод 999999.
' Форма окна DLL должна не реагировать кнопку закрытия только свернуть и развернуть.

' Видимая DLL может иметь всё для работы обычного приложения, значит такие DLL должны иметь собственный каталог,
' наверно с таким же именем или путь к этому каталогу хранится в конфигурации DLL.

' Каждая DLL хранит настроечные данные в базе данных, имя которого соответствует имени DLL.
' (соответствие входных параметров расчета каналам, массив выходных параметров (расчетных),
' и коллекция настроечных значений - цифра или логическая и их величины.


<Assembly: NeutralResourcesLanguage("ru-RU")>
<Assembly: CLSCompliant(True)>
Public Class FrmBase
    Friend varMeasurementParameters As FormMeasurementParameters
    Friend varTuningParameters As FormTuningParameters
    Friend varCalculatedParameters As FormCalculatedParameters

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Manager = New ProjectManager(Me)
    End Sub

    Public Property Manager() As ProjectManager

    Private mDescription As String = "Это базовая форма для насследования"
    ''' <summary>
    ''' Описание предназначения плагина.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property

    Private mOwnCatalogue As String = "Это путь собственного каталога"
    ''' <summary>
    ''' Путь собственного каталога.
    ''' Не делать это свойство свёрнутым.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property OwnCatalogue() As String
        Get
            Return mOwnCatalogue
        End Get
        Set(ByVal value As String)
            mOwnCatalogue = value
        End Set
    End Property

    Private mIsDllVisible As Boolean = False
    ''' <summary>
    ''' Видима DLL или нет, т.е. имеются вкладки с другими окнами или она только вычисляет.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property IsDllVisible() As Boolean
        Get
            Return mIsDllVisible
        End Get
    End Property

    ''' <summary>
    ''' Пользовательский расчётный класс реализующий интерфейс IClassCalculation.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property ClassCalculation() As IClassCalculation

    ''' <summary>
    ''' Свойство для управления родителем закрытия окон плагина.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsWindowClosed() As Boolean

    ''' <summary>
    ''' Считать из базы настроечные величины. Переопределяется в наследнике.
    ''' Получить Значения Настроечных Параметров
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub GetValueTuningParameters()
    End Sub

    ''' <summary>
    ''' Загрузить вкладку НастроечныеПараметры
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFormTuningParameters()
        varTuningParameters = New FormTuningParameters(Me)
        varTuningParameters.Show()
        varTuningParameters.ConfigureTableAdapter()
        Manager.TuningDataTable = varTuningParameters.BaseFormDataSet.НастроечныеПараметры
        'varTuningParameters.WindowState = FormWindowState.Maximized
        'varTuningParameters.Activate()
    End Sub

    ''' <summary>
    ''' Загрузить вкладку РасчетныеПараметры
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFormCalculatedParameters()
        varCalculatedParameters = New FormCalculatedParameters(Me)
        varCalculatedParameters.Show()
        varCalculatedParameters.ConfigureTableAdapter()
        Manager.CalculatedDataTable = varCalculatedParameters.BaseFormDataSet.РасчетныеПараметры
        'varCalculatedParameters.WindowState = FormWindowState.Maximized
        'varCalculatedParameters.Activate()
    End Sub

    ''' <summary>
    ''' Загрузить вкладку ИзмеренныеПараметры
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFormMeasurementParameters()
        varMeasurementParameters = New FormMeasurementParameters(Me)
        varMeasurementParameters.Show()
        varMeasurementParameters.ConfigureTableAdapter()
        Manager.MeasurementDataTable = varMeasurementParameters.BaseFormDataSet.ИзмеренныеПараметры
        'varMeasurementParameters.WindowState = FormWindowState.Maximized
        'varMeasurementParameters.Activate()
    End Sub

    ''' <summary>
    ''' Загрузка производится родителем, иначе может быть ошибка в визуальном конструкторе.
    ''' </summary>
    Public Sub FrmBaseLoad()
        ' обработку ошибок убрать после отладки, чтобы если была ошибка, то форма бы не загружалась
        Try
            'Me.WindowManagerPanel1.CustomTabsProviderType = GetType(Vs05StTabsProvider.Vs05StTabsProvider)

            LoadFormMeasurementParameters()
            LoadFormTuningParameters()
            LoadFormCalculatedParameters()

            ' varProjectManager.ДобавитьКолонкиТаблицыИзмеренные()
            'WindowManagerPanel1.SetActiveWindow(0)

            ' СчитатьНастройки()
        Catch ex As Exception
            MessageBox.Show(Me, $"Ошибка загрузки базовой формы.{Environment.NewLine}Error: {ex.Message}",
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Test()
    End Sub

    ' ''' <summary>
    ' ''' Для автономной отладки приложения
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub Test()
    '    Manager.ИменаПараметровРегистратора(New String() {"Отсутствует", "One", "Two", "N1", "Вк", "Вб"})
    '    Manager.ИндексыПараметровРегистратора(New Integer() {0, 1, 2, 3, 4, 5})
    '    Manager.FillCombo()
    'End Sub

    Private Sub FrmBase_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If IsWindowClosed Then
            SavePathSettinngXml()
            If Manager.NeedToRewrite Then Manager.SaveTable()
        Else
            e.Cancel = True
        End If

        varMeasurementParameters.Close()
        varTuningParameters.Close()
        varCalculatedParameters.Close()
    End Sub

    ''' <summary>
    ''' Сохранить положение окна в файле настроек
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SavePathSettinngXml()
        Try
            'Dim nsLMZ As XNamespace = "urn:LMZ-org:lmz"
            'Dim nsProgrammers As XNamespace = "urn:LMZ-org:LmzProgrammers"

            ' создать документ
            Dim xmlDocumentSettings = New XDocument()

            ' создать xml описание и установить в документе
            'document.Declaration = New XDeclaration("1.0", Nothing, Nothing)

            ' создать Settings element и добавить в документ
            Dim xmlSettings = New XElement("Settings")
            xmlDocumentSettings.Add(xmlSettings)

            ' создать order инструкцию добавить перед предыдущим элементом
            'Dim pi = New XProcessingInstruction("order", "alpha ascending")
            'Settings.AddBeforeSelf(pi)

            ' создать Location element и добавить в Settings element
            Dim xmlLocation = New XElement("Location")
            xmlSettings.Add(xmlLocation)
            Dim xmlSize = New XElement("Size")
            xmlSettings.Add(xmlSize)

            If WindowState <> FormWindowState.Minimized Then
                ' добавить аттрибуты размерности в Location и Size element 
                xmlLocation.SetAttributeValue("Left", Left)
                xmlLocation.SetAttributeValue("Top", Top)

                xmlSize.SetAttributeValue("Width", Width)
                xmlSize.SetAttributeValue("Height", Height)
                Dim xmlWindowState = New XElement("WindowState", [Enum].GetName(GetType(FormWindowState), WindowState))
                xmlSettings.Add(xmlWindowState)
            Else
                ' добавить аттрибуты размерности в Location и Size element 
                xmlLocation.SetAttributeValue("Left", 0)
                xmlLocation.SetAttributeValue("Top", 0)

                xmlSize.SetAttributeValue("Width", 640)
                xmlSize.SetAttributeValue("Height", 480)
                Dim WindowState = New XElement("WindowState", [Enum].GetName(GetType(FormWindowState), FormWindowState.Normal))
                xmlSettings.Add(WindowState)
            End If

            Dim xmlDescription = New XElement("Description", Description)
            xmlSettings.Add(xmlDescription)

            '' создать namespace declaration xmlns:a и добавить в Size элемент 
            'Dim nsdecl = New XAttribute(XNamespace.Xmlns + "a", nsProgrammers)
            'Size.Add(nsdecl)

            ' создать элемент под текстовым узлом
            'xmlSize.SetElementValue(nsProgrammers + "programmer", "Michelangelo")

            'Dim programmer = New XElement(nsProgrammers + "programmer", "Leonardo ", "da ", "Vinci")
            'Size.AddFirst(programmer)

            'programmer = New XElement(nsProgrammers + "programmer")
            'Size.Add(programmer)
            'Dim cdata = New XText("Donatello")
            'programmer.Add(cdata)

            '' создать комментарий и добавить элемент
            'Dim comment = New XComment("вставить Size здесь")
            'Settings.Add(comment)

            xmlDocumentSettings.Save(Manager.PathSettingXml)
        Catch ex As Exception
            MessageBox.Show(Me,
                            String.Format("Невозможно сохранить настройки в конфигурационном файле.{0}Error: {1}", Environment.NewLine, ex.Message),
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

End Class

'******************************************************************************
'Public Overloads Sub Show(ByVal Description As String, _
'    Optional ByVal Image As System.Drawing.Bitmap = Nothing, _
'    Optional ByVal BackEnabled As Boolean = True)

'    'Me.lblDescription.Text = Description
'    'Me.PictureBox1.Image = Image
'    'Me.cmdBack.Enabled = BackEnabled
'    Me.Show()

'    'myContr.Location = New System.Drawing.Point(16, 160)
'    'myContr.Name = "Button1"
'    'myContr.Size = New System.Drawing.Size(75, 23)
'    'myContr.TabIndex = 5
'    'myContr.Text = "Button1"
'    'myContr.UseVisualStyleBackColor = True

'    'myContr.AutoEllipsis = True

'    'Me.Controls.Add(Me.myContr)

'    'myscrollingtext.Location = New System.Drawing.Point(16, 160)
'    'myscrollingtext.Name = "Button1"
'    'myscrollingtext.Size = New System.Drawing.Size(275, 23)
'    'myscrollingtext.TabIndex = 5
'    'myscrollingtext.Text = "Button1 erwerwe "

'    'myscrollingtext.AutoScroll = True
'    'myscrollingtext.AutoSize = False

'    'Me.Controls.Add(Me.myscrollingtext)

'    'Dim m_ChildFormNumber As Integer
'    'For I As Integer = 1 To 1
'    '    Dim ChildForm As New Explorer1 'System.Windows.Forms.Form
'    '    ' Создать в родительской MDI дочерние перед выводом на экран.
'    '    ChildForm.MdiParent = Me

'    '    m_ChildFormNumber += 1
'    '    ChildForm.Text = "Window " & m_ChildFormNumber
'    '    ChildForm.Show()
'    'Next

'    ' LoadИзмеренныеПараметры()
'    ' LoadНастроечныеПараметры()
'    ' LoadРасчетныеПараметры()
'End Sub


'Protected Overridable Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
'    System.Windows.Forms.Application.Exit()
'End Sub

'Private m_NextForm As BaseForm
'Private m_PreviousForm As BaseForm

'Public Property PreviousForm() As BaseForm
'    Get
'        Return m_PreviousForm
'    End Get
'    Set(ByVal Value As BaseForm)
'        m_PreviousForm = Value
'    End Set
'End Property

'Public Property NextForm() As BaseForm
'    Get
'        Return m_NextForm
'    End Get
'    Set(ByVal Value As BaseForm)
'        m_NextForm = Value
'    End Set
'End Property

'Private Sub BaseForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
'    System.Windows.Forms.Application.Exit()
'End Sub

'Private Sub lblDescription_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles lblDescription.Paint
'    Dim g As Graphics = e.Graphics
'    Dim rect As New Rectangle(sender.Left, sender.Top, sender.Width, sender.Height)
'    'Dim brush As New Drawing2D.LinearGradientBrush(rect, color1, color2, angle)
'    Dim brush As New Drawing2D.LinearGradientBrush(rect, Color.DarkRed, Color.Gold, Drawing2D.LinearGradientMode.Vertical)

'    g.FillRectangle(brush, rect)
'End Sub

'******************************************************************************

