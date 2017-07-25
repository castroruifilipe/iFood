using System;
using System.Collections.Generic;
using Modulo1.Models;
using Xamarin.Forms;

namespace Modulo1.Pages.ItensMenu {
    
    public partial class ItensMenuEditPage : ContentPage {


        public ItensMenuEditPage(ItemMenu itemMenu) {
            InitializeComponent();
            gridControl.PopularControls(itemMenu);
        }
    }
}
