using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//AnalisadorLexico
namespace AnalisadorLexico
{
    public partial class Form1 : Form
    {
        #region Inicialização Forms e funções
        private SyncListBoxes _SyncListBoxes = null;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            const string message =
                "Esse aplicativo retorna comentários, identificadores (nomes de variáveis, funções, classes, métodos, etc.) e palavras " +
                "reservadas encontradas no código que é escrito no RichTextBox à esquerda da aplicação. Ao clicar em Analisar, " +
                "será colocado no primeiro ListBox os lexemas encontradas no código referentes àqueles que a aplicação está " +
                "construída para mostrar. O segundo ListBox mostra a posição e a classificação destes lexemas. A terceira ListBox aponta " +
                "como erro todos os outros lexemas (literais numéricos, operadores, " +
                "novas linhas, cadeias de caracteres, separadores, etc.) " +
                "não considerados como comentários, identificadores e palavras reservadas.";
            const string caption = "Leia-Me";
            MessageBox.Show(message, caption);
            this._SyncListBoxes = new SyncListBoxes(this.lbLexema, this.lbIdent);
        }
        private void  LimpaControles()
        {
            lbErro.Items.Clear();
            lbIdent.Items.Clear();
            lbLexema.Items.Clear();
            lbIndex.Items.Clear();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            #region Limpa Controles e seta variaveis

            LimpaControles();

            int l = 1, pos = 1, somapos = 0, posQL = 1;
            int caso = 0;
            string CodFonte = rtbCodigoFonte.Text;
            progressBar1.Value = 0;
            progressBar1.Maximum = CodFonte.Length;
            progressBar1.Visible = true;

            #endregion



            #region Regex geral


            Regex Identificador = new Regex("(#region.*$)|(\".*?\")|(\\$|\\^|\\!|\\$|\\%|\\^|\\&)(\\w+)|(\\p{Lu}|\\p{Ll}|\\p{Lt}|\\p{Lm}|\\p{Lo}|\\p{Nl}|_|@|\\\\)((\\p{Lu}|\\p{Ll}|\\p{Lt}|\\p{Lm}|\\p{L}|\\p{Lo}|\\p{Nl}|\\p{Nd}|\\p{Pc}|\\p{Mn}|\\p{Mc}|\\p{Cf})+)*|(\n|\r|\r\n)|(/\\*([^*]|[\\r\\n]|(\\*+([^*/]|[\\r\\n])))*\\*+/)|[0-9a-lA-L]+|((//((?!\\*/).)*)(?!\\*/)[^\\r\\n])|[()]|[=]|[[]|[]]|[<>]|[+]|[-]|[*]|(//)|[/]|[?]|[:]|[{}]|[;]", RegexOptions.Multiline);
            var Match = Identificador.Match(CodFonte);

            #endregion
            
            while (Match.Success)
            {

                #region Posicionamento dos itens

                if (String.IsNullOrEmpty(Match.ToString().Trim()))
                {
                    l++;
                    posQL = pos;
                    pos = 1;
                    somapos = Match.Index;
                    caso = -1;
                }
                pos = Match.Index - somapos;
                if (l == 1 && pos == 0)
                {
                    pos = 1;
                }

                #endregion

                #region Verifica Palavra Reservada

                List<string> palavraReservada = new List<string>
                {
                    "abstract", "add", "alias", "as", "ascending",
                    "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
                    "const", "continue", "decimal", "default", "delegate", "descending", "do",
                    "double", "dynamic", "else", "enum", "event", "exception", "explicit", "extern",
                    "false", "finally", "fixed", "float", "for", "foreach", "from", "get", "global",
                    "goto", "group", "if", "implicit", "in", "int", "interface", "internal", "into",
                    "is", "join", "let", "lock", "long", "namespace", "new", "null", "object", "operator",
                    "orderby", "out", "override", "params", "partial", "private", "protected",
                    "public", "readonly", "ref", "remove", "return", "sbyte", "sealed", "select",
                    "set", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
                    "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
                    "ushort", "using", "value", "var", "virtual", "void", "volatile", "where", "while", "yield"
                };

                foreach (string palavra in palavraReservada)
                {
                    if (palavra.Equals(Match.ToString()))
                    {
                        lbLexema.Items.Add(CodFonte.Substring(Match.Index, Match.Length));
                        lbIndex.Items.Add(Match.Index);
                        caso = 1;
                    }
                }

                #endregion

                #region Tratando endregion

                if (Match.ToString() == "endregion")
                    caso = 6;

                #endregion

                #region Verifica Identificadores

                Regex identificador = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
                var matchIdentificador = identificador.Match(Match.ToString());
                Regex identificadorLatino = new Regex("^[\\p{L}0-9_]+$", RegexOptions.Compiled);
                var matchIdentificadorLatino = identificadorLatino.Match(Match.ToString());
                if (caso != 1 && caso != 6 && matchIdentificador.Success)
                {
                    lbLexema.Items.Add(CodFonte.Substring(Match.Index, Match.Length));
                    lbIndex.Items.Add(Match.Index);
                    caso = 5;
                }
                else if (caso != 1 && caso != 6 && matchIdentificadorLatino.Success)
                {
                    Regex numeros = new Regex("[0-9]+", RegexOptions.Compiled);
                    var matchNumeros = numeros.Match(Match.ToString());
                    if (caso != 1 && caso != 6 && matchNumeros.Success)
                    {
                        caso = 4;
                    }
                    else
                    {
                        lbLexema.Items.Add(CodFonte.Substring(Match.Index, Match.Length));
                        lbIndex.Items.Add(Match.Index);
                        caso = 5;
                    }
                }

                #endregion

                #region Verifica Comentarios

                Regex comentario = new Regex("(//)|(/\\*[\\w\\W]*\\*/)|(/\\*([^*]|[\\r\\n]|(\\*+([^*/]|[\\r\\n])))*\\*+/)|((//((?!\\*/).)*)(?!\\*/)[^\\r\\n])");
                Regex cadeiaChar = new Regex("(\".*?\")");
                char aspa = Convert.ToChar(Match.ToString().Substring(0, 1));
                var matchCadeia = cadeiaChar.Match(Match.ToString());
                var matchComentario = comentario.Match(Match.ToString());
                if (aspa == '"' && matchComentario.Success)
                {
                    caso = 2;
                }
                else if (matchComentario.Success)
                {
                    lbLexema.Items.Add(CodFonte.Substring(Match.Index, Match.Length));
                    lbIndex.Items.Add(Match.Index);
                    caso = 3;
                    if (Match.ToString().Contains("/*"))
                    {
                        l++;
                    }
                }
                else
                {
                    Regex operador = new Regex("[+]|[-]|[*]|[/]|[?]|[:]|[=]");
                    var matchOperador = operador.Match(Match.ToString());
                    if (matchOperador.Success)
                    {
                        caso = 2;
                    }

                    else if (!matchOperador.Success && caso != 1 && !matchIdentificadorLatino.Success && !matchIdentificador.Success && !matchComentario.Success && caso != -1)
                    {
                        caso = 4;
                    }
                }

                #endregion

                #region Verifica Identificador como palavra reservada

                List<string> palavraReservadaComoIdentificador = new List<string>
                {
                    "@abstract","@add","@alias","@as","@ascending",
                    "@base","@bool","@break","@byte","@case","@catch","@char","@checked","@class",
                    "@const","@continue","@decimal","@default","@delegate","@descending","@do",
                    "@double","@dynamic","@else","@enum","@event","@exception","@explicit","@extern",
                    "@false","@finally","@fixed","@float","@for","@foreach","@from","@get","@global",
                    "@goto","@group","@if","@implicit","@in","@int","@interface","@internal","@into",
                    "@is","@join","@let","@lock","@long","@namespace","@new","@null","@object","@operator",
                    "@orderby","@out","@override","@params","@partial","@private","@protected",
                    "@public","@readonly","@ref","@remove","@return","@sbyte","@sealed","@select",
                    "@set","@short","@sizeof","@stackalloc","@static","@string","@struct","@switch",
                    "@this","@throw","@true","@try","@typeof","@uint","@ulong","@unchecked","@unsafe",
                    "@ushort","@using","@value","@var","@virtual","@void","@volatile","@where","@while","@yield"
                };

                foreach (string palavra in palavraReservadaComoIdentificador)
                {
                    if (palavra.Equals(Match.ToString()))
                    {
                        lbLexema.Items.Add(CodFonte.Substring(Match.Index, Match.Length));
                        lbIndex.Items.Add(Match.Index);
                        caso = 5;
                    }
                }

                #endregion
                progressBar1.Value = Match.Index;
                #region Switch/case listboxes

                switch (caso)
                {
                    case 1:
                        lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Palavra Reservada", l, pos));
                        break;
                    case 2:
                        //lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Operador", l, pos));
                        lbErro.Items.Add(String.Format("L: {0}, pos: {1}  {2}", l, pos, Match.ToString()));
                        break;
                    case 3:
                        lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Comentario", l, pos));
                        break;
                    case 4:
                        // lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Outro", l, pos));
                        lbErro.Items.Add(String.Format("L: {0}, pos: {1}   {2}", l, pos, Match.ToString()));
                        break;
                    case 5:
                        lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Identificador", l, pos));
                        break;
                    case 6:
                        //lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Chamada de Metodo", l-1, posQL));
                        lbErro.Items.Add(String.Format("L: {0}, pos: {1}  #endregion", l, pos));
                        break;
                    case -1:
                        //lbIdent.Items.Add(String.Format("L: {0}, pos: {1} - Quebra de Linha", l-1, posQL));
                        lbErro.Items.Add(String.Format("L: {0}, pos: {1}  \\n", l - 1, posQL + 1));
                        break;
                }

                caso = 0;
                Match = Match.NextMatch();

                #endregion

            }

            progressBar1.Value = CodFonte.Length;

            #region Comentarios

            //var match = buscador.Match(cadeia);
            //while (match.Success)
            //{
            //    Regex palavraReservada = new Regex("abstract|add|alias|as|ascending|" +
            //                            "base|bool|break|byte|case|catch|char|checked|" +
            //                            "class|const|continue|decimal|default|delegate|" +
            //                            "descending|do|double|dynamic|else|enum|event|exception|" +
            //                            "explicit|extern|false|finally|fixed|float|for|foreach|" +
            //                            "from|get|global|goto|group|if|implicit|in|int|interface|" +
            //                            "internal|into|is|join|let|lock|long|namespace|new|null|" +
            //                            "object|operator|orderby|out|override|params|partial|" +
            //                            "private|protected|public|readonly|ref|remove|return|" +
            //                            "sbyte|sealed|select|set|short|sizeof|stackalloc|static|" +
            //                            "string|struct|switch|this|throw|true|try|typeof|uint|ulong|" +
            //                            "unchecked|unsafe|ushort|using|value|var|virtual|void|volatile|" +
            //                            "where|while|yield");
            //    var matchPalavraReservada = palavraReservada.Match(match.ToString());
            //    while (matchPalavraReservada.Success)
            //    {
            //        lbLexema.Items.Add(cadeia.Substring(match.Index, match.Length));
            //        lbIdent.Items.Add("Palavra Reservada");
            //        match = match.NextMatch();
            //        matchPalavraReservada = palavraReservada.Match(match.ToString());

            //    }

            //    //CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            //    //if (provider.IsValidIdentifier(match.ToString()))
            //    //{
            //    //    lbLexema.Items.Add(cadeia.Substring(match.Index, match.Length) + " Valido");
            //    //}
            //    //else
            //    //{
            //    //    lbLexema.Items.Add(cadeia.Substring(match.Index, match.Length) + " Invalido");
            //    //}


            //    //lbLexema.Items.Add(cadeia.Substring(match.Index,match.Length));
            //    match = match.NextMatch();
            //}

            #endregion
        }
        #region Tooltip nos listboxes e sincronia com richtext

