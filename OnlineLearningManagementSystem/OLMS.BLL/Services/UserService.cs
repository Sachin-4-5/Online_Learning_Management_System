using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using OLMS.BLL.Helpers;
using OLMS.DAL.Interfaces;
using OLMS.DAL.Repositories;
using OLMS.Models.Entities;

namespace OLMS.BLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        // Register user (stores salt:hash in PasswordHash).
        public void Register(User user, string password)
        {
            //user.PasswordHash = password;
            //_userRepository.Add(user);

            if (string.IsNullOrWhiteSpace(user.FullName) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("All fields are required.");

            string salt = GenerateSalt();
            string hash = GenerateHashPassword(password, salt);
            string stored = salt + ":" + hash;

            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = stored,
                RoleID = user.RoleID,
                DateCreated = user.DateCreated,
                IsActive = user.IsActive,
                RoleName = user.RoleName
            };
            _userRepository.Add(newUser);
        }

        private string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        private string GenerateHashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        //OR
        //Hash password with salt
        //public static string HashPassword(string password, string salt)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var saltedPassword = password + salt;
        //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        //        return Convert.ToBase64String(bytes);
        //    }
        //}

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(int userId)
        {
            _userRepository.Delete(userId);
        }

        public User GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        /// <summary>
        /// Login Validation/Authentication
        /// </summary>
        public User ValidateLogin(string email, string password)
        {
            // Get user from DB
            var user = _userRepository.GetByEmail(email);
            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
                return null;

            // Stored format: "salt:hash"
            var parts = user.PasswordHash.Split(':');
            if (parts.Length != 2)
                return null;

            string salt = parts[0];
            string storedHash = parts[1];

            // Recompute hash with provided password + stored salt
            string computedHash = GenerateHashPassword(password, salt);

            // Compare stored hash with computed hash (constant-time comparison is better)
            if (storedHash == computedHash)
            {
                return user; // login success
            }
            return null; // login failed
        }

        /// <summary>
        /// Wrapper to keep backward compatibility
        /// </summary>
        public User Login(string email, string password)
        {
            return ValidateLogin(email, password);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}