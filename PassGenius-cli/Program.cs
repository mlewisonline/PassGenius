namespace PassGenius_cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if  (args.Length > 1 && args[0] == "p")
            {
                switch (args.Length)
                {
                    case 3:
                        if (int.TryParse(args[1], out var length) && int.TryParse(args[2], out var set)){
                            Console.WriteLine(PasswordGenerator.GetPassword(length, set));
                        }
                        else
                        {
                            DisplayHelp();
                        }
                        break;
                    default:
                        DisplayHelp();
                        break;
                        
                }
               
            }
            else if (args.Length > 1 && args[0] == "w")
            {
                switch (args.Length)
                {
                    case 4:
                        if (int.TryParse(args[1], out var numOfWords) && char.TryParse(args[2], out var separatorCase4))
                        {
                            if (args[3].Equals("t", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.WriteLine(WordGenerator.GetWords(numOfWords, separatorCase4, enableTitleCase:true));
                            }
                            else if (args[3].Equals("u", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.WriteLine(WordGenerator.GetWords(numOfWords, separatorCase4, enableUpperCase: true));
                            }
                            else
                            {
                                DisplayHelp();
                            }
                        }
                        break;
                    case 3:
                        if (int.TryParse(args[1], out var case3Words))
                        {
                            if (args[2].Equals("t", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.WriteLine(WordGenerator.GetWords(case3Words, enableTitleCase: true));
                            }
                            else if (args[2].Equals("u", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.WriteLine(WordGenerator.GetWords(case3Words, enableUpperCase: true));
                            }
                            else if (char.TryParse(args[2], out var separatorCase3))
                            {
                                Console.WriteLine(WordGenerator.GetWords(case3Words, separator: separatorCase3));
                            }
                            else
                            {
                                DisplayHelp();
                            }
                        }
                        break;
                    case 2:
                        if (int.TryParse(args[1], out var case2Words))
                        {
                            Console.WriteLine(WordGenerator.GetWords(case2Words));
                        }
                        break;
                    default:
                        DisplayHelp();
                        break;
                }
            }
            else
            {
                DisplayHelp();
            }
        }


        static void DisplayHelp()
        {
            Console.WriteLine("\nPassGenius-cli Copyright Matthew Lewis 2024");
            Console.WriteLine("Usage Password: passgenius-cli [p] [length] [set]");
            Console.WriteLine("Usage Passphrase: PassGenius-cli [w] [NumberOfWords] [Separator] [U]uppercase || [T]titlecase\r\n");
            Console.WriteLine("""
                Available character sets:
                1:  Lower + Upper + Numbers + Special | 2:  Lower + Upper + Numbers
                3:  Lower + Upper + Special           | 4:  Lower + Upper
                5:  Lower + Numbers + Special         | 6:  Lower + Numbers
                7:  Lower + Special                   | 8:  Lower
                9:  Upper + Numbers + Special         | 10: Upper + Numbers
                11: Upper + Special                   | 12: Upper
                13: Numbers + Special                 | 14: Numbers
                15: Special

                """);
            Console.WriteLine("Password: passgenius-cli p 14 1 (creates a password of 14 character made of Lower, Upper, Numbers and Special characters)");
            Console.WriteLine("Passphrase: passgenius-cli w 4 U (creates GALLANTLY ANTHOLOGY LAZILY ENSLAVE )\r\n");

        }
    }
}
