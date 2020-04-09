namespace SistemaFacturaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EntradaMercancia
    {
        public int Id { get; set; }

        public int? producto_id { get; set; }

        public int? proveedor_id { get; set; }

        public int Cantidad { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Proveedore Proveedore { get; set; }
    }
}
