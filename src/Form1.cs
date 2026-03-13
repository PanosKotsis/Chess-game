using Perfect_line;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        Label title;
        Button play;
        Button createPlayers;
        TextBox playerName1;
        TextBox playerName2;
        Button back;
        Player p1;
        Player p2;
        ComboBox color1;
        ComboBox color2;
        Button save;
        public Form1()
        {
            InitializeComponent();
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            this.BackgroundImage = Image.FromFile("Pictures/chess_game.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.WindowState = FormWindowState.Maximized;

            title = new Label();
            title.Text = "Chess";
            title.Font = new Font("Microsoft YaHei", 30F, FontStyle.Bold);
            title.Location = new Point(20, 20);
            title.Size = new Size(140, 60);
            this.Controls.Add(title);

            color1 = new ComboBox();
            color1.Items.Add("Black");
            color1.Items.Add("White");
            color1.Location = new Point(200, 180);
            color1.Size = new Size(100, 50);
            color1.Enabled = false;
            color1.Visible = false;
            color1.Text = "Black";
            this.Controls.Add(color1);

            color2 = new ComboBox();
            color2.Items.Add("Black");
            color2.Items.Add("White");
            color2.Location = new Point(350, 180);
            color2.Size = new Size(100, 50);
            color2.Enabled = false;
            color2.Visible = false;
            color2.Text = "White";
            this.Controls.Add(color2);

            play = CreateButton(new Point(20, 100), "Play game");
            play.Size = new Size(100, 70);
            play.Enabled = false;
            play.Click += playClick;
            this.Controls.Add(play);

            createPlayers = CreateButton(new Point(20, 180), "Name players");
            createPlayers.Size = new Size(100, 70);
            createPlayers.Click += createPlayersClick;
            this.Controls.Add(createPlayers);

            // Όνομα πρώτου παίτη
            playerName1 = new TextBox();
            playerName1.Location = new Point(200, 50);
            playerName1.Enabled = false;
            playerName1.Visible = false;
            this.Controls.Add(playerName1);

            // Όνομα δεύτερου παίκτη
            playerName2 = new TextBox();
            playerName2.Location = new Point(350, 50);
            playerName2.Enabled = false;
            playerName2.Visible = false;
            this.Controls.Add(playerName2);

            back = CreateButton(new Point(500, 100), "Back");
            back.Enabled = false;
            back.Visible = false;
            back.Size = new Size(100, 50);
            back.Click += backClick;
            this.Controls.Add(back);

            save = CreateButton(new Point(500, 180), "Save");
            save.Size = new Size(100, 50);
            save.Enabled = false;
            save.Visible = false;
            save.Click += saveClick;
            this.Controls.Add(save);
        }

        private Button CreateButton(Point Location, string text)
        {
            Button button = new Button();
            button.Location = Location;
            button.Text = text;
            button.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            button.Size = new Size(120, 50);

            return button;
        }

        private void playClick(object sender, EventArgs e)
        {
            if (p1.color == "Black")
            {
                // Τοποθετεί το όνομα του παίκτη με τα μαύρα πιόνια δίπλα από τα μαύρα πιόνια
                // Τοποθετεί το όνομα του παίκτη με τα άσπρα πιόνια δίπλα από τα άσπρα πιόνια
                Form2 f2 = new Form2(p1, p2);
                f2.Show();
            }
            else
            {
                // Τοποθετεί το όνομα του παίκτη με τα μαύρα πιόνια δίπλα από τα μαύρα πιόνια
                // Τοποθετεί το όνομα του παίκτη με τα άσπρα πιόνια δίπλα από τα άσπρα πιόνια
                Form2 f2 = new Form2(p2, p1);
                f2.Show();
            }
        }

        private void createPlayersClick(object sender, EventArgs e)
        {
            playerName1.Enabled = true;
            playerName1.Visible = true;

            playerName2.Enabled = true;
            playerName2.Visible = true;

            back.Enabled = true;
            back.Visible = true;

            color1.Enabled = true;
            color1.Visible = true;

            color2.Enabled = true;
            color2.Visible = true;

            save.Enabled = true;
            save.Visible = true;

            if (p1 == null)
            {
                // Δημιουργία πρώτου παίτη
                p1 = new Player(this, null, 0, null, new Point(200, 100));
            }

            if (p2 == null)
            {
                // Δημιουργία δεύτερου παίτη
                p2 = new Player(this, null, 0, null, new Point(350, 100));
            }
        }

        private void backClick(object sender, EventArgs e)
        {
            playerName1.Enabled = false;
            playerName1.Visible = false;

            playerName2.Enabled = false;
            playerName2.Visible = false;

            back.Enabled = false;
            back.Visible = false;

            color1.Enabled = false;
            color1.Visible = false;

            color2.Enabled = false;
            color2.Visible = false;

            save.Enabled = false;
            save.Visible = false;
        }

        private void saveClick(object sender, EventArgs e)
        {
            // Έλέγχει αν οι παίκτες έχουν επιλέξει το ίδιο χρώμα
            if (color1.SelectedItem == color2.SelectedItem)
            {
                MessageBox.Show("You cannot choose the same color");
                color1.SelectedIndex = 0;
                color2.SelectedIndex = 1;
            }
            else
            {
                p1.color = color1.SelectedItem.ToString();
                p2.color = color2.SelectedItem.ToString();

                p1.Name = playerName1.Text;
                p2.Name = playerName2.Text;

                play.Enabled = true;
            }
        }
    }
}
