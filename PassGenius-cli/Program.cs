namespace PassGenius_cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // If 0 arguments passed display help
            if (args.Length == 0)
            {
                DisplayHelp();
            }
            // If arguments are correct length and start with P Try and generate a password
            if (args.Length == 3 && args[0] == "p") {

                if (int.TryParse(args[1], out var length) && int.TryParse(args[2], out var set))
                {
                    // User gave us vaild length and set we can proceed to generate password.
                    Console.WriteLine(PasswordGenerator.GetPassword(length, set));
                }
                else
                {
                    DisplayHelp();
                }  
            }
            else
            {
                DisplayHelp();
            }
            
        }


        static void DisplayHelp()
        {
            Console.WriteLine("\nUsage Password: passgenius [p] [length] [set]\r\n");
            Console.WriteLine("""
                Available character sets:
                1  = Lower + Upper + Numbers + Special
                2  = Lower + Upper + Numbers
                3  = Lower + Upper + Special
                4  = Lower + Upper
                5  = Lower + Numbers + Special
                6  = Lower + Numbers
                7  = Lower + Special
                8  = Lower
                9  = Upper + Numbers + Special
                10 = Upper + Numbers
                11 = Upper + Special
                12 = Upper
                13 = Numbers + Special
                14 = Numbers
                15 = Special
                """);
            Console.WriteLine("Example: passgenius p 14 1 (creates a password of 14 character made of Lower, Upper, Numbers and Special characters)");

        }
    }
}
