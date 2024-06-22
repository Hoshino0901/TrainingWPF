using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF003
{
    public partial class MainWindow : Window
    {
        private string input = "";　// ディスプレイに出力する値を格納しておくためのフィールド
        private string operand1 = ""; // a + bなどの演算をするときのaを格納するためのフィールド
        private string operand2 = ""; // a + bなどの演算をするときのaを格納するためのフィールド
        private char operation; // 演算子を格納するためのフィールド
        private double result = 0.0; // 計算結果を格納しておくためのフィールド

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string content = button.Content.ToString();

            if (content == "AC")
            {
                input = "";
                operand1 = "";
                operand2 = "";
                result = 0.0;
                DisplayLabel.Content = "0";
            }
            else if (content == "+/-")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    // -から始まったら、
                    if (input.StartsWith("-"))
                    {
                        // 頭の-を取り除く。
                        input = input.Substring(1);
                    }
                    // -から始まらなければ、
                    else
                    {
                        // -を頭につける。
                        input = "-" + input;
                    }
                    DisplayLabel.Content = input;
                }
            }
            else if (content == "%")
            {
                if (double.TryParse(input, out double num))
                {
                    input = (num / 100).ToString();
                    DisplayLabel.Content = input;
                }
            }
            else if (content == "/" || content == "*" || content == "-" || content == "+")
            {
                // 演算子が入力されたタイミングで、aをoperand1に格納。
                operand1 = input;
                // 演算子をoperationに格納。
                operation = content[0];　// char型だから文字列の頭を格納。
                input = ""; // bを各王するためにリセットしておく。
            }
            else if (content == "=")
            {
                operand2 = input;

                // aとbどちらにも値が入っている場合のみ、
                if (!string.IsNullOrEmpty(operand1) && !string.IsNullOrEmpty(operand2))
                {
                    double num1, num2;
                    if (double.TryParse(operand1, out num1) && double.TryParse(operand2, out num2))
                    {
                        switch (operation)
                        {
                            case '+':
                                result = num1 + num2;
                                break;
                            case '-':
                                result = num1 - num2;
                                break;
                            case '*':
                                result = num1 * num2;
                                break;
                            case '/':
                                result = num1 / num2;
                                break;
                        }
                        DisplayLabel.Content = result.ToString();
                        input = result.ToString();
                    }
                }
            }
            // 演算子以外のボタン(i.e.数字ボタン)が入力されたら、DisplayLabelに表示。
            else
            {
                input += content;
                DisplayLabel.Content = input;
            }
        }
    }
}