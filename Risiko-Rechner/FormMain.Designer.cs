namespace Risiko_Rechner
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.Textausgabe = new System.Windows.Forms.TextBox();
            this.AttackerUnitBox1 = new System.Windows.Forms.ComboBox();
            this.DefenderUnitBox6 = new System.Windows.Forms.ComboBox();
            this.AttackerUnitNumber1 = new System.Windows.Forms.NumericUpDown();
            this.DefenderUnitNumber6 = new System.Windows.Forms.NumericUpDown();
            this.Resetknopf = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Faehigkeitswahl = new System.Windows.Forms.ComboBox();
            this.AllgFaehigkeiten = new System.Windows.Forms.Button();
            this.blub = new System.Windows.Forms.NumericUpDown();
            this.Textausgabe2 = new System.Windows.Forms.TextBox();
            this.DefenderUnitNumber7 = new System.Windows.Forms.NumericUpDown();
            this.AttackerUnitNumber2 = new System.Windows.Forms.NumericUpDown();
            this.DefenderUnitBox7 = new System.Windows.Forms.ComboBox();
            this.AttackerUnitBox2 = new System.Windows.Forms.ComboBox();
            this.DefenderUnitNumber8 = new System.Windows.Forms.NumericUpDown();
            this.AttackerUnitNumber3 = new System.Windows.Forms.NumericUpDown();
            this.DefenderUnitBox8 = new System.Windows.Forms.ComboBox();
            this.AttackerUnitBox3 = new System.Windows.Forms.ComboBox();
            this.DefenderUnitNumber9 = new System.Windows.Forms.NumericUpDown();
            this.AttackerUnitNumber4 = new System.Windows.Forms.NumericUpDown();
            this.DefenderUnitBox9 = new System.Windows.Forms.ComboBox();
            this.AttackerUnitBox4 = new System.Windows.Forms.ComboBox();
            this.DefenderUnitNumber10 = new System.Windows.Forms.NumericUpDown();
            this.AttackerUnitNumber5 = new System.Windows.Forms.NumericUpDown();
            this.DefenderUnitBox10 = new System.Windows.Forms.ComboBox();
            this.AttackerUnitBox5 = new System.Windows.Forms.ComboBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpAttacker = new System.Windows.Forms.GroupBox();
            this.grpDefender = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber5)).BeginInit();
            this.grpAttacker.SuspendLayout();
            this.grpDefender.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "FIGHT!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Textausgabe
            // 
            this.Textausgabe.Location = new System.Drawing.Point(484, 85);
            this.Textausgabe.Multiline = true;
            this.Textausgabe.Name = "Textausgabe";
            this.Textausgabe.ReadOnly = true;
            this.Textausgabe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Textausgabe.Size = new System.Drawing.Size(488, 598);
            this.Textausgabe.TabIndex = 1;
            // 
            // AttackerUnitBox1
            // 
            this.AttackerUnitBox1.FormattingEnabled = true;
            this.AttackerUnitBox1.Location = new System.Drawing.Point(72, 20);
            this.AttackerUnitBox1.Name = "AttackerUnitBox1";
            this.AttackerUnitBox1.Size = new System.Drawing.Size(121, 21);
            this.AttackerUnitBox1.TabIndex = 2;
            this.AttackerUnitBox1.SelectedIndexChanged += new System.EventHandler(this.UnitBox1_SelectedIndexChanged);
            // 
            // DefenderUnitBox6
            // 
            this.DefenderUnitBox6.FormattingEnabled = true;
            this.DefenderUnitBox6.Location = new System.Drawing.Point(74, 20);
            this.DefenderUnitBox6.Name = "DefenderUnitBox6";
            this.DefenderUnitBox6.Size = new System.Drawing.Size(121, 21);
            this.DefenderUnitBox6.TabIndex = 3;
            this.DefenderUnitBox6.SelectedIndexChanged += new System.EventHandler(this.UnitBox6_SelectedIndexChanged);
            // 
            // AttackerUnitNumber1
            // 
            this.AttackerUnitNumber1.Location = new System.Drawing.Point(6, 21);
            this.AttackerUnitNumber1.Name = "AttackerUnitNumber1";
            this.AttackerUnitNumber1.Size = new System.Drawing.Size(60, 20);
            this.AttackerUnitNumber1.TabIndex = 4;
            // 
            // DefenderUnitNumber6
            // 
            this.DefenderUnitNumber6.Location = new System.Drawing.Point(6, 21);
            this.DefenderUnitNumber6.Name = "DefenderUnitNumber6";
            this.DefenderUnitNumber6.Size = new System.Drawing.Size(60, 20);
            this.DefenderUnitNumber6.TabIndex = 5;
            // 
            // Resetknopf
            // 
            this.Resetknopf.Location = new System.Drawing.Point(849, 18);
            this.Resetknopf.Name = "Resetknopf";
            this.Resetknopf.Size = new System.Drawing.Size(104, 42);
            this.Resetknopf.TabIndex = 8;
            this.Resetknopf.Text = "Reset";
            this.Resetknopf.UseVisualStyleBackColor = true;
            this.Resetknopf.Click += new System.EventHandler(this.ResetProgram);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 555);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Allgemeine Fähigkeiten";
            // 
            // Faehigkeitswahl
            // 
            this.Faehigkeitswahl.FormattingEnabled = true;
            this.Faehigkeitswahl.Location = new System.Drawing.Point(14, 578);
            this.Faehigkeitswahl.Name = "Faehigkeitswahl";
            this.Faehigkeitswahl.Size = new System.Drawing.Size(204, 21);
            this.Faehigkeitswahl.TabIndex = 10;
            // 
            // AllgFaehigkeiten
            // 
            this.AllgFaehigkeiten.Location = new System.Drawing.Point(248, 569);
            this.AllgFaehigkeiten.Name = "AllgFaehigkeiten";
            this.AllgFaehigkeiten.Size = new System.Drawing.Size(204, 36);
            this.AllgFaehigkeiten.TabIndex = 11;
            this.AllgFaehigkeiten.Text = "Fähigkeit einsetzen";
            this.AllgFaehigkeiten.UseVisualStyleBackColor = true;
            // 
            // blub
            // 
            this.blub.Location = new System.Drawing.Point(158, 624);
            this.blub.Name = "blub";
            this.blub.Size = new System.Drawing.Size(60, 20);
            this.blub.TabIndex = 12;
            // 
            // Textausgabe2
            // 
            this.Textausgabe2.Location = new System.Drawing.Point(248, 611);
            this.Textausgabe2.Multiline = true;
            this.Textausgabe2.Name = "Textausgabe2";
            this.Textausgabe2.ReadOnly = true;
            this.Textausgabe2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Textausgabe2.Size = new System.Drawing.Size(204, 73);
            this.Textausgabe2.TabIndex = 0;
            // 
            // DefenderUnitNumber7
            // 
            this.DefenderUnitNumber7.Location = new System.Drawing.Point(6, 48);
            this.DefenderUnitNumber7.Name = "DefenderUnitNumber7";
            this.DefenderUnitNumber7.Size = new System.Drawing.Size(60, 20);
            this.DefenderUnitNumber7.TabIndex = 16;
            // 
            // AttackerUnitNumber2
            // 
            this.AttackerUnitNumber2.Location = new System.Drawing.Point(6, 48);
            this.AttackerUnitNumber2.Name = "AttackerUnitNumber2";
            this.AttackerUnitNumber2.Size = new System.Drawing.Size(60, 20);
            this.AttackerUnitNumber2.TabIndex = 15;
            // 
            // DefenderUnitBox7
            // 
            this.DefenderUnitBox7.FormattingEnabled = true;
            this.DefenderUnitBox7.Location = new System.Drawing.Point(74, 47);
            this.DefenderUnitBox7.Name = "DefenderUnitBox7";
            this.DefenderUnitBox7.Size = new System.Drawing.Size(121, 21);
            this.DefenderUnitBox7.TabIndex = 14;
            this.DefenderUnitBox7.SelectedIndexChanged += new System.EventHandler(this.UnitBox7_SelectedIndexChanged);
            // 
            // AttackerUnitBox2
            // 
            this.AttackerUnitBox2.FormattingEnabled = true;
            this.AttackerUnitBox2.Location = new System.Drawing.Point(72, 47);
            this.AttackerUnitBox2.Name = "AttackerUnitBox2";
            this.AttackerUnitBox2.Size = new System.Drawing.Size(121, 21);
            this.AttackerUnitBox2.TabIndex = 13;
            this.AttackerUnitBox2.SelectedIndexChanged += new System.EventHandler(this.UnitBox2_SelectedIndexChanged);
            // 
            // DefenderUnitNumber8
            // 
            this.DefenderUnitNumber8.Location = new System.Drawing.Point(6, 75);
            this.DefenderUnitNumber8.Name = "DefenderUnitNumber8";
            this.DefenderUnitNumber8.Size = new System.Drawing.Size(60, 20);
            this.DefenderUnitNumber8.TabIndex = 20;
            // 
            // AttackerUnitNumber3
            // 
            this.AttackerUnitNumber3.Location = new System.Drawing.Point(6, 75);
            this.AttackerUnitNumber3.Name = "AttackerUnitNumber3";
            this.AttackerUnitNumber3.Size = new System.Drawing.Size(60, 20);
            this.AttackerUnitNumber3.TabIndex = 19;
            // 
            // DefenderUnitBox8
            // 
            this.DefenderUnitBox8.FormattingEnabled = true;
            this.DefenderUnitBox8.Location = new System.Drawing.Point(74, 74);
            this.DefenderUnitBox8.Name = "DefenderUnitBox8";
            this.DefenderUnitBox8.Size = new System.Drawing.Size(121, 21);
            this.DefenderUnitBox8.TabIndex = 18;
            this.DefenderUnitBox8.SelectedIndexChanged += new System.EventHandler(this.UnitBox8_SelectedIndexChanged);
            // 
            // AttackerUnitBox3
            // 
            this.AttackerUnitBox3.FormattingEnabled = true;
            this.AttackerUnitBox3.Location = new System.Drawing.Point(72, 74);
            this.AttackerUnitBox3.Name = "AttackerUnitBox3";
            this.AttackerUnitBox3.Size = new System.Drawing.Size(121, 21);
            this.AttackerUnitBox3.TabIndex = 17;
            this.AttackerUnitBox3.SelectedIndexChanged += new System.EventHandler(this.UnitBox3_SelectedIndexChanged);
            // 
            // DefenderUnitNumber9
            // 
            this.DefenderUnitNumber9.Location = new System.Drawing.Point(6, 102);
            this.DefenderUnitNumber9.Name = "DefenderUnitNumber9";
            this.DefenderUnitNumber9.Size = new System.Drawing.Size(60, 20);
            this.DefenderUnitNumber9.TabIndex = 24;
            // 
            // AttackerUnitNumber4
            // 
            this.AttackerUnitNumber4.Location = new System.Drawing.Point(6, 102);
            this.AttackerUnitNumber4.Name = "AttackerUnitNumber4";
            this.AttackerUnitNumber4.Size = new System.Drawing.Size(60, 20);
            this.AttackerUnitNumber4.TabIndex = 23;
            // 
            // DefenderUnitBox9
            // 
            this.DefenderUnitBox9.FormattingEnabled = true;
            this.DefenderUnitBox9.Location = new System.Drawing.Point(74, 101);
            this.DefenderUnitBox9.Name = "DefenderUnitBox9";
            this.DefenderUnitBox9.Size = new System.Drawing.Size(121, 21);
            this.DefenderUnitBox9.TabIndex = 22;
            this.DefenderUnitBox9.SelectedIndexChanged += new System.EventHandler(this.UnitBox9_SelectedIndexChanged);
            // 
            // AttackerUnitBox4
            // 
            this.AttackerUnitBox4.FormattingEnabled = true;
            this.AttackerUnitBox4.Location = new System.Drawing.Point(72, 101);
            this.AttackerUnitBox4.Name = "AttackerUnitBox4";
            this.AttackerUnitBox4.Size = new System.Drawing.Size(121, 21);
            this.AttackerUnitBox4.TabIndex = 21;
            this.AttackerUnitBox4.SelectedIndexChanged += new System.EventHandler(this.UnitBox4_SelectedIndexChanged);
            // 
            // DefenderUnitNumber10
            // 
            this.DefenderUnitNumber10.Location = new System.Drawing.Point(6, 129);
            this.DefenderUnitNumber10.Name = "DefenderUnitNumber10";
            this.DefenderUnitNumber10.Size = new System.Drawing.Size(60, 20);
            this.DefenderUnitNumber10.TabIndex = 28;
            // 
            // AttackerUnitNumber5
            // 
            this.AttackerUnitNumber5.Location = new System.Drawing.Point(6, 129);
            this.AttackerUnitNumber5.Name = "AttackerUnitNumber5";
            this.AttackerUnitNumber5.Size = new System.Drawing.Size(60, 20);
            this.AttackerUnitNumber5.TabIndex = 27;
            // 
            // DefenderUnitBox10
            // 
            this.DefenderUnitBox10.FormattingEnabled = true;
            this.DefenderUnitBox10.Location = new System.Drawing.Point(74, 128);
            this.DefenderUnitBox10.Name = "DefenderUnitBox10";
            this.DefenderUnitBox10.Size = new System.Drawing.Size(121, 21);
            this.DefenderUnitBox10.TabIndex = 26;
            // 
            // AttackerUnitBox5
            // 
            this.AttackerUnitBox5.FormattingEnabled = true;
            this.AttackerUnitBox5.Location = new System.Drawing.Point(72, 128);
            this.AttackerUnitBox5.Name = "AttackerUnitBox5";
            this.AttackerUnitBox5.Size = new System.Drawing.Size(121, 21);
            this.AttackerUnitBox5.TabIndex = 25;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(14, 247);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(204, 274);
            this.checkedListBox1.TabIndex = 29;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(248, 247);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(204, 274);
            this.checkedListBox2.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 626);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "gegnerische Einheitenstärke";
            // 
            // grpAttacker
            // 
            this.grpAttacker.Controls.Add(this.AttackerUnitBox1);
            this.grpAttacker.Controls.Add(this.AttackerUnitNumber1);
            this.grpAttacker.Controls.Add(this.AttackerUnitBox2);
            this.grpAttacker.Controls.Add(this.AttackerUnitNumber2);
            this.grpAttacker.Controls.Add(this.AttackerUnitNumber5);
            this.grpAttacker.Controls.Add(this.AttackerUnitBox3);
            this.grpAttacker.Controls.Add(this.AttackerUnitNumber3);
            this.grpAttacker.Controls.Add(this.AttackerUnitBox5);
            this.grpAttacker.Controls.Add(this.AttackerUnitBox4);
            this.grpAttacker.Controls.Add(this.AttackerUnitNumber4);
            this.grpAttacker.Location = new System.Drawing.Point(14, 83);
            this.grpAttacker.Name = "grpAttacker";
            this.grpAttacker.Size = new System.Drawing.Size(204, 158);
            this.grpAttacker.TabIndex = 32;
            this.grpAttacker.TabStop = false;
            this.grpAttacker.Text = "Angreifer";
            // 
            // grpDefender
            // 
            this.grpDefender.Controls.Add(this.DefenderUnitBox6);
            this.grpDefender.Controls.Add(this.DefenderUnitNumber6);
            this.grpDefender.Controls.Add(this.DefenderUnitBox7);
            this.grpDefender.Controls.Add(this.DefenderUnitNumber7);
            this.grpDefender.Controls.Add(this.DefenderUnitBox8);
            this.grpDefender.Controls.Add(this.DefenderUnitNumber10);
            this.grpDefender.Controls.Add(this.DefenderUnitNumber8);
            this.grpDefender.Controls.Add(this.DefenderUnitBox10);
            this.grpDefender.Controls.Add(this.DefenderUnitBox9);
            this.grpDefender.Controls.Add(this.DefenderUnitNumber9);
            this.grpDefender.Location = new System.Drawing.Point(248, 83);
            this.grpDefender.Name = "grpDefender";
            this.grpDefender.Size = new System.Drawing.Size(204, 158);
            this.grpDefender.TabIndex = 33;
            this.grpDefender.TabStop = false;
            this.grpDefender.Text = "Verteidiger";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 702);
            this.Controls.Add(this.grpDefender);
            this.Controls.Add(this.grpAttacker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkedListBox2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.Textausgabe2);
            this.Controls.Add(this.blub);
            this.Controls.Add(this.AllgFaehigkeiten);
            this.Controls.Add(this.Faehigkeitswahl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Resetknopf);
            this.Controls.Add(this.Textausgabe);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoadUnits);
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefenderUnitNumber10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackerUnitNumber5)).EndInit();
            this.grpAttacker.ResumeLayout(false);
            this.grpDefender.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Textausgabe;
        private System.Windows.Forms.ComboBox AttackerUnitBox1;
        private System.Windows.Forms.ComboBox DefenderUnitBox6;
        private System.Windows.Forms.NumericUpDown AttackerUnitNumber1;
        private System.Windows.Forms.NumericUpDown DefenderUnitNumber6;
        private System.Windows.Forms.Button Resetknopf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Faehigkeitswahl;
        private System.Windows.Forms.Button AllgFaehigkeiten;
        private System.Windows.Forms.NumericUpDown blub;
        private System.Windows.Forms.TextBox Textausgabe2;
        private System.Windows.Forms.NumericUpDown DefenderUnitNumber7;
        private System.Windows.Forms.NumericUpDown AttackerUnitNumber2;
        private System.Windows.Forms.ComboBox DefenderUnitBox7;
        private System.Windows.Forms.ComboBox AttackerUnitBox2;
        private System.Windows.Forms.NumericUpDown DefenderUnitNumber8;
        private System.Windows.Forms.NumericUpDown AttackerUnitNumber3;
        private System.Windows.Forms.ComboBox DefenderUnitBox8;
        private System.Windows.Forms.ComboBox AttackerUnitBox3;
        private System.Windows.Forms.NumericUpDown DefenderUnitNumber9;
        private System.Windows.Forms.NumericUpDown AttackerUnitNumber4;
        private System.Windows.Forms.ComboBox DefenderUnitBox9;
        private System.Windows.Forms.ComboBox AttackerUnitBox4;
        private System.Windows.Forms.NumericUpDown DefenderUnitNumber10;
        private System.Windows.Forms.NumericUpDown AttackerUnitNumber5;
        private System.Windows.Forms.ComboBox DefenderUnitBox10;
        private System.Windows.Forms.ComboBox AttackerUnitBox5;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpAttacker;
        private System.Windows.Forms.GroupBox grpDefender;
    }
}

