using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace WebLab.Services
{
	public class FileLogger<T> : ILogger<T> {
		// путь к файлу логирования
		private string _filePath;
		// объект для синхронизации записи в файл
		private object _lock;
		/// <summary>
		/// Констркутор
		/// </summary>
		/// <param name="path">путь к файлу логирования</param>
		public FileLogger(string path) {
			_filePath = path;
			_lock = new object();
		}
		public IDisposable BeginScope<TState>(TState state) {
			return null;
		}

		public bool IsEnabled(LogLevel logLevel) {
			// логирование разрешено для уровня Infomation
			return logLevel == LogLevel.Information;
		}

		public void Log<TState>(LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception exception,
		Func<TState, Exception, string> formatter) {
			if (formatter != null) {
				lock (_lock) {
					File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
				}
			}
		}
	}
}
