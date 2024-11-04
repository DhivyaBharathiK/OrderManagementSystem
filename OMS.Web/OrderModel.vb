Imports OMS.Business
Public Class OrderModel
    Public Property OrderId As Integer
    Public Property OrderDate As Date
    Public Property OrderNumber As Integer
    Public Property Amount As Decimal
    Public Property ShopId As Integer
    Public Property VendorId As Integer
    Public Property VendorName As String
End Class

Public Class OrderModelMapper

    Public Function MapOrderModel(result As List(Of Order)) As List(Of OrderModel)
        Dim orderModelList As New List(Of OrderModel)()
        For Each order As Order In result
            Dim orderModel As New OrderModel()
            orderModel.OrderId = order.OrderId
            orderModel.OrderDate = order.OrderDate
            orderModel.OrderNumber = order.OrderNumber
            orderModel.Amount = order.Amount
            orderModel.ShopId = order.ShopId
            orderModel.VendorId = order.Vendor.VendorId
            orderModel.VendorName = order.Vendor.VendorName
            orderModelList.Add(orderModel)
        Next
        Return orderModelList
    End Function

End Class
