using Chess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perfect_line
{
    // Η κλάση μπορεί να κάνει serialize
    [Serializable]
    public class Player
    {
        public string name;
        public float score;
        public string color;
        public Point location;

        // Το στγκεκριμένο label δεν αποθηκεύεται, δεν κάνει serialize
        [NonSerialized]
        Label labelName;

        public string Name
        {
            set
            {
                name = value;
                updatePlayerDetails();
            }
            get
            {
                return name;
            }
        }

        public Player() { }

        public Player(Form1 form, string name, float score, string color, Point location)
        {
            // Δημιουργία παίκτη
            labelName = new Label();
            labelName.Location = location;
            labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelName.Size = new Size(100, 70);
            this.name = name;
            this.score = score;
            this.color = color;
            form.Controls.Add(labelName);
            labelName.Text = "Name: " + name;
        }

        public void updatePlayerDetails()
        {
            // Αλλάζει το όνομα του παίκτη
            if (labelName != null)
            {
                labelName.Text = "Name: " + name;
            }
        }

        public override string ToString()
        {
            return "Name: " + name + ", Percentage: " + score;
        }
    }
}
