using EmploymentApp.Core.Entities;
using EmploymentApp.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmploymentApp.Core.DataFilter
{
    public static class UserDataFilter
    {
        public static IEnumerable<User> FilterUsers(IEnumerable<User> users, UserQueryFilter userQueryFilter)
        {
            if (!string.IsNullOrEmpty(userQueryFilter.Name) && users != null)
                users = FilterByName(users, userQueryFilter.Name);

            if (!string.IsNullOrEmpty(userQueryFilter.Surnames) && users != null)
                users = FilterBySurnames(users, userQueryFilter.Surnames);

            if (userQueryFilter.Bithdate != null && users != null)
                users = FilterByDate(users, userQueryFilter.Bithdate);
            return users;
        }
        private static IEnumerable<User> FilterByName(IEnumerable<User> users, string name) =>
            users.Where(x => x.Name.ToLower().Contains(name.ToLower()));

        private static IEnumerable<User> FilterBySurnames(IEnumerable<User> users, string surnames) =>
           users.Where(x => x.Surnames.ToLower().Trim().Contains(surnames.ToLower().Trim()));

        private static IEnumerable<User> FilterByDate(IEnumerable<User> urers, DateTime? date) =>
            urers.Where(x => x.Bithdate <= date);
    }
}
