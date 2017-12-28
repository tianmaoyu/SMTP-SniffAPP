using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchRepleace
{
    public class Family
    {
        private string patternFamily = @"<inlFFamily `.+[^>]";

        private string patternFamilyValue = @"(?<=<inlFFamily `).+?(?='>)";

        private string patternFontCatalog = @"<inlCombinedFontCatalog";

        private string patternColorCatalog = @"> # end of ColorCatalog";

        private List<string> SpecialValues = new List<string>() { "Symbol", "Windings", "Zapf Dingbats" };

        private static string fileName;
        private DataGridView dataGridView;

        private List<FamilyEntity> familyEntitys = new List<FamilyEntity>();

        public Family(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }
        private Dictionary<string, string> ReadValue(string fileName)
        {
            Family.fileName = fileName;
            var result = new Dictionary<string, string>();
            var listStr = FileHelper.MatchStr(this.patternFamily, fileName);
            foreach (var str in listStr)
            {
                var match = Regex.Match(str, patternFamilyValue);
                if (!result.ContainsKey(str) && !SpecialValues.Contains(match.Value))
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
                var entity = new FamilyEntity();
                entity.Id = i;
                entity.OldText = keyValue.Key;
                entity.OldValue = keyValue.Value;
                entity.IsCombination = false;
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
                var OldValueCell = new DataGridViewTextBoxCell();
                OldValueCell.Value = entity.OldValue;
                var isCombinationCell = new DataGridViewCheckBoxCell();
                isCombinationCell.Selected = false;
                var newValueCell = new DataGridViewTextBoxCell();
                row.Cells.Add(OldTextCell);
                row.Cells.Add(idCell);
                row.Cells.Add(OldValueCell);
                row.Cells.Add(isCombinationCell);
                row.Cells.Add(newValueCell);
                this.dataGridView.Rows.Add(row);
            }

        }

        public void ReadDataView()
        {
            var _familyEntitys = new List<FamilyEntity>();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                FamilyEntity familyEntity = new FamilyEntity();
                familyEntity.OldText = row.Cells["OldText"].Value.ToString();
                familyEntity.Id = (int)row.Cells["Id"].Value;
                familyEntity.OldValue = row.Cells["OldValue"].Value.ToString();
                familyEntity.NewValue = row.Cells["NewValue"]?.Value?.ToString();
                familyEntity.IsCombination = !string.IsNullOrEmpty(row.Cells["IsCombination"]?.Value?.ToString());
                _familyEntitys.Add(familyEntity);
            }
            this.ReplaceNoCombination(_familyEntitys);
            this.ReplaceCombination(_familyEntitys);
        }

        /// <summary>
        /// 直接替换
        /// </summary>
        /// <param name="entitys"></param>
        private void ReplaceNoCombination(List<FamilyEntity> entitys)
        {
            var _list = entitys.ToList();
            if (_list.Any())
            {
                foreach (var entity in _list)
                {
                    var oldText = entity.OldText;
                    var newText = string.Empty;
                    if (entity.IsCombination)
                    {
                        if (string.IsNullOrEmpty(entity.NewValue))
                        {
                            newText = $"<inlFFamily `{entity.OldValue}+'>";
                        }
                        else
                        {
                            newText = $"<inlFFamily `{entity.OldValue}_{entity.NewValue}'>";
                        }
                    }
                    else{
                        if (string.IsNullOrEmpty(entity.NewValue)) continue;
                        newText = $"<inlFFamily `{entity.NewValue}'>";
                    }
                    FileHelper.Replace(Family.fileName, oldText, newText);
                }
            }
        }
        /// <summary>
        /// 组合字体替换
        /// </summary>
        /// <param name="entitys"></param>
        private void ReplaceCombination(List<FamilyEntity> entitys)
        {
            var _list = entitys.Where(i => i.IsCombination).ToList();
            if (!_list.Any()) return;
            var colorCatalogList = this.HasColorCatalog();
            if (colorCatalogList == null|| colorCatalogList.Count!=1)
                throw new Exception(@"错误的文件：找不到,或者有多个'> # end of ColorCatalo'");
            var fontCatalogList = this.hasFontCatalog();
            if(fontCatalogList!=null && fontCatalogList.Count>1)
                throw new Exception(@"错误的文件：有多个'> > # end of CombinedFontCatalog'");
            if (fontCatalogList == null || fontCatalogList.Count == 0)
                this.AddFontCatalog();
            foreach (var entity in _list)
            {
                if (entity.IsCombination)
                {
                    if (string.IsNullOrEmpty(entity.NewValue))
                    {
                        var newText = $@"<inlCombinedFontCatalog
<inlCombinedFontDefn 
<inlCombinedFontName `{entity.OldValue}+'>
<inlCombinedFontBaseFamily `Arial Unicode MS'>
<inlCombinedFontAllowBaseFamilyBoldedAndObliqued Yes>
<inlCombinedFontWesternFamily `{entity.OldValue}'>
<inlCombinedFontWesternSize  100.0%>
<inlCombinedFontWesternShift  0.0%>
<inlCombinedFontBaseEncoding `GB2312-80.EUC'>
> # end of CombinedFontDefn";
                        var oldText = @"<inlCombinedFontCatalog";
                        FileHelper.Replace(Family.fileName, oldText, newText);
                    }
                    else
                    {
                        var newText = $@"<inlCombinedFontCatalog
<inlCombinedFontDefn 
<inlCombinedFontName `{entity.OldValue}_{entity.NewValue}'>
<inlCombinedFontBaseFamily `{entity.NewValue}'>
<inlCombinedFontAllowBaseFamilyBoldedAndObliqued Yes>
<inlCombinedFontWesternFamily `{entity.OldValue}'>
<inlCombinedFontWesternSize  100.0%>
<inlCombinedFontWesternShift  0.0%>
<inlCombinedFontBaseEncoding `GB2312-80.EUC'>
> # end of CombinedFontDefn";
                        var oldText = @"<inlCombinedFontCatalog";
                        FileHelper.Replace(Family.fileName, oldText, newText);
                    }
                }
                
            }
           

        }
        private void AddFontCatalog()
        {
            var newText = @"> # end of ColorCatalog
<inlCombinedFontCatalog
> # end of CombinedFontCatalog";

            var oldText = @"> # end of ColorCatalog";
            FileHelper.Replace(Family.fileName, oldText, newText);
        }

        private List<string> hasFontCatalog()
        {
            var listStr = FileHelper.MatchStr(this.patternFontCatalog, Family.fileName);
            return listStr;
        }

        private List<string> HasColorCatalog()
        {
            var listStr = FileHelper.MatchStr(this.patternColorCatalog, Family.fileName);
            return listStr;
        }
    }

    public class FamilyEntity
    {
        public int Id { get; set; }
        public string OldValue { set; get; }
        public string NewValue { set; get; }
        public bool IsCombination { set; get; }
        public string OldText { set; get; }
        public string NewTest { set; get; }
    }

}
