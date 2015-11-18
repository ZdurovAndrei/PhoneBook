using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PhoneBook
{
    public partial class PhoneBook : Form
    {
        public const string Path = "Data.xml";
        public PhoneBook()
        {
            InitializeComponent();
        }

        readonly XmlData _data = new XmlData();
        private void PhoneBook_Load(object sender, EventArgs e)
        {
            var list = _data.ReadSaveXml(Path);
            foreach (var t in list)
            {
                dataGridView1.Rows.Add(t.Name, t.Phone, t.Mail);
            }
        }
        
        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            Regex RegName = new Regex(@"[А-Я]{1}[а-я]+\s[А-Я]{1}[а-я]+\s[А-Я]{1}[а-я]");
            Regex RegPhone = new Regex(@"\+\d{11}");
            Regex RegMail = new Regex(@"\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}\b");
            if (!RegName.IsMatch(textBoxName.Text) || textBoxName.Text == "" || !RegPhone.IsMatch(textBoxPhone.Text) || textBoxPhone.Text == "" || !RegMail.IsMatch(textBoxMail.Text) || textBoxMail.Text == "")
            {
                MessageBox.Show(@"Поля заполнены неправильно");
            }
            else
            {

                dataGridView1.Rows.Add(textBoxName.Text, textBoxPhone.Text, textBoxMail.Text);
                textBoxName.Text = "";
                textBoxPhone.Text = "";
                textBoxMail.Text = "";
                
            }
        }

        private void PhoneBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlData.XmlRemoveAll(Path);
            for(var i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                _data.WriteSaveXml(Path, dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
        }
    }
}
