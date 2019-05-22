using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chamado.Models;
using Chamado.DAL;
using System.Web.Routing;
using System.Drawing;
using System.IO;
using Util;
using BLL;

namespace Chamado.Controllers
{    
    
    public class ChamadoController : _BaseController
    {
                
        private ChamadoContexto db = new ChamadoContexto();
        //private ChamadoController Chamado = new ChamadoController();
        //Chamado.Models.Chamado chamado = new Chamado.Models.Chamado;
        // GET: /Chamado/
        public ActionResult Index()
        {
            //var obj = new List<ChamadoModel>
            //{                
            //        new ChamadoModel
            //        {

            //            IdChamado = 1,
            //            Descricao = "DescricaoTeste",
            //            Motivo = "Erro",
            //            Sistema = "Sicob",
            //            Usuario = "Dragonite"

            //        }
            // };
            return View(/*obj*/);

        }


   

        public ActionResult MeusChamados(string filtro)
        {
            //ar usuario = "externo";
            //Convert.ToString(infoBusca);

            //buscar chamados no banco
            //try
            //{
            //    if (!String.IsNullOrEmpty(filtro))
            //    {
            //        List<Chamado.Models.Chamado> chamados = db.Chamados.Where(x => x.NumChamado.Contains(filtro)).ToList();
            //        if (chamados != null)
            //        {
            //            return PartialView(chamados);
            //        }
            //    }
            //    else
            //    {
            //        List<Chamado.Models.Chamado> chamados = db.Chamados.ToList();
            //        if (chamados != null)
            //        {
            //            return PartialView(chamados);
            //        }
            //    }          
            //}
            //catch (Exception)
            //{                
            //    throw;
            //}            


            var obj = new List<Chamado.Models.Chamado>
            {               
               
                new Chamado.Models.Chamado
                    {

                        IdChamado = 1,
                        Descricao = "DescricaoTeste",
                        Motivo = "Erro",
                        Sistema = "Sicob",
                        Usuario = "Rogerio Ceni"

                    },
                    new Chamado.Models.Chamado
                    {

                        IdChamado = 1,
                        Descricao = "DescricaoTeste",
                        Motivo = "Duvida",
                        Sistema = "CapiGiro",
                        Usuario = "Kaká"

                    },
                    new Chamado.Models.Chamado
                    {

                        IdChamado = 1,
                        Descricao = "DescricaoTeste",
                        Motivo = "Não consigo acesso",
                        Sistema = "RAMAIS",
                        Usuario = "Telê Santana"

                    },
                    new Chamado.Models.Chamado
                    {

                        IdChamado = 1,
                        Descricao = "DescricaoTeste",
                        Motivo = "Teste",
                        Sistema = "Teste",
                        Usuario = "Profeta"

                    }

             };
            return PartialView(obj);

        }

        // GET: /Chamado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamado.Models.Chamado chamadomodel = await db.Chamados.FindAsync(id);
            if (chamadomodel == null)
            {
                return HttpNotFound();
            }
            return View(chamadomodel);
        }

        // GET: /Chamado/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SucessoCadastro(Chamado.Models.Chamado chamadoModel)
        {
            return View(chamadoModel);
        }

