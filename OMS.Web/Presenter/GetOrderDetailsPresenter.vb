Imports OMS.Business

Public Class GetOrderDetailsPresenter

#Region "Private Variables"
    Private ReadOnly view As IGetOrderDetailsView
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
    Public Sub New(view As IGetOrderDetailsView)
        Me.view = view
        Me.adapter = New ServiceReference1.OrderDetailsAdapter()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Get the order details from the database and display them in the view
    ''' </summary>
    Public Sub GetOrderDetails()
        Try
            Dim orderList = adapter.GetOrderDetails()
            view.DisplayOrderDetails(New OrderModelMapper().MapOrderModel(orderList))
        Catch ex As Exception
            _errors.Add("Unable to Get Order Details: " & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Delete an order from the database
    ''' </summary>
    ''' <param name="orderId"></param>
    Public Sub DeleteOrder(orderId As Integer)
        Try
            adapter.DeleteOrder(orderId)
        Catch ex As Exception
            ' Add an error message to the list if deletion fails
            _errors.Add("Error deleting order with ID " & orderId & ": " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Delete a list of orders from the database
    ''' This executes on Delete Selected button click
    ''' </summary>
    ''' <param name="orderIdList"></param>
    Public Sub DeleteSelectedOrders(orderIdList As List(Of Integer))
        Try
            adapter.DeleteSelectedOrders(orderIdList)
        Catch ex As Exception
            ' Add an error message for each order ID in the list that failed to delete
            For Each orderId In orderIdList
                _errors.Add("Error deleting order with ID " & orderId & ": " & ex.Message)
            Next
        End Try
    End Sub

#End Region
End Class
