
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
				lblStatus.Text = "���������� ...";
				_index.BuildFromFolder(txtFolder.Text);
				lblDocCount.Text = $"����������: {_index.DocumentCount}";
				lblTermCount.Text = $"��������:   {_index.TermCount}";
				btnSearch.Enabled = true;
				lblStatus.Text = "���������� ���������.";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

				lblStatus.Text = $"������� ����������: {docIds.Count}";
			}
			catch (QuerySyntaxException qex)
			{
				MessageBox.Show(qex.Message, "�������������� ������ �������",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
