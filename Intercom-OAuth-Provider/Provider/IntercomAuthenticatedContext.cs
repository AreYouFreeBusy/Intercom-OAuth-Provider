//  Copyright 2018 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;

namespace Owin.Security.Providers.Intercom
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class IntercomAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="IntercomAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The JSON-serialized user</param>
        /// <param name="accessToken">Intercom access token</param>
        public IntercomAuthenticatedContext(
            IOwinContext context, string accessToken, JObject userJson) 
            : base(context)
        {
            AccessToken = accessToken;

            // per https://developers.intercom.com/v2.0/reference#admins
            if (userJson != null) 
            {
                UserId = userJson["id"]?.Value<string>();            
                Email = userJson["email"]?.Value<string>();
                Name = userJson["name"]?.Value<string>();
                AppId = userJson["app"]?["id_code"]?.Value<string>();
                AppName = userJson["app"]?["name"]?.Value<string>();
            }
        }

        /// <summary>
        /// Gets the Intercom OAuth access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the scope for this Intercom OAuth access token
        /// </summary>
        public string[] Scope { get; private set; }

        /// <summary>
        /// Gets the Intercom user ID
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Gets the email address
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets the Intercom application ID
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// Gets the Internal application Name
        /// </summary>
        public string AppName { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }

        /// <summary>
        /// Useful for composing a display name our of a first and last name.
        /// </summary>
        public static Tuple<string, string> DecomposeFullName(string displayName) 
        {
            if (String.IsNullOrEmpty(displayName)) return new Tuple<string, string>(null, null);

            var segments = System.Text.RegularExpressions.Regex.Split(displayName.Trim(), @"\s+");
            if (segments.Length > 1) 
            {
                return new Tuple<string, string>(segments[0], String.Join(" ", segments.Skip(1)));
            }
            else 
            {
                return new Tuple<string, string>(String.Empty, segments[0]);
            }
        }
    }
}
