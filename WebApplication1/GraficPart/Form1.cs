using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PsClient;

namespace GraficPart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            while (PsClientApp.connected==true)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.ImageLocation = "C:\\Users\\Asus\\Desktop\\III\\SEM. II\\PS- IERCAN\\pompe.png";

        }

    }
}
