using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ChatWpf.Web.Server.Identity
{
    public static class IdentityErrorExtensions
    {
        public static string AggregateErrors(this IEnumerable<IdentityError> errors)
        {
            return errors?.ToList()
                .Select(f => f.Description)
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }
    }
}
