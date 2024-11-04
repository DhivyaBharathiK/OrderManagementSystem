<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddUpdateOrderDetails.aspx.vb" Inherits="OMS.Web.AddUpdateOrderDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Add/Update Order Details</title>
    <style>
        .form-container {
            width: 300px;
            margin: auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .form-container h3 {
            text-align: left;
            margin-bottom: 10px;
        }
        .form-container .form-field {
            margin-bottom: 15px;
        }
        .form-container label {
            display: block;
            font-weight: bold;
        }
        .form-container input[type="text"],
        .form-container input[type="date"],
        .form-container select {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }
        .form-container .save-button {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h3>Order Details</h3>

            <div class="form-field" id="divOrderNumber" runat="server">
                <label for="txtOrderNumber">Order Number</label>
                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="order-field" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-field">
                <label for="txtOrderDate">Order Date</label>
                <asp:TextBox ID="txtOrderDate" runat="server" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-field" id="divAmount" runat="server">
                <label for="txtOrderAmount">Order Amount</label>
                <asp:TextBox ID="txtOrderAmount" runat="server"></asp:TextBox>
            </div>

            <div class="form-field">
                <label for="ddlVendor">Vendor</label>
                <asp:DropDownList ID="ddlVendor" runat="server"></asp:DropDownList>
            </div>

            <div class="form-field">
                <label for="txtShop">Shop</label>
                <asp:TextBox ID="txtShop" runat="server"></asp:TextBox>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-button" OnClick="btnSave_Click" />
            <asp:HiddenField ID="OrderIdHiddenField" runat="server" />
        </div>
    </form>
</body>
</html>
