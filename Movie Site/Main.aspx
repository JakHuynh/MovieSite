<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Milestone2.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panel1" runat="server" Wrap="true" Visible="true">
            <h2>New or Old users may search for a item in our video  library</h2>
            <asp:Button runat="server" Text="old user" OnClick="loginForm" /><br />
            <asp:TextBox runat="server" ID="MovieTitle2"></asp:TextBox> <asp:Button runat="server" Text="Search" OnClick="Fake_Search" /><br />
            <asp:Button runat="server" Text="New User" OnClick="register" />
        </asp:Panel>


        <asp:Panel ID="panel2" runat="server" Wrap="true" Visible="false">
            <h2 id="logError" runat="server" visible="false">Error logging in</h2>
            <strong>Username</strong><asp:TextBox runat="server" ID="loginName"></asp:TextBox><br />
            <strong>Password</strong><asp:TextBox runat="server" TextMode="Password" ID="loginPass"></asp:TextBox> <br />
            <asp:Button runat="server" Text="Return" OnClick="ReturnMain" />
            <asp:Button runat="server" Text="Log in" OnClick="login" /><br />
            <asp:Label ID="lblInfo2" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="YouHaveLoggedIn" runat="server" Visible="false">
            <h2>Thanks for logging in</h2><br />
            <strong>Check Out Movie</strong><br />
            <h4>Search movie below</h4><br />
            
        </asp:Panel>

        <asp:Panel ID="lockedOut" runat="server" Wrap="false" Visible="false">
            <h1>You have been locked out.</h1>
        </asp:Panel>



        <asp:Panel ID="panel3" runat="server" Wrap="true" Visible="false">
            <table>
                <tr>
                    <td>
                        <strong>First Name</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Last Name</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Email(Will be used for Username</strong><br />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Password</strong><br />
                        Must meet the following requirements<br />
                        At least one Capital letter<br />
                        At least one lower case letter<br />
                        8 characters in length<br />
                        One of the following special characters<br />
                        $ # @ & ?
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td>
                        <asp:Button Text="Submit" runat="server" OnClick="registerUser" />
                    </td>
                </tr>

            </table>
            <asp:Label ID="lblInf" runat="server"></asp:Label>
        </asp:Panel>


        <asp:Label ID="lblInfo" runat="server"></asp:Label>
        <asp:Panel ID="CheckOutPanel" runat="server" Visible="false">

            <h4>Select a Move below to check out</h4>

            <asp:ListBox ID="CheckOutList" runat="server" OnSelectedIndexChanged="Get_data" AutoPostBack="true">

            </asp:ListBox>
        </asp:Panel>


        <asp:Panel ID="MovieInfo" runat="server" Visible="false">
            <asp:GridView id="one_data" AutoGenerateColumns="false" runat="server" DataKeyNames="MovieID">
                <Columns>
                    <asp:BoundField DataField="MovieID"
                        HeaderText="Movie ID"/>
                    <asp:BoundField DataField="MovieTitle"
                        HeaderText="Movie"/>
                    <asp:BoundField DataField="DateChecked"
                        HeaderText="Date Checked"/>
                    <asp:BoundField DataField="CheckedOut"
                        HeaderText="Checked Out"/>
                    <asp:BoundField DataField="MovieDescription" HeaderText="Description" />
                    <asp:ImageField DataImageUrlField="ImageLocation" ControlStyle-Width="50"
                    ControlStyle-Height = "50" HeaderText = "Movie Image" />
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" Text="Choose another Movie" OnClick="GoBack" />
            <asp:Button runat="server" Text="Check Out" OnClick="CheckOut" />
        </asp:Panel>



        <asp:Panel ID="panel6" runat="server" Visible="false">
            <asp:Button runat="server" Text="Rent a Movie" OnClick="btnSubmit_Click_one" />
            <asp:Button runat="server" Text="Return a movie" OnClick="btnSubmit_Click_two" />
        </asp:Panel>



        <asp:Panel runat="server" ID="ReturnMovie" Visible="false">
            <asp:ListBox ID="ReturnList" runat="server" OnSelectedIndexChanged="Get_data2" AutoPostBack="true" BackColor="LightGray">
                
            </asp:ListBox>

        </asp:Panel>


        <asp:Panel ID="ReturnInfo" runat="server" Visible="false">
            <asp:GridView id="one_data2" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="MovieID"
                        HeaderText="Movie ID"/>
                    <asp:BoundField DataField="SubscriberID"
                        HeaderText="Checked Out By"/>
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" Text="Return This Movie" OnClick="Return_func" />
        </asp:Panel>


        <asp:Panel runat="server" ID="CommentForm" Visible="false">
            <table>
                <tr>
                    <td>
                        <strong>Comment</strong>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="TxtComment"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Rating</strong>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="RateList">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                 </tr>
               </table>
            <asp:Button runat="server" Text="Submit Comment" OnClick="addComment" /> <asp:Button runat="server" Text="No Comment"  OnClick="NoComment"/>
        </asp:Panel>


        <asp:Panel ID="WishPanel" runat="server" Visible="false" BorderStyle="Dashed">
            <h4>Don't see a movie you want? Add to our wishlist.</h4>
            <asp:Button runat="server" Text="Wishlist" OnClick="Wish_form" ID="Wishbtn"/>
            <h4>Movie Title: </h4><asp:TextBox ID="txtWish" runat="server" Visible="false"></asp:TextBox>
            <asp:Button runat="server" ID="Wishbtn2" Visible="false" Text="Add to Wishlist" OnClick="Wish_func"/>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
