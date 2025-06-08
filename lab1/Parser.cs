using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace lab1_compiler.Bar
{
    public class Parser
    {
        private List<string> tokens;
        private int position;
        private RichTextBox output;

        private HashSet<string> nouns = new() { "flight", "passenger", "trip", "morning" };
        private HashSet<string> verbs = new() { "is", "prefers", "like", "need", "depend", "fly" };
        private HashSet<string> adjectives = new() { "non-stop", "first", "direct" };

        public Parser(string input, RichTextBox outputBox)
        {
            tokens = new List<string>(input.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            position = 0;
            output = outputBox;
        }

        private string Current => position < tokens.Count ? tokens[position] : null;

        private void Log(string message)
        {
            output.AppendText(message + Environment.NewLine);
        }

        public void Parse()
        {
            Log("S");
            ParseS();

            if (position < tokens.Count)
                Log($"Остались необработанные токены: {string.Join(" ", tokens.GetRange(position, tokens.Count - position))}");
            else
                Log("Разбор завершён успешно.");
        }

        private void ParseS()
        {
            Log("Noun phrase");
            ParseNounPhrase();

            Log("Verb phrase");
            ParseVerbPhrase();
        }

        private void ParseNounPhrase()
        {
            Log("Noun phrase");

            if (Current == null)
            {
                Log("ε-переход (пустая фраза)");
                return;
            }

            if (nouns.Contains(Current))
            {
                Log($"Найден Noun: {Current}");
                position++;
                return;
            }

            if (adjectives.Contains(Current))
            {
                ParseAdjectivePhrase();

                if (nouns.Contains(Current))
                {
                    Log($"Найден Noun после прилагательных: {Current}");
                    position++;
                }
                else
                {
                    Log($"Ошибка: ожидался Noun, найдено '{Current ?? "конец"}'");
                }

                return;
            }

            Log($"Ошибка: ожидалась Noun phrase, найдено '{Current ?? "конец"}'");
        }

        private void ParseVerbPhrase()
        {
            Log("Verb phrase");

            if (Current == null)
            {
                Log("Ошибка: ожидался Verb, но достигнут конец ввода");
                return;
            }

            if (verbs.Contains(Current))
            {
                Log($"Найден Verb: {Current}");
                position++;
                ParseNounPhrase();
            }
            else
            {
                Log($"Ошибка: ожидался Verb, найдено '{Current}'");
            }
        }

        private void ParseAdjectivePhrase()
        {
            Log("Adjective phrase");

            while (Current != null && adjectives.Contains(Current))
            {
                Log($"Найден Adjective: {Current}");
                position++;
                Log("Adjective phrase");
            }

            if (Current == null || !adjectives.Contains(Current))
                Log("ε-переход (нет больше прилагательных)");
        }
    }
}
