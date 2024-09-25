/*
This module is responsible for the credits screen.
It was created to fulfill #16
*/

class Credits {
    private String EhharveyCredits() {
        return "Emil Harvey";
    }
    private String GhostCredits(){
        return "Parth Gajjar";
    }
    public String[] GetCredits() {
        return new string[] {
            EhharveyCredits(),
            GhostCredits(),
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