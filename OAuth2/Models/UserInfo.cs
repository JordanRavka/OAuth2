namespace OAuth2.Models
{
    public class AvatarInfo
    {
        /// <summary>
        /// Image size constants.
        /// </summary>
        internal const int SmallSize = 36;
        internal const int LargeSize = 300;

        /// <summary>
        /// Uri of small photo.
        /// </summary>
        public string Small { get; set; }

        /// <summary>
        /// Uri of normal photo.
        /// </summary>
        public string Normal { get; set; }

        /// <summary>
        /// Uri of large photo.
        /// </summary>
        public string Large { get; set; }

        public string BiggestPicAvailable // RAVKA
        {
            get
            {
                if (!string.IsNullOrEmpty(Large))
                    return Large;
                else if (!string.IsNullOrEmpty(Normal))
                    return Normal;
                else
                    return Small;

            }
        }
    }

    /// <summary>
    /// Contains information about user who is being authenticated.
    /// </summary>
    public partial class UserInfo // ORIGINAL + PARTIAL (RAVKA)
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserInfo()
        {
            AvatarUri = new AvatarInfo();
        }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Friendly name of <see cref="UserInfo"/> provider (which is, in its turn, the client of OAuth/OAuth2 provider).
        /// </summary>
        /// <remarks>
        /// Supposed to be unique per OAuth/OAuth2 client.
        /// </remarks>
        public string ProviderName { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Photo URI.
        /// </summary>
        public string PhotoUri
        {
            get { return AvatarUri.BiggestPicAvailable; } // RAVKA
        }

        /// <summary>
        /// Contains URIs of different sizes of avatar.
        /// </summary>
        public AvatarInfo AvatarUri { get; private set; }
    }
}

