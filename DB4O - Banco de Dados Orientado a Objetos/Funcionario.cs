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
    public partial class Funcionario : Form
    {
        IObjectContainer DB;

        public void exibir()
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            IList<classeFuncionario> resultFuncionario = DB.Query<classeFuncionario>();

            if (resultFuncionario.Count > 0)
            {
                foreach (classeFuncionario P in resultFuncionario)
                {
                    dataGridView1.DataSource = resultFuncionario.ToArray();
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



        public Funcionario()
        {
            InitializeComponent();
            exibir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");

            classeFuncionario f = new classeFuncionario()
            {
                cpfFuncionario = maskedTextBox1.Text,
                nomeFuncionario = textBox1.Text,
                telefoneFuncionario = textBox2.Text,
                cargoFuncionario = comboBox1.Text,
                salarioFuncionario = Convert.ToDouble(textBox3.Text)
            };

            DB.Store(f);
            MessageBox.Show("Funcionário adicionado com sucesso!");
            DB.Close();
            exibir();
            limpar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exibir();
            limpar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            string parametro = maskedTextBox1.Text;
            try
            {
                IList<classeFuncionario> resultFuncionario = DB.Query<classeFuncionario>(P => P.cpfFuncionario == parametro);
                foreach (classeFuncionario item in resultFuncionario)
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
                classeFuncionario func = DB.Query<classeFuncionario>(P => P.cpfFuncionario.Equals(maskedTextBox1.Text)).Single();
                func.cpfFuncionario = maskedTextBox1.Text;
                func.nomeFuncionario = textBox1.Text;
                func.telefoneFuncionario = textBox2.Text;
                func.cargoFuncionario = comboBox1.Text;
                func.salarioFuncionario = Convert.ToDouble(textBox3.Text);

                DB.Store(func);
                DB.Commit();
            }
            finally
            {
                DB.Close();
                exibir();
                limpar();
            }
        }
    }
}
