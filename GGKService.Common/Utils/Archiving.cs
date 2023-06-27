using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace GGKService.Common.Utils{ 

	public static class Archiving {

		public static byte[] Compress(byte[] data){
			using (var compressedStream = new MemoryStream())
				using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress)){
					zipStream.Write(data, 0, data.Length);
					zipStream.Close();
					return compressedStream.ToArray();
				}
		}

		public static byte[] Decompress(byte[] data){
			using (var compressedStream = new MemoryStream(data))
				using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
					using (var resultStream = new MemoryStream()){
						zipStream.CopyTo(resultStream);
						return resultStream.ToArray();
					}
		}

		public static string PackFile(string packedFilePath, string sourceFile){
			try{
				//return CmdExecute("winrar.exe", "-y x " + rarFilePath + " " + dirName);
				var numOfError = 0;
				//var UnPackArgementsFormat = "-y x {0} {1}";
				//var UnPackArgementsFormat = "x {0} -o{1} -aoa";
				//var UnPackFilePath = "winrar.exe";
				//var UnPackFilePath = @"C:\Program Files\7-Zip\7z.exe";
				// инициализация команды

				var packExeFilePath = ConfigurationManager.AppSettings["packExeFilePath"];
				var packParameters = ConfigurationManager.AppSettings["packParameters"];
				var cmdProcess = new Process{
					StartInfo = {
						// что запускать
						FileName = packExeFilePath,
						WindowStyle = ProcessWindowStyle.Hidden,
						// параметры
						Arguments = string.Format(packParameters, packedFilePath, sourceFile)
					}
				};
				// запуск				
				cmdProcess.Start();
				// ждем завершения
				cmdProcess.WaitForExit();
				numOfError = cmdProcess.ExitCode;
				return numOfError.ToString();
			}
			catch (Exception ex){
				Logger.Log.Error("Не удалось заархивировать", ex);
				return "-666";
			}
		}
	}
}
