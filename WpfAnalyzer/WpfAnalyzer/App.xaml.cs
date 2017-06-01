using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Thinktecture.IO;
using Thinktecture.IO.Adapters;
using WpfAnalyzer.Commands;
using WpfAnalyzer.Services;
using WpfAnalyzer.ViewModels;

namespace WpfAnalyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = InitializeContainer();
            MainWindow = (Window)container.Resolve<IMainWindow>();
            MainWindow.ShowDialog();
        }

        private IContainer InitializeContainer()
        {
            //Here's my composition root!
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().As<IMainWindow>();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<AnalyzerService>().As<IAnalyzerService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<BrowseCommand>().As<IBrowseCommand>();
            builder.RegisterType<DirectoryInfoAdapterService>().As<IDirectoryInfoAdapterService>();
            builder.RegisterType<FileAdapter>().As<IFile>();
            var container = builder.Build();
            return container;
        }
    }
}
