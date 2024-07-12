using System.Globalization;
using System.Text;

public class WordGenerator
{
    public static string GetWords(int numberOfWords = 3, char separator = ' ', bool enableUpperCase = false, bool enableTitleCase = false)
    {
        Random random = new();
        StringBuilder sb = new();
        var count = WordList.EFFLongWordList.Count;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        int luckyNumber = random.Next(numberOfWords);

        List<string> words = [];
        for (int i = 0; i < numberOfWords; i++)
        {
            var word = WordList.EFFLongWordList[random.Next(count)];
            if (enableUpperCase && !enableTitleCase)
            {
                words.Add(word.ToUpper());
            }
            else if (enableTitleCase && !enableUpperCase)
            {
                words.Add(textInfo.ToTitleCase(word.ToLower()));
            }
            else
            {
                words.Add(word);
            }
        }
        return sb.AppendJoin(separator, words).ToString();
    }
}
