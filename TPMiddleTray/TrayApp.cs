using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using TPMiddleTray.Properties;

namespace TPMiddleTray {
  class TrayApp : ApplicationContext {
    private readonly BackgroundWorker worker = new BackgroundWorker();
    private readonly MenuItem statusMenuItem = new MenuItem("TPMiddleTray");
    private readonly NotifyIcon notifyIcon;
    private readonly MiddleClickProcessor middleClickProcessor;
    private readonly SynchronizationContext uiThreadContext;

    public TrayApp() {
      uiThreadContext = SynchronizationContext.Current;
      if (uiThreadContext == null) {
        uiThreadContext = new WindowsFormsSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(uiThreadContext);
      }

      notifyIcon = new NotifyIcon() {
        ContextMenu = new ContextMenu(
          new MenuItem[] {
            statusMenuItem,
            new MenuItem("-"),
            new MenuItem("Exit", OnExit),
          }),
        Icon = Resources.TrayIcon,
        Visible = true,
      };
      notifyIcon.Text = "TPMiddleTray: Handling 0 devices";

      middleClickProcessor = new MiddleClickProcessor();
      middleClickProcessor.DevicesEnumerated += MiddleClickProcessor_DevicesEnumerated;
      worker.DoWork += (sender, args) => middleClickProcessor.WorkLoop();
      worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      notifyIcon.Text = $"TPMiddleTray: Failed with {e.Error}";
    }

    private void MiddleClickProcessor_DevicesEnumerated(int nDevices) {
      uiThreadContext.Post(state => notifyIcon.Text = $"TPMiddleTray: Handling {nDevices} device(s)", null);
    }

    private void OnExit(object sender, EventArgs e) {
      notifyIcon.Visible = false;
      Application.Exit();
    }

  }
}
