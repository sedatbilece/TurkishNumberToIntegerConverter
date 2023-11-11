using System.Text.RegularExpressions;

namespace TurkishNumberToIntegerConverter.Services
{
    public class AppConverterService :IAppConverterService
    {

        public string SeparateTheWords(string text)
        {

            var numberWordsPattern = @"(bir|iki|üç|dört|beş|altı|yedi|sekiz|dokuz|on|yirmi|otuz|kırk|elli|altmış|yetmiş|seksen|doksan|yüz|bin|milyon)+";
            // Metindeki tüm eşleşmeleri bul ve sayısal değerlerle değiştir.

            var value = Regex.Replace(text, numberWordsPattern, match =>
            {
                var singleNumberPattern = @"bir|iki|üç|dört|beş|altı|yedi|sekiz|dokuz|on|yirmi|otuz|kırk|elli|altmış|yetmiş|seksen|doksan|yüz|bin|milyon";
                var temp = Regex.Replace(match.Value, singleNumberPattern, "$& ");
                return temp;
            });

            return value.ToString();
        }


        public string ParseTheSentence(string output)
        {
            var wordsToNumbers = new Dictionary<string, int>
                {
            {"bir", 1}, {"iki", 2}, {"üç", 3}, {"dört", 4}, {"beş", 5},
            {"altı", 6}, {"yedi", 7}, {"sekiz", 8}, {"dokuz", 9},
            {"on", 10}, {"yirmi", 20}, {"otuz", 30}, {"kırk", 40},
            {"elli", 50}, {"altmış", 60}, {"yetmiş", 70}, {"seksen", 80},
            {"doksan", 90}, {"yüz", 100}, {"bin", 1000}, {"milyon", 1000000}
                };

            var KeyList = output.Split(' ').Where(x => x != "").ToList();

            var i = 0;
            var sentence = "";
            while (i != KeyList.Count)
            {

                if (wordsToNumbers.ContainsKey(KeyList[i]))
                {
                    sentence += " " + KeyList[i];
                    KeyList[i] = "";
                }
                else
                {
                    if (sentence != "")
                    {
                        var number = ConvertTurkishNumberToInteger(sentence.Trim());
                        sentence = "";
                        KeyList[i - 1] = number.ToString();
                    }
                }
                i++;
            }

            KeyList = KeyList.Where(x => x != "").ToList();

            foreach (var key in KeyList)
            {
                sentence += " " + key;
            }
            var result = sentence.Trim();
            return char.ToUpper(result[0]) + result.Substring(1);

        }

        public int ConvertTurkishNumberToInteger(string text)
        {
            var wordsToNumbers = new Dictionary<string, int>
                {
            {"bir", 1}, {"iki", 2}, {"üç", 3}, {"dört", 4}, {"beş", 5},
            {"altı", 6}, {"yedi", 7}, {"sekiz", 8}, {"dokuz", 9},
            {"on", 10}, {"yirmi", 20}, {"otuz", 30}, {"kırk", 40},
            {"elli", 50}, {"altmış", 60}, {"yetmiş", 70}, {"seksen", 80},
            {"doksan", 90}, {"yüz", 100}, {"bin", 1000}, {"milyon", 1000000}
                };

            int result = 0;
            int current = 0;

            foreach (var word in text.Split(' '))
            {
                if (wordsToNumbers.TryGetValue(word, out int value))
                {
                    if (value == 100 || value == 1000 || value == 1000000)
                    {


                        var temp = 0;
                        if (current != 0)
                            temp = current;
                        else
                            temp = 1;

                        current = temp * value;// öncden sayılan değerler çarpılıyor 368 * 1000

                        if (value == 1000 || value == 1000000)//  section sıfırlamak için
                        {
                            result += current;
                            current = 0;
                        }
                    }
                    else
                    {
                        current += value;
                    }
                }
            }

            return result + current;
        }



    }

}
