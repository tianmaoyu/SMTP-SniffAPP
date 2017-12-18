using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchRepleace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件|*.txt*|*.cs|";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.text_fileNames.AppendText(openFileDialog.FileName);
                //this.text_fileNames.AppendText(Environment.NewLine);
                this.text_fileNames.ReadOnly = true;
                //foreach (var fileName in openFileDialog.FileName)
                //{
                //    this.text_fileNames.AppendText(fileName);
                //    //this.text_fileNames.AppendText(Environment.NewLine);
                //    this.text_fileNames.ReadOnly = true;
                //}
            }

        }



        private void FillDataView(string fileName)
        {
          
            var rows= this.dataGridView_Fmaily.Rows;
            for (int i = rows.Count - 2; i >= 0; i--)
            {
                this.dataGridView_Fmaily.Rows.RemoveAt(i);
            }
          
            var familys = this.ReadFileWithFamily(fileName);
            this.label1.Text = string.Format("一共找到{0}", familys.Count);
            //this.label1.Font.Style
            foreach (var family in familys)
            {
                var row = (DataGridViewRow)this.dataGridView_Fmaily.RowTemplate.Clone();

                var familyNameCell = new DataGridViewTextBoxCell();
                familyNameCell.Value = family.Key;

                var familyNameCount = new DataGridViewTextBoxCell();
                if (family.Value > 1)
                {
                    familyNameCount.Value = string.Format("异常一共找到:{0}处", family.Value);
                    familyNameCount.Style.ForeColor = Color.Red;
                }
                else
                {
                    familyNameCount.Value = "";
                }
               

                var isCombination = new DataGridViewCheckBoxCell();
                isCombination.Selected = false;
             
                var newFaily = new DataGridViewTextBoxCell();
                newFaily.Value = "new";

                row.Cells.Add(familyNameCell);
                row.Cells.Add(familyNameCount);
                row.Cells.Add(isCombination);
                row.Cells.Add(newFaily);
                this.dataGridView_Fmaily.Rows.Add(row);
            }


        }



        private Dictionary<string, int> ReadFileWithFamily(string fileName)
        {
            var result = new Dictionary<string, int>();
            var str = this.ReadFile(fileName);
            string pattern = @"<inlFFamily `.+[^>]";
            foreach (Match match in Regex.Matches(str, pattern))
            {
                try
                {
                    var family = match.Value.Replace("<inlFFamily `", "").Replace("'>\r\n", "");
                    if (result.ContainsKey(family))
                    {
                        result.Add(family, result[family]++);
                    }
                    result.Add(family, 1);
                }
                catch (Exception ex)
                {

                }

            }
            return result;
        }

        private Dictionary<string,string> ReadNewFamily()
        {
            var dictionary = new Dictionary<string, string>();
            var count = this.dataGridView_Fmaily.Rows.Count;
            foreach (DataGridViewRow row in this.dataGridView_Fmaily.Rows)
            {
                if (count - 1 == 0) break;
                var oldFamily = row.Cells[0].Value.ToString();
                var newFamily = row.Cells[3].Value.ToString();
                if (oldFamily.Equals(newFamily) || string.IsNullOrEmpty(newFamily)) break;
                var _oldFamily = string.Format(@"<inlFFamily `{0}'>", oldFamily);
                var _newFamily = string.Format(@"<inlFFamily `{0}'>", newFamily);
                dictionary.Add(_oldFamily, _newFamily);
                count--;
            }
            return dictionary;
        }
        private void RepleaceFamily()
        {
            var fileName = this.text_fileNames.Lines[0];
            var contents=  File.ReadAllText(fileName);
            var dictionary = this.ReadNewFamily();
            foreach(var keyValue in dictionary)
            {
                contents = contents.Replace(keyValue.Key, keyValue.Value);
            }
            File.WriteAllText(fileName, contents, Encoding.UTF8);
        }

        private string ReadFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileLoadException("文件错误");
            var str = File.ReadAllText(fileName);
            return str;
        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {
            var fileNames = this.text_fileNames.Lines;
            if (fileNames.Count()==0 || string.IsNullOrEmpty(fileNames[0]))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
               
            this.FillDataView(fileNames[0]);
        }

        private void button_Replece_Click(object sender, EventArgs e)
        {
            var fileNames = this.text_fileNames.Lines;
            if (fileNames.Count() == 0 || string.IsNullOrEmpty(fileNames[0]))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
            this.RepleaceFamily();
        }

        private void dataGridView_Fmaily_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
