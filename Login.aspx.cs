using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace NextGateMusic
{
    public partial class Login : System.Web.UI.Page
    {
        private Controller _controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new Controller();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (_controller.OpenConn())
            {
                if (_controller.isValidUser(username, password))
                {
                    _controller.CloseConn();
                    lblInvalidLogin.Visible = false;
                    Response.Cookies["user"].Value = username;

                    if (username == "admin")
                    {
                        Response.Redirect("Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("Search.aspx");
                    }
                    
                } else
                {
                    lblInvalidLogin.Text = "Invalid username or password";
                    lblInvalidLogin.Visible = true;
                }

                _controller.CloseConn();
            }
            else
            {
                lblInvalidLogin.Text = "Couldn't connect to server";
                lblInvalidLogin.Visible = true;
            }
            
        }
    }
}