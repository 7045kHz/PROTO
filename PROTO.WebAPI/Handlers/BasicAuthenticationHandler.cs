using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using PROTO.Core.Models;
using PROTO.UseCase.Interfaces;
using System.Security.Claims;

namespace PROTO.WebAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUnitOfWorkAuth _unitOfWorkAuth;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory logger, IUnitOfWorkAuth unitOfWorkAuth, UrlEncoder encoder, ISystemClock clock) : base(option,logger,encoder,clock) 
        {
            _unitOfWorkAuth = unitOfWorkAuth;

        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No Basic Authentication Header");
            var headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (headervalue.Parameter == null )
                return AuthenticateResult.Fail("Malformed Authentication Header");

            var bytes = Convert.FromBase64String(headervalue.Parameter);

            string credentials = Encoding.UTF8.GetString(bytes);
            if(!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");
                string username=array[0];
                string password=array[1];
                if (username == null )
                    return AuthenticateResult.Fail("Malformed Authentication Header");
                if (password == null)
                    return AuthenticateResult.Fail("Malformed Authentication Header");

                var user=await this._unitOfWorkAuth.BasicAuthorization.GetBasicAuthAsync(username,password);
                if(user==null)
                    return AuthenticateResult.Fail("Authorization Failed");
                var claim = new[] { 
                    new Claim(ClaimTypes.Name, user.UserName) ,
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var identity=new ClaimsIdentity(claim,Scheme.Name);
                var principle=new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principle, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            } else
            {
                return AuthenticateResult.Fail("UnAuthorized");
            }
            
        }
    }
}
