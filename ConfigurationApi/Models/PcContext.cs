using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConfigurationApi.Models
{
	public class PcContext : DbContext
	{
		public PcContext()
		  : base("name=PcCon")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<PcConfigurationData> PcData { get; set; }
		
	}
}