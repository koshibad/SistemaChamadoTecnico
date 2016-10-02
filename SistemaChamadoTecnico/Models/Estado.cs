using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaChamadoTecnico.Models
{
    public class Estado
    {
        public enum eEstado
        {
            Aguardando,
            Pendente,
            Finalizado
        }

        public Estado(int id, string nome)
        {
            this.IdEstado = id;
            this.NomeEstado = nome;
        }

        public int IdEstado { get; set; }
        public string NomeEstado { get; set; }
    }
}