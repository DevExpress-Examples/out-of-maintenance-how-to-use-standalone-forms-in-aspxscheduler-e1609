<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="DevExpress.Web.v10.1" Namespace="DevExpress.Web.ASPxGlobalEvents"
    TagPrefix="dxge" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1" Namespace="DevExpress.Web.ASPxEditors"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1" Namespace="DevExpress.Web.ASPxTabControl"
    TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v10.1" Namespace="DevExpress.Web.ASPxClasses"
    TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1" Namespace="DevExpress.Web.ASPxScheduler"
    TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v10.1.Core, Version=10.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="dxschsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1" Namespace="DevExpress.Web.ASPxScheduler.Controls"
    TagPrefix="dxwschsc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page2</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;</div>
        <div>
            <asp:AccessDataSource ID="AppointmentDataSource" runat="server" DataFile="~/App_Data/CarsDB.mdb"
                DeleteCommand="DELETE FROM [CarScheduling] WHERE [ID] = ?" InsertCommand="INSERT INTO [CarScheduling] ([CarId], [Status], [Subject], [Description], [Label], [StartTime], [EndTime], [Location], [AllDay], [EventType], [RecurrenceInfo], [ReminderInfo]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                SelectCommand="SELECT [ID], [CarId], [Status], [Subject], [Description], [Label], [StartTime], [EndTime], [Location], [AllDay], [EventType], [RecurrenceInfo], [ReminderInfo] FROM [CarScheduling]"
                UpdateCommand="UPDATE [CarScheduling] SET [CarId] = ?, [Status] = ?, [Subject] = ?, [Description] = ?, [Label] = ?, [StartTime] = ?, [EndTime] = ?, [Location] = ?, [AllDay] = ?, [EventType] = ?, [RecurrenceInfo] = ?, [ReminderInfo] = ? WHERE [ID] = ?"
                OnInserted="AppointmentsDataSource_Inserted">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CarId" Type="Int32" />
                    <asp:Parameter Name="Status" Type="Int32" />
                    <asp:Parameter Name="Subject" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Label" Type="Int32" />
                    <asp:Parameter Name="StartTime" Type="DateTime" />
                    <asp:Parameter Name="EndTime" Type="DateTime" />
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="AllDay" Type="Boolean" />
                    <asp:Parameter Name="EventType" Type="Int32" />
                    <asp:Parameter Name="RecurrenceInfo" Type="String" />
                    <asp:Parameter Name="ReminderInfo" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CarId" Type="Int32" />
                    <asp:Parameter Name="Status" Type="Int32" />
                    <asp:Parameter Name="Subject" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Label" Type="Int32" />
                    <asp:Parameter Name="StartTime" Type="DateTime" />
                    <asp:Parameter Name="EndTime" Type="DateTime" />
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="AllDay" Type="Boolean" />
                    <asp:Parameter Name="EventType" Type="Int32" />
                    <asp:Parameter Name="RecurrenceInfo" Type="String" />
                    <asp:Parameter Name="ReminderInfo" Type="String" />
                </InsertParameters>
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="ResourceDataSource" runat="server" DataFile="~/App_Data/CarsDB.mdb"
                SelectCommand="SELECT [ID], [Model] FROM [Cars] WHERE ID < 6"></asp:AccessDataSource>
            <asp:AccessDataSource ID="StatusDataSource" runat="server" DataFile="~/App_Data/CarsDB.mdb"
                SelectCommand="SELECT [Name], [Color] FROM [UsageType]"></asp:AccessDataSource>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ClientInstanceName="scheduler"
                GroupType="Resource" ResourceDataSourceID="resourceDataSource" AppointmentDataSourceID="appointmentDataSource"
                OnAppointmentRowInserting="ASPxScheduler1_AppointmentRowInserting" OnAppointmentRowInserted="ASPxScheduler1_AppointmentRowInserted"
                OnAppointmentsInserted="ASPxScheduler1_OnAppointmentsInserted">
                <ClientSideEvents MenuItemClicked="function(s, e) { OnMenuItemClick(s,e); }" />
                <Storage EnableReminders="False">
                    <Appointments>
                        <Mappings AppointmentId="ID" End="EndTime" Start="StartTime" Subject="Subject" Description="Description"
                            Location="Location" AllDay="AllDay" Type="EventType" RecurrenceInfo="RecurrenceInfo"
                            ReminderInfo="ReminderInfo" Label="Label" Status="Status" ResourceId="CarId" />
                    </Appointments>
                    <Resources>
                        <Mappings ResourceId="ID" Caption="Model" />
                    </Resources>
                </Storage>
                <Views>
                    <DayView>
                        <TimeRulers>
                            <dxschsc:TimeRuler>
                            </dxschsc:TimeRuler>
                        </TimeRulers>
                    </DayView>
                    <WorkWeekView>
                        <TimeRulers>
                            <dxschsc:TimeRuler>
                            </dxschsc:TimeRuler>
                        </TimeRulers>
                    </WorkWeekView>
                </Views>
            </dxwschs:ASPxScheduler>
        </div>

        <script type="text/javascript">
    function OnMenuItemClick(s, e) {
        e.handled = true;
        if (e.itemName == SchedulerMenuItemId.NewAppointment) {
            var interval = scheduler.GetSelectedInterval();
            var resourceId = scheduler.GetSelectedResource();
            var start = _aspxDateTimeToMilliseconds(interval.start);
            var end = _aspxDateTimeToMilliseconds(interval.end);
            window.location.href = "EditAppointment.aspx?start=" + start + "&end=" + end + "&resourceId=" + resourceId;
        } else if (e.itemName == SchedulerMenuItemId.NewAllDayEvent) {
            var resourceId = scheduler.GetSelectedResource();
            var start = scheduler.GetSelectedInterval().start;
            var today = new Date(start.getFullYear(), start.getMonth(), start.getDate());
            var todayInMeliseconds = _aspxDateTimeToMilliseconds(today);
            window.location.href = "EditAppointment.aspx?start=" + todayInMeliseconds + "&resourceId=" + resourceId + "&allDay";
        } else if (e.itemName == SchedulerMenuItemId.NewRecurringAppointment) {
            var interval = scheduler.GetSelectedInterval();
            var resourceId = scheduler.GetSelectedResource();
            var start = _aspxDateTimeToMilliseconds(interval.start);
            var end = _aspxDateTimeToMilliseconds(interval.end);
            window.location.href = "EditAppointment.aspx?start=" + start + "&end=" + end + "&resourceId=" + resourceId + "&isRecurring=true";
        } else if (e.itemName == SchedulerMenuItemId.NewRecurringEvent) {
            var resourceId = scheduler.GetSelectedResource();
            var start = scheduler.GetSelectedInterval().start;
            var today = new Date(start.getFullYear(), start.getMonth(), start.getDate());
            var todayInMeliseconds = _aspxDateTimeToMilliseconds(today);
            window.location.href = "EditAppointment.aspx?start=" + todayInMeliseconds + "&resourceId=" + resourceId + "&allDay=true&isRecurring=true";
        } else if (e.itemName == SchedulerMenuItemId.OpenAppointment) {
            var apt = GetSelectedAppointment(scheduler);
            window.location.href = "EditAppointment.aspx?id=" + apt.appointmentId;
        } else if (e.itemName == SchedulerMenuItemId.EditSeries) {
            var apt = GetSelectedAppointment(scheduler);
            window.location.href = "EditAppointment.aspx?id=" + apt.appointmentId + "&isRecurring=true";
        }
        else
            e.handled = false;
        
    }
    function CreateAppointment(scheduler) {
        var apt = new ASPxClientAppointment();
        apt.interval = scheduler.GetSelectedInterval();
        apt.AddResource(scheduler.GetSelectedResource());
        apt.labelIndex = 0;
        apt.statusIndex = 0;
        return apt;
    }
    function GetSelectedAppointment(scheduler) {
        var aptIds = scheduler.GetSelectedAppointmentIds();
        if (aptIds.length == 0)
            return;
        var apt = scheduler.GetAppointment(aptIds[0]);
        return apt;
    }
        </script>

    </form>
</body>
</html>
