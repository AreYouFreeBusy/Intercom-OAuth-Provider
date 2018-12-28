//  Copyright 2018 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;

namespace Owin.Security.Providers.Intercom
{
    public static class IntercomAuthenticationExtensions
    {
        public static IAppBuilder UseIntercomAuthentication(this IAppBuilder app, IntercomAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");

            app.Use(typeof(IntercomAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseIntercomAuthentication(this IAppBuilder app, string clientId, string clientSecret)
        {
            return app.UseIntercomAuthentication(new IntercomAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            });
        }
    }
}