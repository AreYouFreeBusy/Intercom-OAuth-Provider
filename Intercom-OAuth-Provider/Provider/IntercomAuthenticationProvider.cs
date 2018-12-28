//  Copyright 2018 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Threading.Tasks;

namespace Owin.Security.Providers.Intercom
{
    /// <summary>
    /// Default <see cref="IIntercomAuthenticationProvider"/> implementation.
    /// </summary>
    public class IntercomAuthenticationProvider : IIntercomAuthenticationProvider
    {
        /// <summary>
        /// Initializes a <see cref="IntercomAuthenticationProvider"/>
        /// </summary>
        public IntercomAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
            OnApplyRedirect = context => context.Response.Redirect(context.RedirectUri);
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// </summary>
        public Func<IntercomAuthenticatedContext, Task> OnAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
        /// </summary>
        public Func<IntercomReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the delegate that is invoked when the ApplyRedirect method is invoked.
        /// </summary>
        public Action<IntercomApplyRedirectContext> OnApplyRedirect { get; set; }

        /// <summary>
        /// Invoked whenever Intercom successfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task Authenticated(IntercomAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task ReturnEndpoint(IntercomReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the Intercom 2.0 middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge </param>
        public virtual void ApplyRedirect(IntercomApplyRedirectContext context) 
        {
            OnApplyRedirect(context);
        }
    }
}