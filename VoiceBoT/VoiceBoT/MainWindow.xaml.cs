using System;
using System.Diagnostics;
using System.Windows;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;



namespace VoiceBoT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        Choices choices = new Choices();
        SpeechRecognitionEngine speechRecognition = new SpeechRecognitionEngine();
        bool awake = true;
        public MainWindow()
        {
            InitializeComponent();

            InputTxt.IsReadOnly = true;
            OutputTxt.IsReadOnly = true;

            GreetMe();

            choices.Add(new string[] { "hello", "how are you", "what time is it", "open google", "sleep", "wake", "restart" });

            Grammar grammar = new Grammar(new GrammarBuilder(choices));

            try
            {
                speechRecognition.RequestRecognizerUpdate();
                speechRecognition.LoadGrammar(grammar);
                speechRecognition.SpeechRecognized += SpeechRecognitionOnSpeechRecognized;
                speechRecognition.SetInputToDefaultAudioDevice();
                speechRecognition.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void GreetMe()
        {
            synthesizer.Speak("Hello Pavel");
        }

        private void Say(string text)
        {
            OutputTxt.AppendText(text + "\n");
            synthesizer.Speak(text);
        }

        private void SpeechRecognitionOnSpeechRecognized(object sender, SpeechRecognizedEventArgs speechRecognizedEventArgs)
        {
            string text = speechRecognizedEventArgs.Result.Text;


            if (text.Equals("wake"))
            {
                awake = true;
                Statelbl.Content = "Awake";
            }
            if (text.Equals("sleep"))
            {
                awake = false;
                Statelbl.Content = "Sleeping";
            }


            if (awake == true)
            {
                if (text.Equals("hello"))
                {
                    Say("Hey");
                }
                if (text.Equals("how are you"))
                {
                    Say("I'm good, how are you?");
                }
                if (text.Equals("what time is it"))
                {
                    Say(DateTime.Now.ToString("h:mm tt"));
                }
                if (text.Equals("open google"))
                {
                    Process.Start("https://www.youtube.com/watch?v=DraCptKRg28&index=13&list=PLuTSXWlOadXOCEfgFic0Q4CwaydRRYJrr");
                }
                if (text.Equals("restart"))
                {
                    Process.Start(@"C:\Users\irony\Source\Repos\VoiceBot\VoiceBoT\VoiceBoT\bin\Debug\VoiceBot.exe");
                    Environment.Exit(0);
                }
            }
            InputTxt.AppendText(text + "\n");
        }
    }
}
