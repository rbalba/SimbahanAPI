using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class UserService : IBasicService<User>
    {
        private readonly UserTransformer _userTransformer;

        public UserService()
        {
            _userTransformer = new UserTransformer();
        }

        public User Create(User model)
        {
            throw new NotImplementedException();
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(int id, User model)
        {
            var user = new User();

            using (var sp = new StoredProcedure("spUpdateUserInformation"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userId", id);
                sp.SqlCommand.Parameters.AddWithValue("@firstName", model.FirstName);
                sp.SqlCommand.Parameters.AddWithValue("@lastName", model.LastName);
                sp.SqlCommand.Parameters.AddWithValue("@gender", model.Gender);
                sp.SqlCommand.Parameters.AddWithValue("@birthday", model.DateOfBirth);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    user = _userTransformer.Transform(reader);
            }

            return user;
        }

        public void Delete(User model)
        {
            throw new NotImplementedException();
        }

        public List<User> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            throw new NotImplementedException();
        }
    }
}