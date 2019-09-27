using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService
{
	public class PcConfigurationData
	{

		public PcConfigurationData()
		{
			Id = 1;
		    PcUser = HardwareInfo.PcUser();
		    PcNAme = HardwareInfo.GetComputerName();
			PcManufacturer = HardwareInfo.GetCPUManufacturer();
			CpuLoad = HardwareInfo.GetCPULoad();
			RamLoad = HardwareInfo.GetRAMLoad();

		}

		public int Id { get; set; }
		public string PcUser { get; set; }
		public string PcNAme { get; set; } 
		public string PcManufacturer { get; set; }
		public string CpuLoad { get; set; } 
		public string RamLoad { get; set; } 
	}
}
