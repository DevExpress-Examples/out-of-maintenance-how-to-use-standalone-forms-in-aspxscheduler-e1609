using DevExpress.Web.ASPxScheduler.Internal;
using System;
public delegate void CreateControllerEventHandler(object sender, CreateControllerEventArgs args);
public class CreateControllerEventArgs : EventArgs {
    AppointmentFormController controller;

    public AppointmentFormController Controller {
        get {
            return controller;
        }
        set {
            controller = value;
        }
    }
}
