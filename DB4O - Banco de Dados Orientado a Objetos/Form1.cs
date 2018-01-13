using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB4O___Banco_de_Dados_Orientado_a_Objetos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carro prod = new Carro();
            prod.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fornecedor forn = new Fornecedor();
            forn.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Funcionario func = new Funcionario();
            func.Show();
        }
    }
}
