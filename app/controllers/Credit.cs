/*
This module is responsible for the credits screen.
It was created to fulfill #16
*/

using Lms;

namespace Lms.Controllers
{
    [Cli.Controller]
    class Credit
    {
        private LmsDbContext? db;

        public Credit()
        {
            this.db = null;
        }

        public Credit(LmsDbContext db)
        {
            this.db = db;
        }

        private String EhharveyCredits()
        {
            return "Emil Harvey";
        }
        private String GhostCredits()
        {
            return "Parth Gajjar";
        }
        private String BoaCredits()
        {
            return "Boa Im";
        }
        private String NimeshCredits()
        {
            return "Nimeshkumar Chaudhari";
        }
        private String DaphneCredits()
        {
            return "Daphne Duong";
        }
        private String SyedCredits()
        {
            return "Shaik Mathar Syed";
        }

        private String BharatCredits()
        {
            return "Bharat Chauhan";
        }

        private String PrabhdeepSinghCredits()
        {
            return "Prabhdeep Singh";
        }
        private String TaoBoyceCredits()
        {
            return "Tao Boyce";
        }
        private String ZumhliansangLungLerCredits()
        {
            return "Zumhliansang Lung Ler";
        }
        private String DaeseongCredits()
        {
            return "Daeseong Yu";
        }
        private String TianYangCredits()
        {
            return "Tian Yang";
        }

        [Cli.Verb]
        public List<object> List()
        {
            return new List<object> {
                new { Name = EhharveyCredits() },
                new { Name = GhostCredits() },
                new { Name = BoaCredits() },
                new { Name = NimeshCredits() },
                new { Name = DaphneCredits() },
                new { Name = SyedCredits() },
                new { Name = BharatCredits() },
                new { Name = PrabhdeepSinghCredits() },
                new { Name = TaoBoyceCredits() },
                new { Name = ZumhliansangLungLerCredits() },
                new { Name = DaeseongCredits() },
                new { Name = TianYangCredits() }
            };
        }

        public void DisplayCredits()
        {
            Console.WriteLine("Credits");
            Console.WriteLine("-------");
            foreach (string credit in List())
            {
                Console.WriteLine(credit);
            }
        }
    }
}