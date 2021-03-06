﻿using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;
using Timer_Plugin.Timer_Window.Data;
using System.Xml;
using Microsoft.VisualStudio;
using System.Windows.Forms;

namespace Timer_Plugin.Timer_Window
{
    /// <summary>
    /// Command handler
    /// </summary>
    /// 
    
    internal sealed class Time_Form
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;
        
        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("a6fb0a45-8ce0-4cbc-b264-fc852dd1bc5f");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="Time_Form"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        /// 
        OpenData data = new OpenData();
        DateTime dt = new DateTime();
       // public static Time_Form winform = new Time_Form();
        private Time_Form(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);

        }

        
        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Time_Form Instance
        {
            get;
            private set;
        }
        


        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in TimerWindow's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new Time_Form(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        /// 

        
        private void Execute(object sender, EventArgs e)
        {
            // System.Timers.Timer aTimer;
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);


            if ((!Timer_Plugin.Time_Form.IsFormOpen) && (!Forms.DataForm.IsDataFormOpen))
            {
                Timer_Plugin.Time_Form newForm1 = new Timer_Plugin.Time_Form();
                newForm1.Show();
                               
            }

        }
    }
}
