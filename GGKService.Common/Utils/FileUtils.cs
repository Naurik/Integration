using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGKService.Common.Utils{
	public static class FileUtils{

		public static string ReadAllTextFromFile(string filePath) {
			return System.IO.File.ReadAllText(filePath);
		}

		public static void WriteAllTextToFile(string filePath, string data) {
			System.IO.File.WriteAllText(filePath, data);
		}

	}
}
