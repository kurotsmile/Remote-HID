

using System.Diagnostics;
using System.Speech.Recognition;

namespace Remote_HID
{
    internal class ActionSpeech
    {
        private SpeechRecognitionEngine recognizer;
        private bool isPaused = false;
        private Form1 frm;

        public void InitializeSpeechRecognition(Form1 f, IList<string> list_cmd)
        {
            this.frm = f;
            recognizer = new SpeechRecognitionEngine();
            Choices commands = new Choices();
            commands.Add("open");
            commands.Add("close");
            commands.Add("exit");

            for (int i = 0; i < list_cmd.Count; i++)
            {
                commands.Add(list_cmd[i]);
            }

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(commands);

            Grammar grammar = new Grammar(gb);
            recognizer.LoadGrammar(grammar);

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.frm.get_Btn_speech().Text = e.Result.Text;
            if (e.Result.Text == "setting") this.frm.act_sys().OpenSettings();
            if (e.Result.Text == "exit") this.frm.Hide();
            if (e.Result.Text == "close") this.frm.Hide();
            if (e.Result.Text == "open") this.frm.Show();
        }

        public void Start()
        {
            try
            {
                if (!isPaused)
                {
                    recognizer.SetInputToDefaultAudioDevice();
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);
                }
                else
                {
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);
                    isPaused = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Pause()
        {
            try
            {
                if (!isPaused)
                {
                    recognizer.RecognizeAsyncStop();
                    isPaused = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        public void Stop()
        {
            recognizer.RecognizeAsyncStop();
            isPaused = false;
        }
    }
}