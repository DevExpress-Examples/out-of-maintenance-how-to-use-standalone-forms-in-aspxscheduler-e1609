Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxClasses
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native

Partial Public Class Forms_RecurrenceControl
	Inherits ASPxSchedulerClientFormBase
	Private pattern_Renamed As Appointment

	Public Property Pattern() As Appointment
		Get
			Return pattern_Renamed
		End Get
		Set(ByVal value As Appointment)
			pattern_Renamed = value
		End Set
	End Property
	Public Overrides ReadOnly Property ClassName() As String
		Get
			Return "ASPxClientRecurrenceAppointmentForm"
		End Get
	End Property

	Public Sub SetClientVisible(ByVal visible As Boolean)
		If visible Then
			mainDiv.Style.Add(HtmlTextWriterStyle.Display, "")
		Else
			mainDiv.Style.Add(HtmlTextWriterStyle.Display, "none")
		End If
	End Sub
	Protected Overrides Function GetChildControls() As Control()
		Dim controls() As Control = { edtDailyRecurrenceControl, edtWeeklyRecurrenceControl, edtMonthlyRecurrenceControl, edtYearlyRecurrenceControl, edtRecurrenceTypeEdit, edtRecurrenceRangeControl, mainDiv}
		Return controls
	End Function
	Public Overrides Sub DataBind()
		MyBase.DataBind()
		Dim recurrenceInfo As RecurrenceInfo
		If (Pattern Is Nothing) Then
			recurrenceInfo = New RecurrenceInfo(DateTime.Today)
		Else
			recurrenceInfo = Pattern.RecurrenceInfo
		End If

		edtRecurrenceTypeEdit.Type = recurrenceInfo.Type
		edtRecurrenceRangeControl.Range = recurrenceInfo.Range
		edtRecurrenceRangeControl.End = recurrenceInfo.End
		edtRecurrenceRangeControl.OccurrenceCount = recurrenceInfo.OccurrenceCount
		InitializeRecurrenceRuleConrtol(recurrenceInfo)
	End Sub
	Private Sub InitializeRecurrenceRuleConrtol(ByVal info As RecurrenceInfo)
		Dim defaultInfo As New RecurrenceInfo(info.Start)
		InitializeEdtDailyRecurrenceControl(info, defaultInfo)
		InitializeEdtWeeklyRecurrenceControl(info, defaultInfo)
		InitializeEdtMonthlyRecurrenceControl(info, defaultInfo)
		InitializeEdtYearlyRecurrenceControl(info, defaultInfo)
	End Sub
	Private Sub InitializeEdtDailyRecurrenceControl(ByVal info As RecurrenceInfo, ByVal defaultInfo As RecurrenceInfo)
		Dim actualInfo As RecurrenceInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Daily)
		Me.edtDailyRecurrenceControl.ClientVisible = actualInfo Is info
		Me.edtDailyRecurrenceControl.Periodicity = actualInfo.Periodicity
		Me.edtDailyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Daily)
	End Sub
	Private Function GetActualInfo(ByVal info As RecurrenceInfo, ByVal defaultInfo As RecurrenceInfo, ByVal type As RecurrenceType) As RecurrenceInfo
		Dim isActive As Boolean = info.Type.Equals(type)
		If (isActive) Then
			Return info
		Else
			Return defaultInfo
		End If
	End Function
	Private Sub InitializeEdtWeeklyRecurrenceControl(ByVal info As RecurrenceInfo, ByVal defaultInfo As RecurrenceInfo)
		Dim actualInfo As RecurrenceInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Weekly)
		Me.edtWeeklyRecurrenceControl.ClientVisible = actualInfo Is info
		Me.edtWeeklyRecurrenceControl.Periodicity = actualInfo.Periodicity
		Me.edtWeeklyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Weekly)
	End Sub
	Private Sub InitializeEdtMonthlyRecurrenceControl(ByVal info As RecurrenceInfo, ByVal defaultInfo As RecurrenceInfo)
		Dim actualInfo As RecurrenceInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Monthly)
		Me.edtMonthlyRecurrenceControl.ClientVisible = actualInfo Is info
		Me.edtMonthlyRecurrenceControl.Periodicity = info.Periodicity
		Me.edtMonthlyRecurrenceControl.DayNumber = info.DayNumber
		Me.edtMonthlyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Monthly)
		Me.edtMonthlyRecurrenceControl.WeekOfMonth = info.WeekOfMonth
	End Sub
	Private Sub InitializeEdtYearlyRecurrenceControl(ByVal info As RecurrenceInfo, ByVal defaultInfo As RecurrenceInfo)
		Dim actualInfo As RecurrenceInfo = GetActualInfo(info, defaultInfo, RecurrenceType.Yearly)
		Me.edtYearlyRecurrenceControl.ClientVisible = info Is actualInfo
		Me.edtYearlyRecurrenceControl.DayNumber = info.DayNumber
		Me.edtYearlyRecurrenceControl.Month = info.Month
		Me.edtYearlyRecurrenceControl.WeekDays = CalcRecurrenceControlWeekDaysValue(actualInfo, RecurrenceType.Yearly)
		Me.edtYearlyRecurrenceControl.WeekOfMonth = info.WeekOfMonth
	End Sub
	Protected Function CalcRecurrenceControlWeekDaysValue(ByVal info As RecurrenceInfo, ByVal controlType As RecurrenceType) As WeekDays
		If (info.Type.Equals(controlType)) Then
			Return info.WeekDays
		Else
			Return GetValidWeekDays(controlType, info.Start.DayOfWeek)
		End If
	End Function
	Protected Function GetValidWeekDays(ByVal type As RecurrenceType, ByVal dayOfWeek As DayOfWeek) As WeekDays
		If (type.Equals(RecurrenceType.Daily)) Then
			Return WeekDays.EveryDay
		Else
			Return DateTimeHelper.ToWeekDays(dayOfWeek)
		End If
	End Function

	Public Function AssignControllerValues(ByVal controller As AppointmentFormController, ByVal clientStart As DateTime) As Boolean
		Dim isValid As Boolean = IsRecurrenceValid()
		If isValid Then
			ApplyRecurrence(controller, clientStart)
		Else
			controller.RemoveRecurrence()
		End If
		Return isValid
	End Function
	Private Function IsRecurrenceValid() As Boolean
		Dim args As New DevExpress.XtraScheduler.UI.ValidationArgs()
		Dim recurrenceRuleControl As RecurrenceRuleControlBase = GetCurrentRecurrenceRuleControl()
		recurrenceRuleControl.ValidateValues(args)
		If args.Valid Then
			edtRecurrenceRangeControl.ValidateValues(args)
		End If
		Return args.Valid
	End Function
	Private Sub ApplyRecurrence(ByVal controller As AppointmentFormController, ByVal clientStart As DateTime)
		Dim patternCopy As Appointment = controller.PrepareToRecurrenceEdit()
		AssignRecurrenceInfoProperties(controller, patternCopy, patternCopy.RecurrenceInfo, clientStart)
		controller.ApplyRecurrence(patternCopy)
	End Sub
	Protected Overridable Sub AssignRecurrenceInfoProperties(ByVal controller As AppointmentFormController, ByVal patternCopy As Appointment, ByVal rinfo As RecurrenceInfo, ByVal clientStart As DateTime)
		rinfo.Type = edtRecurrenceTypeEdit.Type
		controller.AssignRecurrenceInfoRangeProperties(rinfo, edtRecurrenceRangeControl.ClientRange, clientStart, edtRecurrenceRangeControl.ClientEnd, edtRecurrenceRangeControl.ClientOccurrenceCount, patternCopy)
		Dim ruleControl As RecurrenceRuleControlBase = GetCurrentRecurrenceRuleControl()
		Dim valueAccessor As RecurrenceRuleValuesAccessor = ruleControl.ValuesAccessor
		rinfo.DayNumber = valueAccessor.GetDayNumber()
		rinfo.Periodicity = valueAccessor.GetPeriodicity()
		rinfo.Month = valueAccessor.GetMonth()
		rinfo.WeekDays = valueAccessor.GetWeekDays()
		rinfo.WeekOfMonth = valueAccessor.GetWeekOfMonth()
	End Sub
	Protected Function GetCurrentRecurrenceRuleControl() As RecurrenceRuleControlBase
		Dim type As RecurrenceType = edtRecurrenceTypeEdit.Type
		Select Case type
			Case RecurrenceType.Weekly
				Return edtWeeklyRecurrenceControl
			Case RecurrenceType.Monthly
				Return edtMonthlyRecurrenceControl
			Case RecurrenceType.Yearly
				Return edtYearlyRecurrenceControl
			Case Else
				Return edtDailyRecurrenceControl
		End Select
	End Function
End Class
