using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchRepleace
{
    /// <summary>
    /// 字符样式
    /// </summary>
    public class FTag
    {
        private string partternFTag = @"(?s)(?:<inlFTag).+?(?:# end of ParaLine)";

        private string partternValue = @"(?<=<inlFTag `).+?(?='>)";

        private string partternFTagInside = "(?:<inlFTag `).*?(?:'>)";

        private string partternFont = @"(?s)(?:<inlFTag).*?(?:> # end of Font)";

        private static string fileName;

        private DataGridView dataGridView;

        private List<FTagEntity> languageEntitys = new List<FTagEntity>();

        public FTag(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        private Dictionary<string, string> ReadValue(string fileName)
        {
            FTag.fileName = fileName;
            var result = new Dictionary<string, string>();
            var markerList = FileHelper.MatchStr(this.partternFTag, fileName).ToList();
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
                var entity = new FTagEntity();
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

                var isFillter = new DataGridViewCheckBoxCell();
                isFillter.Selected = false;

                row.Cells.Add(idCell);
                row.Cells.Add(OldTextCell);
                row.Cells.Add(oldValueCell);
                row.Cells.Add(isFillter);
                this.dataGridView.Rows.Add(row);
            }

        }

        public void ReadDataView()
        {
            var _entitys = new List<FTagEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var addEntity = new FTagEntity();
                addEntity.OldText = row.Cells["FTagOldText"].Value.ToString();
                addEntity.OldValue = row.Cells["FTagOldValue"].Value.ToString();
                addEntity.IsFillter = !string.IsNullOrEmpty(row.Cells["FTagIsFillter"]?.Value?.ToString());
                if (Regex.Matches(addEntity.OldText, this.partternFTagInside).Count > 1)
                {
                    addEntity.IsInside = true;
                }
                _entitys.Add(addEntity);
            }
            this.ReplaceCondition();
            this.ReplaceFTagNoInside(_entitys);
            this.ReplaceFTagInside(_entitys);
        }
        //没有内部的
        private void ReplaceFTagNoInside(List<FTagEntity> entities)
        {
            var _entities = entities?.Where(i => !i.IsInside && i.IsFillter).ToList();
            if (_entities == null || _entities.Count < 1) return;
            foreach (var entity in _entities)
            {
                var _oldFontLine = "> # end of Font";
                var _newFontLine = @"> # end of Font
<inlConditional 
<inlInCondition `CodeNOT'>
> # end of Conditional";
                var _oldParaLineline = "> # end of ParaLine";
                var _newParalineLine = @"<inlUnconditional >
<inlString `'>
> # end of ParaLine";
                var _newTextTemp = entity.OldText.Replace(_oldFontLine, _newFontLine);
                var newText = _newTextTemp.Replace(_oldParaLineline, _newParalineLine);
                FileHelper.Replace(FTag.fileName, entity.OldText, newText);
            }
        }

        //内部的
        private void ReplaceFTagInside(List<FTagEntity> entities)
        {
            var _entities = entities?.Where(i => i.IsInside&& i.IsFillter).ToList();
            if (_entities == null || _entities.Count < 1) return;
            foreach (var entity in _entities)
            {
                var matchs = Regex.Matches(entity.OldText, this.partternFont);

                var _oldFistFontText = matchs[0].Value;
                var _addText = @"<inlConditional
<inlInCondition `CodeNOT'>
> # end of Conditional";
                var _newFistFontText = _oldFistFontText+"\r" + _addText;
                var _newTextTemp = entity.OldText.Replace(_oldFistFontText, _newFistFontText);
                var _oldLastFontText = @"<inlFont 
" +matchs[matchs.Count - 1].Value;
                var _addText2 = @"<inlUnconditional >
<inlString `'>
";
                var _newLastFontText = _addText2 + _oldLastFontText;
                var newText = _newTextTemp.Replace(_oldLastFontText, _newLastFontText);
                FileHelper.Replace(FTag.fileName, entity.OldText, newText);
            }

        }

        private void ReplaceCondition()
        {
            var oldText = @"<inlConditionCatalog
";
            var newText = @"<inlConditionCatalog
<inlCondition 
  <inlCTag `CodeNOT'>
  <inlCState CHidden>
  <inlCStyle CUnderline>
  <inlCSeparation 2>
  <inlCColor `Red'>
 > # end of Condition";
            FileHelper.Replace(FTag.fileName, oldText, newText);
        }
    }


    public class FTagEntity
    {
        public int Id { get; set; }
        public string OldText { set; get; }
        public string OldValue { set; get; }
        public bool IsFillter { set; get; }
        public bool IsInside { set; get; }
    }
}