        private void lbLexema_MouseMove(object sender, MouseEventArgs e)
        {
            string strTip = "";

            //Get the item
            int nIdx = lbLexema.IndexFromPoint(e.Location);
            if ((nIdx >= 0) && (nIdx < lbLexema.Items.Count))
                strTip = lbLexema.Items[nIdx].ToString();

            toolTip1.SetToolTip(lbLexema, strTip);
        }

        private void lbIdent_MouseMove(object sender, MouseEventArgs e)
        {
            string strTip = "";

            //Get the item
            int nIdx = lbIdent.IndexFromPoint(e.Location);
            if ((nIdx >= 0) && (nIdx < lbIdent.Items.Count))
                strTip = lbIdent.Items[nIdx].ToString();

            toolTip1.SetToolTip(lbIdent, strTip);
        }

        private void lbErro_MouseMove(object sender, MouseEventArgs e)
        {
            string strTip = "";

            //Get the item
            int nIdx = lbErro.IndexFromPoint(e.Location);
            if ((nIdx >= 0) && (nIdx < lbErro.Items.Count))
                strTip = lbErro.Items[nIdx].ToString();

            toolTip1.SetToolTip(lbErro, strTip);
        }

        private void lbLexema_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbIndex.SelectedIndex = lbLexema.SelectedIndex;
            int posicao = Convert.ToInt32(lbIndex.SelectedItem.ToString());
            rtbCodigoFonte.SelectionStart = posicao;
            rtbCodigoFonte.Focus();

        }

        #endregion
    }
}
#region sincronizar List Boxes

