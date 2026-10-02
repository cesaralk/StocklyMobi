namespace StocklyMobi
{
    partial class QuantityPromptForm
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components=new System.ComponentModel.Container();
            this.lblProduct=new System.Windows.Forms.Label();
            this.lblQuantity=new System.Windows.Forms.Label();
            this.numQuantity=new System.Windows.Forms.NumericUpDown();
            this.btnAdd=new System.Windows.Forms.Button();
            this.btnCancel=new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            this.lblProduct.AutoEllipsis=true;
            this.lblProduct.Font=new System.Drawing.Font("Century Gothic",9F,System.Drawing.FontStyle.Bold);
            this.lblProduct.ForeColor=System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblProduct.Location=new System.Drawing.Point(18,16);
            this.lblProduct.Name="lblProduct";
            this.lblProduct.Size=new System.Drawing.Size(395,42);
            this.lblProduct.Text="Product:";
            this.lblQuantity.Font=new System.Drawing.Font("Century Gothic",9F,System.Drawing.FontStyle.Bold);
            this.lblQuantity.ForeColor=System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblQuantity.Location=new System.Drawing.Point(18,66);
            this.lblQuantity.Name="lblQuantity";
            this.lblQuantity.Size=new System.Drawing.Size(120,28);
            this.lblQuantity.Text="Quantity";
            this.lblQuantity.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.numQuantity.Font=new System.Drawing.Font("Century Gothic",11F,System.Drawing.FontStyle.Bold);
            this.numQuantity.Location=new System.Drawing.Point(140,64);
            this.numQuantity.Maximum=1000000M;
            this.numQuantity.Minimum=1M;
            this.numQuantity.Name="numQuantity";
            this.numQuantity.Size=new System.Drawing.Size(270,29);
            this.numQuantity.TextAlign=System.Windows.Forms.HorizontalAlignment.Center;
            this.numQuantity.ThousandsSeparator=true;
            this.numQuantity.Value=1M;
            this.btnAdd.BackColor=System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.DialogResult=System.Windows.Forms.DialogResult.OK;
            this.btnAdd.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize=0;
            this.btnAdd.ForeColor=System.Drawing.Color.White;
            this.btnAdd.Location=new System.Drawing.Point(220,118);
            this.btnAdd.Name="btnAdd";
            this.btnAdd.Size=new System.Drawing.Size(90,42);
            this.btnAdd.Text="Add";
            this.btnAdd.UseVisualStyleBackColor=false;
            this.btnCancel.BackColor=System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCancel.DialogResult=System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize=0;
            this.btnCancel.ForeColor=System.Drawing.Color.White;
            this.btnCancel.Location=new System.Drawing.Point(320,118);
            this.btnCancel.Name="btnCancel";
            this.btnCancel.Size=new System.Drawing.Size(90,42);
            this.btnCancel.Text="Cancel";
            this.btnCancel.UseVisualStyleBackColor=false;
            this.AcceptButton=this.btnAdd;
            this.CancelButton=this.btnCancel;
            this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor=System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize=new System.Drawing.Size(434,184);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblProduct);
            this.Font=new System.Drawing.Font("Century Gothic",9.5F);
            this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox=false;
            this.MinimizeBox=false;
            this.MinimumSize=new System.Drawing.Size(390,220);
            this.Name="QuantityPromptForm";
            this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text="Stock-In Quantity";
            this.Shown += new System.EventHandler(this.QuantityPromptForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
