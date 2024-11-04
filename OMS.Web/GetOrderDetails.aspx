<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GetOrderDetails.aspx.vb" Inherits="OMS.Web.GetOrderDetails" %>

<!DOCTYPE html>
<div class="header">
    <h1>Order Management</h1>
</div>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orders</title>
    <style>
        .btn {
            background-color: #0099FF;
            color: white;
            padding: 5px 10px;
            border: none;
            cursor: pointer;
            text-align: center;
            margin-left: 10px;
        }

        .top-right {
            position: absolute;
            top: 20px;
            right: 20px;
        }

        .top-right-buttons {
            display: flex;
            justify-content: flex-end;
            margin-bottom: 10px;
            padding-right: 20px;
        }

        .gridview {
            border-collapse: collapse;
            width: 100%;
        }

            .gridview th, .gridview td {
                border: 1px solid #ddd;
                padding: 8px;
                text-align: center;
            }

        .modal {
            display: none;
            position: fixed;
            z-index: 1000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.5);
        }

        .modal-content {
            margin: 10% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
            max-width: 500px;
            background-color: #fff;
            border-radius: 5px;
            position: relative;
        }

            .modal-content iframe {
                width: 100%;
                height: 600px;
                border: none;
            }

        .close {
            position: absolute;
            top: 10px;
            right: 20px;
            color: #aaa;
            font-size: 28px;
            font-weight: bold;
            cursor: pointer;
        }

            .close:hover,
            .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
            }
    </style>
    <script type="text/javascript">
        function openModal() {
            document.getElementById("myModal").style.display = "block";
            document.getElementById("modalFrame").src = "AddUpdateOrderDetails.aspx?action=add";
        }

        function closeModal() {
            document.getElementById("myModal").style.display = "none";
            document.getElementById("modalFrame").src = "";
            location.reload();
        }
        function openOrderDialog(orderId) {
            document.getElementById("myModal").style.display = "block"; 
            document.getElementById("modalFrame").src = "AddUpdateOrderDetails.aspx?OrderId=" + orderId; 
        }

        function toggleSelectAll(chk) {
            const checkboxes = document.querySelectorAll('input[type="checkbox"]');
            checkboxes.forEach(function (cb) {
                if (cb !== chk) {
                    cb.checked = chk.checked;
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Orders</h2>
            <div class="top-right-buttons">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" OnClientClick="openModal(); return false;" />
                <asp:Button ID="btnDeleteSelected" runat="server" Text="Delete Selected" CssClass="btn" OnClick="btnDeleteSelected_Click" OnClientClick="return confirm('Are you sure you want to delete the orders?');" />
            </div>
            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="toggleSelectAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            <asp:HiddenField ID="hfOrderId" runat="server" Value='<%# Eval("OrderId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
                    <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" />
                    <asp:BoundField DataField="Amount" HeaderText="Order Amount" />
                    <asp:BoundField DataField="VendorName" HeaderText="Vendor" />
                    <asp:BoundField DataField="ShopId" HeaderText="Shop" />
                    <asp:BoundField DataField="VendorId" HeaderText="VendorID" Visible="False" />

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn" OnClientClick='<%# String.Format("openOrderDialog({0}); return false;", Eval("OrderId")) %>' />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CommandArgument='<%# Eval("OrderId") %>' OnClientClick="return confirm('Are you sure you want to delete the order?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="myModal" class="modal" style="display: none;">
            <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <iframe id="modalFrame" src="AddUpdateOrderDetails.aspx"></iframe>
            </div>
        </div>
    </form>
</body>
</html>
