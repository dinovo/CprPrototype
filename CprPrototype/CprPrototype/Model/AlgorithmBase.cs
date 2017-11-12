using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CprPrototype.Model
{
    /// <summary>
    /// The AlgorithmBase class represents a collection of algorithm steps,
    /// resulting in the digital form of CPR algorithm.
    /// </summary>
    public class AlgorithmBase : IDisposable
    {
        #region Properties

        private List<AlgorithmStep> steps;
        public BindableProperty _currentStepProperty = BindableProperty.Create(nameof(CurrentStep), typeof(AlgorithmStep), typeof(AlgorithmBase));

        /// <summary>
        /// Current step in the algorithm.
        /// </summary>
        public AlgorithmStep CurrentStep { get; set; }

        #endregion

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
            AssessmentStep step1 = new AssessmentStep("Rythm Assesment", "Rate the rythm of the patient.");

            // Shockable Steps
            AlgorithmStep shock1 = new AlgorithmStep("Shock once.", "Minimize interruptions.");
            AlgorithmStep shock2 = new AlgorithmStep("CPR 2 Minutes", "Continue CPR for 2 minutes, then reassess the rythm.");

            // Non-Shockable Steps
            AlgorithmStep nshock1 = new AlgorithmStep("HLR 2 Minutes", "Continue HLR for 2 minutes, then reassess the rythm.");

            // Exit Steps
            AlgorithmStep exit1 = new AlgorithmStep("Circulation restored", "Continue with further resuscitation");

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

        /// <summary>
        /// IDisposible implementation.
        /// </summary>
        public void Dispose()
        {
            this.steps = null;
        }
    }
}