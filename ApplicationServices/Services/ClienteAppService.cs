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
    public class ClienteAppService : AppServiceBase<CLIENTE>, IClienteAppService
    {
        private readonly IClienteService _baseService;

        public ClienteAppService(IClienteService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<CLIENTE> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public List<CLIENTE> GetByNome(String nome)
        {
            return _baseService.GetByNome(nome);
        }

        public List<TIPO_CLIENTE> GetAllTipos()
        {
            return _baseService.GetAllTipos();
        }

        public List<ORIGEM_CLIENTE> GetAllOrigens()
        {
            return _baseService.GetAllOrigens();
        }

        public Int32 ExecuteFilter(Int32? tipo, Int32? origem, String nome, String razao, Int32? pessoa, String cpf, String cnpj, String cidade, Int32? uf, out List<CLIENTE > objeto)
        {
            try
            {
                objeto = new List<CLIENTE>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(tipo, origem, nome, razao, pessoa, cpf, cnpj, cidade, uf);
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
