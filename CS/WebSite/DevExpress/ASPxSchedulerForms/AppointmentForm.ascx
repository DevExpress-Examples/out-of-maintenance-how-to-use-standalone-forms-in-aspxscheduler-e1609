<%--
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the form             }
{   rendering. Improper modifications may result in incorrect behavior of            }
{   the appointment form.                                                            }
{                                                                                    }
{   In order to create and use your own custom template, perform the following       }
{   steps:                                                                           }
{       1. Save a copy of this file with a different name in another location.       }
{       2. Specify the file location as the 'OptionsForms.AppointmentFormTemplateUrl'}
{          property of the ASPxScheduler control.                                    }
{       3. If you need custom fields to be displayed and processed, you should       }
{          accomplish steps 4-9; otherwise, go to step 10.                           }
{       4. Create a class, derived from the AppointmentFormTemplateContainer,        }
{          containing custom properties. This class definition can be located        }
{          within a class file in the App_Code folder.                               }
{       5. Replace AppointmentFormTemplateContainer references in the template       }
{          page with the name of the class you've created in step 4.                 }
{       6. Handle the AppointmentFormShowing event to create an instance of the      }
{          template container class, defined in step 4, and specify it as the        }
{          destination container instead of the default one.                         }
{       7. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormController.          }
{          This class provides data exchange between the form and the appointment.   }
{          You should override ApplyCustomFieldsValues() method of the base class.   }
{       8. Define a class, which inherits from the                                   }
{          DevExpress.Web.ASPxScheduler.Internal.AppointmentFormSaveCallbackCommand. }
{          This class creates an instance of the AppointmentFormController inheritor }
{          (defined in step 7) via the CreateAppointmentFormController method and    }
{          overrides the AssignControllerValues method  of the base class to collect }
{          user data from the form's editors.                                        }
{       9. Handle the BeforeExecuteCallbackCommand event. The event handler code     }
{          should create an instance of the class defined in step 8, and specify it  }
{          as the destination command instead of the default one.                    }
{      10. Modify the overall appearance of the page and its layout.                 }
{                                                                                    }
{************************************************************************************}
--%>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="AppointmentForm" CodeFile="AppointmentForm.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.1, Version=10.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<table class="dxscAppointmentForm" cellpadding="0" cellspacing="0" style="width: 100%; height: 230px;">
    <tr>
        <td class="dxscDoubleCell" colspan="2">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="tbSubject" Text="Subject:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Subject %>' />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr> 
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblLocation" runat="server" AssociatedControlID="tbLocation" Text="Location:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbLocation" runat="server" Width="100%" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Location %>' />
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel ID="lblLabel" runat="server" AssociatedControlID="edtLabel" Text="Label:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtLabel" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).LabelDataSource %>' />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblStartDate" runat="server" AssociatedControlID="edtStartDate" Text="Start time:" Wrap="false">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxDateEdit ID="edtStartDate" runat="server" Width="100%" Date='<%# ((AppointmentFormTemplateContainer)Container).Start %>' EditFormat="DateTime" DateOnError="Undo" AllowNull="false" EnableClientSideAPI="true" >
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True" Display="Dynamic"
                                ValidationGroup="DateValidatoinGroup">
                            </ValidationSettings>
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 25px;">
                        <dxe:ASPxLabel runat="server" ID="lblEndDate" Text="End time:" Wrap="false" AssociatedControlID="edtEndDate"/>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxDateEdit id="edtEndDate" runat="server" Date='<%# ((AppointmentFormTemplateContainer)Container).End %>' EditFormat="DateTime" Width="100%" DateOnError="Undo" AllowNull="false" EnableClientSideAPI="true">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" EnableCustomValidation="True" Display="Dynamic"
                                ValidationGroup="DateValidatoinGroup">
                            </ValidationSettings>
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblStatus" runat="server" AssociatedControlID="edtStatus" Text="Show time as:" Wrap="false">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtStatus" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).StatusDataSource %>' />
                    </td>
                </tr>
            </table>
        </td>
        <td class="dxscSingleCell" style="padding-left: 22px;">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 20px; height: 20px;">
                        <dxe:ASPxCheckBox ClientInstanceName="_dx" ID="chkAllDay" runat="server" Checked='<%# ((AppointmentFormTemplateContainer)Container).Appointment.AllDay %>'>
                        </dxe:ASPxCheckBox>
                    </td>
                    <td style="padding-left: 2px;">
                        <dxe:ASPxLabel ID="lblAllDay" runat="server" Text="All day event" AssociatedControlID="chkAllDay" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
<% if(CanShowReminders) { %>
        <td class="dxscSingleCell">
<% } else { %>
        <td class="dxscDoubleCell" colspan="2">
<% } %>
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell">
                        <dxe:ASPxLabel ID="lblResource" runat="server" AssociatedControlID="edtResource" Text="Resource:">
                        </dxe:ASPxLabel>
                    </td>
                    <td class="dxscControlCell">
<% if(ResourceSharing) { %>
                        <dxe:ASPxDropDownEdit id="ddResource" runat="server" Width="100%" ClientInstanceName="ddResource" Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>' AllowUserInput="false">
                            <DropDownWindowTemplate>
                                <dxe:ASPxListBox id="edtMultiResource" runat="server" width="100%" SelectionMode="CheckColumn" DataSource='<%# ResourceDataSource %>' Border-BorderWidth="0">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                        var resourceNames = new Array();
                                        var items = s.GetSelectedItems();
                                        var count = items.length;
                                        if (count > 0) {
                                            for(var i=0; i<count; i++) 
                                                resourceNames.push(items[i].text);
                                        }
                                        else
                                            resourceNames.push(ddResource.cp_Caption_ResourceNone);
                                        ddResource.SetValue(resourceNames.join(', '));
                                    }"></ClientSideEvents>
                                </dxe:ASPxListBox>
                            </DropDownWindowTemplate>
                        </dxe:ASPxDropDownEdit>                        
