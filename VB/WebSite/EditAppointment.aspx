<%@ Page Language="vb" AutoEventWireup="true" CodeFile="EditAppointment.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Forms/MyAppointmentForm.ascx" TagName="AppointmentForm" TagPrefix="forms" %>
<%@ Register Src="~/SchedulerAsDataSource.ascx" TagName="SchedulerDataSource" TagPrefix="forms" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page3</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:300px">
        <forms:AppointmentForm runat="server" ID="appointmentForm" ClientInstanceName="appointmentForm"
            OnFormClosed="OnFormClosed"/>        
        <forms:SchedulerDataSource runat="server" ID="schedulerDataSource" />
    </div>
    </form>
</body>
</html>