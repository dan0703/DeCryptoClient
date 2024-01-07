using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeCryptoWPF;
using System.Security.Policy;
using System.Diagnostics;
using DeCryptoWPF.DeCryptoServices;
using DeCryptoWPF.Logic;

namespace Tests
{
    [TestClass]
    public class RegisterTests
    {
        private StringBuilder validationErrors;
        private Register register;

        [TestInitialize]
        public void TestInitialize()
        {
            validationErrors = new StringBuilder();
            register = new Register();
        }

        [TestMethod]
        public void ValidateName_ValidNameNoSpaces_NoErrorMessage()
        {
            string validName = "John";
            register.ValidateName(validName, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateName_NameWithNumber_ReturnsErrorMassage()
        {
            string invalideName = "Juan123";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_NameBeginningWithBlankSpace_ReturnsErrorMassage()
        {
            string invalideName = " Carlos";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_NameFinishingWithBlankSpace_ReturnsErrorMassage()
        {
            string invalideName = "James ";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_NameWithMoreThanOneBlankSpace_ReturnsErrorMassage()
        {
            string invalideName = "Juan  García";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_NameWithSpecialCharacters_ReturnsErrorMassage()
        {
            string invalideName = "Martha Mendoza.";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_NullName_ReturnsErrorMassage()
        {
            string invalideName = " ";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorNameCharacters));
        }

        [TestMethod]
        public void ValidateName_AccentedName_DoesNotAddErrorMessage()
        {
            string invalideName = "Juan Carlos Pérez Arriaga";
            register.ValidateName(invalideName, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateEmail_EmailWithMultipleDots_ReturnsErrorMessage()
        {
            string invalidEmail = "john.doe@example..com";
            register.ValidateEmail(invalidEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidatePasswords_PasswordWithAdditionalSpecialChars_ReturnsErrorMessage()
        {
            string passwordWithSpecialChars = "Pwd@With!Special*Chars";
            string confirmPassword = "Pwd@With!Special*Chars";
            register.ValidatePasswords(passwordWithSpecialChars, confirmPassword, validationErrors);
            Assert.IsFalse(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords));
        }

        [TestMethod]
        public void ValidateEmail_EmailWithoutDot_ReturnsErrorMessage()
        {
            String invalidEmail = "Marcos54@gmailcom";
            register.ValidateEmail(invalidEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidateEmail_EmailWithoutAt_ReturnsErrorMessage()
        {
            String invalidEmail = "mingi54gmail.com";
            register.ValidateEmail(invalidEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidateEmail_ValidFormat_DoesNotAddErrorMessage()
        {
            String validEmail = "Sujey542003@gmail.com";
            register.ValidateEmail(validEmail, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateEmail_ValidEmail_MinLength_NoErrors()
        {
            string validEmail = "a@b.com";
            register.ValidateEmail(validEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidateEmail_ValidEmail_MaxLength_NoErrors()
        {
            string validEmail = "maxlenemail@example.com";
            register.ValidateEmail(validEmail, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateEmail_InvalidEmail_ExceedsMaxLength_ReturnsErrorMessage()
        {
            string invalidEmail = "exceedingmaxlenemail12345456342453535363535@example.com";
            register.ValidateEmail(invalidEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidateEmail_InvalidEmail_MinLength_ReturnsErrorMessage()
        {
            string invalidEmail = "a@b.c";
            register.ValidateEmail(invalidEmail, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_EmailInvalid));
        }

        [TestMethod]
        public void ValidateBirthday_NullDate_ReturnsErrorMessage()
        {
            DateTime? nullDate = null;
            register.ValidateBirthday(nullDate, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorDateBirthday));
        }

        [TestMethod]
        public void ValidateBirthday_CurrentDate_ReturnsErrorMessage()
        {
            DateTime currentDate = DateTime.Now.Date;
            register.ValidateBirthday(currentDate, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorDateBirthday));
        }

        [TestMethod]
        public void ValidateBirthday_DateInThePast_ReturnsNoErrorMessage()
        {
            DateTime pastDate = DateTime.Now.AddDays(-1);
            register.ValidateBirthday(pastDate, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateBirthday_DateInTheFuture_ReturnsErrorMessage()
        {
            DateTime futureDate = DateTime.Now.AddDays(1);
            register.ValidateBirthday(futureDate, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorDateBirthday));
        }

        [TestMethod]
        public void ValidateBirthday_LowerLimitDate_ReturnsNoErrorMessage()
        {
            DateTime lowerLimitDate = DateTime.Now.AddYears(-1);
            register.ValidateBirthday(lowerLimitDate, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateBirthday_UpperLimitDate_ReturnsErrorMessage()
        {
            DateTime upperLimitDate = DateTime.Now.Date;
            register.ValidateBirthday(upperLimitDate, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorDateBirthday));
        }

        [TestMethod]
        public void ValidateBirthday_DateBeyondUpperLimit_ReturnsErrorMessage()
        {
            DateTime beyondUpperLimitDate = DateTime.Now.AddYears(1);
            register.ValidateBirthday(beyondUpperLimitDate, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorDateBirthday));
        }

        [TestMethod]
        public void ValidateBirthday_DateWithinDesiredRange_ReturnsNoErrorMessage()
        {
            DateTime desiredRangeDate = DateTime.Now.AddYears(-25);
            register.ValidateBirthday(desiredRangeDate, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidatePasswords_MatchingPasswords_ReturnsNoErrorMessage()
        {
            string password = "SecurePassword123*";
            string confirmPassword = "SecurePassword123*";
            register.ValidatePasswords(password, confirmPassword, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidatePasswords_NonMatchingPasswords_ReturnsErrorMessage()
        {
            string password = "SecurePassword123*";
            string confirmPassword = "DifferentPassword456/";
            register.ValidatePasswords(password, confirmPassword, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords));
        }

        [TestMethod]
        public void ValidateUser_ValidUser_MinLength_NoErrors()
        {
            string validUser = "user";
            register.ValidateUser(validUser, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateUser_ValidUser_MaxLength_NoErrors()
        {
            string validUser = "maxlenuser123456";
            register.ValidateUser(validUser, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateUser_InvalidUser_MinLength_ReturnsErrorMessage()
        {
            string invalidUser = "";  
            register.ValidateUser(invalidUser, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorUserCharacters));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithOnlySpecialChars_ReturnsErrorMessage()
        {
            string specialCharsOnlyPassword = "#$%^&*!";
            register.ValidatePasswordComplexity(specialCharsOnlyPassword, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_NeedOneLowecase));
        }

        [TestMethod]
        public void ValidateUser_UsernameWithAdditionalSpaces_ReturnsErrorMessage()
        {
            string userWithSpaces = "Mr   Raymond";
            register.ValidateUser(userWithSpaces, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorUserCharacters));
        }

        [TestMethod]
        public void ValidateUser_ValidUser_NoErrorMessage()
        {
            string validUser = "elRevo";
            register.ValidateUser(validUser, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateUser_UserWithSpaceBetweenWords_ReturnsErrorMessage()
        {
            string userWithSpace = "Mr Raymond";
            register.ValidateUser(userWithSpace, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorUserCharacters));
        }

        [TestMethod]
        public void ValidateUser_UserExceedsMaxLength_ReturnsErrorMessage()
        {
            string longUser = "ThisIsAUserWithExcessiveLength";
            register.ValidateUser(longUser, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorUserCharacters));
        }

        [TestMethod]
        public void ValidateUser_UserWithInvalidCharacters_ReturnsErrorMessage()
        {
            string invalidUser = "Invalid@User";
            register.ValidateUser(invalidUser, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_Register_ErrorUserCharacters));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_ValidPassword_NoErrorMessage()
        {
            string password = "StrongPwd123*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_TooShortPassword_ReturnsErrorMessage()
        {
            string password = "WeakPwd1";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_PasswordLong));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithoutLowCharacters_ReturnsErrorMessage()
        {
            string password = "UPPERCASE123*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_NeedOneLowecase));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithoutUpperCharacters_ReturnsErrorMessage()
        {
            string password = "lowercase123*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_NeedOneUppercase));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithoutNumbers_ReturnsErrorMessage()
        {
            string password = "NoNumbers*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_NeedOneNumber));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithoutSpecialCharacters_ReturnsErrorMessage()
        {
            string password = "NoSpecialChars123";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_NeedOneSpecialCharacter));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_PasswordWithSpaces_ReturnsErrorMessage()
        {
            string password = "Pwd With Spaces 123*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsFalse(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_ValidPassword_MinLength_NoErrorMessage()
        {
            string password = "Pwd1234*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidatePasswordComplexity_ValidPassword_MaxLength_NoErrorMessage()
        {
            string password = "MaxLenPwd12345678*";
            register.ValidatePasswordComplexity(password, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }
    }
}
