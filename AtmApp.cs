using System;
using System.Collections.Generic;

namespace atm_uygulamasi
{
    public class AtmApp
    {
        public AtmApp()
        {
            users = new List<User> {
                new User{ Name = "Dogan", Surname = "DoganSyd", CardNo = "12341", Password = "123", Balance = 700 },
                new User{ Name = "Mehmet", Surname = "MehmetSyd", CardNo = "12342", Password = "123", Balance = 600 },
                new User{ Name = "Ayşe", Surname = "AyşeSyd", CardNo = "12343", Password = "123", Balance = 500 },
                new User{ Name = "Duygu", Surname = "DuyguSyd", CardNo = "12344", Password = "123", Balance = 400 },
                new User{ Name = "Ahmet", Surname = "AhmetSyd", CardNo = "12345", Password = "123", Balance = 300 },
            };

            transactionList = new List<string>{"Para Çek", "Para Yatır","Ödeme Yap","Gün Sonu","Çıkış Yap"};

            fileLogger = new FileLogger();
        }
        

        private List<User> users;
        private List<string> transactionList;
        FileLogger fileLogger;
        User user = null;
        bool isRegisteredUser = false;


        private int GetAmount()
        {
            int amount = 0;
            bool isNumeric = int.TryParse(Console.ReadLine(), out amount) && (amount > 0);
            if (!isNumeric)
            {
                Console.WriteLine("Geçersiz miktar girdiniz.");
            }

            return amount;
        }

        public void MainMenu()
        {
            bool again = true;

            do
            {
                Console.WriteLine("\n***** Aana Menü *****");
                for (int i = 0; i < transactionList.Count; i++)
                {
                    Console.WriteLine($"{i+1}- {transactionList[i]}");
                }

                Console.Write("Lütfen yapmak istediğiniz işlemi seçin : ");

                if (int.TryParse(Console.ReadLine(), out int transaction) && ( transaction >= 0 && transaction <= transactionList.Count))
                {
                    

                    if (transaction == 5)
                    {
                        Console.WriteLine("Sistemden çıkış yapıldı.");
                        break;
                    }
                    else
                    {
                        if(isRegisteredUser == false)
                        {
                            this.user = Login();
                            if(this.user != null)
                                isRegisteredUser = true;
                        }

                        if (isRegisteredUser)
                        {
                            int amount = 0;

                            switch (transaction)
                            {
                                case 1:
                                    Console.Write("Lütfen çekmek istediğiniz miktarı girin : ");
                                    amount = GetAmount();
                                    Windraw(amount);
                                    break;
                                case 2:
                                    Console.Write("Lütfen hesabınıza yatırmak istediğiniz miktarı girin : ");
                                    amount = GetAmount();
                                    Deposit(amount);
                                    break;
                                case 3:
                                    Console.Write("Lütfen ödeme yapmak istediğiniz miktarı girin : ");
                                    amount = GetAmount();
                                    Pay(amount);
                                    break;
                                case 4:
                                    fileLogger.ReadLog();
                                    break;
                            }
                        
                        }
                        else
                        {
                            Console.WriteLine("Giriş Yapmadığınız için işlem yapamıyorsunuz.");
                            break;
                        }
                    }
                }
                else
                {
                    again = IncorrectInput();
                }
                
            }while (again);
            
        }

        private User Login()
        {
            User user = null;
            bool again = false;

            do
            {
                
                Console.Write("Kart numaranızı giriniz : ");
                string cardNo = Console.ReadLine();
                Console.Write("Parolanızı giriniz : ");
                string password = Console.ReadLine();
                
                foreach (User itemUser in users)
                {
                    if (itemUser.CardNo == cardNo && itemUser.Password == password)
                    {
                        user = itemUser;
                        break;
                    }
                }

                if(user == null)
                {
                    fileLogger.WriteLog(LogType.Error, "Hatalı giriş denemesi.");
                    again = IncorrectInput();
                } 
                else
                    again = false;

            } while (again);

            return user;
        }

        private void Windraw(int amount)
        {
            if(user.Balance >= amount)
            {
                user.Balance -= amount;
                string logMessage = $"{user.Name} {user.Surname}, hesabından {amount} miktarda para çekti.";
                fileLogger.WriteLog(LogType.Info, logMessage);
            }
            else
            {
                Console.WriteLine("Bakiye yetersiz.");
            }
        }

        private void Deposit(int amount)
        {
            user.Balance += amount;
            string logMessage = $"{user.Name} {user.Surname}, hesabına {amount} miktarda para ekledi.";
            fileLogger.WriteLog(LogType.Info, logMessage);
        }

        private void Pay(int amount)
        {
            if (user.Balance >= amount )
            {
                user.Balance -= amount;
                string logMessage = $"{user.Name} {user.Surname}, {amount} miktarda ödeme yaptı.";
                fileLogger.WriteLog(LogType.Info, logMessage);
            }
            else
            {
                Console.WriteLine("Bakiye yetersiz.");
            }
        }
    
        private bool IncorrectInput()
        {
            bool again = false;

            while (true)
            {
                Console.WriteLine("Giriş hatalı.");
                Console.WriteLine("Tekrar denemek için (1) ");
                Console.WriteLine("İptal etmek için (2) ");

                if (int.TryParse(Console.ReadLine(), out int input) && (input == 1 || input == 2))
                {
                    if (input == 1)
                    {
                        again = true;
                        break;
                    }
                    else
                    {
                        again = false;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Hatalı işlem seçimi. Tekrar deneyin ( 1 - 2 ).");
                }
            }

            return again;

        }
    }
}