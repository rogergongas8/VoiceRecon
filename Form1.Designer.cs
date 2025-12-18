namespace VoiceRecon {
    partial class Form1 {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblUltimo = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.Text = "Iniciar voz";

            this.btnStop.Location = new System.Drawing.Point(120, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 30);
            this.btnStop.Text = "Parar voz";

            this.lblUltimo.Location = new System.Drawing.Point(12, 50);
            this.lblUltimo.Size = new System.Drawing.Size(200, 20);
            this.lblUltimo.Text = "Esperando...";

            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(12, 80);
            this.panelColor.Size = new System.Drawing.Size(208, 60);

            this.txtLog.Location = new System.Drawing.Point(12, 150);
            this.txtLog.Multiline = true;
            this.txtLog.Size = new System.Drawing.Size(208, 100);

            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.lblUltimo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "VoiceRecon";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblUltimo;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.TextBox txtLog;
    }
}