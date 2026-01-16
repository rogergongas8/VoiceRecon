using System;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;
using System.Diagnostics;

namespace VoiceRecon {
    public class Form1 : Form {
        // Controles de la interfaz
        private Button btnStart;
        private Button btnStop;
        private Label lblUltimo;
        private Panel panelColor;
        private RichTextBox txtLog;
        private GroupBox grpStatus;
        private Panel panelListening;
        private Label lblStatus;
        private GroupBox grpLastCommand;
        private ProgressBar progressConfidence;
        private Label lblConfidence;
        private GroupBox grpColorDemo;
        private GroupBox grpLog;
        private GroupBox grpStats;
        private Label lblCommandCount;
        private Label lblConfidenceThreshold;
        private TrackBar trackConfidence;
        private Label lblThresholdValue;
        private GroupBox grpCommands;
        private Label lblCommandsList;

        // Variables para el reconocimiento de voz
        private SpeechRecognitionEngine reconocedor;
        private Timer temporizador;
        private int paso = 0;
        private int contadorComandos = 0;
        private double nivelConfianza = 0.7;

        public Form1() {
            InitializeComponent();
            
            // Eventos del formulario
            this.Shown += Form1_Shown;
            btnStart.Click += BotonIniciar_Click;
            btnStop.Click += BotonDetener_Click;
            trackConfidence.ValueChanged += CambioConfianza;
            
            // Timer para la animacion
            temporizador = new Timer();
            temporizador.Interval = 500;
            temporizador.Tick += AnimarCirculo;
        }

        private void InitializeComponent() {
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.lblUltimo = new Label();
            this.panelColor = new Panel();
            this.txtLog = new RichTextBox();
            this.grpStatus = new GroupBox();
            this.panelListening = new Panel();
            this.lblStatus = new Label();
            this.grpLastCommand = new GroupBox();
            this.progressConfidence = new ProgressBar();
            this.lblConfidence = new Label();
            this.grpColorDemo = new GroupBox();
            this.grpLog = new GroupBox();
            this.grpStats = new GroupBox();
            this.lblCommandCount = new Label();
            this.lblConfidenceThreshold = new Label();
            this.trackConfidence = new TrackBar();
            this.lblThresholdValue = new Label();
            this.grpCommands = new GroupBox();
            this.lblCommandsList = new Label();
            this.grpStatus.SuspendLayout();
            this.grpLastCommand.SuspendLayout();
            this.grpColorDemo.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackConfidence)).BeginInit();
            this.grpCommands.SuspendLayout();
            this.SuspendLayout();

