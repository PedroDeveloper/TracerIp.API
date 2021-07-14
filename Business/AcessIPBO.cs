using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using SalvadorCaps.API.Data.Repository;
using SalvadorCaps.API.Model;

namespace SalvadorCaps.API.Business
{
	public class AcessIPBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<AcessIPBO> _log;
		private readonly IConfiguration _config;

		public AcessIPBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<AcessIPBO>();
			_config = config;
		}

		#region Change Data

		public AcessIP Insert(AcessIP acessIP)
		{
			AcessIPRepository acessIPRepository;
		

			try
			{
				acessIPRepository = new AcessIPRepository(_loggerFactory, _config);

				if (!string.IsNullOrEmpty(acessIP.ip))
				{

					acessIP = acessIPRepository.Insert(acessIP);
				}
				else
				{
					throw new Exception("IP vazio");
				}
			}
			catch (Exception ex)
			{
			
				throw ex;
			}

			return acessIP;
		}



		#endregion

		#region Retrieve Repository

		public List<AcessIP> Get()
		{
			AcessIPRepository acessIPRepository;
			List<AcessIP> products;

			try
			{
				acessIPRepository = new AcessIPRepository(_loggerFactory, _config);

				products = acessIPRepository.Get();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return products;
		}

		public List<AcessIPModelView> GetByGroup()
		{
			AcessIPRepository acessIPRepository;
			List<AcessIPModelView> acessIPModelViews;

			try
			{
				acessIPRepository = new AcessIPRepository(_loggerFactory, _config);

				acessIPModelViews = acessIPRepository.GetByGroup();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return acessIPModelViews;
		}

		#endregion
	}
}
