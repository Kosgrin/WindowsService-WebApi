using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConfigurationApi.Models
{
	public class PcConfigurationData
	{
		public int id { get; set; }
		public string PcUser { get; set; }
		public string PcNAme { get; set; }
		public string PcManufacturer { get; set; }
		public string CpuLoad { get; set; }
		public string RamLoad { get; set; }


		
	}
}