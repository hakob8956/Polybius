using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolibusLogic;
namespace PolibusGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            string key = MyPolibus.Normalize(txtKey1.Text);
            string text = txtText1.Text.Replace(" ", string.Empty).ToLower();
            var keyArray = MyPolibus.CreateMatrixWithKey(key);
            var indexArray = MyPolibus.CreateIndexArray(text, keyArray);
            answer1.Text = MyPolibus.getEncrypedText(indexArray, keyArray);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string key = MyPolibus.Normalize(txtKey2.Text);
            string text = txtText2.Text.Replace(" ", string.Empty).ToLower();
            var keyArray = MyPolibus.CreateMatrixWithKey(key);
            answer2.Text = MyPolibus.getDecodeText(text, keyArray);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
