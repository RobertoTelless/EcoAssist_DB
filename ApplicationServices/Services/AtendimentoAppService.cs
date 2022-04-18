using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class AtendimentoAppService : AppServiceBase<TICKET_ATENDIMENTO>, IAtendimentoAppService
    {
        private readonly IAtendimentoService _baseService;

        public AtendimentoAppService(IAtendimentoService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<TICKET_ATENDIMENTO> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public List<TICKET_ATENDIMENTO> GetByData(DateTime data)
        {
            return _baseService.GetByData(data);
        }

        public TICKET_ATENDIMENTO GetItemById(Int32 id)
        {
            return _baseService.GetItemById(id);
        }

        public Int32 ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista, out List<TICKET_ATENDIMENTO > objeto)
        {
            try
            {
                objeto = new List<TICKET_ATENDIMENTO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(categoria, cliente, ordem, status, numero, assunto, inicio, prevista);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CATEGORIA_ATENDIMENTO> GetAllCats()
        {
            return _baseService.GetAllCats();
        }

        public List<DEPARTAMENTO> GetAllDeptos()
        {
            return _baseService.GetAllDeptos();
        }

        public List<CLIENTE> GetAllClientes()
        {
            return _baseService.GetAllClientes();
        }

        public List<ATENDIMENTO_STATUS> GetAllStatus()
        {
            return _baseService.GetAllStatus();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _baseService.GetAllUsuarios();
        }

        public List<ORDEM_SERVICO> GetAllOrdensServico()
        {
            return _baseService.GetAllOrdensServico();
        }

    }
}
