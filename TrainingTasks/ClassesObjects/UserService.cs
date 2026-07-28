using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using UtilityLibrary;

namespace ClassesObjects
{
    public class UserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User>();
        }

        public void DisplayUser(User user)
        {
            Console.WriteLine($"ID         : {user.UserId}");
            Console.WriteLine($"Name       : {user.UserName}");
            Console.WriteLine($"Age        : {user.Age}");
            Console.WriteLine($"Email      : {user.Email}");
            Console.WriteLine($"Profession : {user.Profession}");
        }

        public void AddOrUpdateUser()
        {
            try
            {
                Console.Write("Enter User ID:\t");
                int id = ReadInt();

                User user = _users.Find(u => u.UserId == id);

                Console.Write("Enter Name:\t");
                string name = ReadUserName();

                Console.Write("Enter Age:\t");
                int age = ReadInt();

                Console.Write("Enter Email:\t");
                string email = ReadEmail();

                Console.Write("Enter Profession:\t");
                string profession = Console.ReadLine();

                Console.Write("Enter Password:\t");
                string password = Console.ReadLine();

                if (user == null)
                {
                    _users.Add(new User
                    {
                        UserId = id,
                        UserName = name,
                        Age = age,
                        Email = email,
                        Profession = profession,
                        Password = password
                    });

                    Loggings.MessageLog("User added successfully.");
                    Console.WriteLine("\nUser added successfully.");
                }
                else
                {
                    user.UserName = name;
                    user.Age = age;
                    user.Email = email;
                    user.Profession = profession;
                    user.Password = password;

                    Loggings.MessageLog("\nUser updated successfully.");
                    Console.WriteLine("\nUser updated successfully.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (Exception ex)
            {
                Loggings.MessageLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllUsers()
        {
            if (_users.Count == 0)
            {
                Console.WriteLine("\nNo users found.");
                Loggings.MessageLog("No users found.");
                return;
            }

            Console.WriteLine("\n===== USER LIST =====");

            foreach (User user in _users)
            {
                DisplayUser(user);
                Console.WriteLine(new string('-', 35));
            }
            Loggings.MessageLog("Users fetched successfully");
        }

        public void SearchUserById()
        {
            Console.Write("Enter User ID to Search: ");

            int id = ReadInt();

            User user = _users.Find(u => u.UserId == id);

            if (user == null)
            {
                Loggings.MessageLog("User not found.");
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("\n===== USER DETAILS =====");
            DisplayUser(user);
            Loggings.MessageLog("User fetched successfully");
            Console.WriteLine("User fetched successfully");
        }

        public void DeleteUser()
        {
            Console.Write("Enter User ID to Delete: ");

            int id = ReadInt();

            User user = _users.Find(u => u.UserId == id);

            if (user == null)
            {
                Loggings.MessageLog("User not found.");
                Console.WriteLine("User not found.");
                return;
            }

            _users.Remove(user);
            Loggings.MessageLog("User deleted successfully.");
            Console.WriteLine("User deleted successfully.");
        }

        public int ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid number, try again");
            return result;
        }

        public string ReadUserName()
        {
            while (true)
            {
                string? username = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(username))
                    return username;

                Console.WriteLine("Username cannot be empty.\n");
            }
        }

        public string ReadEmail()
        {
            while (true)
            {
                string? email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email cannot be empty.\n");
                    continue;
                }

                try
                {
                    MailAddress mail = new MailAddress(email);

                    if (mail.Address == email)
                        return email;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Invalid email address.\n");
            }
        }
    }
    }
