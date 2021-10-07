﻿using Microsoft.Owin;
using Owin;
using System;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(CodingExercise.Startup))]

namespace CodingExercise
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(10), //cookie expires after 10m of inactivity
                SlidingExpiration = true
            });
        }
    }
}
