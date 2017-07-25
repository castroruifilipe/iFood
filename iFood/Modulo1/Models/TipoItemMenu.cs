using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Modulo1.Models {
    
    public class TipoItemMenu {

        [PrimaryKey, AutoIncrement]
        public long? TipoItemMenuId { get; set; }
        public string Nome { get; set; }
        public byte[] Foto { get; set; }

        [OneToMany]
        public List<ItemMenu> Itens { get; set; }


        public override bool Equals(object obj) {
            TipoItemMenu tipoItemMenu = obj as TipoItemMenu;
            if (tipoItemMenu == null) {
                return false;
            }
            return (TipoItemMenuId.Equals(tipoItemMenu.TipoItemMenuId));
        }

        public override int GetHashCode() {
            return TipoItemMenuId.GetHashCode();
        }
    }
}
