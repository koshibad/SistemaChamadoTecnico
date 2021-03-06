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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class Chamado
    {
        public int IdChamado { get; set; }
        public Nullable<int> IdCliente { get; set; }

        public Nullable<int> IdAtendente { get; set; }

        [DisplayName("T�tulo")]
        [Required(ErrorMessage = "O T�tulo � obrigat�rio !")]
        public string TituloChamado { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Descri��o")]
        [Required(ErrorMessage = "A descri��o � obrigat�ria !")]
        public string DescricaoChamado { get; set; }

        [DisplayName("Estado")]
        public string EstadoChamado { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Observa��o")]
        public string ObsevacaoChamado { get; set; }

        [DisplayName("Data de Cria��o")]
        public Nullable<System.DateTime> DataCriacaoChamado { get; set; }

        [DisplayName("Data de Encerramento")]
        public Nullable<System.DateTime> DataEncerramentoChamado { get; set; }

        public virtual Atendente Atendente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
