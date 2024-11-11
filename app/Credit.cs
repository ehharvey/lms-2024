/*
This module is responsible for the credits screen.
It was created to fulfill #16
*/

using Lms;

class Credit : ICommand {
    private LmsDbContext? db;

    public Credit() {
        this.db =  null;
    }

    public Credit(LmsDbContext db) {
        this.db = db;
    }

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
    private String SyedCredits() {
        return "Shaik Mathar Syed";
    }

    private String BharatCredits() {
        return "Bharat Chauhan";
    }
    
    private String PrabhdeepSinghCredits() {
        return "Prabhdeep Singh";
    }
    private String TaoBoyceCredits() {
        return "Tao Boyce";
    }
    private String ZumhliansangLungLerCredits()
    {
        return "Zumhliansang Lung Ler";
    }
    private String DaeseongCredits() {
        return "Daeseong Yu";
    }
    private String TianYangCredits()
    {
        return "Tian Yang";
    }
    public String[] GetCredits() {
        return new string[] {
            EhharveyCredits(),
            GhostCredits(),
            BoaCredits(),
            NimeshCredits(),
            DaphneCredits(),
            SyedCredits(),
            BharatCredits(),
            PrabhdeepSinghCredits(),
            TaoBoyceCredits(),
            ZumhliansangLungLerCredits(),
            DaeseongCredits(),
            TianYangCredits()
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

    public void Execute(Verb verb, string[] command_args) {
        Execute(verb);
    }
}
