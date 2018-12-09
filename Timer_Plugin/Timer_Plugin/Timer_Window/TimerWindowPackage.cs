﻿using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using Task = System.Threading.Tasks.Task;
using System.Windows;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;

namespace Timer_Plugin.Timer_Window
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]

    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(TimerWindowPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
    public sealed class TimerWindowPackage : AsyncPackage, IVsSolutionEvents
    {
        /// <summary>
        /// TimerWindowPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "d8e1aa18-381f-4418-94bb-2bfe851d4203";
        public static int timer_time = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerWindowPackage"/> class.
        /// </summary>
        /// 
        public TimerWindowPackage()
        {
            
            Initialize();
            TimerCallback tm = new TimerCallback(write_tick);
            System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 0, 1000);
           
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }
        
        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            MessageBox.Show("Opened a solution!");
            return VSConstants.S_OK;
        }


        private void write_tick(object sender)
        {
            timer_time += 1;
            //Time_Form.label1.Text = timer_time.ToString();
            //dt = dt.AddSeconds(1);
            //int current_time = dt.Second + dt.Minute * 60 + dt.Hour * 3600;
            //data.WriteToFile(current_time);
        }
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Time_Form.InitializeAsync(this);
            IVsSolution solution = GetService(typeof(SVsSolution)) as IVsSolution;
            uint cookie = 0;
            solution.AdviseSolutionEvents(this, out cookie);

        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            MessageBox.Show("Opened a project!");
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            throw new NotImplementedException();
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            throw new NotImplementedException();
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            throw new NotImplementedException();
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            throw new NotImplementedException();
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    
}
