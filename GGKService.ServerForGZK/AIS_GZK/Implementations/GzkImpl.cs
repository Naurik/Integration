using System;
using System.Linq;
using GGKService.Common.Interfaces;
using GGKService.Common.Objects;
using GGKService.Common.Utils;
using GGKService.Common;
using GGKService.Common.Config;
using GGKService.Common.Classes.GZKRequestResponse;
using GGKService.Common.Extensions;
using GGKService.Common.Objects.DBTables;

namespace GGKService.ServerForGZK.AIS_GZK.Implementations
{
    public class GzkImpl
    {
		/// <summary>
		/// Реализация Gzk
		/// </summary>
		/// <returns></returns>
		public static SendMessageResponse GzkImplResp(String initReqXml)
		{
			try
			{
				Logger.Log.DebugFormat("GISendActualDataRequest. Получены xml данные: {0}", initReqXml);

				#region ПРОВЕРКА ПОДПИСИ
				//Todo Спрятано только для тестирования
				if (!ConfigHelper.IsTest)
				{
					//Нужно еще доработать не пашет проверка подписи
					if (!CertificateHelper.CheckSigningXmlData(initReqXml))
						return ConfigHelper.Error("Сертификат не прошел проверку или данные не соответствуют подписи");
				}

				#endregion

				#region ДЕСЕРИАЛИЗАЦИЯ ПОЛУЧЕННЫХ ДАННЫХ
				GISendActualDataRequest sendMessage;
				try
				{
					sendMessage = (GISendActualDataRequest)initReqXml.XmlDeserializeFromString(typeof(GISendActualDataRequest));
				}
				catch (Exception ex)
				{
					Logger.Log.Debug("Ошибка при десериализации данных", ex);
					return ConfigHelper.Error("Ошибка при десериализации данных");
				}
				#endregion

				//Здесь нужно доработать(Дату вставите или как искать данные в таблице из БД или вообще можете дату убрать и чисто все данные будут отправляться)
				//И самое главное измените TableOne на название таблицы из вашего БД, я просто не знаю как они у вас называются
				//Потом такая идея, возможно все таблицы нужно будет создать как класс TableOne и по одному их передавать, а возможно нет, тоже подумать нужно!!!
				var result = TableOne.GetAllDATA(DateTime.Now);
				return ConfigHelper.Success(result);


			}
			catch (Exception ex)
			{
				Logger.Log.Debug("Техническая ошибка инициализации платежа", ex);
				return ConfigHelper.Error("Техническая ошибка инициализации платежа");
			}
		}
	}
}