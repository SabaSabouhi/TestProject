using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using VendingMachine.Common;
using VendingMachine.ViewModels;
using VendingMachine.Views;

namespace VendingMachine {
    public partial class App {
        private CompositionContainer _container;
        [Import( typeof( IVendingMachine ) )]
        public IVendingMachine VendingMachine;

        private void App_OnStartup( object sender, StartupEventArgs e ) {
            var catalog = new AggregateCatalog();

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyLocation = Path.GetDirectoryName( assembly.Location );
            if ( string.IsNullOrEmpty( assemblyLocation ) ) assemblyLocation = ".";
            var pluginPath = Path.Combine( assemblyLocation, "Plugins" );
            if ( !Directory.Exists( pluginPath ) )
                Directory.CreateDirectory( pluginPath );
            catalog.Catalogs.Add( new DirectoryCatalog( pluginPath ) );

            _container = new CompositionContainer( catalog, CompositionOptions.DisableSilentRejection );

            try {
                var batch = new CompositionBatch();
                batch.AddExportedValue( _container );
                _container.Compose( batch );

            } catch ( CompositionException compositionException ) {
                Console.WriteLine( compositionException.ToString() );
            }

            var beverageParts = _container.GetExportedValues<IBeverage>()
                .ToList();

            var viewFactory = new ViewFactory();
            Global.ViewFactory = viewFactory;

            var mainWindow = new MainWindow();
            var mainWindowViewModel = (MainWindowViewModel)mainWindow.DataContext;
            mainWindowViewModel.AddBeverageParts( beverageParts );
            mainWindow.ShowDialog();
        }

        private void App_OnExit( object sender, ExitEventArgs e ) {
            MessageBox.Show( "See you next time." );
        }
        private void App_OnDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e ) {
            Debug.WriteLine( e.Exception.Message );
            e.Handled = true;
        }
    }
}
