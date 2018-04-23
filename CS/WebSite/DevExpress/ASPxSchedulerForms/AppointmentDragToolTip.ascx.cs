using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;

public partial class UserForms_AppointmentDragToolTip : ASPxSchedulerToolTipBase {
    public override string ClassName { get { return "ASPxClientAppointmentDragTooltip"; } }
    public override bool ToolTipShowStem { get { return true; } }

    protected void Page_Load(object sender, EventArgs e) {
        DevExpress.Web.ASPxClasses.ASPxWebControl.RegisterBaseScript(Page);
    }
    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { lblInterval };
        return controls;
    }
}
