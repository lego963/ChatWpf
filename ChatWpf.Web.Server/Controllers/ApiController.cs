using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChatWpf.Core.ApiModels;
using ChatWpf.Core.ApiModels.Contacts;
using ChatWpf.Core.ApiModels.LoginRegister;
using ChatWpf.Core.ApiModels.UserProfile;
using ChatWpf.Core.Routes;
using ChatWpf.Web.Server.Authentication;
using ChatWpf.Web.Server.Data;
using ChatWpf.Web.Server.Email;
using ChatWpf.Web.Server.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatWpf.Web.Server.Controllers
{
    [AuthorizeToken]
    public class ApiController : Controller
    {
        protected ApplicationDbContext mContext;

        protected UserManager<ApplicationUser> mUserManager;

        protected SignInManager<ApplicationUser> mSignInManager;

        public ApiController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        [AllowAnonymous]
        [Route(ApiRoutes.Register)]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Please provide all required details to register for an account";

            // The error response for a failed login
            var errorResponse = new ApiResponse<RegisterResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we have no credentials...
            if (registerCredentials == null)
                // Return failed response
                return errorResponse;

            // Make sure we have a user name
            if (string.IsNullOrWhiteSpace(registerCredentials.Username))
                // Return error message to user
                return errorResponse;

            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email
            };

            // Try and create a user
            var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

                // Send email verification
                await SendUserEmailVerificationAsync(user);

                // Return valid response containing all users details
                return new ApiResponse<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName,
                        Email = userIdentity.Email,
                        Username = userIdentity.UserName,
                        Token = userIdentity.GenerateJwtToken()
                    }
                };
            }
            // Otherwise if it failed...

            return new ApiResponse<RegisterResultApiModel>
            {
                // Aggregate all errors into a single error string
                ErrorMessage = result.Errors.AggregateErrors()
            };
        }

        [AllowAnonymous]
        [Route(ApiRoutes.Login)]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<UserProfileDetailsApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a user name
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message to user
                return errorResponse;

            // Validate if the user credentials are correct...

            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await mUserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await mUserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginCredentials.Password);

            // If the password was wrong
            if (!isValidPassword)
                // Return error message to user
                return errorResponse;

            // If we get here, we are valid and the user passed the correct login details

            // Get username
            var username = user.UserName;

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        [AllowAnonymous]
        [Route(ApiRoutes.VerifyEmail)]
        [HttpGet]
        public async Task<ActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            // Get the user
            var user = await mUserManager.FindByIdAsync(userId);

            // If the user is null
            if (user == null)
                // TODO: Nice UI
                return Content("User not found");

            // If we have the user...

            // Verify the email token
            var result = await mUserManager.ConfirmEmailAsync(user, emailToken);

            // If succeeded...
            if (result.Succeeded)
                // TODO: Nice UI
                return Content("Email Verified :)");

            // TODO: Nice UI
            return Content("Invalid Email Verification Token :(");
        }

        [Route(ApiRoutes.GetUserProfile)]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> GetUserProfileAsync()
        {
            // Get user claims
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                // Return error
                return new ApiResponse<UserProfileDetailsApiModel>
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            // Return token to user
            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details and the token
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName
                }
            };
        }

        [Route(ApiRoutes.UpdateUserProfile)]
        public async Task<ApiResponse> UpdateUserProfileAsync([FromBody]UpdateUserProfileApiModel model)
        {
            var errors = new List<string>();

            // Keep track of email change
            var emailChanged = false;

            // Get the current user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                return new ApiResponse
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            // If we have a first name...
            if (model.FirstName != null)
                // Update the profile details
                user.FirstName = model.FirstName;

            // If we have a last name...
            if (model.LastName != null)
                // Update the profile details
                user.LastName = model.LastName;

            // If we have a email...
            if (model.Email != null &&
                // And it is not the same...
                !string.Equals(model.Email.Replace(" ", ""), user.NormalizedEmail))
            {
                // Update the email
                user.Email = model.Email;

                // Un-verify the email
                user.EmailConfirmed = false;

                // Flag we have changed email
                emailChanged = true;
            }

            // If we have a username...
            if (model.Username != null)
                // Update the profile details
                user.UserName = model.Username;

            // Attempt to commit changes to data store
            var result = await mUserManager.UpdateAsync(user);

            // If successful, send out email verification
            if (result.Succeeded && emailChanged)
                // Send email verification
                await SendUserEmailVerificationAsync(user);

            // If we were successful...
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise if it failed...
            return new ApiResponse
            {
                ErrorMessage = result.Errors.AggregateErrors()
            };

        }

        [Route(ApiRoutes.UpdateUserPassword)]
        public async Task<ApiResponse> UpdateUserPasswordAsync([FromBody]UpdateUserPasswordApiModel model)
        {
            var errors = new List<string>();

            var user = await mUserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return new ApiResponse
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };


            // Attempt to commit changes to data store
            var result = await mUserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            // If we were successful...
            if (result.Succeeded)
                // Return successful response
                return new ApiResponse();
            // Otherwise if it failed...
            return new ApiResponse
            {
                ErrorMessage = result.Errors.AggregateErrors()
            };

        }

        [Route(ApiRoutes.SearchUsers)]
        public async Task<ApiResponse<SearchUsersResultsApiModel>> SearchUsersAsync([FromBody]SearchUsersApiModel model)
        {
            // Get the current user
            var user = await mUserManager.GetUserAsync(HttpContext.User);

            // If we have no user...
            if (user == null)
                return new ApiResponse<SearchUsersResultsApiModel>
                {
                    // TODO: Localization
                    ErrorMessage = "User not found"
                };

            // Check if the user provided both a first and last name
            var firstOrLastNameMissing = string.IsNullOrEmpty(model?.FirstName) || string.IsNullOrEmpty(model?.LastName);

            // Check if enough details are provided for a search
            var notEnoughSearchDetails =
                // First and last name
                firstOrLastNameMissing &&
                // Username
                string.IsNullOrEmpty(model?.Username) &&
                // Phone number
                string.IsNullOrEmpty(model?.PhoneNumber) &&
                // Email
                string.IsNullOrEmpty(model?.Email);

            // If we don't have enough details for a search...
            if (notEnoughSearchDetails)
                // Return error
                return new ApiResponse<SearchUsersResultsApiModel>
                {
                    // TODO: Localization
                    ErrorMessage = "Please provide a first and last name, or an email, username or phone number"
                };

            // Create a found user variable
            var foundUser = default(ApplicationUser);

            // If we have a username...
            if (!string.IsNullOrEmpty(model.Username))
                // Find the user by username
                foundUser = await mUserManager.FindByNameAsync(model.Username);

            // If we have an email...
            if (foundUser == null && !string.IsNullOrEmpty(model.Email))
                // Find the user by email
                foundUser = await mUserManager.FindByEmailAsync(model.Email);

            // If we have a phone number...
            if (foundUser == null && !string.IsNullOrEmpty(model.PhoneNumber))
            {
                // Find the user by phone number
                foundUser = mUserManager.Users.FirstOrDefault(u =>
                                // Phone number is confirmed
                                u.PhoneNumberConfirmed &&
                                // Phone number must match exactly 
                                // including country code if provided
                                u.PhoneNumber == model.PhoneNumber);
            }

            // If we found a user...
            if (foundUser != null)
            {
                // Return that users details
                return new ApiResponse<SearchUsersResultsApiModel>
                {
                    Response = new SearchUsersResultsApiModel
                        {
                            new SearchUsersResultApiModel
                            {
                                Username = foundUser.UserName,
                                FirstName = foundUser.FirstName,
                                LastName = foundUser.LastName
                            }
                        }
                };
            }

            // Create a new list of results
            var results = new SearchUsersResultsApiModel();

            // If we have a first and last name...
            if (!firstOrLastNameMissing)
            {
                // Search for users...
                var foundUsers = mUserManager.Users.Where(u =>
                                    // With the same first name
                                    u.FirstName == model.FirstName &&
                                    // And same last name
                                    u.LastName == model.LastName)
                                    // And for now, limit to 100 results
                                    // TODO: Add pagination
                                    .Take(100);

                // If we found any users...
                if (foundUsers.Any())
                {
                    // Add each users details
                    results.AddRange(foundUsers.Select(u => new SearchUsersResultApiModel
                    {
                        Username = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }));
                }
            }

            // Return the results
            return new ApiResponse<SearchUsersResultsApiModel>
            {
                Response = results
            };

        }


        private async Task SendUserEmailVerificationAsync(ApplicationUser user)
        {
            // Get the user details
            var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

            // Generate an email verification code
            var emailVerificationCode = await mUserManager.GenerateEmailConfirmationTokenAsync(user);

            // TODO: Replace with APIRoutes that will contain the static routes to use
            var confirmationUrl = $"http://{Request.Host.Value}/api/verify/email/?userId={HttpUtility.UrlEncode(userIdentity.Id)}&emailToken={HttpUtility.UrlEncode(emailVerificationCode)}";

            // Email the user the verification code
            await SynthesisEmailSender.SendUserVerificationEmailAsync(user.UserName, userIdentity.Email, confirmationUrl);
        }

    }
}
