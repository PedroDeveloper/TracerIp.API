using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TracerIP.API.Data.Base;
using TracerIP.API.Model;


namespace TracerIP.API.Data.Repository
{
	public class AcessIPRepository
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<AcessIPRepository> _log;
		private readonly IConfiguration _config;

		public AcessIPRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<AcessIPRepository>();
			_config = config;
		}

		#region LoadModel

		private List<AcessIP> Load(DataSet data)
		{
			List<AcessIP> acessIPs;
			AcessIP acessIP;

			try
			{
				acessIPs = new List<AcessIP>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					acessIP = new AcessIP();

					acessIP.ip = row.Field<string>("ip");
					acessIP.dateAcess = row.Field<DateTime>("dateAcess");
					acessIP.latitude = row.Field<string>("latitude");
					acessIP.longitude = row.Field<string>("longitude");
					acessIP.city = row.Field<string>("city");
					acessIP.region = row.Field<string>("region");


					acessIPs.Add(acessIP);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return acessIPs;
		}

		private List<AcessIPModelView> LoadByGroup(DataSet data)
		{
			List<AcessIPModelView> acessIPs;
			AcessIPModelView acessIPModelView;

			try
			{
				acessIPs = new List<AcessIPModelView>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					acessIPModelView = new AcessIPModelView();

					acessIPModelView.ip = row.Field<string>("ip");
					acessIPModelView.qtdAcess = row.Field<int>("qtdAcess");


					acessIPs.Add(acessIPModelView);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return acessIPs;
		}

		#endregion

		#region Change Data

		public AcessIP Insert(AcessIP acessIP)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO AcessWebSite
											(
												 ip
												,dateAcess
												,longitude
												,latitude
												,city
												,region

											)
										
										 VALUES
											(
												 @ip
												,@dateAcess
												,@longitude
												,@latitude
												,@city
												,@region
											)");

				command.Parameters.AddWithValue("ip", acessIP.ip.AsDbValue());
				command.Parameters.AddWithValue("dateAcess", DateTime.Now);
				command.Parameters.AddWithValue("longitude", acessIP.longitude.AsDbValue());
				command.Parameters.AddWithValue("latitude", acessIP.latitude.AsDbValue());
				command.Parameters.AddWithValue("city", acessIP.city.AsDbValue());
				command.Parameters.AddWithValue("region", acessIP.region.AsDbValue());

				dataConnection.ExecuteNonQuery(command);

			}
			catch (Exception ex)
			{
				throw ex;
			} 
			

			return acessIP;
		}


		#endregion

		#region Retrieve Data


		public List<AcessIP> Get()
        {
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<AcessIP> acessIP;
		
			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"select * from AcessWebSite  ");


				dataSet = dataConnection.ExecuteDataSet(command);

				acessIP = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return acessIP;

		}

		public List<AcessIPModelView> GetByGroup()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<AcessIPModelView> acessIP;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"select ip,count (ip) as 'qtdAcess' from AcessWebSite group by ip ");

				dataSet = dataConnection.ExecuteDataSet(command);

				acessIP = LoadByGroup(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return acessIP;
		}

		#endregion
	}
}
