using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaChamadoTecnico.Models
{
    public class Funcao
    {
        public enum eFuncao
        {
            Admin,
            Atendente,
            Cliente
        }

        public Funcao(int id, string nome)
        {
            this.IdFuncao = id;
            this.NomeFuncao = nome;
        }

        public int IdFuncao { get; set; }
        public string NomeFuncao { get; set; }
    }
}