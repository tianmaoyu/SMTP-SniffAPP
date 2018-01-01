using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchRepleace
{
    class Paragraph
    {

        private string partternParagraph = @"(?s)(?:<inlPgfTag).+?(?:> # end of ParaLine)";
        //(?:<inlPgfTag `).+?(?:'>)
        private string partternValue = @"(?<=<inlPgfTag `).+?(?='>)";

        private string partternCondition = "<inlConditionCatalog\r";

        private static string fileName;

        private DataGridView dataGridView;

        private List<LanguageEntity> languageEntitys = new List<LanguageEntity>();

        public Paragraph(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        private Dictionary<string, string> ReadValue(string fileName)
        {
            Paragraph.fileName = fileName;
            var result = new Dictionary<string, string>();
            var markerList = FileHelper.MatchStr(this.partternParagraph, fileName).ToList();
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
            var _entitys = new List<ParagraphEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var addEntity = new ParagraphEntity();
                addEntity.OldText = row.Cells["ParagraphOldText"].Value.ToString();
                addEntity.OldValue = row.Cells["ParagraphOldValue"].Value.ToString();
                addEntity.IsFillter = !string.IsNullOrEmpty(row.Cells["ParagraphIsFillter"]?.Value?.ToString());
                _entitys.Add(addEntity);
            }
            this.ReplaceCommon(_entitys);

        }
        //普通替换
        private void ReplaceCommon(List<ParagraphEntity> entities)
        {
            var _entities = entities.Where(i => i.IsFillter).ToList();
            if (_entities.Any()) this.ReplaceCondition();
            foreach (var entity in _entities)
            {
                var _oldValueLineText = $"<inlPgfTag `{entity.OldValue}'>\r";
                var _newVlueLineText = $@"<inlPgfTag `{entity.OldValue}'>
<inlConditional 
< inlInCondition `CodeNOT01'>
> # end of Conditional
< inlParaLine
< inlString `'>";
                var _newTextTemp = entity.OldText.Replace(_oldValueLineText, _newVlueLineText);
                var _oldEndParagphLine=$@"> # end of ParaLine";
                var _newEndParagphLine = $@"<inlUnconditional >
<inlString `'>
> # end of ParaLine
";
                var newText = _newTextTemp.Replace(_oldEndParagphLine, _newEndParagphLine);
                FileHelper.Replace(Paragraph.fileName, entity.OldText, newText);
            }
        }
        private void ReplaceCondition()
        {
            var oldText =@"<inlConditionCatalog
";
            var newText = @"<inlConditionCatalog
<inlCondition 
  <inlCTag `CodeNOT'>
  <inlCState CHidden>
  <inlCStyle CUnderline>
  <inlCSeparation 2>
  <inlCColor `Red'>
 > # end of Condition";
            FileHelper.Replace(Paragraph.fileName, oldText, newText);
        }

    }
    public class ParagraphEntity
    {
        public int Id { get; set; }
        public string OldText { set; get; }
        public string OldValue { set; get; }
        public bool IsFillter { set; get; }
    }
}
