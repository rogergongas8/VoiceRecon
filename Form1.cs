using System;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;
using System.Diagnostics;

namespace VoiceRecon {
    public partial class Form1 : Form {
        private SpeechRecognitionEngine _rec;

        public Form1() {
            InitializeComponent();
            this.Shown += Form1_Shown;
            btnStart.Click += (sender, e) => StartRecognition();
            btnStop.Click += (sender, e) => StopRecognition();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            try {
                _rec = new SpeechRecognitionEngine(new CultureInfo("es-ES"));
                Choices comandos = new Choices();
                comandos.Add(new string[] { "hola", "limpiar", "salir", "color rojo", "color verde", "color azul", "abrir bloc de notas", "abrir navegador" });
                _rec.LoadGrammar(new Grammar(new GrammarBuilder(comandos)));
                _rec.SpeechRecognized += Rec_SpeechRecognized;
                _rec.SetInputToDefaultAudioDevice();
            } catch (Exception ex) { Log("Error: " + ex.Message, true); }
        }

        private void StartRecognition() {
            _rec.RecognizeAsync(RecognizeMode.Multiple);
            Log("Iniciado");
        }

        private void StopRecognition() {
            _rec.RecognizeAsyncStop();
            Log("Detenido");
        }

        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) {
            string cmd = e.Result.Text;
            lblUltimo.Text = cmd;
            Log($"{cmd} ({e.Result.Confidence:P})");

            if (e.Result.Confidence < 0.4) return;

            switch (cmd) {
                case "hola": MessageBox.Show("¡Hola!"); break;
                case "limpiar": txtLog.Clear(); break;
                case "salir": Application.Exit(); break;
                case "color rojo": panelColor.BackColor = Color.Red; break;
                case "color verde": panelColor.BackColor = Color.Green; break;
                case "color azul": panelColor.BackColor = Color.Blue; break;
                case "abrir bloc de notas": Process.Start("notepad.exe"); break;
                case "abrir navegador": Process.Start("https://es.wikipedia.org"); break;
            }
        }

        private void Log(string msg, bool isError = false) {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }
    }
}