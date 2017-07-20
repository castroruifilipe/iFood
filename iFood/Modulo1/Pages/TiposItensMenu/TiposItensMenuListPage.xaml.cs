using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Modulo1.Models;
using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuListPage : ContentPage {

        private TipoItemMenuDAL dalTipoItemMenu = TipoItemMenuDAL.GetInstance();

       
        public TiposItensMenuListPage() {
            InitializeComponent();
            lvTiposItensMenu.ItemsSource = dalTipoItemMenu.GetAll();
        }

        public async void OnRemoverClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemMenu;

            var opcao = await DisplayAlert("Confirmação de remoção", "Confirma remover o item " + item.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao == true) {
                dalTipoItemMenu.Remove(item);
            }
        }

        public async void OnAlterarClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemMenu;
            await Navigation.PushModalAsync(new TiposItensMenuEditPage(item));
        }
    }
}
