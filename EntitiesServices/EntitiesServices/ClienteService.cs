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
    public class ClienteService : ServiceBase<CLIENTE>, IClienteService
    {
        private readonly IClienteRepository _baseRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public ClienteService(IClienteRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<CLIENTE> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<CLIENTE> GetByNome(String nome)
        {
            return _baseRepository.GetByNome(nome);
        }
        public List<CLIENTE> ExecuteFilter(Int32? tipo, Int32? origem, String nome, String razao, Int32? pessoa, String cpf, String cnpj, String cidade, Int32? uf)
        {
            return _baseRepository.ExecuteFilter(tipo, origem, nome, razao, pessoa, cpf, cnpj, cidade, uf);
        }

    }
}
