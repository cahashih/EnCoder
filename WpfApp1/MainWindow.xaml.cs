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
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace WpfApp1
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
        enum EncryptionType { Encode, Decode }
        const string alf = "аАбБвВгГдДеЕёЁжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩъЪыЫьЬэЭюЮяЯ";
        const string alfen = "qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLzZxXcCvVbBnNmM";

        
        char[] charactersRu = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И',
                                                        'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                        'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ы', 'Ь',
                                                        'Э', 'Ю', 'Я', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };

        string[] codeMorse = new string[] { "*–", "–***", "*––", "––*",
                                                        "–**", "*", "***–", "––**",
                                                        "**", "*–––", "–*–", "*–**",
                                                        "––", "–*", "–––", "*––*",
                                                        "*–*", "***", "–", "**–",
                                                        "**–*", "****", "–*–*",
                                                        "–––*", "––––", "−−*−",
                                                        "−*−−", "−**−", "**−**",
                                                        "**−−", "*−*−", "*−−−−",
                                                        "**−−−", "***−−", "****−",
                                                        "*****", "−****", "−−***",
                                                        "−−−**", "−−−−*", "−−−−−" };


        char[] charactersEn = new char[] { 'A', 'B', 'W', 'G', 'D', 'E', 'V', 'Z', 'I',
                                                        'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S',
                                                        'T', 'U', 'F', 'H', 'C', ' ', ' ', 'Q', 'Y', 'X',
                                                        ' ', ' ', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                        '8', '9', '0' };






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int method = comboBox1.SelectedIndex;
            try
            {
                switch (method)
                {
                    case 0:
                        MethodRelsShifr();
                        break;
                    case 1:
                        MainVigener(EncryptionType.Encode);
                        break;
                    case 2:
                        MainVigenerEn(EncryptionType.Encode);
                        break;
                    case 3:
                        textResult.Text = Shifrovka(textOne.Text, textLong.Text);
                        break;
                    case 4:
                        MethodMorzeRuEncode();
                        break;
                    case 5:
                        MethodMorzeEnEncode();
                        break;

                }
            }
            catch {
                MessageBox.Show("Ошибка, возможно некоректные данные");
            }


        }

        private static Random random = new Random();
        public static string RandomStringEn(int length)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomStringRu(int length)
        {
            const string chars = "аАбБвВгГдДеЕёЁжЖзЗиИйЙкКлЛмМнНоОпПрРсСтТуУфФхХцЦчЧшШщЩъЪыЫьЬэЭюЮяЯ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            int method = comboBox1.SelectedIndex;
            try {
                switch (method)
                {
                    case 0:
                        MethodRelsDeShifr();
                        break;
                    case 1:
                        MainVigener(EncryptionType.Decode);
                        break;
                    case 2:
                        MainVigenerEn(EncryptionType.Decode);
                        break;
                    case 3:
                        textResult.Text = DeShifrovka(textOne.Text, textLong.Text);
                        break;
                    case 4:
                        MethodMorzeRuDecode();
                        break;
                    case 5:
                        MethodMorzeEnDecode();
                        break;
                } 
            }
            catch
            {
                MessageBox.Show("Ошибка, возможно неккоректные данные");
            }
            
            //String s = ComboBox.Text;
            
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {

            int method = comboBox1.SelectedIndex;
            var rand = new Random();
            int i = rand.Next(3, 16);
            switch (method)
            {
                case 0:
                    if (textOne.Text.Length > 4)
                    {
                        textLong.Text = rand.Next(2, textOne.Text.Length / 2).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Введите текст который нужно зашифровать (Не менее 5 символов)");
                    }
                    break;
                case 1:
                    
                    textLong.Text = RandomStringRu(i);
                    break;
                case 2:
                    textLong.Text = RandomStringEn(i);

                   
                    break;
                case 3:
                    textLong.Text = RandomStringEn(i);
                    break;

            }



        }


        public static string Shifrovka(string ishText, string pass,
               string sol = "doberman", string cryptographicAlgorithm = "SHA1",
               int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
               int keySize = 256)
        {
            if (string.IsNullOrEmpty(ishText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        //метод дешифрования строки
        public static string DeShifrovka(string ciphText, string pass,
               string sol = "doberman", string cryptographicAlgorithm = "SHA1",
               int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
               int keySize = 256)
        {
            if (string.IsNullOrEmpty(ciphText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] cipherTextBytes = Convert.FromBase64String(ciphText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);

            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
            {
                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }
        
        private void MethodMorzeRuEncode()
        {
            string input = textOne.Text;
            input = input.ToUpper();
            string output = "";
            int index;
            foreach (char c in input)
            {
                if (c != ' ')
                {
                    index = Array.IndexOf(charactersRu, c);
                    output += codeMorse[index] + " ";
                }
            }
            output = output.Remove(output.Length - 1);
            textResult.Text = output;
        }

        private void MethodMorzeEnEncode()
        {
            string input = textOne.Text;
            input = input.ToUpper();
            string output = "";
            int index;
            foreach (char c in input)
            {
                if (c != ' ')
                {
                    index = Array.IndexOf(charactersEn, c);
                    output += codeMorse[index] + " ";
                }
            }
            output = output.Remove(output.Length - 1);
            textResult.Text = output;
        }

        private void MethodMorzeRuDecode()
        {
            string input = textOne.Text;
            string[] split = input.Split(' ');
            string output = "";
            int index;
            foreach (string s in split)
            {
                index = Array.IndexOf(codeMorse, s);
                output += charactersRu[index] + " ";
            }
            output = output.Remove(output.Length - 1);
            textResult.Text = output;
        }
        private void MethodMorzeEnDecode()
        {
            string input = textOne.Text;
            string[] split = input.Split(' ');
            string output = "";
            int index;
            foreach (string s in split)
            {
                index = Array.IndexOf(codeMorse, s);
                output += charactersEn[index] + " ";
            }
            output = output.Remove(output.Length - 1);
            textResult.Text = output;
        }




        private void MethodRelsShifr()
        {
            string stringMain = textOne.Text;
            string streCount = textLong.Text;
            int strCount = 1;
            try { 
                strCount = Convert.ToInt32(streCount);
                if (stringMain != "")
                {
                    var strWithoutKey = Regex.Replace(stringMain, "&", "");
                    var strWithoutSpaces = Regex.Replace(strWithoutKey, " ", "%");
                    int columns = (strWithoutSpaces.Length);

                    stringMain = strWithoutSpaces.ToString();

                    string[,] nums3 = new string[strCount, columns];
                    int PoryadokStr = 0, strNumber = 0, direction = 1;
                    for (int j = 0; j < columns; j++)
                    {
                        nums3[strNumber, j] = stringMain[PoryadokStr].ToString();
                        strNumber += direction;
                        if (strNumber == strCount - 1 || strNumber == 0)
                        {
                            direction *= -1;
                        }
                        PoryadokStr++;
                    }
                    string result = "";
                    for (int i = 0; i < strCount; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            result = string.Concat(result, nums3[i, j]);
                        }
                        result = result + "&";
                    }
                    textResult.Text = result.Remove(result.Length - 1, 1); ;
                }
                else
                {
                    MessageBox.Show("Введите текст");
                }
            }
            catch
            {
                MessageBox.Show("Некоректные данные");
                MessageBox.Show("Ключ должен быть больше 2 и меньше количества символов в тексте");
            }
        }
        private void MethodRelsDeShifr()
        {
            string stringMain = textOne.Text;
            string streCount = textLong.Text;
            int strCount = 1;
            try { 

                if (stringMain != "")
                {
                    strCount = Convert.ToInt32(streCount);
                    string[] words = stringMain.Split(new char[] { '&' });
                    int strNumber = 0, direction = 1;
                    String processWord = words[strNumber];
                    string result = "";
                    result += words[0][0];
                    words[strNumber] = processWord.Remove(0, 1);


                    for (int j = 0; j < stringMain.Length - strCount; j++)
                    {

                        strNumber += direction;
                        processWord = words[strNumber];
                        try
                        {
                            processWord.Remove(0, 1);
                        }
                        catch { }
                        words[strNumber] = processWord.Remove(0, 1);
                        result += (processWord[0]);
                        if (strNumber == strCount - 1 || strNumber == 0)
                        {
                            direction *= -1;
                        }
                    }
                    textResult.Text = Regex.Replace(result, "%", " ");
                }
                else
                {
                    MessageBox.Show("Введите текст");
                }
            }
            catch
            {
                MessageBox.Show("Некоректные данные");
                MessageBox.Show("Ключ должен быть больше 2 и меньше количества символов в тексте");
            }
            
        }
        
        
        private void MainVigener(EncryptionType encryptionType)
        {
            // Проверка ключа на не пустую строку и на принадлежность всех символов алфавиту.
            static bool CheckKeyString(string key)
            => !string.IsNullOrWhiteSpace(key) && key.All(symbol => alf.Contains(symbol));

            
            string textClasic = textOne.Text;
            string text = textClasic.ToLower();
            string keyClasic = textLong.Text;
            string key = keyClasic.ToLower();
            

            textResult.Text = CheckKeyString(key) ? VigenerEncryption(text, key, encryptionType) : "Введен некорректный ключ"; 
        }


        private void MainVigenerEn(EncryptionType encryptionType)
        {
            // Проверка ключа на не пустую строку и на принадлежность всех символов алфавиту.
            static bool CheckKeyString(string key)
            => !string.IsNullOrWhiteSpace(key) && key.All(symbol => alfen.Contains(symbol));


            string textClasic = textOne.Text;
            string text = textClasic.ToLower();
            string keyClasic = textLong.Text;
            string key = keyClasic.ToLower();


            textResult.Text = CheckKeyString(key) ? VigenerEncryptionEn(text, key, encryptionType) : "Введен некорректный ключ";
        }
        private string VigenerEncryptionEn(string text, string key, EncryptionType encryptionType)
        {
            // индекс текущего символа ключа
            int keyIndex = -1;
            // получить следующий символ ключа
            char NextKeySymbol()
            => key[++keyIndex < key.Length ? keyIndex : keyIndex = 0];
            // зашифровать символ: (t + k) % N
            char EncryptSymbol(char symbol)
            => alfen[(alfen.IndexOf(symbol) + alfen.IndexOf(NextKeySymbol())) % alfen.Length];
            // расшифровать символ: (t + N - k) % N
            char DecryptSymbol(char symbol)
            => alfen[(alfen.IndexOf(symbol) + alfen.Length - alfen.IndexOf(NextKeySymbol())) % alfen.Length];

            // пересобираем строку, выполняя операцию шифрования/дешифрования для символов входящих в алфавит (alf)
            return encryptionType switch
            {
                EncryptionType.Encode => new string(text.Select(s => alfen.Contains(s) ? EncryptSymbol(s) : s).ToArray()),
                EncryptionType.Decode => new string(text.Select(s => alfen.Contains(s) ? DecryptSymbol(s) : s).ToArray()),
                _ => "",
            };
        }

        // Метод шифрования (decrypt:false) / дешифрования (decrypt:true)
        private string VigenerEncryption(string text, string key, EncryptionType encryptionType)
        {
            // индекс текущего символа ключа
            int keyIndex = -1;
            // получить следующий символ ключа
            char NextKeySymbol()
            => key[++keyIndex < key.Length ? keyIndex : keyIndex = 0];
            // зашифровать символ: (t + k) % N
            char EncryptSymbol(char symbol)
            => alf[(alf.IndexOf(symbol) + alf.IndexOf(NextKeySymbol())) % alf.Length];
            // расшифровать символ: (t + N - k) % N
            char DecryptSymbol(char symbol)
            => alf[(alf.IndexOf(symbol) + alf.Length - alf.IndexOf(NextKeySymbol())) % alf.Length];

            // пересобираем строку, выполняя операцию шифрования/дешифрования для символов входящих в алфавит (alf)
            return encryptionType switch
            {
                EncryptionType.Encode => new string(text.Select(s => alf.Contains(s) ? EncryptSymbol(s) : s).ToArray()),
                EncryptionType.Decode => new string(text.Select(s => alf.Contains(s) ? DecryptSymbol(s) : s).ToArray()),
                _ => "",
            };
        }

        

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int method = comboBox2.SelectedIndex;
            switch (method)
            {
                case 0:
                    textOne.SetValue(Grid.ColumnSpanProperty, 1);
                    textLong.SetValue(Grid.ColumnSpanProperty, 1);
                    Application.Current.MainWindow.Height = 287;
                    Application.Current.MainWindow.MaxHeight = 287;
                    Application.Current.MainWindow.MaxWidth = 880;
                    textOne.Height = 30;
                    textResult.Height = 30;
                    break;

                case 1:
                    textOne.SetValue(Grid.ColumnSpanProperty, 2);
                    textLong.SetValue(Grid.ColumnSpanProperty, 2);
                    Application.Current.MainWindow.Height = 287;
                    Application.Current.MainWindow.MaxHeight = 287;
                    Application.Current.MainWindow.MaxWidth = 880;
                    textOne.Height = 30;
                    textResult.Height = 30;

                    break;

                case 2:
                    textOne.SetValue(Grid.ColumnSpanProperty, 3);
                    textLong.SetValue(Grid.ColumnSpanProperty, 2);
                    textOne.Height = 400;
                    textResult.Height = 400;
                    Application.Current.MainWindow.Height = 520;
                    Application.Current.MainWindow.MaxHeight = 4280;
                    Application.Current.MainWindow.MaxWidth = 9900;
                    MessageBox.Show("Если нужно больше - растяните окно =)");
                    break;
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int method = comboBox1.SelectedIndex;
            if (method >= 4)
            {
                Key1.Visibility = Visibility.Hidden;
                Gen1.Visibility = Visibility.Hidden;
                textLong.Visibility = Visibility.Hidden;
            }
            else
            {
                Key1.Visibility = Visibility.Visible;
                Gen1.Visibility = Visibility.Visible;
                textLong.Visibility = Visibility.Visible;
            }
            textOne.Text = "";
            textLong.Text = "";
            textResult.Text = "";
            
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int method = comboBox1.SelectedIndex;
            /*
            if (method == 0)
            {
                if (sender is TextBox textBox)
                {
                    textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch =>
                            ch == '+' || ch == '-'
                            || (ch >= '0' && ch <= '9')
                            || (ch >= 'а' && ch <= 'я')
                            || (ch >= 'А' && ch <= 'Я')
                            || ch == 'ё' || ch == 'Ё'
                         )
                             .ToArray()
                        );
                }
            }
            */
            if (sender is TextBox textBox)
            {

                switch (method)
                {
                    case 0:
                        textBox.Text = new string
                    (
                         textBox
                         .Text
                         .Where
                         (ch =>
                            ch >= '0' && ch <= '9'
                         )
                         .ToArray()
                    );
                        break;
                    case 1:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch =>
                             (ch >= 'а' && ch <= 'я')
                            || (ch >= 'А' && ch <= 'Я')
                            || ch == 'ё' || ch == 'Ё'
                         )
                             .ToArray()
                        );
                        break;
                    case 2:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch =>
                             (ch >= 'a' && ch <= 'z')
                            || (ch >= 'A' && ch <= 'z'))
                             .ToArray()
                        );
                        break;
                    
                }

                
            }
        }
        private void TextBox_TextChanged1(object sender, TextChangedEventArgs e)
        {
            int method = comboBox1.SelectedIndex;
            if (sender is TextBox textBox)
            {

                switch (method)
                {
                                        case 1:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch => (ch >= '0' && ch <= '9')
                            || ch == ' ' ||
                             (ch >= 'а' && ch <= 'я')
                            || (ch >= 'А' && ch <= 'Я')
                            || ch == 'ё' || ch == 'Ё'
                         )
                             .ToArray()
                        );
                        break;
                    case 2:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch => (ch >= '0' && ch <= '9')
                            || ch == ' ' ||
                             (ch >= 'a' && ch <= 'z')
                            || (ch >= 'A' && ch <= 'z'))
                             .ToArray()
                        );
                        break;
                    case 4:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch => ch == '*' || ch == '–' || ch == ' '

                            ||
                             (ch >= '0' && ch <= '9')
                            || (ch >= 'а' && ch <= 'я')
                            || (ch >= 'А' && ch <= 'Я')
                            || ch == 'ё' || ch == 'Ё'
                         )
                             .ToArray()
                        );
                        break;
                    case 5:
                        textBox.Text = new string
                        (
                             textBox
                             .Text
                             .Where
                             (ch => ch == '*' || ch == '–' || ch == ' '
                            ||
                             (ch >= '0' && ch <= '9')
                            || (ch >= 'a' && ch <= 'z')
                            || (ch >= 'A' && ch <= 'Z')
                            )
                             .ToArray()
                        );
                        break;
                }


            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создатель приложения: Шихов Александр ИСП-320п");
        }
    }
   
}
