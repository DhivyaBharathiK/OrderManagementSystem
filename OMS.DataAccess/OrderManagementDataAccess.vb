Imports System.Data.SqlClient
Imports OMS.Business

Public Class OrderManagementDataAccess
    Private ReadOnly connectionString As String = "Server=localhost;Database=OrderManagementDB;Integrated Security=True;"

#Region "Helper Methods"

    ' Helper method to execute a command and return a DataTable
    Private Function ExecuteDataTable(storedProcedure As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim resultTable As New DataTable()
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(storedProcedure, connection)
                command.CommandType = CommandType.StoredProcedure
                AddParameters(command, parameters)
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(resultTable)
                End Using
            End Using
        End Using
        Return resultTable
    End Function

    ' Helper method to execute a non-query command (e.g., insert, update, delete)
    Private Sub ExecuteNonQuery(storedProcedure As String, Optional parameters As Dictionary(Of String, Object) = Nothing)
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(storedProcedure, connection)
                command.CommandType = CommandType.StoredProcedure
                AddParameters(command, parameters)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Helper method to add parameters to a command
    Private Sub AddParameters(command As SqlCommand, parameters As Dictionary(Of String, Object))
        If parameters IsNot Nothing Then
            For Each param In parameters
                command.Parameters.AddWithValue(param.Key, param.Value)
            Next
        End If
    End Sub

#End Region

#Region "Get Methods"

    Public Function GetOrderDetails() As DataTable
        Return ExecuteDataTable("usp_get_order_details")
    End Function

    Public Function GetVendors() As DataTable
        Return ExecuteDataTable("usp_get_vendors")
    End Function

    Public Function GetOrderSummary(orderId As Integer) As DataTable
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@OrderId", orderId}
        }
        Return ExecuteDataTable("usp_get_order_summary", parameters)
    End Function

#End Region

#Region "Add/Update Methods"

    Public Sub AddUpdateOrderDetails(order As Order)
        If order.OrderNumber = 0 Then
            AddOrderDetails(order)
        Else
            UpdateOrderDetails(order)
        End If
    End Sub

    Private Sub AddOrderDetails(order As Order)
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@OrderDate", order.OrderDate},
            {"@VendorId", order.Vendor.VendorId},
            {"@Amount", order.Amount},
            {"@ShopId", order.ShopId}
        }
        ExecuteNonQuery("usp_insert_order_details", parameters)
    End Sub

    Private Sub UpdateOrderDetails(order As Order)
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@OrderId", order.OrderId},
            {"@OrderDate", order.OrderDate},
            {"@VendorId", order.Vendor.VendorId},
            {"@ShopId", order.ShopId}
        }
        ExecuteNonQuery("usp_update_order_details", parameters)
    End Sub

#End Region

#Region "Delete Methods"

    Public Sub DeleteOrder(orderId As Integer)
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@OrderId", orderId}
        }
        ExecuteNonQuery("usp_delete_order_details", parameters)
    End Sub

    Public Sub DeleteSelectedOrders(orderIds As String)
        Dim parameters As New Dictionary(Of String, Object) From {
            {"@OrderIds", orderIds}
        }
        ExecuteNonQuery("usp_delete_order_details", parameters)
    End Sub

#End Region

End Class
