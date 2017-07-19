using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuListPage : ContentPage {

        private TipoItemMenuDAL dalTipoItemMenu = TipoItemMenuDAL.GetInstance();

       
        public TiposItensMenuListPage() {
            InitializeComponent();
            lvTiposItensMenu.ItemsSource = dalTipoItemMenu.GetAll();
        }
    }
}
