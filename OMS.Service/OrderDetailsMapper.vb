Imports OMS.Business

Public Class OrderDetailsMapper
    Private Function MapOrderRow(row As DataRow) As Order
        Return New Order With {
            .OrderId = row("OrderId"),
            .OrderDate = row("OrderDate"),
            .OrderNumber = row("OrderNumber"),
            .Amount = row("Amount"),
            .ShopId = row("ShopId"),
            .Vendor = New Vendor With {
                .VendorId = row("VendorId"),
                .VendorName = If(IsDBNull(row("VendorName")), "Ford", row("VendorName").ToString())
            }
        }
    End Function

    Public Function MapOrderDetails(table As DataTable) As List(Of Order)
        Dim orderList As New List(Of Order)
        For Each row As DataRow In table.Rows
            orderList.Add(MapOrderRow(row))
        Next
        Return orderList
    End Function

    Public Function MapVendorDetails(table As DataTable) As List(Of Vendor)
        Dim vendorsList As New List(Of Vendor)
        For Each row As DataRow In table.Rows
            Dim vendor As New Vendor With {
                .VendorId = row("VendorId"),
                .VendorName = row("VendorName")
            }
            vendorsList.Add(vendor)
        Next
        Return vendorsList
    End Function

    Public Function MapOrderSummary(table As DataTable) As Order
        Return If(table.Rows.Count > 0, MapOrderRow(table.Rows(0)), New Order())
    End Function
End Class
