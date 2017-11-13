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
            UpdateUI();
		}

        /// <summary>
        /// Updates UI elements based on the current step in the algorithm.
        /// </summary>
        private void UpdateUI()
        {
            if (viewModel.Algorithm.FirstStep == viewModel.Algorithm.CurrentStep)
            {
                btnShockable.IsVisible = true;
                btnNShockable.IsVisible = true;
                btnNextStep.IsVisible = false;
            }
            else
            {
                btnShockable.IsVisible = false;
                btnNShockable.IsVisible = false;
                btnNextStep.IsVisible = true;
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