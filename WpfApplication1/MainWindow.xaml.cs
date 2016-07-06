using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Document.Blocks.Clear();
        }

        private void button_convert_Click(object sender, RoutedEventArgs e)
        {
            string s = StringFromRichTextBox(richTextBox);
            s = s.Replace("\r\n", "\n");
            richTextBox.Document.Blocks.Clear();
            string temp = "";
            int i = 0, j = 0;
            for(i = 0; i < s.Length; i++)
            {
                if (s[i] == '@' || s[i] == '#')
                {
                    temp += s[i];
                    for(j = i + 1; j < s.Length; j++)
                    {
                        //only letters, numbers, periods and underscores are allowed
                        if (s[j] >= 'a' && s[j] <= 'z' || s[j] >= 'A' && s[j] <= 'Z' || 
                            s[j] >= '0' && s[j] <= '9' || s[j] == '_' || s[j] == '.')
                        {
                            temp += s[j];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (j > i + 1)
                    {
                        //hashtags and usernames cannot begin with '.'
                        if(s[i+1]!='.')
                        {
                            AppendRtfText(temp, Brushes.Blue);
                        }
                        else
                        {
                            AppendRtfText(temp, Brushes.Black);
                        }
                        
                    }
                    else
                    {
                        AppendRtfText(s[i].ToString(), Brushes.Black);
                    }

                    temp = null;
                    i = j-1;
                }
                else
                {
                    AppendRtfText(s[i].ToString(), Brushes.Black);
                }

            }
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
              rtb.Document.ContentStart,
              rtb.Document.ContentEnd
            );
            return textRange.Text;
        }

        private void AppendRtfText(string Text, Brush Color)
        {
            TextRange range = new TextRange(richTextBox.Document.ContentEnd, 
                richTextBox.Document.ContentEnd);
            range.Text = Text;
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Color);
        }








    }


}
