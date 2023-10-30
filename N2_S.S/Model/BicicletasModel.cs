using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N2_S.S.Model
{
    [Table("bicicletas")]
    public class BicicletasModel 
    {
        [Column("cod_bicicleta")]
        public int Cod_bicicleta { get; set; }
        
        [Column("marca_bicicleta")]
        public string Marca_Bicicleta { get; set; }
        
        [Column("cor_bicicleta")]
        public string Cor_Bicicleta { get; set;}
    }
}
