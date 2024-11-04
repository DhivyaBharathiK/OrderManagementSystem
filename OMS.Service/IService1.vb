Imports OMS.Business
<ServiceContract()>
Public Interface IService1

    <OperationContract()>
    Function GetOrderDetails() As List(Of Order)

    <OperationContract()>
    Function GetOrderSummary(orderId As Integer) As Order

    <OperationContract()>
    Function GetVendors() As List(Of Vendor)

    <OperationContract()>
    Sub AddOrderDetails(order As Order)

    <OperationContract()>
    Sub DeleteOrder(orderId As Integer)

    <OperationContract()>
    Sub DeleteSelectedOrderInList(orderIdsList As List(Of Integer))
End Interface