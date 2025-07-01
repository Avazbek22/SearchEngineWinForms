
namespace SearchEngineWinForms
{
	/// <summary>Инвертированный индекс: term → список ID документов.</summary>
	public sealed class InvertedIndex
	{
		private readonly Dictionary<string, HashSet<int>> _index = new();
		private readonly List<Document> _documents = new();

		public int DocumentCount => _documents.Count;
		public int TermCount => _index.Count;

		public void BuildFromFolder(string folderPath)
		{
			_index.Clear();
			_documents.Clear();

			int id = 0;
			foreach (string file in Directory.GetFiles(folderPath, "*.txt"))
			{
				var doc = new Document(id++, file);
				_documents.Add(doc);

				foreach (string term in doc.GetTokens())
				{
					if (!_index.TryGetValue(term, out var set))
					{
						set = new HashSet<int>();
						_index[term] = set;
					}
					set.Add(doc.Id);
				}
			}
		}

		public IReadOnlyCollection<int> GetPostingList(string term) => _index.TryGetValue(term.ToLowerInvariant(), out var set) ? set : Array.Empty<int>();

		public string GetDocumentPath(int id) => _documents[id].Path;
	}
}
