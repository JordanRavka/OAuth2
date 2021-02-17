using Newtonsoft.Json.Linq;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using Ravka.Helpers.Extensions;
using Ravka.Helpers.Fabric.Logging;

namespace OAuth2.Client.Impl
{
    /// <summary>
    /// Facebook authentication client.
    /// </summary>
    public class FacebookClient2 : FacebookClient
    {
        public FacebookClient2(IRequestFactory factory, IClientConfiguration configuration)
         : base(factory, configuration)
        {
        }
        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            args.Request.AddParameter("fields", "id,first_name,last_name,email,picture,gender,location,age_range,birthday");
        }

        protected override UserInfo ParseUserInfo(string content)
        {
            // var userInfo = base.ParseUserInfo(content);
            var userInfo2 = base.ParseUserInfo(content); // = Ravka.Helpers.Fabric.Mappers.Genereric<UserInfo2>(userInfo);
            /*  "Birthday": "06/01/1978",
  "AgeRange": {
    "Min": 21,
    "Max": 0
  },*/

            var response = JObject.Parse(content);
            userInfo2.Gender = response["gender"].SafeGet(x => x.Value<string>());
            userInfo2.Birthday = response["birthday"].SafeGet(x => x.Value<string>()); // 
            userInfo2.AgeRange = new AgeRange();
            try
            {
                userInfo2.AgeRange.Min = response["age_range"]["min"].SafeGet(x => x.Value<string>()).ToInt();
                userInfo2.AgeRange.Max = response["age_range"]["max"].SafeGet(x => x.Value<string>()).ToInt();

            }
            catch (System.Exception ex)
            {
                Logger.TryCatch(ex, "ParseUserInfo2");
            }
            userInfo2.Location = new Location();
            try
            {
                userInfo2.Location.Id = response["location"]["id"].SafeGet(x => x.Value<string>());
                userInfo2.Location.LocationText = response["location"]["name"].SafeGet(x => x.Value<string>());

            }
            catch (System.Exception ex)
            {
                Logger.TryCatch(ex, "ParseUserInfo2");
            }

            return userInfo2;
        }
    }
}