public class SyncListBoxes
{

    private ListBox _LB1 = null;
    private ListBox _LB2 = null;

    private ListBoxScroll _ListBoxScroll1 = null;
    private ListBoxScroll _ListBoxScroll2 = null;

    public SyncListBoxes(ListBox LB1, ListBox LB2)
    {
        if (LB1 != null && LB1.IsHandleCreated && LB2 != null && LB2.IsHandleCreated &&
            LB1.Items.Count == LB2.Items.Count && LB1.Height == LB2.Height)
        {
            this._LB1 = LB1;
            this._ListBoxScroll1 = new ListBoxScroll(LB1);
            this._ListBoxScroll1.Scroll += _ListBoxScroll1_VerticalScroll;

            this._LB2 = LB2;
            this._ListBoxScroll2 = new ListBoxScroll(LB2);
            this._ListBoxScroll2.Scroll += _ListBoxScroll2_VerticalScroll;

            this._LB1.SelectedIndexChanged += _LB1_SelectedIndexChanged;
            this._LB2.SelectedIndexChanged += _LB2_SelectedIndexChanged;
        }
    }

    private void _LB1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this._LB2.TopIndex != this._LB1.TopIndex)
        {
            this._LB2.TopIndex = this._LB1.TopIndex;
        }
        if (this._LB2.SelectedIndex != this._LB1.SelectedIndex)
        {
            this._LB2.SelectedIndex = this._LB1.SelectedIndex;
        }
    }

    private void _LB2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this._LB1.TopIndex != this._LB2.TopIndex)
        {
            this._LB1.TopIndex = this._LB2.TopIndex;
        }
        if (this._LB1.SelectedIndex != this._LB2.SelectedIndex)
        {
            this._LB1.SelectedIndex = this._LB2.SelectedIndex;
        }
    }

    private void _ListBoxScroll1_VerticalScroll(ListBox LB)
    {
        if (this._LB2.TopIndex != this._LB1.TopIndex)
        {
            this._LB2.TopIndex = this._LB1.TopIndex;
        }
    }

    private void _ListBoxScroll2_VerticalScroll(ListBox LB)
    {
        if (this._LB1.TopIndex != this._LB2.TopIndex)
        {
            this._LB1.TopIndex = this._LB2.TopIndex;
        }
    }

    private class ListBoxScroll : NativeWindow
    {

        private ListBox _LB = null;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20a;

        public event dlgListBoxScroll Scroll;
        public delegate void dlgListBoxScroll(ListBox LB);

        public ListBoxScroll(ListBox LB)
        {
            this._LB = LB;
            this.AssignHandle(LB.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_VSCROLL:
                case WM_MOUSEWHEEL:
                    if (this.Scroll != null)
                    {
                        this.Scroll(_LB);
                    }
                    break;
            }
        }

    }







}


#endregion

