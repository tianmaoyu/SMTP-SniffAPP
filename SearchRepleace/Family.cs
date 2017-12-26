using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchRepleace
{
    public class Family
    {
        private string patternFamily = @"<inlFFamily `.+[^>]";

        private string patternFamilyValue = @"(?<=<inlFFamily `).+?(?='>)";

        private List<string> SpecialValues = new List<string>() { "Symbol", "Windings", "Zapf Dingbats" };

        private Dictionary<string, string> ReadValue(string fileName)
        {
            var result = new Dictionary<string, string>();
            var listStr = FileHelper.MatchStr(this.patternFamily, fileName);
            foreach (var str in listStr)
            {
                var match = Regex.Match(str, patternFamilyValue);
                if (!result.ContainsKey(str)&& !SpecialValues.Contains(match.Value))
                {
                    result.Add(str, match.Value);
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



    }
}
