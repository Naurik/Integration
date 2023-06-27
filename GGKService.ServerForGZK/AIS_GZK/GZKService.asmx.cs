using GGKService.Common;
using GGKService.Common.Config;
using GGKService.Common.Interfaces;
using GGKService.ServerForGZK.AIS_GZK.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

namespace GGKService.ServerForGZK.AIS_GZK
{
    /// <summary>
    /// Сводное описание для GZKService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class GZKService : System.Web.Services.WebService
    {

        [WebMethod]
        [XmlInclude(typeof(GGKService.Common.Classes.GZKRequestResponse.GISendActualDataRequest))]
        [XmlInclude(typeof(GGKService.Common.Classes.GZKRequestResponse.GISendActualDataResponse))]
        public string SendMessage(SendMessage sendMessage)
        {
            try
            {
                // ОТОБРАЖЕНИЕ ТОГО ЧТО ПРИШЛО В ЛОГИ
                // Определение типа пришедших данных
                if (sendMessage.request.requestData.data is String)
                {
                    Logger.Log.Debug("is String!");
                    //Короч мозгов не хватило это сделать, ругается что не может преобразовать в строку, но нужно ее передать как строку
                    //return ImplForString(sendMessage.request.requestData.data as String);
                }
                
                //// ЛОГИРОВАНИЕ ПРИШЕДШИХ ПАРАМЕТРОВ ОСНОВНОГО ПАКЕТА

                Logger.Log.Debug("Не удалось определить метод. xml=" + sendMessage.request.requestData.data);
                throw new Exception("Не удалось определить метод");
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Ошибка проверки типа сообщения. xml=" + sendMessage.request.requestData.data);
                throw new Exception("Ошибка проверки типа сообщения. ", ex);
            }
        }

        private SendMessageResponse ImplForString(string xmlString)
        {
            Logger.Log.Debug("Пришедшие данные: " + xmlString);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            //ПОЛУЧАЕМ НАИМЕНОВАНИЕ КОРНЕВОГО ЭЛЕМЕНТА
            var envelope = xmlDocument.DocumentElement != null ? xmlDocument.DocumentElement.Name : "";

            //Получаем первое слово
            if (envelope.IndexOf(" ") > 0)
                envelope = envelope.Substring(1, envelope.IndexOf(" "));

            //если в нем есть ":"(префикс) - убираем его
            if (envelope.IndexOf(":") > 0)
                envelope = envelope.Substring(envelope.IndexOf(":") + 1);

            switch (envelope.ToLower())
            {
                case "GISendActualDataRequest":
                    return GzkImpl.GzkImplResp(xmlString);
                default:
                    Logger.Log.Debug("Не удалось определить тип пакета. xml=" + xmlString);
                    Logger.Log.Debug("Корневой элемент =" + envelope);
                    return ConfigHelper.Error("Не удалось определить тип пакета для элемента " + envelope);
            }
        }
    }
}
