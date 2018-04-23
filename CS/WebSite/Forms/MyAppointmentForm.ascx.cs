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
using System.Text;
using DevExpress.Web.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web;

public partial class Forms_AppointmentForm : ASPxSchedulerClientFormBase {
    #region Fields
    IEnumerable labelDataSource;
    IEnumerable statusDataSource;
    IEnumerable resourceDataSource;
    ASPxScheduler scheduler;
    Appointment appointment;
    #endregion

    #region Properties
    public override string ClassName { get { return "ASPxClientAppointmentForm"; } }
    public ASPxScheduler Scheduler {
        get {
            if(scheduler == null)
                this.scheduler = FindControlById(SchedulerId) as ASPxScheduler;
            return scheduler;
        }
        set
        {
            scheduler = value;
        }
    }
    protected  IEnumerable LabelDataSource {
        get {
            if(labelDataSource == null) {
                this.labelDataSource = ASPxSchedulerFormDataHelper.CreateLabelDataSource(Scheduler);
            }
            return labelDataSource;
        }
    }
    protected IEnumerable StatusDataSource {
        get {
            if (statusDataSource == null)
                this.statusDataSource = ASPxSchedulerFormDataHelper.CreateStatusesDataSource(Scheduler);
            return statusDataSource;
        }
    }
    protected IEnumerable ResourceDataSource {
        get {
            if (resourceDataSource == null)
                this.resourceDataSource = ASPxSchedulerFormDataHelper.CreateResourceDataSource(Scheduler);
            return resourceDataSource;
        }
    }
	public Appointment Appointment {
        get {
            return appointment;
        }
        set {
            appointment = value;
        }
    }
    #endregion

    #region Events
    static readonly object onFormClosed = new object();
    public event EventHandler FormClosed {
        add {
            Events.AddHandler(onFormClosed, value);
        }
        remove {
            Events.RemoveHandler(onFormClosed, value);
        }
    }
    protected void RaiseFormClosed() {
        EventHandler handler = Events[onFormClosed] as EventHandler;
        if (handler == null)
            return;
        handler(this, new EventArgs());
    }
    #endregion

    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { edtStartDate, edtEndDate, tbSubject,
                tbDescription, tbLocation, edtLabel, edtStatus, chkAllDay,
                chkRecurrence, edtResource, recurrenceControl, btnOk, btnCancel, btnDelete};
        return controls;
    }
    public override void DataBind() {
        base.DataBind();
        if(Appointment == null)
            return;
        object objectAppointmentId = Scheduler.GetAppointmentClientId(appointment);
        appointmentId.Value = (objectAppointmentId == null) ? String.Empty : objectAppointmentId.ToString();
        edtStartDate.Value = Appointment.Start;
        edtEndDate.Value = Appointment.End;
        tbSubject.Value = Appointment.Subject;
        tbLocation.Value = Appointment.Location;
        edtLabel.Value = Appointment.LabelKey.ToString();
        edtStatus.Value = Appointment.StatusKey.ToString();
        edtResource.Value = (Appointment.ResourceId == ResourceEmpty.Resource) ? "null" : Appointment.ResourceId.ToString();
        tbDescription.Value = Appointment.Description;
        chkAllDay.Checked = Appointment.AllDay;
        if(Appointment.Type.Equals(AppointmentType.Occurrence) || Appointment.Type.Equals(AppointmentType.ChangedOccurrence)) {
            chkRecurrence.Visible = false;
            recurrenceControl.Visible = false;
        }
        else {
            chkRecurrence.Checked = Appointment.IsRecurring;
            recurrenceControl.SetClientVisible(Appointment.IsRecurring);
            recurrenceControl.AppointmentStart = Appointment.Start;
            recurrenceControl.Pattern = (Appointment.Type.Equals(AppointmentType.Pattern)) ? Appointment : null;
            recurrenceControl.DataBind();
        }
        btnDelete.ClientEnabled = !Scheduler.Storage.Appointments.IsNewAppointment(Appointment);
    }
    protected void OnBtnDeleteClick(object sender, EventArgs e) {
        Appointment apt = Scheduler.LookupAppointmentByIdString(appointmentId.Value);
        if(apt == null)
            return;
        apt.Delete();
        RaiseFormClosed();
    }
    protected void OnBtnCancelClick(object sender, EventArgs e) {
        RaiseFormClosed();
    }
    protected void OnBtnOkClick(object sender, EventArgs e) {
        Appointment apt = (String.IsNullOrEmpty(appointmentId.Value)) ? Scheduler.Storage.CreateAppointment(AppointmentType.Normal) : Scheduler.LookupAppointmentByIdString(appointmentId.Value);
        AppointmentFormController formController = new AppointmentFormController(Scheduler, apt);
        if (formController == null)
            return;
        AssignControllerValues(formController);
        formController.ApplyChanges();
        RaiseFormClosed();
    }
    public void AssignControllerValues(AppointmentFormController controller) {
        TimeZoneHelper helper = new TimeZoneHelper(Scheduler.OptionsBehavior.ClientTimeZoneId);
        controller.Start = helper.FromClientTime(edtStartDate.Date);
        controller.End = helper.FromClientTime(edtEndDate.Date);
        controller.Subject = tbSubject.Text;
        controller.Location = tbLocation.Text;
        controller.Description = tbDescription.Text;
        controller.AllDay = chkAllDay.Checked;
        controller.StatusKey = Convert.ToInt32(edtStatus.Value);
        controller.LabelKey = Convert.ToInt32(edtLabel.Value);
        controller.ResourceId = (edtResource.Value.ToString() != "null") ? edtResource.Value : ResourceEmpty.Id;
        if(chkRecurrence.Checked) 
            recurrenceControl.AssignControllerValues(controller, helper.FromClientTime(edtStartDate.Date));
    }
}
