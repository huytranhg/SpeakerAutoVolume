// <copyright file="ThemeManager.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Controls
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;
    using SpeakerAutoVolume.Presentation.Interfaces;

    /// <summary>
    /// Implementation of IThemeManager.
    /// </summary>
    [Export(typeof(IThemeManager))]
    public class ThemeManager : IThemeManager
    {
        private readonly ResourceDictionary themeResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeManager"/> class.
        /// </summary>
        public ThemeManager()
        {
            this.themeResources = new ResourceDictionary
                                      {
                                          Source = new Uri("pack://application:,,,/SpeakerAutoVolume;component/Resources/Theme.xaml"),
                                      };
        }

        /// <summary>
        /// Get Theme Resources.
        /// </summary>
        /// <returns> ResourceDictionary object.</returns>
        public ResourceDictionary GetThemeResources()
        {
            return this.themeResources;
        }
    }
}