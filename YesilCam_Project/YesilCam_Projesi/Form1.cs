using Microsoft.VisualBasic;
using Npgsql;
using System.Data;
using System.Text;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace YesilCam_Projesi


{
    public partial class Form1 : Form
    {


        //my connection
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=hastalar;user ID = postgres; Password=12345");//SQL C# bağlantısı oluşturma




        public Form1()
        {
            InitializeComponent();
            //To make changes via Datagridview

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView2.CellEndEdit += DataGridView2_CellEndEdit;
            dataGridView3.CellEndEdit += DataGridView3_CellEndEdit;



        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            // Değişen hücrenin değerini al
            string yeniDeger = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();


            string filmAdi = dataGridView1.Rows[e.RowIndex].Cells["Adi"].Value.ToString();

            //Distinguish column types
            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {

                case "Rating":   //changing all values ​​via datagridview

                    if (double.TryParse(yeniDeger, out double ratingValue))
                    {
                        UpdateDatabase1("Rating", ratingValue, filmAdi);
                    }
                    else
                    {
                        MessageBox.Show("Error occurred:The Rating value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Butce(M)":
                    if (double.TryParse(yeniDeger, out double butceValue))
                    {
                        UpdateDatabase1("Butce(M)", butceValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The Butce(M) value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    break;

                case "Yapim_yili":
                    if (DateTime.TryParse(yeniDeger, out DateTime yapimYiliValue))
                    {
                        UpdateDatabase1("Yapim_yili", yapimYiliValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The value Yapim_yili could not be converted to a suitable date type. (Example format: YYYY-MM-DD) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Gise_sayisi":
                    if (double.TryParse(yeniDeger, out double giseSayisiValue))
                    {
                        UpdateDatabase1("Gise_sayisi", giseSayisiValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred: The value of Gise_sayisi could not be converted to a suitable number type ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                // Apply the same logic for other columns

                default:

                    MessageBox.Show("Error occurred:Unknown Column Type. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }


        }


        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            // Değişen hücrenin değerini al
            string yeniDeger = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();


            string filmAdi = dataGridView2.Rows[e.RowIndex].Cells["Adi"].Value.ToString();

            //Distinguish column types
            switch (dataGridView2.Columns[e.ColumnIndex].Name)
            {

                case "Rating":   //changing all values ​​via datagridview

                    if (double.TryParse(yeniDeger, out double ratingValue))
                    {
                        UpdateDatabase2("Rating", ratingValue, filmAdi);
                    }
                    else
                    {
                        MessageBox.Show("Error occurred:The Rating value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Butce(M)":
                    if (double.TryParse(yeniDeger, out double butceValue))
                    {
                        UpdateDatabase2("Butce(M)", butceValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The Butce(M) value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    break;

                case "Yapim_yili":
                    if (DateTime.TryParse(yeniDeger, out DateTime yapimYiliValue))
                    {
                        UpdateDatabase2("Yapim_yili", yapimYiliValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The value Yapim_yili could not be converted to a suitable date type. (Example format: YYYY-MM-DD) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Gise_sayisi":
                    if (double.TryParse(yeniDeger, out double giseSayisiValue))
                    {
                        UpdateDatabase2("Gise_sayisi", giseSayisiValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred: The value of Gise_sayisi could not be converted to a suitable number type ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                // Apply the same logic for other columns

                default:

                    MessageBox.Show("Error occurred:Unknown Column Type. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }


        }


        //updating the database by changing the column name
        private void UpdateDatabase1(string columnName, object columnValue, string filmAdi)
        {
            try
            {
                string sorgu = $"UPDATE filmler SET \"{columnName}\" = @YeniDeger WHERE \"Adi\" = @Adi";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@YeniDeger", columnValue);
                komut.Parameters.AddWithValue("@Adi", filmAdi);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                Grid1Guncelle(); // Update DataGridView after database is updated
                MessageBox.Show("Value updated successfully.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                baglanti.Close();
            }
        }

        //updating the database by changing the column name
        private void UpdateDatabase2(string columnName, object columnValue, string yoneticiAdi)
        {
            try
            {
                string sorgu = $"UPDATE yonetmenler SET \"{columnName}\" = @YeniDeger WHERE \"Adi\" = @Adi";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@YeniDeger", columnValue);
                komut.Parameters.AddWithValue("@Adi", yoneticiAdi);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                Grid1Guncelle(); // Update DataGridView after database is updated
                MessageBox.Show("Value updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                baglanti.Close();
            }
        }
        //updating the database by changing the column name
        private void UpdateDatabase3(string columnName, object columnValue, string oyuncuAdi)
        {
            try
            {
                string sorgu = $"UPDATE oyuncular SET \"{columnName}\" = @YeniDeger WHERE \"Adi\" = @Adi";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@YeniDeger", columnValue);
                komut.Parameters.AddWithValue("@Adi", oyuncuAdi);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                Grid1Guncelle(); // Update DataGridView after database is updated
                MessageBox.Show("Value updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                baglanti.Close();
            }
        }


        private void DataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Değişen hücrenin değerini al
            string yeniDeger = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();


            string filmAdi = dataGridView3.Rows[e.RowIndex].Cells["Adi"].Value.ToString();

            //Distinguish column types
            switch (dataGridView3.Columns[e.ColumnIndex].Name)
            {

                case "Rating":   //changing all values ​​via datagridview

                    if (double.TryParse(yeniDeger, out double ratingValue))
                    {
                        UpdateDatabase3("Rating", ratingValue, filmAdi);
                    }
                    else
                    {
                        MessageBox.Show("Error occurred:The Rating value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Butce(M)":
                    if (double.TryParse(yeniDeger, out double butceValue))
                    {
                        UpdateDatabase3("Butce(M)", butceValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The Butce(M) value was not converted to an appropriate number. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    break;

                case "Yapim_yili":
                    if (DateTime.TryParse(yeniDeger, out DateTime yapimYiliValue))
                    {
                        UpdateDatabase3("Yapim_yili", yapimYiliValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred:The value Yapim_yili could not be converted to a suitable date type. (Example format: YYYY-MM-DD) ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case "Gise_sayisi":
                    if (double.TryParse(yeniDeger, out double giseSayisiValue))
                    {
                        UpdateDatabase3("Gise_sayisi", giseSayisiValue, filmAdi);
                    }
                    else
                    {

                        MessageBox.Show("Error occurred: The value of Gise_sayisi could not be converted to a suitable number type ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                // Apply the same logic for other columns

                default:

                    MessageBox.Show("Error occurred:Unknown Column Type. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }


        }



        public void Grid1Guncelle()
        {


            try//A method to update DGW
            {
                string sorgu = "select * from filmler";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();
                da.Fill(ds);


                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["id"].Visible = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        }
        public void Grid2Guncelle()//A method to update DGW
        {

            try
            {
                string sorgu = "select * from yonetmenler";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();
                da.Fill(ds);


                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Columns["id"].Visible = false;
                dataGridView2.Columns["Alinan_Odul"].Visible = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }



        }


        public void Grid3Guncelle()//A method to update DGW
        {

            try
            {
                string sorgu = "select * from oyuncular";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();
                da.Fill(ds);



                dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.Columns["id"].Visible = false;
                dataGridView3.Columns["Alinan_Odul"].Visible = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }

        }











        private void button1_Click(object sender, EventArgs e)
        {
            Grid1Guncelle();


        }






        //Page to page transition buttons
        private void label7_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;

            int nextIndex = (currentIndex + 2) % tabControl1.TabPages.Count;

            tabControl1.SelectedIndex = nextIndex;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex + 1) % tabControl1.TabPages.Count;

            tabControl1.SelectedIndex = nextIndex;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex + 3) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex - 1) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex + 1) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex - 1) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex + 1) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex - 2) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex - 1) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControl1.SelectedIndex;


            int nextIndex = (currentIndex - 3) % tabControl1.TabPages.Count;


            tabControl1.SelectedIndex = nextIndex;
        }



        private void label20_Click(object sender, EventArgs e)
        {
            try
            {
                // Get new movie information from user
                string filmAdi = Interaction.InputBox("Enter Movie Name:", "Add New Film", "");
                double rating = Convert.ToDouble(Interaction.InputBox("Enter Rating:", "Add New Film", ""));
                double butce = Convert.ToDouble(Interaction.InputBox("Enter Budget (Million):", "Add New Film", ""));
                DateTime yapimYili = Convert.ToDateTime(Interaction.InputBox("Enter Release Year (YYYY-MM-DD):", "Add New Film", ""));
                double giseSayisi = Convert.ToDouble(Interaction.InputBox(" Enter number of box offices:", "Add New Film", ""));
                string oyuncular = Interaction.InputBox("Enter movie actors:", "Add New Film", "");
                string yonetmen = Interaction.InputBox("Enter director:", "Add New Film", "");
                string filmTuru = Interaction.InputBox("Enter movie genre:", "Add New Film", "");

                //Add new movie to database
                string sorgu = "INSERT INTO public.filmler (\"Adi\", \"Rating\", \"Butce(M)\", \"Yapim_yili\", \"Gise_sayisi\", \"Oyuncular\", \"Yonetmen\", \"Filmin_turu\") " +
                               "VALUES (@Adi, @Rating, @Butce, @YapimYili, @GiseSayisi, @Oyuncular, @Yonetmen, @FilminTuru)";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@Adi", filmAdi);
                komut.Parameters.AddWithValue("@Rating", rating);
                komut.Parameters.AddWithValue("@Butce", butce);
                komut.Parameters.AddWithValue("@YapimYili", yapimYili);
                komut.Parameters.AddWithValue("@GiseSayisi", giseSayisi);
                komut.Parameters.AddWithValue("@Oyuncular", oyuncular);
                komut.Parameters.AddWithValue("@Yonetmen", yonetmen);
                komut.Parameters.AddWithValue("@FilminTuru", filmTuru);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                // Update DataGridView
                Grid1Guncelle();

                MessageBox.Show("The movie has been added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }

        }

        private void label22_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the movie name to be deleted from the user
                string filmAdi = Interaction.InputBox("Enter Movie Name to Delete:", "Delete Film", "");

                if (string.IsNullOrWhiteSpace(filmAdi))
                {

                    MessageBox.Show("Movie name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Deleting process
                string sorgu = "DELETE FROM public.filmler WHERE \"Adi\" = @Adi";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@Adi", filmAdi);

                baglanti.Open();
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                baglanti.Close();

                if (etkilenenSatirSayisi > 0)
                {
                    // Update DataGridView
                    Grid1Guncelle();
                    MessageBox.Show("Movie deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No movie found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label25_Click(object sender, EventArgs e)
        {
            try
            {
                // Get top 10 movies in order of rating
                string sorgu = "SELECT * FROM public.filmler ORDER BY \"Rating\" DESC LIMIT 10";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Grid3Guncelle();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Grid2Guncelle();
        }



        private string GetBilgiler(string yonetmenAdi)
        {
            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID = postgres; Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT \"Alinan_Odul\" FROM yonetmenler WHERE \"Adi\" = @YonetmenAdi";
                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder bilgiler = new StringBuilder();
                        while (reader.Read())
                        {
                            bilgiler.AppendLine(reader["Alinan_Odul"].ToString());
                        }

                        // Call the method that adds the actors and movie numbers the director worked with


                        return bilgiler.ToString();
                    }
                }
            }
        }

        private string GetOyuncuVeFilmBilgileri1(string yonetmenAdi)

        {  //Shows the films made by the director and the actors he worked with in the films he made.

            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID=postgres;Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT filmler.\"Adi\" as \"Adi\", filmler.\"Oyuncular\", COUNT(*) as \"FilmSayisi\" " +
                               "FROM filmler " +
                               "INNER JOIN yonetmenler ON yonetmenler.\"Adi\" = filmler.\"Yonetmen\" " +
                               "WHERE yonetmenler.\"Adi\" = @YonetmenAdi " +
                               "GROUP BY filmler.\"Adi\", filmler.\"Oyuncular\"";

                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder oyuncuVeFilmBilgileri = new StringBuilder();
                        while (reader.Read())
                        {
                            string filmAdi = reader["Adi"].ToString();
                            string oyuncular = reader["Oyuncular"].ToString();


                            oyuncuVeFilmBilgileri.AppendLine($"Movie name: {filmAdi}, Actors: {oyuncular}");
                        }

                        return oyuncuVeFilmBilgileri.ToString();
                    }
                }
            }
        }
        private string GetOyuncuVeFilmBilgileri2(string yonetmenAdi)
        {

            //Writes how many times you worked with the actor.

            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID=postgres;Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT unnest(string_to_array(filmler.\"Oyuncular\", ' ')) as \"OyuncuAdi\", COUNT(*) as \"FilmSayisi\" " +
                               "FROM filmler " +
                               "INNER JOIN yonetmenler ON yonetmenler.\"Adi\" = filmler.\"Yonetmen\" " +
                               "WHERE yonetmenler.\"Adi\" = @YonetmenAdi " +
                               "GROUP BY \"OyuncuAdi\"";

                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder oyuncuVeFilmBilgileri = new StringBuilder();
                        while (reader.Read())
                        {
                            string oyuncuAdi = reader["OyuncuAdi"].ToString();
                            int filmSayisi = Convert.ToInt32(reader["FilmSayisi"]);

                            oyuncuVeFilmBilgileri.AppendLine($"Actor: {oyuncuAdi}, Number of Films Worked on Together: {filmSayisi}");
                        }

                        return oyuncuVeFilmBilgileri.ToString();
                    }
                }
            }
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)//We can access the manager's detailed information by clicking on any manager row (via Messagebox)

        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string yonetmenAdi = row.Cells["Adi"].Value.ToString();

                    string bilgiler = GetBilgiler(yonetmenAdi) + "\n\n" + GetOyuncuVeFilmBilgileri1(yonetmenAdi) + "\n\n" + GetOyuncuVeFilmBilgileri2(yonetmenAdi);


                    if (!string.IsNullOrWhiteSpace(bilgiler))
                    {
                        MessageBox.Show($"Awards Received by the Director:\n\n{bilgiler}", "Director Information");

                    }
                    else
                    {
                        MessageBox.Show("This director has no awards.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        private string GetBilgilerOyuncu(string oyuncuAdi)
        {
            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID = postgres; Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT \"Alinan_Odul\" FROM oyuncular WHERE \"Adi\" = @OyuncuAdi";
                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder bilgiler = new StringBuilder();
                        while (reader.Read())
                        {
                            bilgiler.AppendLine(reader["Alinan_Odul"].ToString());
                        }

                        //Awards received by the actor are displayed


                        return bilgiler.ToString();
                    }
                }
            }
        }


        private string GetOyuncuVeBeraberOynadigiBilgiler(string oyuncuAdi)// Which actor the actor has played with is shown, and the movies he starred in are also shown.

        {
            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID=postgres;Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT filmler.\"Adi\" as \"FilmAdi\", filmler.\"Oyuncular\" as \"BeraberOynadigiOyuncular\" " +
                               "FROM filmler " +
                               "INNER JOIN oyuncular ON POSITION(oyuncular.\"Adi\" IN filmler.\"Oyuncular\") > 0 " +
                               "WHERE oyuncular.\"Adi\" = @OyuncuAdi";

                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder oyuncuVeBeraberOynadigiBilgiler = new StringBuilder();
                        while (reader.Read())
                        {
                            string filmAdi = reader["FilmAdi"].ToString();
                            string beraberOynadigiOyuncular = reader["BeraberOynadigiOyuncular"].ToString();

                            // Oyuncunun kendi adını listeden çıkar
                            beraberOynadigiOyuncular = beraberOynadigiOyuncular.Replace(oyuncuAdi, "").Trim();

                            oyuncuVeBeraberOynadigiBilgiler.AppendLine($"Movie Name: {filmAdi}, Actors He Played With: {beraberOynadigiOyuncular}");
                        }

                        return oyuncuVeBeraberOynadigiBilgiler.ToString();
                    }
                }
            }
        }


        private string GetBeraberOynadigiOyuncularVeFilmSayisi(string oyuncuAdi)// This shows how many movies he worked with the actor named ...

        {
            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID=postgres;Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT unnest(string_to_array(filmler.\"Oyuncular\", ' ')) as \"OyuncuAdi\", COUNT(*) as \"FilmSayisi\" " +
                               "FROM filmler " +
                               "INNER JOIN oyuncular ON POSITION(oyuncular.\"Adi\" IN filmler.\"Oyuncular\") > 0 " +
                               "WHERE oyuncular.\"Adi\" = @OyuncuAdi AND filmler.\"Oyuncular\" != @OyuncuAdi " +
                               "GROUP BY \"OyuncuAdi\" " +
                               "ORDER BY \"FilmSayisi\" DESC";

                using (NpgsqlCommand command = new NpgsqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder oyuncuVeFilmBilgileri = new StringBuilder();
                        while (reader.Read())
                        {
                            string oyuncuAdi_calisilan = reader["OyuncuAdi"].ToString();
                            int filmSayisi = Convert.ToInt32(reader["FilmSayisi"]);

                            if (!oyuncuAdi_calisilan.Equals(oyuncuAdi))
                            {
                                oyuncuVeFilmBilgileri.AppendLine($"Actor: {oyuncuAdi_calisilan}, Number of Films Worked on Together: {filmSayisi}");
                            }
                        }

                        return oyuncuVeFilmBilgileri.ToString();
                    }
                }
            }
        }





        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




            if (e.RowIndex >= 0) // Eğer satıra tıklanmışsa (başlık satırı değilse)
            {
                DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];
                string oyuncuAdi = selectedRow.Cells["Adi"].Value.ToString();

                // İlgili oyuncuAdi'ni kullanarak bilgileri çek ve MessageBox'a yazdır
                string oyuncuBilgileri = "Awards Received by the Actor:" + GetBilgilerOyuncu(oyuncuAdi) + "\n\n" + GetOyuncuVeBeraberOynadigiBilgiler(oyuncuAdi) + "\n\n" + GetBeraberOynadigiOyuncularVeFilmSayisi(oyuncuAdi);
                MessageBox.Show(oyuncuBilgileri, "Actor Information");
            }





        }

        private DataTable BirdenCokFilmdeOynayanOyuncular()//Birden çok oyunda oynayanların listesi ekranda gösterilir
        {
            string connectionString = "server=localhost;port=5432;Database=hastalar;user ID=postgres;Password=12345";
            using (NpgsqlConnection baglanti = new NpgsqlConnection(connectionString))
            {
                baglanti.Open();

                string query = "SELECT oyuncular.\"Adi\", COUNT(*) as \"FilmSayisi\" " +
                               "FROM oyuncular " +
                               "INNER JOIN filmler ON POSITION(oyuncular.\"Adi\" IN filmler.\"Oyuncular\") > 0 " +
                               "GROUP BY oyuncular.\"Adi\" " +
                               "HAVING COUNT(*) > 1";  // Select actors who have appeared in more than one movie

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, baglanti))
                {
                    DataTable oyuncularDataTable = new DataTable();
                    adapter.Fill(oyuncularDataTable);

                    return oyuncularDataTable;
                }
            }
        }

        private void label34_Click(object sender, EventArgs e)//Button of the above method
        {
            DataTable oyuncularDataTable = BirdenCokFilmdeOynayanOyuncular();

            // Sort DataTable by number of movies from largest to smallest
            oyuncularDataTable.DefaultView.Sort = "FilmSayisi DESC";

            // Load sorted DataTable into DataGridView
            dataGridView3.DataSource = oyuncularDataTable.DefaultView.ToTable();
        }



        private void YonetmenSil(string yonetmenAdi)
        {
            try
            {
                // Önce yoneticinin yönettiği filmleri bul
                string filmSilmeSorgu = "DELETE FROM filmler WHERE \"Yonetmen\" = @YonetmenAdi";
                using (NpgsqlCommand filmSilmeKomut = new NpgsqlCommand(filmSilmeSorgu, baglanti))
                {
                    filmSilmeKomut.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);
                    baglanti.Open();
                    filmSilmeKomut.ExecuteNonQuery();
                }

                // Then delete the director itself
                string yonetmenSilmeSorgu = "DELETE FROM yonetmenler WHERE \"Adi\" = @YonetmenAdi";
                using (NpgsqlCommand yonetmenSilmeKomut = new NpgsqlCommand(yonetmenSilmeSorgu, baglanti))
                {
                    yonetmenSilmeKomut.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);
                    yonetmenSilmeKomut.ExecuteNonQuery();
                }

                Grid1Guncelle(); // Update DataGridView
                MessageBox.Show("The director and affiliated movies have been successfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }
        private void label41_Click(object sender, EventArgs e)
        {


            try
            {
                // Get director name from user
                string yonetmenAdi = Interaction.InputBox("Enter the name of the director you want to delete:", "Delete Director", "");

                if (string.IsNullOrWhiteSpace(yonetmenAdi))
                {
                    MessageBox.Show("You must enter a valid director name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Deleting director method
                YonetmenSil(yonetmenAdi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }






        //add director
        private void label39_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan yeni yönetmen bilgilerini al
                string sahneAdi = Interaction.InputBox("Enter Stage Name:", "Add New Director", "");
                string sahneSoyadi = Interaction.InputBox("Enter Stage Surname:", "Add New Director", "");
                string yonetmenAdi = Interaction.InputBox("Enter Director Name:", "Add New Director", "");
                string yonetmenSoyadi = Interaction.InputBox("Enter Director Surname:", "Add New Director", "");
                DateTime dogumTarihi = Convert.ToDateTime(Interaction.InputBox("Enter Birthday (YYYY-MM-DD):", "Add New Director", ""));
                string cinsiyet = Interaction.InputBox("Enter Gender:", "Add New Director", "");
                string memleketi = Interaction.InputBox("Enter hometown:", "Add New Director", "");
                string alinanOdul = Interaction.InputBox("Enter the Award Received:", "Add New Director", "");

                // Veritabanına yeni yönetmen ekleyin
                string sorgu = "INSERT INTO public.yonetmenler (\"Sahne_adi\", \"Sahne_soyadi\", \"Adi\", \"Soyadi\", \"Dogum_Tarihi\", \"Cinsiyeti\", \"Memleketi\", \"Alinan_Odul\") " +
                               "VALUES (@SahneAdi, @SahneSoyadi, @YonetmenAdi, @YonetmenSoyadi, @DogumTarihi, @Cinsiyet, @Memleket, @AlinanOdul)";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@SahneAdi", sahneAdi);
                komut.Parameters.AddWithValue("@SahneSoyadi", sahneSoyadi);
                komut.Parameters.AddWithValue("@YonetmenAdi", yonetmenAdi);
                komut.Parameters.AddWithValue("@YonetmenSoyadi", yonetmenSoyadi);
                komut.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@Memleket", memleketi);
                komut.Parameters.AddWithValue("@AlinanOdul", alinanOdul);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                //Update DGW
                Grid2Guncelle();

                MessageBox.Show("Director added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }

        }




        //Add Actor
        private void label45_Click(object sender, EventArgs e)
        {


            try
            {
                // Get new actor information from user
                string sahneAdi = Interaction.InputBox("Enter Stage Name:", "Add Actor", "");
                string sahneSoyadi = Interaction.InputBox("Enter Stage Surname:", "Add Actor", "");
                string oyuncuAdi = Interaction.InputBox("Enter Actor Name:", "Add Actor", "");
                string oyuncuSoyadi = Interaction.InputBox("Enter Actor Surname:", "Add Actor", "");
                DateTime dogumTarihi = Convert.ToDateTime(Interaction.InputBox("Enter Birthday (YYYY-MM-DD):", "Add Actor", ""));
                string cinsiyet = Interaction.InputBox("Enter Gender:", "Add Actor", "");
                string memleketi = Interaction.InputBox("Enter Hometown:", "Add Actor", "");
                string alinanOdul = Interaction.InputBox("Enter the Award Received:", "Add Actor", "");

                // Veritabanına yeni oyuncu ekleyin
                string sorgu = "INSERT INTO public.oyuncular (\"Sahne_adi\", \"Sahne_soyadi\", \"Adi\", \"Soyadi\", \"Dogum_Tarihi\", \"Cinsiyeti\", \"Memleketi\",\"Alinan_Odul\") " +
                               "VALUES (@SahneAdi, @SahneSoyadi, @YonetmenAdi, @YonetmenSoyadi, @DogumTarihi, @Cinsiyet, @Memleket,@AlinanOdul)";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@SahneAdi", sahneAdi);
                komut.Parameters.AddWithValue("@SahneSoyadi", sahneSoyadi);
                komut.Parameters.AddWithValue("@YonetmenAdi", oyuncuAdi);
                komut.Parameters.AddWithValue("@YonetmenSoyadi", oyuncuSoyadi);
                komut.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@Memleket", memleketi);
                komut.Parameters.AddWithValue("@AlinanOdul", alinanOdul);


                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();


                Grid3Guncelle();

                MessageBox.Show("Actor added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }





        }


        //Delete Player

        private void OyuncuSil(string oyuncuAdi)
        {
            try
            {
                // Oyuncuyu oyuncular tablosundan sil
                string oyuncuSilSorgu = "DELETE FROM public.oyuncular WHERE \"Adi\" = @OyuncuAdi";
                NpgsqlCommand oyuncuSilKomut = new NpgsqlCommand(oyuncuSilSorgu, baglanti);
                oyuncuSilKomut.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);

                baglanti.Open();
                oyuncuSilKomut.ExecuteNonQuery();
                baglanti.Close();

                // Filmler tablosundan oyuncular kısmında bu oyuncuyu sil
                string filmSilSorgu = "UPDATE public.filmler SET \"Oyuncular\" = REPLACE(\"Oyuncular\", @OyuncuAdi, '')";
                NpgsqlCommand filmSilKomut = new NpgsqlCommand(filmSilSorgu, baglanti);
                filmSilKomut.Parameters.AddWithValue("@OyuncuAdi", oyuncuAdi);

                baglanti.Open();
                filmSilKomut.ExecuteNonQuery();
                baglanti.Close();

                // DataGridView'ları güncelle
                Grid1Guncelle();
                Grid3Guncelle();

                MessageBox.Show("Player deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }



        private void label47_Click(object sender, EventArgs e)
        {


            try
            {
                // Get actor name from user

                string oyuncuAdi = Interaction.InputBox("Enter the name of the actor you want to delete:", "Delete Actor ", "");

                if (string.IsNullOrWhiteSpace(oyuncuAdi))
                {
                    MessageBox.Show("You must enter a valid actor name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //player delete function
                OyuncuSil(oyuncuAdi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }


}