        // POST: /Chamado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Bind(Include = "Motivo,Descricao,Sistema,SituacaoProj")]
        public ActionResult GravaChamado(Chamado.Models.Chamado chamado)
        {

            
            var files = Request.Files;

            //var imageTypes = new string[]{
            //        "image/gif",
            //        "image/jpeg",
            //        "image/pjpeg",
            //        "image/png"
            //    };
                      

            //var numero = rndNumero.Next().ToString();
            var date = DateTime.Now.Ticks;

            DateTime myDate = new DateTime(date);
            String test = myDate.ToString("MMMM dd, yyyy");
                  


            //### - 
            //var imagemNome = String.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);
            //var extensao = System.IO.Path.GetExtension(chamadomodel.Anexo.Extensao).ToLower();
            //using (var img = System.Drawing.Image.FromStream()
            //{
            //    chamadomodel.Anexo.ConteudoAnexo = String.Format("/ProdutoImagens/{0}{1}", imagemNome, extensao);
            //    // Salva imagem 
            //    SalvarNaPasta(img, chamadomodel.Anexo.ConteudoAnexo);
            //}

            //chamadomodel.Usuario = "Externo";
            //chamadomodel.NumChamado = "brati" + date;

            //if (ModelState.IsValid)
            //{
            //    db.Chamados.Add(chamadomodel);
            //    db.SaveChanges();
            //    return RedirectToAction("SucessoCadastro", chamadomodel);
            //}         


            return Json(chamado);

            //return PartialView("SucessoCadastro", chamadomodel);
            //return RedirectToRoute("SucessoCadastro", new RouteValueDictionary(chamadomodel));
            //return RedirectToAction("SucessoCadastro", "Chamado", new { chamadoModel = obj });
        }

        // GET: /Chamado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamado.Models.Chamado chamadomodel = await db.Chamados.FindAsync(id);
            if (chamadomodel == null)
            {
                return HttpNotFound();
            }
            return View(chamadomodel);
        }

        // GET: /Chamado/Edit/5
        public ActionResult Editar(Chamado.Models.Chamado chamadomodel)
        {
            return View(chamadomodel);
        }

        // POST: /Chamado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdChamado,Descricao")]Chamado.Models.Chamado chamadomodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamadomodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chamadomodel);
        }

        // GET: /Chamado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamado.Models.Chamado chamadomodel = await db.Chamados.FindAsync(id);
            if (chamadomodel == null)
            {
                return HttpNotFound();
            }
            return View(chamadomodel);
        }

        // POST: /Chamado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Chamado.Models.Chamado chamadomodel = await db.Chamados.FindAsync(id);
            db.Chamados.Remove(chamadomodel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult _Modal()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SalvarNaPasta(Image img, string caminho)
        {
            using (System.Drawing.Image novaImagem = new Bitmap(img))
            {
                novaImagem.Save(Server.MapPath(caminho), img.RawFormat);
            }
        }

        public FileResult ConsultarAnexo(int pIdAnexo)
        {
            if (!ValidaUsuario())
                return null;

            Mod_Anexo vTb_Anexo = new Mod_Anexo();

            Log vLog = new Log("GET /Home/ConsultarAnexo");
            vLog.DsParametros = "IdAnexo='" + pIdAnexo + "'; ";

            try
            {
                vTb_Anexo = BLL_Anexo.Consulta(pIdAnexo);

            }
            catch (Exception ex)
            {
                vLog.FlSucesso = false;
                vLog.DsResultado = "Erro. Mensagem: " + ex.Message + ". Detalhe: " + ex.StackTrace;
                TempData["erro"] = vLog.DsResultado;
            }
            finally
            {
                vLog.InsereLog();
            }

            ViewBag.erro = TempData["erro"];

            return File(vTb_Anexo.Arquivo, vTb_Anexo.TipoArquivo, vTb_Anexo.NmArquivo);
        }

        //
        // POST: /Home/SalvarAnexo
        public void SalvarAnexo()
        {
            if (Request.Files.Count > 0)
            {
                IList<Mod_Anexo> vListAnexo = new List<Mod_Anexo>();

                foreach (string fileKey in Request.Files)
                {
                    System.Web.HttpPostedFileBase file = Request.Files[fileKey];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        Mod_Anexo pAnexo = new Mod_Anexo();
                        pAnexo.NmArquivo = file.FileName.Split('\\').Last();
                        pAnexo.TipoArquivo = file.ContentType;
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            pAnexo.Arquivo = binaryReader.ReadBytes(file.ContentLength);
                        }

                        vListAnexo.Add(pAnexo);
                    }
                }
            }
        }
       
    }
}
