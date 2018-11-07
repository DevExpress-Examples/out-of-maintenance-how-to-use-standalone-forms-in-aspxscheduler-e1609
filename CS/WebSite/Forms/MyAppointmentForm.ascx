<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyAppointmentForm.ascx.cs"
    Inherits="Forms_AppointmentForm" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2" Namespace="DevExpress.Web.ASPxScheduler.Controls"
    TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="my" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Src="~/Forms/MyRecurrenceForm.ascx" TagName="RecurrenceControl" TagPrefix="recur" %>

<link runat="server" rel="Stylesheet" href="/WebSite/css/form.css" type="text/css" />
<asp:HiddenField runat="server" ID="appointmentId" Value="null" />
<table class="myAppointmentForm" cellpadding="0" cellspacing="0" style="width: 500px;
    height: 100px;">
    <tr>
        <td class="myDoubleCell" colspan="2">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell">
                        <dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject" Text="Subject:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxTextBox ID="tbSubject" runat="server" Width="100%" EnableClientSideAPI="true"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="mySingleCell">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell">
                        <dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation" Text="Location:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxTextBox ID="tbLocation" runat="server" Width="100%" EnableClientSideAPI="true"/>
                    </td>
                </tr>
            </table>
        </td>
        <td class="mySingleCell">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" Text="Label:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxComboBox ID="edtLabel" runat="server" Width="100%" DataSource='<%# LabelDataSource %>' />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="mySingleCell">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell">
                        <dxe:ASPxLabel ID="lblStartDate" runat="server" AssociatedControlID="edtStartDate"
                            Text="Start time:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxDateEdit ID="edtStartDate" runat="server"
                            Width="100%" EditFormat="DateTime" />
                    </td>
                </tr>
            </table>
        </td>
        <td class="mySingleCell">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel runat="server" ID="lblEndDate" Text="End time:" AssociatedControlID="edtEndDate" />
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxDateEdit ID="edtEndDate" runat="server" EditFormat="DateTime" Width="100%">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="mySingleCell">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell">
                        <dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" Text="Show time as:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxComboBox ID="edtStatus" runat="server" Width="100%" DataSource='<%# StatusDataSource %>' />
                    </td>
                </tr>
            </table>
        </td>
        <td class="mySingleCell" style="padding-left: 22px;">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 20px; height: 20px;">
                        <dxe:ASPxCheckBox ID="chkAllDay" runat="server" />
                    </td>
                    <td style="padding-left: 2px;">
                        <dxe:ASPxLabel ID="lblAllDay" runat="server" Text="All day event" AssociatedControlID="chkAllDay" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="mySingleCell" colspan="2">
            <table class="myLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="myLabelCell">
                        <dxe:ASPxLabel ID="lblResource" runat="server" AssociatedControlID="edtResource" Text="Resource:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="myControlCell">
                        <dxe:ASPxComboBox ID="edtResource" runat="server" Width="100%" DataSource='<%# ResourceDataSource %>'/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="myDoubleCell" colspan="2" style="height: 90px;">
            <dxe:ASPxMemo ID="tbDescription" runat="server" Width="100%" Rows="6" EnableClientSideAPI="true" />
        </td>
    </tr>
</table>
<dxe:ASPxCheckBox id="chkRecurrence" runat="server" Text="Recurrence">
</dxe:ASPxCheckBox>

<recur:RecurrenceControl runat="server" ID="recurrenceControl">
</recur:RecurrenceControl>

<table cellpadding="0" cellspacing="0" style="width: 500px; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;" align="center">
            <table style="height: 100%;">
                <tr>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnOk" Text="OK" UseSubmitBehavior="false"
                            AutoPostBack="true" EnableViewState="false" Width="91px" OnClick="OnBtnOkClick" />
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnCancel" Text="Cancel"
                            UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" Width="91px"
                            CausesValidation="False" onClick="OnBtnCancelClick" />
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnDelete" Text="Delete"
                            UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" Width="91px"
                            OnClick="OnBtnDeleteClick" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script id="dxss_ASPxSchedulerClientAppoinmentForm" type="text/javascript"><!--
    ASPxClientAppointmentForm = _aspxCreateClass(ASPxClientFormBase, {
        Initialize: function() {
            if (this.controls.chkRecurrence)
                this.controls.chkRecurrence.CheckedChanged.AddHandler(_aspxCreateDelegate(this.OnChkRecurrenceChanged, this));
        },
        OnChkRecurrenceChanged: function(s, e) {
            var isChecked = s.GetChecked();
            if(this.controls.recurrenceControl) 
                this.controls.recurrenceControl.SetVisible(isChecked);
        }
    });
//--></script>
