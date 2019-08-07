// <copyright file="IViewLocator.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Interfaces
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interface for ViewLocator.
    /// </summary>
    public interface IViewLocator
    {
        /// <summary>
        /// Get Or Create View Type.
        /// </summary>
        /// <param name="viewType"> View type.</param>
        /// <returns> UIElement object.</returns>
        UIElement GetOrCreateViewType(Type viewType);
    }
}