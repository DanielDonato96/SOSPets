using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using SOSPets.DBAcess;
using SOSPets.Models;

namespace SOSPets.Controllers
{
    public class AnunciosController : Controller
    {
        #region Action Result

        public ActionResult AnunciosAnimaisIndex(int pagina = 1, int estadoID = 0, int cidadeID = 0)
        {
            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                if (estadoID == 0) ViewBag.Estado = null;
                else ViewBag.Estado = db.estado.Where(e => e.id == estadoID).FirstOrDefault();

                if (cidadeID == 0) ViewBag.Cidade = null;
                else ViewBag.Cidade = db.cidade.Where(c => c.id == cidadeID).FirstOrDefault();

                ViewBag.FotoAnimalPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Animais";
            }
            return View();
        }

        //[OutputCache(Duration = 1800, VaryByParam = "slug")]
        public ActionResult AnuncioAnimal(string slug)
        {
            AnuncioAnimal anuncioAnimal = new AnuncioAnimal();
            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                var animal = db.Animais.Where(a => !a.excluido && a.FriendlyUrl == slug).FirstOrDefault();
                if (animal == null)
                    return Redirect("/");

                anuncioAnimal.animal = db.proc_001_GetAnimalDetail(animal.AnimalID).FirstOrDefault();
                ViewBag.FotoAnimalPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Animais";
            }
            return View(anuncioAnimal);
        }

        #endregion
        //public ActionResult Index()
        //{
        //    using(SOSPETSEntities db = new SOSPETSEntities())
        //    {
        //        ViewBag.Propagandas = db.Propaganda.Where(p => p.Excluido != true && p.Ativo != false).ToList();
        //    }
        //    ViewBag.PropagandaPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Propagandas";
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        //public ActionResult AnimaisCategorias()
        //{
        //    using (SOSPETSEntities db = new SOSPETSEntities())
        //    {
        //        ViewBag.AnimaisCategoria = db.AnimaisCategorias.ToList();
        //    }
        //    return View();
        //}

        //public ActionResult AnimaisLista(int pagina = 1)
        //{
        //    List<AnimalInfo> animais = new List<AnimalInfo>();
        //    int totalRecords;

        //    if (pagina == 0) pagina = 1;
        //    using (SOSPETSEntities db = new SOSPETSEntities())
        //    {
        //        totalRecords = db.Animais.ToList().Count;

        //        int skip = pagina > 1 ? (pagina - 1) * 10 : 0;

        //        var query = (from A in db.Animais
        //                            join AC in db.AnimaisCategorias on A.AnimalCategoriaID equals AC.AnimalCategoriaID
        //                            select new
        //                            {
        //                                A.AnimalID,
        //                                A.Nome,
        //                                A.DtDesaparecimento,
        //                                A.Whatsapp,
        //                                A.FotoUrl,
        //                                AC.AnimalCategoriaID,
        //                                Especie = AC.Nome
        //                            })
        //                             .OrderByDescending(a => a.DtDesaparecimento)
        //                             .Skip(skip)
        //                             .Take(10)
        //                             .ToList();

        //        foreach (var item in query)
        //        {
        //            AnimalInfo animal = new AnimalInfo();
        //            animal.AnimalID = item.AnimalID;
        //            animal.Nome = item.Nome;
        //            animal.Whatsapp = item.Whatsapp;
        //            animal.FotoUrl = item.FotoUrl;
        //            animal.DataDesaparecimento = Convert.ToDateTime(item.DtDesaparecimento);
        //            animal.AnimalCategoriaID = item.AnimalCategoriaID;
        //            animal.Especie = item.Especie;
        //            animais.Add(animal);
        //        }

        //    }

        //    List<int> backwardPages = new List<int>();

        //    for(int i = 1; i < pagina; i++)
        //    {
        //        backwardPages.Add(i);
        //    }

        //    ViewBag.BackwardPages = backwardPages;

        //    List<int> forwardPages = new List<int>();

        //    for (int i = pagina + 1; (i * 10) < (totalRecords + 10); i++)
        //    {
        //        forwardPages.Add(i);
        //    }

        //    ViewBag.CurrentPage = pagina;

        //    ViewBag.ForwardPages = forwardPages;

        //    return View(animais);
        //}

        //public ActionResult AnimalDetalhe(int animalID = 0)
        //{
        //    AnimalInfo animal = new AnimalInfo();
        //    bool newRecord = false;
        //    bool naoEncontrado = false;
        //    using (SOSPETSEntities db = new SOSPETSEntities())
        //    {

        //        if (animalID > 0)
        //        {
        //            var query = (from A in db.Animais
        //                         join AC in db.AnimaisCategorias on A.AnimalCategoriaID equals AC.AnimalCategoriaID
        //                         select new
        //                         {
        //                             A.AnimalID,
        //                             A.Nome,
        //                             A.DtDesaparecimento,
        //                             A.Whatsapp,
        //                             A.FotoUrl,
        //                             AC.AnimalCategoriaID,
        //                             Especie = AC.Nome
        //                         })
        //                                 .Where(a => a.AnimalID == animalID)
        //                                 .FirstOrDefault();

        //            if (query != null)
        //            {
        //                animal.AnimalID = query.AnimalID;
        //                animal.Nome = query.Nome;
        //                animal.Whatsapp = query.Whatsapp;
        //                animal.FotoUrl = query.FotoUrl;
        //                animal.DataDesaparecimento = Convert.ToDateTime(query.DtDesaparecimento);
        //                animal.AnimalCategoriaID = query.AnimalCategoriaID;
        //                animal.Especie = query.Especie;
        //            }
        //            else naoEncontrado = true;
        //        }
        //        else newRecord = true;

        //        ViewBag.NovoRegistro = newRecord;
        //        ViewBag.NaoEncontrado = naoEncontrado;

        //        ViewBag.Especies = db.AnimaisCategorias.ToList();

        //        ViewBag.FotoAnimalPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Animais";

        //    }
        //    return View(animal);
        //}

        //#endregion

        //#region JsonResult

        //public JsonResult SaveAnimal(FormCollection values)
        //{
        //    try
        //    {
        //        string sAnimalID = values["animalID"];
        //        string nome_animal = values["nome_animal"];
        //        string animalCategoriaID = values["especie"];
        //        string whatsapp = values["whatsapp"];

        //        if (string.IsNullOrEmpty(nome_animal)) throw new Exception("Forneça um nome para o animal");
        //        if (string.IsNullOrEmpty(nome_animal)) throw new Exception("Forneça whatsapp para contato");

        //        string newFotoUrl = WebConfigurationManager.AppSettings["LocalHostPath"] + "Content/Images/Animais/";
        //        bool newRecord = false;
        //        string dataDesaparecimento = string.Empty;
        //        int animalID;

        //        using (SOSPETSEntities db = new SOSPETSEntities())
        //        {
        //            var animal = new Animai();
        //            if (!string.IsNullOrEmpty(sAnimalID))
        //            {
        //                animalID = Convert.ToInt32(sAnimalID);
        //                animal = db.Animais.Where(a => a.AnimalID == animalID).FirstOrDefault();
        //            }
        //            else
        //            {
        //                dataDesaparecimento = Regex.Replace(string.Format("{0}:00", values["new_data_desaparecimento"]), "T", " ");
        //                animal.DtDesaparecimento = Convert.ToDateTime(dataDesaparecimento);
        //                newRecord = true;
        //            }

        //            animal.Nome = nome_animal;                   
        //            animal.Whatsapp = whatsapp;
        //            animal.AnimalCategoriaID = Convert.ToInt32(animalCategoriaID);

        //            #region Salvar Imagem do Animal
        //            animal.FotoUrl = UploadFotoAnimal(HttpContext.Request.Files[0]);
        //            newFotoUrl += animal.FotoUrl;
        //            #endregion

        //            if (newRecord) db.Animais.Add(animal);
        //            db.SaveChanges();
        //            animalID = animal.AnimalID;
        //        }

        //        return Json(new { success = true, message = "Animal salvo com sucesso", FotoUrl = newFotoUrl, newRecord, dataDesaparecimento, animalID });
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message});
        //    }


        //}

        //public JsonResult SaveAnimaisCategorias(int id, string nome)
        //{
        //    bool success = true;
        //    bool newRecord = (id == 0);
        //    try
        //    {
        //        using (SOSPETSEntities db = new SOSPETSEntities())
        //        {
        //            var categoria = new AnimaisCategoria();
        //            if (!newRecord)
        //            {
        //                categoria = db.AnimaisCategorias
        //                     .Where(l => l.AnimalCategoriaID == id)
        //                      .FirstOrDefault();
        //            }
        //            else
        //            {
        //                db.AnimaisCategorias.Add(categoria);
        //            }
        //            categoria.Nome = nome;
        //            db.SaveChanges();
        //            id = categoria.AnimalCategoriaID;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //    }

        //    return Json(new { success, id, nome });
        //}

        //public JsonResult DeleteAnimais(int id)
        //{
        //    bool success = true;
        //    try
        //    {
        //        using (SOSPETSEntities db = new SOSPETSEntities())
        //        {
        //            var animal = db.Animais
        //                .Where(l => l.AnimalID == id)
        //                .FirstOrDefault();

        //            DeletarAnimalFoto(animal.FotoUrl);

        //            if (animal != null)
        //            {
        //                db.Animais.Remove(animal);
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //    }

        //    return Json(new { success });
        //}

        //public JsonResult DeleteAnimaisCategorias(int id)
        //{
        //    bool success = true;
        //    try
        //    {
        //        using (SOSPETSEntities db = new SOSPETSEntities())
        //        {
        //            var categoria = db.AnimaisCategorias
        //                .Where(l => l.AnimalCategoriaID == id)
        //                .FirstOrDefault();

        //            if (categoria != null)
        //            {
        //                db.AnimaisCategorias.Remove(categoria);
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //    }

        //    return Json(new { success });
        //}

        //#endregion

        //#region string
        //public string UploadFotoAnimal(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        var formatosAceitos = new[] { ".png", ".jpg", ".jpeg" };

        //        var nomeArquivo = Path.GetFileName(file.FileName);
        //        var extensao = Path.GetExtension(file.FileName);

        //        if (!formatosAceitos.Contains(extensao))
        //        {
        //            throw new Exception("Utilize apenas imagens no formato .png, .jpg ou .jpeg");
        //        }

        //        //Gerando um nome aleatório para o arquivo
        //        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //        var stringChars = new char[8];
        //        var random = new Random();

        //        for (int i = 0; i < stringChars.Length; i++)
        //        {
        //            stringChars[i] = chars[random.Next(chars.Length)];
        //        }

        //        string nomeArquivoAlfanumerico = new String(stringChars) + extensao;

        //        string nomeArquivoCompleto = Path.Combine(Server.MapPath("~/Content/Images/Animais"), nomeArquivoAlfanumerico);
        //        file.SaveAs(nomeArquivoCompleto);

        //        return nomeArquivoAlfanumerico;
        //    }
        //    catch (Exception ex)
        //    {
        //        return string.Empty;
        //    }

        //}

        //#endregion

        #region Void



        public void DeletarAnimalFoto(string fotoUrl)
        {
            string fotoAnimalPath = Path.Combine(Server.MapPath("~/Content/Images/Animais"), fotoUrl);

            try
            {
                if (System.IO.File.Exists(fotoAnimalPath)) System.IO.File.Delete(fotoAnimalPath);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}