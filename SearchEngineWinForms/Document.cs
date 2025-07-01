using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchEngineWinForms
{
	/// <summary>Простая модель документа — текстовый файл.</summary>
	public sealed class Document
	{
		public int Id { get; }
		public string Path { get; }
		public string Content { get; }

		public Document(int id, string path)
		{
			Id = id;
			Path = path;
			Content = File.ReadAllText(path, Encoding.UTF8);
		}

		/// <summary>Получить лексемы, приведённые к нижнему регистру,
		///     без знаков препинания, длиной ≥ 2 символов.</summary>
		public IEnumerable<string> GetTokens()
		{
			foreach (Match m in Regex.Matches(Content.ToLowerInvariant(), @"\b[\p{L}\p{Nd}]{2,}\b", RegexOptions.CultureInvariant))
			{
				yield return m.Value;
			}
		}
	}
}
