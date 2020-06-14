using Microsoft.Extensions.Logging;

namespace WebLab.Services
{
	public class FileLoggerProvider : ILoggerProvider {
		// путь к файлу логирования
		private string _filepath;
		/// <summary> 
		/// Констркутор
		/// </summary>
		/// <param name="path">путь к файлу логирования</param>
		public FileLoggerProvider(string path) {
			_filepath = path;
		}

		public ILogger CreateLogger(string categoryName) {
			return new FileLogger(_filepath);
		}

		public void Dispose() {
		}
	}
}
