﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Intercom_OAuth_Demo.Startup))]
namespace Intercom_OAuth_Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
