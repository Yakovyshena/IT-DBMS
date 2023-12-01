using DBMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace DBMS.Forms
{
    public partial class FormTable : Form
    {
        public Table Table;

        public FormTable(Table table)
        {
            InitializeComponent();
            Table = table;
            Text = Table.Name;
            foreach (Column column in Table.Columns)
                dataGridView.Columns.Add("", column.Name + "\n" + column.Type);
            foreach (DataGridViewColumn column in dataGridView.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            RefreshRows();
            if (Table is TableDifference)
            {
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.AllowUserToAddRows = false;
            }
        }

        public void RefreshRows()
        {
            if (Table is TableDifference difference)
                difference.Calculate();
            dataGridView.Rows.Clear();
            foreach (Row row in Table.Rows.Values)
                dataGridView.Rows[dataGridView.Rows.Add(row.Cells)].Tag = row;
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex == dataGridView.NewRowIndex || !dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                return;
            try
            {
                Table.Columns[e.ColumnIndex].Type.Parse(e.FormattedValue.ToString()!);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message, "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == dataGridView.NewRowIndex)
                return;
            try
            {
                for (int i = 0; i < Table.Columns.Count; i++)
                    Table.Columns[i].Type.Parse(dataGridView.Rows[e.RowIndex].Cells[i].Value?.ToString() ?? "");
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("Fill all cells to create row (or remove the entire row)", "Cannot create row", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dataGridView.Rows[e.RowIndex];
            dgvRow.Cells[e.ColumnIndex].Value = Table.Columns[e.ColumnIndex].Type.Parse(dgvRow.Cells[e.ColumnIndex].Value?.ToString() ?? "").ToString();
            if (dgvRow.Tag is not Row dbRow)
            {
                try
                {
                    Models.Values.Value[] cells = new Models.Values.Value[Table.Columns.Count];
                    for (int i = 0; i < Table.Columns.Count; i++)
                        cells[i] = Table.Columns[i].Type.Parse(dgvRow.Cells[i].Value?.ToString() ?? "");
                    dgvRow.Tag = Table.AddRow(cells);
                    (Parent.FindForm() as FormDatabase)?.RefreshDifferences(Table.Id);
                }
                catch { }
            }
            else
            {
                DataGridViewCell dgvCell = dgvRow.Cells[e.ColumnIndex];
                dbRow.Cells[e.ColumnIndex] = Table.Columns[e.ColumnIndex].Type.Parse(dgvCell.Value?.ToString() ?? "");
                (Parent.FindForm() as FormDatabase)?.RefreshDifferences(Table.Id);
            }
        }

        private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Tag is not Row dbRow)
                return;
            Table.RemoveRow(dbRow.Id);
            (Parent.FindForm() as FormDatabase)?.RefreshDifferences(Table.Id);
        }

        private void FormTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Parent.FindForm() as FormDatabase)?.ListTables(Table.Id);
        }
    }
}
