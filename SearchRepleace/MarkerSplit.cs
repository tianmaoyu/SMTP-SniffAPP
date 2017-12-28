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

        private string partternValue = @"(?<=<inlMText `).+(?='>)";

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

                row.Cells.Add(OldTextCell);
                row.Cells.Add(idCell);
                this.dataGridView.Rows.Add(row);
            }

        }

        public void ReadDataView()
        {
            var _familyEntitys = new List<MarkerSplitEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                var familyEntity = new MarkerSplitEntity();
                familyEntity.OldText = row.Cells["OldText"].Value.ToString();
                familyEntity.Id = (int)row.Cells["Id"].Value;
                _familyEntitys.Add(familyEntity);
            }
            this.Replace(_familyEntitys);
            
        }
        private void Replace(List<MarkerSplitEntity> list)
        {

        }

    }
    public class MarkerSplitEntity
    {
        public int Id { get; set; }
        public string OldText { set; get; }
        public string NewText { set; get; }
    }
}
