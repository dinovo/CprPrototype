using CprPrototype.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CprPrototype.ViewModel
{ 
    /// <summary>
    /// The Base View Model class. This class links our 
    /// model and view together. Part of MVVM architecture.
    /// <remarks>
    /// All additional ViewModels should inherit the BaseViewModel.
    /// </remarks>
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        private static BaseViewModel instance;
        private AlgorithmBase algoBase;

        private string stepName, stepDescription;
        private AlgorithmStep currStep;
        private TimeSpan totalTime, stepTime;

        public event PropertyChangedEventHandler PropertyChanged;

        // Shared Bindable properties for our views
        public static readonly BindableProperty stepNameProperty = BindableProperty.Create(nameof(StepName), typeof(string), typeof(BaseViewModel));
        public static readonly BindableProperty stepDescriptionProperty = BindableProperty.Create(nameof(StepDescription), typeof(string), typeof(BaseViewModel));
        public static readonly BindableProperty nextStepProperty = BindableProperty.Create(nameof(CurrentPosition), typeof(AlgorithmStep), typeof(BaseViewModel));

        /// <summary>
        /// StepNameProperty accessor.
        /// </summary>
        public string StepName
        {
            get
            {
                return stepName;
            }
            set
            {
                if (stepName != value)
                {
                    stepName = value;
                    
                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("StepName");
                    }
                }
            }
        }

        /// <summary>
        /// StepDescriptionProperty accessor.
        /// </summary>
        public string StepDescription
        {
            get
            {
                return stepDescription;
            }
            set
            {
                if (stepDescription != value)
                {
                    stepDescription = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("StepDescription");
                    }
                }
            }
        }

        /// <summary>
        /// NextStepProperty accessor.
        /// </summary>
        public AlgorithmStep CurrentPosition
        {
            get
            {
                return currStep;
            }
            set
            {
                if (currStep != value)
                {
                    currStep = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("CurrentPosition");
                    }
                }
            }
        }

        /// <summary>
        /// Total time spent in the resuscitation process.
        /// </summary>
        public TimeSpan TotalTime
        {
            get { return totalTime; }
            set
            {
                if (totalTime != value)
                {
                    totalTime = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("TotalTime");
                    }
                }
            }
        }

        /// <summary>
        /// Total time spent in the resuscitation process.
        /// </summary>
        public TimeSpan StepTime
        {
            get { return stepTime; }
            set
            {
                if (stepTime != value)
                {
                    stepTime = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged("StepTime");
                    }
                }
            }
        }

        /// <summary>
        /// The Algorithm Model.
        /// </summary>
        public AlgorithmBase Algorithm { get { return algoBase; } }

        public InteractionMode Mode { get; set; }

        #endregion

        #region Construction & Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseViewModel()
        {
            // Init our algorithm base
            this.algoBase = new AlgorithmBase();
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        /// <remarks>
        /// Use this to access the BaseViewModel.
        /// </remarks>
        /// <returns>BaseViewModel Instance</returns>
        public static BaseViewModel Instance()
        {
            // uses lazy initialization.
            // this is not thread safe.
            if (instance == null)
            {
                instance = new BaseViewModel();
            }

            return instance;
        }

        #endregion

        /// <summary>
        /// Advances the algorithm and updates the current step property.
        /// </summary>
        public void AdvanceAlgorithm()
        {
            Algorithm.AdvanceOneStep();
            CurrentPosition = Algorithm.CurrentStep;
        }

        /// <summary>
        /// Event handler for INotifyPropertyChanged.
        /// </summary>
        /// <param name="propertyName">Name of the property changed. Optional</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                Debug.WriteLine("PropertyChanged - " + propertyName);

                if (propertyName.Equals("CurrentStep") && Algorithm.CurrentStep != null)
                {
                    CurrentPosition = Algorithm.CurrentStep;

                    StepName = CurrentPosition.Name;
                    StepDescription = CurrentPosition.Description;
                }
            }
        }
        
    }
}
