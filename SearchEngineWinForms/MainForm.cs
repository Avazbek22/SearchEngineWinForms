
namespace SearchEngineWinForms
{
	public partial class MainForm : Form
	{
		private readonly InvertedIndex _index = new();
		private readonly BooleanQueryProcessor _processor;

		public MainForm()
		{
			InitializeComponent();
			_processor = new BooleanQueryProcessor(_index);
			btnSearch.Enabled = false;
			btnIndex.Enabled = false;
		}

		private void btnSelectFolder_Click(object sender, EventArgs e)
		{
			using var dialog = new FolderBrowserDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtFolder.Text = dialog.SelectedPath;
				btnIndex.Enabled = Directory.GetFiles(dialog.SelectedPath, "*.txt").Length > 0;
			}
		}

		private void btnIndex_Click(object sender, EventArgs e)
		{
			try
			{
				lblStatus.Text = "Индексация ...";
				_index.BuildFromFolder(txtFolder.Text);
				lblDocCount.Text = $"Документов: {_index.DocumentCount}";
				lblTermCount.Text = $"Терминов:   {_index.TermCount}";
				btnSearch.Enabled = true;
				lblStatus.Text = "Индексация завершена.";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				lstResults.Items.Clear();
				IReadOnlyCollection<int> docIds =
					_processor.Execute(txtQuery.Text);

				foreach (int id in docIds)
				{
					lstResults.Items.Add(_index.GetDocumentPath(id));
				}

				lblStatus.Text = $"Найдено документов: {docIds.Count}";
			}
			catch (QuerySyntaxException qex)
			{
				MessageBox.Show(qex.Message, "Синтаксическая ошибка запроса",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
