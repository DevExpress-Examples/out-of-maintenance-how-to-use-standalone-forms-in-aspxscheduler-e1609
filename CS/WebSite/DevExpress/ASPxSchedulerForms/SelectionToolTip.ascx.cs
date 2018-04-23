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
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxClasses;

public partial class UserForms_SelectionToolTip : ASPxSchedulerToolTipBase {
    public override bool ToolTipShowStem { get { return false; } }
    public override string ClassName { get { return "ASPxClientSelectionToolTip"; } }

    protected void Page_Load(object sender, EventArgs e) {
        DevExpress.Web.ASPxClasses.ASPxWebControl.RegisterBaseScript(Page);
    }
    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
        btnShowMenu.Image.Assign(GetSmartTagImage());
    }
    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { buttonDiv };
        return controls;
    }
}
