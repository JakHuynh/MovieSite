using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Milestone2
{
    public partial class Main : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public static string photo_filename;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool ValidatePassword(string password)
        {
            string patternPassword = @"^(?=.*[0-9])(?=.*[@#$&?])[0-9a-zA-Z!@#$%^&*0-9]{8,}$";
            if (!string.IsNullOrEmpty(password))
            {
                if (!Regex.IsMatch(password, patternPassword))
                {
                    return false;
                }

            }
            return true;
        }




        public void get_connection()
        {
            string connectionString =
             "Data Source = teach-web01.park.edu\\MSSQLSER2;" +
           " Initial Catalog = Jhuynh_dbn;" +
           " Integrated Security = False;" +
           " User Id = Jhuynh_usn;" +
           " Password = @W1jy91i;" +
           " MultipleActiveResultSets = True";

            connection = new SqlConnection(connectionString);
        }

        public void loginForm(Object src, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;

        }
        //return to opening form
        public void ReturnMain(Object src, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }
        /*---------------------------------------------------------------------------------------------------------------------------------*/
        //login function
        public void login(Object src, EventArgs e)
        {
            int attempt = 0;
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM subscribers WHERE Email =@Email and Password = @Password", connection);
                command.Parameters.AddWithValue("@Email", loginName.Text);
                command.Parameters.AddWithValue("@Password", loginPass.Text);
                //Session["User"] = loginName.Text;
                //Session["Number"] = attempt;
                


                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //notification that the user has logged in
                    YouHaveLoggedIn.Visible = true;
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel6.Visible = true;
                    WishPanel.Visible = true;
                }
                else
                {
                    attempt++;
                    lblInfo2.Text = "Attempt count: " + attempt;
                    
                    logError.Visible = true;
                    if (attempt >= 3) // lockout function but does not work, unsure why
                    {
                        lockedOut.Visible = true;
                        panel2.Visible = false;
                    }
                }

                reader.Close();
                Session["attempt"] = attempt;
            }
            catch (Exception err)
            {
                //user did not log in successfully
                lblInfo2.Text = "Error reading the database. ";
                lblInfo2.Text += err.Message;
            }
            finally
            {
                //lblInfo.Text = "good connect. ";
                connection.Close();
                attempt++;
            }

        }
        /*----------------------------------------------------------------------------------------------------------------------------------------------*/
        //opens register form
        public void register(Object src, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = true;
        }
        /*---------------------------------------------------------------------------------------------------------------------------*/
        //registration function
        protected void registerUser(Object src, EventArgs e)
        {
            //Response.Write("you have connected to your .cs page add records");
            get_connection();

            if (ValidatePassword(txtPassword.Text))
            {
                try
                {
                    connection.Open();
                    //command = new SqlCommand("INSERT INTO subscribers (FirstName, LastName, Email, Password)" +
                    //" VALUES (@FirstName, @LastName, @Email, @Password)", connection);
                    string cmdText = @"IF NOT EXISTS(SELECT 1 FROM subscribers where Email = @Email) 
                        INSERT INTO subscribers (FirstName, LastName, Email, Password)
                                 VALUES (@FirstName, @LastName, @Email, @Password)";

                    command = new SqlCommand(cmdText, connection);

                    command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Password", txtPassword.Text);

                    command.ExecuteNonQuery();



                    //connection.Close();
                }
                catch (Exception err)
                {
                    lblInfo.Text = "Error reading the database. ";
                    lblInfo.Text += err.Message;
                }
                finally
                {
                    connection.Close();
                    lblInfo.Text += "<br /><b>Record has been added</b>";
                    //lblInfo.Text = "<b>Server Version:</b> " + connection.ServerVersion;
                    lblInfo.Text += "<br /><b>Connection Is:</b> " + connection.State.ToString();
                }
            }
            else
            {
                Response.Write("<br /> Password must be at least 8 characters, and must include at least one upper case letter," +
                   " one lower case letter, one numeric digit, and a special character.");
            }
        }



        /*-----------------------------------------------------------------------------------------------------------------------------*/
        // search function for movies when user has logged in
       /* protected void Search_func(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            
            command = new SqlCommand("IF EXISTS(SELECT * FROM Content WHERE MovieTitle=@MovieTitle) SELECT * FROM Content WHERE MovieTitle=@MovieTitle ELSE SELECT * FROM Content", connection);
            command.Parameters.AddWithValue("@MovieTitle", MovieTitle.Text);
            //command.Parameters.AddWithValue("@ImageLocation", path);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("Movie Found");
                //CheckOutPanel.Visible = true;
                panel6.Visible = true;
            }
            else if (!reader.HasRows)
            {
                Response.Write("Movie Not Found Or Has Already Been Checked Out");
                
                //CheckOutPanel.Visible = true;
                panel6.Visible = true;
            }

            //MovieResults.DataSource = reader;
            //MovieResults.DataBind();

            reader.Close(); 

        } */
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        //Shows user that movie does exist but has to log in first
        protected void Fake_Search(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            command = new SqlCommand("SELECT * FROM content WHERE MovieTitle=@MovieTitle", connection);
            command.Parameters.AddWithValue("@MovieTitle", MovieTitle2.Text);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("Movie Found, Please Log in First");
               
            }
            else
            {
                Response.Write("Movie Not Found");
            }
        }
  /*----------------------------------------------------------------CHECKOUT MOVIES-----------------------------------------------------------------*/
        public void btnSubmit_Click_one(Object Src, EventArgs E)
        {
            CheckOutPanel.Visible = true;
            //MovieResults.Visible = false;
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM content", connection);
                reader = command.ExecuteReader();
                //MovieResults.DataSource = reader;
                //MovieResults.DataBind();

                reader.Close();

                reader = command.ExecuteReader();
                CheckOutList.DataSource = reader;
                CheckOutList.DataTextField = "MovieTitle";
                CheckOutList.DataValueField = "MovieID";
                CheckOutList.DataBind();
                reader.Close();
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                // Either way, make sure the connection is properly closed.
                // (Even if the connection wasn't opened successfully,
                //  calling Close() won't cause an error.)
                connection.Close();
                
            }
        }

        public void Get_data(Object Src, EventArgs E)
        {
            CheckOutPanel.Visible = false;
            
            MovieInfo.Visible = true;
            panel6.Visible = false;
            //MovieResults.Visible = false;
            get_connection();
            try
            {
                // Try to open the connection.
                connection.Open();

                command = new SqlCommand("SELECT * FROM content WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("@MovieID", CheckOutList.SelectedValue);

                reader = command.ExecuteReader();
                one_data.DataSource = reader;
                one_data.DataBind();



                reader.Close();


            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                
            }
        } // end of get one data


        public void GoBack(object sender, EventArgs e)
        {
            MovieInfo.Visible = false;
            panel6.Visible = true;
            CheckOutPanel.Visible = true;
            //MovieResults.Visible = true;
        }

        public void CheckOut(object sender, EventArgs e)
        {
            get_connection();
            try
            {
                
                connection.Open();
                command = new SqlCommand("UPDATE Content SET DateChecked=@DateChecked, CheckedOut=@CheckedOut WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("@DateChecked", DateTime.Now);
                command.Parameters.AddWithValue("@CheckedOut", 'Y');
                command.Parameters.AddWithValue("@MovieID", CheckOutList.SelectedValue);

                //reader = command.ExecuteReader();
                command.ExecuteNonQuery();
                one_data.DataSource = reader;
                one_data.DataBind();
                connection.Close();

                

                connection.Open();
                command = new SqlCommand("INSERT INTO checkout (MovieID, SubscriberID, DueDate) VALUES (@MovieID, @SubscriberID, @DueDate)", connection);
                command.Parameters.AddWithValue("@MovieID", CheckOutList.SelectedValue);
                command.Parameters.AddWithValue("@SubscriberID", loginName.Text);
                command.Parameters.AddWithValue("@DueDate", DateTime.Today.AddDays(7));
                command.ExecuteNonQuery();
                
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                lblInfo.Text = "Movie Checked Out Due on " + DateTime.Today.AddDays(7);
                
            }
        }
/*------------------------------------------------------------------RETURN MOVIES-----------------------------------*/
        public void btnSubmit_Click_two(Object Src, EventArgs E)
        {

            ReturnMovie.Visible = true;
            //MovieResults.Visible = false;
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM checkout", connection);
                reader = command.ExecuteReader();

                reader.Close();

                reader = command.ExecuteReader();
                ReturnList.DataSource = reader;
                ReturnList.DataValueField = "MovieID";
                //ReturnList.DataTextField = "MovieTitle";
                ReturnList.DataBind();
                reader.Close();
            }
            catch (Exception err)
            {
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {              
                connection.Close();

            }
        }
        public void Get_data2(Object Src, EventArgs E)
        {
            ReturnMovie.Visible = false;
            ReturnInfo.Visible = true;
            //MovieResults.Visible = false;
            get_connection();
            try
            {
                // Try to open the connection.
                connection.Open();

                command = new SqlCommand("SELECT * FROM checkout WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("@MovieID", ReturnList.SelectedValue);

                reader = command.ExecuteReader();
                one_data2.DataSource = reader;
                one_data2.DataBind();



                reader.Close();


            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();

            }
        }

        public void Return_func(object sender, EventArgs e)
        {
            CommentForm.Visible = true;
            ReturnInfo.Visible = false;
            ReturnMovie.Visible = false;
            get_connection();
            try
            {

                connection.Open();
                command = new SqlCommand("UPDATE Content SET DateChecked=@DateChecked, CheckedOut=@CheckedOut WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("@DateChecked", ' ');
                command.Parameters.AddWithValue("@CheckedOut", 'N');
                //command.Parameters.AddWithValue("@MovieID",);
                command.Parameters.AddWithValue("@MovieID", ReturnList.SelectedValue);

                //reader = command.ExecuteReader();
                command.ExecuteNonQuery();
                one_data.DataSource = reader;
                one_data.DataBind();
                connection.Close();



                connection.Open();
                command = new SqlCommand("DELETE FROM checkout WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("@MovieID", ReturnList.SelectedValue);
                //command.Parameters.AddWithValue("@SubscriberID", loginName.Text);
                command.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                lblInfo.Text = "Movie Returned, Leave Us a Comment?";

            }
        }
 /*-----------------------------------------------------------WISHLIST--------------------------------------------*/

        public void Wish_form(object sender, EventArgs e)
        {
            txtWish.Visible = true;
            Wishbtn2.Visible = true;
        }
        public void Wish_func(object sender, EventArgs e)
        {
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("INSERT INTO wishlist(MovieTitle, SubscriberID) VALUES (@MovieTitle, @SubscriberID)", connection);
                command.Parameters.AddWithValue("@MovieTitle", txtWish.Text);
                command.Parameters.AddWithValue("@SubscriberID", loginName.Text);
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                lblInfo.Text = "Movie Added to Wishlist";

            }
        }

  /*--------------------------------------------------------COMMENT---------------------------------------------*/
        public void addComment(object sender, EventArgs e)
        {
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("Insert INTO comment(MovieID, SubscriberID, Rating, MovieComment) VALUES(@MovieID, @SubscriberID, @Rating, @MovieComment)", connection);
                command.Parameters.AddWithValue("@MovieID", ReturnList.SelectedValue);
                command.Parameters.AddWithValue("@SubscriberID", loginName.Text);
                command.Parameters.AddWithValue("@Rating", RateList.SelectedValue);
                command.Parameters.AddWithValue("@MovieComment", TxtComment.Text);
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                lblInfo.Text = "Comment Added, Thank you";

            }
        }
        public void NoComment(Object sender, EventArgs e)
        {
            CommentForm.Visible = false;
        }

    }
}