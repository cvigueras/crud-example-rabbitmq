using Crud.Example.Main.Auth.Models;
using Crud.Example.Main.ValidatorModels;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Crud.Example.Domain.Test
{
    public class PersonValidatorTest
    {
        private UserValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new UserValidator();
        }

        [Test]
        public void ShouldHaveError_When_NameIsNull()
        {
            var model = new RegisterModel { Username = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Username);
        }

        [Test]
        public void ShouldNotHaveError_When_NameIsSpecified()
        {
            var model = new RegisterModel { Username = "Jeremy" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(person => person.Username);
        }

        [Test]
        public void ShouldHaveError_When_EmailIsNull()
        {
            var model = new RegisterModel { Email = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Email);
        }

        [Test]
        public void ShouldHaveError_When_EmailIsNotValid()
        {
            var model = new RegisterModel { Email = "carlos@carlos" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Email);
        }

        [Test]
        public void ShouldNotHaveError_When_EmailIsCorrectSpecified()
        {
            var model = new RegisterModel { Email = "carlos@carlos.com" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(person => person.Email);
        }
    }
}
