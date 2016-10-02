using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaChamadoTecnico.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [DataType(DataType.Password)]
        public string Confirma { get; set; }
        public String Funcao { get; set; }
    }
}