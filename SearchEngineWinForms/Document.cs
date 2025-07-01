using System.Text;
using System.Text.RegularExpressions;

namespace SearchEngineWinForms
{
	/// <summary>Простая модель документа — текстовый файл.</summary>
	public sealed class Document(int id, string path)
	{
		public int Id { get; } = id;
		public string Path { get; } = path;
		public string Content { get; } = File.ReadAllText(path, Encoding.UTF8);

		/// <summary>Получить лексемы, приведённые к нижнему регистру, без знаков препинания, длиной ≥ 2 символов.</summary>
		public IEnumerable<string> GetTokens()
		{
			foreach (Match m in Regex.Matches(Content.ToLowerInvariant(), @"\b[\p{L}\p{Nd}]{2,}\b", RegexOptions.CultureInvariant))
			{
				yield return m.Value;
			}
		}
	}
}
