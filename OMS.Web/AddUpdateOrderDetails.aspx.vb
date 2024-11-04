Imports OMS.Business

Public Class AddUpdateOrderDetails
    Inherits System.Web.UI.Page
    Implements IAddUpdateOrderDetails

#Region "Private Variables"
    Private ReadOnly presenter As AddUpdateOrderDetailsPresenter
#End Region

#Region "Constructor"
    Public Sub New()
        presenter = New AddUpdateOrderDetailsPresenter(Me)
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HandlePageLoad()
        End If
    End Sub
    ''' <summary>
    ''' Determines the action (Add/Edit) to be performed on the page load
    ''' </summary>
    Private Sub HandlePageLoad()
        Dim action As String = Request.QueryString("action")
        ShowHideOrderFields(action)

        If Integer.TryParse(Request.QueryString("OrderID"), OrderId) Then
            LoadOrderSummaryDetails(OrderId)
        Else
            LoadVendors()
        End If
    End Sub
#End Region

#Region "Click Events"
    ''' <summary>
    ''' Saves the order details to the database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            presenter.AddOrderDetails(CreateOrder())
            CloseModalWithReload()
        Catch ex As Exception
            DisplayError("Error saving order")
        End Try
    End Sub
#End Region

#Region "Properties"
    Public Property OrderId As Integer Implements IAddUpdateOrderDetails.OrderId
        Get
            Return If(String.IsNullOrEmpty(OrderIdHiddenField.Value), 0, CInt(OrderIdHiddenField.Value))
        End Get
        Set(value As Integer)
            OrderIdHiddenField.Value = value.ToString()
        End Set
    End Property

    Public Property OrderDate As Date Implements IAddUpdateOrderDetails.OrderDate
        Get
            Return DateTime.Parse(txtOrderDate.Text)
        End Get
        Set(value As Date)
            txtOrderDate.Text = value.ToString("yyyy-MM-dd")
        End Set
    End Property

    Public Property OrderNumber As Integer Implements IAddUpdateOrderDetails.OrderNumber
        Get
            Return If(String.IsNullOrEmpty(txtOrderNumber.Text), 0, Integer.Parse(txtOrderNumber.Text))
        End Get
        Set(value As Integer)
            txtOrderNumber.Text = value.ToString()
        End Set
    End Property

    Public Property Amount As Decimal Implements IAddUpdateOrderDetails.OrderAmount
        Get
            Return Decimal.Parse(txtOrderAmount.Text)
        End Get
        Set(value As Decimal)
            txtOrderAmount.Text = value.ToString()
        End Set
    End Property

    Public Property VendorId As Integer Implements IAddUpdateOrderDetails.VendorId
        Get
            Return Convert.ToInt32(ddlVendor.SelectedValue)
        End Get
        Set(value As Integer)
            ddlVendor.SelectedValue = value.ToString()
        End Set
    End Property

    Public Property ShopId As String Implements IAddUpdateOrderDetails.ShopId
        Get
            Return txtShop.Text
        End Get
        Set(value As String)
            txtShop.Text = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Load the list of vendors to the dropdown
    ''' </summary>
    Private Sub LoadVendors() Implements IAddUpdateOrderDetails.LoadVendors
        Try
            BindVendorsToDropdown(presenter.GetVendors())
        Catch ex As Exception
            DisplayError("Error loading vendors")
        End Try
    End Sub
    ''' <summary>
    ''' Data bind the vendors list to the dropdown
    ''' </summary>
    ''' <param name="vendorsList"></param>
    Private Sub BindVendorsToDropdown(vendorsList As List(Of Vendor))
        ddlVendor.DataSource = vendorsList
        ddlVendor.DataTextField = "VendorName"
        ddlVendor.DataValueField = "VendorId"
        ddlVendor.DataBind()
        ddlVendor.Items.Insert(0, New ListItem("--Select Vendor--", ""))
    End Sub
    ''' <summary>
    ''' Load the selected order details for editing
    ''' </summary>
    ''' <param name="orderId"></param>
    Private Sub LoadOrderSummaryDetails(orderId As Integer) Implements IAddUpdateOrderDetails.LoadOrderSummaryDetails
        Try
            Dim orderDetails As Order = presenter.GetOrderSummary(orderId)
            If orderDetails IsNot Nothing Then PopulateOrderDetailsForEdit(orderDetails)
        Catch ex As Exception
            DisplayError("Error loading order summary details")
        End Try
    End Sub
    ''' <summary>
    ''' Populate the order details for editing
    ''' </summary>
    ''' <param name="order"></param>
    Private Sub PopulateOrderDetailsForEdit(order As Order) Implements IAddUpdateOrderDetails.PopulateOrderDetailsForEdit
        OrderDate = order.OrderDate
        OrderNumber = order.OrderNumber
        Amount = order.Amount
        ShopId = order.ShopId
        SetVendorForSelectedOrder(order.Vendor.VendorId)
    End Sub
    ''' <summary>
    ''' Set the selected vendor for the order 
    ''' </summary>
    ''' <param name="selectedVendorId"></param>
    Private Sub SetVendorForSelectedOrder(selectedVendorId As Integer) Implements IAddUpdateOrderDetails.SetVendorForSelectedOrder
        Try
            LoadVendors()
            ddlVendor.SelectedValue = selectedVendorId.ToString()
        Catch ex As Exception
            DisplayError("Error setting vendor")
        End Try
    End Sub

    Private Sub ShowHideOrderFields(action As String) Implements IAddUpdateOrderDetails.ShowHideOrderFields
        divOrderNumber.Visible = action <> "add"
        txtOrderNumber.ReadOnly = (action = "edit")
        txtOrderNumber.Enabled = Not (action = "edit")
    End Sub

    Private Function CreateOrder() As Order
        Dim order = New Order() With {
            .OrderDate = OrderDate,
            .Amount = Amount,
            .Vendor = New Vendor With {.VendorId = VendorId},
            .ShopId = CInt(ShopId)
        }

        If OrderNumber <> 0 Then
            order.OrderId = OrderId
            order.OrderNumber = OrderNumber
        End If

        Return order
    End Function

    Private Sub DisplayError(message As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", $"alert('{message}');", True)
    End Sub

    Private Sub CloseModalWithReload()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Success", "parent.closeModal(); parent.location.reload();", True)
    End Sub
#End Region

End Class
