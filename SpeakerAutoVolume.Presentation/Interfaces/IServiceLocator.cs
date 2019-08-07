// <copyright file="IServiceLocator.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Interfaces
{
    /// <summary>
    /// Interface for ServiceLocator.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Interface for ServiceLocator.
        /// </summary>
        /// <typeparam name="T">The generic type parameter.</typeparam>
        /// <returns>Type T.</returns>
        T GetInstance<T>()
            where T : class;
    }
}