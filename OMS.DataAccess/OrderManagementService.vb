Imports OMS.Business

Public Class OrderManagementService
    Private ReadOnly _dataAccessObject As New OrderManagementDataAccess()

#Region "Get Methods"
    Public Function GetOrderDetails() As DataTable
        ' Call the DAL method to get the order details
        Return _dataAccessObject.GetOrderDetails()
    End Function

    Public Function GetVendors() As DataTable
        ' Call the DAL method to get the vendors
        Return _dataAccessObject.GetVendors()
    End Function

    Public Function GetOrderSummary(orderID As Integer) As DataTable
        ' Call the DAL method to get the order summary
        Return _dataAccessObject.GetOrderSummary(orderID)
    End Function
#End Region

#Region "Add Methods"
    Public Sub AddOrderDetails(order As Order)
        ' Call the DAL method to add the order details
        _dataAccessObject.AddUpdateOrderDetails(order)
    End Sub
#End Region

#Region "Delete Methods"
    Public Sub DeleteOrder(orderID As Integer)
        ' Call the DAL method to delete the order
        _dataAccessObject.DeleteOrder(orderID)
    End Sub

    Public Sub DeleteSelectedOrders(orderIdList As List(Of Integer))
        ' Join the order IDs as a comma-separated string and call the DAL method
        Dim orderIds As String = String.Join(",", orderIdList)
        _dataAccessObject.DeleteSelectedOrders(orderIds)
    End Sub
#End Region
End Class
