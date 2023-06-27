using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using GGKService.Common.Helpers;
using GGKService.Common.Config.DbHelpers;
using GGKService.Common.Config;

namespace GGKService.Common.Objects.DBTables
{

	/// <summary>
	/// Очередь для уведомлений
	/// </summary>
	public class TableOne : CommonObject
	{

		#region feilds
		public static string TableName
		{
			get { return "TableOne"; }
		}

		/// <summary>
		/// Дата для запроса
		/// </summary>
		public DateTime Date { get; set; }

		//Здесь либо нужно будет все поля таблицы прописать, либо как я уже сказал подумать и создать универсальный класс с одним скриптом для всех таблиц


		#endregion


		#region methods
		

		public static bool Delete(long queueOid)
		{
			return DbHelper.ExecDelete(TableName, queueOid);
		}

		public static List<TableOne> GetAllDATA(DateTime dateTime)
		{
			try
			{
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
				{
					sqlConnection.Open();

					//Здесь написать скрипт для текущей таблицы
					var query = string.Format("select * from TableOne where Date = {0}", dateTime);

					var notifications = sqlConnection.Query<TableOne>(query);
					sqlConnection.Close();

					return notifications.ToList();
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Debug("Не удалось получить уведомления из очереди", ex);
				throw;
			}
		}
		#endregion
	}

}

