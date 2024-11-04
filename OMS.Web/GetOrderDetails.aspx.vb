Imports OMS.Business

Public Class GetOrderDetails
    Inherits System.Web.UI.Page
    Implements IGetOrderDetailsView

#Region "Private Variables"
    Private ReadOnly presenter As GetOrderDetailsPresenter
#End Region

#Region "Constructor"
    Public Sub New()
        presenter = New GetOrderDetailsPresenter(Me)
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadOrderDetails()
        End If
    End Sub

#End Region

#Region "Click Events"
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        Dim btnEdit As Button = TryCast(sender, Button)
        Dim orderID As Integer

        If btnEdit IsNot Nothing AndAlso Integer.TryParse(btnEdit.CommandArgument, orderID) Then
            Response.Redirect($"AddUpdateOrderDetails.aspx?action=edit&OrderID={orderID}")
        Else
            DisplayError("Invalid Order ID.")
        End If
    End Sub
    ''' <summary>
    ''' Delete a single order from the database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim btnDelete As Button = TryCast(sender, Button)
        Dim orderID As Integer

        If btnDelete IsNot Nothing AndAlso Integer.TryParse(btnDelete.CommandArgument, orderID) Then
            Try
                presenter.DeleteOrder(orderID)
                CheckForPresenterErrors()
                presenter.GetOrderDetails()
            Catch ex As Exception
                DisplayError($"Error: {ex.Message}")
            End Try
        Else
            DisplayError("Invalid Order ID.")
        End If
    End Sub
    ''' <summary>
    ''' Delete Selected Order(s) from the database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnDeleteSelected_Click(sender As Object, e As EventArgs)
        Try
            Dim selectedOrderIds = GetSelectedOrderIds()

            If selectedOrderIds.Any() Then
                presenter.DeleteSelectedOrders(selectedOrderIds)
                CheckForPresenterErrors()
                presenter.GetOrderDetails()
            End If
        Catch ex As Exception
            DisplayError("An error occurred while deleting selected orders")
        End Try
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Get the order details from the database and display them in the view
    ''' </summary>
    Private Sub LoadOrderDetails()
        Try
            presenter.GetOrderDetails()
        Catch ex As Exception
            DisplayError("Error loading order details")
        End Try
    End Sub
    Private Sub DisplayOrderDetails(result As List(Of OrderModel)) Implements IGetOrderDetailsView.DisplayOrderDetails
        gvOrders.DataSource = result
        gvOrders.DataBind()
    End Sub

    Private Sub DisplayError(message As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Error", $"alert('{message}');", True)
    End Sub

    Private Sub CheckForPresenterErrors()
        If presenter.HasErrors Then
            Throw New Exception(String.Join(", ", presenter.Errors))
        End If
    End Sub
    ''' <summary>
    ''' Get the orders that are selected in the GridView
    ''' </summary>
    ''' <returns></returns>
    Private Function GetSelectedOrderIds() As List(Of Integer)
        Return gvOrders.Rows.Cast(Of GridViewRow)() _
            .Where(Function(row) CType(row.FindControl("chkSelect"), CheckBox)?.Checked = True) _
            .Select(Function(row) CType(row.FindControl("hfOrderId"), HiddenField)?.Value) _
            .Where(Function(orderId) Integer.TryParse(orderId, Nothing)) _
            .Select(Function(orderId) Convert.ToInt32(orderId)) _
            .ToList()
    End Function
#End Region

End Class
