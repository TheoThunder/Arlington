using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.ClientValidation;
using DataAnnotationsExtensions;
namespace Data.Domain
{
    public class Permission
    {
        public Permission()
        {
        }

        /// <summary>
        /// This Constructor expects a string from the Constants.Permissions class to generate a Permissions object
        /// This is to deal with IsInRole in Forms Auth. only accepting a string for role/permissions checking.
        /// </summary>
        /// <param name="PermissionString"></param>
        public Permission(string PermissionString)
        {
            string[] splitArray = PermissionString.Split('_');
            if (splitArray.Length != 2)
                throw new ArgumentException("A Permission string that does not follow correct format was passed to Permission Constructor");
            Name = splitArray[0];
            Action = splitArray[1];
        }

        [Integer]
        [Min(1, ErrorMessage = "Need only digits.")]
        public int PermissionId { get; set; } //Database Id
        public string Name { get; set; } //The Name of a permission, which is used in code to track it
        public string Action { get; set; } //What the action is: Read, Delete, etc
        public string LongName
        {
            get
            {
                return Name + "_" + Action;
            }
        }

        #region Equality and ToString Methods
        //Much of this was shamelessly adapted from http://stackoverflow.com/questions/1421289/icomparable-and-equals
        public bool Equals(Permission other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Action, this.Action) && Equals(other.Name, this.Name);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != typeof(Permission))
            {
                return false;
            }

            return Equals((Permission)other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Action != null ? Action.GetHashCode() : 0); 
            }
        }

        public static bool operator ==(Permission left, Permission right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Permission left, Permission right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Permission ID: {0}, Name: {1}, Action: {2}", this.PermissionId, this.Name, this.Action);
        }
        #endregion
    }
}
