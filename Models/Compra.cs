using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace accountmetrics.Models;

[Table("Compras")]
public class Compra
{
    [Key]
    public int CompraId { get; set; }
    
    [Column("CodigoNF")]
    [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} dever ter no máximo {1} caracteres.")]
    [Required(ErrorMessage = "Informe o código da NF.")]
    [Display(Name = "Código da Nota Fiscal.")]
    public string CodigoNF { get; set; }

    [Column("DataCompra")]
    [Required(ErrorMessage = "Por favor a data da compra.")]
    //[Range(0, 130, ErrorMessage = "A idade precisa estar entre 0 and 130.")]
    public DateTime DataCompra { get; set; }

    // New property: Valor
    [Column("Valor", TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "Informe o valor da compra.")]
    [Display(Name = "Valor (BRL)")]
    public decimal Valor { get; set; }
    
}