﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KalkulatorValuta
{
    public partial class KalkulatorValuta : Form
    {
        string ConnString = "Data Source=MASTER2\\SQLEXPRESS;Initial Catalog=ValuteDB;Integrated Security=True;Encrypt=False;";
        public KalkulatorValuta()
        {
            InitializeComponent();
        }

        private void KalkulatorValuta_Load(object sender, EventArgs e)
        {
            string upit = "SELECT Name from Currencies";
            SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            SqlCommand command = new SqlCommand(upit, conn);
            SqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Name"].ToString());
                comboBox2.Items.Add(reader["Name"].ToString());
            }




            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string unos = textBox1.Text;
            string valuta = comboBox2.Text;
            double rez;

            rez=GetExchangeRates(valuta);
            textBox2.Text = rez.ToString();
        }
    

        private double GetExchangeRates(string valuta)
        {
            string upit = "select Value from Currencies where Name=@valuta";
            SqlConnection conn = new SqlConnection(ConnString);
            double tecaj = 0;
            conn.Open();

            SqlCommand command = new SqlCommand(upit, conn);

            command.Parameters.AddWithValue("@valuta", valuta);

            object result=command.ExecuteScalar();
            if(result!=null && double.TryParse(result.ToString(),out tecaj))
            {
                return tecaj;
            }

            conn.Close();
            
            
            return 0;
        }

    }
}
