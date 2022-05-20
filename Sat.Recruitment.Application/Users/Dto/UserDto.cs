using AutoMapper;
using Sat.Recruitment.Core.Domain;
using System;
using System.Text;

namespace Sat.Recruitment.Application.Users
{
    /// <summary>
    /// Use class to send info to user
    /// </summary>    
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; } = 0;

        private readonly StringBuilder errors = new StringBuilder("");

        public UserDto(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            SetMoney(money);
        }


        //Validate errors
        public string ValidateErrors()
        {
            // -- Validate if Name is null
            if (Name == null)
                errors.AppendLine("The name is required");
            // -- Validate if Email is null
            if (Email == null)
                errors.AppendLine(" The email is required");
            // -- Validate if Address is null
            if (Address == null)
                errors.AppendLine(" The address is required");
            // -- Validate if Phone is null
            if (Phone == null)
                errors.AppendLine(" The phone is required");

            return errors.ToString();
        }

        // -- Get User to Domain fill
        public User GetUser(IMapper mapper)
        {
            // Normalized Email
            NormalizedEmail();

            // -- AutoMapper
            var user = mapper.Map<User>(this);

            return user;
        }

        /// <summary>
        /// Normalized Email
        /// </summary>
        private void NormalizedEmail()
        {
            // -- Normalize email
            var aux = Email.Trim().Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);            
            
            // -- Replace 
            aux[0] = aux[0].Replace(".", "").Replace("+","");
            
            // -- Concat
            Email = $"{aux[0]}@{aux[1]}";
        }

        /// <summary>
        /// Set Money
        /// </summary>
        /// <param name="money"></param>
        private void SetMoney(string money)
        {
            if (!decimal.TryParse(money, out decimal moneyParse)) return;

            decimal gif = 1;
            decimal percentage;
            switch (UserType)
            {
                case "Normal":

                    //If new user is normal and has more than USD100
                    percentage = (moneyParse > 100) ? Convert.ToDecimal(0.12) : (moneyParse < 100 && moneyParse > 10) ? Convert.ToDecimal(0.8) : 1;
                    gif = moneyParse * percentage;

                    break;

                case "SuperUser":

                    percentage = (moneyParse > 100) ? Convert.ToDecimal(0.20) : 1;
                    gif = moneyParse * percentage;

                    break;
                case "Premium":
                    gif = (moneyParse > 100) ? moneyParse * 2 : moneyParse;
                    break;

            }

            Money += gif;
        }
    }
}
