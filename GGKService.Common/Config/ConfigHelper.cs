using GGKService.Common.Extensions;
using GGKService.Common.Helpers.Settings;
using GGKService.Common.Interfaces;
using GGKService.Common.Objects.DBTables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GGKService.Common.Config{

	public static class ConfigHelper{

        private static List<ConfigParameter> _parameters;

        public static void LoadParamenters()
        {
            _parameters = ConfigParameter.GetParameters();
        }

		public static string GetValue(string key) {
			if(string.IsNullOrEmpty(key))
				return null;
			
			if (_parameters == null) {
				LoadParamenters();
			}
			if (_parameters == null) {
				Logger.Log.Debug("Не удалось загрузить параметры конфигурации");
				return null;
			}
			if (_parameters.Count == 0) {
				Logger.Log.Debug("_parameters.Count = 0");
				return null;
			}

			var val = _parameters.FirstOrDefault(x => x.KeyValue.ToLower().Equals(key.ToLower()));
			return val != null ? val.Value : null;
		}

        //public static string GetValueUser(string iin)
        //{
        //    if (iin.IsNullOrEmpty())
        //        return null;

        //    if (_users == null)
        //    {
        //        LoadUsers();
        //    }
        //    if (_users == null)
        //    {
        //        Logger.Log.Debug("Не удалось загрузить пользователей");
        //        return null;
        //    }
        //    if (_users.Count == 0)
        //    {
        //        Logger.Log.Debug("_users.Count = 0");
        //        return null;
        //    }

        //    var val = _users.FirstOrDefault(x => x.IIN.ToLower().Equals(iin));
        //    return val != null ? val.IIN : null;
        //}

        //public static bool IsAdmin(string iin)
        //{
        //    if (iin.IsNullOrEmpty())
        //        return false;

        //    if (_users == null)
        //    {
        //        LoadUsers();
        //    }
        //    if (_users == null)
        //    {
        //        Logger.Log.Debug("Не удалось загрузить пользователей");
        //        return false;
        //    }
        //    if (_users.Count == 0)
        //    {
        //        Logger.Log.Debug("_users.Count = 0");
        //        return false;
        //    }

        //    var val = _users.FirstOrDefault(x => x.IIN.ToLower().Equals(iin));
        //    return val != null ? val.isAdmin : false;
        //}

        public static String GetMD5Hash(String text)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

	    public static bool ValidPassword(string password)
	    {
	        if (password.Length < 8)
                return false;

	        var regex = new Regex(@"([!,@,#,$,%,^,&,*,?,_,~])");
	        var regex1 = new Regex(@"([0-9])");
            var regex2 = new Regex(@"([a-z].*[A-Z])|([а-я].*[А-Я])|([A-Z].*[a-z])|([А-Я].*[а-я])");
            var regex3 = new Regex(@"admin|password|test|qwerty|111111|123123|321369|windows|abc123|helpme|123qwe|administrator|hello|cisco|654321|root|1q2w3e4r|iloveyou|159753|forward|lol123|test123|zxcvbnm|abcd1234|secret|backward|aaaaaa|welcome|123123123|guest|12qwaszx|user|computer|pshep|nitec");

            if (regex.IsMatch(password) && regex1.IsMatch(password) && regex2.IsMatch(password) && !regex3.IsMatch(password.ToLower()))
	            return true;
            return false;
	    }

		public static string ConnectionString {
			get  {
				return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			}
		}

		#region GZK

		public static string GZKEndPoint{
			get {
				var endpoint = GetValue("endPointGZK");
				if (string.IsNullOrEmpty(endpoint))
					endpoint = ConfigurationManager.AppSettings["endPointGZK"];
				return endpoint;
			}
		}

		public static string FirstRequestDate{
			get {
				var value = GetValue("dateDataFirstRequest");
				if (string.IsNullOrEmpty(value))
					value = ConfigurationManager.AppSettings["dateDataFirstRequest"];
				return value;
			}
		}

		#endregion

		public static string ExcludeSystemsFromQueue{
			get {
				var val = GetValue("ExcludeSystemsFromQueue");
				return val;
			}
		}

		public static Clients.ShepSynchService.SenderInfo PshepSenderInfo{
			get {
				return new Clients.ShepSynchService.SenderInfo {
					senderId = "pshep",
					password = "pshep"
				};
			}
		}

        public static Boolean IsWssEnabled {
            get {
                var enabled = false;
                var isWssEnabled = GetValue("IsWssEnabled");
                if (string.IsNullOrEmpty(isWssEnabled))
                    isWssEnabled = ConfigurationManager.AppSettings["IsWssEnabled"];
                if (!string.IsNullOrEmpty(isWssEnabled) && Boolean.TryParse(isWssEnabled, out enabled))
                    return enabled;
                return enabled;
            }
        }

        public static bool verifySignature {
            get {
                var verifySignature = ConfigurationManager.AppSettings["verifySignature"];
                if (!string.IsNullOrEmpty(verifySignature) && verifySignature.ToLower().Equals("true"))
                    return true;
                return false;
            }
        }

        public static bool IsTest
        {
            get
            {
                var isTest = ConfigurationManager.AppSettings["isTest"];
                if(!isTest.IsNullOrEmpty() && isTest.ToLower().Equals("true"))
                {
                    return true;
                }
                return false;
            }
        }

        public static string ConnectionStringArchive {
            get {
                return ConfigurationManager.ConnectionStrings["ConnectionStringArchive"].ConnectionString;
            }
        }

        public static SendMessageResponse Error(string errorMessage)
        {
            return new SendMessageResponse
            {
                response = new SyncSendMessageResponse
                {
                    responseData = new MessageData
                    {
                        data = errorMessage
                    }
                }
            };
        }

        public static SendMessageResponse Success(List<TableOne> successMessage)
        {
            return new SendMessageResponse
            {
                response = new SyncSendMessageResponse
                {
                    responseData = new MessageData
                    {
                        data = successMessage
                    }
                }
            };
        }
    }

}