Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxClasses.Internal
Imports System.Text
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal

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
