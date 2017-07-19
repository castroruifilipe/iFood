using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Xamarin.Forms;

namespace Modulo1.Pages.Garcons {
    
    public partial class GarconsListPage : ContentPage {

        private GarconDAL dalGarcons = GarconDAL.GetInstance();

        public GarconsListPage() {
            InitializeComponent();
            lvGarcons.ItemsSource = dalGarcons.GetAll();
        }
    }
}
