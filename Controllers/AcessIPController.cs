using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TracerIP.API.Business;
using TracerIP.API.Model;

namespace TracerIP.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]
	public class AcessIPController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<AcessIPController> _log;
		private readonly IConfiguration _config;

		public AcessIPController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<AcessIPController>();
			_config = config;
		}



		[HttpGet]

		public IActionResult Get()
		{
			AcessIPBO acessIPBO;
			List<AcessIP> acessIPs;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				acessIPBO = new AcessIPBO(_loggerFactory, _config);
				acessIPs = acessIPBO.Get();

				response = Ok(acessIPs);

				_log.LogInformation($"Finishing Get() with '{acessIPs.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}


		[HttpGet]
		[Route("GetByGroup")]
		public IActionResult GetByGroup()
		{
			AcessIPBO acessIPBO;
			List<AcessIPModelView> acessIPs;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				acessIPBO = new AcessIPBO(_loggerFactory, _config);
				acessIPs = acessIPBO.GetByGroup();

				response = Ok(acessIPs);

				_log.LogInformation($"Finishing Get() with '{acessIPs.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Post([FromBody] AcessIP acessIP)
		{
			AcessIPBO acessIPBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(acessIP, Formatting.None)}')");

				acessIPBO = new AcessIPBO(_loggerFactory, _config);

				acessIP = acessIPBO.Insert(acessIP);

				response = Ok(acessIP);

				_log.LogInformation($"Finishing Post");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

	}
}
