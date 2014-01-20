using System;
using System.Windows.Forms;
using Movies.Mappings;
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

        enum months { Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };

        public Form1()
        {

            InitializeComponent();
            dbMovie = new MovieRepository();
            dbPerson = new PersonRepository();
            urlParams = "&type=json&plot=full&episode=1&lang=en-US&aka=simple&release=full&business=0&tech=0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                getDataTwo();
            else
                getDataOne();
        }

        private void getDataOne()
        {
            try
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
            catch
            {
                MessageBox.Show("Nie można połączyć z serwerem");
            }
        }

        private void getDataTwo()
        {
            try
            {
                JObject data;
                movie tempMovie;
                person tempPerson;

                url = "http://www.omdbapi.com/?i=";
                url += textBox1.Text;

                tempMovie = new movie();
                tempPerson = new person();

                WebRequest request = WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                data = JObject.Parse(reader.ReadToEnd());

                string[] dateTemp = Convert.ToString(data["Released"]).Split(' ');
                
                int tempMonth = (int)Enum.Parse(typeof(months), dateTemp[1]);

                DateTime date = new DateTime(Convert.ToInt32(dateTemp[2]), tempMonth, Convert.ToInt32(dateTemp[0]));

                tempMovie.title = ((string)data["Title"]).ToLower();
                tempMovie.release_date = date;
                tempMovie.description = (string)data["Plot"];

                bool c = dbMovie.addMovie(tempMovie);

                textBox2.Text = c.ToString();

                cast tempCast = new cast();
                tempCast.movie_id = tempMovie.id;

                char[] separator = { ' ' };

                tempMovie.id = (dbMovie.getMovieByTitle(tempMovie.title)).id;

                string[] Actors = Convert.ToString(data["Actors"]).Split(',');
                string[] Writers = Convert.ToString(data["Writer"]).Split(',');
                string[] Directors = Convert.ToString(data["Director"]).Split(',');

                foreach (string a in Actors)
                {
                    tempPerson.name = a.ToLower();

                    dbPerson.addNewPerson(tempPerson);


                }

                foreach (string d in Directors)
                {
                    tempPerson.name = d.ToLower();

                    dbPerson.addNewPerson(tempPerson);

                }

                foreach (string w in Writers)
                {
                    string[] tempName = w.ToLower()
                                         .Split('(');

                    tempPerson.name = tempName[0];
                    dbPerson.addNewPerson(tempPerson);
                }

                foreach (string a2 in Actors)
                {

                    tempCast.person_id = (dbPerson.getPersonByName(a2)).id;
                    tempCast.role = 2;
                    dbPerson.addRole(tempCast);
                }

                foreach (string d2 in Directors)
                {

                    tempCast.person_id = (dbPerson.getPersonByName(d2)).id;
                    tempCast.role = 1;
                    dbPerson.addRole(tempCast);
                }

                foreach (string w2 in Writers)
                {
                    string[] tempName = w2.ToLower()
                                         .Split('(');
                    tempCast.person_id = (dbPerson.getPersonByName(tempName[0])).id;
                    tempCast.role = 3;
                    dbPerson.addRole(tempCast);
                }

                dataStream.Close();
                reader.Close();
                response.Close();

                textBox1.Clear();
            }
            catch
            {
                MessageBox.Show("Nie można połączyć z serwerem");
            }            
        }
    }
}
