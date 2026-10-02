using System;
using System.Windows.Forms;
namespace StocklyMobi
{
    public sealed partial class TextEntryDialog : Form
    {
        public string EnteredText { get { return txtValue.Text.Trim(); } }
        public TextEntryDialog() { InitializeComponent(); }
        public TextEntryDialog(string message, string caption) : this() { Text=caption; lblMessage.Text=message; }
        public static string Show(string message, string caption)
        {
            using (TextEntryDialog form = new TextEntryDialog(message, caption))
                return form.ShowDialog() == DialogResult.OK ? form.EnteredText : string.Empty;
        }
        private void TextEntryDialog_Shown(object sender, EventArgs e) { txtValue.Focus(); txtValue.SelectAll(); }
    }
}
