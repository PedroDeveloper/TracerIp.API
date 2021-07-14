using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TracerIP.API.Model
{
	public class AcessIP
	{
		public string ip { get; set; }
		public DateTime? dateAcess { get; set; }
		public string latitude { get; set; }
		public string longitude { get; set; }
		public string city { get; set; }
		public string region { get; set; }



	}
}
