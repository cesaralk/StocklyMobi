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
    public partial class Category : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        private Panel panelSearch;
        private Label lblSearch;
        private TextBox txtSearch;
        public Category()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            InitializeSearchBar();
            ConfigureResponsiveLayout();
            LoadCategory();
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
            MinimumSize = new Size(800, 500);
            FormBorderStyle = FormBorderStyle.Sizable;

            Resize += (s, e) => ApplyResponsiveLayout();
            Shown += (s, e) => ApplyResponsiveLayout();

            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            if (panelSearch == null || panel1 == null || dgvCategory == null)
                return;

            SuspendLayout();

            panelSearch.Dock = DockStyle.None;
            panelSearch.SetBounds(0, 0, ClientSize.Width, 48);

            panel1.Dock = DockStyle.None;
            panel1.SetBounds(0, ClientSize.Height - 80, ClientSize.Width, 80);

            dgvCategory.Dock = DockStyle.None;
            dgvCategory.SetBounds(0, panelSearch.Bottom, ClientSize.Width, Math.Max(120, panel1.Top - panelSearch.Bottom));

            int textLeft = lblSearch.Right + 10;
            txtSearch.SetBounds(textLeft, 10, Math.Max(220, panelSearch.ClientSize.Width - textLeft - 12), 27);

            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.Location = new Point(panel1.ClientSize.Width - btnAdd.Width - 15, (panel1.ClientSize.Height - btnAdd.Height) / 2);

            ResumeLayout();
        }

        public void LoadCategory(string search = "")
        {
            int i = 0;
            dgvCategory.Rows.Clear();

            try
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();

                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbCategory WHERE category LIKE @search ORDER BY category", cn);
                cm.Parameters.AddWithValue("@search", "%" + search + "%");
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
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
            LoadCategory(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModule module = new CategoryModule(this);
            module.ShowDialog();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE id LIKE '" + dgvCategory[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Category has been successfully deleted.", "StocklyMobi POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (colName == "Edit")
            {
                CategoryModule module = new CategoryModule(this);
                module.lblId.Text = dgvCategory[1, e.RowIndex].Value.ToString();
                module.txtCategory.Text = dgvCategory[2, e.RowIndex].Value.ToString();
                module.btnSave.Enabled = false;
                module.btnUpdate.Enabled = true;
                module.ShowDialog();
            }
            LoadCategory();
        }
    }
}
