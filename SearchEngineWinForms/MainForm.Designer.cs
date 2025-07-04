﻿
namespace SearchEngineWinForms
{
	partial class MainForm
	{
		/// <summary>Обязательная переменная конструктора.</summary>
		private System.ComponentModel.IContainer components = null;

		#region Поля-контролы
		private System.Windows.Forms.Button btnSelectFolder;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Button btnIndex;
		private System.Windows.Forms.Label lblDocCount;
		private System.Windows.Forms.Label lblTermCount;
		private System.Windows.Forms.TextBox txtQuery;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.ListBox lstResults;
		private System.Windows.Forms.Label lblStatus;
		#endregion

		/// <summary>Создание и конфигурация всех контролов.</summary>
		private void InitializeComponent()
		{
			btnSelectFolder = new Button();
			txtFolder = new TextBox();
			btnIndex = new Button();
			lblDocCount = new Label();
			lblTermCount = new Label();
			txtQuery = new TextBox();
			btnSearch = new Button();
			lstResults = new ListBox();
			lblStatus = new Label();
			SuspendLayout();
			// 
			// btnSelectFolder
			// 
			btnSelectFolder.Location = new Point(14, 16);
			btnSelectFolder.Margin = new Padding(3, 4, 3, 4);
			btnSelectFolder.Name = "btnSelectFolder";
			btnSelectFolder.Size = new Size(137, 40);
			btnSelectFolder.TabIndex = 0;
			btnSelectFolder.Text = "Выбрать каталог";
			btnSelectFolder.UseVisualStyleBackColor = true;
			btnSelectFolder.Click += btnSelectFolder_Click;
			// 
			// txtFolder
			// 
			txtFolder.Location = new Point(160, 21);
			txtFolder.Margin = new Padding(3, 4, 3, 4);
			txtFolder.Name = "txtFolder";
			txtFolder.ReadOnly = true;
			txtFolder.Size = new Size(548, 27);
			txtFolder.TabIndex = 1;
			// 
			// btnIndex
			// 
			btnIndex.Location = new Point(720, 16);
			btnIndex.Margin = new Padding(3, 4, 3, 4);
			btnIndex.Name = "btnIndex";
			btnIndex.Size = new Size(131, 40);
			btnIndex.TabIndex = 2;
			btnIndex.Text = "Индексировать";
			btnIndex.UseVisualStyleBackColor = true;
			btnIndex.Click += btnIndex_Click;
			// 
			// lblDocCount
			// 
			lblDocCount.AutoSize = true;
			lblDocCount.Location = new Point(14, 67);
			lblDocCount.Name = "lblDocCount";
			lblDocCount.Size = new Size(0, 20);
			lblDocCount.TabIndex = 3;
			// 
			// lblTermCount
			// 
			lblTermCount.AutoSize = true;
			lblTermCount.Location = new Point(229, 67);
			lblTermCount.Name = "lblTermCount";
			lblTermCount.Size = new Size(0, 20);
			lblTermCount.TabIndex = 4;
			// 
			// txtQuery
			// 
			txtQuery.Location = new Point(14, 107);
			txtQuery.Margin = new Padding(3, 4, 3, 4);
			txtQuery.Name = "txtQuery";
			txtQuery.Size = new Size(685, 27);
			txtQuery.TabIndex = 5;
			// 
			// btnSearch
			// 
			btnSearch.Location = new Point(720, 104);
			btnSearch.Margin = new Padding(3, 4, 3, 4);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(131, 36);
			btnSearch.TabIndex = 6;
			btnSearch.Text = "Поиск";
			btnSearch.UseVisualStyleBackColor = true;
			btnSearch.Click += btnSearch_Click;
			// 
			// lstResults
			// 
			lstResults.FormattingEnabled = true;
			lstResults.Location = new Point(14, 147);
			lstResults.Margin = new Padding(3, 4, 3, 4);
			lstResults.Name = "lstResults";
			lstResults.Size = new Size(837, 444);
			lstResults.TabIndex = 7;
			// 
			// lblStatus
			// 
			lblStatus.AutoSize = true;
			lblStatus.Location = new Point(14, 563);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(0, 20);
			lblStatus.TabIndex = 8;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(865, 600);
			Controls.Add(lblStatus);
			Controls.Add(lstResults);
			Controls.Add(btnSearch);
			Controls.Add(txtQuery);
			Controls.Add(lblTermCount);
			Controls.Add(lblDocCount);
			Controls.Add(btnIndex);
			Controls.Add(txtFolder);
			Controls.Add(btnSelectFolder);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(3, 4, 3, 4);
			MaximizeBox = false;
			Name = "MainForm";
			Text = "Поисковая система — Инвертированный индекс";
			ResumeLayout(false);
			PerformLayout();
		}

		/// <summary>Очистка всех используемых ресурсов.</summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
