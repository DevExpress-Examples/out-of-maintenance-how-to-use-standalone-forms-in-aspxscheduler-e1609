using System;
using System.Web.UI;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler.Controls;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;

public partial class Forms_RecurrenceControl : ASPxSchedulerClientFormBase {
    Appointment pattern;
    DateTime start = DateTime.Today;

    public Appointment Pattern { get { return pattern; } set { pattern = value; } }
    public DateTime AppointmentStart { get { return start; } set { start = value; } }
    public override string ClassName { get { return "ASPxClientRecurrenceAppointmentForm"; } }
    
    public void SetClientVisible(bool visible) {
        if (visible)
            mainDiv.Style.Add(HtmlTextWriterStyle.Display, "");
        else
            mainDiv.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { edtDailyRecurrenceControl, edtWeeklyRecurrenceControl, 
            edtMonthlyRecurrenceControl, edtYearlyRecurrenceControl, 
            edtRecurrenceTypeEdit, edtRecurrenceRangeControl, mainDiv};
        return controls;
    }
    public override void DataBind() {
        base.DataBind();

        RecurrenceInfo defaultRecurrenceInfo = new RecurrenceInfo(AppointmentStart.Date.AddDays(10));
        defaultRecurrenceInfo.OccurrenceCount = 10;
        RecurrenceInfo recurrenceInfo = (Pattern == null) ? defaultRecurrenceInfo : (RecurrenceInfo)Pattern.RecurrenceInfo;

        edtRecurrenceTypeEdit.Type = recurrenceInfo.Type;
        edtRecurrenceRangeControl.Range = recurrenceInfo.Range;
        edtRecurrenceRangeControl.End = recurrenceInfo.End;
        edtRecurrenceRangeControl.OccurrenceCount = recurrenceInfo.OccurrenceCount;
        InitializeRecurrenceRuleConrtol(recurrenceInfo);
    }
    void InitializeRecurrenceRuleConrtol(RecurrenceInfo info) {
        RecurrenceInfo defaultInfo = new RecurrenceInfo(info.Start);
        InitializeEdtDailyRecurrenceControl(info, defaultInfo);
        InitializeEdtWeeklyRecurrenceControl(info, defaultInfo);
        InitializeEdtMonthlyRecurrenceControl(info, defaultInfo);
        InitializeEdtYearlyRecurrenceControl(info, defaultInfo);
     }
    void InitializeEdtDailyRecurrenceControl(RecurrenceInfo info, RecurrenceInfo defaultInfo) {
        RecurrenceInfo actualInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Daily);
        this.edtDailyRecurrenceControl.ClientVisible = actualInfo == info;
        this.edtDailyRecurrenceControl.Periodicity = actualInfo.Periodicity;
        this.edtDailyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Daily);
    }
    RecurrenceInfo GetActualInfo(RecurrenceInfo info, RecurrenceInfo defaultInfo, RecurrenceType type) {
        bool isActive = info.Type.Equals(type);
        return (isActive) ? info : defaultInfo;
    }
    void InitializeEdtWeeklyRecurrenceControl(RecurrenceInfo info, RecurrenceInfo defaultInfo) {
        RecurrenceInfo actualInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Weekly);
        this.edtWeeklyRecurrenceControl.ClientVisible = actualInfo == info;
        this.edtWeeklyRecurrenceControl.Periodicity = actualInfo.Periodicity;
        this.edtWeeklyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Weekly);
    }
    void InitializeEdtMonthlyRecurrenceControl(RecurrenceInfo info, RecurrenceInfo defaultInfo) {
        RecurrenceInfo actualInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Monthly);
        this.edtMonthlyRecurrenceControl.ClientVisible = actualInfo == info;
        this.edtMonthlyRecurrenceControl.Periodicity = info.Periodicity;
        this.edtMonthlyRecurrenceControl.DayNumber = info.DayNumber;
        this.edtMonthlyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Monthly);
        this.edtMonthlyRecurrenceControl.WeekOfMonth = info.WeekOfMonth;
    }
    void InitializeEdtYearlyRecurrenceControl(RecurrenceInfo info, RecurrenceInfo defaultInfo) {
        RecurrenceInfo actualInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Yearly);
        this.edtYearlyRecurrenceControl.ClientVisible = info == actualInfo;
        this.edtYearlyRecurrenceControl.DayNumber = info.DayNumber;
        this.edtYearlyRecurrenceControl.Month = info.Month;
        this.edtYearlyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Yearly);
        this.edtYearlyRecurrenceControl.WeekOfMonth = info.WeekOfMonth;
    }
    protected WeekDays CalcRecurrenceControlWeekDaysValue(RecurrenceInfo info, RecurrenceType controlType) {
        return (info.Type.Equals(controlType)) ? info.WeekDays : GetValidWeekDays(controlType, info.Start.DayOfWeek);
    }
    protected WeekDays GetValidWeekDays(RecurrenceType type, DayOfWeek dayOfWeek) {
        return (type.Equals(RecurrenceType.Daily)) ? WeekDays.EveryDay : DateTimeHelper.ToWeekDays(dayOfWeek);
    }

    public bool AssignControllerValues(AppointmentFormController controller, DateTime clientStart) {
        bool isValid = IsRecurrenceValid();
        if(isValid)
            ApplyRecurrence(controller, clientStart);
        else
            controller.RemoveRecurrence();
        return isValid;
    }
    bool IsRecurrenceValid() {
        DevExpress.XtraScheduler.UI.ValidationArgs args = new DevExpress.XtraScheduler.UI.ValidationArgs();
        RecurrenceRuleControlBase recurrenceRuleControl = GetCurrentRecurrenceRuleControl();
        recurrenceRuleControl.ValidateValues(args);
        if(args.Valid)
            edtRecurrenceRangeControl.ValidateValues(args);
        return args.Valid;
    }
    void ApplyRecurrence(AppointmentFormController controller, DateTime clientStart) {
        Appointment patternCopy = controller.PrepareToRecurrenceEdit();
        AssignRecurrenceInfoProperties(controller, patternCopy, (RecurrenceInfo)patternCopy.RecurrenceInfo, clientStart);
        controller.ApplyRecurrence(patternCopy);
    }
    protected virtual void AssignRecurrenceInfoProperties(AppointmentFormController controller, Appointment patternCopy, RecurrenceInfo rinfo, DateTime clientStart) {
        rinfo.Type = edtRecurrenceTypeEdit.Type;
        controller.AssignRecurrenceInfoRangeProperties(rinfo, edtRecurrenceRangeControl.ClientRange, clientStart, edtRecurrenceRangeControl.ClientEnd, edtRecurrenceRangeControl.ClientOccurrenceCount, patternCopy);
        RecurrenceRuleControlBase ruleControl = GetCurrentRecurrenceRuleControl();
        RecurrenceRuleValuesAccessor valueAccessor = ruleControl.ValuesAccessor;
        rinfo.DayNumber = valueAccessor.GetDayNumber();
        rinfo.Periodicity = valueAccessor.GetPeriodicity();
        rinfo.Month = valueAccessor.GetMonth();
        rinfo.WeekDays = valueAccessor.GetWeekDays();
        rinfo.WeekOfMonth = valueAccessor.GetWeekOfMonth();
    }
    protected RecurrenceRuleControlBase GetCurrentRecurrenceRuleControl() {
        RecurrenceType type = edtRecurrenceTypeEdit.Type;
        switch(type) {
            case RecurrenceType.Weekly:
                return edtWeeklyRecurrenceControl;
            case RecurrenceType.Monthly:
                return edtMonthlyRecurrenceControl;
            case RecurrenceType.Yearly:
                return edtYearlyRecurrenceControl;
            case RecurrenceType.Daily:
            default:
                return edtDailyRecurrenceControl;
        }
    }
}
