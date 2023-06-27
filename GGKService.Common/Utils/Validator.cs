using System;
using System.Linq;
using GGKService.Common.Extensions;
using GGKService.Common.Helpers;

namespace GGKService.Common.Utils{

	public class ValidInfo {
		public ValidInfo() {
			IsValid = true;
		}

		public ValidInfo(string code, string message) {
			IsValid = false;
			ErrorCode = code;
			ErrorMessage = message;
		}

		public bool IsValid;
		public string ErrorCode;
		public string ErrorMessage;
	}

	public static class Validator{

		public static bool ValidIdn(string idn) {
			if (idn.IsNullOrEmpty())
				return false;
			if (idn.Length != 12)
				return false;
			if(idn.Any(x=>!char.IsDigit(x)))
				return false;
			return true;
		}

		public static bool ValidBik(string bik) {
			if (bik.IsNullOrEmpty())
				return false;
			if (bik.Length != 8)
				return false;
			if (bik.Any(char.IsDigit))
				return false;
			return true;
		}
	}
}