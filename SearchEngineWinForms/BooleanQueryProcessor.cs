
namespace SearchEngineWinForms
{
    /// <summary>Выполняет булевы запросы: AND, OR, NOT, скобки.</summary>
    public sealed class BooleanQueryProcessor(InvertedIndex index)
    {
        private readonly QueryParser _parser = new();

        public IReadOnlyCollection<int> Execute(string query)
        {
            var postfix = _parser.ToPostfix(query);
            var stack = new Stack<HashSet<int>>();

            foreach (Token t in postfix)
            {
                switch (t.Type)
                {
                    case TokenType.Term:
                        stack.Push(index.GetPostingList(t.Value).ToHashSet());
                        break;

                    case TokenType.Not:
                        var setNot = stack.Pop();
                        var all = Enumerable.Range(0, index.DocumentCount).ToHashSet();
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

            return (stack.Count == 1) ? stack.Pop().OrderBy(id => id).ToArray() : throw new QuerySyntaxException("Ошибка обработки запроса.");
        }
    }
}