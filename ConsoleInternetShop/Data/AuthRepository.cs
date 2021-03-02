using ConsoleEShop.Entities;
using ConsoleEShop.Interfaces;
using System.Linq;

namespace ConsoleEShop.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly StoreContext _context;
        public AuthRepository(StoreContext context)
        {
            _context = context;
        }
        public User Login(string username, string password)
        {
            var user = _context.Users.Find(x => x.UserName == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public User Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var maxIndex = _context.Users.Count == 0 ? 0 :_context.Users.Max(u => u.Id);

            user.Id = maxIndex + 1;

            _context.Users.Add(user);

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool UserExists(string username)
        {
            return _context.Users.Find(x => x.UserName == username) != null;
        }
    }
}
