using System;
using System.Windows.Forms;
namespace StocklyMobi
{
    public sealed partial class QuantityPromptForm : Form
    {
        public int Quantity { get { return Decimal.ToInt32(numQuantity.Value); } }
        public QuantityPromptForm() { InitializeComponent(); }
        public QuantityPromptForm(string productDescription, int defaultValue) : this()
        {
            lblProduct.Text = "Product: " + productDescription;
            decimal value=Math.Max(numQuantity.Minimum,Math.Min(numQuantity.Maximum,defaultValue)); numQuantity.Value=value;
        }
        public static int ShowQuantity(string productDescription, int defaultValue)
        {
            using(QuantityPromptForm form=new QuantityPromptForm(productDescription,defaultValue))
                return form.ShowDialog()==DialogResult.OK ? form.Quantity : -1;
        }
        private void QuantityPromptForm_Shown(object sender, EventArgs e) { numQuantity.Focus(); numQuantity.Select(0,numQuantity.Text.Length); }
    }
}
