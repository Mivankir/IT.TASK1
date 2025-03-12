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

        
        private void btnAddMessage_Click(object sender, RoutedEventArgs e)  // ���������� ���������
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

        
        private void btnFilterByType_Click(object sender, RoutedEventArgs e)        // ���������� �� ����
        {
            if (cmbFilterType.SelectedIndex == 0) // "All"
            {
                
                UpdateMessageList();        // �������� ��� ���������
            }
            else
            {
                // ����������� �� ���������� ����
                LogType filterType = (LogType)(cmbFilterType.SelectedIndex - 1); // -1, ������ ��� ������ ������� "All"
                var filteredMessages = _logManager.GetMessagesByType(filterType);
                DisplayMessages(filteredMessages);
            }
        }

        
        private void btnFilterByDate_Click(object sender, RoutedEventArgs e)    // ���������� �� ��������� �������
        {
            DateTime startDate = dtpStartDate.SelectedDate?.DateTime ?? DateTime.MinValue;
            DateTime endDate = dtpEndDate.SelectedDate?.DateTime ?? DateTime.MaxValue;

            var filteredMessages = _logManager.GetMessagesByDateRange(startDate, endDate);
            DisplayMessages(filteredMessages);
        }

        
        private async void btnSaveToFile_Click(object sender, RoutedEventArgs e)    // ���������� � ����
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(new FileDialogFilter { Name = "Text Files", Extensions = { "txt" } });
            var result = await saveFileDialog.ShowAsync(this);

            if (result != null)
            {
                _logManager.SaveMessagesToFile(result);
            }
        }

        
        private void UpdateMessageList()    // ���������� ������ ���������
        {
            DisplayMessages(_logManager.GetMessagesByDateRange(DateTime.MinValue, DateTime.MaxValue));
        }

        
        private void DisplayMessages(List<LogMessage> messages)     // ����������� ���������
        {
            lstMessages.Items.Clear();
            foreach (var message in messages)
            {
                lstMessages.Items.Add(message.ToString());
            }
        }
    }
}