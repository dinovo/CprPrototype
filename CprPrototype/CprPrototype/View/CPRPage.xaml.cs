using CprPrototype.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CprPrototype.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CPRPage : ContentPage
	{
        private BaseViewModel viewModel = BaseViewModel.Instance();

		public CPRPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel;

            // ListView
            DataTemplate template = new DataTemplate(typeof(DrugCell));
            template.SetBinding(DrugCell.NameProperty, "DrugDoseString");
            template.SetBinding(DrugCell.TimeRemainingProperty, "TimeRemainingString");
            template.SetBinding(DrugCell.ButtonCommandProperty, "DrugCommand");

            listView.HasUnevenRows = true;
            listView.ItemTemplate = template;
            listView.ItemsSource = viewModel.DoseQueue;
            listView.BindingContext = viewModel;

            UpdateUI();
		}

        /// <summary>
        /// Updates UI elements based on the current step in the algorithm.
        /// </summary>
        private void UpdateUI()
        {
            if (viewModel.CurrentPosition.NextStep == viewModel.Algorithm.FirstStep)
            {
                btnNextStep.Text = "Done";
            }
            else
            {
                btnNextStep.Text = "Next Step";
            }

            if (viewModel.Algorithm.FirstStep == viewModel.Algorithm.CurrentStep)
            {
                btnShockable.IsVisible = true;
                btnNShockable.IsVisible = true;
                btnNextStep.IsVisible = false;
                lblStepTime.IsVisible = false;
            }
            else
            {
                btnShockable.IsVisible = false;
                btnNShockable.IsVisible = false;
                btnNextStep.IsVisible = true;
                lblStepTime.IsVisible = true;
            }

        }

        /// <summary>
        /// Handler for Silent Button clicked event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void ShockableButton_Clicked(object sender, EventArgs e)
        {
            viewModel.Algorithm.BeginSequence(Model.RythmStyle.Shockable);
            viewModel.Algorithm.AddDrugsToQueue(viewModel.DoseQueue, Model.RythmStyle.Shockable);
            viewModel.AdvanceAlgorithm();
            UpdateUI();
        }

        /// <summary>
        /// Handler for Silent Button clicked event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void NShockableButton_Clicked(object sender, EventArgs e)
        {
            viewModel.Algorithm.BeginSequence(Model.RythmStyle.NonShockable);
            viewModel.Algorithm.AddDrugsToQueue(viewModel.DoseQueue, Model.RythmStyle.NonShockable);
            viewModel.AdvanceAlgorithm();
            UpdateUI();
        }

        /// <summary>
        /// Handler for Silent Button clicked event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void NextStepButton_Clicked(object sender, EventArgs e)
        {
            viewModel.AdvanceAlgorithm();
            UpdateUI();
        }
    }
}