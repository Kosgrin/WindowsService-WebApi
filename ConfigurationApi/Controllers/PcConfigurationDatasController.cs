using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConfigurationApi.Models;


namespace ConfigurationApi.Controllers
{
    public class PcConfigurationDatasController : ApiController
    {
        private PcContext db = new PcContext();

        // GET: api/PcConfigurationDatas
        public IQueryable<PcConfigurationData> GetPcData()
        {
            return db.PcData;
        }

        // GET: api/PcConfigurationDatas/5
        [ResponseType(typeof(PcConfigurationData))]
        public async Task<IHttpActionResult> GetPcConfigurationData(int id)
        {
            PcConfigurationData pcConfigurationData = await db.PcData.FindAsync(id);
            if (pcConfigurationData == null)
            {
                return NotFound();
            }

            return Ok(pcConfigurationData);
        }

        // PUT: api/PcConfigurationDatas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPcConfigurationData(int id, PcConfigurationData pcConfigurationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pcConfigurationData.id)
            {
                return BadRequest();
            }

            db.Entry(pcConfigurationData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PcConfigurationDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PcConfigurationDatas
        [ResponseType(typeof(PcConfigurationData))]
        public async Task<IHttpActionResult> PostPcConfigurationData(PcConfigurationData pcConfigurationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PcData.Add(pcConfigurationData);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pcConfigurationData.id }, pcConfigurationData);
        }

        // DELETE: api/PcConfigurationDatas/5
        [ResponseType(typeof(PcConfigurationData))]
        public async Task<IHttpActionResult> DeletePcConfigurationData(int id)
        {
            PcConfigurationData pcConfigurationData = await db.PcData.FindAsync(id);
            if (pcConfigurationData == null)
            {
                return NotFound();
            }

            db.PcData.Remove(pcConfigurationData);
            await db.SaveChangesAsync();

            return Ok(pcConfigurationData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PcConfigurationDataExists(int id)
        {
            return db.PcData.Count(e => e.id == id) > 0;
        }
    }
}