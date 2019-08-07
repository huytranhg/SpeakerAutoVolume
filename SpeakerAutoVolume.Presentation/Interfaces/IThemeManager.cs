// <copyright file="IThemeManager.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Interfaces
{
    using System.Windows;

    /// <summary>
    /// Interface to be implemented in presentatation layer.
    /// </summary>
    public interface IThemeManager
    {
        /// <summary>
        /// Get theme resources.
        /// </summary>
        /// <returns>ResourceDictionary.</returns>
        ResourceDictionary GetThemeResources();
    }
}