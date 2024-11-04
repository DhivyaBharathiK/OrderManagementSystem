Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports OMS.Business

<TestClass()>
Public Class AddUpdateOrderDetailsPresenterTests
    Private _presenter As AddUpdateOrderDetailsPresenter
    Private _mockView As Mock(Of IAddUpdateOrderDetails)
    Private _mockAdapter As Mock(Of ServiceReference1.OrderDetailsAdapter)

    <TestInitialize()>
    Public Sub SetUp()
        ' Initialize mocks using Moq
        _mockView = New Mock(Of IAddUpdateOrderDetails)()
        _mockAdapter = New Mock(Of ServiceReference1.OrderDetailsAdapter)()

        ' Inject mock adapter via reflection as the constructor initializes the adapter instance internally
        _presenter = New AddUpdateOrderDetailsPresenter(_mockView.Object)
        Dim adapterField = GetType(AddUpdateOrderDetailsPresenter).GetField("adapter", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        adapterField.SetValue(_presenter, _mockAdapter.Object)
    End Sub

    <TestMethod()>
    Public Sub GetVendors_ShouldReturnVendorList_WhenAdapterReturnsVendors()
        ' Arrange
        Dim vendors = New List(Of Vendor) From {
            New Vendor With {.VendorId = 1, .VendorName = "Vendor 1"},
            New Vendor With {.VendorId = 2, .VendorName = "Vendor 2"}
        }
        _mockAdapter.Setup(Function(a) a.GetVendors()).Returns(vendors)

        ' Act
        Dim result = _presenter.GetVendors()

        ' Assert
        Assert.AreEqual(2, result.Count)
        Assert.AreEqual("Vendor 1", result(0).VendorName)
        Assert.AreEqual("Vendor 2", result(1).VendorName)
    End Sub

    <TestMethod()>
    Public Sub GetVendors_ShouldReturnEmptyListAndLogError_WhenAdapterThrowsException()
        ' Arrange
        _mockAdapter.Setup(Function(a) a.GetVendors()).Throws(New Exception("Database error"))

        ' Act
        Dim result = _presenter.GetVendors()

        ' Assert
        Assert.AreEqual(0, result.Count)
        Assert.IsTrue(_presenter.HasErrors)
        Assert.AreEqual("Unable to get vendor details: Database error", _presenter.Errors(0))
    End Sub

    <TestMethod()>
    Public Sub GetOrderSummary_ShouldReturnOrder_WhenAdapterReturnsOrder()
        ' Arrange
        Dim orderId As Integer = 1
        Dim expectedOrder = New Order With {.OrderId = orderId, .OrderNumber = 123}
        _mockAdapter.Setup(Function(a) a.GetOrderSummary(orderId)).Returns(expectedOrder)

        ' Act
        Dim result = _presenter.GetOrderSummary(orderId)

        ' Assert
        Assert.AreEqual(expectedOrder, result)
    End Sub

    <TestMethod()>
    Public Sub GetOrderSummary_ShouldReturnEmptyOrderAndLogError_WhenAdapterThrowsException()
        ' Arrange
        Dim orderId As Integer = 1
        _mockAdapter.Setup(Function(a) a.GetOrderSummary(orderId)).Throws(New Exception("Database error"))

        ' Act
        Dim result = _presenter.GetOrderSummary(orderId)

        ' Assert
        Assert.IsNotNull(result)
        Assert.AreEqual(0, result.OrderId)
        Assert.IsTrue(_presenter.HasErrors)
        Assert.AreEqual("Unable to get order summary: Database error", _presenter.Errors(0))
    End Sub

    <TestMethod()>
    Public Sub AddOrderDetails_ShouldCallAdapterAddOrderDetails_WhenCalled()
        ' Arrange
        Dim order = New Order With {.OrderId = 1, .OrderNumber = 123}

        ' Act
        _presenter.AddOrderDetails(order)

        ' Assert
        _mockAdapter.Verify(Sub(a) a.AddOrderDetails(order), Times.Once)
    End Sub

    <TestMethod()>
    Public Sub AddOrderDetails_ShouldLogError_WhenAdapterThrowsException()
        ' Arrange
        Dim order = New Order With {.OrderId = 1, .OrderNumber = 123}
        _mockAdapter.Setup(Sub(a) a.AddOrderDetails(order)).Throws(New Exception("Database error"))

        ' Act
        _presenter.AddOrderDetails(order)

        ' Assert
        Assert.IsTrue(_presenter.HasErrors)
        Assert.AreEqual("Unable to add order details: Database error", _presenter.Errors(0))
    End Sub
End Class
