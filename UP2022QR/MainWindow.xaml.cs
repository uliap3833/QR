using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace UP2022QR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string _NeedsStr = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            spBarCode.Children.Clear();
            Random rnd = new Random();
            DateTime startDate = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime NeedsDate = startDate.AddDays(rnd.Next(range));
            string TodayStr = NeedsDate.ToShortDateString();
            TodayStr = TodayStr.Replace(".", "");
            string DateStr = TodayStr.Substring(0, 4) + TodayStr.Substring(6, 2);        
            _NeedsStr = DateStr;
            for (int i = 0; i < 6; i++)
            {
                _NeedsStr += rnd.Next(0, 9)+"";
            }
            int NeedsNum = FirstNum(_NeedsStr);



            string BinaryCodeDef = "000000";
            TextBlock tbDefNumber = new TextBlock()
            {
                Text = NeedsNum+"",
                FontSize = 16,
                TextAlignment = TextAlignment.Center,
            };
            StackPanel spDefNum = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Height = 100
            };
            StackPanel spDefCode = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };

            for (int i = 0; i < BinaryCodeDef.Length; i++)
            {
                if (BinaryCodeDef[i] == '1')
                {
                    Rectangle r = new Rectangle()
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 80
                    };
                    spDefCode.Children.Add(r);
                }
                else
                {
                    Rectangle r = new Rectangle()
                    {
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 80
                    };
                    spDefCode.Children.Add(r);
                }
            }

            spDefNum.Children.Add(spDefCode);
            spDefNum.Children.Add(tbDefNumber);
            spBarCode.Children.Add(spDefNum);


            BinaryCodeDef = "101";
            for (int i = 0; i < BinaryCodeDef.Length; i++)
            {
                if (BinaryCodeDef[i] == '1')
                {
                    Rectangle r = new Rectangle() 
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);
                }
                else
                {
                    Rectangle r = new Rectangle()  
                    {
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);  
                }
            }

            string BinaryCode = "";
            BinaryCode = EAN13Class.GetLrft(NeedsNum);
            BinaryCode += EAN13Class.GetRight();
            string[] BinaryCodeNum = new string[12];
            for (int i = 0; i < 12; i++)
            {
                switch (BinaryCode[i])
                {
                    case 'L':
                        BinaryCodeNum[i] = EAN13Class.L(_NeedsStr[i]);
                        break;
                    case 'R':
                        BinaryCodeNum[i] = EAN13Class.R(_NeedsStr[i]);
                        break;
                    case 'G':
                        BinaryCodeNum[i] = EAN13Class.G(_NeedsStr[i]);
                        break;
                }
                
            }


            StackPanel spLeftCodeAndNumber = new StackPanel() 
            {
                Orientation = Orientation.Vertical,
                Height = 100
            };
            StackPanel spLeftCode = new StackPanel()  
            {
                Orientation = Orientation.Horizontal,
            };

            for (int i = 0; i < BinaryCodeNum.Length/2; i++)
            {
                for (int j = 0; j < BinaryCodeNum[i].Length; j++)
                {
                    if (BinaryCodeNum[i][j] == '1')
                    {
                        Rectangle r = new Rectangle()
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 2,
                            SnapsToDevicePixels = true,
                            Height = 80
                        };
                        spLeftCode.Children.Add(r);
                    }
                    else
                    {
                        Rectangle r = new Rectangle()
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                            SnapsToDevicePixels = true,
                            Height = 80
                        };
                        spLeftCode.Children.Add(r);
                    }
                }               
            }

            TextBlock tbLeftNumber = new TextBlock()
            {
                Text = _NeedsStr.Substring(0,6),
                FontSize = 16,
                TextAlignment = TextAlignment.Center,             
            };

            spLeftCodeAndNumber.Children.Add(spLeftCode); 
            spLeftCodeAndNumber.Children.Add(tbLeftNumber);
            spBarCode.Children.Add(spLeftCodeAndNumber);

            BinaryCodeDef = "01010";

            for(int i = 0; i < BinaryCodeDef.Length; i++)
            {
                if (BinaryCodeDef[i] == '1')
                {
                    Rectangle r = new Rectangle() 
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);
                }
                else
                {
                    Rectangle r = new Rectangle()  
                    {
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);  
                }
            }

            StackPanel spRightCodeAndNumber = new StackPanel() 
            {
                Orientation = Orientation.Vertical,
                Height = 100
            };

            StackPanel spRightCode = new StackPanel()
            { 
                Orientation = Orientation.Horizontal,
            };

            for (int i = 6; i < BinaryCodeNum.Length; i++)
            {
                for (int j = 0; j < BinaryCodeNum[i].Length; j++)
                {
                    if (BinaryCodeNum[i][j] == '1')
                    {
                        Rectangle r = new Rectangle()
                        {
                            Stroke = Brushes.Black,
                            StrokeThickness = 2,
                            SnapsToDevicePixels = true,
                            Height = 80
                        };
                        spRightCode.Children.Add(r);
                    }
                    else
                    {
                        Rectangle r = new Rectangle()
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                            SnapsToDevicePixels = true,
                            Height = 80
                        };
                        spRightCode.Children.Add(r);
                    }
                }
            }

            TextBlock tbRightNumber = new TextBlock()
            {
                Text = _NeedsStr.Substring(6, 6),
                FontSize = 16,
                TextAlignment = TextAlignment.Center
            };

            spRightCodeAndNumber.Children.Add(spRightCode);
            spRightCodeAndNumber.Children.Add(tbRightNumber);
            spBarCode.Children.Add(spRightCodeAndNumber);

            BinaryCodeDef = "101";

            for (int i = 0; i < BinaryCodeDef.Length; i++)
            {
                if (BinaryCodeDef[i] == '1')
                {
                    Rectangle r = new Rectangle()
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);
                }
                else
                {
                    Rectangle r = new Rectangle()
                    {
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true,
                        Height = 100
                    };
                    spBarCode.Children.Add(r);
                }
            }
        }




        public int FirstNum(string OurStr)
        {
            int FirstNum = 0, SecondNum = 0, FinalNum = 0;
            string FinalNumStr = "";
            for (int i = 0; i < 12; i+=2)
            {
                FirstNum += Convert.ToInt32(OurStr[i]+"");
            }
            FirstNum *= 3;
            for (int i = 1; i < 12; i+=2)
            {
                SecondNum += Convert.ToInt32(OurStr[i]+"");
            }
            FinalNum = FirstNum + SecondNum;
            FinalNumStr = FinalNum + "";
            FinalNumStr = FinalNumStr.Substring(FinalNumStr.Length - 1, 1);
            FinalNum = 10 - Convert.ToInt32(FinalNumStr + "");
            if (FinalNum == 10)
            {
                return 0;
            }
            return FinalNum;
        }

    }
}
