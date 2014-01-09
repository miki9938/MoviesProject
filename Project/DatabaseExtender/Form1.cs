using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Movies.Repositories;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Movies;

namespace DatabaseExtender
{
    public partial class Form1 : Form
    {
        MovieRepository dbMovie;
        PersonRepository dbPerson;

        string url;
        string urlParams;

        public Form1()
        {

            InitializeComponent();
            dbMovie = new MovieRepository();
            dbPerson = new PersonRepository();
            urlParams = "&type=json&plot=full&episode=1&lang=en-US&aka=simple&release=full&business=0&tech=0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject data;
            movie tempMovie;
            person tempPerson;

            url = "http://mymovieapi.com/?id=";
            url += textBox1.Text + urlParams;

            tempMovie = new movie();
            tempPerson = new person();

            WebRequest request = WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            data = JObject.Parse(reader.ReadToEnd());

            int year = (int)data["release_date"][0]["year"];
            int day = (int)data["release_date"][0]["day"];
            int month = (int)data["release_date"][0]["month"];

            DateTime date = new DateTime(year, month, day);

            tempMovie.title = ((string)data["title"]).ToLower();
            tempMovie.release_date = date;
            tempMovie.description = (string)data["plot"];
                
            bool c = dbMovie.addMovie(tempMovie);

            textBox2.Text = c.ToString();
            
            cast tempCast = new cast();
            tempCast.movie_id = tempMovie.id;

            char[] separator = { ' ' };

            tempMovie.id = (dbMovie.getMovieByTitle(tempMovie.title)).id;

            foreach (string a in data["actors"])
            {
                tempPerson.name = a.ToLower();

                dbPerson.addNewPerson(tempPerson);
                

            }

            foreach (string d in data["directors"])
            {
                tempPerson.name = d.ToLower();

                dbPerson.addNewPerson(tempPerson);
            }

            foreach (string w in data["writers"])
            {
                tempPerson.name = w.ToLower();

                dbPerson.addNewPerson(tempPerson);
            }

            foreach (string a2 in data["actors"])
            {

                tempCast.person_id = (dbPerson.getPersonByName(a2)).id;
                tempCast.role = 2;
                dbPerson.addRole(tempCast);
            }

            foreach (string d2 in data["directors"])
            {

                tempCast.person_id = (dbPerson.getPersonByName(d2)).id;
                tempCast.role = 1;
                dbPerson.addRole(tempCast);
            }

            foreach (string w2 in data["writers"])
            {

                tempCast.person_id = (dbPerson.getPersonByName(w2)).id;
                tempCast.role = 3;
                dbPerson.addRole(tempCast);
            }

            dataStream.Close();
            reader.Close();
            response.Close();

            textBox1.Clear();
        }
    }
}
