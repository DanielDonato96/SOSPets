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
    public class StaticController : Controller
    {
        #region Action

        public ActionResult AboutUs()
        {
            //AddHistoryPage();

            //ViewBag.SiteUrl = SitePolicy.GetSiteUrl("quem-somos.html");
            return View();
        }

        //public ActionResult Services()
        //{
        //    AddHistoryPage();

        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("servicos.html");
        //    return View();
        //}

        //public ActionResult CustomerList()
        //{
        //    AddHistoryPage();

        //    ViewBag.PathLogo = RstParametroGlobal.Get(eParametro.ImagePathClienteLogo);
        //    using (WebSiteContainer db = new WebSiteContainer())
        //    {
        //        ViewBag.ListaCliente = db.ClienteDestaque.Where(p => p.Ativo && !p.Excluido).OrderBy(p => p.Ordem).ToList();
        //    }
        //    return View();
        //}

        public ActionResult Contact()
        {
            //AddHistoryPage();
            return View();
        }

        public ActionResult Faq()
        {
            //AddHistoryPage();

            //ViewBag.SiteUrl = SitePolicy.GetSiteUrl("faq.html");
            return View();
        }

        //public ActionResult Privacy()
        //{
        //    AddHistoryPage();

        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("privacidade.html");
        //    return View();
        //}

        //public ActionResult TermsOfUse()
        //{
        //    AddHistoryPage();

        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("termos-de-uso.html");
        //    return View();
        //}

        //public ActionResult HowToJoin()
        //{
        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("como-participar.html");
        //    return View();
        //}

        //public ActionResult Cookies()
        //{
        //    AddHistoryPage();

        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("cookies.html");
        //    return View();
        //}

        //public ActionResult MoreInformation()
        //{
        //    AddHistoryPage();

        //    ViewBag.SiteUrl = SitePolicy.GetSiteUrl("mais-informacoes.html");
        //    return View();
        //}

        //public ActionResult LeftBarSecurity()
        //{
        //    AddHistoryPage();
        //    return View();
        //}

        //public ActionResult LeftBarOthers()
        //{
        //    AddHistoryPage();
        //    return View();
        //}
        #endregion

        //#region Partial
        //public PartialViewResult LogonBar()
        //{
        //    UserInfo();
        //    return PartialView();
        //}

        //public PartialViewResult FavoriteInfoFooter()
        //{
        //    UserInfo();
        //    return PartialView();
        //}
        //#endregion
    }

}