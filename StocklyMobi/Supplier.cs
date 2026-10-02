using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocklyMobi
{
    public partial class Supplier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        private Panel panelSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        public Supplier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            InitializeSearchBar();
            ConfigureResponsiveLayout();
            LoadSupplier();
        }

        private void InitializeSearchBar()
        {
            panelSearch = new Panel();
            panelSearch.Height = 48;
            panelSearch.BackColor = Color.White;
            panelSearch.Padding = new Padding(12, 10, 12, 8);

            lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 14);

            txtSearch = new TextBox();
            txtSearch.Name = "txtSearch";
            txtSearch.Location = new Point(80, 10);
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.TextChanged += txtSearch_TextChanged;

            panelSearch.Controls.Add(lblSearch);
            panelSearch.Controls.Add(txtSearch);
            Controls.Add(panelSearch);
            panelSearch.BringToFront();
        }

        private void ConfigureResponsiveLayout()
        {
            MinimumSize = new Size(900, 500);
            FormBorderStyle = FormBorderStyle.Sizable;

            Resize += (s, e) => ApplyResponsiveLayout();
            Shown += (s, e) => ApplyResponsiveLayout();

            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (panelSearch == null || panel1 == null || dgvSupplier == null)
                return;

            SuspendLayout();

            panelSearch.Dock = DockStyle.None;
            panelSearch.SetBounds(0, 0, ClientSize.Width, 48);

            panel1.Dock = DockStyle.None;
            panel1.SetBounds(0, ClientSize.Height - 80, ClientSize.Width, 80);

            dgvSupplier.Dock = DockStyle.None;
            dgvSupplier.SetBounds(0, panelSearch.Bottom, ClientSize.Width, Math.Max(120, panel1.Top - panelSearch.Bottom));

            int textLeft = lblSearch.Right + 10;
            txtSearch.SetBounds(textLeft, 10, Math.Max(220, panelSearch.ClientSize.Width - textLeft - 12), 27);

            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.Location = new Point(panel1.ClientSize.Width - btnAdd.Width - 15, (panel1.ClientSize.Height - btnAdd.Height) / 2);

            ResumeLayout();
        }

        public void LoadSupplier(string search = "")
        {
            dgvSupplier.Rows.Clear();
            int i = 0;

            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                cn.Open();
                cm = new SqlCommand(@"SELECT *
                                      FROM tbSupplier
                                      WHERE supplier LIKE @search
                                         OR address LIKE @search
                                         OR contactperson LIKE @search
                                         OR phone LIKE @search
                                         OR email LIKE @search
                                         OR fax LIKE @search
                                      ORDER BY supplier", cn);
                cm.Parameters.AddWithValue("@search", "%" + search + "%");
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvSupplier.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                    dr.Close();
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierModule supplierModule = new SupplierModule(this);
            supplierModule.ShowDialog();
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSupplier.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                SupplierModule supplierModule = new SupplierModule(this);
                supplierModule.lblId.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                supplierModule.txtSupplier.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                supplierModule.txtAddress.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                supplierModule.txtConPerson.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                supplierModule.txtPhone.Text = dgvSupplier.Rows[e.RowIndex].Cells[5].Value.ToString();
                supplierModule.txtEmail.Text = dgvSupplier.Rows[e.RowIndex].Cells[6].Value.ToString();
                supplierModule.txtFaxNo.Text = dgvSupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

                supplierModule.btnSave.Enabled = false;
                supplierModule.btnUpdate.Enabled = true;
                supplierModule.ShowDialog();
            }
            else if(colName=="Delete")
            {
                if (MessageBox.Show("Delete this record? click yes to confirm", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Delete from tbSupplier where id like '" + dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            LoadSupplier();
        }
    }
}
