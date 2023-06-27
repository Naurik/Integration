using System;
using System.IO;

namespace EGov.Payments.Commons.Utils{

	public static class Signing{

		public static string XorString(string source, int key){
			string resultString = "";
			for (int i = 0; i < source.Length; i++){
				int charValue = Convert.ToInt32(source[i]);
				charValue ^= key;
				resultString += char.ConvertFromUtf32(charValue);
			}
			return resultString;
		}
		
		public static byte[] StreamToByteArray(Stream input){
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream()){
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0){
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public static byte[] GetBytes(string str){
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string GetString(byte[] bytes){
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

	}
}
