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
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Imports System.Collections.Specialized
Imports System.Collections.Generic

Partial Public Class Default3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        schedulerDataSource.DataBind()
        Dim scheduler As ASPxScheduler = schedulerDataSource.Scheduler
        appointmentForm.Scheduler = scheduler
        If Not IsPostBack Then
            Dim apt As Appointment = ObtainAppointmentFromQueryString(scheduler, Request.QueryString)
            BindFormToAppointment(apt)
        End If
    End Sub
    Private Sub BindFormToAppointment(ByVal apt As Appointment)
        appointmentForm.Appointment = apt
        appointmentForm.DataBind()
    End Sub
    Private Function ObtainAppointmentFromQueryString(ByVal scheduler As ASPxScheduler, ByVal queryString As NameValueCollection) As Appointment
        Dim apt As Appointment = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
        Dim stringId As String = queryString("id")
        Dim stringStart As String = queryString("start")
        Dim stringEnd As String = queryString("end")
        Dim stringResourceId As String = queryString("resourceId")
        Dim stringIsAllDay As String = queryString("isAllDay")
        Dim stringIsRecurring As String = queryString("isRecurring")
        If Not String.IsNullOrEmpty(stringId) Then
            apt = scheduler.LookupAppointmentByIdString(stringId)
            If apt Is Nothing Then
                GoToMainPage()
            End If
            If Not String.IsNullOrEmpty(stringIsRecurring) Then
                apt = apt.RecurrencePattern
            End If
        ElseIf IsCreateRecurringAllDayEvent(queryString) Then
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Pattern)
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart)
            apt.Duration = TimeSpan.FromDays(1)
            apt.AllDay = True
        ElseIf IsCreateNewAllDayEvent(queryString) Then
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart)
            apt.Duration = TimeSpan.FromDays(1)
            apt.AllDay = True
        ElseIf IsCreateRecurringAppointment(queryString) Then
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Pattern)
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart)
            apt.End = SchedulerWebUtils.ToDateTime(stringEnd)
            apt.RecurrenceInfo.End = SchedulerWebUtils.ToDateTime(stringEnd)
        ElseIf IsCreateAppointment(queryString) Then
            apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Start = SchedulerWebUtils.ToDateTime(stringStart)
            apt.End = SchedulerWebUtils.ToDateTime(stringEnd)
        Else
            Dim nowTime As Date = Date.Now
            Dim now As New Date(nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute, nowTime.Second)
            apt.Start = now
            apt.Duration = TimeSpan.FromHours(3)
            stringResourceId = String.Empty
        End If
        apt.ResourceId = GetResourceId(stringResourceId)
        Return apt
    End Function

    Private Function GetResourceId(ByVal stringResourceId As String) As Object
        If String.IsNullOrEmpty(stringResourceId) Then
            Return ResourceEmpty.Id
        End If
        Return Integer.Parse(stringResourceId)
    End Function
    Private Function IsCreateNewAllDayEvent(ByVal queryString As NameValueCollection) As Boolean
        Dim parameters() As String = { "start", "allDay", "resourceId" }
        Return IsQueryStringContainAllParams(queryString, parameters)
    End Function
    Private Function IsCreateAppointment(ByVal queryString As NameValueCollection) As Boolean
        Dim parameters() As String = { "start", "end", "resourceId"}
        Return IsQueryStringContainAllParams(queryString, parameters)
    End Function
    Private Function IsCreateRecurringAppointment(ByVal queryString As NameValueCollection) As Boolean
        Dim parameters() As String = { "start", "end", "resourceId", "isRecurring"}
        Return IsQueryStringContainAllParams(queryString, parameters)
    End Function
    Private Function IsCreateRecurringAllDayEvent(ByVal queryString As NameValueCollection) As Boolean
        Dim parameters() As String = { "start", "resourceId", "isRecurring", "allDay" }
        Return IsQueryStringContainAllParams(queryString, parameters)
    End Function
    Private Function IsQueryStringContainAllParams(ByVal queryString As NameValueCollection, ByVal parameters() As String) As Boolean
        Dim count As Integer = parameters.Length
        For i As Integer = 0 To count - 1
            Dim paramName As String = parameters(i)
            If String.IsNullOrEmpty(queryString(paramName)) Then
                Return False
            End If
        Next i
        Return True
    End Function
    Protected Sub OnFormClosed(ByVal sender As Object, ByVal args As EventArgs)
        GoToMainPage()
    End Sub
    Private Sub GoToMainPage()
        Response.Redirect("default.aspx", True)
    End Sub
End Class
