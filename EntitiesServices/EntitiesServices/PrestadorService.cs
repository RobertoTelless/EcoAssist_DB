using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class PrestadorService : ServiceBase<PRESTADOR>, IPrestadorService
    {
        private readonly IPrestadorRepository _baseRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public PrestadorService(IPrestadorRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<PRESTADOR> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<PRESTADOR> GetByNome(String nome)
        {
            return _baseRepository.GetByNome(nome);
        }
        public List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String cidade, Int32? uf)
        {
            return _baseRepository.ExecuteFilter(nome, razao, cnpj, cidade, uf);
        }

    }
}
