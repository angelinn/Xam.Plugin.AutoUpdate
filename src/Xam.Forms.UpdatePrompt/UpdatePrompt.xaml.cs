using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xam.Forms.UpdatePrompt
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePrompt : Grid
	{
        private static Button button;
		public UpdatePrompt ()
		{
			InitializeComponent ();
            button = btn;
		}

        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create("ButtonText", typeof(string), typeof(UpdatePrompt), null, propertyChanged: OnButtonTextChanged);
        
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        private static void OnButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            button.Text = newValue.ToString();
        }
    }
}
