Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports DevExpress.Web.ASPxClasses.Internal
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxClasses

Partial Public Class Forms_AppointmentForm
	Inherits ASPxSchedulerClientFormBase
	#Region "Fields"
	Private labelDataSource_Renamed As IEnumerable
	Private statusDataSource_Renamed As IEnumerable
	Private resourceDataSource_Renamed As IEnumerable
	Private scheduler_Renamed As ASPxScheduler
	Private appointment_Renamed As Appointment
	#End Region

	#Region "Properties"
	Public Overrides ReadOnly Property ClassName() As String
		Get
			Return "ASPxClientAppointmentForm"
		End Get
	End Property
	Public Property Scheduler() As ASPxScheduler
		Get
			If scheduler_Renamed Is Nothing Then
				Me.scheduler_Renamed = TryCast(FindControlById(SchedulerId), ASPxScheduler)
			End If
			Return scheduler_Renamed
		End Get
		Set(ByVal value As ASPxScheduler)
			scheduler_Renamed = value
		End Set
	End Property
	Protected ReadOnly Property LabelDataSource() As IEnumerable
		Get
			If labelDataSource_Renamed Is Nothing Then
				Me.labelDataSource_Renamed = ASPxSchedulerFormDataHelper.CreateLabelDataSource(Scheduler)
			End If
			Return labelDataSource_Renamed
		End Get
	End Property
	Protected ReadOnly Property StatusDataSource() As IEnumerable
		Get
			If statusDataSource_Renamed Is Nothing Then
				Me.statusDataSource_Renamed = ASPxSchedulerFormDataHelper.CreateStatusesDataSource(Scheduler)
			End If
			Return statusDataSource_Renamed
		End Get
	End Property
	Protected ReadOnly Property ResourceDataSource() As IEnumerable
		Get
			If resourceDataSource_Renamed Is Nothing Then
				Me.resourceDataSource_Renamed = ASPxSchedulerFormDataHelper.CreateResourceDataSource(Scheduler)
			End If
			Return resourceDataSource_Renamed
		End Get
	End Property
	Public Property Appointment() As Appointment
		Get
			Return appointment_Renamed
		End Get
		Set(ByVal value As Appointment)
			appointment_Renamed = value
		End Set
	End Property
	#End Region

	#Region "Events"
	Private Shared ReadOnly onFormClosed As Object = New Object()
	Public Custom Event FormClosed As EventHandler
		AddHandler(ByVal value As EventHandler)
			Events.AddHandler(onFormClosed, value)
		End AddHandler
		RemoveHandler(ByVal value As EventHandler)
			Events.RemoveHandler(onFormClosed, value)
		End RemoveHandler
		RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
		End RaiseEvent
	End Event
	Protected Sub RaiseFormClosed()
		Dim handler As EventHandler = TryCast(Events(onFormClosed), EventHandler)
		If handler Is Nothing Then
			Return
		End If
		handler(Me, New EventArgs())
	End Sub
	#End Region

	Protected Overrides Function GetChildControls() As Control()
		Dim controls() As Control = { edtStartDate, edtEndDate, tbSubject, tbDescription, tbLocation, edtLabel, edtStatus, chkAllDay, chkRecurrence, edtResource, recurrenceControl, btnOk, btnCancel, btnDelete}
		Return controls
	End Function
	Public Overrides Sub DataBind()
		MyBase.DataBind()
		If Appointment Is Nothing Then
			Return
		End If
		Dim objectAppointmentId As Object = Scheduler.GetAppointmentClientId(appointment_Renamed)
		If (objectAppointmentId Is Nothing) Then
			appointmentId.Value = String.Empty
		Else
			appointmentId.Value = objectAppointmentId.ToString()
		End If
		edtStartDate.Value = Appointment.Start
		edtEndDate.Value = Appointment.End
		tbSubject.Value = Appointment.Subject
		tbLocation.Value = Appointment.Location
		edtLabel.Value = Appointment.LabelId.ToString()
		edtStatus.Value = Appointment.StatusId.ToString()
		If (Appointment.ResourceId Is Resource.Empty) Then
			edtResource.Value = "null"
		Else
			edtResource.Value = Appointment.ResourceId.ToString()
		End If
		tbDescription.Value = Appointment.Description
		chkAllDay.Checked = Appointment.AllDay
		If Appointment.Type.Equals(AppointmentType.Occurrence) OrElse Appointment.Type.Equals(AppointmentType.ChangedOccurrence) Then
			chkRecurrence.Visible = False
			recurrenceControl.Visible = False
		Else
			chkRecurrence.Checked = Appointment.IsRecurring
			recurrenceControl.SetClientVisible(Appointment.IsRecurring)
			If (Appointment.Type.Equals(AppointmentType.Pattern)) Then
				recurrenceControl.Pattern = Appointment
			Else
				recurrenceControl.Pattern = Nothing
			End If
			recurrenceControl.DataBind()
		End If
		btnDelete.ClientEnabled = Not Scheduler.Storage.Appointments.IsNewAppointment(Appointment)
	End Sub
	Protected Sub OnBtnDeleteClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim apt As Appointment = Scheduler.LookupAppointmentByIdString(appointmentId.Value)
		If apt Is Nothing Then
			Return
		End If
		apt.Delete()
		RaiseFormClosed()
	End Sub
	Protected Sub OnBtnCancelClick(ByVal sender As Object, ByVal e As EventArgs)
		RaiseFormClosed()
	End Sub
	Protected Sub OnBtnOkClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim apt As Appointment
		If (String.IsNullOrEmpty(appointmentId.Value)) Then
			apt = Scheduler.Storage.CreateAppointment(AppointmentType.Normal)
		Else
			apt = Scheduler.LookupAppointmentByIdString(appointmentId.Value)
		End If
		Dim formController As New AppointmentFormController(Scheduler, apt)
		If formController Is Nothing Then
			Return
		End If
		AssignControllerValues(formController)
		formController.ApplyChanges()
		RaiseFormClosed()
	End Sub
	Public Sub AssignControllerValues(ByVal controller As AppointmentFormController)
		Dim helper As New TimeZoneHelper(Scheduler.OptionsBehavior.ClientTimeZoneId)
		controller.Start = helper.FromClientTime(edtStartDate.Date)
		controller.End = helper.FromClientTime(edtEndDate.Date)
		controller.Subject = tbSubject.Text
		controller.Location = tbLocation.Text
		controller.Description = tbDescription.Text
		controller.AllDay = chkAllDay.Checked
		controller.StatusId = Convert.ToInt32(edtStatus.Value)
		controller.LabelId = Convert.ToInt32(edtLabel.Value)
		If (edtResource.Value.ToString() <> "null") Then
			controller.ResourceId = Convert.ToInt32(edtResource.Value)
		Else
			controller.ResourceId = Resource.Empty.Id
		End If
		If chkRecurrence.Checked Then
			recurrenceControl.AssignControllerValues(controller, edtStartDate.Date)
		End If
	End Sub
End Class
