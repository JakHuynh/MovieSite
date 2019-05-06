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
using System.Net.Mail;
using System.Text;
using System.Net;

namespace Milestone2
{
    public partial class Admin : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public static string photo_filename;

        protected void Page_Load(object sender, EventArgs e)
        {

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
  /*------------------------------------------------------------------LOGIN---------------------------------------*/
        public void login(Object src, EventArgs e)
        {
        
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM admin WHERE Email =@Email and Password = @Password", connection);
                command.Parameters.AddWithValue("@Email", loginName.Text);
                command.Parameters.AddWithValue("@Password", loginPass.Text);



                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //notification that the user has logged in
                    YouHaveLoggedIn.Visible = true;
                    AddBtn.Visible = true;
                    ChkBtn.Visible = true;
                    panel2.Visible = false;
                    Wishbtn.Visible = true;
                    Duebtn.Visible = true;
                }
                else
                {
                    lblInfo2.Text = "Incorrect login info";
                }

                reader.Close();
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
            }
        }
   /*-----------------------------------------------------------------SEARCH----------------------------*/
        protected void Search_func(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            command = new SqlCommand("SELECT * FROM Content", connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                MovieResults.Visible = true;
            }
            

            MovieResults.DataSource = reader;
            MovieResults.DataBind();

            reader.Close();

        }
   /*-------------------------------------------------------------------EDIT UPDATE DELETE--------------------------------*/
        public void btnSubmit_Click_one(Object Src, EventArgs E)
        {
            get_connection();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM content", connection);
                reader = command.ExecuteReader();
                MovieResults.DataSource = reader;
                MovieResults.DataBind();

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
                lblInfo.Text += "<br /><b>Now Connection Is:</b> ";
                lblInfo.Text += connection.State.ToString();
            }
        }
        public void btnSubmit_update_record(Object Src, GridViewUpdateEventArgs e)
        {
            get_connection();
            try
            {
                int id = int.Parse(MovieResults.DataKeys[e.RowIndex].Value.ToString());
                connection.Open();

                command = new SqlCommand("UPDATE content SET MovieTitle=@MovieTitle, MovieDescription=@MovieDescription WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("MovieID", SqlDbType.Int).Value = id;
                command.Parameters.AddWithValue("@MovieTitle",
                    ((TextBox)MovieResults.Rows[e.RowIndex].Cells[0].Controls[0]).Text.ToString());
                command.Parameters.AddWithValue("@MovieDescription",
                    ((TextBox)MovieResults.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());

                command.ExecuteNonQuery();
                MovieResults.DataBind();
            }
            catch (Exception err)
            {
                lblInfo.Text = "Error reading the database. ";
                lblInfo.Text += err.Message;
            }
            finally
            {
                connection.Close();
                lblInfo.Text += "<br /><b>Update was successfull</b> ";
                lblInfo.Text += connection.State.ToString();

            }
        }
        public void btnSubmit_delete_record(Object Src, GridViewDeleteEventArgs e)
        {
            get_connection();
            try
            {
                int id = int.Parse(MovieResults.DataKeys[e.RowIndex].Value.ToString());
                connection.Open();

                command = new SqlCommand("DELETE from content WHERE MovieID=@MovieID", connection);
                command.Parameters.AddWithValue("MovieID", SqlDbType.Int).Value = id;
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
            }
        }
   /*-------------------------------------------------------------ADD MOVIE-------------------------------------------*/
        public void AddMovie(Object Src, EventArgs e)
        {
            AddtoMovie.Visible = true;
        }

        public void Add_Func(Object Src, EventArgs e)
        {
            get_connection();
            if (FileUploadControl.HasFile)
            {
                try
                {
                    connection.Open();
                    string filename = System.IO.Path.GetFileName(FileUploadControl.FileName);
                    FileUploadControl.SaveAs(Server.MapPath("~/Project2") + filename);
                    string mapname = (Server.MapPath("~/Project2") + filename);
                    photo_filename = filename;

                    command = new SqlCommand("INSERT INTO content (MovieTitle, MovieDescription, ImageLocation)" +
                  " VALUES (@MovieTitle, @MovieDescription, @ImageLocation)", connection);

                    command.Parameters.AddWithValue("@MovieTitle", NewMovie.Text);
                    command.Parameters.AddWithValue("@MovieDescription", NewDescription.Text);
                    command.Parameters.AddWithValue("@ImageLocation", photo_filename);

                    command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    lblInfo.Text = "Error reading the database. ";
                    lblInfo.Text += err.Message;
                    StatusLabel.Text = "Upload status: The file could not be uploaded.";
                }
                finally
                {
                    connection.Close();
                    StatusLabel.Text = "Upload status: uploaded.";
                }
            }
            else
            {
                try
                {
                    connection.Open();

                    command = new SqlCommand("INSERT INTO content (MovieTitle, MovieDescription, ImageLocation)" +
                  " VALUES (@MovieTitle, @MovieDescription, @ImageLocation)", connection);

                    command.Parameters.AddWithValue("@MovieTitle", NewMovie.Text);
                    command.Parameters.AddWithValue("@MovieDescription", NewDescription.Text);
                    command.Parameters.AddWithValue("@ImageLocation", "blank");

                    command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    lblInfo.Text = "Error reading the database. ";
                    lblInfo.Text += err.Message;
                }
                finally
                {
                    connection.Close();
                    lblInfo.Text = "Movie Added";
                }
            }
        }
   /*-------------------------------------------------------------------CHECKOUT MOVIE----------------------------*/
        protected void showCheckOut(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            command = new SqlCommand("SELECT * FROM checkout", connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                CheckoutPanel.Visible = true;
            }


            one_data2.DataSource = reader;
            one_data2.DataBind();

            reader.Close();

        }

    /*------------------------------------------------------SHOW DUE MOVIES-------------------------------------*/
        public void DueMovies(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            //command = new SqlCommand("SELECT * FROM checkout", connection);
            //reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT MovieID, SubscriberID, DueDate FROM checkout", connection))
            {
                da.Fill(dt);
            }
            foreach (DataRow row in dt.Rows)
            {
                string movieId = row["MovieID"].ToString();
                string subscriberId = row["SubscriberID"].ToString();
                DateTime DueDate = Convert.ToDateTime(row["DueDate"]);

                if (DateTime.Now >= DueDate)
                {
                    var fromAdress = "XXXXXX@gmail.com";
                    var fromPassword = "password";
                    var toAddress = "XXXXXXX@gmail.com";
                    var msgSubject = "Movie due date exceeded";
                    var msgBody = "Your Movie with Id " + movieId + " have exceeded the due date.";

                    SendMail(fromAdress, fromPassword, toAddress, msgSubject, msgBody);
                }
                lblInfo.Text = subscriberId;
                lblInfo2.Text = DueDate.ToLongDateString();
                
            }
            if (!string.IsNullOrWhiteSpace(lblInfo.Text))
            {
                lblInfo.Text = "A movie has been found overdue an Email sent to" + lblInfo.Text;
                lblInfo2.Text = "The movie was originally due on " + lblInfo2.Text;
            }
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    pnlCheckOut.Visible = true;
                    grdCheckOut.DataSource = dt;
                    grdCheckOut.DataBind();
                }
            }

        }
        private void SendMail(string fromAddress, string fromPassword, string toAddress, string msgSubject, string msgBody)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);

                MailMessage mm = new MailMessage(fromAddress, toAddress, msgSubject, msgBody);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);

                lblInfo.Text = toAddress + ",";
            }
            catch (Exception ex)
            {
                lblInfo.Text = "OOPS! some exception occured.";
            }
            finally
            {
            }
        }
   /*------------------------------------------------------------------WISHLIST---------------------------------*/
        protected void Wishlist(Object src, EventArgs e)
        {
            get_connection();
            connection.Open();
            command = new SqlCommand("SELECT * FROM wishlist", connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                WishPanel.Visible = true;
            }


            GridWish.DataSource = reader;
            GridWish.DataBind();

            reader.Close();

        }
    }
}