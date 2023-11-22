using Firebase.Database;
using Firebase.Database.Query;
using MoneyTransferApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Repository
{
    public class UserRepository
    {
        private FirebaseClient firebase;

        public UserRepository(string firebaseUrl)
        {
            firebase = new FirebaseClient(firebaseUrl);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                // Fetch user from the Realtime Database based on the email
                var user = await firebase
                    .Child("Users")
                    .OrderBy("Email")
                    .EqualTo(email)
                    .OnceSingleAsync<User>();

                return user;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error fetching user: {ex.Message}");
                return null;
            }
        }

        // Add other methods for user-related operations as needed
    }
}
