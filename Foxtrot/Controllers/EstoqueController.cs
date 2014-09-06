using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Foxtrot.Models;

namespace Foxtrot.Controllers
{
    public class EstoqueController : ApiController
    {
        private FoxtrotEntities db = new FoxtrotEntities();

        // GET api/Estoque
        public IEnumerable<Estoque> GetEstoque()
        {

            //return db.Estoque;

            var listEstoque =
            (from c in db.Estoque
             join d in db.Produto on c.idProduto equals d.idProduto
             select new { NomeProduto = d.nomeProduto, QuantidadeEstoque = c.qtdProdutoDisponivel }).ToList();//query incompleta

            return new List<Estoque>();//isso ta errado gente
        }

        // GET api/Estoque/5
        [ResponseType(typeof(Estoque))]
        public IHttpActionResult GetEstoque(int id)
        {
            Estoque estoque = db.Estoque.Find(id);
            if (estoque == null)
            {
                return NotFound();
            }

            return Ok(estoque);
        }

        // PUT api/Estoque/5
        public IHttpActionResult PutEstoque(int id, Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estoque.idProduto)
            {
                return BadRequest();
            }

            db.Entry(estoque).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstoqueExists(id))
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

        // POST api/Estoque
        [ResponseType(typeof(Estoque))]
        public IHttpActionResult PostEstoque(Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estoque.Add(estoque);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EstoqueExists(estoque.idProduto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = estoque.idProduto }, estoque);
        }

        // DELETE api/Estoque/5
        [ResponseType(typeof(Estoque))]
        public IHttpActionResult DeleteEstoque(int id)
        {
            Estoque estoque = db.Estoque.Find(id);
            if (estoque == null)
            {
                return NotFound();
            }

            db.Estoque.Remove(estoque);
            db.SaveChanges();

            return Ok(estoque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstoqueExists(int id)
        {
            return db.Estoque.Count(e => e.idProduto == id) > 0;
        }

    }
}