using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.IO;

namespace AvaloniaLogManagerApp.Views
{
    public partial class MainWindow : Window
    {
        private LogManager _logManager;

        public MainWindow()
        {
            InitializeComponent();
            _logManager = new LogManager();
        }

        
        private void btnAddMessage_Click(object sender, RoutedEventArgs e)  // Добавление сообщения
        {
            LogType type = (LogType)cmbMessageType.SelectedIndex;
            DateTime dateTime = dtpDateTime.SelectedDate?.DateTime ?? DateTime.Now;
            string text = txtMessageText.Text;

            LogMessage message = new LogMessage
            {
                Type = type,
                DateTime = dateTime,
                Text = text
            };

            _logManager.AddMessage(message);
            UpdateMessageList();
        }

        
        private void btnFilterByType_Click(object sender, RoutedEventArgs e)        // Фильтрация по типу
        {
            if (cmbFilterType.SelectedIndex == 0) // "All"
            {
                
                UpdateMessageList();        // Показать все сообщения
            }
            else
            {
                // Фильтровать по выбранному типу
                LogType filterType = (LogType)(cmbFilterType.SelectedIndex - 1); // -1, потому что первый элемент "All"
                var filteredMessages = _logManager.GetMessagesByType(filterType);
                DisplayMessages(filteredMessages);
            }
        }

        
        private void btnFilterByDate_Click(object sender, RoutedEventArgs e)    // Фильтрация по диапазону времени
        {
            DateTime startDate = dtpStartDate.SelectedDate?.DateTime ?? DateTime.MinValue;
            DateTime endDate = dtpEndDate.SelectedDate?.DateTime ?? DateTime.MaxValue;

            var filteredMessages = _logManager.GetMessagesByDateRange(startDate, endDate);
            DisplayMessages(filteredMessages);
        }

        
        private async void btnSaveToFile_Click(object sender, RoutedEventArgs e)    // Сохранение в файл
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Text Files", Extensions = { "txt" } });
            var result = await saveFileDialog.ShowAsync(this);

            if (result != null)
            {
                _logManager.SaveMessagesToFile(result);
            }
        }

        
        private void UpdateMessageList()    // Обновление списка сообщений
        {
            DisplayMessages(_logManager.GetMessagesByDateRange(DateTime.MinValue, DateTime.MaxValue));
        }

        
        private void DisplayMessages(List<LogMessage> messages)     // Отображение сообщений
        {
            lstMessages.Items.Clear();
            foreach (var message in messages)
            {
                lstMessages.Items.Add(message.ToString());
            }
        }
    }
}