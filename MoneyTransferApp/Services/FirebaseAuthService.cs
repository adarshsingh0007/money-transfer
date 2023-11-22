using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Services
{
    public class FirebaseAuthService
    {
        private readonly FirebaseAuthProvider authProvider;

        public FirebaseAuthService(string apiKey)
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                return auth.User.LocalId;
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Firebase registration failed: {ex.Message}");
            }
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                return auth.User.LocalId;
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Firebase login failed: {ex.Message}");
            }
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            try
            {
                // Use Firebase authentication SDK to check if the email exists
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyB1_Q0pDNbqwpIlFdMNurjc8nx7oC1jGJg"));
                var user = await authProvider.GetUserAsync(email);

                // If user is null, the email does not exist
                return user != null;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or return false as needed
                Console.WriteLine($"Error checking email existence: {ex.Message}");
                return false;
            }
        }

    }
}
