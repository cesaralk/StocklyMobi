namespace StocklyMobi
{
    partial class TextEntryDialog
    {
        private System.ComponentModel.IContainer components=null;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();
        base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components=new System.ComponentModel.Container();
            this.lblMessage=new System.Windows.Forms.Label();
            this.txtValue=new System.Windows.Forms.TextBox();
            this.btnSave=new System.Windows.Forms.Button();
            this.btnCancel=new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblMessage.Font=new System.Drawing.Font("Century Gothic",9F,System.Drawing.FontStyle.Bold);
            this.lblMessage.ForeColor=System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblMessage.Location=new System.Drawing.Point(18,18);
            this.lblMessage.Name="lblMessage";
            this.lblMessage.Size=new System.Drawing.Size(420,24);
            this.lblMessage.Text="Enter value";
            this.txtValue.Font=new System.Drawing.Font("Century Gothic",10F);
            this.txtValue.Location=new System.Drawing.Point(18,48);
            this.txtValue.Name="txtValue";
            this.txtValue.Size=new System.Drawing.Size(420,28);
            this.btnSave.BackColor=System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSave.DialogResult=System.Windows.Forms.DialogResult.OK;
            this.btnSave.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize=0;
            this.btnSave.ForeColor=System.Drawing.Color.White;
            this.btnSave.Location=new System.Drawing.Point(250,94);
            this.btnSave.Name="btnSave";
            this.btnSave.Size=new System.Drawing.Size(90,42);
            this.btnSave.Text="Save";
            this.btnSave.UseVisualStyleBackColor=false;
            this.btnCancel.BackColor=System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCancel.DialogResult=System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize=0;
            this.btnCancel.ForeColor=System.Drawing.Color.White;
            this.btnCancel.Location=new System.Drawing.Point(348,94);
            this.btnCancel.Name="btnCancel";
            this.btnCancel.Size=new System.Drawing.Size(90,42);
            this.btnCancel.Text="Cancel";
            this.btnCancel.UseVisualStyleBackColor=false;
            this.AcceptButton=this.btnSave;
            this.CancelButton=this.btnCancel;
            this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor=System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize=new System.Drawing.Size(464,172);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblMessage);
            this.Font=new System.Drawing.Font("Century Gothic",9.5F);
            this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox=false;
            this.MinimizeBox=false;
            this.Name="TextEntryDialog";
            this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text="Enter Value";
            this.Shown += new System.EventHandler(this.TextEntryDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
