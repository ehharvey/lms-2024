/*
This module is responsible for the credits screen.
It was created to fulfill #16
*/

class Credits {
    private String EhharveyCredits() {
        return "Emil Harvey";
    }
    private String BoaCredits() {
        return "Boa Im";
    }
    private String NimeshCredits()
    {
        return "Nimeshkumar Chaudhari";
    }
    public String[] GetCredits() {
        return new string[] {
            EhharveyCredits(),
<<<<<<< Updated upstream
            BoaCredits()
=======
            GhostCredits(),
            NimeshCredits(),
>>>>>>> Stashed changes
        };
    }

    public void DisplayCredits() {
        Console.WriteLine("Credits");
        Console.WriteLine("-------");
        foreach (string credit in GetCredits()) {
            Console.WriteLine(credit);
        }
    }
}