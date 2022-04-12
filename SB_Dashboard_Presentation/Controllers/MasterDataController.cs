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
        CLIENTE objetoCL = new CLIENTE();
        CLIENTE objetoCLAntes = new CLIENTE();
        List<CLIENTE> listaMasterCL = new List<CLIENTE>();

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

        public ActionResult AtualizaDados()
        {
            Session["ListaCliente"] = null;
            Session["ListaPrestador"] = null;
            Session["ListaOS"] = null;
            Session["ListaAtendimento"] = null;
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
            Session["ListaClienteDia"] = listaCli;

            // Evolucao por mes/ano
            List<DateTime> meses = listaCli.Select(p => p.CLIE_DT_CADASTRO.Date).Distinct().ToList();
            meses.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod1 = new List<ModeloViewModel>();
            String ind = String.Empty;
            foreach (DateTime item in meses)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    Int32? conta1 = listaCli.Where(m => m.CLIE_DT_CADASTRO.Month == item.Month & m.CLIE_DT_CADASTRO.Year == item.Year).ToList().Count;
                    ModeloViewModel mod1 = new ModeloViewModel();
                    mod1.Nome = item.Month.ToString() + "/" + item.Year.ToString();
                    mod1.Valor1 = conta1.Value;
                    listaMod1.Add(mod1);
                }
            }
            ViewBag.ListaClienteMes = listaMod1;
            Session["ListaClienteMeses"] = meses;
            Session["ListaClienteMes"] = listaCli;

            // Carrega lista de filtros
            if ((List<CLIENTE>)Session["ListaCliente"] == null)
            {
                Session["ListaCliente"] = cliApp.GetAllItens();
            }
            List<SelectListItem> pessoa = new List<SelectListItem>();
            pessoa.Add(new SelectListItem() { Text = "Física", Value = "1" });
            pessoa.Add(new SelectListItem() { Text = "Jurídica", Value = "2" });
            ViewBag.Pessoas = new SelectList(pessoa, "Value", "Text");
            ViewBag.Listas = (List<CLIENTE>)Session["ListaCliente"];
            ViewBag.Tipos = new SelectList(cliApp.GetAllTipos(), "TICL_CD_ID", "TICL_NM_NOME");
            ViewBag.Origens = new SelectList(cliApp.GetAllOrigens(), "TICL_CD_ID", "TICL_NM_NOME");

            // Monta objeto de filtro
            CLIENTE objeto = new CLIENTE();
            objeto.CLIE_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpPost]
        public ActionResult FiltrarCliente(CLIENTE item)
        {
            try
            {
                // Executa a operação
                List<CLIENTE> listaObj = new List<CLIENTE>();
                Int32 volta = cliApp.ExecuteFilter(item.TICL_CD_ID, item.ORCL_CD_ID, item.CLIE_NM_NOME, item.CLIE_NM_RAZAO_SOCIAL, item.CLIE_IN_TIPO_PESSOA, item.CLIE_NR_CPF, item.CLIE_NR_CNPJ, null, null, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    return RedirectToAction("MontarTelaCliente");
                }

                // Sucesso
                Session["ListaCliente"] = listaObj;
                return RedirectToAction("MontarTelaCliente");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaCliente");
            }
        }

        public ActionResult RetirarFiltroCliente()
        {
            Session["ListaCliente"] = null;
            return RedirectToAction("MontarTelaCliente");
        }

        public JsonResult GetDadosGraficoClienteSituacao()
        {
            List<String> desc = new List<String>();
            List<Int32> quant = new List<Int32>();
            List<String> cor = new List<String>();

            Int32 q1 = (Int32)Session["CliAtivo"];
            Int32 q2 = (Int32)Session["CliInativo"];

            desc.Add("Ativos");
            quant.Add(q1);
            cor.Add("#359E18");
            desc.Add("Inativos");
            quant.Add(q2);
            cor.Add("#FFAE00");

            Hashtable result = new Hashtable();
            result.Add("labels", desc);
            result.Add("valores", quant);
            result.Add("cores", cor);
            return Json(result);
        }

        public JsonResult GetDadosGraficoCliente()
        {
            List<CLIENTE> listaCP1 = (List<CLIENTE>)Session["ListaClienteDia"];
            List<DateTime> datas = (List<DateTime>)Session["ListaClienteDatas"];
            List<CLIENTE> listaDia = new List<CLIENTE>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.CLIE_DT_CADASTRO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        public JsonResult GetDadosGraficoMesCliente()
        {
            List<CLIENTE> listaCP1 = (List<CLIENTE>)Session["ListaClienteMes"];
            List<DateTime> datas = (List<DateTime>)Session["ListaClienteMeses"];
            List<CLIENTE> listaMes = new List<CLIENTE>();
            List<String> meses = new List<String>();
            List<Int32> valor = new List<Int32>();
            meses.Add(" ");
            valor.Add(0);

            String ind = String.Empty;
            foreach (DateTime item in datas)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    listaMes = listaCP1.Where(m => m.CLIE_DT_CADASTRO.Month == item.Month & m.CLIE_DT_CADASTRO.Year == item.Year).ToList();
                    Int32 contaMes = listaMes.Count();
                    meses.Add(item.Month.ToString() + "/" + item.Year.ToString());
                    valor.Add(contaMes);
                }
            }

            Hashtable result = new Hashtable();
            result.Add("dias", meses);
            result.Add("valores", valor);
            return Json(result);
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