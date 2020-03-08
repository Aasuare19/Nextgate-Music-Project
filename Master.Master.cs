using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NextGateMusic
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;

            if (url.Contains("Login")){

                //if cookie exsits no need to login
                if (Request.Cookies["user"] != null)
                {
                    //log admin out
                    if (Request.Cookies["user"].Value == "admin")
                    {
                        Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(-1);
                    }
                    else
                    {
                        Response.Redirect("Search.aspx");
                    }
                }

                //modify jumbotron text
                btnNav.InnerText = "Sign Up";

                //modify nav button
                headerTitle.InnerText = "NextGate Music";

            }
            else if (url.Contains("Search"))
            {
                string cookieUser;

                //check for user login cookie
                if (Request.Cookies["user"] == null)
                {
                    //redirect user to login page
                    Response.Redirect("Login.aspx");
                }

                //get cookie value
                cookieUser = Request.Cookies["user"].Value;

                //modify nav button
                btnNav.InnerText = "Log Out";

                //modify jumbotron text
                headerTitle.InnerText = "Welcome, " + cookieUser;
            }
            else if (url.Contains("Signup"))
            {
                //modify nav button
                btnNav.InnerText = "Log In";

                //modify jumbotron text
                headerTitle.InnerText = "Sign Up";
            }
            else if (url.Contains("Admin"))
            {
                //modify nav button
                btnNav.InnerText = "Log Out";

                //modify jumbotron text
                headerTitle.InnerText = "Admin";
            }

        }

        protected void btnNav_Click(object sender, EventArgs e)
        {
            if (btnNav.InnerText.Equals("Log Out"))
            {
                //end cookie session
                Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(-1);
                //redirect to login
                Response.Redirect("Login.aspx");
            }
            else if (btnNav.InnerText.Equals("Log In"))
            {
                Response.Redirect("Login.aspx");
            }
            else if (btnNav.InnerText.Equals("Sign Up"))
            {
                Response.Redirect("Signup.aspx");
            }
        }
    }
}