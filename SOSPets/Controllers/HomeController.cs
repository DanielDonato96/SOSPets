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
using SOSPets.Repository;

namespace SOSPets.Controllers
{
    public class HomeController : Controller
    {
        #region Action Result
        public ActionResult Index()
        {
            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                ViewBag.Propagandas = db.Propaganda.Where(p => p.Excluido != true && p.Ativo != false).ToList();
            }
            ViewBag.PropagandaPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Propagandas";
            return View();
        }

        public ActionResult AcessarConta()
        {
            return View();
        }

        public ActionResult CriarConta()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AnimaisCategorias()
        {
            if(Session["UserName"] == null)
                return Redirect("/");

            string login = Session["UserName"].ToString();

            if (login != "daniel")
                return Redirect("/");


            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                ViewBag.AnimaisCategoria = db.AnimaisCategorias.ToList();
            }
            return View();
        }

        public ActionResult AnimaisLista(int pagina = 1)
        {
            List<AnimalInfo> animais = new List<AnimalInfo>();
            int totalRecords;

            if (pagina == 0) pagina = 1;
            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                totalRecords = db.Animais.ToList().Count;

                int skip = pagina > 1 ? (pagina - 1) * 10 : 0;

                var query = (from A in db.Animais
                             join AC in db.AnimaisCategorias on A.AnimalCategoriaID equals AC.AnimalCategoriaID
                             select new
                             {
                                 A.AnimalID,
                                 A.Nome,
                                 A.DtDesaparecimento,
                                 A.Whatsapp,
                                 A.FotoUrl,
                                 AC.AnimalCategoriaID,
                                 Especie = AC.Nome
                             })
                                     .OrderByDescending(a => a.DtDesaparecimento)
                                     .Skip(skip)
                                     .Take(10)
                                     .ToList();

                foreach (var item in query)
                {
                    AnimalInfo animal = new AnimalInfo();
                    animal.AnimalID = item.AnimalID;
                    animal.Nome = item.Nome;
                    animal.Whatsapp = item.Whatsapp;
                    animal.FotoUrl = item.FotoUrl;
                    animal.DataDesaparecimento = Convert.ToDateTime(item.DtDesaparecimento);
                    animal.AnimalCategoriaID = item.AnimalCategoriaID;
                    animal.Especie = item.Especie;
                    animais.Add(animal);
                }

            }

            List<int> backwardPages = new List<int>();

            for (int i = 1; i < pagina; i++)
            {
                backwardPages.Add(i);
            }

            ViewBag.BackwardPages = backwardPages;

            List<int> forwardPages = new List<int>();

            for (int i = pagina + 1; (i * 10) < (totalRecords + 10); i++)
            {
                forwardPages.Add(i);
            }

            ViewBag.CurrentPage = pagina;

            ViewBag.ForwardPages = forwardPages;

            return View(animais);
        }

        public ActionResult AnimalDetalhe(int animalID = 0)
        {
            AnimalInfo animal = new AnimalInfo();
            bool newRecord = false;
            bool naoEncontrado = false;
            using (SOSPETSEntities db = new SOSPETSEntities())
            {

                if (animalID > 0)
                {
                    var query = (from A in db.Animais
                                 join AC in db.AnimaisCategorias
                                 on A.AnimalCategoriaID equals AC.AnimalCategoriaID
                                 join C in db.cidade
                                 on A.CidadeID equals C.id
                                 join E in db.estado
                                 on C.uf equals E.id
                                 select new
                                 {
                                     A.AnimalID,
                                     A.Nome,
                                     A.DtDesaparecimento,
                                     A.Whatsapp,
                                     A.FotoUrl,
                                     A.SituacaoAnimalID,
                                     AC.AnimalCategoriaID,
                                     Especie = AC.Nome,
                                     CidadeID = A.CidadeID,
                                     EstadoId = E.id,
                                     A.Bairro,
                                     A.Descricao
                                 })
                                         .Where(a => a.AnimalID == animalID)
                                         .FirstOrDefault();

                    if (query != null)
                    {
                        animal.AnimalID = query.AnimalID;
                        animal.Nome = query.Nome;
                        animal.Whatsapp = query.Whatsapp;
                        animal.FotoUrl = query.FotoUrl;
                        animal.DataDesaparecimento = Convert.ToDateTime(query.DtDesaparecimento);
                        animal.AnimalCategoriaID = query.AnimalCategoriaID;
                        animal.SituacaoAnimalID = query.SituacaoAnimalID;
                        animal.Especie = query.Especie;
                        animal.EstadoID = query.EstadoId;
                        animal.CidadeID = query.CidadeID;
                        animal.Bairro = query.Bairro;
                        animal.Descricao = query.Descricao;
                    }
                    else naoEncontrado = true;
                }
                else newRecord = true;

                ViewBag.NovoRegistro = newRecord;
                ViewBag.NaoEncontrado = naoEncontrado;

                ViewBag.Especies = db.AnimaisCategorias.ToList();
                ViewBag.Estados = db.estado.Where(e => e.id != 99).ToList();

                ViewBag.SituacaoAnimal = db.SituacaoAnimal.ToList();

                ViewBag.FotoAnimalPath = WebConfigurationManager.AppSettings["LocalHostPath"] + "/Content/Images/Animais";

            }
            return View(animal);
        }

        #endregion

        #region JsonResult

        public JsonResult CreateAccount(FormCollection values)
        {
            try
            {
                string login = values["txtLogin"];
                if (string.IsNullOrEmpty(login))
                    throw new Exception("Login não informado");

                string nome = values["txtNome"];
                if (string.IsNullOrEmpty(nome))
                    throw new Exception("Nome não informado");

                string email = values["txtEmail"];
                if (string.IsNullOrEmpty(email))
                    throw new Exception("Email não informado");

                string whatsapp = values["whatsapp"];

                if (string.IsNullOrEmpty(whatsapp))
                    throw new Exception("Whatsapp não informado");

                if (!whatsapp.All(char.IsDigit))
                    throw new Exception("Whatsapp deve conter apenas números");

                string senha = values["txtSenha"];
                string confirmSenha = values["txtConfirmSenha"];

                if(string.IsNullOrEmpty(senha))
                    throw new Exception("Senha não informada");

                if (senha != confirmSenha)
                    throw new Exception("Senha e confirmação de senha diferentes");

                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    //Valida se já existe algum usuário com o mesmo login/email
                    Usuario usuarioAntigo = db.Usuarios.Where(u => u.Excluido == false && u.Login == login).FirstOrDefault();
                    if (usuarioAntigo != null)
                        throw new Exception(string.Format("Já existe um usuário com o login '{0}' cadastrado.", login));

                    usuarioAntigo = db.Usuarios.Where(u => u.Excluido == false && u.Email == email).FirstOrDefault();
                    if (usuarioAntigo != null)
                        throw new Exception(string.Format("Já existe um usuário com o email '{0}' cadastrado.", email));

                    Usuario usuario = new Usuario();

                    usuario.Nome = nome;
                    usuario.Login = login;
                    usuario.Email = email;
                    usuario.PasswordToken = senha;
                    usuario.Excluido = false;
                    usuario.Whatsapp = whatsapp;

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                  
                }

                return Json(new { success = true, message = "Seu cadasto foi realizado com sucesso. Realize o Login" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }


        }

        public JsonResult Login(FormCollection values)
        {
            try
            {
                string login = values["txtLogin"];
                if (string.IsNullOrEmpty(login))
                    throw new Exception("Login não informado");

                string senha = values["txtSenha"];

                if (string.IsNullOrEmpty(senha))
                    throw new Exception("Senha não informada");

                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    Usuario usuario = db.Usuarios.Where(u => u.Login == login & !u.Excluido).FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("Nenhum usuário com esse login foi encontrado");

                    if (usuario.PasswordToken != senha)
                        throw new Exception("Senha inválida");

                    Session["UserID"] = usuario.UsuarioID.ToString();
                    Session["UserName"] = usuario.Login.ToString();

                }

                return Json(new { success = true, message = "Seu cadasto foi realizado com sucesso. Realize o Login" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }


        }

        public JsonResult SaveAnimal(FormCollection values)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserID"] == null)
                    throw new Exception("É necessário estar logado no sistema para criar um anúncio");

                string sessionLogin = Session["UserName"].ToString();
                string sUsuarioID = Session["UserID"].ToString();

                int usuarioID = Convert.ToInt32(sUsuarioID);

                string sAnimalID = values["animalID"];
                string nome_animal = values["nome_animal"];
                string animalCategoriaID = values["especie"];
                //string whatsapp = values["whatsapp"];

                if (string.IsNullOrEmpty(nome_animal)) throw new Exception("Forneça um nome para o animal");
                //if (string.IsNullOrEmpty(whatsapp)) throw new Exception("Forneça whatsapp para contato");

                string descricao = values["descricao"];

                if (string.IsNullOrEmpty(descricao)) throw new Exception("Forneça uma descrição");

                string sCidadeID = values["cidade"];

                if (string.IsNullOrEmpty(sCidadeID)) throw new Exception("Selecione uma cidade");

                string bairro = values["bairro"];

                if (string.IsNullOrEmpty(bairro)) throw new Exception("Preencha o bairro");

                string sSituacaoAnimalID = values["situacao"];

                if (string.IsNullOrEmpty(sCidadeID)) throw new Exception("Selecione uma situação para o anuncio");

                string newFotoUrl = WebConfigurationManager.AppSettings["LocalHostPath"] + "Content/Images/Animais/";
                bool newRecord = false;
                string dataDesaparecimento = string.Empty;
                int animalID;

                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    var animal = new Animai();
                    if (!string.IsNullOrEmpty(sAnimalID))
                    {
                        animalID = Convert.ToInt32(sAnimalID);
                        animal = db.Animais.Where(a => a.AnimalID == animalID).FirstOrDefault();

                        if (animal.UsuarioID != usuarioID)
                            throw new Exception("Você não pode alterar anúncios de outros usuários");
                    }
                    else
                    {
                        dataDesaparecimento = Regex.Replace(string.Format("{0}:00", values["new_data_desaparecimento"]), "T", " ");
                        animal.DtDesaparecimento = Convert.ToDateTime(dataDesaparecimento);
                        animal.UsuarioID = usuarioID;
                        newRecord = true;

                        var dataAtual = DateTime.Now;

                        int totalAnunciosNoMes = db.Animais
                            .Where(a => a.UsuarioID == usuarioID &&
                            a.DtDesaparecimento.Value.Month == dataAtual.Month &&
                            a.DtDesaparecimento.Value.Year == dataAtual.Year)
                            .ToList()
                            .Count;

                        if (totalAnunciosNoMes > 3)
                            throw new Exception("Você excedeu o limite de anúncios no mês");
                    }

                    animal.Nome = nome_animal;
                    animal.Whatsapp = string.Empty;
                    animal.Descricao = descricao;
                    animal.AnimalCategoriaID = Convert.ToInt32(animalCategoriaID);

                    animal.CidadeID = Convert.ToInt32(sCidadeID);
                    animal.Bairro = bairro;

                    if (!newRecord)
                    {
                        if(animal.SituacaoAnimalID == (int)eSituacaoAnimal.Encontrado
                            && Convert.ToInt32(sSituacaoAnimalID) != (int)eSituacaoAnimal.Encontrado)
                            throw new Exception("Não é possível editar dados de um animal já encontrado");
                    }

                    animal.SituacaoAnimalID = Convert.ToInt32(sSituacaoAnimalID);

                    #region Salvar Imagem do Animal
                    string animalFotoUrl = UploadFotoAnimal(HttpContext.Request.Files[0]);
                    if(!string.IsNullOrEmpty(animalFotoUrl)) 
                        animal.FotoUrl = animalFotoUrl;
                    

                    if (newRecord && string.IsNullOrEmpty(animal.FotoUrl))
                        throw new Exception("É necessário inserir uma foto do animal no anúncio");

                    newFotoUrl += animal.FotoUrl;

                    #endregion

                    if (newRecord)
                    {
                        if (animal.SituacaoAnimalID != (int)eSituacaoAnimal.AnuncioAtivo)
                            throw new Exception("O anúncio deve ser criado com o Status 'Ativo'");
                        db.Animais.Add(animal);
                    }
                    db.SaveChanges();

                    
                    animalID = animal.AnimalID;

                    if (newRecord)
                    {
                        animal.FriendlyUrl = string.Format("{0}-{1}", animalID, nome_animal.ToLower());
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true, message = "Animal salvo com sucesso", FotoUrl = newFotoUrl, newRecord, dataDesaparecimento, animalID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }


        }

        public JsonResult SaveAnimaisCategorias(int id, string nome)
        {
            bool success = true;
            bool newRecord = (id == 0);
            try
            {
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    var categoria = new AnimaisCategoria();
                    if (!newRecord)
                    {
                        categoria = db.AnimaisCategorias
                             .Where(l => l.AnimalCategoriaID == id)
                              .FirstOrDefault();
                    }
                    else
                    {
                        db.AnimaisCategorias.Add(categoria);
                    }
                    categoria.Nome = nome;
                    db.SaveChanges();
                    id = categoria.AnimalCategoriaID;

                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Json(new { success, id, nome });
        }

        public JsonResult DeleteAnimais(int id)
        {
            bool success = true;
            try
            {
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    var animal = db.Animais
                        .Where(l => l.AnimalID == id)
                        .FirstOrDefault();

                    DeletarAnimalFoto(animal.FotoUrl);

                    if (animal != null)
                    {
                        animal.excluido = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Json(new { success });
        }

        public JsonResult DeleteAnimaisCategorias(int id)
        {
            bool success = true;
            try
            {
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    var categoria = db.AnimaisCategorias
                        .Where(l => l.AnimalCategoriaID == id)
                        .FirstOrDefault();

                    if (categoria != null)
                    {
                        db.AnimaisCategorias.Remove(categoria);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Json(new { success });
        }

        public JsonResult GetAnimaisEstados()
        {
            try
            {
                List<vwAnimalEstados> animaisEstados = new List<vwAnimalEstados>();
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    animaisEstados = db.proc_003_ConsultaAnimaisPorEstado().ToList();
                }
                return Json(new { success = true, estados = animaisEstados });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetAnimaisCidades(int estadoID)
        {
            try
            {
                List<vwAnimalCidades> animaisCidades = new List<vwAnimalCidades>();
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    animaisCidades = db.proc_004_ConsultaAnimaisCidade(estadoID).ToList();
                }
                return Json(new { success = true, cidades = animaisCidades });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetCidades(int estadoID)
        {
            List<vwCidades> cidades = new List<vwCidades>();
            string message = string.Empty;
            bool success = true;
            try
            {
                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    cidades = db.proc_005_GetCidades(estadoID).ToList();
                }

            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }
            return Json(new { success = success, cidades, message = message });
        }

        #endregion

        #region string
        public string UploadFotoAnimal(HttpPostedFileBase file)
        {
            try
            {
                var formatosAceitos = new[] { ".png", ".jpg", ".jpeg" };

                var nomeArquivo = Path.GetFileName(file.FileName);
                var extensao = Path.GetExtension(file.FileName);

                if (!formatosAceitos.Contains(extensao))
                {
                    throw new Exception("Utilize apenas imagens no formato .png, .jpg ou .jpeg");
                }

                //Gerando um nome aleatório para o arquivo
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                string nomeArquivoAlfanumerico = new String(stringChars) + extensao;

                string nomeArquivoCompleto = Path.Combine(Server.MapPath("~/Content/Images/Animais"), nomeArquivoAlfanumerico);
                file.SaveAs(nomeArquivoCompleto);

                return nomeArquivoAlfanumerico;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        #endregion

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