using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System.Net;
using System.Web.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(AspNetIdentity.Startup))]

namespace AspNetIdentity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new MyUserManager(new MyUserStore()));
            
            var apiConfig = new HttpConfiguration();
            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
            app.UseWebApi(apiConfig);
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }
    }

    public class MyUserManager : UserManager<MyUser, int>
    {
        public MyUserManager(IUserStore<MyUser, int> store) : base(store)
        {
        }

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<MyUser, int> store, MyUser user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            return new MyUserManager(new MyUserStore());
        }
    }

    public class MyUser : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }

    public class MyUserStore : IUserStore<MyUser, int>
    {
        public Task CreateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }
    }

    public class MyRole : IRole<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class MyRoleStore : IRoleStore<MyRole, int>
    {
        public Task CreateAsync(MyRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<MyRole> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<MyRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyRole role)
        {
            throw new NotImplementedException();
        }
    }

    public class MyUserRoleStore : IUserRoleStore<MyUser, int>
    {
        public Task AddToRoleAsync(MyUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(MyUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(MyUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }
    }

    public class MyUserPasswordStore : IUserPasswordStore<MyUser, int>
    {
        public Task CreateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<MyUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }
    }

    public class MYSignInManager : SignInManager<MyUser, int>
    {
        public MYSignInManager(UserManager<MyUser, int> userManager, IAuthenticationManager authenticationManager) : 
            base(userManager, authenticationManager)
        {
        }
    }
}
