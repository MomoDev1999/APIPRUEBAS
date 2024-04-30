using System;
using System.Collections.Generic;

namespace APIPRUEBAS.Models;

public partial class Producto
{
    public decimal Idproducto { get; set; }

    public string? Codigobarra { get; set; }

    public string? Descripcion { get; set; }

    public string? Marca { get; set; }

    public decimal? Idcategoria { get; set; }

    public decimal? Precio { get; set; }

    public virtual Categoria? oCategoria { get; set; }
}
