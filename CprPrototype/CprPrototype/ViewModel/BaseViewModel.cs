using CprPrototype.Model;
using System;

namespace CprPrototype.ViewModel
{ 
    /// <summary>
    /// The Base View Model class. This class links our 
    /// model and view together. Part of MVVM architecture.
    /// <remarks>
    /// All additional ViewModels should inherit the BaseViewModel.
    /// </remarks>
    /// </summary>
    public class BaseViewModel
    {
        private AlgorithmBase algoBase;

        public AlgorithmStep CurrentStep { get { return algoBase.CurrentStep;  } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseViewModel()
        {
            // Init our algorithm base
            this.algoBase = new AlgorithmBase();
        }
    }
}
