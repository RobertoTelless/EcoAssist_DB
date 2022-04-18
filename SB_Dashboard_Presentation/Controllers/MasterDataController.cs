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
        PRESTADOR objetoPR = new PRESTADOR();
        PRESTADOR objetoPRAntes = new PRESTADOR();
        List<PRESTADOR> listaMasterPR = new List<PRESTADOR>();
        ORDEM_SERVICO objetoOS = new ORDEM_SERVICO();
        ORDEM_SERVICO objetoOSAntes = new ORDEM_SERVICO();
        List<ORDEM_SERVICO> listaMasterOS = new List<ORDEM_SERVICO>();
        TICKET_ATENDIMENTO objetoAT = new TICKET_ATENDIMENTO();
        TICKET_ATENDIMENTO objetoATAntes = new TICKET_ATENDIMENTO();
        List<TICKET_ATENDIMENTO> listaMasterAT = new List<TICKET_ATENDIMENTO>();


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
            List<CLIENTE> listaCli = new List<CLIENTE>();
            listaCli = cliApp.GetAllItens();
            Session["ListaClienteCompleta"] = listaCli;
            Int32 cliAtivo = listaCli.Where(p => p.CLIE_IN_ATIVO == 1).ToList().Count;
            Int32 cliInativo = listaCli.Where(p => p.CLIE_IN_ATIVO == 0).ToList().Count;
            List<CLIENTE> listaCliAtivo = listaCli.Where(p => p.CLIE_IN_ATIVO == 1).ToList();
            ViewBag.CliAtivo = cliAtivo;
            ViewBag.CliInativo = cliInativo;  
            Session["CliAtivo"] = cliAtivo;
            Session["CliInativo"] = cliInativo;

            // Registros / Dia
            List<DateTime> datas = listaCli.Where(m => m.CLIE_DT_CADASTRO.Month == DateTime.Today.Date.Month & m.CLIE_DT_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.CLIE_DT_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
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
            Session["ListaClienteDia"] = listaCliAtivo;

            // Evolucao por mes/ano
            List<DateTime> meses = listaCliAtivo.Select(p => p.CLIE_DT_CADASTRO.Date).Distinct().ToList();
            meses.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod1 = new List<ModeloViewModel>();
            String ind = String.Empty;
            foreach (DateTime item in meses)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    Int32? conta1 = listaCliAtivo.Where(m => m.CLIE_DT_CADASTRO.Month == item.Month & m.CLIE_DT_CADASTRO.Year == item.Year).ToList().Count;
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
                listaCli = (List<CLIENTE>)Session["ListaClienteCompleta"];
                Session["ListaCliente"] = listaCli;
            }

            List<SelectListItem> pessoa = new List<SelectListItem>();
            pessoa.Add(new SelectListItem() { Text = "Física", Value = "1" });
            pessoa.Add(new SelectListItem() { Text = "Jurídica", Value = "2" });
            ViewBag.Pessoas = new SelectList(pessoa, "Value", "Text");
            ViewBag.Listas = (List<CLIENTE>)Session["ListaCliente"];
            ViewBag.Tipos = new SelectList(cliApp.GetAllTipos(), "TICL_CD_ID", "TICL_NM_NOME");
            ViewBag.Origens = new SelectList(cliApp.GetAllOrigens(), "ORCL_CD_ID", "ORCL_NM_NOME");

            // Monta objeto de filtro
            Session["VoltaCliente"] = 1;
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
            if ((Int32)Session["VoltaCliente"] == 1)
            {
                return RedirectToAction("MontarTelaCliente");
            }
            else
            {
                return RedirectToAction("VerClientesDataGeral");
            }
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

        public JsonResult GetDadosGraficoClienteGeral()
        {
            List<CLIENTE> listaCP1 = (List<CLIENTE>)Session["ListaClienteDiaGeral"];
            List<DateTime> datas = (List<DateTime>)Session["ListaClienteDatasGeral"];
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

        [HttpGet]
        public ActionResult VerClientesDataGeral()
        {
            // Carrega lista completa e dados basicos
            List<CLIENTE> listaCli = cliApp.GetAllItens();

            // Registros / Dia
            List<DateTime> datas = listaCli.Select(p => p.CLIE_DT_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaCli.Where(p => p.CLIE_DT_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaClienteDia = listaMod.OrderBy(p => p.DataEmissao);
            Session["ListaClienteDatasGeral"] = datas;
            Session["ListaClienteDiaGeral"] = listaCli;

            // Monta objeto
            Session["VoltaCliente"] = 2;
            CLIENTE objeto = new CLIENTE();
            objeto.CLIE_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerRegistroCliente(Int32 id)
        {
            // Recupera Cliente
            CLIENTE cliente = cliApp.GetItemById(id);
            return View(cliente);
        }

        [HttpGet]
        public ActionResult MontarTelaPrestador()
        {
            // Carrega lista completa e dados basicos
            List<PRESTADOR> listaPres = new List<PRESTADOR>();
            listaPres = preApp.GetAllItens();
            Session["ListaPrestadorCompleta"] = listaPres;
            Int32 preAtivo = listaPres.Where(p => p.PRES_IN_FLAG_ATIVO == 1).ToList().Count;
            Int32 preInativo = listaPres.Where(p => p.PRES_IN_FLAG_ATIVO == 0).ToList().Count;
            List<PRESTADOR> listaPreAtivo = listaPres.Where(p => p.PRES_IN_FLAG_ATIVO == 1).ToList();
            ViewBag.PreAtivo = preAtivo;
            ViewBag.PreInativo = preInativo;
            Session["PreAtivo"] = preAtivo;
            Session["PreInativo"] = preInativo;

            // Registros / Dia
            List<DateTime> datas = listaPres.Where(m => m.PRES_DT_DATA_CADASTRO.Month == DateTime.Today.Date.Month & m.PRES_DT_DATA_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.PRES_DT_DATA_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaPres.Where(p => p.PRES_DT_DATA_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaPrestadorDia = listaMod;
            Session["ListaPrestadorDatas"] = datas;
            Session["ListaPrestadorDia"] = listaPreAtivo;

            // Evolucao por mes/ano
            List<DateTime> meses = listaPreAtivo.Select(p => p.PRES_DT_DATA_CADASTRO.Date).Distinct().ToList();
            meses.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod1 = new List<ModeloViewModel>();
            String ind = String.Empty;
            foreach (DateTime item in meses)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    Int32? conta1 = listaPreAtivo.Where(m => m.PRES_DT_DATA_CADASTRO.Month == item.Month & m.PRES_DT_DATA_CADASTRO.Year == item.Year).ToList().Count;
                    ModeloViewModel mod1 = new ModeloViewModel();
                    mod1.Nome = item.Month.ToString() + "/" + item.Year.ToString();
                    mod1.Valor1 = conta1.Value;
                    listaMod1.Add(mod1);
                }
            }
            ViewBag.ListaPrestadorMes = listaMod1;
            Session["ListaPrestadorMeses"] = meses;
            Session["ListaPrestadorMes"] = listaPres;

            // Carrega lista de filtros
            if ((List<PRESTADOR>)Session["ListaPrestador"] == null)
            {
                listaPres = (List<PRESTADOR>)Session["ListaPrestadorCompleta"];
                Session["ListaPrestador"] = listaPres;
            }

            // Monta objeto de filtro
            Session["VoltaPrestador"] = 1;
            PRESTADOR objeto = new PRESTADOR();
            objeto.PRES_IN_FLAG_ATIVO = 1;
            return View(objeto);
        }

        [HttpPost]
        public ActionResult FiltrarPrestador(PRESTADOR item)
        {
            try
            {
                // Executa a operação
                List<PRESTADOR> listaObj = new List<PRESTADOR>();
                Int32 volta = preApp.ExecuteFilter(item.PRES_NM_NOME, item.PRES_NM_RAZAO_SOCIAL, item.PRES_NR_CNPJ, null, null, out listaObj);

                // Verifica retorno

                // Sucesso
                Session["ListaPrestador"] = listaObj;
                return RedirectToAction("MontarTelaPrestador");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaPrestador");
            }
        }

        public ActionResult RetirarFiltroPrestador()
        {
            Session["ListaPrestador"] = null;
            if ((Int32)Session["VoltaPrestador"] == 1)
            {
                return RedirectToAction("MontarTelaPrestador");
            }
            else
            {
                return RedirectToAction("VerPrestadorDataGeral");
            }
        }

        public JsonResult GetDadosGraficoPrestadorSituacao()
        {
            List<String> desc = new List<String>();
            List<Int32> quant = new List<Int32>();
            List<String> cor = new List<String>();

            Int32 q1 = (Int32)Session["PreAtivo"];
            Int32 q2 = (Int32)Session["PreInativo"];

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

        public JsonResult GetDadosGraficoPrestador()
        {
            List<PRESTADOR> listaCP1 = (List<PRESTADOR>)Session["ListaPrestadorDia"];
            List<DateTime> datas = (List<DateTime>)Session["ListaPrestadorDatas"];
            List<PRESTADOR> listaDia = new List<PRESTADOR>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.PRES_DT_DATA_CADASTRO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        public JsonResult GetDadosGraficoMesPrestador()
        {
            List<PRESTADOR> listaCP1 = (List<PRESTADOR>)Session["ListaPrestadorMes"];
            List<DateTime> datas = (List<DateTime>)Session["ListaPrestadorMeses"];
            List<PRESTADOR> listaMes = new List<PRESTADOR>();
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
                    listaMes = listaCP1.Where(m => m.PRES_DT_DATA_CADASTRO.Month == item.Month & m.PRES_DT_DATA_CADASTRO.Year == item.Year).ToList();
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

        public JsonResult GetDadosGraficoPrestadorGeral()
        {
            List<PRESTADOR> listaCP1 = (List<PRESTADOR>)Session["ListaPrestadorDiaGeral"];
            List<DateTime> datas = (List<DateTime>)Session["ListaPrestadorDatasGeral"];
            List<PRESTADOR> listaDia = new List<PRESTADOR>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.PRES_DT_DATA_CADASTRO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        [HttpGet]
        public ActionResult VerPrestadorDataGeral()
        {
            // Carrega lista completa e dados basicos
            List<PRESTADOR> listaCli = preApp.GetAllItens();

            // Registros / Dia
            List<DateTime> datas = listaCli.Select(p => p.PRES_DT_DATA_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaCli.Where(p => p.PRES_DT_DATA_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaClienteDia = listaMod.OrderBy(p => p.DataEmissao);
            Session["ListaPrestadorDatasGeral"] = datas;
            Session["ListaPrestadorDiaGeral"] = listaCli;

            // Monta objeto
            Session["VoltaPrestador"] = 2;
            PRESTADOR objeto = new PRESTADOR();
            objeto.PRES_IN_FLAG_ATIVO = 1;
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerRegistroPrestador(Int32 id)
        {
            // Recupera Cliente
            CLIENTE cliente = cliApp.GetItemById(id);
            return View(cliente);
        }

        [HttpGet]
        public ActionResult MontarTelaOrdemServico()
        {
            // Carrega lista completa e dados basicos
            List<ORDEM_SERVICO> listaOS = new List<ORDEM_SERVICO>();
            listaOS = OSApp.GetAllItens();
            Session["ListaOSCompleta"] = listaOS;
            Int32 osAtivo = listaOS.Where(p => p.ORSE_IN_ATIVO == 1).ToList().Count;
            Int32 osInativo = listaOS.Where(p => p.ORSE_IN_ATIVO == 0).ToList().Count;
            List<ORDEM_SERVICO> listaOSAtivo = listaOS.Where(p => p.ORSE_IN_ATIVO == 1).ToList();
            ViewBag.OSAtivo = osAtivo;
            ViewBag.OSInativo = osInativo;
            Session["OSAtivo"] = osAtivo;
            Session["OSInativo"] = osInativo;

            // Registros / Dia
            List<DateTime> datas = listaOS.Where(m => m.ORSE_DT_CADASTRO.Month == DateTime.Today.Date.Month & m.ORSE_DT_CADASTRO.Year == DateTime.Today.Date.Year).Select(p => p.ORSE_DT_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
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
            Session["ListaOSDia"] = listaOSAtivo;

            // Evolucao por mes/ano
            List<DateTime> meses = listaOSAtivo.Select(p => p.ORSE_DT_CADASTRO.Date).Distinct().ToList();
            meses.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod1 = new List<ModeloViewModel>();
            String ind = String.Empty;
            foreach (DateTime item in meses)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    Int32? conta1 = listaOSAtivo.Where(m => m.ORSE_DT_CADASTRO.Month == item.Month & m.ORSE_DT_CADASTRO.Year == item.Year).ToList().Count;
                    ModeloViewModel mod1 = new ModeloViewModel();
                    mod1.Nome = item.Month.ToString() + "/" + item.Year.ToString();
                    mod1.Valor1 = conta1.Value;
                    listaMod1.Add(mod1);
                }
            }
            ViewBag.ListaOSMes = listaMod1;
            Session["ListaOSMeses"] = meses;
            Session["ListaOSMes"] = listaOS;

            // Carrega lista de filtros
            if ((List<ORDEM_SERVICO>)Session["ListaOS"] == null)
            {
                listaOS = (List<ORDEM_SERVICO>)Session["ListaOSCompleta"];
                Session["ListaOS"] = listaOS;
            }

            // Monta objeto de filtro
            Session["VoltaOS"] = 1;
            ORDEM_SERVICO objeto = new ORDEM_SERVICO();
            objeto.ORSE_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpPost]
        public ActionResult FiltrarOrdemServico(ORDEM_SERVICO item)
        {
            try
            {
                // Executa a operação
                List<ORDEM_SERVICO> listaObj = new List<ORDEM_SERVICO>();
                Int32 volta = OSApp.ExecuteFilter(item.TIOS_CD_ID, item.CLIE_CD_ID, item.STOS_CD_ID, item.ORSE_NR_NUMERO, item.ORSE_DT_CADASTRO, null, null, out listaObj);

                // Verifica retorno

                // Sucesso
                Session["ListaOS"] = listaObj;
                return RedirectToAction("MontarTelaOrdemServico");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaOrdemServico");
            }
        }

        public ActionResult RetirarFiltroOrdemServico()
        {
            Session["ListaOS"] = null;
            if ((Int32)Session["VoltaOS"] == 1)
            {
                return RedirectToAction("MontarTelaOrdemServico");
            }
            else
            {
                return RedirectToAction("VerOrdemServicoDataGeral");
            }
        }

        public JsonResult GetDadosGraficoOrdemServicoSituacao()
        {
            List<String> desc = new List<String>();
            List<Int32> quant = new List<Int32>();
            List<String> cor = new List<String>();

            Int32 q1 = (Int32)Session["OSAtivo"];
            Int32 q2 = (Int32)Session["OSInativo"];

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

        public JsonResult GetDadosGraficoOrdemServico()
        {
            List<ORDEM_SERVICO> listaCP1 = (List<ORDEM_SERVICO>)Session["ListaOSDia"];
            List<DateTime> datas = (List<DateTime>)Session["ListaOSDatas"];
            List<ORDEM_SERVICO> listaDia = new List<ORDEM_SERVICO>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.ORSE_DT_CADASTRO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        public JsonResult GetDadosGraficoMesOrdemServico()
        {
            List<ORDEM_SERVICO> listaCP1 = (List<ORDEM_SERVICO>)Session["ListaOSMes"];
            List<DateTime> datas = (List<DateTime>)Session["ListaOSMeses"];
            List<ORDEM_SERVICO> listaMes = new List<ORDEM_SERVICO>();
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
                    listaMes = listaCP1.Where(m => m.ORSE_DT_CADASTRO.Month == item.Month & m.ORSE_DT_CADASTRO.Year == item.Year).ToList();
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

        public JsonResult GetDadosGraficoOrdemServicoGeral()
        {
            List<ORDEM_SERVICO> listaCP1 = (List<ORDEM_SERVICO>)Session["ListaOSDiaGeral"];
            List<DateTime> datas = (List<DateTime>)Session["ListaOSDatasGeral"];
            List<ORDEM_SERVICO> listaDia = new List<ORDEM_SERVICO>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.ORSE_DT_CADASTRO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        [HttpGet]
        public ActionResult VerOrdemServicoDataGeral()
        {
            // Carrega lista completa e dados basicos
            List<ORDEM_SERVICO> listaCli = OSApp.GetAllItens();

            // Registros / Dia
            List<DateTime> datas = listaCli.Select(p => p.ORSE_DT_CADASTRO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaCli.Where(p => p.ORSE_DT_CADASTRO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaOSDia = listaMod.OrderBy(p => p.DataEmissao);
            Session["ListaOSDatasGeral"] = datas;
            Session["ListaOSDiaGeral"] = listaCli;

            // Monta objeto
            Session["VoltaOS"] = 2;
            ORDEM_SERVICO objeto = new ORDEM_SERVICO();
            objeto.ORSE_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerRegistroOrdemServico(Int32 id)
        {
            // Recupera registro
            ORDEM_SERVICO reg = OSApp.GetItemById(id);
            return View(reg);
        }

        [HttpGet]
        public ActionResult MontarTelaAtendimento()
        {
            // Carrega lista completa e dados basicos
            List<TICKET_ATENDIMENTO> listaAT = new List<TICKET_ATENDIMENTO>();
            listaAT = ateApp.GetAllItens();
            Session["ListaATCompleta"] = listaAT;
            Int32 atAtivo = listaAT.Where(p => p.ATEN_IN_ATIVO == 1).ToList().Count;
            Int32 atInativo = listaAT.Where(p => p.ATEN_IN_ATIVO == 0).ToList().Count;
            List<TICKET_ATENDIMENTO> listaATAtivo = listaAT.Where(p => p.ATEN_IN_ATIVO == 1).ToList();
            ViewBag.ATAtivo = atAtivo;
            ViewBag.ATInativo = atInativo;
            Session["ATAtivo"] = atAtivo;
            Session["ATInativo"] = atInativo;

            // Registros / Dia
            List<DateTime> datas = listaAT.Where(m => m.ATEN_DT_INICIO.Month == DateTime.Today.Date.Month & m.ATEN_DT_INICIO.Year == DateTime.Today.Date.Year).Select(p => p.ATEN_DT_INICIO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaAT.Where(p => p.ATEN_DT_INICIO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaATDia = listaMod;
            Session["ListaATDatas"] = datas;
            Session["ListaATDia"] = listaATAtivo;

            // Evolucao por mes/ano
            List<DateTime> meses = listaATAtivo.Select(p => p.ATEN_DT_INICIO.Date).Distinct().ToList();
            meses.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod1 = new List<ModeloViewModel>();
            String ind = String.Empty;
            foreach (DateTime item in meses)
            {
                String ind1 = item.Month.ToString() + item.Year.ToString();
                if (ind != ind1)
                {
                    ind = ind1;
                    Int32? conta1 = listaATAtivo.Where(m => m.ATEN_DT_INICIO.Month == item.Month & m.ATEN_DT_INICIO.Year == item.Year).ToList().Count;
                    ModeloViewModel mod1 = new ModeloViewModel();
                    mod1.Nome = item.Month.ToString() + "/" + item.Year.ToString();
                    mod1.Valor1 = conta1.Value;
                    listaMod1.Add(mod1);
                }
            }
            ViewBag.ListaATMes = listaMod1;
            Session["ListaATMeses"] = meses;
            Session["ListaATMes"] = listaAT;

            // Carrega lista de filtros
            if ((List<TICKET_ATENDIMENTO>)Session["ListaAT"] == null)
            {
                listaAT = (List<TICKET_ATENDIMENTO>)Session["ListaATCompleta"];
                Session["ListaAT"] = listaAT;
            }

            // Monta objeto de filtro
            Session["VoltaAT"] = 1;
            TICKET_ATENDIMENTO objeto = new TICKET_ATENDIMENTO();
            objeto.ATEN_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpPost]
        public ActionResult FiltrarAtendimento(TICKET_ATENDIMENTO item)
        {
            try
            {
                // Executa a operação
                List<TICKET_ATENDIMENTO> listaObj = new List<TICKET_ATENDIMENTO>();
                Int32 volta = ateApp.ExecuteFilter(item.CAAT_CD_ID, item.CLIE_CD_ID, item.ORSE_CD_ID, item.ATST_CD_ID, item.ATEN_NR_NUMERO, item.ATEN_NM_ASSUNTO, item.ATEN_DT_INICIO, item.ATEN_DT_PREVISTA, out listaObj);

                // Verifica retorno

                // Sucesso
                Session["ListaAT"] = listaObj;
                return RedirectToAction("MontarTelaAtendimento");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaAtendimento");
            }
        }

        public ActionResult RetirarFiltroAtendimento()
        {
            Session["ListaAT"] = null;
            if ((Int32)Session["VoltaAT"] == 1)
            {
                return RedirectToAction("MontarTelaAtendimento");
            }
            else
            {
                return RedirectToAction("VerAtendimentoDataGeral");
            }
        }

        public JsonResult GetDadosGraficoAtendimentoSituacao()
        {
            List<String> desc = new List<String>();
            List<Int32> quant = new List<Int32>();
            List<String> cor = new List<String>();

            Int32 q1 = (Int32)Session["ATAtivo"];
            Int32 q2 = (Int32)Session["ATInativo"];

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

        public JsonResult GetDadosGraficoAtendimento()
        {
            List<TICKET_ATENDIMENTO> listaCP1 = (List<TICKET_ATENDIMENTO>)Session["ListaATDia"];
            List<DateTime> datas = (List<DateTime>)Session["ListaATDatas"];
            List<TICKET_ATENDIMENTO> listaDia = new List<TICKET_ATENDIMENTO>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.ATEN_DT_INICIO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        public JsonResult GetDadosGraficoMesAtendimento()
        {
            List<TICKET_ATENDIMENTO> listaCP1 = (List<TICKET_ATENDIMENTO>)Session["ListaATMes"];
            List<DateTime> datas = (List<DateTime>)Session["ListaATMeses"];
            List<TICKET_ATENDIMENTO> listaMes = new List<TICKET_ATENDIMENTO>();
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
                    listaMes = listaCP1.Where(m => m.ATEN_DT_INICIO.Month == item.Month & m.ATEN_CD_ID.Year == item.Year).ToList();
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

        public JsonResult GetDadosGraficoAtendimentoGeral()
        {
            List<TICKET_ATENDIMENTO> listaCP1 = (List<TICKET_ATENDIMENTO>)Session["ListaATDiaGeral"];
            List<DateTime> datas = (List<DateTime>)Session["ListaATDatasGeral"];
            List<TICKET_ATENDIMENTO> listaDia = new List<TICKET_ATENDIMENTO>();
            List<String> dias = new List<String>();
            List<Int32> valor = new List<Int32>();
            dias.Add(" ");
            valor.Add(0);

            foreach (DateTime item in datas)
            {
                listaDia = listaCP1.Where(p => p.ATEN_DT_INICIO.Date == item).ToList();
                Int32 contaDia = listaDia.Count();
                dias.Add(item.ToShortDateString());
                valor.Add(contaDia);
            }

            Hashtable result = new Hashtable();
            result.Add("dias", dias);
            result.Add("valores", valor);
            return Json(result);
        }

        [HttpGet]
        public ActionResult VerAtendimentoDataGeral()
        {
            // Carrega lista completa e dados basicos
            List<TICKET_ATENDIMENTO> listaCli = ateApp.GetAllItens();

            // Registros / Dia
            List<DateTime> datas = listaCli.Select(p => p.ATEN_DT_INICIO.Date).Distinct().ToList();
            datas.Sort((x, y) => x.Date.CompareTo(y.Date));
            List<ModeloViewModel> listaMod = new List<ModeloViewModel>();
            foreach (DateTime item in datas)
            {
                Int32? conta = listaCli.Where(p => p.ATEN_DT_INICIO == item).ToList().Count;
                ModeloViewModel mod1 = new ModeloViewModel();
                mod1.DataEmissao = item;
                mod1.Valor1 = conta.Value;
                listaMod.Add(mod1);
            }
            ViewBag.ListaOSDia = listaMod.OrderBy(p => p.DataEmissao);
            Session["ListaATDatasGeral"] = datas;
            Session["ListaATDiaGeral"] = listaCli;

            // Monta objeto
            Session["VoltaAT"] = 2;
            TICKET_ATENDIMENTO objeto = new TICKET_ATENDIMENTO();
            objeto.ATEN_IN_ATIVO = 1;
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerRegistroAtendimento(Int32 id)
        {
            // Recupera registro
            TICKET_ATENDIMENTO reg = ateApp.GetItemById(id);
            return View(reg);
        }
    }
}