using GGKService.Common.Helpers.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GGKService.Common.Config{

	public static class CertificateHelper
    {
        public static bool CheckSigningXmlData(string xml)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(xml);
                //if (!CertificateChecker.StreamXmlCertificateIsValid(bytes))
                //{
                //    Logger.Log.Debug("Сертификат не прошел проверку");
                //    return false;
                //}
                return true;
            }
            catch(Exception ex)
            {
                Logger.Log.Debug("Ошибка в процессе проверки подписанных данных", ex);
                return false;
            }
        }
    }

}