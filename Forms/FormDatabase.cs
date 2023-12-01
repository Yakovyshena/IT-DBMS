using DBMS.Models;
using System.Diagnostics;
using System.Windows.Forms;

namespace DBMS.Forms
{
    public partial class FormDatabase : Form
    {
        private Database database;
        private string path;
        bool saved, modified;
        private FormCreateTable? formCreateTable = null;

        private IEnumerable<FormTable> AllFormsTable => splitContainer.Panel2.Controls.OfType<FormTable>();

        private FormTable? FindFormTable(int id) =>
            AllFormsTable.FirstOrDefault(form => form.Table.Id == id);

        private void InitEmpty()
        {
            database = new();
            path = "NewDatabase.dbf";
            saved = false;
            modified = false;
            SetText();
        }

        public FormDatabase()
        {
            InitializeComponent();
            InitEmpty();
        }

        public FormDatabase(string path)
        {
            InitializeComponent();
            try
            {
                database = Database.Load(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to open database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitEmpty();
                return;
            }
            this.path = path;
            saved = true;
            modified = false;
            SetText();
            ListTables();
        }

        private void SetText()
        {
            Text = (modified ? "* " : "") + Path.GetFileName(path) + " - Database Management System";
        }

        private void SetModified()
        {
            modified = true;
            SetText();
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            if (formCreateTable == null || formCreateTable.IsDisposed)
            {
                formCreateTable = new()
                {
                    TopLevel = false
                };
                splitContainer.Panel2.Controls.Add(formCreateTable);
                formCreateTable.WindowState = FormWindowState.Maximized;
                formCreateTable.BringToFront();
                formCreateTable.Show();
            }
            else
            {
                formCreateTable.WindowState = FormWindowState.Maximized;
                formCreateTable.BringToFront();
            }
        }

        public void ListTables(int? closedId = null)
        {
            groupBoxTables.SuspendLayout();
            groupBoxDifferences.SuspendLayout();
            groupBoxTables.Controls.Clear();
            groupBoxDifferences.Controls.Clear();
            foreach (Table table in database.Tables.Values.Reverse())
            {
                LinkLabel linkLabelTable = new()
                {
                    Text = table.Name + "  ×",
                    AutoSize = true,
                    AutoEllipsis = true,
                    Dock = DockStyle.Top
                };
                if ((!closedId.HasValue || closedId.Value != table.Id) &&
                        FindFormTable(table.Id) is not null)
                    linkLabelTable.LinkColor = Color.Purple;
                linkLabelTable.Links.Add(0, table.Name.Length, "Name");
                linkLabelTable.Links.Add(table.Name.Length + 2, 1, "Remove");
                Table tableCopy = table;
                linkLabelTable.LinkClicked += (sender, e) =>
                {
                    if (e.Link.LinkData.ToString() == "Remove")
                        RemoveTable(table);
                    else
                        ShowTable(table);
                };
                groupBoxTables.Controls.Add(linkLabelTable);
            }
            foreach (Table leftTable in database.Tables.Values.Reverse())
                foreach (Table rightTable in database.Tables.Values.Reverse())
                    if (leftTable.Id != rightTable.Id)
                    {
                        TableDifference? difference = null;
                        try
                        {
                            difference = TableDifference.Create(leftTable, rightTable);
                        }
                        catch
                        {
                            continue;
                        }
                        LinkLabel linkLabelTable = new()
                        {
                            Text = difference.Name,
                            AutoSize = true,
                            AutoEllipsis = true,
                            Dock = DockStyle.Top
                        };
                        if ((!closedId.HasValue || closedId.Value != difference.Id) &&
                                FindFormTable(difference.Id) is not null)
                            linkLabelTable.LinkColor = Color.Purple;
                        linkLabelTable.LinkClicked += (sender, e) => ShowTable(difference);
                        groupBoxDifferences.Controls.Add(linkLabelTable);
                    }
            groupBoxTables.ResumeLayout();
            groupBoxDifferences.ResumeLayout();
        }

        private void RemoveTable(Table table)
        {
            if (MessageBox.Show($"Are you sure you want to remove table \"{table.Name}\"?", "Attempt to remove table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            foreach (FormTable form in AllFormsTable.ToArray())
                if (form.Table.Id == table.Id ||
                        form.Table is TableDifference difference &&
                        (difference.LeftTable.Id == table.Id || difference.RightTable.Id == table.Id))
                    form.Close();
            database.RemoveTable(table.Id);
            SetModified();
            ListTables();
        }

        private void ShowTable(Table table)
        {
            FormTable? formTable = FindFormTable(table.Id);
            if (formTable is not null)
            {
                formTable.WindowState = FormWindowState.Maximized;
                formTable.BringToFront();
            }
            else
            {
                formTable = new(table)
                {
                    TopLevel = false
                };
                splitContainer.Panel2.Controls.Add(formTable);
                formTable.WindowState = FormWindowState.Maximized;
                formTable.BringToFront();
                formTable.Show();
            }
            ListTables();
        }

        public void CreateTable()
        {
            if (formCreateTable == null)
                return;
            if (formCreateTable.textBoxTableName.Text == "")
            {
                MessageBox.Show("Table name must be non-empty", "Cannot create table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (formCreateTable.flowLayoutPanelColumns.Controls.Count == 0)
            {
                MessageBox.Show("Table must have at least one column", "Cannot create table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            database.AddTable(
                formCreateTable.textBoxTableName.Text,
                (from GroupBox groupBoxCreateColumn
                 in formCreateTable.flowLayoutPanelColumns.Controls
                 select new Column(
                     groupBoxCreateColumn.Controls[0].Controls[0].Controls[0].Controls[0].Text,
                     (groupBoxCreateColumn.Controls[0].Controls[1].Controls[0].Controls[0] as ComboBox)!.SelectedIndex switch
                     {
                         0 => new Models.Types.Integer(),
                         1 => new Models.Types.Real(),
                         2 => new Models.Types.Char(),
                         3 => new Models.Types.String(),
                         4 => new Models.Types.Color(),
                         5 => new Models.Types.ColorInvl(
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[2].Controls[0].Controls[0] as NumericUpDown)!.Value,
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[2].Controls[0].Controls[2] as NumericUpDown)!.Value,
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[3].Controls[0].Controls[0] as NumericUpDown)!.Value,
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[3].Controls[0].Controls[2] as NumericUpDown)!.Value,
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[4].Controls[0].Controls[0] as NumericUpDown)!.Value,
                             (byte)(groupBoxCreateColumn.Controls[0].Controls[4].Controls[0].Controls[2] as NumericUpDown)!.Value
                             )
                     })
                 ).ToArray());
            splitContainer.Panel2.Controls.Remove(formCreateTable);
            formCreateTable.Dispose();
            formCreateTable = null;
            SetModified();
            ListTables();
        }

        int? splitContainer_Panel2_Width, splitContainer_Panel2_Height = null;

        private void splitContainer_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer_Panel2_Width is null)
                splitContainer_Panel2_Width = splitContainer.Panel2.Width;
            if (splitContainer_Panel2_Height is null)
                splitContainer_Panel2_Height = splitContainer.Panel2.Height;
            foreach (Control control in splitContainer.Panel2.Controls)
            {
                if (control is not Form form)
                    continue;
                if (form.Left < 0 && splitContainer.Panel2.Width > splitContainer_Panel2_Width)
                    form.Left += splitContainer.Panel2.Width - splitContainer_Panel2_Width.Value;
                if (form.Top < 0 && splitContainer.Panel2.Height > splitContainer_Panel2_Height)
                    form.Top += splitContainer.Panel2.Height - splitContainer_Panel2_Height.Value;
                if (form.Left + form.Width > splitContainer.Panel2.Width)
                    form.Width = splitContainer.Panel2.Width - form.Left;
                if (form.Left + form.Width > splitContainer.Panel2.Width)
                    form.Left = splitContainer.Panel2.Width - form.Width;
                if (form.Top + form.Height > splitContainer.Panel2.Height)
                    form.Height = splitContainer.Panel2.Height - form.Top;
                if (form.Top + form.Height > splitContainer.Panel2.Height)
                    form.Top = splitContainer.Panel2.Height - form.Height;
                if (form.WindowState == FormWindowState.Maximized)
                {
                    int ind = splitContainer.Panel2.Controls.IndexOf(form);
                    form.WindowState = FormWindowState.Normal;
                    form.WindowState = FormWindowState.Maximized;
                    splitContainer.Panel2.Controls.SetChildIndex(form, ind);
                }
            }
            splitContainer_Panel2_Width = splitContainer.Panel2.Width;
            splitContainer_Panel2_Height = splitContainer.Panel2.Height;
        }

        public void RefreshDifferences(int id)
        {
            SetModified();
            foreach (FormTable form in AllFormsTable)
                if (form.Table is TableDifference difference &&
                        (difference.LeftTable.Id == id || difference.RightTable.Id == id))
                    form.RefreshRows();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private bool SaveImpl(string newPath)
        {
            try
            {
                database.Save(newPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to save database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            path = newPath;
            Text = Path.GetFileName(newPath);
            saved = true;
            modified = false;
            SetText();
            return true;
        }

        private bool SaveAs()
        {
            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Database files (*.dbf)|*.dbf";
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = Path.GetFileName(path);
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(path);
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                return SaveImpl(saveFileDialog.FileName);
            else
                return false;
        }

        private bool Save()
        {
            if (saved)
                return SaveImpl(path);
            else
                return SaveAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private bool Exit()
        {
            if (modified)
                switch (MessageBox.Show("Do you want to save database?", "Database is not saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        return Save();
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                }
            else
                return true;
            return true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Exit())
                Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Database files (*.dbf)|*.dbf";
            openFileDialog.DefaultExt = "dbf";
            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    ArgumentList = { openFileDialog.FileName }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to open database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!saved && !modified)
                Close();
        }

        private void FormDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Exit();
        }
    }
}
