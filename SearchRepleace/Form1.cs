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
            this.dataGridView_Mtype.KeyDown += new KeyEventHandler(Data_Past);
        }

        private void btn_SelectFileClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择Txt所在文件夹";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(selectedPath);
                    var fileInfos = dir.GetFiles().Where(item => item.FullName.Contains(".txt"));
                    int i = 0;
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        var row = (DataGridViewRow)this.dataGridView_file.RowTemplate.Clone();
                        var isCompleteCell = new DataGridViewCheckBoxCell();
                        isCompleteCell.Selected = false;
                        var idCell = new DataGridViewTextBoxCell();
                        idCell.Value = i;
                        var fileNameCell = new DataGridViewTextBoxCell();
                        fileNameCell.Value = fileInfo.FullName;
                        row.Cells.Add(isCompleteCell);
                        row.Cells.Add(idCell);
                        row.Cells.Add(fileNameCell);
                        this.dataGridView_file.Rows.Add(row);
                        i++;
                    }
                }

            }

        }



        private string ReadFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileLoadException("文件错误");
            var str = File.ReadAllText(fileName);
            return str;
        }



        private void button_Replece_Click(object sender, EventArgs e)
        {
            var fileName = this.GetNoCompleteFileName();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
            var currentTabName = this.tabControl.SelectedTab.Name;
            if (currentTabName.Equals(this.tab_Family.Name))
            {
                var family = new Family(this.dataGridView_Fmaily);
                family.ReadDataView();
            }
            if (currentTabName.Equals(this.tab_Split.Name))
            {
                var split = new MarkerSplit(this.dataGridView_Split);
                split.ReadDataView();
            }
            if (currentTabName.Equals(this.tab_Mtype.Name))
            {
                var markerAdd = new MarkerAdd(this.dataGridView_Mtype);
                markerAdd.ReadDataView();
            }
            if (currentTabName.Equals(this.tabPage_Language.Name))
            {
                var language = new language(this.dataGridView_Language);
                language.ReadDataView();
            }
            if (currentTabName.Equals(this.tabPage_Paragraph.Name))
            {
                var paragraph = new Paragraph(this.dataGridView_Paragraph);
                paragraph.ReadDataView();
            }
            if (currentTabName.Equals(this.tabPage_FTag.Name))
            {
                var fTag = new FTag(this.dataGridView_FTag);
                fTag.ReadDataView();
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fileName = this.GetNoCompleteFileName();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("请选择一个文件");
                return;
            }
            var currentTabName = this.tabControl.SelectedTab.Name;
            if (currentTabName.Equals(this.tab_Family.Name))
            {
                var family = new Family(this.dataGridView_Fmaily);
                family.WriteDataView(fileName);
            }
            if (currentTabName.Equals(this.tab_Split.Name))
            {
                var split = new MarkerSplit(this.dataGridView_Split);
                split.WriteDataView(fileName);
            }
            if (currentTabName.Equals(this.tab_Mtype.Name))
            {
                //先进行拆分
                var split = new MarkerSplit(this.dataGridView_Split);
                split.WriteDataView(fileName);
                split.ReadDataView();

                var markerAdd = new MarkerAdd(this.dataGridView_Mtype);
                markerAdd.WriteDataView(fileName);
            }
            if (currentTabName.Equals(this.tabPage_Language.Name))
            {
                var language = new language(this.dataGridView_Language);
                language.WriteDataView(fileName);
            }
            if (currentTabName.Equals(this.tabPage_Paragraph.Name))
            {
                var language = new Paragraph(this.dataGridView_Paragraph);
                language.WriteDataView(fileName);
            }
            if (currentTabName.Equals(this.tabPage_FTag.Name))
            {
                var fTag = new FTag(this.dataGridView_FTag);
                fTag.WriteDataView(fileName);
            }
        }

        private void btn_clearFile_Click(object sender, EventArgs e)
        {
            this.dataGridView_file.Rows.Clear();
        }

        private void btn_NextFile_Click(object sender, EventArgs e)
        {
            var selectFileList = new List<SelectFile>();
            foreach (DataGridViewRow row in this.dataGridView_file.Rows)
            {
                var selectFile = new SelectFile();
                selectFile.FileId = (int)row.Cells["SelectFileId"].Value;
                selectFile.FileName = row.Cells["SelectFileName"].Value.ToString();
                selectFile.IsComplete = !string.IsNullOrEmpty(row.Cells["SelectIsComplete"]?.Value?.ToString());
                selectFileList.Add(selectFile);
            }
            var id = -1;
            if (selectFileList.Where(i => !i.IsComplete).Any())
            {
                id=selectFileList.Where(i => !i.IsComplete).OrderBy(i => i.FileId).FirstOrDefault().FileId;
            }
            foreach (DataGridViewRow row in this.dataGridView_file.Rows)
            {
               var _fileId = (int)row.Cells["SelectFileId"].Value;
                if(_fileId== id&& id!=-1)
                {
                  var cell=  (DataGridViewCheckBoxCell)row.Cells["SelectIsComplete"];
                  cell.Value =(object) true;
                }
             
            }

        }

        /// <summary>
        /// 得到未处理的文件
        /// </summary>
        /// <returns></returns>
        private string GetNoCompleteFileName()
        {
            var selectFileList = new List<SelectFile>();
            foreach (DataGridViewRow row in this.dataGridView_file.Rows)
            {
                var selectFile = new SelectFile();
                selectFile.FileId = (int)row.Cells["SelectFileId"].Value;
                selectFile.FileName = row.Cells["SelectFileName"].Value.ToString();
                selectFile.IsComplete = !string.IsNullOrEmpty(row.Cells["SelectIsComplete"]?.Value?.ToString());
                selectFileList.Add(selectFile);
            }
            if (selectFileList.Where(i => !i.IsComplete).Any())
            {
                return selectFileList.Where(i => !i.IsComplete).OrderBy(i => i.FileId).FirstOrDefault().FileName;
            }
            return "";
        }

        #region 粘贴复制
        private void DataGirdViewCellPaste(DataGridView p_Data)
        {
            try
            {
                string pasteText = Clipboard.GetText();
                if (string.IsNullOrEmpty(pasteText))
                    return;
                var lines = pasteText.Replace("\r\n","$").Split('$').ToList();
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line.Trim()))
                        continue;
                    var values= line.Split('\t');
                    foreach(DataGridViewRow row in this.dataGridView_Mtype.Rows)
                    {
                        var id = (int)row.Cells["MarkerAddId"].Value;
                        if(id==int.Parse(values[0]))
                        {
                            row.Cells["MarkerAddNewValue"].Value = values[2];
                        }
                    }
                   
                }
            }
            catch
            {
                
            }
        }

        private void Data_Past(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                if (sender != null && sender.GetType() == typeof(DataGridView))
                {
                    var currentTabName = this.tabControl.SelectedTab.Name;
                    if (currentTabName.Equals(this.tab_Mtype.Name))
                    {
                        DataGirdViewCellPaste((DataGridView)sender);
                    }
                }
            }
        }
        #endregion
    }

    public class SelectFile
    {
        public int FileId { set; get; }
        public string FileName { set; get; }
        public bool IsComplete { set; get; }
    }
}
