using GGKService.Common;
using shep = SHEP.GzkShepSynchService.GzkShepSynchService;
using save = GGKService.Logic.GzkManager.GzkManager;
using GGKService.Common.Classes.GZKRequestResponse;
using System;
using System.Web.Mvc;

namespace GGKService.ClientForGZK.Controllers
{
    public class ActualDataSendController : Controller
    {
        // GET: ActualDataSend
        public ActionResult FirstRequest()
        {
            return View();
        }

        [HttpPost]
        public bool FirstRequest(string reqUser)
        {
            try
            {
                Logger.Log.Debug("RequestUser = " + reqUser);

                //Заполняем запрос из введеного нами представления RequestUser
                var request = new GIRelevanceRequest
                {
                    RequestUser = reqUser
                };

                //Отправляем по синхронному каналу ШЭП и получаем ответ от ГЗК
                var response = shep.SendFirstRequest(request);
                if ((response == null) || (response.AvailableInfo == null) || (response.AvailableInfo.Length < 1))
                {
                    throw new Exception("Ответ пустой");
                };

                //Полученный ответ сохраняем в бд
                long req = save.SaveGzkFirstRequestResult(response.AvailableInfo);
                if(req == 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error" + ex);
            }
            
        }

        public ActionResult SecondRequest()
        {
            return View();
        }

        [HttpPost]
        public bool SecondRequest(string reqUser, string layerName)
        {
            try
            {
                Logger.Log.Debug("RequestUser = " + reqUser);
                Logger.Log.Debug("LayerName = " + layerName);

                //Заполняем запрос из введеного нами представления RequestUser
                var request = new GIDataRequest
                {
                    RequestUser = reqUser,
                    LayerName = layerName
                };

                var response = shep.SendSecondRequest(request);
                if ((response == null) || (response.DataStructure == null) || (response.DataCount == 0) || (string.IsNullOrEmpty(response.LayerName)) || (string.IsNullOrEmpty(response.PushId)))
                {
                    throw new Exception("Ответ пустой");
                };

                //Полученный ответ сохраняем в бд
                long req = save.SaveGzkSecondRequestResult(response.DataStructure);
                if (req == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex);
            }

        }

    }
}