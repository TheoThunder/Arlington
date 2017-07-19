using System.Linq;
using System;
using Data.Repositories.Abstract;
using Data.Domain;
using Ninject;
using System.Text;

namespace Web.Infrastructure.Authentication
{
    public class TrinityMembershipProvider : System.Web.Security.MembershipProvider
    {
        #region Unimplemented/Unneccesary Methods
        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new System.NotImplementedException(); }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new System.NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new System.NotImplementedException(); }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        [Inject]
        public IUserRepository userRepository
        {
            get;
            set;
        }
        

        public TrinityMembershipProvider()
            : this(null)
        {

        }

        public TrinityMembershipProvider(IUserRepository repository)
            : base()
        {

        }

        public User user
        {
            get;
            private set;
        }

        public User CreateUser(string fullName, string passWord, string email)
        {
            return (null);
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(password.Trim())) return false;
            string hash = User.EncryptPassword(password);
            User _user = null;
            try
            {
                _user = userRepository.GetUserByUsername(username);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            if (_user == null) return false;

            if (_user.Password == hash)
            {
                this.user = _user;
                return true;
            }

            return false;
        }

        public User AssignUser(string username)
        {
            User newUser = null;

            newUser = userRepository.GetUserByUsername(username);
            
            return newUser;
        }
    }
}