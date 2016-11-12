using System;
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

        public MainWindow()
        {
            Thread t1 = new Thread(new ThreadStart(TalkWithMe));

            InitializeComponent();
            GreetMe();

            t1.Start();
        }

        public void GreetMe()
        {
            synthesizer.Speak("Hello, from now on you can talk with me");
        }

        public void TalkWithMe()
        {
            choices.Add(new string[] {"hello", "how are you"});

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

        public void Say(string text)
        {
            synthesizer.Speak(text);
        }

        private void SpeechRecognitionOnSpeechRecognized(object sender, SpeechRecognizedEventArgs speechRecognizedEventArgs)
         {
            string text = speechRecognizedEventArgs.Result.Text;

            if(text.Equals("hello"))
            {
                Say("Hey");
            }
            if (text.Equals("how are you"))
            {
                Say("I'm good, how are you?");
            }
        }
    }
}
