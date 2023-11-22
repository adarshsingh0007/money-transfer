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
    public class UserRegisterViewModel : BaseViewModel
    {
        //  Entry Text Property 
        private string _name;         // Name Entry Property
        private string _email;        // Email Entry Property
        private string _password;     // Password Entry Property
        private string _mobileNumber; // Mobile Number Entry Property

        // Name Entry Property
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
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
        // Mobile Number Entry Property
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                OnPropertyChanged();
            }
        }

        // Error Label Property
        private string _nameError;          // Name Error Label Property Bind
        private string _emailError;         // Email Error Label Property Bind 
        private string _passwordError;      // Password Error Label Property Bind 
        private string _mobileNumberError;  // Mobile Number Error Label Property Bind 

        // Name Error Label Property Bind
        public string NameError
        {
            get
            {
                return _nameError;
            }
            set
            {
                _nameError = value;
                OnPropertyChanged();
            }
        }

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

        // Mobile Number Error Label Property Bind 
        public string MobileNumberError
        {
            get
            {
                return _mobileNumberError;
            }
            set
            {
                _mobileNumberError = value;
                OnPropertyChanged();
            }
        }
        // ErrorLabel Visibility Property
        private bool _isNameErrorVisible;                 // Name Error Label Visibility Property Bind 
        private bool _isEmailErrorVisible;                // Email Error Label Visibility Property Bind 
        private bool _isPasswordErrorVisible;            // Password Error Label Visibility Property Bind 
        private bool _isMobileNumberErrorVisible;        // Mobile Number Error Label Visibility Property Bind 


        // Name Error Label Visibility Property Bind 
        public bool IsNameErrorVisible
        {
            get
            {
                return _isNameErrorVisible;
            }
            set
            {
                _isNameErrorVisible = value;
                OnPropertyChanged();
            }
        }

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

        // Mobile Number  Error Label Visibility Property Bind 
        public bool IsMobileNumberErrorVisible
        {
            get
            {
                return _isMobileNumberErrorVisible;
            }
            set
            {
                _isMobileNumberErrorVisible = value;
                OnPropertyChanged();
            }
        }


        // Set Labels Property
        public string RegisterHeaderText { get; set; }     // RegisterHeaderText Label Property Bind 
        public string NameLabelText { get; set; }          // Name Label Property Bind
        public string EmailLabelText { get; set; }         // Email Label Property Bind 
        public string MobileNoLabelText { get; set; }      // Mobile Number Label Property Bind
        public string PasswordLabelText { get; set; }      // Password Label Property Bind
        public string RegisterButtonText { get; set; }     // Register Button Text Property Bind 
        public string LoginButtonText { get; set; }           // Login Button Text Property Bind

        // Placeholder Property
        public string NamePlaceholder { get; set; }         // Name Placeholder Text Property Bind 
        public string EmailPlaceholder { get; set; }        // Email Placeholder Text Property Bind 
        public string MobileNoPlaceholder { get; set; }     // Mobile Number Place Holder Text Property Bind 
        public string PasswordPlaceholder { get; set; }     // Password Place Holder Text Property Bind 





        // Set Button Command Property
        public ICommand RegisterCommand { get; set; }  // Register Button Command Bind
        public ICommand LoginCommand { get; set; }     // Login Button Command Bind 

        private readonly FirebaseAuthService authService;

        public UserRegisterViewModel()
        {


            authService = new FirebaseAuthService("AIzaSyB1_Q0pDNbqwpIlFdMNurjc8nx7oC1jGJg");


            LoginCommand = new Command(LoginUser);
            RegisterCommand = new Command(RegisterUserAsync);
            // Set Placeholder Data
            NamePlaceholder = "Enter your name";
            EmailPlaceholder = "Enter your email";
            MobileNoPlaceholder = "Enter your mobile number";
            PasswordPlaceholder = "Enter your password";

            // Set Labels Data
            RegisterHeaderText = "Register";
            NameLabelText = "NAME:";
            EmailLabelText = "EMAIL:";
            MobileNoLabelText = "MOB NO:";
            PasswordLabelText = "PASSWORD:";
            RegisterButtonText = "REGISTER";
            LoginButtonText = "Login";


        }

        private async void RegisterUserAsync()
        {
            ValidateAllFields();

            if (!IsNameErrorVisible && !IsEmailErrorVisible && !IsPasswordErrorVisible && !IsMobileNumberErrorVisible)
            {
                try
                {
                    // Register the user using the FirebaseAuthService
                    string userId = await authService.RegisterUserAsync(Email, Password);

                    if (!string.IsNullOrEmpty(userId))
                    {
                       
                        // Navigate to the RestaurantListPage after successful registration
                        await Application.Current.MainPage.Navigation.PushAsync(new PaymentDetailPage());
                    }
                    else
                    {
                        // Registration failed, handle accordingly
                        Console.WriteLine("Registration failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
        }


        private async void LoginUser(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserLoginPage());
        }

        private void ValidateAllFields()
        {
            bool isAnyFieldEmpty = false;

            if (string.IsNullOrEmpty(Name))
            {
                NameError = "Name is required";
                isAnyFieldEmpty = true;

            }
            else
            {
                NameError = string.Empty;
            }

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

            if (string.IsNullOrEmpty(MobileNumber))
            {
                MobileNumberError = "Mobile Number is required";
                isAnyFieldEmpty = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(MobileNumber, @"^\d{10}$"))
            {
                MobileNumberError = "Enter a valid 10-digit mobile number";
                isAnyFieldEmpty = true;
            }
            else
            {
                MobileNumberError = string.Empty;
            }

            // Add more validation for other fields if needed

            if (isAnyFieldEmpty)
            {

                IsNameErrorVisible = true;
                IsEmailErrorVisible = true;
                IsPasswordErrorVisible = true;
                IsMobileNumberErrorVisible = true;
            }
            else
            {
                IsNameErrorVisible = false;
                IsEmailErrorVisible = false;
                IsPasswordErrorVisible = false;
                IsMobileNumberErrorVisible = false;
            }
        }
    }
}