            // btnStart
            this.btnStart.BackColor = Color.FromArgb(46, 204, 113);
            this.btnStart.FlatStyle = FlatStyle.Flat;
            this.btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStart.ForeColor = Color.White;
            this.btnStart.Location = new Point(20, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(140, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "▶ Iniciar Voz";
            this.btnStart.UseVisualStyleBackColor = false;

            // btnStop
            this.btnStop.BackColor = Color.FromArgb(231, 76, 60);
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStop.ForeColor = Color.White;
            this.btnStop.Location = new Point(170, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(140, 45);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "⏹ Detener Voz";
            this.btnStop.UseVisualStyleBackColor = false;

            // grpStatus
            this.grpStatus.Controls.Add(this.panelListening);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpStatus.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpStatus.Location = new Point(20, 80);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new Size(290, 80);
            this.grpStatus.TabIndex = 2;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Estado del Sistema";

            // panelListening
            this.panelListening.BackColor = Color.FromArgb(189, 195, 199);
            this.panelListening.Location = new Point(15, 25);
            this.panelListening.Name = "panelListening";
            this.panelListening.Size = new Size(40, 40);
            this.panelListening.TabIndex = 0;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 11F);
            this.lblStatus.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblStatus.Location = new Point(65, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(150, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sistema detenido";

            // grpLastCommand
            this.grpLastCommand.Controls.Add(this.lblUltimo);
            this.grpLastCommand.Controls.Add(this.progressConfidence);
            this.grpLastCommand.Controls.Add(this.lblConfidence);
            this.grpLastCommand.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpLastCommand.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpLastCommand.Location = new Point(20, 170);
            this.grpLastCommand.Name = "grpLastCommand";
            this.grpLastCommand.Size = new Size(290, 100);
            this.grpLastCommand.TabIndex = 3;
            this.grpLastCommand.TabStop = false;
            this.grpLastCommand.Text = "Último Comando Reconocido";

            // lblUltimo
            this.lblUltimo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblUltimo.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblUltimo.Location = new Point(10, 25);
            this.lblUltimo.Name = "lblUltimo";
            this.lblUltimo.Size = new Size(270, 25);
            this.lblUltimo.TabIndex = 0;
            this.lblUltimo.Text = "Esperando...";
            this.lblUltimo.TextAlign = ContentAlignment.MiddleCenter;

            // progressConfidence
            this.progressConfidence.Location = new Point(10, 70);
            this.progressConfidence.Name = "progressConfidence";
            this.progressConfidence.Size = new Size(270, 20);
            this.progressConfidence.TabIndex = 1;

            // lblConfidence
            this.lblConfidence.Font = new Font("Segoe UI", 9F);
            this.lblConfidence.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblConfidence.Location = new Point(10, 50);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new Size(270, 20);
            this.lblConfidence.TabIndex = 2;
            this.lblConfidence.Text = "Confianza: 0%";
            this.lblConfidence.TextAlign = ContentAlignment.MiddleCenter;

            // grpColorDemo
            this.grpColorDemo.Controls.Add(this.panelColor);
            this.grpColorDemo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpColorDemo.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpColorDemo.Location = new Point(20, 280);
            this.grpColorDemo.Name = "grpColorDemo";
            this.grpColorDemo.Size = new Size(290, 110);
            this.grpColorDemo.TabIndex = 4;
            this.grpColorDemo.TabStop = false;
            this.grpColorDemo.Text = "Demostración de Colores";

            // panelColor
            this.panelColor.BackColor = Color.White;
            this.panelColor.BorderStyle = BorderStyle.FixedSingle;
            this.panelColor.Location = new Point(10, 25);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new Size(270, 75);
            this.panelColor.TabIndex = 0;

            // grpLog
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpLog.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpLog.Location = new Point(320, 170);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new Size(560, 220);
            this.grpLog.TabIndex = 5;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Registro de Actividad";

            // txtLog
            this.txtLog.BackColor = Color.FromArgb(44, 62, 80);
            this.txtLog.Font = new Font("Consolas", 9F);
            this.txtLog.ForeColor = Color.FromArgb(236, 240, 241);
            this.txtLog.Location = new Point(10, 25);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtLog.Size = new Size(540, 185);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";

            // grpStats
            this.grpStats.Controls.Add(this.lblCommandCount);
            this.grpStats.Controls.Add(this.lblConfidenceThreshold);
            this.grpStats.Controls.Add(this.trackConfidence);
            this.grpStats.Controls.Add(this.lblThresholdValue);
            this.grpStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpStats.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpStats.Location = new Point(320, 20);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new Size(560, 140);
            this.grpStats.TabIndex = 6;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Estadísticas y Configuración";

            // lblCommandCount
            this.lblCommandCount.Font = new Font("Segoe UI", 10F);
            this.lblCommandCount.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCommandCount.Location = new Point(10, 25);
            this.lblCommandCount.Name = "lblCommandCount";
            this.lblCommandCount.Size = new Size(540, 25);
            this.lblCommandCount.TabIndex = 0;
            this.lblCommandCount.Text = "Comandos ejecutados: 0";

            // lblConfidenceThreshold
            this.lblConfidenceThreshold.AutoSize = true;
            this.lblConfidenceThreshold.Font = new Font("Segoe UI", 9F);
            this.lblConfidenceThreshold.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblConfidenceThreshold.Location = new Point(10, 60);
            this.lblConfidenceThreshold.Name = "lblConfidenceThreshold";
            this.lblConfidenceThreshold.Size = new Size(200, 15);
            this.lblConfidenceThreshold.TabIndex = 1;
            this.lblConfidenceThreshold.Text = "Umbral mínimo de confianza:";

            // trackConfidence
            this.trackConfidence.Location = new Point(10, 85);
            this.trackConfidence.Maximum = 100;
            this.trackConfidence.Minimum = 10;
            this.trackConfidence.Name = "trackConfidence";
            this.trackConfidence.Size = new Size(470, 45);
            this.trackConfidence.TabIndex = 2;
            this.trackConfidence.TickFrequency = 10;
            this.trackConfidence.Value = 70;

            // lblThresholdValue
            this.lblThresholdValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblThresholdValue.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblThresholdValue.Location = new Point(485, 85);
            this.lblThresholdValue.Name = "lblThresholdValue";
            this.lblThresholdValue.Size = new Size(65, 25);
            this.lblThresholdValue.TabIndex = 3;
            this.lblThresholdValue.Text = "70%";
            this.lblThresholdValue.TextAlign = ContentAlignment.MiddleCenter;

            // grpCommands
            this.grpCommands.Controls.Add(this.lblCommandsList);
            this.grpCommands.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpCommands.ForeColor = Color.FromArgb(52, 73, 94);
            this.grpCommands.Location = new Point(20, 400);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.Size = new Size(860, 180);
            this.grpCommands.TabIndex = 7;
            this.grpCommands.TabStop = false;
            this.grpCommands.Text = "Comandos Disponibles";

            // lblCommandsList
            this.lblCommandsList.Font = new Font("Segoe UI", 9F);
            this.lblCommandsList.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblCommandsList.Location = new Point(10, 25);
            this.lblCommandsList.Name = "lblCommandsList";
            this.lblCommandsList.Size = new Size(840, 145);
            this.lblCommandsList.TabIndex = 0;
            this.lblCommandsList.Text = "• \"hola\" - Muestra un saludo\r\n• \"limpiar\" - Limpia el registro\r\n• \"salir\" - Cierra la aplicación\r\n• \"color rojo/verde/azul/amarillo/morado/naranja\" - Cambia el color del panel\r\n• \"abrir bloc de notas\" - Abre el Bloc de notas\r\n• \"abrir navegador\" - Abre Wikipedia\r\n• \"abrir calculadora\" - Abre la Calculadora\r\n• \"minimizar\" - Minimiza la ventana\r\n• \"maximizar\" - Maximiza la ventana";

            // Form1
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.grpCommands);
            this.Controls.Add(this.grpStats);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpColorDemo);
            this.Controls.Add(this.grpLastCommand);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "VoiceRecon Pro - Reconocimiento de Voz Avanzado";
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpLastCommand.ResumeLayout(false);
            this.grpColorDemo.ResumeLayout(false);
            this.grpLog.ResumeLayout(false);
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackConfidence)).EndInit();
            this.grpCommands.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Form1_Shown(object sender, EventArgs e) {
            try {
                reconocedor = new SpeechRecognitionEngine(new CultureInfo("es-ES"));
                
                reconocedor.BabbleTimeout = TimeSpan.FromSeconds(0);
                reconocedor.InitialSilenceTimeout = TimeSpan.FromSeconds(2); 
                reconocedor.EndSilenceTimeout = TimeSpan.FromSeconds(0.5); 
                reconocedor.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(1.0);
                
                Choices listaComandos = new Choices();
                listaComandos.Add(new string[] { 
                    "hola", "limpiar", "salir", 
                    "color rojo", "color verde", "color azul", 
                    "color amarillo", "color morado", "color naranja",
                    "abrir bloc de notas", "abrir navegador", "abrir calculadora",
                    "minimizar", "maximizar"
                });
                
                GrammarBuilder constructor = new GrammarBuilder(listaComandos);
                constructor.Culture = new CultureInfo("es-ES");
                Grammar gramatica = new Grammar(constructor);
                
                reconocedor.LoadGrammar(gramatica);
                reconocedor.SpeechRecognized += ComandoReconocido;
                reconocedor.SpeechRecognitionRejected += ComandoRechazado;
                reconocedor.SetInputToDefaultAudioDevice();
                
                EscribirLog("✓ Sistema de reconocimiento de voz inicializado correctamente.", Color.FromArgb(46, 204, 113));
                EscribirLog($"✓ Idioma: Español (es-ES)", Color.FromArgb(52, 152, 219));
                EscribirLog($"✓ Umbral de confianza: {(nivelConfianza * 100):F0}%", Color.FromArgb(52, 152, 219));
                EscribirLog($"✓ Filtros anti-ruido activados", Color.FromArgb(52, 152, 219));
            } 
            catch (Exception ex) { 
                EscribirLog("✗ Error al inicializar: " + ex.Message, Color.FromArgb(231, 76, 60));
                MessageBox.Show("Error al inicializar el reconocimiento de voz:\n\n" + ex.Message + 
                    "\n\nAsegúrate de tener instalado el paquete de idioma español (es-ES) en Windows.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BotonIniciar_Click(object sender, EventArgs e) {
            if (reconocedor == null) {
                EscribirLog("✗ Error: El motor de reconocimiento no está inicializado.", Color.FromArgb(231, 76, 60));
                return;
            }
            
            reconocedor.RecognizeAsync(RecognizeMode.Multiple);
            EscribirLog("▶ Reconocimiento de voz iniciado", Color.FromArgb(46, 204, 113));
            
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "Escuchando...";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
            
            temporizador.Start();
        }

        private void BotonDetener_Click(object sender, EventArgs e) {
            if (reconocedor == null) {
                EscribirLog("✗ Error: El motor de reconocimiento no está inicializado.", Color.FromArgb(231, 76, 60));
                return;
            }
            
            reconocedor.RecognizeAsyncStop();
            EscribirLog("⏹ Reconocimiento de voz detenido", Color.FromArgb(230, 126, 34));
            
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Sistema detenido";
            lblStatus.ForeColor = Color.FromArgb(127, 140, 141);
            
            temporizador.Stop();
            panelListening.BackColor = Color.FromArgb(189, 195, 199);
        }

        private void AnimarCirculo(object sender, EventArgs e) {
            paso = (paso + 1) % 3;
            
            if (paso == 0) {
                panelListening.BackColor = Color.FromArgb(46, 204, 113);
            } else if (paso == 1) {
                panelListening.BackColor = Color.FromArgb(39, 174, 96);
            } else {
                panelListening.BackColor = Color.FromArgb(26, 188, 156);
            }
        }

        private void CambioConfianza(object sender, EventArgs e) {
            nivelConfianza = trackConfidence.Value / 100.0;
            lblThresholdValue.Text = $"{trackConfidence.Value}%";
            EscribirLog($"⚙ Umbral de confianza ajustado a: {trackConfidence.Value}%", Color.FromArgb(155, 89, 182));
        }

        private void ComandoReconocido(object sender, SpeechRecognizedEventArgs e) {
            string comando = e.Result.Text;
            double confianza = e.Result.Confidence;
            
            lblUltimo.Text = comando;
            lblConfidence.Text = $"Confianza: {confianza:P0}";
            
            int porcentaje = (int)(confianza * 100);
            if (porcentaje > 100) porcentaje = 100;
            progressConfidence.Value = porcentaje;
            
            if (confianza >= 0.7) {
                progressConfidence.ForeColor = Color.FromArgb(46, 204, 113);
            } else if (confianza >= 0.4) {
                progressConfidence.ForeColor = Color.FromArgb(241, 196, 15);
            } else {
                progressConfidence.ForeColor = Color.FromArgb(231, 76, 60);
            }

            Color colorLog;
            if (confianza >= nivelConfianza) {
                colorLog = Color.FromArgb(52, 152, 219);
            } else {
                colorLog = Color.FromArgb(149, 165, 166);
            }
            EscribirLog($"🎤 Detectado: \"{comando}\" (Confianza: {confianza:P0})", colorLog);

            if (confianza < nivelConfianza) {
                EscribirLog($"⚠ Comando ignorado (confianza menor al {(nivelConfianza * 100):F0}%)", Color.FromArgb(243, 156, 18));
                return;
            }

            contadorComandos++;
            lblCommandCount.Text = $"Comandos ejecutados: {contadorComandos}";

            EjecutarComando(comando);
        }

        private void ComandoRechazado(object sender, SpeechRecognitionRejectedEventArgs e) {
            if (e.Result != null && e.Result.Confidence > 0.2) {
                EscribirLog($"✗ Audio rechazado (Confianza: {e.Result.Confidence:P0})", Color.FromArgb(189, 195, 199));
            }
        }

        private void EjecutarComando(string comando) {
            try {
                switch (comando) {
                    case "hola":
                        MessageBox.Show("¡Hola! \n\n¿Cómo estás?", "Saludo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EscribirLog("✓ Comando ejecutado: Saludo mostrado", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "limpiar":
                        txtLog.Clear();
                        EscribirLog("✓ Registro limpiado", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "salir":
                        EscribirLog("✓ Cerrando aplicación...", Color.FromArgb(231, 76, 60));
                        Application.Exit();
                        break;
                    
                    case "color rojo":
                        panelColor.BackColor = Color.FromArgb(231, 76, 60);
                        EscribirLog("✓ Color cambiado a: Rojo", Color.FromArgb(231, 76, 60));
                        break;
                    
                    case "color verde":
                        panelColor.BackColor = Color.FromArgb(46, 204, 113);
                        EscribirLog("✓ Color cambiado a: Verde", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "color azul":
                        panelColor.BackColor = Color.FromArgb(52, 152, 219);
                        EscribirLog("✓ Color cambiado a: Azul", Color.FromArgb(52, 152, 219));
                        break;
                    
                    case "color amarillo":
                        panelColor.BackColor = Color.FromArgb(241, 196, 15);
                        EscribirLog("✓ Color cambiado a: Amarillo", Color.FromArgb(241, 196, 15));
                        break;
                    
                    case "color morado":
                        panelColor.BackColor = Color.FromArgb(155, 89, 182);
                        EscribirLog("✓ Color cambiado a: Morado", Color.FromArgb(155, 89, 182));
                        break;
                    
                    case "color naranja":
                        panelColor.BackColor = Color.FromArgb(230, 126, 34);
                        EscribirLog("✓ Color cambiado a: Naranja", Color.FromArgb(230, 126, 34));
                        break;
                    
                    case "abrir bloc de notas":
                        Process.Start("notepad.exe");
                        EscribirLog("✓ Bloc de notas abierto", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "abrir navegador":
                        ProcessStartInfo psi = new ProcessStartInfo("https://es.wikipedia.org");
                        psi.UseShellExecute = true;
                        Process.Start(psi);
                        EscribirLog("✓ Navegador abierto", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "abrir calculadora":
                        Process.Start("calc.exe");
                        EscribirLog("✓ Calculadora abierta", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "minimizar":
                        this.WindowState = FormWindowState.Minimized;
                        EscribirLog("✓ Ventana minimizada", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "maximizar":
                        if (this.WindowState == FormWindowState.Maximized) {
                            this.WindowState = FormWindowState.Normal;
                        } else {
                            this.WindowState = FormWindowState.Maximized;
                        }
                        EscribirLog("✓ Estado de ventana cambiado", Color.FromArgb(46, 204, 113));
                        break;
                }
            } 
            catch (Exception ex) {
                EscribirLog($"✗ Error al ejecutar comando: {ex.Message}", Color.FromArgb(231, 76, 60));
            }
        }

        private void EscribirLog(string mensaje, Color color) {
            if (txtLog.InvokeRequired) {
                txtLog.Invoke(new Action(() => EscribirLog(mensaje, color)));
                return;
            }
            
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;
            
            string horaActual = DateTime.Now.ToString("HH:mm:ss");
            txtLog.AppendText($"[{horaActual}] {mensaje}{Environment.NewLine}");
            
            txtLog.SelectionColor = txtLog.ForeColor;
            txtLog.ScrollToCaret();
        }
    }
}