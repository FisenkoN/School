using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using School.DAL.EF.Repository;

namespace App
{
    public partial class Form1 : Form
    {
        private StudentRepository Repository;
        public Form1()
        {
            InitializeComponent();
            Repository = new StudentRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var student in Repository.GetAll())
            {
                listBox1.Items.Add(student.FullName);
            }
        }
    }
}