using System;
using Modulo1.Dal;
using Modulo1.Models;
using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuListPage : ContentPage {

        private TipoItemMenuDAL dalTipoItemMenu = new TipoItemMenuDAL();

       
        public TiposItensMenuListPage() {
            InitializeComponent();
        }

        public async void OnRemoverClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemMenu;

            var opcao = await DisplayAlert("Confirmação de remoção", "Confirma remover o item " + item.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao == true) {
                dalTipoItemMenu.DeleteById((long)item.TipoItemMenuId);
                lvTiposItensMenu.ItemsSource = dalTipoItemMenu.GetAll();
            }
        }

        public async void OnAlterarClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemMenu;
            await Navigation.PushModalAsync(new TiposItensMenuEditPage(item));
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            lvTiposItensMenu.ItemsSource = dalTipoItemMenu.GetAll();
        }
    }
}
