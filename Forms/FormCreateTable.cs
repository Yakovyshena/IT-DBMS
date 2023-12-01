using System.Windows.Forms;

namespace DBMS.Forms
{
    public partial class FormCreateTable : Form
    {
        public FormCreateTable()
        {
            InitializeComponent();
            buttonSaveTable.Click += buttonSaveTable_Click;
        }

        private void buttonSaveTable_Click(object? sender, EventArgs e)
        {
            if (Parent.FindForm() is not FormDatabase formDatabase)
                return;
            formDatabase.CreateTable();
        }

        private void textBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
                return;
            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            textBox.Width = Math.Max(size.Width + 10, 60);
        }

        private void comboBoxType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox comboBox)
                return;
            if (comboBox.Parent.Parent.Parent is not FlowLayoutPanel flowLayoutPanelCreateColumn)
                return;
            if (comboBox.SelectedIndex == 5)
            {
                flowLayoutPanelCreateColumn.Controls[2].Visible = true;
                flowLayoutPanelCreateColumn.Controls[3].Visible = true;
                flowLayoutPanelCreateColumn.Controls[4].Visible = true;
            }
            else
            {
                flowLayoutPanelCreateColumn.Controls[2].Visible = false;
                flowLayoutPanelCreateColumn.Controls[3].Visible = false;
                flowLayoutPanelCreateColumn.Controls[4].Visible = false;
            }
        }

        private void numericUpDownRGB1_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDownRGB1)
                return;
            if (numericUpDownRGB1.Parent is not FlowLayoutPanel flowLayoutPanelRGB)
                return;
            if (flowLayoutPanelRGB.Controls[2] is not NumericUpDown numericUpDownRGB2)
                return;
            numericUpDownRGB2.Minimum = numericUpDownRGB1.Value;
        }

        private void numericUpDownRGB2_ValueChanged(object? sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDownRGB2)
                return;
            if (numericUpDownRGB2.Parent is not FlowLayoutPanel flowLayoutPanelRGB)
                return;
            if (flowLayoutPanelRGB.Controls[0] is not NumericUpDown numericUpDownRGB1)
                return;
            numericUpDownRGB1.Maximum = numericUpDownRGB2.Value;
        }

        private void enumerateColumns()
        {
            for (int i = 0; i < flowLayoutPanelColumns.Controls.Count; i++)
            {
                flowLayoutPanelColumns.Controls[i].Text = "Column #" + (i + 1);
                flowLayoutPanelColumns.Controls[i].Controls[0].Controls[5].Controls[0].Controls[0].Enabled = i != 0;
                flowLayoutPanelColumns.Controls[i].Controls[0].Controls[5].Controls[0].Controls[1].Enabled = i != flowLayoutPanelColumns.Controls.Count - 1;
            }
        }

        private void buttonMoveUp_Click(object? sender, EventArgs e)
        {
            if (sender is not Button buttonMoveUp)
                return;
            if (buttonMoveUp.Parent.Parent.Parent.Parent is not GroupBox groupBoxCreateColumn)
                return;
            int index = flowLayoutPanelColumns.Controls.GetChildIndex(groupBoxCreateColumn);
            if (index == 0)
                return;
            flowLayoutPanelColumns.Controls.SetChildIndex(groupBoxCreateColumn, index - 1);
            enumerateColumns();
        }

        private void buttonMoveDown_Click(object? sender, EventArgs e)
        {
            if (sender is not Button buttonMoveDown)
                return;
            if (buttonMoveDown.Parent.Parent.Parent.Parent is not GroupBox groupBoxCreateColumn)
                return;
            int index = flowLayoutPanelColumns.Controls.GetChildIndex(groupBoxCreateColumn);
            if (index == flowLayoutPanelColumns.Controls.Count - 1)
                return;
            flowLayoutPanelColumns.Controls.SetChildIndex(groupBoxCreateColumn, index + 1);
            enumerateColumns();
        }

        private void buttonRemove_Click(object? sender, EventArgs e)
        {
            if (sender is not Button buttonRemove)
                return;
            if (buttonRemove.Parent.Parent.Parent.Parent is not GroupBox groupBoxCreateColumn)
                return;
            flowLayoutPanelColumns.Controls.Remove(groupBoxCreateColumn);
            enumerateColumns();
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            FormCreateColumn formCreateColumn = new();
            formCreateColumn.textBoxName.TextChanged += textBox_TextChanged;
            formCreateColumn.comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
            formCreateColumn.comboBoxType.SelectedIndex = 0;
            formCreateColumn.numericUpDownR1.ValueChanged += numericUpDownRGB1_ValueChanged;
            formCreateColumn.numericUpDownR2.ValueChanged += numericUpDownRGB2_ValueChanged;
            formCreateColumn.numericUpDownG1.ValueChanged += numericUpDownRGB1_ValueChanged;
            formCreateColumn.numericUpDownG2.ValueChanged += numericUpDownRGB2_ValueChanged;
            formCreateColumn.numericUpDownB1.ValueChanged += numericUpDownRGB1_ValueChanged;
            formCreateColumn.numericUpDownB2.ValueChanged += numericUpDownRGB2_ValueChanged;
            formCreateColumn.buttonMoveUp.Click += buttonMoveUp_Click;
            formCreateColumn.buttonMoveDown.Click += buttonMoveDown_Click;
            formCreateColumn.buttonRemove.Click += buttonRemove_Click;
            GroupBox groupBoxCreateColumn = formCreateColumn.groupBoxCreateColumn;
            formCreateColumn.Controls.Remove(groupBoxCreateColumn);
            flowLayoutPanelColumns.Controls.Add(groupBoxCreateColumn);
            formCreateColumn.Dispose();
            enumerateColumns();
        }
    }
}
