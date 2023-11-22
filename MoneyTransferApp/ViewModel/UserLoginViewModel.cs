using MoneyTransferApp.Services;
using MoneyTransferApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoneyTransferApp.ViewModel
{
    public class UserLoginViewModel : BaseViewModel
    {
        private readonly FirebaseAuthService authService;

        //  Entry Text Property 
        private string _email;        // Email Entry Property
        private string _password;     // Password Entry Property

        // Email Entry Property

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        // Password Entry Property
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        // Error Label Property

        private string _emailError;         // Email Error Label Property Bind 
        private string _passwordError;      // Password Error Label Property Bind

        // Password Error Label Property Bind 
        public string PasswordError
        {
            get
            {
                return _passwordError;
            }
            set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }

        // Email Error Label Property Bind 
        public string EmailError
        {
            get
            {
                return _emailError;
            }
            set
            {
                _emailError = value;
                OnPropertyChanged();
            }
        }

        // ErrorLabel Visibility Property

        private bool _isEmailErrorVisible;                // Email Error Label Visibility Property Bind 
        private bool _isPasswordErrorVisible;            // Password Error Label Visibility Property Bind 

        // Email Error Label Visibility Property Bind 
        public bool IsEmailErrorVisible
        {
            get
            {
                return _isEmailErrorVisible;
            }
            set
            {
                _isEmailErrorVisible = value;
                OnPropertyChanged();
            }
        }

        // Password Error Label Visibility Property Bind 
        public bool IsPasswordErrorVisible
        {
            get
            {
                return _isPasswordErrorVisible;
            }
            set
            {
                _isPasswordErrorVisible = value;
                OnPropertyChanged();
            }
        }

        // Set Labels Property
        public string LoginHeaderText { get; set; }     // LoginHeaderText Label Property Bind  
        public string EmailLabelText { get; set; }         // Email Label Property Bind 
        public string PasswordLabelText { get; set; }      // Password Label Property Bind
        public string RegisterButtonText { get; set; }     // Register Button Text Property Bind 
        public string ForgotPasswordButtonText { get; set; }  // ForgotPassword Button Text Property Bind 
        public string LoginButtonText { get; set; }           // Login Button Text Property Bind
        public string AccountLabelText { get; set; }            // Have Not Any Account Label Property Bind 
        public string TermLabelText { get; set; }              // Term & Conditions Label Property Bind

        // Placeholder Property
        public string EmailPlaceholder { get; set; }        // Email Placeholder Text Property Bind 
        public string PasswordPlaceholder { get; set; }     // Password Place Holder Text Property Bind 

        // Set Button Command Property
        public ICommand RegisterCommand { get; set; }  // Register Button Command Bind
        public ICommand LoginCommand { get; set; }     // Login Button Command Bind 
        public ICommand ForgotPasswordCommand { get; set; } // Forgot Password Command Bind 

        public UserLoginViewModel()
        {
            authService = new FirebaseAuthService("AIzaSyB1_Q0pDNbqwpIlFdMNurjc8nx7oC1jGJg");

            RegisterCommand = new Command(RegisterUser);
            LoginCommand = new Command(LoginUser);
            

            // Set Placeholder Data

            EmailPlaceholder = "Enter your email";
            PasswordPlaceholder = "Enter your password";

            // Set Labels Data
            LoginHeaderText = "Login";
            EmailLabelText = "EMAIL:";
            PasswordLabelText = "PASSWORD:";
            RegisterButtonText = "Register";
            LoginButtonText = "Login";
            ForgotPasswordButtonText = "forgot password?";
            AccountLabelText = "Not have Account?";
            TermLabelText = "By you Agree Term & Condition";
        }



        private async void RegisterUser(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserRegisterPage());
        }



        private async void LoginUser()
        {
            ValidateAllFields();

            if (!IsEmailErrorVisible && !IsPasswordErrorVisible)
            {
                try
                {
                    // Log in the user using the FirebaseAuthService
                    string userId = await authService.LoginUserAsync(Email, Password);

                    if (!string.IsNullOrEmpty(userId))
                    {
                        // Login successful, navigate to the BakerySuggestionsPage (or your main page)
                        await Application.Current.MainPage.Navigation.PushAsync(new UserDashBoard());
                    }
                    else
                    {
                        // Login failed, handle accordingly
                        Console.WriteLine("Login failed. Please check your email and password.");

                        // Check if the failure is due to incorrect email or password
                        bool emailExists = await authService.CheckIfEmailExistsAsync(Email);
                        if (!emailExists)
                        {
                            Console.WriteLine("The entered email does not exist.");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
        }


        private void ValidateAllFields()
        {
            bool isAnyFieldEmpty = false;



            if (string.IsNullOrEmpty(Email))
            {
                EmailError = "Email is required";
                isAnyFieldEmpty = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                EmailError = "Enter a valid email address";
                isAnyFieldEmpty = true;
            }
            else
            {
                EmailError = string.Empty;
            }

            if (string.IsNullOrEmpty(Password))
            {
                PasswordError = "Password is required";
                isAnyFieldEmpty = true;
            }
            else if (Password.Length < 8)
            {
                PasswordError = "Password must be at least 8 characters";
                isAnyFieldEmpty = true;
            }
            else
            {
                PasswordError = string.Empty;
            }

            // Add more validation for other fields if needed

            if (isAnyFieldEmpty)
            {
                IsEmailErrorVisible = true;
                IsPasswordErrorVisible = true;

            }
            else
            {
                IsEmailErrorVisible = false;
                IsPasswordErrorVisible = false;

            }
        }
    }
}
