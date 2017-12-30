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
    /// 注音添加
    /// </summary>
    public class MarkerAdd
    {
        private string partternMarker = @"(?s)(?:<inlMarker).+?(?:> # end of Marker)";

        //MType=2 表示注音
        private string partternMType2 = @"<inlMType 2>";

        private string partternValue = @"(?<=<inlMText `|<MText `).+(?='>)";
        //特殊的字符
        private List<string> spcialStr = new List<string>() { @"\[", @"\]", @"\>" };

        private string partternSpcial = @"<.+>";

        private static string fileName;

        private DataGridView dataGridView;

        private List<MarkerSplitEntity> familyEntitys = new List<MarkerSplitEntity>();

        public MarkerAdd(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        private Dictionary<string, string> ReadValue(string fileName)
        {
            MarkerAdd.fileName = fileName;
            var result = new Dictionary<string, string>();
            var markerList = FileHelper.MatchStr(this.partternMarker, fileName).Where(str => Regex.Match(str, partternMType2).Success).ToList();
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
            familyEntitys.Clear();
            var keyValues = this.ReadValue(fileName);
            int i = 1;
            foreach (var keyValue in keyValues)
            {
                var entity = new MarkerSplitEntity();
                entity.Id = i;
                entity.OldText = keyValue.Key;
                entity.OldValue = keyValue.Value;

                familyEntitys.Add(entity);
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
            foreach (var entity in familyEntitys)
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
            var _entitys = new List<MarkerAddEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var addEntity = new MarkerAddEntity();
                addEntity.OldText = row.Cells["MarkerOldText"].Value.ToString();
                addEntity.OldValue = row.Cells["MarkerAddOldValue"].Value.ToString();
                addEntity.NewValue = row.Cells["MarkerAddNewValue"].Value.ToString();
                _entitys.Add(addEntity);
            }
            this.ReplaceCommon(_entitys);
          
        }
        //普通替换
        private void ReplaceCommon(List<MarkerAddEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (string.IsNullOrEmpty(entity.NewValue) || entity.OldValue.Equals(entity.NewValue)) continue;

                //删除掉<xxxxxx>
                if (Regex.Match(entity.OldValue, this.partternSpcial).Success)
                {
                    var _str = Regex.Match(entity.OldValue, this.partternSpcial).Value;
                    entity.NewValue = entity.OldValue.Replace(_str, "");
                }
                var newText = entity.OldText.Replace(entity.OldValue, entity.NewValue);
                FileHelper.Replace(MarkerAdd.fileName, entity.OldText, newText);
            }
        }
       
    }
    public class MarkerAddEntity
    {
        public int Id { get; set; }
        public string OldText { set; get; }
        public string OldValue { set; get; }
        public string NewValue { set; get; }
    }
}
