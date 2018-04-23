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
using DevExpress.XtraScheduler;
using System.Data.OleDb;
using System.Drawing;

public partial class SchedulerAsDataSource : System.Web.UI.UserControl {
    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        SetupStatuses(ASPxScheduler1);
    }
    void SetupStatuses(ASPxScheduler control) {
        control.Storage.Appointments.Statuses.Clear();
        IEnumerable data = StatusDataSource.Select(new DataSourceSelectArguments());
        foreach(DataRowView dataItem in data) {
            string name = (string)dataItem.Row["Name"];
            Color color = GetStatusColor(dataItem.Row["Color"]);
            control.Storage.Appointments.Statuses.Add(GetStatusColor(color), name, name);
        }
    }
    Color GetStatusColor(object cl) {
        if(cl == DBNull.Value)
            return Color.FromArgb(0xFFFFFF);
        if(cl is Color)
            return (Color)cl;
        int statusColor = Convert.ToInt32(cl);
        return Color.FromArgb(statusColor);
    }

    protected void Page_Load(object sender, EventArgs e) {
        DataBind();
    }
    public ASPxScheduler Scheduler {
        get {
            return ASPxScheduler1;
        }
    }
    #region DataBind
    int lastInsertedAppointmentId;
    protected void ASPxScheduler1_AppointmentRowInserting(object sender, ASPxSchedulerDataInsertingEventArgs e) {
        // Autoincremented primary key case
        e.NewValues.Remove("ID");
    }
    protected void ASPxScheduler1_AppointmentRowInserted(object sender, ASPxSchedulerDataInsertedEventArgs e) {
        // Autoincremented primary key case
        e.KeyFieldValue = this.lastInsertedAppointmentId;
    }
    protected void AppointmentsDataSource_Inserted(object sender, System.Web.UI.WebControls.SqlDataSourceStatusEventArgs e) {
        // Autoincremented primary key case
        OleDbConnection connection = (OleDbConnection)e.Command.Connection;
        using(OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY", connection)) {
            this.lastInsertedAppointmentId = (int)cmd.ExecuteScalar();
        }
    }
    protected void ASPxScheduler1_OnAppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
        // Autoincremented primary key case
        int count = e.Objects.Count;
        System.Diagnostics.Debug.Assert(count == 1);
        Appointment apt = (Appointment)e.Objects[0];
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        storage.SetAppointmentId(apt, lastInsertedAppointmentId);
    }
    #endregion
}
