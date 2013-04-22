namespace Mut3Decoder
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.carType = new System.Windows.Forms.TextBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.codingHex = new System.Windows.Forms.TextBox();
            this.btnDecode = new System.Windows.Forms.Button();
            this.itemValues = new System.Windows.Forms.DataGridView();
            this.codingHexNewRich = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.decode2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.carYear = new System.Windows.Forms.TextBox();
            this.carKind = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioEtacs = new System.Windows.Forms.RadioButton();
            this.radioMotor = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.diagVer = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemValues)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // carType
            // 
            this.carType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.carType.Location = new System.Drawing.Point(49, 19);
            this.carType.MaxLength = 4;
            this.carType.Name = "carType";
            this.carType.Size = new System.Drawing.Size(61, 20);
            this.carType.TabIndex = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle5;
            this.grid.Location = new System.Drawing.Point(12, 107);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 30;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(619, 477);
            this.grid.TabIndex = 13;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // codingHex
            // 
            this.codingHex.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codingHex.Location = new System.Drawing.Point(12, 51);
            this.codingHex.Name = "codingHex";
            this.codingHex.Size = new System.Drawing.Size(891, 21);
            this.codingHex.TabIndex = 5;
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(928, 50);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 6;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // itemValues
            // 
            this.itemValues.AllowUserToAddRows = false;
            this.itemValues.AllowUserToDeleteRows = false;
            this.itemValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            this.itemValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemValues.DefaultCellStyle = dataGridViewCellStyle6;
            this.itemValues.Location = new System.Drawing.Point(637, 107);
            this.itemValues.MultiSelect = false;
            this.itemValues.Name = "itemValues";
            this.itemValues.ReadOnly = true;
            this.itemValues.RowHeadersWidth = 30;
            this.itemValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemValues.Size = new System.Drawing.Size(266, 477);
            this.itemValues.TabIndex = 6;
            this.itemValues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemValues_CellDoubleClick);
            // 
            // codingHexNewRich
            // 
            this.codingHexNewRich.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codingHexNewRich.Location = new System.Drawing.Point(12, 77);
            this.codingHexNewRich.Name = "codingHexNewRich";
            this.codingHexNewRich.Size = new System.Drawing.Size(891, 22);
            this.codingHexNewRich.TabIndex = 8;
            this.codingHexNewRich.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(928, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Encode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // decode2
            // 
            this.decode2.Location = new System.Drawing.Point(928, 77);
            this.decode2.Name = "decode2";
            this.decode2.Size = new System.Drawing.Size(75, 23);
            this.decode2.TabIndex = 10;
            this.decode2.Text = "Decode";
            this.decode2.UseVisualStyleBackColor = true;
            this.decode2.Click += new System.EventHandler(this.decode2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Year";
            // 
            // carYear
            // 
            this.carYear.Location = new System.Drawing.Point(171, 19);
            this.carYear.MaxLength = 4;
            this.carYear.Name = "carYear";
            this.carYear.Size = new System.Drawing.Size(61, 20);
            this.carYear.TabIndex = 1;
            // 
            // carKind
            // 
            this.carKind.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.carKind.Location = new System.Drawing.Point(302, 19);
            this.carKind.MaxLength = 10;
            this.carKind.Name = "carKind";
            this.carKind.Size = new System.Drawing.Size(100, 20);
            this.carKind.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Kind";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioEtacs);
            this.groupBox1.Controls.Add(this.radioMotor);
            this.groupBox1.Location = new System.Drawing.Point(482, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 41);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ECU";
            // 
            // radioEtacs
            // 
            this.radioEtacs.AutoSize = true;
            this.radioEtacs.Location = new System.Drawing.Point(110, 18);
            this.radioEtacs.Name = "radioEtacs";
            this.radioEtacs.Size = new System.Drawing.Size(60, 17);
            this.radioEtacs.TabIndex = 1;
            this.radioEtacs.TabStop = true;
            this.radioEtacs.Text = "ETACS";
            this.radioEtacs.UseVisualStyleBackColor = true;
            // 
            // radioMotor
            // 
            this.radioMotor.AutoSize = true;
            this.radioMotor.Location = new System.Drawing.Point(17, 18);
            this.radioMotor.Name = "radioMotor";
            this.radioMotor.Size = new System.Drawing.Size(52, 17);
            this.radioMotor.TabIndex = 0;
            this.radioMotor.TabStop = true;
            this.radioMotor.Text = "Motor";
            this.radioMotor.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(742, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Diagversion";
            // 
            // diagVer
            // 
            this.diagVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.diagVer.FormatString = "N0";
            this.diagVer.FormattingEnabled = true;
            this.diagVer.Location = new System.Drawing.Point(811, 18);
            this.diagVer.Name = "diagVer";
            this.diagVer.Size = new System.Drawing.Size(53, 21);
            this.diagVer.TabIndex = 4;
            this.diagVer.Enter += new System.EventHandler(this.diagVer_Enter);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnDecode;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 596);
            this.Controls.Add(this.diagVer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.carKind);
            this.Controls.Add(this.carYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decode2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.codingHexNewRich);
            this.Controls.Add(this.itemValues);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.codingHex);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.carType);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1030, 630);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mut3 [de]coder by Pavel Ivlev (ivlevp@gmail.com)";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemValues)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox carType;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox codingHex;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.DataGridView itemValues;
        private System.Windows.Forms.RichTextBox codingHexNewRich;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button decode2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox carYear;
        private System.Windows.Forms.TextBox carKind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioEtacs;
        private System.Windows.Forms.RadioButton radioMotor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox diagVer;
    }
}

