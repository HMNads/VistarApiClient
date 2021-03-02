using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VistarApiClient
{
	public class VistarReponseException : Exception
	{
		public VistarErrorResponse Error { get; set; }
		public VistarReponseException(VistarErrorResponse error)
		{
			Error = error;
			var test = "";
		}
	}

	public class VistarErrorResponse
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("code")]
		public long Code { get; set; }

		[JsonProperty("details")]
		public ErrorDetail[] Details { get; set; }
	}

	public class ErrorDetail
	{
		[JsonProperty("@type")]
		public string Type { get; set; }

		[JsonProperty("field_violations")]
		public FieldViolation[] FieldViolations { get; set; }
	}

	public class FieldViolation
	{
		[JsonProperty("field")]
		public string Field { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}

	public class VistarVenuePayload
	{
		[JsonProperty("creative_category_restrictions")]
		public CreativeCategoryRestrictions CreativeCategoryRestrictions { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("venue_type")]
		public string VenueType { get; set; }

		[JsonProperty("excluded_buy_types")]
		public List<string> ExcludedBuyTypes { get; set; }

		[JsonProperty("network_id")]
		public string NetworkId { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		[JsonProperty("advertiser_restrictions")]
		public AdvertiserRestrictions AdvertiserRestrictions { get; set; }

		[JsonProperty("operating_minutes")]
		public List<OperatingMinute> OperatingMinutes { get; set; }

		[JsonProperty("activation_date")]
		public DateTime? ActivationDate { get; set; }

		[JsonProperty("cpm_floor_cents")]
		public long CpmFloorCents { get; set; }

		[JsonProperty("partner_venue_id")]
		public string PartnerVenueId { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("impressions")]
		public Impressions Impressions { get; set; }

		[JsonProperty("gvt")]
		public Gvt Gvt { get; set; }

        [JsonProperty("video_supported")]
        public bool VideoSupported { get; set; }

        [JsonProperty("static_supported")]
        public bool StaticSupported { get; set; }

        [JsonProperty("cortex_supported")]
        public bool CortexSupported { get; set; }

        [JsonProperty("registration_id")]
        public string RegistrationId { get; set; }

        [JsonProperty("height_px")]
        public int HeightPx { get; set; }

        [JsonProperty("width_px")]
        public int WidthPx { get; set; }

        [JsonProperty("static_duration_seconds")]
        public int StaticDurationSeconds { get; set; }
    }

	public class VistarVenueUpdatePayload : VistarVenuePayload
	{
		[JsonProperty("location_id")]
		public string LocationId { get; set; }
		[JsonProperty("id")]
		public string Id { get; set; }
	}

	public class VistarUser
	{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("is_buyer")]
		public bool IsBuyer { get; set; }

		[JsonProperty("abilities")]
		public Dictionary<string, string[]> Abilities { get; set; }

		[JsonProperty("public_id")]
		public string PublicId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("allow_letterbox")]
		public bool AllowLetterbox { get; set; }

		[JsonProperty("is_multi_authed")]
		public bool IsMultiAuthed { get; set; }

		[JsonProperty("locale")]
		public string Locale { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("client_fee_bp")]
		public long ClientFeeBp { get; set; }

		[JsonProperty("account_id")]
		public string AccountId { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("role_name")]
		public string RoleName { get; set; }

		[JsonProperty("is_admin")]
		public bool IsAdmin { get; set; }

		[JsonProperty("is_multi_account")]
		public bool IsMultiAccount { get; set; }

		[JsonProperty("is_seller")]
		public bool IsSeller { get; set; }

		[JsonProperty("homepage")]
		public string Homepage { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("screen_count_enabled")]
		public bool ScreenCountEnabled { get; set; }

		[JsonProperty("account_email")]
		public string AccountEmail { get; set; }
	}

	public class VistarVenueResultList
	{
		[JsonProperty("venues")]
		public List<VistarVenue> Venues { get; set; }
	}

	public class VistarVenue
	{
		[JsonProperty("venue_type_id")]
		public long VenueTypeId { get; set; }

		[JsonProperty("creative_category_restrictions")]
		public CreativeCategoryRestrictions CreativeCategoryRestrictions { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("venue_type")]
		public string VenueType { get; set; }

		[JsonProperty("excluded_buy_types")]
		public List<string> ExcludedBuyTypes { get; set; }

		[JsonProperty("network_id")]
		public string NetworkId { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		[JsonProperty("advertiser_restrictions")]
		public AdvertiserRestrictions AdvertiserRestrictions { get; set; }

		[JsonProperty("operating_minutes")]
		public List<OperatingMinute> OperatingMinutes { get; set; }

		[JsonProperty("activation_date")]
		public DateTime? ActivationDate { get; set; }

		[JsonProperty("gvt")]
		public Gvt Gvt { get; set; }

		[JsonProperty("partner_venue_id")]
		public string PartnerVenueId { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("impressions")]
		public Impressions Impressions { get; set; }

		[JsonProperty("location_id")]
		public string LocationId { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("cpm_floor_cents")]
		public long CpmFloorCents { get; set; }

		[JsonProperty("video_supported")]
		public bool VideoSupported { get; set; }

		[JsonProperty("static_supported")]
		public bool StaticSupported { get; set; }

		[JsonProperty("cortex_supported")]
		public bool CortexSupported { get; set; }

		[JsonProperty("registration_id")]
		public string RegistrationId { get; set; }

		[JsonProperty("height_px")]
		public int HeightPx { get; set; }

		[JsonProperty("width_px")]
		public int WidthPx { get; set; }

		[JsonProperty("static_duration_seconds")]
		public int StaticDurationSeconds { get; set; }
	}

	public class AdvertiserRestrictions
	{
		[JsonProperty("exclude")]
		public bool Exclude { get; set; }

		[JsonProperty("advertiser_ids")]
		public List<string> AdvertiserIds { get; set; }
	}

	public class CreativeCategoryRestrictions
	{
		[JsonProperty("exclude")]
		public bool Exclude { get; set; }

		[JsonProperty("categories")]
		public List<string> Categories { get; set; }
	}

	public class Gvt
	{
		[JsonProperty("seconds")]
		public long Seconds { get; set; }

		[JsonProperty("impressions")]
		public long Impressions { get; set; }

		[JsonProperty("dwell_time_seconds")]
		public long DwellTimeSeconds { get; set; }
	}

	public class Impressions
	{
		[JsonProperty("per_spot")]
		public double PerSpot { get; set; }

		[JsonProperty("per_second")]
		public double PerSecond { get; set; }
	}

	public class OperatingMinute
	{
		[JsonProperty("local_end_minute_of_week")]
		public int LocalEndMinuteOfWeek { get; set; }

		[JsonProperty("local_start_minute_of_week")]
		public int LocalStartMinuteOfWeek { get; set; }

		public OperatingMinute()
		{

		}
		/// <summary>
		/// Monday = 0 -> Sunday = 6
		/// </summary>
		/// <param name="openTime"></param>
		/// <param name="closeTime"></param>
		/// <param name="day"></param>
		public OperatingMinute(TimeSpan openTime, TimeSpan closeTime, int day)
		{
			//Day of week (0-6) * minutes in an hour (60) * hours in a day (24) + hour of day (0-23) * minutes in an hour (60) + minute of hour (0-59).
			this.LocalStartMinuteOfWeek = day * 60 * 24 + openTime.Hours * 60 + openTime.Minutes;
			this.LocalEndMinuteOfWeek = day * 60 * 24 + closeTime.Hours * 60 + closeTime.Minutes;
		}
	}
}
