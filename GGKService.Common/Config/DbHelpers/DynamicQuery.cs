using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GGKService.Common.Config.DbHelpers{

	/// <summary>
	/// Динамический построитель запросов
	/// </summary>
	public static class DynamicQuery{

		/// <summary>
		/// Генерирует запрос на UPDATE
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string GetUpdateQuery(string tableName, dynamic item){
			PropertyInfo[] props = item.GetType().GetProperties();
			string[] columns = props.Select(p => p.Name).Where(s => s != "Oid").ToArray();

			var parameters = columns.Select(name => name + "=@" + name);

			return string.Format("UPDATE {0} WITH(ROWLOCK) SET {1} WHERE Oid=@Oid", tableName, string.Join(", ", parameters));
		}

		/// <summary>
		/// Генерирует запрос на INSERT
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="item"></param>
		/// <param name="executeOnly"></param>
		/// <returns></returns>
		public static string GetInsertQuery(string tableName, dynamic item, bool executeOnly = false){
			PropertyInfo[] props = item.GetType().GetProperties();
			string[] columns = props.Select(p => p.Name).Where(s => s != "ID").ToArray();

			if (executeOnly){
				return string.Format("INSERT INTO {0} ({1}) VALUES (@{2})",
					tableName, string.Join(",", columns), string.Join(",@", columns));
			}
			else{
				return string.Format("INSERT INTO {0} ({1}) VALUES (@{2}) select cast(scope_identity() as bigint)",
					tableName, string.Join(", ", columns), string.Join(", @", columns));
			}
		}


	}
}