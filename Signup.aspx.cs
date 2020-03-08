using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NextGateMusic
{
    public partial class Signup : System.Web.UI.Page
    {
        private Controller _controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new Controller();
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string password2 = tbPassword2.Text;

            if (username.Any(char.IsWhiteSpace) || password.Any(char.IsWhiteSpace) || password2.Any(char.IsWhiteSpace))
            {
                lblInvalidSignup.Text = "Please refrain from using spaces";
                lblInvalidSignup.Visible = true;
                return;
            }

            if (!password.Equals(password2))
            {
                lblInvalidSignup.Text = "Passwords must match";
                lblInvalidSignup.Visible = true;
                return;
            }

            if (_controller.OpenConn())
            {
                if (!_controller.usernameExists(username))
                {
                    _controller.createNewUser(username, password);

                    if (_controller.usernameExists(username)) //check if user add was successful
                    {
                        _controller.CloseConn();
                        lblInvalidSignup.Visible = false;
                        Response.Cookies["user"].Value = username;
                        Response.Redirect("Search.aspx");
                    }
                    else //unable to create new user
                    {
                        lblInvalidSignup.Text = "Unalbe to create new username. Please try again";
                        lblInvalidSignup.Visible = true;
                    }

                    _controller.CloseConn();
                }
                else
                {
                    lblInvalidSignup.Text = "Username already exists. Please try a different username or log in";
                    lblInvalidSignup.Visible = true;
                }
            }
            else
            {
                lblInvalidSignup.Text = "Couldn't connect to server";
                lblInvalidSignup.Visible = true;
            }
        }
    }
}