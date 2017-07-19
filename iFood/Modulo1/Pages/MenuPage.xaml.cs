using System;
using System.Collections.Generic;
using Modulo1.Pages.Entregadores;
using Modulo1.Pages.Garcons;
using Modulo1.Pages.TiposItensMenu;

using Xamarin.Forms;

namespace Modulo1.Pages {

    public partial class MenuPage : ContentPage {


        public MenuPage() {
            InitializeComponent();
        }

        private async void GarconsOnClicked(object sender, EventArgs args) {
            await Navigation.PushAsync(new GarconsPage());
        }

        private async void EntregadoresOnClicked(object sender, EventArgs args) {
            await Navigation.PushAsync(new EntregadoresPage());
        }

        private async void TiposItensMenuOnClicked(object sender, EventArgs args) {
            await Navigation.PushAsync(new TiposItensMenuPage());
        }
    }
}
