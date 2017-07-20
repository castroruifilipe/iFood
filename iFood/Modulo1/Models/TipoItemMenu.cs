using System;

namespace Modulo1.Models {
    
    public class TipoItemMenu {

        public long Id { get; set; }
        public string Nome { get; set; }
        public string CaminhoArquivoFoto { get; set; }


        public override bool Equals(object obj) {
            TipoItemMenu tipoItemMenu = obj as TipoItemMenu;
            if (tipoItemMenu == null) {
                return false;
            }
            return (Id.Equals(tipoItemMenu.Id));
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }
    }
}
