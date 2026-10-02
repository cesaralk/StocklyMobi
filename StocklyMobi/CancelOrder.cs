using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class CancelOrder : Form
    {
        DailySale dailySale;
        DBConnect dbcon = new DBConnect();

        public CancelOrder(DailySale sale)
        {
            InitializeComponent();
            dailySale = sale;
        }

        public bool PrepareForCancellation()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cboInventory.Text))
                    cboInventory.Text = "Yes";

                int remainingQty = GetRemainingCancellableQty();
                udCancelQty.Minimum = 1;
                udCancelQty.Maximum = remainingQty > 0 ? remainingQty : 1;
                udCancelQty.Value = remainingQty > 0 ? 1 : 1;

                if (remainingQty <= 0)
                {
                    MessageBox.Show("This sold item is already fully cancelled/returned.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
        }

        private void btnCOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cboInventory.Text))
                    cboInventory.Text = "Yes";

                if (udCancelQty.Value <= 0)
                {
                    MessageBox.Show("Cancel quantity must be greater than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    udCancelQty.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    MessageBox.Show("Please enter the cancellation reason.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReason.Focus();
                    return;
                }

                int remainingQty = GetRemainingCancellableQty();
                if (udCancelQty.Value > remainingQty)
                {
                    MessageBox.Show("Unable to cancel. Remaining sold quantity is " + remainingQty + ".", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Void @void = new Void(this);
                @void.txtUsername.Focus();
                @void.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetRemainingCancellableQty()
        {
            using (SqlConnection con = new SqlConnection(dbcon.myConnection()))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 qty
                    FROM tbCart
                    WHERE id = @id
                      AND status = 'Sold'", con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text);
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(result);
                }
            }
        }

        public void ReloadSoldList()
        {
            dailySale.LoadSold();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void CancelOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
