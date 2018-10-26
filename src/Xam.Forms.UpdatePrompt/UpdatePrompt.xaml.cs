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
        private static Label label;
        
        public UpdatePrompt()
        {
            InitializeComponent();
            button = btn;
            label = lbl;
        }

        public event EventHandler<UpdatesCheckArgs> UpdatesCheckDelegate;

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

        public static readonly BindableProperty UpdateTextProperty =
    BindableProperty.Create("ButtonText", typeof(string), typeof(UpdatePrompt), null, propertyChanged: OnUpdateTextChanged);

        public string UpdateText
        {
            get { return (string)GetValue(UpdateTextProperty); }
            set { SetValue(UpdateTextProperty, value); }
        }

        private static void OnUpdateTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            label.Text = newValue.ToString();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            content.HeightRequest = height / 3;
            content.WidthRequest = width / 3;
        }

        protected override async void OnParentSet()
        {
            UpdatesCheckArgs args = new UpdatesCheckArgs();
            UpdatesCheckDelegate.Invoke(this, args);

            IsVisible = await args.UpdateTask();
        }
    }
}
