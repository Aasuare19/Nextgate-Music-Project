using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NextGateMusic
{
    public partial class Admin : System.Web.UI.Page
    {
        private Controller _controller;
        private string _tab;
        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new Controller();

            if (!Page.IsPostBack) 
            {
                refreshArtistList(); //on load create singer list
            }
            else //on postback set the selected tab
            {
                //if (!string.IsNullOrEmpty(_tab)) Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction(" + _tab + ")", true);

                _tab = Request.Form["hidden"];
            }
        }
        private void refreshArtistList()
        {
            rtrSingerName.DataSource = null;

            if (_controller.OpenConn())
            {
                rtrSingerName.DataSource = _controller.bindSingerButtons();
                rtrSingerName.DataBind();
                _controller.CloseConn();
            }
        }

        protected void rtrSingerName_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
         
        }

        protected void tabClick(object sender, EventArgs e)
        {
            //var item =  
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //sets the artist name label
            string name = (sender as Button).CommandArgument;
            lblSingerName.Text = name;
        }

        protected void btnAddAlbum_Click(object sender, EventArgs e)
        {
            string singername = lblSingerName.Text;
            string albumname = tbAlbumName.Text;
            string releaseyear = tbReleaseYear.Text;
            string recordcompany = tbRecordCompany.Text;

            //check for blank values
            if (string.IsNullOrWhiteSpace(singername) || string.IsNullOrWhiteSpace(albumname) || 
                string.IsNullOrWhiteSpace(releaseyear) || string.IsNullOrWhiteSpace(recordcompany))
            {
                lblInvalid.Text = "Please enter all values";
                lblInvalid.ForeColor = System.Drawing.Color.Red;
                lblInvalid.Visible = true;
                return;
            }

            //clean up
            albumname = _controller.cleanseUserInput(albumname);
            recordcompany = _controller.cleanseUserInput(recordcompany);
            releaseyear = _controller.cleanseUserInput(releaseyear);

            //validate year
            try
            {
                int year = Convert.ToInt32(releaseyear);
            }
            catch (Exception ex)
            {
                lblInvalid.Text = "Please enter a valid release year";
                lblInvalid.ForeColor = System.Drawing.Color.Red;
                lblInvalid.Visible = true;
                return;
            }

            //reset warning
            lblInvalid.Text = "";
            lblInvalid.Visible = false;

            //add album to database
            if (_controller.OpenConn())
            {
                //get singer id
                int id = (_controller.getSingerIdByName(singername));

                if (id != 0)
                {
                    if (!_controller.albumbExists(id, albumname, releaseyear, recordcompany)) //check if album exists
                    {
                        _controller.addAlbum(id, albumname, releaseyear, recordcompany);

                        if (_controller.albumbExists(id, albumname, releaseyear, recordcompany)) //if album add was successful
                        {
                            lblInvalid.Text = "Album successfully added";
                            lblInvalid.ForeColor = System.Drawing.Color.Green;
                            lblInvalid.Visible = true;
                        }
                        else //unable to add album
                        {
                            lblInvalid.Text = "Unable to add album. Please try again";
                            lblInvalid.ForeColor = System.Drawing.Color.Red;
                            lblInvalid.Visible = true;
                        }
                    }
                    else
                    {
                        lblInvalid.Text = "Album already exists";
                        lblInvalid.ForeColor = System.Drawing.Color.Red;
                        lblInvalid.Visible = true;
                    }
                }

                _controller.CloseConn();
            }


        }

        protected void btnAddArtist_Click(object sender, EventArgs e)
        {
            string firstname = tbFirstName.Value;
            string lastname = tblLastName.Value;
            string dob = dtDob.Value; //yyyy-mm-dd
            string sex = Request.Form["sex"];

            //check for blank values
            if (string.IsNullOrWhiteSpace(firstname) ||
                string.IsNullOrWhiteSpace(dob) || string.IsNullOrWhiteSpace(sex))
            {
                lblInvalidSinger.Text = "Please enter all mandatory values";
                lblInvalidSinger.ForeColor = System.Drawing.Color.Red;
                lblInvalidSinger.Visible = true;
                return;
            }

            //validate year
            string[] date = dob.Split('-').ToArray();
            string year = date[0];

            if (year.Length != 4)
            {
                lblInvalidSinger.Text = "Please enter a valid year";
                lblInvalidSinger.ForeColor = System.Drawing.Color.Red;
                lblInvalidSinger.Visible = true;
                return;
            }

            //concantonate name
            string name = firstname + " " + lastname;

            //clean name
            name = _controller.cleanseUserInput(name);

            //reset warning sing
            lblInvalidSinger.Text = "";
            lblInvalidSinger.Visible = false;

            //add to database
            if (_controller.OpenConn())
            {
                if (!_controller.artistExists(name, dob, sex)) //check if artists already exsists
                {
                    _controller.addNewArtist(name, dob, sex);

                    if (_controller.artistExists(name, dob, sex)) //check if add was successful
                    {
                        lblInvalidSinger.Text = "Successfully added new artist";
                        lblInvalidSinger.ForeColor = System.Drawing.Color.Green;
                        lblInvalidSinger.Visible = true;

                        tbFirstName.Value = "";
                        tblLastName.Value = "";
                        dtDob.Value = "";

                        refreshArtistList();
                    }
                    else //add wasn't successful
                    {
                        lblInvalidSinger.Text = "Unable to add add artist. Please try again.";
                        lblInvalidSinger.ForeColor = System.Drawing.Color.Red;
                        lblInvalidSinger.Visible = true;
                    }
                }
                else //artist already exists
                {
                    lblInvalidSinger.Text = "Artist already exists";
                    lblInvalidSinger.ForeColor = System.Drawing.Color.Red;
                    lblInvalidSinger.Visible = true;
                }

                _controller.CloseConn();
            }
            else //can't connect to db
            {
                lblInvalidSinger.Text = "Unable to connect to server. Please try again";
                lblInvalidSinger.ForeColor = System.Drawing.Color.Red;
                lblInvalidSinger.Visible = true;
            }

        }
    }
}