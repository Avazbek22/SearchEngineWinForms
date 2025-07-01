using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SearchEngineWinForms
{
	internal enum TokenType { Term, And, Or, Not, LParen, RParen }

	internal readonly record struct Token(TokenType Type, string Value);

	/// <summary>Синтаксический анализатор булевых запросов.
	/// Реализует shunting-yard для преобразования в постфиксную запись.</summary>
	internal sealed class QueryParser
	{
		private static readonly Regex _tokenRegex = new(@"\s*(\()|(\))|AND\b|OR\b|NOT\b|[\p{L}\p{Nd}]{2,}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

		private static readonly Dictionary<string, TokenType> _keywords = new(StringComparer.OrdinalIgnoreCase)
		{
			["AND"] = TokenType.And,
			["OR"] = TokenType.Or,
			["NOT"] = TokenType.Not
		};

		private static readonly Dictionary<TokenType, int> _precedence = new()
		{
			[TokenType.Not] = 3,
			[TokenType.And] = 2,
			[TokenType.Or] = 1
		};

		public IEnumerable<Token> ToPostfix(string input)
		{
			var output = new List<Token>();
			var ops = new Stack<Token>();

			foreach (Match m in _tokenRegex.Matches(input))
			{
				string lexeme = m.Value.Trim();
				if (lexeme.Length == 0) continue;

				if (_keywords.TryGetValue(lexeme, out var kw))
				{
					if (kw == TokenType.Not)
					{
						while (ops.Count > 0 && ops.Peek().Type == TokenType.Not)
							output.Add(ops.Pop());
					}
					else
					{
						while (ops.Count > 0 &&
							   ops.Peek().Type != TokenType.LParen &&
							   _precedence[ops.Peek().Type] >= _precedence[kw])
						{
							output.Add(ops.Pop());
						}
					}
					ops.Push(new Token(kw, lexeme.ToLowerInvariant()));
				}
				else if (lexeme == "(")
				{
					ops.Push(new Token(TokenType.LParen, lexeme));
				}
				else if (lexeme == ")")
				{
					while (ops.Count > 0 && ops.Peek().Type != TokenType.LParen)
						output.Add(ops.Pop());
					if (ops.Count == 0)
						throw new QuerySyntaxException("Несогласованные скобки.");
					ops.Pop(); // снимаем '('
				}
				else // term
				{
					output.Add(new Token(TokenType.Term, lexeme.ToLowerInvariant()));
				}
			}

			while (ops.Count > 0)
			{
				var op = ops.Pop();
				if (op.Type is TokenType.LParen or TokenType.RParen)
					throw new QuerySyntaxException("Несогласованные скобки.");
				output.Add(op);
			}

			return output;
		}
	}

	public sealed class QuerySyntaxException : Exception
	{
		public QuerySyntaxException(string message) : base(message) { }
	}
}
