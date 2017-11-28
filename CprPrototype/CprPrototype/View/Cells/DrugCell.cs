using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CprPrototype.View
{
    public class DrugCell : ViewCell
    {
        #region Properties 

        Label lblName, lblTime;
        Button btnCommand;

        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(DrugCell));
        public static readonly BindableProperty TimeRemainingProperty = BindableProperty.Create(nameof(Time), typeof(string), typeof(DrugCell));
        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(nameof(DrugInjectedCommand), typeof(ICommand), typeof(DrugCell));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public ICommand DrugInjectedCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        public string Time
        {
            get { return (string)GetValue(TimeRemainingProperty); }
            set { SetValue(TimeRemainingProperty, value); }
        }

        #endregion

        #region Construct

        public DrugCell()
        {
            // Init Views
            lblName = new Label();
            lblTime = new Label();
            btnCommand = new Button();
            btnCommand.Text = "OK";
            btnCommand.WidthRequest = 60;
            //btnCommand.HeightRequest = 15;
            btnCommand.HorizontalOptions = LayoutOptions.End;

            var labelLayout = new StackLayout
            {
                Spacing = 2,
                Margin = new Thickness(15, 0, 0, 0),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            labelLayout.Children.Add(lblName);
            labelLayout.Children.Add(lblTime);

            var labelBtnLayout = new StackLayout
            {
                Spacing = 2,
                Orientation = StackOrientation.Horizontal
            };
            labelBtnLayout.Children.Add(labelLayout);
            labelBtnLayout.Children.Add(btnCommand);

            // Set bindings
            //lblName.SetBinding(Label.TextProperty, nameof(Name));
            //lblTime.SetBinding(Label.TextProperty, nameof(Time), BindingMode.OneWay, null, "{0:mm\\:ss}");
            //btnCommand.SetBinding(Button.CommandProperty, nameof(DrugInjectedCommand));

            View = labelBtnLayout;
        }

        #endregion

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lblName.Text = Name;
                lblTime.Text = Time;
                btnCommand.Command = DrugInjectedCommand;
            }
        }
    }
}
