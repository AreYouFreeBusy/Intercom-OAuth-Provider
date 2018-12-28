//  Copyright 2018 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Threading.Tasks;

namespace Owin.Security.Providers.Intercom
{
    /// <summary>
    /// Specifies callback methods which the <see cref="IntercomAuthenticationMiddleware"></see> invokes to enable developer control over the authentication process. />
    /// </summary>
    public interface IIntercomAuthenticationProvider
    {
        /// <summary>
        /// Invoked whenever Intercom successfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        Task Authenticated(IntercomAuthenticatedContext context);

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        Task ReturnEndpoint(IntercomReturnEndpointContext context);

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the Intercom middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge </param>
        void ApplyRedirect(IntercomApplyRedirectContext context);
    }
}