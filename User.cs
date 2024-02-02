namespace atm_uygulamasi
{
    public class User
    {
        private string name;
        private string surname;
        private string cardNo;
        private string password;
        private double balance;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string CardNo { get => cardNo; set => cardNo = value; }
        public string Password { get => password; set => password = value; }
        public double Balance { get => balance; set => balance = value; }
    }
}