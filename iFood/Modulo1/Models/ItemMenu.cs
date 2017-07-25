using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Modulo1.Models {
    
    public class ItemMenu {

        [PrimaryKey, AutoIncrement]
        public long? ItemMenuId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Foto { get; set; }

        [ForeignKey(typeof(TipoItemMenu))]
        public long? TipoItemMenuId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public TipoItemMenu TipoItemMenu { get; set; }

    }
}
