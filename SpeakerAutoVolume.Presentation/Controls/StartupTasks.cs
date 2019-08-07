// <copyright file="StartupTasks.cs" company="Huy Tran">
// Copyright (c) Huy Tran. All rights reserved.
// </copyright>

namespace SpeakerAutoVolume.Presentation.Controls
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using Caliburn.Micro;
    using MahApps.Metro.Controls;
    using SpeakerAutoVolume.Presentation.Interfaces;

    /// <summary>
    /// Startup Task.
    /// </summary>
    public delegate void StartupTask();

    /// <summary>
    /// Startup Tasks.
    /// </summary>
    public class StartupTasks
    {
        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupTasks"/> class.
        /// </summary>
        /// <param name="serviceLocator"> Service locator.</param>
        [ImportingConstructor]
        public StartupTasks(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        /// <summary>
        /// Apply Binding Scope Override.
        /// </summary>
        [Export(typeof(StartupTask))]
        public void ApplyBindingScopeOverride()
        {
            var getNamedElements = BindingScope.GetNamedElements;
            BindingScope.GetNamedElements = o =>
                {
                    if (!(o is MetroWindow metroWindow))
                    {
                        return getNamedElements(o);
                    }

                    var list = new List<FrameworkElement>(getNamedElements(o));
                    var type = o.GetType();
                    var fields =
                        o.GetType()
                         .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                         .Where(f => f.DeclaringType == type);
                    var flyouts =
                        fields.Where(f => f.FieldType == typeof(FlyoutsControl))
                              .Select(f => f.GetValue(o))
                              .Cast<FlyoutsControl>();
                    list.AddRange(flyouts);
                    return list;
                };
        }

        /// <summary>
        /// Apply View Locator Override.
        /// </summary>
        [Export(typeof(StartupTask))]
        public void ApplyViewLocatorOverride()
        {
            var viewLocator = this.serviceLocator.GetInstance<IViewLocator>();
            Caliburn.Micro.ViewLocator.GetOrCreateViewType = viewLocator.GetOrCreateViewType;
        }
    }
}