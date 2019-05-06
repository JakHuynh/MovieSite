<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Milestone2.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panel2" runat="server" Wrap="true" Visible="true">
            <h2 id="logError" runat="server" visible="false">Error logging in</h2>
            <strong>Username</strong><asp:TextBox runat="server" ID="loginName"></asp:TextBox><br />
            <strong>Password</strong><asp:TextBox runat="server" TextMode="Password" ID="loginPass"></asp:TextBox> <br />
            
            <asp:Button runat="server" Text="Log in" OnClick="login" /><br />
            
        </asp:Panel>

        <asp:Panel ID="YouHaveLoggedIn" runat="server" Visible="false">
            <h2>Thanks for logging in</h2><br />
            <asp:Button runat="server" Text="Display Movies" OnClick="Search_func" /><br />
            
        </asp:Panel>
        
        <asp:Label ID="lblInfo" runat="server"></asp:Label><br />
        <asp:Label ID="lblInfo2" runat="server"></asp:Label><br />

                <asp:GridView runat="server" ID="MovieResults" Wrap="true" Visible="false" AutoGenerateColumns="false"
            OnRowEditing="btnSubmit_Click_one" OnRowUpdating="btnSubmit_update_record"
            DataKeyNames="MovieID" OnRowDeleting="btnSubmit_delete_record">
            <Columns>
                
                <asp:BoundField DataField="MovieTitle" HeaderText="Movie" />
                <asp:BoundField DataField="DateChecked" HeaderText="Date Checked" />
                <asp:BoundField DataField="CheckedOut" HeaderText="Checked Out" />
                <asp:BoundField DataField="MovieDescription" HeaderText="Description" />
                <asp:ImageField DataImageUrlField="ImageLocation" ControlStyle-Width="50"
                    ControlStyle-Height = "50" HeaderText = "Movie Image" />
                <asp:BoundField DataField="MovieID" HeaderText="Movie ID" />
                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>

        <asp:Panel runat="server" ID="CheckoutPanel" Visible="false">
            <h4>Movies that are Checked Out</h4>
            <asp:GridView id="one_data2" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="MovieID"
                        HeaderText="Movie ID"/>
                    <asp:BoundField DataField="SubscriberID"
                        HeaderText="Checked Out By"/>
                    <asp:BoundField DataField="DueDate"
                        HeaderText="Due On" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <asp:Panel runat="server" ID="WishPanel" Visible="false">
            <h4>Wishlist</h4>
            <asp:GridView id="GridWish" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="MovieTitle"
                        HeaderText="Movie Title"/>
                    <asp:BoundField DataField="SubscriberID"
                        HeaderText="Suggested By"/>
                    
                </Columns>
            </asp:GridView>
        </asp:Panel>


        <asp:Button runat="server" OnClick="showCheckOut" Text="Show Checked Out Movie" ID="ChkBtn" Visible="false" />
        <asp:Button runat="server" OnClick="AddMovie" Text="Add Movie" Visible="false" ID="AddBtn" />
        <asp:Button runat="server" OnClick="Wishlist" Text="Show Wishlist" Visible="false" ID="Wishbtn" />
        <asp:Button runat="server" OnClick="DueMovies" Text="Check for Due Movies" ID="Duebtn" Visible="false" />

        <asp:Panel runat="server" ID="AddtoMovie" Wrap="true" Visible="false">
            <h4>Movie Title</h4> <asp:TextBox ID="NewMovie" runat="server"></asp:TextBox><br />
            <h4>Movie Description</h4> <asp:TextBox ID="NewDescription" runat="server"></asp:TextBox><br />
            <asp:FileUpload id="FileUploadControl" runat="server" />
            <asp:Button runat="server" Text="Submit Movie" OnClick="Add_Func" />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
        </asp:Panel>
        <asp:Panel ID="pnlCheckOut" runat="server" Visible="false" Height="314px">
            <asp:GridView ID="grdCheckOut" runat="server" AutoGenerateColumns="false">
               <Columns>
                <asp:BoundField DataField="MovieID" HeaderText="Movie ID" ItemStyle-Width="150" />
                <asp:BoundField DataField="SubscriberID" HeaderText="Subscriber ID" ItemStyle-Width="150" />
                <asp:BoundField DataField="DueDate" HeaderText="DueOn" ItemStyle-Width="150" />
               </Columns>
            </asp:GridView>
            <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
        </asp:Panel>

    
    </div>
    </form>
</body>
</html>
