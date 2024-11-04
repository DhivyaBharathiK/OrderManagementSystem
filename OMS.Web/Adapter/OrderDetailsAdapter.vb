
Imports OMS.Business
Imports OMS.Service

Namespace ServiceReference1
    Public Class OrderDetailsAdapter

#Region "Private Variables"
        Private ReadOnly servicePort As IService1
#End Region

#Region "Constructor"
        Public Sub New()
            servicePort = New Service1Client()
        End Sub
#End Region

#Region "Get Methods"
        Public Function GetOrderDetails() As List(Of Order)
            Try
                Return servicePort.GetOrderDetails().ToList()
            Catch ex As Exception
                Return New List(Of Order)()
            End Try
        End Function
        Public Function GetVendors() As List(Of Vendor)
            Try
                Return servicePort.GetVendors().ToList()
            Catch ex As Exception
                Return New List(Of Vendor)()
            End Try
        End Function
        Public Function GetOrderSummary(orderId As Integer) As Order
            Try
                Return servicePort.GetOrderSummary(orderId)
            Catch ex As Exception
                Return New Order()
            End Try
        End Function
#End Region

#Region "Add Methods"
        Public Sub AddOrderDetails(order As Order)
            Try
                servicePort.AddOrderDetails(order)
            Catch ex As Exception
                Console.WriteLine("An error occurred while adding order details.")
            End Try
        End Sub
#End Region

#Region "Delete Methods"
        Public Sub DeleteOrder(orderId As Integer)
            Try
                servicePort.DeleteOrder(orderId)
            Catch ex As Exception
                ' Log the exception
                Console.WriteLine("An error occurred while deleting the order.")
            End Try
        End Sub
        Public Sub DeleteSelectedOrders(orderIdList As List(Of Integer))
            Try
                servicePort.DeleteSelectedOrderInList(orderIdList.ToArray)
            Catch ex As Exception
                ' Log the exception
                Console.WriteLine("An error occurred while deleting the selected order(s)")
            End Try
        End Sub
#End Region
    End Class
End Namespace
