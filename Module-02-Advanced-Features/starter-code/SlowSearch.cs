using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    public class UserRepository
    {
        public bool UserExists(List<User> users, string email)
        {
            // O(n) search - inefficient for large lists
            foreach (var user in users)
            {
                if (user.Email == email) return true;
            }
            return false;
        }
        
        public List<User> FindUsersWithDomain(List<User> users, string domain)
        {
            // O(n²) with nested loops
            var result = new List<User>();
            foreach (var user in users)
            {
                foreach (var otherUser in users)
                {
                    if (user.Email. EndsWith(domain) && otherUser.Email. EndsWith(domain))
                    {
                        if (!result.Contains(user))
                        {
                            result.Add(user);
                        }
                    }
                }
            }
            return result;
        }
        
        public User FindUserById(List<User> users, int id)
        {
            // Linear search instead of using dictionary
            foreach (var user in users)
            {
                if (user.Id == id) return user;
            }
            return null;
        }
    }
}