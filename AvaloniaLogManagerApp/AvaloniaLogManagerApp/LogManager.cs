using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvaloniaLogManagerApp
{
    public class LogManager
    {
        private List<LogMessage> _messages;

        public LogManager()
        {
            _messages = new List<LogMessage>();
        }

        
        public LogMessage this[int index]   // Индексатор для доступа к сообщениям
        {
            get
            {
                if (index >= 0 && index < _messages.Count)
                    return _messages[index];
                throw new IndexOutOfRangeException("Индекс выходит за пределы списка сообщений.");
            }
        }

       
        public int Count => _messages.Count;     // Количество сообщений

        
        public void AddMessage(LogMessage message)  // Добавление сообщения
        {
            _messages.Add(message);
        }

        
        public List<LogMessage> GetMessagesByType(LogType type)     // Получение списка сообщений по типу
        {
            return _messages.Where(m => m.Type == type).ToList();
        }

        
        public List<LogMessage> GetMessagesByDateRange(DateTime start, DateTime end)    // Получение списка сообщений по диапазону времени
        {
            return _messages.Where(m => m.DateTime >= start && m.DateTime <= end).ToList();
        }

        
        public void SaveMessagesToFile(string filePath)     // Сохранение сообщений в файл
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var message in _messages)
                {
                    writer.WriteLine(message.ToString());
                }
            }
        }
    }
}