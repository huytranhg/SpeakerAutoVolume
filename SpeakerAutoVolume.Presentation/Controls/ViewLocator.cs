// <copyright file="ViewLocator.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Controls
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using SpeakerAutoVolume.Presentation.Interfaces;

    /// <summary>
    /// Implementation of IViewLocator.
    /// </summary>
    [Export(typeof(IViewLocator))]
    public class ViewLocator : IViewLocator
    {
        private readonly IThemeManager themeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewLocator"/> class.
        /// </summary>
        /// <param name="themeManager"> Implemented object of IThemeManager.</param>
        [ImportingConstructor]
        public ViewLocator(IThemeManager themeManager)
        {
            this.themeManager = themeManager;
        }

        /// <summary>
        /// Get Or Create View Type.
        /// </summary>
        /// <param name="viewType"> View type.</param>
        /// <returns> UIElement object.</returns>
        public UIElement GetOrCreateViewType(Type viewType)
        {
            var cached = IoC.GetAllInstances(viewType).OfType<UIElement>().FirstOrDefault();
            if (cached != null)
            {
                Caliburn.Micro.ViewLocator.InitializeComponent(cached);
                return cached;
            }

            if (viewType.IsInterface || viewType.IsAbstract || !typeof(UIElement).IsAssignableFrom(viewType))
            {
                return new TextBlock { Text = string.Format("Cannot create {0}.", viewType.FullName) };
            }

            var newInstance = (UIElement)Activator.CreateInstance(viewType);

            if (newInstance is Window window)
            {
                window.Resources.MergedDictionaries.Add(this.themeManager.GetThemeResources());
            }

            Caliburn.Micro.ViewLocator.InitializeComponent(newInstance);
            return newInstance;
        }
    }
}