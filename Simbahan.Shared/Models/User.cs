using System;
using System.Text;

namespace Simbahan.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime DateRegistered { get; set; }
        public string FullName => FirstName + " " + LastName;

        public static User Parse(string userData)
        {
            var data = userData.Split(',');

            var user = new User
            {
                Id = Convert.ToInt32(data[0]),
                FirstName = data[1],
                LastName = data[2],
                Email = data[3],
                Gender = data[4],
                DateOfBirth = data[5]
            };

            return user;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Email))
                return "";

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Id)
                .Append(",")
                .Append(FirstName)
                .Append(",")
                .Append(LastName)
                .Append(",")
                .Append(Email)
                .Append(",")
                .Append(Gender)
                .Append(",")
                .Append(DateOfBirth);

            return stringBuilder.ToString();
        }
    }
}