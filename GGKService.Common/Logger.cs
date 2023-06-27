using log4net;
using log4net.Config;

namespace GGKService.Common
{
    /// <summary>
    /// Класс для логгирования событий
    /// </summary>
    public class Logger {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static ILog Log {
            get { return log; }
        }

        public static void InitLogger() {
            XmlConfigurator.Configure();
        }

        public const string InfoMessage001 = "Операция формирования платежа: Получен заказ [{0}] на формирование платежа.";
        public const string InfoMessage002 = "Операция формирования платежа: Присвоен код платежа [{0}] для заказа [{1}].";
        public const string InfoMessage003 = "Операция формирования платежа: Платеж [{0}] успешно сохранен в БД.";
        public const string InfoMessage004 = "Операция формирования платежа: Отправлен URL для оплаты платежа [{0}].";
        public const string InfoMessage005 = "Операция формирования платежа: Проверка сертификата на валидность: TRUE.";
        public const string InfoMessage006 = "Операция формирования платежа: Проверка подписи на валидность: TRUE.";
        public const string InfoMessage007 = "Операция фиксаций платежа: Получен рефернс транзакций от Банка [{0}] по коду платежа [{1}] : [{2}].";
        
        public const string InfoMessage101 = "Операция оплаты: Инициализирована страница для оплаты платежа [{0}].";
        public const string InfoMessage102 = "Операция оплаты: Выбран банк [{0}] для оплаты платежа [{1}].";
        public const string InfoMessage103 = "Операция оплаты: Сформирован пакет данных для ККБ по платежу [{0}].";
        public const string InfoMessage104 = "Операция оплаты: Отправлен пакет данных для ККБ по платежу [{0}].";
        public const string InfoMessage105 = "Операция оплаты: Получен ответ по платежу [{0}].";
        public const string InfoMessage106 = "Операция оплаты: Сохранен факт оплаты в БД по платежу [{0}].";
        public const string InfoMessage107 = "Операция оплаты: Инициализирована страница с чеком по платежу [{0}].";
        public const string InfoMessage108 = "Операция оплаты: Переход на домашнюю страницу после осуществления платежа [{0}].";
		
        public const string InfoMessage201 = "Запрос чека: Получен код платежа [{0}].";
        public const string InfoMessage202 = "Запрос чека: Платеж [{0}] не найден.";
        public const string InfoMessage203 = "Запрос чека: Платеж [{0}] найден.";
        public const string InfoMessage204 = "Запрос чека: Отправлен ответ на запрос по платежу [{0}].";

        public const string InfoMessage301 = "Запрос списка чеков: Получен период платежей [{0}-{1}].";
        public const string InfoMessage302 = "Запрос списка чеков: Платежей за период [{0}-{1}] найдено [{2}].";
        public const string InfoMessage303 = "Запрос списка чеков: Отправлен ответ на запрос платежей за период [{0}-{1}].";

        public const string InfoMessage401 = "Уведомление об ошибке от Банка";

        public const string InfoMessage501 = "TransactionInfo is NULL (Пустой запрос)";
        public const string InfoMessage502 = "OrderInfo is NULL (Пустой запрос на заказ)";
        public const string InfoMessage503 = "OrderNumber is NULL (Отсутствует номер заказа)";
        public const string InfoMessage504 = "IIN is not valid (Неверный ИИН)";
        public const string InfoMessage505 = "Service count equal 0 (Количество услуг равно 0)";
        public const string InfoMessage506 = "Service is NULL (Пустая услуга)";
        public const string InfoMessage507 = "Service is NULL (Пустая услуга)";
        public const string InfoMessage508 = "Paycode not found (Не найден код платежа)";
        public const string InfoMessage509 = "IIN is EMPTY (Параметр ИИН пустой)";

        public const string ErrorMessage001 = "Ошибка сохранения в БД.";
        public const string ErrorMessage002 = "Ошибка формирования пакета данных для Банка.";
        public const string ErrorMessage003 = "Ошибка инициализации страницы оплаты.";
        public const string ErrorMessage004 = "Ошибка получения данных от ККБ.";
        public const string ErrorMessage005 = "Ошибка инициализации страницы c чеком.";
        public const string ErrorMessage006 = "Проверка сертификата на валидность: FALSE.";
        public const string ErrorMessage007 = "Ошибка десериализации Xml.";
        public const string ErrorMessage008 = "Проверка подписи на валидность: FALSE.";
        public const string ErrorMessage009 = "Ошибка получения данных от КН.";
        public const string ErrorMessage010 = "Ошибка операции формирования платежа.";
        public const string ErrorMessage011 = "Ошибка Уведомление об ошибке от Банка";
        public const string ErrorMessage012 = "Ошибка формирования данных для оплаты";
        public const string ErrorMessage013 = "Ошибка при обработке запроса от Wooppay по латежу {0}";
        public const string ErrorMessage014 = "Подпись банка не прошла верификацию";

        public const string InfoMessage601 = "Формирование данных для выставления счета Wooppay по платежу {0}";
        public const string InfoMessage602 = "Отправка данных для выставления счета Wooppay по платежу {0}";
        public const string InfoMessage603 = "Перенаправление пользователя на страницу оплаты Wooppay по платежу {0}";
        public const string InfoMessage604 = "Проверка ЭЦП запроса подтверждения платежа от Wooppay";
        public const string InfoMessage605 = "Запрос от Wooppay прошел процерку ЭЦП по платежу {0}";
        public const string InfoMessage606 = "Запрос от Wooppay не прошел процерку ЭЦП. Будет отправлен ответ по платежу {0}";
        public const string InfoMessage607 = "Отправка ответа в Wooppay по платежу {0}";
        public const string InfoMessage608 = "Запрос от Wooppay пришел с ошибкой по платежу {0}";

        public const string ResponseMessage01 = "Успешная обработка";
        public const string ResponseMessage02 = "По данному номеру транзакция не найдена";
        public const string ResponseMessage03 = "Платеж уже осуществлен";
        public const string ResponseMessage04 = "Ошибка проверки данных отправителя";
        public const string ResponseMessage05 = "Ошибка проверки ЭЦП";
        public const string ResponseMessage06 = "Неопознанная ошибка процесса выполнения";

        public const string RegCardMessage001 = "Операция регистрации карты: Запрос на регистрацию [{0}] успешно сохранен в БД.";
        public const string RegCardMessage002 = "Операция регистрации карты: Отправлен URL для регистрации карты [{0}].";
        public const string RegCardMessage003 = "Операция регистрации карты: Получен ответ по номеру запроса [{0}].";
        public const string RegCardMessage004 = "Операция регистрации карты: Сохранен факт регистрации карты в БД по номеру запроса [{0}].";
    }
}