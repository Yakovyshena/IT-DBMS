namespace DBMS.Forms
{
    partial class FormCreateTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Label labelTableName;
            FlowLayoutPanel flowLayoutPanelTableName;
            Button buttonAddColumn;
            textBoxTableName = new TextBox();
            buttonSaveTable = new Button();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            flowLayoutPanelAll = new FlowLayoutPanel();
            groupBoxColumns = new GroupBox();
            flowLayoutPanelColumns = new FlowLayoutPanel();
            labelTableName = new Label();
            flowLayoutPanelTableName = new FlowLayoutPanel();
            buttonAddColumn = new Button();
            flowLayoutPanelTableName.SuspendLayout();
            flowLayoutPanelButtons.SuspendLayout();
            flowLayoutPanelAll.SuspendLayout();
            groupBoxColumns.SuspendLayout();
            SuspendLayout();
            // 
            // labelTableName
            // 
            labelTableName.AutoSize = true;
            labelTableName.Location = new Point(3, 0);
            labelTableName.MinimumSize = new Size(0, 27);
            labelTableName.Name = "labelTableName";
            labelTableName.Size = new Size(88, 27);
            labelTableName.TabIndex = 0;
            labelTableName.Text = "Table name:";
            labelTableName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // flowLayoutPanelTableName
            // 
            flowLayoutPanelTableName.AutoSize = true;
            flowLayoutPanelTableName.Controls.Add(labelTableName);
            flowLayoutPanelTableName.Controls.Add(textBoxTableName);
            flowLayoutPanelTableName.Location = new Point(3, 3);
            flowLayoutPanelTableName.Name = "flowLayoutPanelTableName";
            flowLayoutPanelTableName.Size = new Size(160, 33);
            flowLayoutPanelTableName.TabIndex = 2;
            // 
            // textBoxTableName
            // 
            textBoxTableName.Location = new Point(97, 3);
            textBoxTableName.Name = "textBoxTableName";
            textBoxTableName.Size = new Size(60, 27);
            textBoxTableName.TabIndex = 1;
            textBoxTableName.TextChanged += textBox_TextChanged;
            // 
            // buttonAddColumn
            // 
            buttonAddColumn.Location = new Point(3, 3);
            buttonAddColumn.Name = "buttonAddColumn";
            buttonAddColumn.Size = new Size(115, 29);
            buttonAddColumn.TabIndex = 3;
            buttonAddColumn.Text = "Add column";
            buttonAddColumn.UseVisualStyleBackColor = true;
            buttonAddColumn.Click += buttonAddColumn_Click;
            // 
            // buttonSaveTable
            // 
            buttonSaveTable.Location = new Point(124, 3);
            buttonSaveTable.Name = "buttonSaveTable";
            buttonSaveTable.Size = new Size(115, 29);
            buttonSaveTable.TabIndex = 4;
            buttonSaveTable.Text = "Save table";
            buttonSaveTable.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelButtons
            // 
            flowLayoutPanelButtons.AutoSize = true;
            flowLayoutPanelButtons.Controls.Add(buttonAddColumn);
            flowLayoutPanelButtons.Controls.Add(buttonSaveTable);
            flowLayoutPanelButtons.Location = new Point(3, 74);
            flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            flowLayoutPanelButtons.Size = new Size(242, 35);
            flowLayoutPanelButtons.TabIndex = 5;
            // 
            // flowLayoutPanelAll
            // 
            flowLayoutPanelAll.AutoScroll = true;
            flowLayoutPanelAll.Controls.Add(flowLayoutPanelTableName);
            flowLayoutPanelAll.Controls.Add(groupBoxColumns);
            flowLayoutPanelAll.Controls.Add(flowLayoutPanelButtons);
            flowLayoutPanelAll.Dock = DockStyle.Fill;
            flowLayoutPanelAll.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelAll.Location = new Point(0, 0);
            flowLayoutPanelAll.Name = "flowLayoutPanelAll";
            flowLayoutPanelAll.Size = new Size(800, 450);
            flowLayoutPanelAll.TabIndex = 0;
            flowLayoutPanelAll.WrapContents = false;
            // 
            // groupBoxColumns
            // 
            groupBoxColumns.AutoSize = true;
            groupBoxColumns.Controls.Add(flowLayoutPanelColumns);
            groupBoxColumns.Location = new Point(3, 42);
            groupBoxColumns.MinimumSize = new Size(100, 20);
            groupBoxColumns.Name = "groupBoxColumns";
            groupBoxColumns.Size = new Size(100, 26);
            groupBoxColumns.TabIndex = 6;
            groupBoxColumns.TabStop = false;
            groupBoxColumns.Text = "Columns";
            // 
            // flowLayoutPanelColumns
            // 
            flowLayoutPanelColumns.AutoSize = true;
            flowLayoutPanelColumns.Dock = DockStyle.Fill;
            flowLayoutPanelColumns.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelColumns.Location = new Point(3, 23);
            flowLayoutPanelColumns.Name = "flowLayoutPanelColumns";
            flowLayoutPanelColumns.Size = new Size(94, 0);
            flowLayoutPanelColumns.TabIndex = 0;
            flowLayoutPanelColumns.WrapContents = false;
            // 
            // FormCreateTable
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanelAll);
            Name = "FormCreateTable";
            Text = "Create new table";
            flowLayoutPanelTableName.ResumeLayout(false);
            flowLayoutPanelTableName.PerformLayout();
            flowLayoutPanelButtons.ResumeLayout(false);
            flowLayoutPanelAll.ResumeLayout(false);
            flowLayoutPanelAll.PerformLayout();
            groupBoxColumns.ResumeLayout(false);
            groupBoxColumns.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelAll;
        private Label labelTableName;
        private FlowLayoutPanel flowLayoutPanelTableName;
        private Button buttonAddColumn;
        private FlowLayoutPanel flowLayoutPanelButtons;
        private Button buttonSaveTable;
        public TextBox textBoxTableName;
        private GroupBox groupBoxColumns;
        public FlowLayoutPanel flowLayoutPanelColumns;
    }
}