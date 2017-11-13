using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace CprPrototype.Model
{
    /// <summary>
    /// The AlgorithmBase class represents a collection of algorithm steps,
    /// resulting in the digital form of CPR algorithm.
    /// </summary>
    public class AlgorithmBase : IDisposable, INotifyPropertyChanged
    {
        #region Properties

        private List<AlgorithmStep> steps;
        private AssessmentStep step1;
        private AlgorithmStep shock1, shock2, nshock1, exit1, currentStep;
        public static readonly BindableProperty _currentStepProperty = BindableProperty.Create(nameof(CurrentStep), typeof(AlgorithmStep), typeof(AlgorithmBase));

        /// <summary>
        /// Property Changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Current step in the algorithm.
        /// </summary>
        public AlgorithmStep CurrentStep
        {
            get { return currentStep; }
            set
            {
                if (currentStep != value)
                {
                    currentStep = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentStep"));
                    }
                }
            }
        }

        /// <summary>
        /// Returns the first step in the algorithm.
        /// </summary>
        public AlgorithmStep FirstStep { get { return steps[0]; } }

        #endregion

        #region Construction & Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public AlgorithmBase()
        {
            steps = InitializeSteps();

            // Select first step
            CurrentStep = steps[0];
        }

        /// <summary>
        /// Consturct and Initialize algorithm steps.
        /// </summary>
        public List<AlgorithmStep> InitializeSteps()
        {
            if (steps != null)
            {
                throw new Exception("AlgorithmBase - InitiliazeSteps() : Steps already initialized.");
            }

            // Initial Step
            step1 = new AssessmentStep("Rythm Assesment", "Rate the rythm of the patient.");

            // Shockable Steps
            shock1 = new AlgorithmStep("Shock once.", "Minimize interruptions.");
            shock2 = new AlgorithmStep("CPR 2 Minutes", "Continue CPR for 2 minutes, then reassess the rythm.");

            // Non-Shockable Steps
            nshock1 = new AlgorithmStep("HLR 2 Minutes", "Continue HLR for 2 minutes, then reassess the rythm.");

            // Exit Steps
            exit1 = new AlgorithmStep("Circulation restored", "Continue with further resuscitation");

            // Setup Step Relations
            step1.PreviousStep = null;
            step1.CircRestoredStep = exit1;

            shock1.PreviousStep = step1;
            shock1.NextStep = shock2;
            shock2.PreviousStep = shock1;
            shock2.NextStep = step1;

            nshock1.PreviousStep = step1;
            nshock1.NextStep = step1;

            exit1.PreviousStep = step1;

            // Add everything to the list
            List<AlgorithmStep> result = new List<AlgorithmStep>();
            result.Add(step1);
            result.Add(shock1);
            result.Add(shock2);
            result.Add(nshock1);
            result.Add(exit1);

            return result;
        }

        #endregion

        /// <summary>
        /// Initiates the Shockable sequence.
        /// </summary>
        public void BeginSequence(RythmStyle style)
        {
            switch (style)
            {
                case RythmStyle.Shockable:
                    CurrentStep.NextStep = steps[1];
                    // TODO: Save info to db
                    break;
                case RythmStyle.NonShockable:
                    CurrentStep.NextStep = steps[3];
                    // TODO: Save info to db
                    break;
            }
        }

        /// <summary>
        /// Advances to the next step. Returns True if successful.
        /// </summary>
        /// <returns>True if successful, else false</returns>
        public bool AdvanceOneStep()
        {
            var next = CurrentStep.NextStep;

            // Sanity check
            if (next != null)
            {
                CurrentStep = next;
                return true;
            }
            else
            {
                throw new OperationCanceledException("AlgorithmBase : AdvanceOneStep() - Next step is null.");
                return false;
            }
        }

        /// <summary>
        /// IDisposible implementation.
        /// </summary>
        public void Dispose()
        {
            this.steps = null;
        }
    }
}