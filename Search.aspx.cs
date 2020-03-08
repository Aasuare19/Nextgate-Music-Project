using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NextGateMusic
{
    public partial class Search : System.Web.UI.Page
    {
        private Controller _controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new Controller();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = tbSearchText.Text;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                lblInvalidSearch.Text = "Please enter an artist or album name to search";
                lblInvalidSearch.Visible = true;
                return;
            }

            //trim
            searchText = searchText.Trim();

            //reduces white space to just one
            searchText = Regex.Replace(searchText, "[ ]{2,}", " ", RegexOptions.None);

            //add's wildcard for query
            searchText = string.Format("%{0}%", searchText);

            DataTable dt = new DataTable();

            if (_controller.OpenConn())
            {
                dt = _controller.searchDatabase(searchText);
                gvSearchResults.DataSource = dt;
                gvSearchResults.DataBind();

                _controller.CloseConn();
                lblInvalidSearch.Visible = false;
            }
            else
            {
                lblInvalidSearch.Text = "Couldn't connect to server";
                lblInvalidSearch.Visible = true;
            }
            
        }
    }
}