<% } else { %>           
                        <dxe:ASPxComboBox ClientInstanceName="_dx" ID="edtResource" runat="server" Width="100%" DataSource='<%# ResourceDataSource %> ' Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanEditResource %>' />
<% } %>             
                    </td>

                </tr>
            </table>
        </td>
<% if(CanShowReminders) { %>
        <td class="dxscSingleCell">
            <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="dxscLabelCell" style="padding-left: 22px;">
                        <table class="dxscLabelControlPair" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 20px; height: 20px;">
                                    <dxe:ASPxCheckBox ID="chkReminder" runat="server"> 
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="padding-left: 2px;">
                                    <dxe:ASPxLabel ID="lblReminder" runat="server" Text="Reminder" AssociatedControlID="chkReminder" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="dxscControlCell" style="padding-left: 3px">
                        <dxe:ASPxComboBox  ID="cbReminder" runat="server" Width="100%" DataSource='<%# ((AppointmentFormTemplateContainer)Container).ReminderDataSource %>' />
                    </td>
                </tr>
            </table>
        </td>
<% } %>
    </tr>
    <tr>
        <td class="dxscDoubleCell" colspan="2" style="height: 90px;">
            <dxe:ASPxMemo ClientInstanceName="_dx" ID="tbDescription" runat="server" Width="100%" Rows="6" Text='<%# ((AppointmentFormTemplateContainer)Container).Appointment.Description %>' />
        </td>
    </tr>
</table>
                        
<dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
    IsRecurring='<%# ((AppointmentFormTemplateContainer)Container).Appointment.IsRecurring %>' 
    DayNumber='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceDayNumber %>' 
    End='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceEnd %>' 
    Month='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceMonth %>' 
    OccurrenceCount='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceOccurrenceCount %>' 
    Periodicity='<%# ((AppointmentFormTemplateContainer)Container).RecurrencePeriodicity %>' 
    RecurrenceRange='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceRange %>' 
    Start='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceStart %>' 
    WeekDays='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekDays %>' 
    WeekOfMonth='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceWeekOfMonth %>' 
    RecurrenceType='<%# ((AppointmentFormTemplateContainer)Container).RecurrenceType %>'
    IsFormRecreated='<%# ((AppointmentFormTemplateContainer)Container).IsFormRecreated %>' >
</dxsc:AppointmentRecurrenceForm>
                   
<table cellpadding="0" cellspacing="0" style="width: 100%; height: 35px;">
    <tr>
        <td style="width: 100%; height: 100%;" align="center">
            <table style="height: 100%;">
                <tr>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnOk" Text="OK" UseSubmitBehavior="false" AutoPostBack="false" 
                            EnableViewState="false" Width="91px" EnableClientSideAPI="true"/>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnCancel" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" 
                            Width="91px" CausesValidation="False" EnableClientSideAPI="true" />
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btnDelete" Text="Delete" UseSubmitBehavior="false"
                            AutoPostBack="false" EnableViewState="false" Width="91px"
                            Enabled='<%# ((AppointmentFormTemplateContainer)Container).CanDeleteAppointment %>'
                            CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 100%;">
    <tr>
        <td style="width: 100%;" align="left">
            <dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1" MasterControlId='<%# ((DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer)Container).ControlId %>' />
        </td>
    </tr>
</table>
<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">
    ASPxAppointmentForm = _aspxCreateClass(ASPxClientFormBase, {
        Initialize: function () {
            this.controls.edtStartDate.Validation.AddHandler(_aspxCreateDelegate(this.OnEdtStartDateValidate, this));
            this.controls.edtEndDate.Validation.AddHandler(_aspxCreateDelegate(this.OnEdtEndDateValidate, this));
            this.controls.edtStartDate.ValueChanged.AddHandler(_aspxCreateDelegate(this.OnUpdateStartEndValue, this));
            this.controls.edtEndDate.ValueChanged.AddHandler(_aspxCreateDelegate(this.OnUpdateStartEndValue, this));
            if (this.controls.chkReminder)
                this.controls.chkReminder.CheckedChanged.AddHandler(_aspxCreateDelegate(this.OnChkReminderCheckedChanged, this));
        },
        OnEdtStartDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate < endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnEdtEndDateValidate: function (s, e) {
            if (!e.isValid)
                return;
            var startDate = this.controls.edtStartDate.GetDate();
            var endDate = this.controls.edtEndDate.GetDate();
            e.isValid = startDate == null || endDate == null || startDate < endDate;
            e.errorText = "The Start Date must precede the End Date.";
        },
        OnUpdateStartEndValue: function (s, e) {
            var isValid = ASPxClientEdit.ValidateEditorsInContainer(null);
            this.controls.btnOk.SetEnabled(isValid);
        },
        OnChkReminderCheckedChanged: function (s, e) {
            var isReminderEnabled = this.controls.chkReminder.GetValue();
        if (isReminderEnabled)
                this.controls.cbReminder.SetSelectedIndex(3);
        else
                this.controls.cbReminder.SetSelectedIndex(-1);
            this.controls.cbReminder.SetEnabled(isReminderEnabled);
    }
    });
</script>