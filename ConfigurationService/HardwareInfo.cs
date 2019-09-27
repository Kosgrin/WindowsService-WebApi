using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService
{
	public static class HardwareInfo
	{



		public static string GetProcessorId()
		{
			ManagementClass mc = new ManagementClass("win32_processor");
			ManagementObjectCollection moc = mc.GetInstances();
			string Id = String.Empty;
			foreach (ManagementObject mo in moc)
			{

				Id = mo.Properties["processorID"].Value.ToString();
				break;
			}
			return Id;
		}

		public static string GetCPUManufacturer()
		{
			string cpuMan = string.Empty;
			//create an instance of the Managemnet class with the
			//Win32_Processor class
			ManagementClass mgmt = new ManagementClass("Win32_Processor");
			//create a ManagementObjectCollection to loop through
			ManagementObjectCollection objCol = mgmt.GetInstances();
			//start our loop for all processors found
			foreach (ManagementObject obj in objCol)
			{
				if (cpuMan == String.Empty)
				{
					// only return manufacturer from first CPU
					cpuMan = obj.Properties["Manufacturer"].Value.ToString();
				}
			}
			return cpuMan;
		}

		public static string GetComputerName()
		{
			ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
			ManagementObjectCollection moc = mc.GetInstances();
			string info = String.Empty;
			foreach (ManagementObject mo in moc)
			{
				info = (string)mo["Name"];
				//mo.Properties["Name"].Value.ToString();
				//break;
			}
			return info;
		}


		public static string GetCPULoad()
		{
			PerformanceCounter cpuCounter;
			cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

			return cpuCounter.NextValue() + "%";
		}

		public static string GetRAMLoad()
		{
			PerformanceCounter ramCounter;
			ramCounter = new PerformanceCounter("Memory", "Available MBytes");

			return ramCounter.NextValue() + "MB";
		}

		public static string PcUser()
		{
			return Environment.UserName;
		}



		
	}
}
