using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using GGKService.Common.Config;
using GGKService.Common.Config.DbHelpers;
using GGKService.Common.Extensions;

namespace GGKService.Common.Helpers.Logs{

	/// <summary>
	/// Сообщения
	/// </summary>
	public class LogMessage : CommonObject
	{

		public static string TableName{
			get { return "LogMessages"; }
		}

		/// <summary>
		/// Тип сообщения
		/// </summary>
		public MessageType MessageType { get; set; }

		/// <summary>
		/// Метод с которого вызывалось логирование
		/// </summary>
		public string Method { get; set; }

		/// <summary>
		/// Наименование получателя(ГО)
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Ссылка на сервис для отправки уведомления
		/// </summary>
		public string Description { get; set; }

		public static void Info(string message, string description = null){
			Log(MessageType.Info, message, description);
		}

		public static void Warning(string message, string description = null){
			Log(MessageType.Warning, message, description);
		}

		public static void Error(string message, string description = null){
			Log(MessageType.Error, message, description);
		}

		public static void Error(string message, Exception exception){
			Log(MessageType.Error, message, exception.Message + " | " + exception.StackTrace, 1);
			if (exception.InnerException != null) {
				Error(message, exception.InnerException);
			}
		}

		private static void Log(MessageType messageType, string message, string description = null, int frameInc = 0){
			try {
				var method = "";
				try {
					StackTrace st = new StackTrace();
					method = st.GetFrame(1 + frameInc).GetMethod().ReflectedType.FullName + "." + st.GetFrame(1 + frameInc).GetMethod().Name;

					Logger.Log.Debug("Method0:" + st.GetFrame(0).GetMethod().ReflectedType.FullName + "." + st.GetFrame(0).GetMethod().Name);
					Logger.Log.Debug("Method1:" + st.GetFrame(1).GetMethod().ReflectedType.FullName + "." + st.GetFrame(1).GetMethod().Name);
					Logger.Log.Debug("Method2:" + st.GetFrame(2).GetMethod().ReflectedType.FullName + "." + st.GetFrame(2).GetMethod().Name);
				}
				catch
				{
				}

				Logger.Log.Debug("Method:" + method);

				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
				{
					sqlConnection.Open();
					var result = DbHelper.InsertRecord(string.Format("insert into {0} (MessageType, Message, Description, Method) values(@messageType, @message, @description, @method);", TableName),
					new
					{
						messageType = messageType,
						message = message.Trunc(2000),
						description = description.Trunc(2000),
						method = method.Trunc(500)
					});
					sqlConnection.Close();
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Не удалось сохранить данные лога",ex);
			}

		}

		public static int GetCount()
		{
			try
			{
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
				{
					sqlConnection.Open();

					var query = string.Format("select count(*) from {0}", TableName);
					var oid = sqlConnection.ExecuteScalar(query);

					sqlConnection.Close();

					var id = oid.ToString();
					return int.Parse(id);
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Debug("Не удалось получить кол-во записей", ex);
				throw;
			}
		}

		public static List<LogMessage> GetMessages(int count, bool isInfo, bool isWarning, bool isError, DateTime? date)
		{
			try
			{
				var messageTypes = "";
				if (isInfo)
				{
					messageTypes += "0";
				}
				if (isWarning)
				{
					messageTypes += messageTypes.IsNullOrEmpty() ? "1" : ",1";
				}
				if (isError)
				{
					messageTypes += messageTypes.IsNullOrEmpty() ? "2" : ",2";
				}

				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
				{
					sqlConnection.Open();

					var query = string.Format(@"select top {0} * from {1}
where messageType in({2}) {3} order by Oid desc", count, TableName, messageTypes, date.HasValue ? " and  CONVERT(nvarchar(MAX), CreatedDate, 104)=@date " : "");

					var notifications = sqlConnection.Query<LogMessage>(query, new
					{
						date = date
					});
					sqlConnection.Close();

					return notifications.ToList();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Debug("Не удалось получить сообщения", ex);
				throw;
			}
		}

		public static List<object> GetStrings(List<LogMessage> list)
		{
			var rows = new List<object>();
			try
			{
				foreach (var queue in list)
				{
					var columns = new
					{
						Oid = queue.Oid.ToString(),
						MessageType = queue.MessageType.ToString(),
						queue.Method,
						queue.Message,
						queue.Description,
						CreatedDate = queue.CreatedDate.ToString("dd.MM.yyyy HH:mm:ss")
					};
					rows.Add(columns);
				}
				return rows;
			}
			catch (Exception ex)
			{
				Logger.Log.Debug(ex);
				return rows;
			}
		}

		#region Archiving and DelData

		public static List<LogMessage> GetData(int count, DateTime date, SqlConnection sqlConnection, DbTransaction transaction = null) {
	        try {
	            var query = string.Format(@"select top {0} *
                        from {1}
                        where CreatedDate is null 
                           or CONVERT(date,CreatedDate,104)<=CONVERT(date,@date,104)
                        order by oid", count, TableName);

	            var result = sqlConnection.Query<LogMessage>(query, new {date}, transaction, commandTimeout: 180);

	            return result.ToList();
	        }
	        catch (Exception ex) {
	            Logger.Log.Debug("Не удалось получить сообщения", ex);
	            throw;
	        }
        }

        #endregion
    }
}
