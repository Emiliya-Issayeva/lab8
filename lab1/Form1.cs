using lab1_compiler.Bar;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;

namespace lab1
{
    public partial class Compiler : Form
    {
        private readonly FileWorks _fileWorks;
        private readonly Correction _correction;
        private readonly Reference _reference;

        private readonly string _helpPath = "C:\\Users\\issay\\Downloads\\lab1\\lab1\\lab1\\Help.html";
        private readonly string _aboutPath = "C:\\Users\\issay\\Downloads\\lab1\\lab1\\lab1\\About.html";

        public Compiler()
        {
            InitializeComponent();
            _fileWorks = new FileWorks(this);
            _correction = new Correction(richTextBox1);
            _reference = new Reference(_helpPath, _aboutPath);
            richTextBox1.TextChanged += richTextBox1_TextChanged;

            this.FormClosing += Compiler_FormClosing;

            this.MinimumSize = new Size(600, 400);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            _fileWorks.MarkAsModified();
        }

        public RichTextBox Editor => richTextBox1;

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _fileWorks.CreateNewFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _fileWorks.OpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveFile();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _correction.Undo();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            _correction.Redo();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            _correction.Copy();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            _correction.Cut();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            _correction.Paste();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            RunCombinedAnalysis();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            _reference.ShowHelp();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            _reference.ShowAbout();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.CreateNewFile();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveAsFile();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.Exit();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Delete();
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.SelectAll();
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reference.ShowHelp();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reference.ShowAbout();
        }

        public void ChangeFontSize(float newSize)
        {
            if (newSize < 8 || newSize > 72) return;

            Editor.Font = new Font(Editor.Font.FontFamily, newSize, Editor.Font.Style);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Compiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            // если CheckUnsavedChanges() возвращает true, значит пользователь нажал "Cancel"
            if (_fileWorks.CheckUnsavedChanges())
            {
                e.Cancel = true; // отменяем закрытие окна
            }
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCombinedAnalysis();
        }

        private void RunCombinedAnalysis()
        {
            string inputText = richTextBox1.Text;
            richTextBox2.Clear();

            // Лексический анализ
            Lexer lexer = new Lexer();
            lexer.Analyze(inputText);

            richTextBox2.AppendText("Лексический анализ:\n");
            foreach (var token in lexer.Tokens)
            {
                if (token.Type == "ERROR")
                    richTextBox2.AppendText($"Ошибка: неизвестная лексема \"{token.Value}\"\n");
                else
                    richTextBox2.AppendText($"[{token.Type}] \"{token.Value}\"\n");
            }

            if (lexer.Errors.Count > 0)
            {
                richTextBox2.AppendText("Найдены лексические ошибки. Синтаксический анализ будет выполнен с возможными ошибками.\n\n");
            }
            else
            {
                richTextBox2.AppendText("Ошибок не обнаружено.\n\n");
            }

            // Синтаксический анализ
            richTextBox2.AppendText("Синтаксический анализ:\n");
            Parser parser = new Parser(inputText, richTextBox2);
            parser.Parse();
        }

    }
}