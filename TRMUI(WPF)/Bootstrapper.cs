using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TRMUI_WPF_.ViewModels;

namespace TRMUI_WPF_
{
   public  class Bootstrapper:BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();


        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            //use some reflection to wire up the viewmodels
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(vmt => _container.RegisterPerRequest(
                    vmt, vmt.ToString(), vmt));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
           DisplayRootViewFor<ShellViewModel>(); //base view
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
           _container.BuildUp(instance);
        }
    }
}
