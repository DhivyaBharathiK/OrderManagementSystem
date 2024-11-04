Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports OMS.Business

<TestClass()>
Public Class GetOrderDetailsPresenterTests
    Private _presenter As GetOrderDetailsPresenter
    Private _mockView As Mock(Of IGetOrderDetailsView)
    Private _mockAdapter As Mock(Of ServiceReference1.OrderDetailsAdapter)

    <TestInitialize()>
    Public Sub SetUp()
        ' Initialize mocks using Moq
        _mockView = New Mock(Of IGetOrderDetailsView)()
        _mockAdapter = New Mock(Of ServiceReference1.OrderDetailsAdapter)()

        ' Inject mock adapter via reflection as the constructor initializes the adapter instance internally
        _presenter = New GetOrderDetailsPresenter(_mockView.Object)
        Dim adapterField = GetType(GetOrderDetailsPresenter).GetField("adapter", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        adapterField.SetValue(_presenter, _mockAdapter.Object)
    End Sub

    <TestMethod()>
    Public Sub GetOrderDetails_ShouldDisplayOrderDetails_WhenAdapterReturnsOrderList()
        ' Arrange
        Dim orderList = New List(Of Order) From {
            New Order With {.OrderId = 1, .OrderNumber = 123},
            New Order With {.OrderId = 2, .OrderNumber = 456}
        }
        _mockAdapter.Setup(Function(a) a.GetOrderDetails()).Returns(orderList)

        ' Act
        _presenter.GetOrderDetails()

        ' Assert
        _mockView.Verify(Sub(v) v.DisplayOrderDetails(It.IsAny(Of List(Of OrderModel))()), Times.Once)
        Assert.IsFalse(_presenter.HasErrors)
    End Sub

    <TestMethod()>
    Public Sub GetOrderDetails_ShouldLogError_WhenAdapterThrowsException()
        ' Arrange
        _mockAdapter.Setup(Function(a) a.GetOrderDetails()).Throws(New Exception("Database error"))

        ' Act
        _presenter.GetOrderDetails()

        ' Assert
        Assert.IsTrue(_presenter.HasErrors)
        Assert.AreEqual("Unable to Get Order Details: Database error", _presenter.Errors(0))
    End Sub

    <TestMethod()>
    Public Sub DeleteOrder_ShouldCallAdapterDeleteOrder_WhenCalled()
        ' Arrange
        Dim orderId As Integer = 1

        ' Act
        _presenter.DeleteOrder(orderId)

        ' Assert
        _mockAdapter.Verify(Sub(a) a.DeleteOrder(orderId), Times.Once)
        Assert.IsFalse(_presenter.HasErrors)
    End Sub

    <TestMethod()>
    Public Sub DeleteOrder_ShouldLogError_WhenAdapterThrowsException()
        ' Arrange
        Dim orderId As Integer = 1
        _mockAdapter.Setup(Sub(a) a.DeleteOrder(orderId)).Throws(New Exception("Database error"))

        ' Act
        _presenter.DeleteOrder(orderId)

        ' Assert
        Assert.IsTrue(_presenter.HasErrors)
        Assert.AreEqual("Error deleting order with ID 1: Database error", _presenter.Errors(0))
    End Sub

    <TestMethod()>
    Public Sub DeleteSelectedOrders_ShouldCallAdapterDeleteSelectedOrders_WhenCalled()
        ' Arrange
        Dim orderIdList = New List(Of Integer) From {1, 2, 3}

        ' Act
        _presenter.DeleteSelectedOrders(orderIdList)

        ' Assert
        _mockAdapter.Verify(Sub(a) a.DeleteSelectedOrders(orderIdList), Times.Once)
        Assert.IsFalse(_presenter.HasErrors)
    End Sub
End Class

