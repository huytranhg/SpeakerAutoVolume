// <copyright file="MefServiceLocator.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Services
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using SpeakerAutoVolume.Presentation.Interfaces;

    /// <summary>
    /// Implementation of IServiceLocator.
    /// </summary>
    [Export(typeof(IServiceLocator))]
    public class MefServiceLocator : IServiceLocator
    {
        private readonly CompositionContainer compositionContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MefServiceLocator"/> class.
        /// </summary>
        /// <param name="compositionContainer"> Composition container.</param>
        [ImportingConstructor]
        public MefServiceLocator(CompositionContainer compositionContainer)
        {
            this.compositionContainer = compositionContainer;
        }

        /// <summary>
        /// Interface for ServiceLocator.
        /// </summary>
        /// <typeparam name="T">The generic type parameter.</typeparam>
        /// <returns>Type T.</returns>
        public T GetInstance<T>()
            where T : class
        {
            var instance = this.compositionContainer.GetExportedValue<T>();
            if (instance != null)
            {
                return instance;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", typeof(T)));
        }
    }
}