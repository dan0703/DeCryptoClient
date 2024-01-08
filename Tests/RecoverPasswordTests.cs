using DeCryptoWPF;
using DeCryptoWPF.DeCryptoServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RecoverPasswordTests
    {
        private readonly int code = 12345;
        private StringBuilder validationErrors;
        private RecoverPassword recoverPassword;

        [TestInitialize]
        public void TestInitialize()
        {
            validationErrors = new StringBuilder();
            recoverPassword = new RecoverPassword();
            Account validAccount = new Account
            {
                nickname = "Sujey",
                email = "sujey542003@gmail.com",
                emailVerify = true,
                password = "af7363cebf5e844dbac559ecae74de7d13a8ade6a1d53b8843f5f4475025d6eb"
            };
            recoverPassword.ConfigurateWindow(validAccount, code);
        }

        [TestMethod]
        public void ValidateCode_CorrectCode_NoErrors()
        {
            int validCode = code;
            recoverPassword.ValidateCode(validCode.ToString(), validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateCode_IncorrectCode_ReturnsErrorMessage()
        {
            string incorrectCode = "54321";
            recoverPassword.ValidateCode(incorrectCode, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorCode_IncorrectCode));
        }

        [TestMethod]
        public void ValidateMatchingPasswords_MatchingPasswords_NoErrors()
        {
            string newPassword = "SecurePassword123*";
            string confirmPassword = "SecurePassword123*";
            recoverPassword.ValidateMatchingPasswords(newPassword, confirmPassword, validationErrors);
            Assert.IsTrue(string.IsNullOrEmpty(validationErrors.ToString()));
        }

        [TestMethod]
        public void ValidateMatchingPasswords_NonMatchingPasswords_ReturnsErrorMessage()
        {
            string newPassword = "SecurePassword123*";
            string confirmPassword = "DifferentPassword456/";
            recoverPassword.ValidateMatchingPasswords(newPassword, confirmPassword, validationErrors);
            Assert.IsTrue(validationErrors.ToString().Contains(DeCryptoWPF.Properties.Resources.Label_ErrorPassword_ErrorMatchingPasswords));
        }
    }
}
