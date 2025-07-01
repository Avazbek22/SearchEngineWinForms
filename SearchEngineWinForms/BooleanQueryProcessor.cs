using System.Collections.Generic;
using System.Linq;

namespace SearchEngineWinForms
{
	/// <summary>Выполняет булевы запросы: AND, OR, NOT, скобки.</summary>
	public sealed class BooleanQueryProcessor
	{
		private readonly InvertedIndex _index;
		private readonly QueryParser _parser;

		public BooleanQueryProcessor(InvertedIndex index)
		{
			_index = index;
			_parser = new QueryParser();
		}

		public IReadOnlyCollection<int> Execute(string query)
		{
			var postfix = _parser.ToPostfix(query);
			var stack = new Stack<HashSet<int>>();

			foreach (Token t in postfix)
			{
				switch (t.Type)
				{
					case TokenType.Term:
						stack.Push(_index.GetPostingList(t.Value).ToHashSet());
						break;

					case TokenType.Not:
						var setNot = stack.Pop();
						var all = Enumerable.Range(0, _index.DocumentCount).ToHashSet();
						all.ExceptWith(setNot);
						stack.Push(all);
						break;

					case TokenType.And:
						var rightAnd = stack.Pop();
						var leftAnd = stack.Pop();
						leftAnd.IntersectWith(rightAnd);
						stack.Push(leftAnd);
						break;

					case TokenType.Or:
						var rightOr = stack.Pop();
						var leftOr = stack.Pop();
						leftOr.UnionWith(rightOr);
						stack.Push(leftOr);
						break;
				}
			}

			return stack.Count == 1
				? stack.Pop().OrderBy(id => id).ToArray()
				: throw new QuerySyntaxException("Ошибка обработки запроса.");
		}
	}
}
