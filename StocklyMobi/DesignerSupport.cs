using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal static class DesignerSupport
    {
        public static bool IsDesignTime
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
                string process = Process.GetCurrentProcess().ProcessName ?? string.Empty;
                return process.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0
                    || process.IndexOf("DesignToolsServer", StringComparison.OrdinalIgnoreCase) >= 0
                    || process.IndexOf("XDesProc", StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        public static void AssignStableNames(Control root, string formName)
        {
            if (root == null) return;
            root.Name = formName;
            Dictionary<string, int> used = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            AssignChildren(root, used);
        }

        private static void AssignChildren(Control parent, Dictionary<string, int> used)
        {
            foreach (Control control in parent.Controls)
            {
                if (string.IsNullOrWhiteSpace(control.Name))
                {
                    string prefix = Prefix(control);
                    string stem = control is Button ? Clean(((Button)control).Text) : control.GetType().Name;
                    string key = prefix + stem;
                    int count;
                    used.TryGetValue(key, out count);
                    used[key] = ++count;
                    control.Name = key + (count == 1 ? string.Empty : count.ToString());
                }
                AssignChildren(control, used);
            }
        }

        private static string Prefix(Control control)
        {
            if (control is Button) return "btn";
            if (control is TextBox) return "txt";
            if (control is ComboBox) return "cmb";
            if (control is NumericUpDown) return "num";
            if (control is DataGridView) return "dgv";
            if (control is Label) return "lbl";
            if (control is DateTimePicker) return "dtp";
            if (control is CheckBox) return "chk";
            if (control is SplitContainer) return "split";
            if (control is TabControl) return "tabs";
            if (control is Panel) return "pnl";
            return "ctl";
        }

        private static string Clean(string value)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in value ?? string.Empty)
                if (char.IsLetterOrDigit(c)) result.Append(c);
            return result.Length == 0 ? "Button" : result.ToString();
        }
    }
}
