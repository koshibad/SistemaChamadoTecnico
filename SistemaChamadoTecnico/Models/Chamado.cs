//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaChamadoTecnico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Chamado
    {
        public int IdChamado { get; set; }
        public Nullable<int> IdCliente { get; set; }

        public Nullable<int> IdAtendente { get; set; }

        public string TituloChamado { get; set; }

        [DataType(DataType.MultilineText)]
        public string DescricaoChamado { get; set; }

        public string EstadoChamado { get; set; }

        [DataType(DataType.MultilineText)]
        public string ObsevacaoChamado { get; set; }

        public Nullable<System.DateTime> DataCriacaoChamado { get; set; }

        public Nullable<System.DateTime> DataEncerramentoChamado { get; set; }
    
        public virtual Atendente Atendente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
