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
    public class CustomerController : Controller
    {
        #region Action Result
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;

            return Redirect("/");

        }

        public ActionResult AlterarSenha()
        {
            if (Session["UserID"] == null || Session["UserName"] == null)
                return Redirect("/");

            return View();
        }

        public ActionResult EditarConta()
        {
            if (Session["UserID"] == null || Session["UserName"] == null)
                return Redirect("/");

            string login = Session["UserName"].ToString();

            using (SOSPETSEntities db = new SOSPETSEntities())
            {
                Usuario usuario = db.Usuarios.Where(u => u.Login == login && !u.Excluido).FirstOrDefault();

                if (usuario == null)
                    return Redirect("/");

                ViewBag.Usuario = usuario;
            }

                return View();
        }

        #endregion

        #region JsonResult
        public JsonResult ChangePassword(FormCollection values)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserID"] == null)
                    throw new Exception("É necessário estar logado no sistema para alterar sua senha");

                string login = Session["UserName"].ToString();

                string sUsuarioID = Session["UserID"].ToString();
              
                string senha = values["txtSenha"];

                if (string.IsNullOrEmpty(senha))
                    throw new Exception("Senha atual não informada");

                string novaSenha = values["txtNovaSenha"];

                string confirmacaoNovaSenha = values["txtConfirmNovaSenha"];

                if (string.IsNullOrEmpty(novaSenha))
                    throw new Exception("Nova Senha não informada");

                if (novaSenha != confirmacaoNovaSenha)
                    throw new Exception("Nova Senha  e Confirmação de Senha devem ser iguais");

                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    Usuario usuario = db.Usuarios.Where(u => u.Login == login & !u.Excluido).FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("Usuário não encontrado no sistema");

                    if (usuario.PasswordToken != senha)
                        throw new Exception("A Senha atual é inválida");

                    usuario.PasswordToken = novaSenha;

                    db.SaveChanges();

                }

                return Json(new { success = true, message = "Sua Senha foi atualizada com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }


        }

        public JsonResult EditAccount(FormCollection values)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserID"] == null)
                    throw new Exception("É necessário estar logado no sistema para alterar seus dados");

                string sessionLogin = Session["UserName"].ToString();

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

                using (SOSPETSEntities db = new SOSPETSEntities())
                {
                    Usuario usuario = db.Usuarios.Where(u => u.Login == sessionLogin & !u.Excluido).FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("Usuário não encontrado no sistema");

                    //Valida se já existe algum usuário com o mesmo login/email
                    Usuario usuarioAntigo = db.Usuarios.Where(u => u.Excluido == false && u.Login == login && u.UsuarioID != usuario.UsuarioID).FirstOrDefault();
                    if (usuarioAntigo != null)
                        throw new Exception(string.Format("Já existe um usuário com o login '{0}' cadastrado.", login));

                    usuarioAntigo = db.Usuarios.Where(u => u.Excluido == false && u.Email == email && u.UsuarioID != usuario.UsuarioID).FirstOrDefault();
                    if (usuarioAntigo != null)
                        throw new Exception(string.Format("Já existe um usuário com o email '{0}' cadastrado.", email));

                    usuario.Login = login;
                    usuario.Nome = nome;
                    usuario.Email = email;
                    usuario.Whatsapp = whatsapp;

                    db.SaveChanges();

                    Session["UserName"] = login;

                }

                return Json(new { success = true, message = "Seus dados foram atualizados com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }


        }

        #endregion

    }
}