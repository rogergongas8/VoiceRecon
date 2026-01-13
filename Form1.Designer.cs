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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.panelListening = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpLastCommand = new System.Windows.Forms.GroupBox();
            this.progressConfidence = new System.Windows.Forms.ProgressBar();
            this.lblConfidence = new System.Windows.Forms.Label();
            this.grpColorDemo = new System.Windows.Forms.GroupBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.lblCommandCount = new System.Windows.Forms.Label();
            this.lblConfidenceThreshold = new System.Windows.Forms.Label();
            this.trackConfidence = new System.Windows.Forms.TrackBar();
            this.lblThresholdValue = new System.Windows.Forms.Label();
            this.grpCommands = new System.Windows.Forms.GroupBox();
            this.lblCommandsList = new System.Windows.Forms.Label();
            this.grpStatus.SuspendLayout();
            this.grpLastCommand.SuspendLayout();
            this.grpColorDemo.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackConfidence)).BeginInit();
            this.grpCommands.SuspendLayout();
            this.SuspendLayout();

            // btnStart
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(20, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(140, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "▶ Iniciar Voz";
            this.btnStart.UseVisualStyleBackColor = false;

            // btnStop
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(170, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(140, 45);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "⏹ Detener Voz";
            this.btnStop.UseVisualStyleBackColor = false;

            // grpStatus
            this.grpStatus.Controls.Add(this.panelListening);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpStatus.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpStatus.Location = new System.Drawing.Point(20, 80);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(290, 80);
            this.grpStatus.TabIndex = 2;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Estado del Sistema";

            // panelListening
            this.panelListening.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.panelListening.Location = new System.Drawing.Point(15, 25);
            this.panelListening.Name = "panelListening";
            this.panelListening.Size = new System.Drawing.Size(40, 40);
            this.panelListening.TabIndex = 0;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblStatus.Location = new System.Drawing.Point(65, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sistema detenido";

            // grpLastCommand
            this.grpLastCommand.Controls.Add(this.lblUltimo);
            this.grpLastCommand.Controls.Add(this.progressConfidence);
            this.grpLastCommand.Controls.Add(this.lblConfidence);
            this.grpLastCommand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLastCommand.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpLastCommand.Location = new System.Drawing.Point(20, 170);
            this.grpLastCommand.Name = "grpLastCommand";
            this.grpLastCommand.Size = new System.Drawing.Size(290, 100);
            this.grpLastCommand.TabIndex = 3;
            this.grpLastCommand.TabStop = false;
            this.grpLastCommand.Text = "Último Comando Reconocido";

            // lblUltimo
            this.lblUltimo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUltimo.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblUltimo.Location = new System.Drawing.Point(10, 25);
            this.lblUltimo.Name = "lblUltimo";
            this.lblUltimo.Size = new System.Drawing.Size(270, 25);
            this.lblUltimo.TabIndex = 0;
            this.lblUltimo.Text = "Esperando...";
            this.lblUltimo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // progressConfidence
            this.progressConfidence.Location = new System.Drawing.Point(10, 70);
            this.progressConfidence.Name = "progressConfidence";
            this.progressConfidence.Size = new System.Drawing.Size(270, 20);
            this.progressConfidence.TabIndex = 1;

            // lblConfidence
            this.lblConfidence.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfidence.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblConfidence.Location = new System.Drawing.Point(10, 50);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new System.Drawing.Size(270, 20);
            this.lblConfidence.TabIndex = 2;
            this.lblConfidence.Text = "Confianza: 0%";
            this.lblConfidence.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // grpColorDemo
            this.grpColorDemo.Controls.Add(this.panelColor);
            this.grpColorDemo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpColorDemo.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpColorDemo.Location = new System.Drawing.Point(20, 280);
            this.grpColorDemo.Name = "grpColorDemo";
            this.grpColorDemo.Size = new System.Drawing.Size(290, 110);
            this.grpColorDemo.TabIndex = 4;
            this.grpColorDemo.TabStop = false;
            this.grpColorDemo.Text = "Demostración de Colores";

            // panelColor
            this.panelColor.BackColor = System.Drawing.Color.White;
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(10, 25);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(270, 75);
            this.panelColor.TabIndex = 0;

            // grpLog
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLog.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpLog.Location = new System.Drawing.Point(320, 170);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(560, 220);
            this.grpLog.TabIndex = 5;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Registro de Actividad";

            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.txtLog.Location = new System.Drawing.Point(10, 25);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(540, 185);
            this.txtLog.TabIndex = 0;

            // grpStats
            this.grpStats.Controls.Add(this.lblCommandCount);
            this.grpStats.Controls.Add(this.lblConfidenceThreshold);
            this.grpStats.Controls.Add(this.trackConfidence);
            this.grpStats.Controls.Add(this.lblThresholdValue);
            this.grpStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpStats.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpStats.Location = new System.Drawing.Point(320, 20);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(560, 140);
            this.grpStats.TabIndex = 6;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Estadísticas y Configuración";

            // lblCommandCount
            this.lblCommandCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCommandCount.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblCommandCount.Location = new System.Drawing.Point(10, 25);
            this.lblCommandCount.Name = "lblCommandCount";
            this.lblCommandCount.Size = new System.Drawing.Size(540, 25);
            this.lblCommandCount.TabIndex = 0;
            this.lblCommandCount.Text = "Comandos ejecutados: 0";

            // lblConfidenceThreshold
            this.lblConfidenceThreshold.AutoSize = true;
            this.lblConfidenceThreshold.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfidenceThreshold.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblConfidenceThreshold.Location = new System.Drawing.Point(10, 60);
            this.lblConfidenceThreshold.Name = "lblConfidenceThreshold";
            this.lblConfidenceThreshold.Size = new System.Drawing.Size(200, 15);
            this.lblConfidenceThreshold.TabIndex = 1;
            this.lblConfidenceThreshold.Text = "Umbral mínimo de confianza:";

            // trackConfidence
            this.trackConfidence.Location = new System.Drawing.Point(10, 85);
            this.trackConfidence.Maximum = 100;
            this.trackConfidence.Minimum = 10;
            this.trackConfidence.Name = "trackConfidence";
            this.trackConfidence.Size = new System.Drawing.Size(470, 45);
            this.trackConfidence.TabIndex = 2;
            this.trackConfidence.TickFrequency = 10;
            this.trackConfidence.Value = 70;

            // lblThresholdValue
            this.lblThresholdValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblThresholdValue.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblThresholdValue.Location = new System.Drawing.Point(485, 85);
            this.lblThresholdValue.Name = "lblThresholdValue";
            this.lblThresholdValue.Size = new System.Drawing.Size(65, 25);
            this.lblThresholdValue.TabIndex = 3;
            this.lblThresholdValue.Text = "70%";
            this.lblThresholdValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // grpCommands
            this.grpCommands.Controls.Add(this.lblCommandsList);
            this.grpCommands.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCommands.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.grpCommands.Location = new System.Drawing.Point(20, 400);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.Size = new System.Drawing.Size(860, 180);
            this.grpCommands.TabIndex = 7;
            this.grpCommands.TabStop = false;
            this.grpCommands.Text = "Comandos Disponibles";

            // lblCommandsList
            this.lblCommandsList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCommandsList.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblCommandsList.Location = new System.Drawing.Point(10, 25);
            this.lblCommandsList.Name = "lblCommandsList";
            this.lblCommandsList.Size = new System.Drawing.Size(840, 145);
            this.lblCommandsList.TabIndex = 0;
            this.lblCommandsList.Text = "• \"hola\" - Muestra un saludo\r\n• \"limpiar\" - Limpia el registro\r\n• \"salir\" - Cierra la aplicación\r\n• \"color rojo/verde/azul/amarillo/morado/naranja\" - Cambia el color del panel\r\n• \"abrir bloc de notas\" - Abre el Bloc de notas\r\n• \"abrir navegador\" - Abre Wikipedia\r\n• \"abrir calculadora\" - Abre la Calculadora\r\n• \"minimizar\" - Minimiza la ventana\r\n• \"maximizar\" - Maximiza la ventana";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.grpCommands);
            this.Controls.Add(this.grpStats);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpColorDemo);
            this.Controls.Add(this.grpLastCommand);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VoiceRecon Pro - Reconocimiento de Voz Avanzado";
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpLastCommand.ResumeLayout(false);
            this.grpColorDemo.ResumeLayout(false);
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackConfidence)).EndInit();
            this.grpCommands.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblUltimo;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Panel panelListening;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpLastCommand;
        private System.Windows.Forms.ProgressBar progressConfidence;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.GroupBox grpColorDemo;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.Label lblCommandCount;
        private System.Windows.Forms.Label lblConfidenceThreshold;
        private System.Windows.Forms.TrackBar trackConfidence;
        private System.Windows.Forms.Label lblThresholdValue;
        private System.Windows.Forms.GroupBox grpCommands;
        private System.Windows.Forms.Label lblCommandsList;
    }
}