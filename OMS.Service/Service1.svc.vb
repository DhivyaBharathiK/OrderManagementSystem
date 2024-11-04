Imports OMS.Business
Imports OMS.DataAccess

Public Class Service1
    Implements IService1

    Public Sub AddOrderDetails(order As Order) Implements IService1.AddOrderDetails
        Try
            Dim orderManagement As New OrderManagementService()
            orderManagement.AddOrderDetails(order)

        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub

    Public Sub DeleteOrder(orderId As Integer) Implements IService1.DeleteOrder
        Try
            Dim orderManagement As New OrderManagementService()
            orderManagement.DeleteOrder(orderId)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub

    Public Sub DeleteSelectedOrderInList(orderIdsList As List(Of Integer)) Implements IService1.DeleteSelectedOrderInList
        Try
            Dim orderManagement As New OrderManagementService()
            orderManagement.DeleteSelectedOrders(orderIdsList)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub

    Public Function GetOrderDetails() As List(Of Order) Implements IService1.GetOrderDetails
        Try
            Return New OrderDetailsMapper().MapOrderDetails(New OrderManagementDataAccess().GetOrderDetails())
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Return New List(Of Order)()
        End Try
    End Function

    Public Function GetOrderSummary(orderId As Integer) As Order Implements IService1.GetOrderSummary
        Try
            Return New OrderDetailsMapper().MapOrderSummary(New OrderManagementDataAccess().GetOrderSummary(orderId))
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Return New Order()
        End Try
    End Function

    Public Function GetVendors() As List(Of Vendor) Implements IService1.GetVendors
        Try
            Return New OrderDetailsMapper().MapVendorDetails(New OrderManagementDataAccess().GetVendors())
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
            Return New List(Of Vendor)()
        End Try
    End Function
End Class
