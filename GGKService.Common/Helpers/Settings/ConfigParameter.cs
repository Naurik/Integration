using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using GGKService.Common.Config.DbHelpers;
using GGKService.Common.Config;
using GGKService.Common.Extensions;

namespace GGKService.Common.Helpers.Settings{

	public class ConfigParameter : CommonObject{

		public static string TableName{
			get { return "ConfigParameters"; }
		}

		/// <summary>
		/// Ключ
		/// </summary>
		public String KeyValue { get; set; }

		/// <summary>
		/// Значение 
		/// </summary>
		public String Value { get; set; }

		/// <summary>
		/// Описание 
		/// </summary>
		public String Description { get; set; }

		public static bool Add(string key, string value, string description) {
			try {
				//var sql = string.Format("insert into {0} set Key=@key, Value=@value, Description=@description", TableName);
				var i = DbHelper.ExecInsert(TableName, new{
					KeyValue = key,
					Value = value,
					Description = description
				});
			    Logger.Log.Debug("Пользователь " + HttpContext.Current.User.Identity.Name + ". Добавление параметра " + key +
			                     ": Value = " + value + "; Description = " + description);
				return true;
			}
			catch (Exception ex) {
				Logger.Log.Error(ex);
				return false;
			}
		}

		public static bool Update(string key, string value, string description) {
			try
			{
			    var varBU = GetValue(key);
				var sql = string.Format("update {0} set Value=@value, Description=@description where KeyValue=@key", TableName);
				var i = DbHelper.ExecuteSql(sql, new{
					key = key,
					value = value,
					description = description
				});
			    if (i)
			    {
			        Logger.Log.Debug("Пользователь " + HttpContext.Current.User.Identity.Name + ". Изменение параметра " + key +
			                         ": Value = " + varBU.Value + "=>" + value + "; Description = " + varBU.Description + "=>" +
			                         description);
			    }
			    return true;
			}
			catch (Exception ex) {
				Logger.Log.Error(ex);
				return false;
			}
		}

		public static bool Delete(string oid) {
			try {
                var varBU = GetValue(long.Parse(oid));
                var i = DbHelper.ExecDelete(TableName, long.Parse(oid));
			    if (i)
			    {
                    Logger.Log.Debug("Пользователь " + HttpContext.Current.User.Identity.Name + ". Удалил параметр " + varBU.KeyValue +
                                     ": Value = " + varBU.Value + "; Description = " + varBU.Description);			        
			    }
			    return i;
			}
			catch (Exception ex) {
				Logger.Log.Error(ex);
				return false;
			}
		}

		public static List<ConfigParameter> GetValues(int count) {
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();

					var query = string.Format("select top {0} * from {1} order by Oid", count, TableName);

					var payments = sqlConnection.Query<ConfigParameter>(query);
					sqlConnection.Close();

					return payments.ToList();
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Не удалось получить уведомления из очереди", ex);
				throw;
			}
		}

	    private static ConfigParameter GetValue(string key)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
                {
                    sqlConnection.Open();

                    var query = string.Format("select * from {0} where KeyValue='{1}'", TableName, key);

                    var payments = sqlConnection.Query<ConfigParameter>(query).FirstOrDefault();
                    sqlConnection.Close();

                    return payments;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Не удалось получить парамерт", ex);
                throw;
            }
        }

        public static ConfigParameter GetValue(long oid)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
                {
                    sqlConnection.Open();

                    var query = string.Format("select * from {0} where oid={1}", TableName, oid);

                    var payments = sqlConnection.Query<ConfigParameter>(query).FirstOrDefault();
                    sqlConnection.Close();

                    return payments;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Не удалось получить параметр", ex);
                throw;
            }
        }
		public static List<List<string>> GetTable(List<ConfigParameter> list)
		{

			var rows = new List<List<string>>();
			var header = new List<string> {
				"Oid",
				"KeyValue",
				"Value",
				"Description",
				"CreatedDate"
			};
			try
			{
				rows.Add(header);
				foreach (var queue in list)
				{
					var columns = new List<string> {
					queue.Oid.ToString(),
					queue.KeyValue.ToNullString(),
					queue.Value.ToNullString(),
					queue.Description.ToNullString(),
					queue.CreatedDate.ToDateTimeString(),
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

		public static List<ConfigParameter> GetParameters(){
			using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
				sqlConnection.Open();
                var list = sqlConnection.Query<ConfigParameter>("select * from " + TableName);

				sqlConnection.Close();
				return list.ToList();
			}
		}

	}
}
