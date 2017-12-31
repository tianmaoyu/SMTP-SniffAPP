using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchRepleace
{
    public class language
    {

        //(?:<inlPgfLanguage|<inlFLanguage).+(?:>)
        private string partternLanguage = @"<inl.+Language.+[^>]";

        private string partternValue = @"(?<=<inlPgfLanguage |<inlFLanguage ).+(?=>)";
 

        private static string fileName;

        private DataGridView dataGridView;

        private List<LanguageEntity> languageEntitys = new List<LanguageEntity>();

        public language(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        private Dictionary<string, string> ReadValue(string fileName)
        {
            language.fileName = fileName;
            var result = new Dictionary<string, string>();
            var markerList = FileHelper.MatchStr(this.partternLanguage, fileName).ToList();
            foreach (var str in markerList)
            {
                var match = Regex.Match(str, partternValue);
                if (!result.ContainsKey(str))
                {
                    result.Add(str, match.Value);
                }
            }
            return result;
        }

        private void InitData(string fileName)
        {
            languageEntitys.Clear();
            var keyValues = this.ReadValue(fileName);
            int i = 1;
            foreach (var keyValue in keyValues)
            {
                var entity = new LanguageEntity();
                entity.Id = i;
                entity.OldText = keyValue.Key;
                entity.OldValue = keyValue.Value;

                languageEntitys.Add(entity);
                i++;
            }
        }

        public void WriteDataView(string fileName)
        {
            this.InitData(fileName);
            var rows = this.dataGridView.Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                this.dataGridView.Rows.RemoveAt(i);
            }
            foreach (var entity in languageEntitys)
            {
                var row = (DataGridViewRow)this.dataGridView.RowTemplate.Clone();

                var idCell = new DataGridViewTextBoxCell();
                idCell.Value = entity.Id;

                var OldTextCell = new DataGridViewTextBoxCell();
                OldTextCell.Value = entity.OldText;

                var oldValueCell = new DataGridViewTextBoxCell();
                oldValueCell.Value = entity.OldValue;

                var newValueCell = new DataGridViewTextBoxCell();
                newValueCell.Value = "";

                row.Cells.Add(idCell);
                row.Cells.Add(OldTextCell);
                row.Cells.Add(oldValueCell);
                row.Cells.Add(newValueCell);
                this.dataGridView.Rows.Add(row);
            }

        }

        public void ReadDataView()
        {
            var _entitys = new List<LanguageEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var addEntity = new LanguageEntity();
                addEntity.OldText = row.Cells["LanguageOldText"].Value.ToString();
                addEntity.OldValue = row.Cells["LanguageOldValue"].Value.ToString();
                addEntity.NewValue = row.Cells["LanguageNewValue"].Value.ToString();
                _entitys.Add(addEntity);
            }
            this.ReplaceCommon(_entitys);

        }
        //普通替换
        private void ReplaceCommon(List<LanguageEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (string.IsNullOrEmpty(entity.NewValue) || entity.OldValue.Equals(entity.NewValue)) continue;
                var newText = entity.OldText.Replace(entity.OldValue, entity.NewValue);
                FileHelper.Replace(language.fileName, entity.OldText, newText);
            }
        }


    }
    public class LanguageEntity
    {
        public int Id { get; set; }
        public string OldText { set; get; }
        public string OldValue { set; get; }
        public string NewValue { set; get; }
    }
}
