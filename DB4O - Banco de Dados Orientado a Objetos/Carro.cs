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
    public partial class Carro : Form
    {
        IObjectContainer DB;

        public void exibir()
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            IList<classeCarro> resultCarro = DB.Query<classeCarro>();

            if (resultCarro.Count > 0)
            {
                StringBuilder car = new StringBuilder();
            //    int i=0;
                foreach (classeCarro P in resultCarro)
                {
                    dataGridView1.DataSource = resultCarro.ToArray();
               //     MessageBox.Show(resultCarro[i++].codCarro.ToString());

                }

            }

            DB.Close();
        }

        public void recuperaFornecedor()
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            IList<classeFornecedor> resultFornecedor = DB.Query<classeFornecedor>();

            if (resultFornecedor.Count > 0)
            {
                int i = 0;
                foreach (classeFornecedor P in resultFornecedor)
                {
                    comboBox1.Items.Add(resultFornecedor[i++].nomeFornecedor.ToString());
                }

            }

            DB.Close();
        }

        public void limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 1;
            comboBox1.Items.Clear();
        }

        // fim funções

        public Carro()
        {
            InitializeComponent();
            exibir();
            recuperaFornecedor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");

            classeCarro p = new classeCarro()
            {
                codCarro = textBox1.Text,
                nomeCarro = textBox2.Text,
                precoCarro = Convert.ToDouble(textBox3.Text),
                quantidadeCarro = (int)numericUpDown1.Value,
                fornecedorCarro = comboBox1.Text
            };
            DB.Store(p);
            MessageBox.Show("Carro adicionado com sucesso!");
            DB.Close();
            exibir();
            limpar();
            recuperaFornecedor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exibir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");
            string parametro = textBox1.Text;
            try
            {
                IList<classeCarro> resultCarro = DB.Query<classeCarro>(P => P.codCarro == parametro);
                foreach (classeCarro item in resultCarro)
                {
                    DB.Delete(item);
                    MessageBox.Show("Carro removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carro não encontrado!");
            }
            finally
            {
                DB.Close();
                exibir();
                limpar();
                recuperaFornecedor();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB = Db4oFactory.OpenFile("dbConcessionaria.yap");

            try
            {
                classeCarro car = DB.Query<classeCarro>(P => P.codCarro.Equals(textBox1.Text)).Single();
                car.codCarro = textBox1.Text;
                car.nomeCarro = textBox2.Text;
                car.precoCarro = Convert.ToDouble(textBox3.Text);
                car.quantidadeCarro = (int)numericUpDown1.Value;
                car.fornecedorCarro = comboBox1.Text;


                DB.Store(car);
                DB.Commit();
            }
            finally
            {
                DB.Close();
                exibir();
                limpar();
                recuperaFornecedor();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            exibir();
            limpar();
            recuperaFornecedor();
        }
    }
}
