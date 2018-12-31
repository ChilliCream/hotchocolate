using System;
using System.Collections.Generic;
using System.Linq;

#if !ASPNETCLASSIC
using System.Collections.ObjectModel;
#endif

#if ASPNETCLASSIC
namespace HotChocolate.AspNetClassic.Authorization
#else
namespace HotChocolate.AspNetCore.Authorization
#endif
{
    public class AuthorizeDirective
    {
#if ASPNETCLASSIC
        public AuthorizeDirective(IEnumerable<string> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            
            Roles = roles.ToArray();
        }
#else
        public AuthorizeDirective()
            : this(null, null) { }

        public AuthorizeDirective(string policy)
            : this(policy, null)
        { }

        public AuthorizeDirective(IEnumerable<string> roles)
            : this(null, roles)
        { }

        public AuthorizeDirective(string policy, IEnumerable<string> roles)
        {
            ReadOnlyCollection<string> readOnlyRoles =
                roles?.ToList().AsReadOnly();

//mlm - I'm not sure if the other changes I've made in AuthorizeDirectiveType
// are applicable only to AspNet Core or not. So, I'm keeping this in but gating
// it for AspNetClassic until someone can confirm it's okay to remove this for both
// AspNetClassic and AspNetCore.
#if ASPNETCLASSIC
            if (string.IsNullOrEmpty(policy)
                && (readOnlyRoles == null || readOnlyRoles.Any()))
            {
                throw new ArgumentException(
                    "Either policy or roles has to be set.");
            }
#endif

            Policy = policy;
            Roles = readOnlyRoles;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the resource.
        /// </summary>
        public string Policy { get; }
#endif

        /// <summary>
        /// Gets or sets of roles that are allowed to access the resource.
        /// </summary>
        public IReadOnlyCollection<string> Roles { get; }
    }
}
