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
    /// 注音拆分
    /// </summary>
    public class MarkerSplit
    {
        private string partternMarker = @"(?s)(?:<inlMarker).+?(?:> # end of Marker)";

        //MType=2 表示注音
        private string partternMType2 = @"<inlMType 2>";

        private string partternValue = @"(?<=<inlMText `|<MText `).+(?='>)";
        //特殊的字符
        private List<string> spcialStr = new List<string>() { @"\[", @"\]", @"\>" };

        private static string fileName;

        private DataGridView dataGridView;

        private List<MarkerSplitEntity> familyEntitys = new List<MarkerSplitEntity>();

        public MarkerSplit(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        private Dictionary<string, string> ReadValue(string fileName)
        {
            MarkerSplit.fileName = fileName;
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

                var valueCell = new DataGridViewTextBoxCell();
                valueCell.Value = entity.OldValue;

                row.Cells.Add(idCell);
                row.Cells.Add(OldTextCell);
                row.Cells.Add(valueCell);

                this.dataGridView.Rows.Add(row);
            }

        }

        public void ReadDataView()
        {
            var _familyEntitys = new List<MarkerSplitEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var splitEntity = new MarkerSplitEntity();
                splitEntity.OldText = row.Cells["SplitOldText"].Value.ToString()+"\r";//加一个回车
                splitEntity.OldValue = row.Cells["SplitValue"].Value.ToString();
                if (splitEntity.OldValue.Contains(@"\]") && splitEntity.OldValue.Contains(@";"))
                {
                    splitEntity.IsSpecial = true;
                }
                _familyEntitys.Add(splitEntity);
            }
            this.ReplaceCommon(_familyEntitys);
            this.ReplaceSpecial(_familyEntitys);
        }
        //普通替换
        private void ReplaceCommon(List<MarkerSplitEntity> entities)
        {
            var _entities = entities.Where(i => !i.IsSpecial).ToList();
            foreach (var entity in _entities)
            {
                if (!entity.OldValue.Contains(";")) continue;
                var valueList = entity.OldValue.Split(';').ToList();
                var oldText = entity.OldText;
                var newText = string.Empty;
                foreach (var value in valueList)
                {
                    newText += entity.OldText.Replace(entity.OldValue, value.TrimStart().TrimEnd());
                }
                FileHelper.Replace(MarkerSplit.fileName, oldText, newText);
            }
        }
        //特殊替换
        private void ReplaceSpecial(List<MarkerSplitEntity> entities)
        {
            var _entities = entities.Where(i => i.IsSpecial).ToList();
            foreach (var entity in _entities)
            {
                var oldValue = entity.OldValue;
                if (oldValue.Contains(@"\["))
                    oldValue = oldValue.Replace(@"\[", @"\\[");
                if (oldValue.Contains(@"\]"))
                    oldValue = oldValue.Replace(@"\]", @"\\]");
                if (oldValue.Contains(@"\<"))
                    oldValue = oldValue.Replace(@"\<", @"\\<");
                if (oldValue.Contains(@"\>"))
                    oldValue = oldValue.Replace(@"\>", @"\\>");
                var valueList = oldValue.Split(';').ToList();
                var oldText = entity.OldText;
                var newText = string.Empty;
                foreach (var value in valueList)
                {
                    newText += entity.OldText.Replace(entity.OldValue, value.TrimStart().TrimEnd());
                }
                FileHelper.Replace(MarkerSplit.fileName, oldText, newText);
            }
        }
    }
    public class MarkerSplitEntity
    {
        public int Id { get; set; }
        public bool IsSpecial { set; get; }
        public string OldText { set; get; }
        public string OldValue { set; get; }
    }
}
