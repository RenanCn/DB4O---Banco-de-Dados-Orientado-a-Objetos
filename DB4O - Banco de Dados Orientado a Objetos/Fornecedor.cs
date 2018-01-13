using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace DB4O___Banco_de_Dados_Orientado_a_Objetos
{
    public partial class Fornecedor : Form
    {

        IObjectContainer DB;

        public void exibir()
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            IList<classeFornecedor> resultFornecedor = DB.Query<classeFornecedor>();

            if (resultFornecedor.Count > 0)
            {
                foreach (classeFornecedor P in resultFornecedor)
                {
                    dataGridView1.DataSource = resultFornecedor.ToArray();
                }

            }

            DB.Close();
        }

        public void limpar()
        {
            maskedTextBox1.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        // fim funções

        public Fornecedor()
        {
            InitializeComponent();
            exibir();
            limpar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");

            classeFornecedor f = new classeFornecedor()
            {
                cnpjFornecedor = maskedTextBox1.Text,
                nomeFornecedor = textBox1.Text,
                telefoneFornecedor = textBox2.Text,
                emailFornecedor = textBox3.Text
            };
            DB.Store(f);
            MessageBox.Show("Fornecedor adicionado com sucesso!");
            DB.Close();
            exibir();
            limpar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exibir();
            limpar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            string parametro = maskedTextBox1.Text;
            try
            {
                IList<classeFornecedor> resultFornecedor = DB.Query<classeFornecedor>(P => P.cnpjFornecedor == parametro);
                foreach (classeFornecedor item in resultFornecedor)
                {
                    DB.Delete(item);
                    MessageBox.Show("Fornecedor removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fornecedor não encontrado!");
            }
            finally
            {
                DB.Close();
                exibir();
                limpar();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");

            try
            {
                classeFornecedor forn = DB.Query<classeFornecedor>(P => P.cnpjFornecedor.Equals(maskedTextBox1.Text)).Single();
                forn.cnpjFornecedor = maskedTextBox1.Text;
                forn.nomeFornecedor = textBox1.Text;
                forn.telefoneFornecedor = textBox2.Text;
                forn.emailFornecedor = textBox3.Text;

                DB.Store(forn);
                DB.Commit();
            }
            finally
            {
                DB.Close();
                exibir();
                limpar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            limpar();
        }
    }
}
