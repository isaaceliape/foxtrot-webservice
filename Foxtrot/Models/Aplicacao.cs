//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foxtrot.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Aplicacao
    {
        public Aplicacao()
        {
            this.Pedido = new HashSet<Pedido>();
        }
    
        public byte idAplicacao { get; set; }
        public string DescAplicacao { get; set; }
        public string TipoAplicacao { get; set; }
    
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}