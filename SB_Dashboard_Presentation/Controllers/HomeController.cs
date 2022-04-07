using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationServices.Interfaces;
using EntitiesServices.Model;
using System.Globalization;
using EntitiesServices.Work_Classes;
using AutoMapper;
using System.IO;

namespace ERP_Condominios_Solution.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["CarregaListas"] = 0;
            Session["Filtro"] = null;
            Session["FalhaCR"] = 0;
            Session["FalhaCP"] = 0;
            Session["FalhaPC"] = 0;
            Session["FalhaLP"] = 0;
            Session["Inicio"] = 0;
            Session["CarregaListas1"] = 0;
            Session["Filtro1"] = null;
            Session["OSAtrasadas"] = null;
            Session["OSPendencia"] = null;
            Session["OSPesquisa"] = null;
            Session["OSAvaliacao"] = null;
            Session["OSCidade"] = null;
            Session["OSEspecialidade"] = null;
            Session["OSSituacao"] = null;
            Session["OSTipo"] = null;
            Session["OSUF"] = null;
            Session["GraficoPagar"] = null;
            Session["GraficoReceita"] = null;
            Session["GraficoDespesa"] = null;
            Session["GraficoCR"] = null;
            return RedirectToAction("Index", "Dashboard");
            //return RedirectToAction("MontarTelaDashboardReal", "Dashboard");
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
    }
}