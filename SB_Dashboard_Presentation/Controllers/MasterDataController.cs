using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationServices.Interfaces;
using EntitiesServices.Model;
using System.Globalization;
using EntitiesServices.WorkClasses;
using AutoMapper;
using SB_Dashboard_Presentation.ViewModels;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Web.UI.WebControls;
using System.Runtime.Caching;
using Image = iTextSharp.text.Image;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SB_Dashboard_Presentation.Controllers
{
    public class MasterDataController : Controller
    {
        private readonly IPrestadorAppService preApp;
        private readonly IClienteAppService cliApp;
        private readonly IAtendimentoAppService ateApp;
        private readonly IOrdemServicoAppService OSApp;

        private String msg;
        private Exception exception;
        String extensao;

        public MasterDataController(IPrestadorAppService preApps, IClienteAppService cliApps, IAtendimentoAppService ateApps, IOrdemServicoAppService OSApps)
        {
            cliApp = cliApps;
            preApp = preApps;
            ateApp = ateApps;
            OSApp = OSApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Voltar()
        {
            return RedirectToAction("MontarTelaCliente", "MasterData");
        }

        [HttpGet]
        public ActionResult MontarTelaCliente()
        {
            // Carrega lista completa e dados basicos
            List<CLIENTE> listaCli = cliApp.GetAllItens();
            Int32 cliAtivo = listaCli.Where(p => p.CLIE_IN_ATIVO == 1).ToList().Count;
            Int32 cliInativo = listaCli.Where(p => p.CLIE_IN_ATIVO == 0).ToList().Count;
            ViewBag.CliAtivo = cliAtivo;    
            ViewBag.CliInativo = cliInativo;  
            Session["CliAtivo"] = cliAtivo;
            Session["CliInativo"] = cliInativo;

            // Registros / Dia
            List<DateTime> datas = listaCli.Where(m => m.CLIE_DT_CADASTRO.Month == DateTime.Today.Date.Month & m.CLIE_DT_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.CLIE_DT_CADASTRO.Date).Distinct().ToList();
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaCli.Where(p => p.CLIE_DT_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaClienteDia = listaMod;
            Session["ListaClienteDatas"] = datas;
            Session["ListaClienteDia"] = listaMod;

            // Evolucao por mes/ano



            // Carrega lista de filtros





            return View();
        }

        [HttpGet]
        public ActionResult MontarTelaPrestador()
        {
            // Carrega lista completa e dados basicos
            List<PRESTADOR> listaPre = preApp.GetAllItens();
            Int32 preAtivo = listaPre.Where(p => p.PRES_IN_FLAG_ATIVO == 1).ToList().Count;
            Int32 preInativo = listaPre.Where(p => p.PRES_IN_FLAG_ATIVO == 0).ToList().Count;
            ViewBag.PreAtivo = preAtivo;
            ViewBag.PreInativo = preInativo;
            Session["PreAtivo"] = preAtivo;
            Session["PreInativo"] = preInativo;

            // Registros / Dia
            List<DateTime> datas = listaPre.Where(m => m.PRES_DT_DATA_CADASTRO.Month == DateTime.Today.Date.Month & m.PRES_DT_DATA_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.PRES_DT_DATA_CADASTRO.Date).Distinct().ToList();
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaPre.Where(p => p.PRES_DT_DATA_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaPrestadorDia = listaMod;
            Session["ListaPrestadorDatas"] = datas;
            Session["ListaPrestadorDia"] = listaMod;

            // Evolucao por mes/ano









            return View();
        }

        [HttpGet]
        public ActionResult MontarTelaOrdemServico()
        {
            // Carrega lista completa e dados basicos
            List<ORDEM_SERVICO> listaOS = OSApp.GetAllItens();
            Int32 OSAtivo = listaOS.Where(p => p.ORSE_IN_ATIVO == 1).ToList().Count;
            Int32 OSInativo = listaOS.Where(p => p.ORSE_IN_ATIVO == 0).ToList().Count;
            ViewBag.OSAtivo = OSAtivo;
            ViewBag.OSInativo = OSInativo;
            Session["OSAtivo"] = OSAtivo;
            Session["OSInativo"] = OSInativo;

            // Registros / Dia
            List<DateTime> datas = listaOS.Where(m => m.ORSE_DT_CADASTRO.Month == DateTime.Today.Date.Month & m.ORSE_DT_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.ORSE_DT_CADASTRO.Date).Distinct().ToList();
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaOS.Where(p => p.ORSE_DT_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaOSDia = listaMod;
            Session["ListaOSDatas"] = datas;
            Session["ListaOSDia"] = listaMod;

            // Evolucao por mes/ano









            return View();
        }

        [HttpGet]
        public ActionResult MontarTelaAtendimento()
        {
            // Carrega lista completa e dados basicos
            List<TICKET_ATENDIMENTO> listaAte = ateApp.GetAllItens();
            Int32 ateAtivo = listaAte.Where(p => p.ATEN_IN_ATIVO == 1).ToList().Count;
            Int32 ateInativo = listaAte.Where(p => p.ATEN_IN_ATIVO == 0).ToList().Count;
            ViewBag.AteAtivo = ateAtivo;
            ViewBag.AteInativo = ateInativo;
            Session["AteAtivo"] = ateAtivo;
            Session["AteInativo"] = ateInativo;

            // Registros / Dia
            List<DateTime> datas = listaAte.Where(m => m.ATEN_DT_INICIO.Month == DateTime.Today.Date.Month & m.ATEN_DT_INICIO.Year == DateTime.Today.Date.Year).Select(p => p.ATEN_DT_INICIO.Date).Distinct().ToList();
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaAte.Where(p => p.ATEN_DT_INICIO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaAtendDia = listaMod;
            Session["ListaAtendDatas"] = datas;
            Session["ListaAtendDia"] = listaMod;

            // Evolucao por mes/ano









            return View();
        }





    }
}