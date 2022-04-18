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
    public class PrestadorAppService : AppServiceBase<PRESTADOR>, IPrestadorAppService
    {
        private readonly IPrestadorService _baseService;

        public PrestadorAppService(IPrestadorService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<PRESTADOR> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public List<PRESTADOR> GetByNome(String nome)
        {
            return _baseService.GetByNome(nome);
        }

        public PRESTADOR GetItemById(Int32 id)
        {
            return _baseService.GetItemById(id);
        }

        public Int32 ExecuteFilter(String nome, String razao, String cnpj, String cidade, Int32? uf, out List<PRESTADOR > objeto)
        {
            try
            {
                objeto = new List<PRESTADOR>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(nome, razao, cnpj, cidade,uf);
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

    }
}
