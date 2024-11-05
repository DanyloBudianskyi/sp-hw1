using System;
using System.DirectoryServices.AccountManagement;

namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //•	Напишите приложение, которое будет:
            //o Создавать нового пользователя с указанными именем, паролем и правами доступа.
            //o Удалять существующего пользователя, если он больше не нужен.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Create a new user");
                Console.WriteLine("2 - Delete an existing user");
                Console.WriteLine("3 - Exit");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Input name: ");
                        string name = Console.ReadLine();
                        Console.Write("Input password: ");
                        string password = Console.ReadLine();
                        Console.Write("Input display name: ");
                        string displayName = Console.ReadLine();
                        Console.WriteLine("Input group name:");
                        string groupName = Console.ReadLine();
                        try
                        {
                            CreateUser(name, password, displayName, groupName);
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Input name: ");
                        string deleteName = Console.ReadLine();
                        try
                        {
                            DeleteUser(deleteName);
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Wrong choice. Try again");
                        Console.ReadKey();
                        break;


                }
            }
        }
        public static void CreateUser(string name, string password, string displayName, string groupName)
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
            {
                if (UserPrincipal.FindByIdentity(context, name) == null)
                {
                    UserPrincipal user = new UserPrincipal(context)
                    {
                        SamAccountName = name,
                        DisplayName = displayName,
                        Enabled = true
                    };
                    user.SetPassword(password);
                    user.Save();
                    AddUserToGroup(name, groupName);
                    Console.WriteLine($"The user: {name} was created");
                }

            }
        }
        public static void DeleteUser(string name)
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, name);
                if (user != null)
                {
                    user.Delete();
                    Console.WriteLine($"User {name} was deleted");
                }
                else
                {
                    Console.WriteLine("User not found");
                }

            }
        }
        private static void AddUserToGroup(string username, string groupName)
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, username);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);

                if (user != null && group != null)
                {
                    group.Members.Add(user);
                    group.Save();
                    Console.WriteLine($"User was {username} added to group: {groupName}.");
                }
                else
                {
                    Console.WriteLine($"User or group not found!");
                }
            }
        }
    }
}
