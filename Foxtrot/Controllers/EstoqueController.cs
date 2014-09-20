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
using System.Data.SqlClient;
using System.Configuration;

namespace Foxtrot.Controllers
{
   public class EstoqueReturn{
        public int qtdProdutoDisponivel { get; set; }
        public string nomeProduto { get; set; }
    }

    public class EstoqueController : ApiController
    {
        private FoxtrotEntities db = new FoxtrotEntities();


        // GET api/Estoque
        public List<EstoqueReturn> GetEstoque()
        {
            List<EstoqueReturn> lstEstoque = (from estoque in db.Estoque
                                              select new EstoqueReturn
                                              {nomeProduto = estoque.Produto.nomeProduto,
                                               qtdProdutoDisponivel = estoque.qtdProdutoDisponivel
                                              }).OrderByDescending(x => x.qtdProdutoDisponivel).Take(5).ToList();

            return lstEstoque;
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