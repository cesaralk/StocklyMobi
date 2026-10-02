using System.Drawing;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal static class MobileUi
    {
        public static readonly Color Navy = Color.FromArgb(15, 23, 42);
        public static readonly Color Slate = Color.FromArgb(30, 41, 59);
        public static readonly Color Blue = Color.FromArgb(37, 99, 235);
        public static readonly Color Sky = Color.FromArgb(14, 165, 233);
        public static readonly Color Green = Color.FromArgb(16, 185, 129);
        public static readonly Color Amber = Color.FromArgb(245, 158, 11);
        public static readonly Color Red = Color.FromArgb(220, 38, 38);
        public static readonly Color Soft = Color.FromArgb(248, 250, 252);
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color Text = Color.FromArgb(51, 65, 85);

        public static Button Button(string text, Color color)
        {
            Button button = new Button();
            button.Text = text;
            button.Height = 42;
            button.AutoSize = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(6);
            return button;
        }

        public static Label Label(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = Text,
                Font = new Font("Century Gothic", 9F, FontStyle.Bold),
                Margin = new Padding(3, 7, 3, 4)
            };
        }

        public static TextBox TextBox(bool multiline = false)
        {
            return new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Century Gothic", 10F),
                Multiline = multiline,
                Height = multiline ? 72 : 30,
                Dock = DockStyle.Top
            };
        }

        public static ComboBox ComboBox()
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Century Gothic", 10F),
                Height = 30,
                Dock = DockStyle.Top
            };
        }

        public static NumericUpDown MoneyBox()
        {
            return new NumericUpDown
            {
                DecimalPlaces = 2,
                Increment = 0.01m,
                Maximum = 1000000000m,
                Minimum = 0m,
                ThousandsSeparator = true,
                Font = new Font("Century Gothic", 10F),
                Height = 30,
                Dock = DockStyle.Top
            };
        }

        public static DataGridView Grid()
        {
            DataGridView grid = new DataGridView();
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 36;
            grid.DefaultCellStyle.Font = new Font("Century Gothic", 9F);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Navy;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Soft;
            grid.RowTemplate.Height = 32;
            return grid;
        }

        public static Panel Card()
        {
            return new Panel
            {
                BackColor = Color.White,
                Padding = new Padding(14),
                Margin = new Padding(8)
            };
        }

        public static Label Header(string title, string subtitle)
        {
            return new Label
            {
                Text = title + (string.IsNullOrWhiteSpace(subtitle) ? string.Empty : "\r\n" + subtitle),
                Dock = DockStyle.Top,
                Height = string.IsNullOrWhiteSpace(subtitle) ? 52 : 72,
                Padding = new Padding(18, 10, 10, 4),
                BackColor = Navy,
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 14F, FontStyle.Bold)
            };
        }

        public static void ConfigureForm(Form form, string title, Size minimum)
        {
            form.Text = title;
            form.BackColor = Soft;
            form.Font = new Font("Century Gothic", 9.5F);
            form.MinimumSize = minimum;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
        }
    }
}
