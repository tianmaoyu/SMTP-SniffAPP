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
    public partial class demo : Form
    {
        public demo()
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



        #region MType

        //(?s)(?<=<inlMarker).+?(?=> # end of Marker)
        private List<Tuple<string, string>> ReadFileWithMType(string fileName)
        {
            var result = new List<Tuple<string, string>>();
            var content = this.ReadFile(fileName);
            string pattern = @"(?s)(?<=<inlMarker).+?(?=> # end of Marker)";
            foreach (Match match in Regex.Matches(content, pattern))
            {
                try
                {
                    //^<inlMText.*[^>]
                    var mtypeMarch = Regex.Match(match.Value, @"^<inlMText.*[^>]");
                    var oldContent = $@"<inlMarker{match.Value} > # end of Marker";
                    var mtypes = mtypeMarch.Value.Replace("<inlMText `", "").Replace("'>", "").Split(';');
                    foreach (var mtyp in mtypes)
                    {
                        result.Add(new Tuple<string, string>(oldContent, mtyp));
                    }

                }
                catch (Exception ex)
                {

                }

            }
            return result;
        }

        private void FillDataViewMtype(string fileName)
        {

            var rows = this.dataGridView_Mtype.Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                this.dataGridView_Fmaily.Rows.RemoveAt(i);
            }

            var mtpyes = this.ReadFileWithMType(fileName);
            this.label1.Text = string.Format("一共找到{0}", mtpyes.Count);
            //this.label1.Font.Style
            foreach (var mtype in mtpyes)
            {
                var row = (DataGridViewRow)this.dataGridView_Mtype.RowTemplate.Clone();

                var mtypeNameCell = new DataGridViewTextBoxCell();
                mtypeNameCell.Value = mtype.Item1;

                var mtypeNameCount = new DataGridViewTextBoxCell();
                mtypeNameCount.Value = mtype.Item2;
             
                var newMtype = new DataGridViewTextBoxCell();
                newMtype.Value = "";
                row.Cells.Add(mtypeNameCell);
                row.Cells.Add(mtypeNameCount);
                row.Cells.Add(newMtype);
                this.dataGridView_Fmaily.Rows.Add(row);
            }
        }

        private Dictionary<string, string> ReadNewMytpe()
        {
            var dictionary = new Dictionary<string, string>();
            string content = string.Empty;
            foreach (DataGridViewRow row in this.dataGridView_Mtype.Rows)
            {
            
                var oldContent = row.Cells[0].Value.ToString();
                var oldMtype = row.Cells[1].Value.ToString();
                var newMtype = row.Cells[2].Value.ToString();
                if (oldMtype.Equals(newMtype) || string.IsNullOrEmpty(newMtype))
                {
                    var str = $@"<inlMText `{oldMtype}'>";
                }
                else
                {

                }


            }
            return null;
        }
        private void RepleaceMytpe()
        {
            var fileName = this.text_fileNames.Lines[0];
            var contents = File.ReadAllText(fileName);
            var dictionary = this.ReadNewFamily();
            foreach (var keyValue in dictionary)
            {
                contents = contents.Replace(keyValue.Key, keyValue.Value);
            }
            File.WriteAllText(fileName, contents, Encoding.UTF8);
        }
        #endregion


        #region Family
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

        private void FillDataView(string fileName)
        {

            var rows = this.dataGridView_Fmaily.Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
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
                newFaily.Value = "";

                row.Cells.Add(familyNameCell);
                row.Cells.Add(familyNameCount);
                row.Cells.Add(isCombination);
                row.Cells.Add(newFaily);
                this.dataGridView_Fmaily.Rows.Add(row);
            }


        }

        private Dictionary<string, string> ReadNewFamily()
        {
            var dictionary = new Dictionary<string, string>();
            var count = this.dataGridView_Fmaily.Rows.Count;
            foreach (DataGridViewRow row in this.dataGridView_Fmaily.Rows)
            {
                //if (count - 1 == 0) break;
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
            var contents = File.ReadAllText(fileName);
            var dictionary = this.ReadNewFamily();
            foreach (var keyValue in dictionary)
            {
                contents = contents.Replace(keyValue.Key, keyValue.Value);
            }
            File.WriteAllText(fileName, contents, Encoding.UTF8);
        }
        #endregion





        private string ReadFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileLoadException("文件错误");
            var str = File.ReadAllText(fileName);
            return str;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var fileNames = this.text_fileNames.Lines;
            if (fileNames.Count() == 0 || string.IsNullOrEmpty(fileNames[0]))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
            var currentTabName = this.tabControl.SelectedTab.Name;
            if (currentTabName.Equals(this.tab_Mtype.Name))
            {

            }
            if (currentTabName.Equals(this.tab_Family.Name))
            {
                this.FillDataView(fileNames[0]);
            }


        }

        private void button_Replece_Click(object sender, EventArgs e)
        {
            var fileNames = this.text_fileNames.Lines;
            if (fileNames.Count() == 0 || string.IsNullOrEmpty(fileNames[0]))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
            var currentTabName = this.tabControl.SelectedTab.Name;
            if (currentTabName.Equals(this.tab_Mtype.Name))
            {

            }
            if (currentTabName.Equals(this.tab_Family.Name))
            {
                this.RepleaceFamily();
                MessageBox.Show("替换成功！");
            }

        }


    }
}
