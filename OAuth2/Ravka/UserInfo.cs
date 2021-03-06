using Ravka.Helpers.Extensions;
using System;

namespace OAuth2.Models
{

    /// <summary>
    /// Contains information about user who is being authenticated.
    /// </summary>
    public partial class UserInfo // RAVKA VERSION
    {
        public bool HasIdOrEmailOrPhone
        {
            get
            {
                return this.Id.IsNullOrWhiteSpace() == false
                    || this.Email.IsNullOrWhiteSpace() == false
                    || this.Phone.IsNullOrWhiteSpace() == false;
            }
        }

        public string Raw { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }

        // public string AgeRangeTxt { get; set; } 
        public AgeRange AgeRange { get; set; } = new AgeRange();
        public Location Location { get; set; } = new Location();
        //  public string AddressTxt { get; set; }
        //  public string Address { get; set; }

        public DateTime ToBirthDate()
        {
            if (Birthday.IsNullOrEmpty() == false)
            {
                var birthdate = Ravka.Helpers.Dates.ToDateTime(Birthday);
                if (Ravka.Helpers.Dates.IsValid(birthdate))
                    return birthdate;
            }
            return Ravka.Helpers.Dates.SqlMinDate;
        }

        public string OriginalPicUrl { get; set; }

        public string BiggestPicAvailableUrl // RAVKA
        {
            get
            {
                if (!string.IsNullOrEmpty(OriginalPicUrl))
                    return OriginalPicUrl;

                if (AvatarUri != null)
                {
                    if (!string.IsNullOrEmpty(AvatarUri.Large))
                        return AvatarUri.Large;
                    else if (!string.IsNullOrEmpty(AvatarUri.Normal))
                        return AvatarUri.Normal;
                    else
                        return AvatarUri.Small;
                }
                else
                    return string.Empty;

            }
        }
    }
    public partial class AgeRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
    public partial class Location
    {
        public string Id { get; set; }
        public string LocationText { get; set; }

        public bool HasValue
        {
            get { return LocationText.IsNullOrWhiteSpace() == false; }
        }
    }
}

