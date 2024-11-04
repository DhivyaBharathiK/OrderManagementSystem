Imports OMS.Business
Public Class AddUpdateOrderDetailsPresenter

#Region "Private Variables"
    Private ReadOnly view As IAddUpdateOrderDetails
    Private ReadOnly adapter As ServiceReference1.OrderDetailsAdapter
    Private _errors As New List(Of String)()
#End Region

#Region "Properties"
    Public ReadOnly Property Errors As List(Of String)
        Get
            Return _errors
        End Get
    End Property
    Public ReadOnly Property HasErrors As Boolean
        Get
            Return _errors.Count > 0
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New(view As IAddUpdateOrderDetails)
        Me.view = view
        Me.adapter = New ServiceReference1.OrderDetailsAdapter()
    End Sub
#End Region

#Region "Get Methods"
    ''' <summary>
    ''' Get vendors from the database
    ''' </summary>
    ''' <returns></returns>
    Public Function GetVendors() As List(Of Vendor)
        Try
            Dim vendorDetails = adapter.GetVendors()

            Return If(vendorDetails?.Any(), vendorDetails.Select(Function(vendor) New Vendor With {
            .VendorId = vendor.VendorId,
            .VendorName = vendor.VendorName
        }).ToList(), New List(Of Vendor)())

        Catch ex As Exception
            _errors.Add("Unable to get vendor details: " & ex.Message)
            Return New List(Of Vendor)()
        End Try
    End Function
    ''' <summary>
    ''' Get the order summary from the database
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function GetOrderSummary(orderId As Integer) As Order
        Try
            Return adapter.GetOrderSummary(orderId)
        Catch ex As Exception
            _errors.Add("Unable to get order summary: " & ex.Message)
            Return New Order()
        End Try
    End Function
#End Region

#Region "Add Methods"
    ''' <summary>
    ''' Add the order details to the database
    ''' </summary>
    ''' <param name="order"></param>
    Public Sub AddOrderDetails(order As Order)
        Try
            adapter.AddOrderDetails(order)

        Catch ex As Exception
            _errors.Add("Unable to add order details: " & ex.Message)
        End Try
    End Sub

#End Region
End Class
