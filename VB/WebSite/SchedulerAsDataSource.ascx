<%@ Control Language="vb" AutoEventWireup="true" CodeFile="SchedulerAsDataSource.ascx.vb"
    Inherits="SchedulerAsDataSource" %>
<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web"
    TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web"
    TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2" Namespace="DevExpress.Web.ASPxScheduler"
    TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="dxschsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2" Namespace="DevExpress.Web.ASPxScheduler.Controls"
    TagPrefix="dxwschsc" %>
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
<dxwschs:aspxscheduler id="ASPxScheduler1" runat="server" clientinstancename="scheduler"
    grouptype="Resource" resourcedatasourceid="resourceDataSource" appointmentdatasourceid="appointmentDataSource"
    onappointmentrowinserting="ASPxScheduler1_AppointmentRowInserting" onappointmentrowinserted="ASPxScheduler1_AppointmentRowInserted"
    onappointmentsinserted="ASPxScheduler1_OnAppointmentsInserted" start="2009-05-15"
    visible="false">
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
        </dxwschs:aspxscheduler>