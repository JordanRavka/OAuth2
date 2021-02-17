using Ravka.Helpers.Extensions;
using System;

namespace OAuth2.Models
{

    /// <summary>
    /// Contains information about user who is being authenticated.
    /// </summary>
    public partial class UserInfo // 2 : UserInfo
    {
        public bool HasUniqueIdentity
        {
            get
            {
                return this.Id.IsNullOrWhiteSpace() == false || this.Email.IsNullOrWhiteSpace() == false;
            }
        }
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

