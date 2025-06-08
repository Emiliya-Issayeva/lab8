using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1_compiler.Bar
{
    public class Token
    {
        public string Value { get; set; }
        public string Type { get; set; } // Noun, verb, adjective, error
    }

    public class Lexer
    {
        private readonly HashSet<string> nouns = new() { "flight", "passenger", "trip", "morning" };
        private readonly HashSet<string> verbs = new() { "is", "prefers", "like", "need", "depend", "fly" };
        private readonly HashSet<string> adjectives = new() { "non-stop", "first", "direct" };

        public List<Token> Tokens { get; private set; } = new();
        public List<string> Errors { get; private set; } = new();

        public void Analyze(string input)
        {
            Tokens.Clear();
            Errors.Clear();

            var words = input.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int position = 1;

            foreach (var word in words)
            {
                string type;

                if (nouns.Contains(word)) type = "Noun";
                else if (verbs.Contains(word)) type = "Verb";
                else if (adjectives.Contains(word)) type = "Adjective";
                else
                {
                    type = "ERROR";
                    Errors.Add($"Ошибка: неизвестная лексема \"{word}\" (позиция {position})");
                }

                Tokens.Add(new Token { Value = word, Type = type });
                position++;
            }
        }
    }
}
