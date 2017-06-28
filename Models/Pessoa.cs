using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Pessoa
    {
        public ObjectId Id {get;set;}

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Ddd {get;set;}

        public string Telefone {get;set;}

        public string  Endereco { get; set; }   

        public string Cidade { get; set; }

        public string Estado {get;set;}

        public bool Ativo { get; set; }
    }
}