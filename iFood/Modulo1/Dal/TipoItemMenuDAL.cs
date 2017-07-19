using System;
using System.Collections.ObjectModel;
using Modulo1.Models;

namespace Modulo1.Dal {
    
    public class TipoItemMenuDAL {

        private ObservableCollection<TipoItemMenu> TipoItensMenu = new ObservableCollection<TipoItemMenu>();
        private static TipoItemMenuDAL TipoItemMenuInstance = new TipoItemMenuDAL();

        private TipoItemMenuDAL() {
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 1, Nome = "Pizzas", CaminhoArquivoFoto = "pizzas.png"
            });
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 2, Nome = "Bebidas", CaminhoArquivoFoto = "bebidas.png"
            });
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 3, Nome = "Saladas", CaminhoArquivoFoto = "saladas.png"
            });
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 4, Nome = "Sanduíches", CaminhoArquivoFoto = "sanduiches.png"
            });
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 5, Nome = "Sobremesas", CaminhoArquivoFoto = "sobremesas.png"
            });
            TipoItensMenu.Add(new TipoItemMenu() {
                Id = 6, Nome = "Carnes", CaminhoArquivoFoto = "carnes.png"
            });
        }

        public static TipoItemMenuDAL GetInstance() {
            return TipoItemMenuInstance;
        }

        public ObservableCollection<TipoItemMenu> GetAll() {
            return TipoItensMenu;
        }

        public void Add(TipoItemMenu tipoItemMenu) {
            this.TipoItensMenu.Add(tipoItemMenu);
        }
    }
}
