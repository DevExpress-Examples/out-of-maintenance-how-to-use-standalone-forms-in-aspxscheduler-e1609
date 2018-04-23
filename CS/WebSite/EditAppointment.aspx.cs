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
using DevExpress.XtraScheduler;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class Default3 : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        schedulerDataSource.DataBind();
        ASPxScheduler scheduler = schedulerDataSource.Scheduler;
        appointmentForm.Scheduler = scheduler;
        if(!IsPostBack) {
            Appointment apt = ObtainAppointmentFromQueryString(scheduler, Request.QueryString);
            BindFormToAppointment(apt);
        }
    }
    void BindFormToAppointment(Appointment apt) {
        appointmentForm.Appointment = apt;
        appointmentForm.DataBind();
    }
    Appointment ObtainAppointmentFromQueryString(ASPxScheduler scheduler, NameValueCollection queryString) {
        Appointment apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);
        string stringId = queryString["id"];
        string stringStart = queryString["start"];
        string stringEnd = queryString["end"];
        string stringResourceId = queryString["resourceId"];
        string stringIsAllDay = queryString["isAllDay"];
        string stringIsRecurring = queryString["isRecurring"];
        if(!String.IsNullOrEmpty(stringId)) {
            apt = scheduler.LookupAppointmentByIdString(stringId);
            if(apt == null)
                GoToMainPage();
            if(!String.IsNullOrEmpty(stringIsRecurring)) {
                apt = apt.RecurrencePattern;
            }
        }
        else if(IsCreateRecurringAllDayEvent(queryString)) {
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Pattern);
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart);
            apt.Duration = TimeSpan.FromDays(1);
            apt.AllDay = true;
         }
        else if(IsCreateNewAllDayEvent(queryString)) {
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart);
            apt.Duration = TimeSpan.FromDays(1);
            apt.AllDay = true;
          }
        else if(IsCreateRecurringAppointment(queryString)) {
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Pattern);
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart);
            apt.End = SchedulerWebUtils.ToDateTime(stringEnd);
            apt.RecurrenceInfo.End = SchedulerWebUtils.ToDateTime(stringEnd);
         }
        else if(IsCreateAppointment(queryString)) {
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart);
            apt.End = SchedulerWebUtils.ToDateTime(stringEnd);
         }
        else { 
            DateTime nowTime = DateTime.Now;
		    DateTime now = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute, nowTime.Second);
            apt.Start = now;
            apt.Duration = TimeSpan.FromHours(3);
            stringResourceId = String.Empty;
        }
        apt.ResourceId = GetResourceId(stringResourceId);
        return apt;
    }

    object GetResourceId(string stringResourceId) {
        if (String.IsNullOrEmpty(stringResourceId)) 
            return ResourceEmpty.Id;
        return int.Parse(stringResourceId);
    }
    bool IsCreateNewAllDayEvent(NameValueCollection queryString) {
        string[] parameters = new string[] { "start", "allDay", "resourceId" };
        return IsQueryStringContainAllParams(queryString, parameters);
    }
    bool IsCreateAppointment(NameValueCollection queryString) {
        string[] parameters = new string[] { "start", "end", "resourceId"};
        return IsQueryStringContainAllParams(queryString, parameters);
    }
    bool IsCreateRecurringAppointment(NameValueCollection queryString) {
        string[] parameters = new string[] { "start", "end", "resourceId", "isRecurring"};
        return IsQueryStringContainAllParams(queryString, parameters);
    }
    bool IsCreateRecurringAllDayEvent(NameValueCollection queryString) {
        string[] parameters = new string[] { "start", "resourceId", "isRecurring", "allDay" };
        return IsQueryStringContainAllParams(queryString, parameters);
    }
    bool IsQueryStringContainAllParams(NameValueCollection queryString, string[] parameters) {
        int count = parameters.Length;
        for(int i = 0; i < count; i++) {
            string paramName = parameters[i];
            if(String.IsNullOrEmpty(queryString[paramName]))
                return false;
        }
        return true;
    }
    protected void OnFormClosed(object sender, EventArgs args) {
        GoToMainPage();
    }
    void GoToMainPage() {
        Response.Redirect("default.aspx", true);
    }
}
