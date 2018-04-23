using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxClasses.Internal;
using System.Text;
using System.Collections.Generic;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;

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
