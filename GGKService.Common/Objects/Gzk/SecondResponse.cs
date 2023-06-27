using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using GGKService.Common.Classes.GZKRequestResponse;
using GGKService.Common.Config;
using GGKService.Common.Extensions;
using GGKService.Common.Helpers;

namespace GGKService.Common.Objects.Gzk
{

	/// <summary>
	/// Запрос в КК
	/// </summary>
	public class SecondResponse : CommonObject
	{

		public static string TableName
		{
			get { return "SecondResponse"; }
		}

		public GIStructureElementType GIStructureElementType { get; set; }

		private int Size { get; set; }


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
	}

}
