using GGKService.Common.Classes.GZKRequestResponse;
using System;
using System.Configuration;
using log4net;
using GGKService.Common;
using GGKService.Common.Clients.ShepSynchService;
using SenderInfo = GGKService.Common.Clients.ShepSynchService.SenderInfo;
using SendMessageResponse = GGKService.Common.Clients.ShepSynchService.SendMessageResponse;
using SendMessage = GGKService.Common.Clients.ShepSynchService.SendMessage;
using GGKService.Common.Extensions;

namespace SHEP.GzkShepSynchService
{
    public static class GzkShepSynchService
    {
        /// <summary>
        /// Отправка пакета(сообщения) по универсальному синхронному каналу ШЭП
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static SendMessageResponse SendMessage(SendMessage message)
        {
            var service = new ISyncChannelHttpService
            {
                Url = ConfigurationManager.AppSettings["endPoint_ShepSynchService"]
            };
            var result = service.SendMessage(message);
            return result;
        }

        /// <summary>
        /// Отправка запроса 1 запроса
        /// </summary>
        public static GIRelevanceResponse SendFirstRequest(object gzkSendRequest)
        {
            try
            {
                var request = new SendMessage
                {
                    request = new SyncSendMessageRequest
                    {
                        requestInfo = new SyncMessageInfo
                        {
                            messageDate = DateTime.Now,
                            messageId = Guid.NewGuid().ToString(),
                            serviceId = "GzkGetRelevance",
                            sender = new SenderInfo
                            {
                                senderId = "pshep",
                                password = "pshep"
                            }
                        },
                        requestData = new MessageData
                        {
                            data = gzkSendRequest
                        }
                    }
                };

                //В логах запишется какой запрос отправляем
                Logger.Log.Info(request.SerializeObject(new Type[] { typeof(GIRelevanceInfo) }));

                var response = SendMessage(request);

                //В логах запишется какой ответ приходит
                Logger.Log.Info(response.SerializeObject(new Type[] { typeof(GIRelevanceInfo) }));

                if ((response == null) || (response.response == null) || (response.response.responseData == null) || response.response.responseData.data == null)
                {
                    Logger.Log.Debug("Системная Ошибка отправки в ГЗК, ответ пустой");
                    return new GIRelevanceResponse
                    {
                        AvailableInfo = new GIRelevanceInfo[]
                        {
                             //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                        }
                    };
                }

                if (!(response.response.responseData.data is GIRelevanceResponse))
                {
                    Logger.Log.Debug("Ошибка отправки в ГЗК. Не удалось привести ответ к заданному типу");
                    return new GIRelevanceResponse
                    {
                        AvailableInfo = new GIRelevanceInfo[]
                        {
                             //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                        }
                    };
                }
                return response.response.responseData.data as GIRelevanceResponse;

            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Системная Ошибка отправки в ГЗК", ex);
                if (ex.InnerException != null)
                    Logger.Log.Debug("InnerException=", ex.InnerException);
                return new GIRelevanceResponse
                {
                    //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                };
            }
        }

        /// <summary>
        /// Отправка запроса 2 запроса
        /// </summary>
        public static GIDataResponse SendSecondRequest(object gzkSendRequest)
        {
            try
            {
                var request = new SendMessage
                {
                    request = new SyncSendMessageRequest
                    {
                        requestInfo = new SyncMessageInfo
                        {
                            messageDate = DateTime.Now,
                            messageId = Guid.NewGuid().ToString(),
                            serviceId = "GzkGetData",
                            sender = new SenderInfo
                            {
                                senderId = "pshep",
                                password = "pshep"
                            }
                        },
                        requestData = new MessageData
                        {
                            data = gzkSendRequest
                        }
                    }
                };

                //В логах запишется какой запрос отправляем
                Logger.Log.Info(request.SerializeObject(new Type[] { typeof(GIDataRequest) }));

                var response = SendMessage(request);

                //В логах запишется какой ответ приходит
                Logger.Log.Info(response.SerializeObject(new Type[] { typeof(GIDataResponse) }));

                if ((response == null) || (response.response == null) || (response.response.responseData == null) || response.response.responseData.data == null)
                {
                    Logger.Log.Debug("Ошибка уведомления Гос Реестр. Нет данных ответа");
                    return new GIDataResponse
                    {
                        //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                    };
                }

                if (!(response.response.responseData.data is GIDataResponse))
                {
                    Logger.Log.Debug("Ошибка уведомления Гос Реестр. Не удалось привести ответ к заданному типу");
                    return new GIDataResponse
                    {
                        //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                    };
                }
                return response.response.responseData.data as GIDataResponse;
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Системная Ошибка отправки в ГЗК", ex);
                if (ex.InnerException != null)
                    Logger.Log.Debug("InnerException=", ex.InnerException);
                return new GIDataResponse
                {
                    //Здесь хотя бы должен был быть код и сообщение при ошибке, но этого нет в хсд ответа
                };
            }
        }
    }
}
