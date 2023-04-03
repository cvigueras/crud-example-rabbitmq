using System.Text.RegularExpressions;
using Crud.Example.Main.Auth.Models;
using FluentValidation;

namespace Crud.Example.Main.ValidatorModels
{
    public class UserValidator : AbstractValidator<RegisterModel>
    {
        public UserValidator()
        {
            //Creating rules
            RuleFor(user => user.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("You need write a username.")
                .Length(2, 50)
                .WithMessage("{PropertyName} has {TotalLength} letters. Must have a long between {MinLength} and {MaxLength} letters.");

            RuleFor(user => user.Email)
                    .Must(IsValidEmail)
                    .WithMessage("Not correct mail.");
        }

        /// <summary>
        /// Check if the email has a correct format.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsValidEmail(string? email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}