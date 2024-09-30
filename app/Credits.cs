/*
This module is responsible for the credits screen.
It was created to fulfill #16
*/

class Credits : ICommand {
    public string GetHelp() {
        return $"""
        Credits

        Description:
        The credits for the program.

        Verbs:
        - list: {GetHelp(Verb.List)}
        """;
    }

    public string GetHelp(Verb verb) {
        switch (verb) {
            case Verb.List:
                return "Lists the credits for the program.";
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }

    private String EhharveyCredits() {
        return "Emil Harvey";
    }
    private String GhostCredits(){
        return "Parth Gajjar";
    }
    private String BoaCredits() {
        return "Boa Im";
    }
    private String NimeshCredits()
    {
        return "Nimeshkumar Chaudhari";
    }
    private String DaphneCredits() {
        return "Daphne Duong";
    }
    public String[] GetCredits() {
        return new string[] {
            EhharveyCredits(),
            GhostCredits(),
            BoaCredits(),
            NimeshCredits(),
            DaphneCredits()
        };
    }

    public void DisplayCredits() {
        Console.WriteLine("Credits");
        Console.WriteLine("-------");
        foreach (string credit in GetCredits()) {
            Console.WriteLine(credit);
        }
    }

    public void Execute(Verb verb) {
        switch (verb) {
            case Verb.List:
                DisplayCredits();
                break;
            default:
                throw new ArgumentException("Invalid verb.");
        }
    }
}