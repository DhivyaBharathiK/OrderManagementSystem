Imports OMS.Business
Public Interface IAddUpdateOrderDetails
    Property OrderId As Integer
    Property OrderDate As DateTime
    Property OrderNumber As Integer
    Property OrderAmount As Decimal
    Property VendorId As Integer
    Property ShopId As String
    Sub LoadVendors()
    Sub LoadOrderSummaryDetails(orderId As Integer)
    Sub PopulateOrderDetailsForEdit(order As Order)
    Sub SetVendorForSelectedOrder(selectedVendorId As Integer)
    Sub ShowHideOrderFields(action As String)
End Interface
