using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using GGKService.Common.Config;

namespace GGKService.Common.Config.DbHelpers{

	public static class DbHelper{

		/// <summary>
		/// Добавление новой записи с помощью запроса и объекта
		/// </summary>
		/// <param name="insertSql"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static int InsertRecord(string insertSql, Object obj){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();

					var oid = sqlConnection.ExecuteScalar(insertSql+
						" select cast(scope_identity() as bigint)", obj);

					var id = oid.ToString();
					return int.Parse(id);
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in Insert: " + insertSql, ex);
				return 0;
			}
		}

		/// <summary>
		/// Изменение данных с помощью запроса и объекта
		/// </summary>
		/// <param name="updateSql"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool UpdateRecord(string updateSql, Object obj){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();
					sqlConnection.Execute(updateSql, obj);
					return true;
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in update: " + updateSql, ex);
				return false;
			}
		}

		/// <summary>
		/// Выполнение запроса
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public static bool ExecuteSql(string sql, object param = null){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();
					var code = sqlConnection.Execute(sql, param);
					return true;
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in execute: " + sql, ex);
				return false;
			}
		}

		/// <summary>
		/// Выполнение запроса
		/// </summary>
		/// <param name="con"></param>
		/// <param name="sql"></param>
		/// <param name="param"></param>
		/// <param name="tran"></param>
		/// <returns></returns>
		public static bool ExecuteSql(this IDbConnection con, string sql, object param, IDbTransaction tran = null){
			try {
				var code = con.Execute(sql, param, tran);
				return true;
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in execute: " + sql, ex);
				return false;
			}
		}

		/// <summary>
		/// Добавление новой записи в транзакции
		/// </summary>
		/// <param name="con"></param>
		/// <param name="tableName"></param>
		/// <param name="param"></param>
		/// <param name="tran"></param>
		/// <returns></returns>
		public static int ExecInsert(this IDbConnection con, string tableName, dynamic param, IDbTransaction tran = null){
			try {
				string sql = DynamicQuery.GetInsertQuery(tableName, param);
				var oid = con.ExecuteScalar(sql, (object)param, tran);

				var id = oid.ToString();
				return int.Parse(id);
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in update " + tableName, ex);
				return 0;
			}
		}

		/// <summary>
		/// Добавление новой записи
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public static int ExecInsert(string tableName, dynamic param){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();
					string sql = DynamicQuery.GetInsertQuery(tableName, param);
					var oid = sqlConnection.ExecuteScalar(sql, (object)param);

					var id = oid.ToString();
					return int.Parse(id);
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in update " + tableName, ex);
				return 0;
			}
		}

		/// <summary>
		/// Удаление записи
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="oid"></param>
		/// <returns></returns>
		public static bool ExecDelete(string tableName, long oid){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();
					string sql = string.Format("delete from {0} where oid={1}", tableName, oid);
					sqlConnection.Execute(sql);
					return true;
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in delete row from " + tableName + " oid=" + oid, ex);
				return false;
			}
		}

        /// <summary>
        /// Удаление записи в транзакции
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tableName"></param>
        /// <param name="oid"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool ExecDelete(this IDbConnection con, string tableName, long oid, IDbTransaction tran = null) {
            try {
                string sql = string.Format("delete from {0} where oid={1}", tableName, oid);
                con.Execute(sql, null, tran, 180);
                return true;
            }
            catch (Exception ex) {
                Logger.Log.Debug("Error in delete row from " + tableName + " oid=" + oid, ex);
                return false;
            }
        }

        /// <summary>
        /// Удаление записи в транзакции
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool ExecDelete(this IDbConnection con, string tableName, string where, IDbTransaction tran = null) {
            try {
                string sql = string.Format("delete from {0} where {1}", tableName, where);
                con.Execute(sql, null, tran, 180);
                return true;
            }
            catch (Exception ex) {
                Logger.Log.Debug("Error in delete row from " + tableName + " " + where, ex);
                return false;
            }
        }

        /// <summary>
		/// Изменение данных
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="param"></param>
		/// <param name="tran"></param>
		/// <returns></returns>
		public static bool ExecUpdate(string tableName, dynamic param, IDbTransaction tran = null){
			try {
				using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString)) {
					sqlConnection.Open();

					string sql = DynamicQuery.GetUpdateQuery(tableName, param);
					sqlConnection.Execute(sql, (object)param, tran);

					return true;
				}
			}
			catch (Exception ex) {
				Logger.Log.Debug("Error in update " + tableName, ex);
				return false;
			}
		}
	}
}