using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ApyDeal.Models;

namespace ApyDeal.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Deal> Get()
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            DataTable table = databaseProvider.GetAllDeals();
            
            List<Deal> deals = GetDealsFromDataTable(table);

            return deals;
        }
        public IEnumerable<Deal> Get(DateTime date)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            DataTable table = databaseProvider.GetDealsByDate(date);

            List<Deal> deals = GetDealsFromDataTable(table);

            return deals;
        }
        // GET api/values/5
        public IEnumerable<Deal> Get(int id)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            DataTable table = databaseProvider.GetDeal(id);

            List<Deal> deals = GetDealsFromDataTable(table);

            return deals;
        }

        private static List<Deal> GetDealsFromDataTable(DataTable table)
        {
            List<Deal> deals = new List<Deal>();

            foreach (DataRow row in table.Rows)
            {
                Deal deal = new Deal();
                deal.Id = (int)row["id"];
                deal.Urgency = row["urgency"].ToString();
                deal.Status = row["status"].ToString();
                deal.Description = row["dealDescription"].ToString();
                deals.Add(deal);
            }

            return deals;
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post(Deal deal)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();
            
            int createdDealId = databaseProvider.CreateDeal(deal);

            deal.Id = createdDealId;
            
            return Ok(deal);
        }

        // PUT api/values/5
        [HttpPut]
        public IHttpActionResult Put(Deal deal)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            databaseProvider.UpdateDeal(deal);

            return Ok(deal);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            databaseProvider.DeleteDeal(id);
        }
        [HttpOptions]
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,POST,PUT,DELETE,OPTIONS");
            return Ok();
        }
    }
}
