using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Dapper;
using GGKService.Common;
using System.Data.SqlClient;
using GGKService.Common.Config;
using GGKService.Common.Helpers.Logs;
using GGKService.Common.Classes.GZKRequestResponse;

namespace GGKService.Logic.GzkManager
{

	public static class GzkManager
	{


		/// Сохранение данных в бд по первому запросу
		public static long SaveGzkFirstRequestResult(GIRelevanceInfo[] data)
		{
			using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
			{
				sqlConnection.Open();
				var transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
				try
				{
					// 1 метод сохранения в бд
					//long treasuryRequestOid=0;
					//foreach (var d in data)
					//{
					//	treasuryRequestOid = sqlConnection.ExecInsert(FirstResponse.TableName, new
					//	{
					//		DateAct = d.DateAct,
					//		GeometryType = d.GeometryType,
					//		Code = d.Territory.Code,
					//		Name = d.Territory.Name
					//	}, transaction);
					//}

					//if (treasuryRequestOid.Equals(0))
					//{
					//	throw new Exception("Не удалось сохранить FirstResponse. treasuryRequestOid=0");
					//}

					// 2 метод сохранения в бд
					var sql = "";
					var objects = new List<object>();
					foreach (var d in data)
					{
						objects.Add(new
						{
							DateAct = d.DateAct,
							GeometryType = d.GeometryType,
							Code = d.Territory.Code,
							Name = d.Territory.Name
						});
					}


					sql =
						"insert FirstResponse(DateAct, GeometryType, TerritoryCode, TerritoryName" +
						"values (@DateAct, @GeometryType, @BankReference, @Code, @Name";

					Logger.Log.Error("Добавление данных в таблицу FirstResponse");
					LogMessage.Info("Добавление данных в таблицу FirstResponse");

					var insertResult = sqlConnection.Execute(sql, objects.ToArray(), transaction, commandTimeout: 180);


					if (insertResult == 0)
					{
						Logger.Log.Error("Не удалось сохранить FirstResponse. insertResult is false");
						throw new Exception("Не удалось сохранить FirstResponse. insertResult is false");
					}

					transaction.Commit();
					transaction.Dispose();

					Logger.Log.Error("Данные в таблицу FirstResponse добавлены");
					LogMessage.Info("Данные в таблицу FirstResponse добавлены");
					return insertResult;
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Не удалось сохранить FirstResponse", ex);
					transaction.Rollback();
					return 0;
				}
			}
		}


		/// Сохранение данных в бд по первому запросу
		public static long SaveGzkSecondRequestResult(GIStructureInfo[] data)
		{
			using (var sqlConnection = new SqlConnection(ConfigHelper.ConnectionString))
			{
				sqlConnection.Open();
				var transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
				try
				{
					// 1 метод сохранения в бд
					//long treasuryRequestOid=0;
					//foreach (var d in data)
					//{
					//	treasuryRequestOid = sqlConnection.ExecInsert(SecondResponse.TableName, new
					//	{
					//		DateAct = d.DateAct,
					//		GeometryType = d.GeometryType,
					//		Code = d.Territory.Code,
					//		Name = d.Territory.Name
					//	}, transaction);
					//}

					//if (treasuryRequestOid.Equals(0))
					//{
					//	throw new Exception("Не удалось сохранить SecondResponse. treasuryRequestOid=0");
					//}

					// 2 метод сохранения в бд
					var sql = "";
					var objects = new List<object>();
					foreach (var d in data)
					{
						objects.Add(new
						{
							GIStructureElementType = d.Type,
							Size = d.Size
						});
					}


					sql =
						"insert SecondResponse(GIStructureElementType, Size" +
						"values (@GIStructureElementType, @Size";

					Logger.Log.Error("Добавление данных в таблицу SecondResponse");
					LogMessage.Info("Добавление данных в таблицу SecondResponse");

					var insertResult = sqlConnection.Execute(sql, objects.ToArray(), transaction, commandTimeout: 180);


					if (insertResult == 0)
					{
						Logger.Log.Error("Не удалось сохранить SecondResponse. insertResult is false");
						throw new Exception("Не удалось сохранить SecondResponse. insertResult is false");
					}

					transaction.Commit();
					transaction.Dispose();

					Logger.Log.Error("Данные в таблицу SecondResponse добавлены");
					LogMessage.Info("Данные в таблицу SecondResponse добавлены");
					return insertResult;
				}
				catch (Exception ex)
				{
					Logger.Log.Error("Не удалось сохранить FirstResponse", ex);
					transaction.Rollback();
					return 0;
				}
			}
		}
	}
}
