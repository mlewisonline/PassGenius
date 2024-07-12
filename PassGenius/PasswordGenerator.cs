using System.Text;

public static class PasswordGenerator
{
    private static readonly string lowercaseCharSet = "abcdefghijklmnopqrstuvwxyz";
    private static readonly string uppercaseCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly string numberCharSet = "0123456789";
    private static readonly string specialCharSet = "!@#$%^&*";
    private static string allCharSet = string.Empty;

    private static readonly int minLowercase = 3;
    private static readonly int minUppercase = 4;
    public static int minNumber = 1;
    public static int minSpecial = 1;

    private static bool lowercaseEnabled = true;
    private static bool uppercaseEnabled = true;
    private static bool numberEnabled = true;
    private static bool specialEnabled = true;


    private static List<char> GeneratePositions(int length)
    {
        List<char> positions = new List<char>();

        if (lowercaseEnabled && minLowercase > 0)
        {
            for (int i = 0; i < minLowercase; i++)
            {
                positions.Add('l');
            }
        }
        if (uppercaseEnabled && minUppercase > 0)
        {
            for (int i = 0; i < minUppercase; i++)
            {
                positions.Add('u');
            }
        }
        if (numberEnabled && minNumber > 0)
        {
            for (int i = 0; i < minNumber; i++)
            {
                positions.Add('n');
            }
        }
        if (specialEnabled && minSpecial > 0)
        {
            for (int i = 0; i < minSpecial; i++)
            {
                positions.Add('s');
            }
        }
        while (positions.Count < length)
        {
            positions.Add('a');
        }

        return ShufflePositions(positions);
    }

    private static List<char> ShufflePositions(List<char> listToShuffle)
    {
        return listToShuffle.OrderBy(x => Guid.NewGuid()).ToList();
    }

    private static string ReturnPasswordString(List<char> positions)
    {
        StringBuilder sb = new();
        Random random = new();

        for (int i = 0; i < positions.Count; i++)
        {
            string positionChars = string.Empty;
            switch (positions[i])
            {
                case 'l':
                    positionChars = lowercaseCharSet;
                    break;
                case 'u':
                    positionChars = uppercaseCharSet;
                    break;
                case 'n':
                    positionChars = numberCharSet;
                    break;
                case 's':
                    positionChars = specialCharSet;
                    break;
                case 'a':
                    positionChars = allCharSet;
                    break;
                default:
                    break;
            }

            int randomCharIndex = random.Next(0, positionChars.Length);
            sb.Append(positionChars[randomCharIndex]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="passwordLength"> Min 10 Max 128</param>
    /// <param name="characterSet"> 1 to 15</param>
    /// <returns></returns>
    public static string GetPassword(int passwordLength = 10, int characterSet = 1)
    {
        if (passwordLength is < 10 or > 128)
        {
            passwordLength = 10;
        }
        switch (characterSet)
        {
            case 1:
                allCharSet = $"{lowercaseCharSet}{uppercaseCharSet}{numberCharSet}{specialCharSet}";
                break;
            case 2:
                allCharSet = $"{lowercaseCharSet}{uppercaseCharSet}{numberCharSet}";
                specialEnabled   = false;
                break;
            case 3:
                allCharSet = $"{lowercaseCharSet}{uppercaseCharSet}{specialCharSet}";
                numberEnabled    = false;
                break;
            case 4:
                allCharSet = $"{lowercaseCharSet}{uppercaseCharSet}";
                numberEnabled    = false;
                specialEnabled   = false;
                break;
            case 5:
                allCharSet = $"{lowercaseCharSet}{numberCharSet}{specialCharSet}";
                uppercaseEnabled = false;
                break;
            case 6:
                allCharSet = $"{lowercaseCharSet}{numberCharSet}";
                uppercaseEnabled = false;
                specialEnabled   = false;
                break;
            case 7:
                allCharSet = $"{lowercaseCharSet}{specialCharSet}";
                uppercaseEnabled = false;
                numberEnabled    = false;
                break;
            case 8:
                allCharSet = $"{lowercaseCharSet}";
                uppercaseEnabled = false;
                numberEnabled    = false;
                specialEnabled   = false;
                break;
            case 9:
                allCharSet = $"{uppercaseCharSet}{numberCharSet}{specialCharSet}";
                lowercaseEnabled = false;
                break;
            case 10:
                allCharSet = $"{uppercaseCharSet}{numberCharSet}";
                lowercaseEnabled = false;
                specialEnabled   = false;
                break;
            case 11:
                allCharSet = $"{uppercaseCharSet}{specialCharSet}";
                lowercaseEnabled = false;
                numberEnabled    = false;
                break;
            case 12:
                allCharSet = $"{uppercaseCharSet}";
                lowercaseEnabled = false;
                numberEnabled    = false;
                specialEnabled   = false;
                break;
            case 13:
                allCharSet = $"{numberCharSet}{specialCharSet}";
                lowercaseEnabled = false;
                uppercaseEnabled = false;
                break;
            case 14:
                allCharSet = $"{numberCharSet}";
                uppercaseEnabled = false;
                lowercaseEnabled = false;
                specialEnabled   = false;
                break;
            case 15:
                allCharSet = $"{specialCharSet}";
                uppercaseEnabled = false;
                lowercaseEnabled = false;
                numberEnabled    = false;
                break;
            default:
                break;
        }
        var password = GeneratePositions(length: passwordLength);
        return ReturnPasswordString(password);
    }

}
