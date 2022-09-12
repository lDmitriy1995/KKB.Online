using KBB.Online.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKB.Online
{
    internal class Program
    {
        static string Path = @"C:\Users\Admin\Desktop\ClassWork.Bankomat\MyData.db";
        static void Main(string[] args)

        {

            int ch = 0;

            try {

                UserService userService = new UserService(Path);

                string message = "";
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Добро пожаловать в Интернет Банкинг");
                Console.WriteLine("");
                Console.WriteLine("Выберите пункты меню: ");
                Console.WriteLine("1. Авторизация");
                Console.WriteLine("2 Регистрация");
                Console.WriteLine("3 Выход");
                Console.WriteLine("");


                ch = Convert.ToInt32(Console.ReadLine());


                switch (ch)

                {
                    case 1:
                        {
                            Console.Clear();
                            Console.Write("Введите ИИН: ");
                            string IIN;
                            IIN = Console.ReadLine();
                            Console.WriteLine("Введите пароль: ");
                            string Password;
                            Password = Console.ReadLine();
                            personal_data user_ = userService.GetUser(IIN, Password);
                            if (user_ == null)
                            {
                                Console.WriteLine("Логин или пароль введен неверно");

                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Добро пожаловать {0} ", user_.FullName);

                                Console.WriteLine("");
                                Console.WriteLine("1. Создать счет");
                                Console.WriteLine("2. Пополнить счет");
                                Console.WriteLine("3. Перевести деньги со счета");

                                Console.WriteLine(" Выберите пункты меню");
                                 ch = Convert.ToInt32(Console.ReadLine());

                                AccountService accountService = new AccountService(Path);
                                switch (ch)
                                {
                                    case 1:
                                        {
                                            string message_=" ";
                                            string accountIBAN = " ";
                                           if( accountService.CreateAccount(user_.UserId, out message_, out accountIBAN))
                                            {
                                                Console.WriteLine("Поздравляем Ваш счет {0} {1} ", accountIBAN, message_);
                                            }
                                           else
                                            {
                                                Console.WriteLine(message);
                                            }
                                            
                                        }
                                        break;
                                        case 2:
                                        {
                                            List<Account> accounts =  accountService.GetUserAccounts(user_.UserId);
                                            if(accounts.Count > 0)
                                            {
                                                foreach (Account item in accounts)
                                                {
                                                    Console.WriteLine("{0}.{1} - {2} {3}",item.AccountId,
                                                        item.IBAN,item.Balance,item.GetCurrencyName);
                                                }
                                                Console.Write("Какой счет хотите пополнить? ");
                                                int temp = Convert.ToInt32(Console.ReadLine());
                                                Console.Write("На какую сумму поплнить счет?");
                                                double balance = Convert.ToDouble(Console.ReadLine());
                                                if(accountService.AddBalance(temp, balance))
                                                {
                                                    Console.WriteLine("Баланс пополнен");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Чтото пошло не так");
                                                }
                                                

                                            }
                                        }
                                        break;

                                    default:
                                        break;
                                }

                            }

                        }
                        break;
                    case 2:

                        {
                            //User user = new User();
                            //user.Accounts = null;
                            //user.Address = null;
                            //user.Birth = new DateTime(1995, 10, 23);
                            //user.IIN = "951023300482";
                            //user.Name = "Dmitriy";
                            //user.SecondName = "Dotsenko";
                            //user.Password = "123";
                            //user.PhoneNumber = "+7 747 681 77 23";
                            //user.Sex = "M";

                            Console.WriteLine("Введите Ваш ИИН");
                            string iin = Console.ReadLine();

                            if (userService.GetUserData(iin, out message))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(message);
                                Console.ForegroundColor = ConsoleColor.White;





                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(message);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                        }
                        break;


                    default:
                        throw new Exception("Необходимо выбрать пунк меню");
                        break;

                }

            }
            catch (Exception) when (ch == 0)
            {
                Console.WriteLine(" у не должен быть равен 0");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);






            }

        }
    } }
