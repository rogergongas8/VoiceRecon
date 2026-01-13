using System;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Globalization;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace VoiceRecon {
    public partial class Form1 : Form {
        // Variables para el reconocimiento de voz
        private SpeechRecognitionEngine reconocedor;
        private Timer temporizador;
        private int paso = 0;
        private int contadorComandos = 0;
        private double nivelConfianza = 0.7; // empiezo con 70% para evitar errores

        public Form1() {
            InitializeComponent();
            
            // Eventos del formulario
            this.Shown += Form1_Shown;
            btnStart.Click += new EventHandler(BotonIniciar_Click);
            btnStop.Click += new EventHandler(BotonDetener_Click);
            trackConfidence.ValueChanged += CambioConfianza;
            
            // Timer para la animacion del circulo verde
            temporizador = new Timer();
            temporizador.Interval = 500;
            temporizador.Tick += AnimarCirculo;
        }

        // Cuando se carga el formulario
        private void Form1_Shown(object sender, EventArgs e) {
            try {
                // Crear el motor de reconocimiento en español
                reconocedor = new SpeechRecognitionEngine(new CultureInfo("es-ES"));
                
                // Configurar timeouts para que no detecte ruido
                reconocedor.BabbleTimeout = TimeSpan.FromSeconds(0);
                reconocedor.InitialSilenceTimeout = TimeSpan.FromSeconds(2); 
                reconocedor.EndSilenceTimeout = TimeSpan.FromSeconds(0.5); 
                reconocedor.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(1.0);
                
                // Lista de comandos que reconoce
                Choices listaComandos = new Choices();
                listaComandos.Add(new string[] { 
                    "hola", "limpiar", "salir", 
                    "color rojo", "color verde", "color azul", 
                    "color amarillo", "color morado", "color naranja",
                    "abrir bloc de notas", "abrir navegador", "abrir calculadora",
                    "minimizar", "maximizar"
                });
                
                // Crear la gramatica
                GrammarBuilder constructor = new GrammarBuilder(listaComandos);
                constructor.Culture = new CultureInfo("es-ES");
                Grammar gramatica = new Grammar(constructor);
                
                // Cargar todo en el motor
                reconocedor.LoadGrammar(gramatica);
                reconocedor.SpeechRecognized += ComandoReconocido;
                reconocedor.SpeechRecognitionRejected += ComandoRechazado;
                reconocedor.SetInputToDefaultAudioDevice();
                
                // Mensajes de inicio
                EscribirLog(" Sistema de reconocimiento de voz inicializado correctamente.", Color.FromArgb(46, 204, 113));
                EscribirLog($" Idioma: Español (es-ES)", Color.FromArgb(52, 152, 219));
                EscribirLog($" Umbral de confianza: {(nivelConfianza * 100):F0}%", Color.FromArgb(52, 152, 219));
                EscribirLog($" Filtros anti-ruido activados", Color.FromArgb(52, 152, 219));
            } 
            catch (Exception ex) { 
                EscribirLog(" Error al inicializar: " + ex.Message, Color.FromArgb(231, 76, 60));
                MessageBox.Show("Error al inicializar el reconocimiento de voz:\n\n" + ex.Message + 
                    "\n\nAsegúrate de tener instalado el paquete de idioma español (es-ES) en Windows.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Boton para iniciar el reconocimiento
        private void BotonIniciar_Click(object sender, EventArgs e) {
            if (reconocedor == null) {
                EscribirLog(" Error: El motor de reconocimiento no está inicializado.", Color.FromArgb(231, 76, 60));
                return;
            }
            
            // Empezar a escuchar
            reconocedor.RecognizeAsync(RecognizeMode.Multiple);
            EscribirLog(" Reconocimiento de voz iniciado", Color.FromArgb(46, 204, 113));
            
            // Cambiar la interfaz
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = " Escuchando...";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
            
            // Iniciar animacion
            temporizador.Start();
        }

        // Boton para detener
        private void BotonDetener_Click(object sender, EventArgs e) {
            if (reconocedor == null) {
                EscribirLog(" Error: El motor de reconocimiento no está inicializado.", Color.FromArgb(231, 76, 60));
                return;
            }
            
            // Parar de escuchar
            reconocedor.RecognizeAsyncStop();
            EscribirLog(" Reconocimiento de voz detenido", Color.FromArgb(230, 126, 34));
            
            // Volver la interfaz a como estaba
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "Sistema detenido";
            lblStatus.ForeColor = Color.FromArgb(127, 140, 141);
            
            // Parar animacion
            temporizador.Stop();
            panelListening.BackColor = Color.FromArgb(189, 195, 199);
        }

        // Animacion del circulo que pulsa
        private void AnimarCirculo(object sender, EventArgs e) {
            paso = (paso + 1) % 3;
            
            // Cambiar entre 3 tonos de verde
            if (paso == 0) {
                panelListening.BackColor = Color.FromArgb(46, 204, 113);
            } else if (paso == 1) {
                panelListening.BackColor = Color.FromArgb(39, 174, 96);
            } else {
                panelListening.BackColor = Color.FromArgb(26, 188, 156);
            }
        }

        // Cuando cambia el slider de confianza
        private void CambioConfianza(object sender, EventArgs e) {
            nivelConfianza = trackConfidence.Value / 100.0;
            lblThresholdValue.Text = $"{trackConfidence.Value}%";
            EscribirLog($" Umbral de confianza ajustado a: {trackConfidence.Value}%", Color.FromArgb(155, 89, 182));
        }

        // Cuando reconoce un comando
        private void ComandoReconocido(object sender, SpeechRecognizedEventArgs e) {
            string comando = e.Result.Text;
            double confianza = e.Result.Confidence;
            
            // Actualizar la interfaz con lo que escucho
            lblUltimo.Text = comando;
            lblConfidence.Text = $"Confianza: {confianza:P0}";
            
            // Calcular el porcentaje para la barra
            int porcentaje = (int)(confianza * 100);
            if (porcentaje > 100) porcentaje = 100; // por si acaso
            progressConfidence.Value = porcentaje;
            
            // Cambiar color segun que tan seguro esta
            if (confianza >= 0.7) {
                progressConfidence.ForeColor = Color.FromArgb(46, 204, 113); // verde
            } else if (confianza >= 0.4) {
                progressConfidence.ForeColor = Color.FromArgb(241, 196, 15); // amarillo
            } else {
                progressConfidence.ForeColor = Color.FromArgb(231, 76, 60); // rojo
            }

            // Escribir en el log
            Color colorLog;
            if (confianza >= nivelConfianza) {
                colorLog = Color.FromArgb(52, 152, 219);
            } else {
                colorLog = Color.FromArgb(149, 165, 166);
            }
            EscribirLog($" Detectado: \"{comando}\" (Confianza: {confianza:P0})", colorLog);

            // Si la confianza es muy baja, no hacer nada
            if (confianza < nivelConfianza) {
                EscribirLog($" Comando ignorado (confianza menor al {(nivelConfianza * 100):F0}%)", Color.FromArgb(243, 156, 18));
                return;
            }

            // Aumentar contador
            contadorComandos++;
            lblCommandCount.Text = $"Comandos ejecutados: {contadorComandos}";

            // Ejecutar el comando
            EjecutarComando(comando);
        }

        // Cuando rechaza audio que no entiende
        private void ComandoRechazado(object sender, SpeechRecognitionRejectedEventArgs e) {
            // Solo mostrar si tiene algo de confianza
            if (e.Result != null && e.Result.Confidence > 0.2) {
                EscribirLog($" Audio rechazado (Confianza: {e.Result.Confidence:P0})", Color.FromArgb(189, 195, 199));
            }
        }

        // Aqui ejecuto cada comando
        private void EjecutarComando(string comando) {
            try {
                // Switch para cada comando
                switch (comando) {
                    case "hola":
                        MessageBox.Show("¡Hola! \n\n¿Cómo estás?", "Saludo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EscribirLog(" Comando ejecutado: Saludo mostrado", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "limpiar":
                        txtLog.Clear();
                        EscribirLog(" Registro limpiado", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "salir":
                        EscribirLog(" Cerrando aplicación...", Color.FromArgb(231, 76, 60));
                        Application.Exit();
                        break;
                    
                    // Comandos de colores
                    case "color rojo":
                        panelColor.BackColor = Color.FromArgb(231, 76, 60);
                        EscribirLog(" Color cambiado a: Rojo", Color.FromArgb(231, 76, 60));
                        break;
                    
                    case "color verde":
                        panelColor.BackColor = Color.FromArgb(46, 204, 113);
                        EscribirLog(" Color cambiado a: Verde", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "color azul":
                        panelColor.BackColor = Color.FromArgb(52, 152, 219);
                        EscribirLog(" Color cambiado a: Azul", Color.FromArgb(52, 152, 219));
                        break;
                    
                    case "color amarillo":
                        panelColor.BackColor = Color.FromArgb(241, 196, 15);
                        EscribirLog(" Color cambiado a: Amarillo", Color.FromArgb(241, 196, 15));
                        break;
                    
                    case "color morado":
                        panelColor.BackColor = Color.FromArgb(155, 89, 182);
                        EscribirLog(" Color cambiado a: Morado", Color.FromArgb(155, 89, 182));
                        break;
                    
                    case "color naranja":
                        panelColor.BackColor = Color.FromArgb(230, 126, 34);
                        EscribirLog(" Color cambiado a: Naranja", Color.FromArgb(230, 126, 34));
                        break;
                    
                    // Comandos para abrir programas
                    case "abrir bloc de notas":
                        Process.Start("notepad.exe");
                        EscribirLog(" Bloc de notas abierto", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "abrir navegador":
                        // Tuve que usar ProcessStartInfo porque sino no funcionaba
                        ProcessStartInfo psi = new ProcessStartInfo("https://es.wikipedia.org");
                        psi.UseShellExecute = true;
                        Process.Start(psi);
                        EscribirLog(" Navegador abierto", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "abrir calculadora":
                        Process.Start("calc.exe");
                        EscribirLog(" Calculadora abierta", Color.FromArgb(46, 204, 113));
                        break;
                    
                    // Comandos de ventana
                    case "minimizar":
                        this.WindowState = FormWindowState.Minimized;
                        EscribirLog(" Ventana minimizada", Color.FromArgb(46, 204, 113));
                        break;
                    
                    case "maximizar":
                        // Alternar entre maximizado y normal
                        if (this.WindowState == FormWindowState.Maximized) {
                            this.WindowState = FormWindowState.Normal;
                        } else {
                            this.WindowState = FormWindowState.Maximized;
                        }
                        EscribirLog(" Estado de ventana cambiado", Color.FromArgb(46, 204, 113));
                        break;
                }
            } 
            catch (Exception ex) {
                EscribirLog($" Error al ejecutar comando: {ex.Message}", Color.FromArgb(231, 76, 60));
            }
        }

        // Funcion para escribir en el log con colores
        private void EscribirLog(string mensaje, Color? color = null) {
            // Por si se llama desde otro thread
            if (txtLog.InvokeRequired) {
                txtLog.Invoke(new Action(() => EscribirLog(mensaje, color)));
                return;
            }
            
            // Posicionar al final
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            
            // Poner el color
            if (color != null) {
                txtLog.SelectionColor = color.Value;
            } else {
                txtLog.SelectionColor = Color.FromArgb(236, 240, 241);
            }
            
            // Escribir el mensaje con la hora
            string horaActual = DateTime.Now.ToString("HH:mm:ss");
            txtLog.AppendText($"[{horaActual}] {mensaje}{Environment.NewLine}");
            
            // Volver al color normal
            txtLog.SelectionColor = txtLog.ForeColor;
            
            // Hacer scroll automatico
            txtLog.ScrollToCaret();
        }
    }
}