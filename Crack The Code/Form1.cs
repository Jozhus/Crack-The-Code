using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crack_The_Code
{
    public partial class Form1 : Form
    {
        int round = 0;
        int digit = 0;
        int[] numbers;
        Label[] digits;
        static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            digits = new Label[3] { label1, label2, label3 };
            numbers = createNumber();
            foreach (var button in Controls.OfType<Button>())
            {
                button.Click += button_Click;
            }
        }

        int[] createNumber()
        {
            int[] newDigits = new int[3];
            for (int i = 0; i < 3; ++i)
            {
                int newDigit;
                do
                {
                    newDigit = random.Next(1, 10);
                    Console.WriteLine(newDigit);
                }
                while (newDigits.Contains(newDigit));
                newDigits[i] = newDigit;
            }
            return newDigits;
        }

        void check()
        {
            int exacts = 0;
            int exists = 0;
            for (int i = 0; i < 3; ++i)
            {
                if (numbers[i] == int.Parse(digits[i].Text)) exacts++;
                else if (numbers.Contains(int.Parse(digits[i].Text))) exists++;
            }
            string[] arr = new string[3];
            arr[0] = digits[0].Text + digits[1].Text + digits[2].Text;
            arr[1] = exacts.ToString();
            arr[2] = exists.ToString();
            ListViewItem itm = new ListViewItem(arr);

            itm.UseItemStyleForSubItems = false;

            if (exacts > 0) itm.SubItems[1].ForeColor = System.Drawing.Color.Green;
            if (exists > 0) itm.SubItems[2].ForeColor = System.Drawing.Color.Orange;

            listView1.Items.Add(itm);


            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            foreach (var label in digits)
            {
                label.Text = "";
            }

            if (exacts == 3)
            {
                MessageBox.Show("You win", "Winner", MessageBoxButtons.OK);
                reset();
            }
            
            if (round > 7)
            {
                MessageBox.Show("You lose", "Loser", MessageBoxButtons.OK);
                reset();
            }

            digit = 0;
            round++;
        }

        void reset()
        {
            round = 0;
            digit = 0;
            numbers = createNumber();

            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            foreach (var label in digits)
            {
                label.Text = "";
            }

            listView1.Items.Clear();
        }

        void button_Click(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text.Equals("Reset"))
            {
                reset();
            }
            else
            {
                digits[digit].Text = button.Text;
                button.Enabled = false;
                button10.Focus();
                digit++;
                if (digit > 2) check();
            }
        }
    }
}
