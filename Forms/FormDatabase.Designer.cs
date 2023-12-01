namespace DBMS.Forms
{
    partial class FormDatabase
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
            MenuStrip menuStrip;
            ToolStripMenuItem newToolStripMenuItem;
            ToolStripMenuItem openToolStripMenuItem;
            ToolStripMenuItem saveToolStripMenuItem;
            ToolStripMenuItem saveAsToolStripMenuItem;
            ToolStripMenuItem exitToolStripMenuItem;
            Button buttonCreateTable;
            splitContainer = new SplitContainer();
            panel1 = new Panel();
            groupBoxDifferences = new GroupBox();
            groupBoxTables = new GroupBox();
            menuStrip = new MenuStrip();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            buttonCreateTable = new Button();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, exitToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1161, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(118, 24);
            newToolStripMenuItem.Text = "New database";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(59, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(54, 24);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(74, 24);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(47, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // buttonCreateTable
            // 
            buttonCreateTable.AutoEllipsis = true;
            buttonCreateTable.Dock = DockStyle.Top;
            buttonCreateTable.Location = new Point(0, 0);
            buttonCreateTable.Name = "buttonCreateTable";
            buttonCreateTable.Size = new Size(266, 29);
            buttonCreateTable.TabIndex = 1;
            buttonCreateTable.Text = "Create new table";
            buttonCreateTable.UseVisualStyleBackColor = true;
            buttonCreateTable.Click += buttonCreateTable_Click;
            // 
            // splitContainer
            // 
            splitContainer.BackColor = SystemColors.AppWorkspace;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.FixedPanel = FixedPanel.Panel1;
            splitContainer.Location = new Point(0, 28);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.BackColor = SystemColors.Control;
            splitContainer.Panel1.Controls.Add(panel1);
            splitContainer.Panel1.Controls.Add(buttonCreateTable);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.BackColor = SystemColors.Control;
            splitContainer.Panel2.SizeChanged += splitContainer_Panel2_SizeChanged;
            splitContainer.Size = new Size(1161, 560);
            splitContainer.SplitterDistance = 266;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(groupBoxDifferences);
            panel1.Controls.Add(groupBoxTables);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(266, 531);
            panel1.TabIndex = 5;
            // 
            // groupBoxDifferences
            // 
            groupBoxDifferences.AutoSize = true;
            groupBoxDifferences.Dock = DockStyle.Top;
            groupBoxDifferences.Location = new Point(0, 30);
            groupBoxDifferences.MinimumSize = new Size(0, 30);
            groupBoxDifferences.Name = "groupBoxDifferences";
            groupBoxDifferences.Size = new Size(266, 30);
            groupBoxDifferences.TabIndex = 4;
            groupBoxDifferences.TabStop = false;
            groupBoxDifferences.Text = "Table differences";
            // 
            // groupBoxTables
            // 
            groupBoxTables.AutoSize = true;
            groupBoxTables.Dock = DockStyle.Top;
            groupBoxTables.Location = new Point(0, 0);
            groupBoxTables.MinimumSize = new Size(0, 30);
            groupBoxTables.Name = "groupBoxTables";
            groupBoxTables.Size = new Size(266, 30);
            groupBoxTables.TabIndex = 3;
            groupBoxTables.TabStop = false;
            groupBoxTables.Text = "Tables";
            // 
            // FormDatabase
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1161, 588);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormDatabase";
            Text = "FormDatabase";
            FormClosing += FormDatabase_FormClosing;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private SplitContainer splitContainer;
        private Button buttonCreateTable;
        private GroupBox groupBoxTables;
        private GroupBox groupBoxDifferences;
        private Panel panel1;
    }
}