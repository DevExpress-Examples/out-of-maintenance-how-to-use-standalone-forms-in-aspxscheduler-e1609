Imports DevExpress.Web.ASPxScheduler.Internal
Imports System
Public Delegate Sub CreateControllerEventHandler(ByVal sender As Object, ByVal args As CreateControllerEventArgs)
Public Class CreateControllerEventArgs
    Inherits EventArgs


    Private controller_Renamed As AppointmentFormController

    Public Property Controller() As AppointmentFormController
        Get
            Return controller_Renamed
        End Get
        Set(ByVal value As AppointmentFormController)
            controller_Renamed = value
        End Set
    End Property
End Class
