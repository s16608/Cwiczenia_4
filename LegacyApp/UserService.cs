using System;
using System.Text.RegularExpressions;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private ICreditLimit _creditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
        }


        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidName(firstName) || !IsValidName(lastName))
            {
                return false;
            }

            if (!IsValidEmail(email))
            {
                return false;
            }

            if (!IsValidAge(dateOfBirth, 21))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            SetCreditLimit(user);


            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }


            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsValidAge(DateTime dateOfBirth, int minimumAge)
        {
            var age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.Month < dateOfBirth.Month ||
                (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age >= minimumAge;
        }

        private void SetCreditLimit(User user)
        {
            if (user.Client.GetType().Equals("VeryImportantClient"))
            {
                user.HasCreditLimit = false;
            }
            else
            {
                {
                    int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    if (user.Client.GetType().Equals("ImportantClient"))
                    {
                        creditLimit *= 2;
                    }

                    user.CreditLimit = creditLimit;
                    user.HasCreditLimit = true;
                }
            }
        }
    }
}