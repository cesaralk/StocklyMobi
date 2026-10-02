using System;
using System.Windows.Forms;
namespace StocklyMobi
{
    public sealed partial class ValidityRetakeDialog : Form
    {
        public decimal RetakeAmount { get { return _retake.Value; } }
        public ValidityRetakeDialog() { InitializeComponent(); }
        public ValidityRetakeDialog(string serviceName, string operatorName, decimal price) : this()
        {
            lblHeader.Text = "Validity Recharge\r\nThe Retake $ amount will be added to the " + operatorName + " line balance after settlement.";
            lblServiceValue.Text = serviceName; lblPriceValue.Text = "$ " + price.ToString("N2");
        }
    }
}
