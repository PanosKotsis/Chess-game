using Perfect_line;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Chess
{
    public partial class Form2 : Form
    {
        // Άσπρα πιόνια
        List<PictureBox> whitePieces = new List<PictureBox>();

        PictureBox whiteRook;
        PictureBox whiteRook2;
        PictureBox whiteKnight;
        PictureBox whiteKnight2;
        PictureBox whiteBishop;
        PictureBox whiteBishop2;
        PictureBox whiteKing;
        PictureBox whiteQueen;
        PictureBox whitePawn;
        PictureBox whitePawn2;
        PictureBox whitePawn3;
        PictureBox whitePawn4;
        PictureBox whitePawn5;
        PictureBox whitePawn6;
        PictureBox whitePawn7;
        PictureBox whitePawn8;


        // Μαύρα πιόνια
        List<PictureBox> blackPieces = new List<PictureBox>();

        PictureBox blackRook;
        PictureBox blackRook2;
        PictureBox blackKnight;
        PictureBox blackKnight2;
        PictureBox blackBishop;
        PictureBox blackBishop2;
        PictureBox blackKing;
        PictureBox blackQueen;
        PictureBox blackPawn;
        PictureBox blackPawn2;
        PictureBox blackPawn3;
        PictureBox blackPawn4;
        PictureBox blackPawn5;
        PictureBox blackPawn6;
        PictureBox blackPawn7;
        PictureBox blackPawn8;

        // Το πιόνι που μεταφέρεται
        PictureBox activePiece = null;

        bool moving = false;
        Point loc = new Point();
        Timer timer = new Timer();
        int remainingTime = 10;
        Label today;
        Label time;
        Label player1;
        Label player2;
        Player p1;
        Player p2;

        bool whiteTurn = true;
        string winner;
        private Database database;
        string dateTime;

        public Form2(Player player1, Player player2)
        {
            InitializeComponent();

            // Αρχικοποίηση των 2 παικτών
            p1 = player1;
            p2 = player2;

            InitializeCustomUI();

            timer.Start();
        }

        private void InitializeCustomUI()
        {
            timer.Tick += timer_Tick;
            timer.Interval = 1000;

            // Δείχνει ημερομηνία και ώρα
            dateTime = DateTime.Now.ToString("dd/MM/yyyy") + " " +  DateTime.Now.ToString("HH:mm");
            today= new Label();
            today.Location = new Point(20, 20);
            today.Size = new Size(150, 50);
            today.Text = dateTime;
            today.Font = new System.Drawing.Font("Microsoft YaHei", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            Controls.Add(today);

            // Δείχνει το χρονόμετρο
            time = new Label();
            time.Location = new Point(1250, 20);
            time.Size = new Size(50, 50);
            time.Text = remainingTime.ToString();
            time.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            Controls.Add(time);

            // Όνομα πρώτου παίτη
            player1 = new Label();
            player1.Text = p1.Name;
            player1.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            player1.Location = new Point(200, 50);
            Controls.Add(player1);

            // Όνονα δεύτερου παίκτη
            player2 = new Label();
            player2.Text = p2.Name;
            player2.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            player2.Location = new Point(200, 580);
            Controls.Add(player2);

            // Ταμπλό
            PictureBox board = CreatePicturebox(new Point(300, 20), new Size(696,696), "Pictures/chess_board.jpg");
            Controls.Add(board);

            // Μαύρος πύργος
            blackRook = CreatePicturebox(new Point(0, 0), new Size(87, 87), "Pictures/μαύρος_πύργος.png");
            board.Controls.Add(blackRook);
            blackRook.BringToFront();

            blackRook.MouseMove += Picturebox_MouseMove;
            blackRook.MouseUp += Picturebox_MouseUp;
            blackRook.MouseDown += Picturebox_MouseDown;

            // Μαύρος ίππος
            blackKnight = CreatePicturebox(new Point(87, 0), new Size(87, 87), "Pictures/μαύρος_ίππος.png");
            board.Controls.Add(blackKnight);
            blackKnight.BringToFront();

            blackKnight.MouseMove += Picturebox_MouseMove;
            blackKnight.MouseUp += Picturebox_MouseUp;
            blackKnight.MouseDown += Picturebox_MouseDown;

            // Μαύρος αξιωματικός
            blackBishop = CreatePicturebox(new Point(174, 0), new Size(87, 87), "Pictures/μαύρος_αξιωματικός.png");
            board.Controls.Add(blackBishop);
            blackBishop.BringToFront();

            blackBishop.MouseMove += Picturebox_MouseMove;
            blackBishop.MouseUp += Picturebox_MouseUp;
            blackBishop.MouseDown += Picturebox_MouseDown;

            // Μαύρος βασιλιάς
            blackKing = CreatePicturebox(new Point(261, 0), new Size(87, 87), "Pictures/μαύρος_βασιλιάς.png");
            board.Controls.Add(blackKing);
            blackKing.BringToFront();

            blackKing.MouseMove += Picturebox_MouseMove;
            blackKing.MouseUp += Picturebox_MouseUp;
            blackKing.MouseDown += Picturebox_MouseDown;

            // Μαύρη βασίλισσα
            blackQueen = CreatePicturebox(new Point(348, 0), new Size(87, 87), "Pictures/μαύρη_βασίλισσα.png");
            board.Controls.Add(blackQueen);
            blackQueen.BringToFront();

            blackQueen.MouseMove += Picturebox_MouseMove;
            blackQueen.MouseUp += Picturebox_MouseUp;
            blackQueen.MouseDown += Picturebox_MouseDown;

            // μαύρος αξιωματικός 2
            blackBishop2 = CreatePicturebox(new Point(435, 0), new Size(87, 87), "Pictures/μαύρος_αξιωματικός.png");
            board.Controls.Add(blackBishop2);
            blackBishop2.BringToFront();

            blackBishop2.MouseMove += Picturebox_MouseMove;
            blackBishop2.MouseUp += Picturebox_MouseUp;
            blackBishop2.MouseDown += Picturebox_MouseDown;


            // Μαύρος ίππος 2
            blackKnight2 = CreatePicturebox(new Point(522, 0), new Size(87, 87), "Pictures/μαύρος_ίππος.png");
            board.Controls.Add(blackKnight2);
            blackKnight2.BringToFront();

            blackKnight2.MouseMove += Picturebox_MouseMove;
            blackKnight2.MouseUp += Picturebox_MouseUp;
            blackKnight2.MouseDown += Picturebox_MouseDown;

            // Μαύρος πύργος 2
            blackRook2 = CreatePicturebox(new Point(609, 0), new Size(87, 87), "Pictures/μαύρος_πύργος.png");
            board.Controls.Add(blackRook2);
            blackRook2.BringToFront();

            blackRook2.MouseMove += Picturebox_MouseMove;
            blackRook2.MouseUp += Picturebox_MouseUp;
            blackRook2.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι
            blackPawn = CreatePicturebox(new Point(0, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn);
            blackPawn.BringToFront();

            blackPawn.MouseMove += Picturebox_MouseMove;
            blackPawn.MouseUp += Picturebox_MouseUp;
            blackPawn.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 2
            blackPawn2 = CreatePicturebox(new Point(87, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn2);
            blackPawn2.BringToFront();

            blackPawn2.MouseMove += Picturebox_MouseMove;
            blackPawn2.MouseUp += Picturebox_MouseUp;
            blackPawn2.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 3
            blackPawn3 = CreatePicturebox(new Point(174, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn3);
            blackPawn3.BringToFront();

            blackPawn3.MouseMove += Picturebox_MouseMove;
            blackPawn3.MouseUp += Picturebox_MouseUp;
            blackPawn3.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 4
            blackPawn4 = CreatePicturebox(new Point(261, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn4);
            blackPawn4.BringToFront();

            blackPawn4.MouseMove += Picturebox_MouseMove;
            blackPawn4.MouseUp += Picturebox_MouseUp;
            blackPawn4.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 5
            blackPawn5 = CreatePicturebox(new Point(348, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn5);
            blackPawn5.BringToFront();

            blackPawn5.MouseMove += Picturebox_MouseMove;
            blackPawn5.MouseUp += Picturebox_MouseUp;
            blackPawn5.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 6
            blackPawn6 = CreatePicturebox(new Point(435, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn6);
            blackPawn6.BringToFront();

            blackPawn6.MouseMove += Picturebox_MouseMove;
            blackPawn6.MouseUp += Picturebox_MouseUp;
            blackPawn6.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 7
            blackPawn7 = CreatePicturebox(new Point(522, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn7);
            blackPawn7.BringToFront();

            blackPawn7.MouseMove += Picturebox_MouseMove;
            blackPawn7.MouseUp += Picturebox_MouseUp;
            blackPawn7.MouseDown += Picturebox_MouseDown;

            // Μαύρο πιόνι 8
            blackPawn8 = CreatePicturebox(new Point(609, 87), new Size(87, 87), "Pictures/μαύρο_πιόνι.png");
            board.Controls.Add(blackPawn8);
            blackPawn8.BringToFront();

            blackPawn8.MouseMove += Picturebox_MouseMove;
            blackPawn8.MouseUp += Picturebox_MouseUp;
            blackPawn8.MouseDown += Picturebox_MouseDown;

            // Άσπρος πύργος
            whiteRook = CreatePicturebox(new Point(0, 609), new Size(87, 87), "Pictures/άσπρος_πύργος.png");
            board.Controls.Add(whiteRook);
            whiteRook.BringToFront();

            whiteRook.MouseMove += Picturebox_MouseMove;
            whiteRook.MouseUp += Picturebox_MouseUp;
            whiteRook.MouseDown += Picturebox_MouseDown;

            // Άσπρος ίππος
            whiteKnight = CreatePicturebox(new Point(87, 609), new Size(87, 87), "Pictures/άσπρος_ίππος.png");
            board.Controls.Add(whiteKnight);
            whiteKnight.BringToFront();

            whiteKnight.MouseMove += Picturebox_MouseMove;
            whiteKnight.MouseUp += Picturebox_MouseUp;
            whiteKnight.MouseDown += Picturebox_MouseDown;

            // Άσπρος αξιωματικός
            whiteBishop = CreatePicturebox(new Point(174, 609), new Size(87, 87), "Pictures/άσπρος_αξιωματικός.png");
            board.Controls.Add(whiteBishop);
            whiteBishop.BringToFront();

            whiteBishop.MouseMove += Picturebox_MouseMove;
            whiteBishop.MouseUp += Picturebox_MouseUp;
            whiteBishop.MouseDown += Picturebox_MouseDown;

            // Άσπρος βασιλιάς
            whiteKing = CreatePicturebox(new Point(261, 609), new Size(87, 87), "Pictures/άσπρος_βασιλιάς.png");
            board.Controls.Add(whiteKing);
            whiteKing.BringToFront();

            whiteKing.MouseMove += Picturebox_MouseMove;
            whiteKing.MouseUp += Picturebox_MouseUp;
            whiteKing.MouseDown += Picturebox_MouseDown;

            // Άσπρη βασίλισσα
            whiteQueen = CreatePicturebox(new Point(348, 609), new Size(87, 87), "Pictures/άσπρη_βασίλισσα.png");
            board.Controls.Add(whiteQueen);
            whiteQueen.BringToFront();

            whiteQueen.MouseMove += Picturebox_MouseMove;
            whiteQueen.MouseUp += Picturebox_MouseUp;
            whiteQueen.MouseDown += Picturebox_MouseDown;

            // Άσπρος αξιωματικός 2
            whiteBishop2 = CreatePicturebox(new Point(435, 609), new Size(87, 87), "Pictures/άσπρος_αξιωματικός.png");
            board.Controls.Add(whiteBishop2);
            whiteBishop2.BringToFront();

            whiteBishop2.MouseMove += Picturebox_MouseMove;
            whiteBishop2.MouseUp += Picturebox_MouseUp;
            whiteBishop2.MouseDown += Picturebox_MouseDown;

            // Άσπρος ίππος 2
            whiteKnight2 = CreatePicturebox(new Point(522, 609), new Size(87, 87), "Pictures/άσπρος_ίππος.png");
            board.Controls.Add(whiteKnight2);
            whiteKnight2.BringToFront();

            whiteKnight2.MouseMove += Picturebox_MouseMove;
            whiteKnight2.MouseUp += Picturebox_MouseUp;
            whiteKnight2.MouseDown += Picturebox_MouseDown;

            // Άσπρος πύργος 2
            whiteRook2 = CreatePicturebox(new Point(609, 609), new Size(87, 87), "Pictures/άσπρος_πύργος.png");
            board.Controls.Add(whiteRook2);
            whiteRook2.BringToFront();

            whiteRook2.MouseMove += Picturebox_MouseMove;
            whiteRook2.MouseUp += Picturebox_MouseUp;
            whiteRook2.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 
            whitePawn = CreatePicturebox(new Point(0, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn);
            whitePawn.BringToFront();

            whitePawn.MouseMove += Picturebox_MouseMove;
            whitePawn.MouseUp += Picturebox_MouseUp;
            whitePawn.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 2
            whitePawn2 = CreatePicturebox(new Point(87, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn2);
            whitePawn2.BringToFront();

            whitePawn2.MouseMove += Picturebox_MouseMove;
            whitePawn2.MouseUp += Picturebox_MouseUp;
            whitePawn2.MouseDown += Picturebox_MouseDown;


            // Άσπρος πιόνι 3
            whitePawn3 = CreatePicturebox(new Point(174, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn3);
            whitePawn3.BringToFront();

            whitePawn3.MouseMove += Picturebox_MouseMove;
            whitePawn3.MouseUp += Picturebox_MouseUp;
            whitePawn3.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 4
            whitePawn4 = CreatePicturebox(new Point(261, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn4);
            whitePawn4.BringToFront();

            whitePawn4.MouseMove += Picturebox_MouseMove;
            whitePawn4.MouseUp += Picturebox_MouseUp;
            whitePawn4.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 5
            whitePawn5 = CreatePicturebox(new Point(348, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn5);
            whitePawn5.BringToFront();

            whitePawn5.MouseMove += Picturebox_MouseMove;
            whitePawn5.MouseUp += Picturebox_MouseUp;
            whitePawn5.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 6
            whitePawn6 = CreatePicturebox(new Point(435, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn6);
            whitePawn6.BringToFront();

            whitePawn6.MouseMove += Picturebox_MouseMove;
            whitePawn6.MouseUp += Picturebox_MouseUp;
            whitePawn6.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 7
            whitePawn7 = CreatePicturebox(new Point(522, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn7);
            whitePawn7.BringToFront();

            whitePawn7.MouseMove += Picturebox_MouseMove;
            whitePawn7.MouseUp += Picturebox_MouseUp;
            whitePawn7.MouseDown += Picturebox_MouseDown;

            // Άσπρος πιόνι 8
            whitePawn8 = CreatePicturebox(new Point(609, 522), new Size(87, 87), "Pictures/άσπρο_πιόνι.png");
            board.Controls.Add(whitePawn8);
            whitePawn8.BringToFront();

            whitePawn8.MouseMove += Picturebox_MouseMove;
            whitePawn8.MouseUp += Picturebox_MouseUp;
            whitePawn8.MouseDown += Picturebox_MouseDown;

            whitePieces.Add(whiteRook);
            whitePieces.Add(whiteRook2);
            whitePieces.Add(whiteKnight);
            whitePieces.Add(whiteKnight2);
            whitePieces.Add(whiteBishop);
            whitePieces.Add(whiteBishop2);
            whitePieces.Add(whiteKing);
            whitePieces.Add(whiteQueen);
            whitePieces.Add(whitePawn);
            whitePieces.Add(whitePawn2);
            whitePieces.Add(whitePawn3);
            whitePieces.Add(whitePawn4);
            whitePieces.Add(whitePawn5);
            whitePieces.Add(whitePawn6);
            whitePieces.Add(whitePawn7);
            whitePieces.Add(whitePawn8);

            blackPieces.Add(blackRook);
            blackPieces.Add(blackRook2);
            blackPieces.Add(blackKnight);
            blackPieces.Add(blackKnight2);
            blackPieces.Add(blackBishop);
            blackPieces.Add(blackBishop2);
            blackPieces.Add(blackKing);
            blackPieces.Add(blackQueen);
            blackPieces.Add(blackPawn);
            blackPieces.Add(blackPawn2);
            blackPieces.Add(blackPawn3);
            blackPieces.Add(blackPawn4);
            blackPieces.Add(blackPawn5);
            blackPieces.Add(blackPawn6);
            blackPieces.Add(blackPawn7);
            blackPieces.Add(blackPawn8);
        }

        private PictureBox CreatePicturebox(Point location, Size size, string path) 
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = size;
            pictureBox.Location = location;
            pictureBox.Image = Image.FromFile(path);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackColor = Color.Transparent;

            return pictureBox;
        }

        private void Picturebox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X + e.X - loc.X,
                                                     ((PictureBox)sender).Location.Y + e.Y - loc.Y);
            }
        }

        private void Picturebox_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;

            // Ανανεώνεται ο χρόνος για την επόμενη κίνηση του επόμενου παίκτη
            remainingTime = 5;
            time.Text = remainingTime.ToString();

            if (activePiece == null)
            {
                return;
            }

            // Κεντράρει το πιόνι
            SnapToGrid(activePiece);

            if (whiteTurn)
            {
                if (whitePieces.Contains(activePiece))
                {
                    PictureBox black = blackPieces.FirstOrDefault(b => sameSquareGrid(activePiece, b));

                    if (black != null)
                    {
                        // Το άσπρο πιόνι τρώει το μαύρο
                        black.Visible = false;
                        blackPieces.Remove(black);
                    }

                    // Έρχεται η σειρά του παίκτη με τα μαύρα πιόνια
                    whiteTurn = false;
                }
            }
            else
            {
                if (blackPieces.Contains(activePiece))
                {
                    PictureBox white = whitePieces.FirstOrDefault(w => sameSquareGrid(activePiece, w));

                    if (white != null)
                    {
                        // Το μαύρο πιόνι τρώει το άσπρο
                        white.Visible = false;
                        whitePieces.Remove(white);
                    }

                    // Έρχεται η σειρά του παίκτη με τα άσπρα πιόνια
                    whiteTurn = true;
                }
            }

            if (whitePieces.Count == 0)
            {
                // Ο παίκτης με τα μαύρα πιόνια κερδίζει
                timer.Stop();
                time.Enabled = false;
                time.Visible = false;
                winner = p1.Name;
                MessageBox.Show("Player " + winner + " won");

                // Αποθήκευση παιχνιδιού
                database.InsertRow(p1.Name, p2.Name, winner, DateTime.Now.ToString("HH:mm:ss"), dateTime);
            }
            else if (blackPieces.Count == 0)
            {
                // Ο παίκτης με τα άσπρα πιόνια κερδίζει
                timer.Stop();
                time.Enabled = false;
                time.Visible = false;
                winner = p2.Name;
                MessageBox.Show("Player " + winner + " won");

                // Αποθήκευση παιχνιδιού
                database.InsertRow(p1.Name, p2.Name, winner, DateTime.Now.ToString("HH:mm:ss"), dateTime);
            }
        }

        private void Picturebox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb == null)
            {
                return;
            }
            
            // Ελέγχει ποιος παίκτης έχει σειρά
            if (whiteTurn && !whitePieces.Contains(pb))
            {
                return;
            }

            if (!whiteTurn && !blackPieces.Contains(pb))
            {
                return; 
            }

            moving = true;
            loc = e.Location;

            // Ελέγχει ποιο πιόνι κινήθηκε και το φέρνει μπροστά 
            activePiece = pb;
            pb.BringToFront();

            remainingTime = 5;
            time.Text = remainingTime.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            today.Text = DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm");

            remainingTime--;
            time.Text = remainingTime.ToString();

            if (remainingTime == 0)
            {
                // Αλλαγή σειράς όταν λήξει το χρονόμετρο
                whiteTurn = !whiteTurn;

                remainingTime = 5;
                time.Text = remainingTime.ToString();
            }
        }

        private bool sameSquareGrid(PictureBox a, PictureBox b)
        {
            // Υπολογίζει σε ποιο τετράγωνο βρίσκεται πάνω το πιόνι 
            int size = 87;

            int ax = a.Left / size;
            int ay = a.Top / size;

            int bx = b.Left / size;
            int by = b.Top / size;

            return ax == bx && ay == by;
        }

        private void SnapToGrid(PictureBox pb)
        {
            // Κεντράρει το πιόνι στο τετράγωνο που βρίσκεται
            int size = 87;

            int x = (int)Math.Round(pb.Left / (double)size) * size;
            int y = (int)Math.Round(pb.Top / (double)size) * size;

            x = Math.Max(0, Math.Min(7 * size, x));
            y = Math.Max(0, Math.Min(7 * size, y));

            pb.Location = new Point(x, y);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Δημιουργία βάσης δεδομένων
            string connectionString = @"Data Source=database.db;Version=3";
            database = new Database(connectionString);
            database.CreateTable();
        }
    }
}
