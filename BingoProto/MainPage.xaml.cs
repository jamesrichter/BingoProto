using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechSynthesis;
using Windows.Media;
using Humanizer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BingoProto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///
    class BingoCage
    {
        static Random rng = new Random();
        static List<int> numberList = Enumerable.Range(1, 30).OrderBy(a => rng.Next()).ToList();
        static IOrderedEnumerable<int> numberList2 = Enumerable.Range(1, 30).OrderBy(a => rng.Next());
        static int idx = 0;


        public static string[] returnRandomBingoString()
        {
            int num = numberList.ElementAt(idx);
            string letter = "B";
            if (num > 20)
                letter = "N";
            else if (num > 10)
                letter = "I";
            else
                letter = "B";
            idx++;
            if (idx == 30) { idx = 0; }
            string[] kjk = new string[2];
            kjk[0] = letter + num.ToString();
            kjk[1] = letter + " " + num.ToWords().ToString();
            return kjk;
        }
    }


    public sealed partial class MainPage : Page
    {
        MediaElement mediaElement = new MediaElement();

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void Background_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            string[] BingoString = BingoCage.returnRandomBingoString();
            this.TextBlock1.Text = BingoString[0];
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(BingoString[1]);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
