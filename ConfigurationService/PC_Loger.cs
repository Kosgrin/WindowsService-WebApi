using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace ConfigurationService
{
	public partial class PC_Loger : ServiceBase
	{
		private Timer timer = new Timer();
		private new readonly EventLog EventLog = new EventLog();
		private int eventId = 1;
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
		public PC_Loger(string[] args)
		{
			InitializeComponent();

			string eventSourceName = "MySource";
			string logName = "MyNewLog";

			if (args.Length > 0)
			{
				eventSourceName = args[0];
			}

			if (args.Length > 1)
			{
				logName = args[1];
			}

			EventLog = new EventLog();

			if (!EventLog.SourceExists(eventSourceName))
			{
				EventLog.CreateEventSource(eventSourceName, logName);
			}

			EventLog.Source = eventSourceName;
			EventLog.Log = logName;
		}

		protected  override void OnStart(string[] args)
		{
			// Update the service state to Start Pending.
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);


			timer.Interval = 40000; // 20 seconds
			 timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
			 timer.Start();

			EventLog.WriteEntry("Service is On Start");
			EventLog.WriteEntry("collecting pc info");

			serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

		}

		public  void OnTimer(object sender, ElapsedEventArgs args)
		{
			 SendToApi();
				// TODO: Insert monitoring activities here.
				EventLog.WriteEntry("Data updated", EventLogEntryType.Information, eventId++);
		}

		protected override void OnStop()
		{
			EventLog.WriteEntry("In OnStop.");
		}


		

		public enum ServiceState
		{
			SERVICE_STOPPED = 0x00000001,
			SERVICE_START_PENDING = 0x00000002,
			SERVICE_STOP_PENDING = 0x00000003,
			SERVICE_RUNNING = 0x00000004,
			SERVICE_CONTINUE_PENDING = 0x00000005,
			SERVICE_PAUSE_PENDING = 0x00000006,
			SERVICE_PAUSED = 0x00000007,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ServiceStatus
		{
			public int dwServiceType;
			public ServiceState dwCurrentState;
			public int dwControlsAccepted;
			public int dwWin32ExitCode;
			public int dwServiceSpecificExitCode;
			public int dwCheckPoint;
			public int dwWaitHint;
		};


		public void SendToApi()
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.Proxy = System.Net.WebRequest.DefaultWebProxy;
			handler.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
			using (var client = new HttpClient(handler))
			{
				PcConfigurationData data = new PcConfigurationData();


				var json = JsonConvert.SerializeObject(data, Formatting.Indented);

				StringContent content = new StringContent(json, Encoding.UTF8, "text/json");
				client.BaseAddress = new Uri("https://localhost:44367/");
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => certificate.Issuer == "CN=localhost";
                //HTTP GET
                var response = client.PostAsync("api/values", content);
				//response.Wait();

				var result = response.Result;
				if (result.IsSuccessStatusCode)
				{
					EventLog.WriteEntry("Data sent succesfully", EventLogEntryType.Information, eventId++);

				}


			}
		}
	}
}



