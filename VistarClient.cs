using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace VistarApiClient
{
	public class VistarClient
	{
		class LoginCredentials
		{
			public string email { get; set; }
			public string password { get; set; }
		}
		public RestClient Client { get; private set; }
		public VistarUser User { get; private set; }
		private LoginCredentials LoginInfo;
		public bool Staging { get; set; }
		private string BaseApiUrl => Staging ? "https://staging.vistarmedia.com" : "https://trafficking.vistarmedia.com";
		private string ApiUriBuilder(string uri) => BaseApiUrl + uri;
		private string AuthCookieId => Staging ? "tr-staging" : "tr-production";

		/// <summary>
		/// Creates a new instance of the client. Use Connect() method to attempt authentication. 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="staging"></param>
		/// <param name="restClientTimeOut"></param>
		public VistarClient(string username, string password, bool staging = true, int restClientTimeOut = -1)
		{
			Staging = staging;
			Client = new RestClient(BaseApiUrl);
			Client.Timeout = restClientTimeOut;
			LoginInfo = new LoginCredentials()
			{
				email = username,
				password = password
			};
		}

		private static class VistarApiUri
		{
			public static string Authenticate = "/session/";
			public static string VenuesBase = "/seller/v1/venues/";
			public static string SingleVenue(string id) => $"{VenuesBase}{id}";
			public static string DeleteVenue(string id) => $"{VenuesBase}{id}";
		}

		public bool Connect()
		{
			var request = new RestRequest(VistarApiUri.Authenticate, Method.POST);
			request.AddHeader("Content-Type", "text/plain");
			request.AddParameter("text/plain", JsonConvert.SerializeObject(LoginInfo), ParameterType.RequestBody);
			IRestResponse response = Client.Execute(request);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				User = JsonConvert.DeserializeObject<VistarUser>(response.Content);
				var authCookie = response.Cookies.FirstOrDefault(x => x.Name == AuthCookieId);
				var cookieContainer = new CookieContainer();
				var cookie = new Cookie(authCookie.Name, authCookie.Value, authCookie.Path, authCookie.Domain);
				cookieContainer.Add(cookie);
				Client.CookieContainer = cookieContainer;
				return true;
			}
			else
			{
				return false;
			}
		}

		private Exception HandleResponseErrors(IRestResponse response)
		{
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				return new VistarReponseException(JsonConvert.DeserializeObject<VistarErrorResponse>(response.Content));
			}
			else
			{
				return new Exception($"{response.StatusDescription}");
			}
		}

		public List<VistarVenue> GetListOfVenues()
		{
			var request = new RestRequest(VistarApiUri.VenuesBase, Method.GET);
			var response = Client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				var result = JsonConvert.DeserializeObject<VistarVenueResultList>(response.Content);
				return result.Venues;
			}
			else
			{
				throw HandleResponseErrors(response);
			}
		}

		public VistarVenue GetVenueById(string id)
		{
			var request = new RestRequest(VistarApiUri.SingleVenue(id), Method.GET);
			var response = Client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<VistarVenue>(response.Content);
			}
			else
			{
				throw HandleResponseErrors(response);
			}
		}

		public VistarVenue CreateVenue(VistarVenuePayload venue)
		{
			var request = new RestRequest(VistarApiUri.VenuesBase, Method.POST);
			request.AddJsonBody(JsonConvert.SerializeObject(venue), "application/json");
			var response = Client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				//Return venue object from api call		
				return JsonConvert.DeserializeObject<VistarVenue>(response.Content);
			}
			else
			{
				throw HandleResponseErrors(response);
			}
		}

		public VistarVenue UpdateVenue(VistarVenueUpdatePayload venue)
		{
			var request = new RestRequest(VistarApiUri.SingleVenue(venue.Id), Method.PUT);
			request.AddJsonBody(JsonConvert.SerializeObject(venue), "application/json");
			var response = Client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				//Return venue object from api call		
				return JsonConvert.DeserializeObject<VistarVenue>(response.Content);
			}
			else
			{
				throw HandleResponseErrors(response);
			}
		}

		public VistarVenue DeleteVenue(string id)
		{
			var request = new RestRequest(VistarApiUri.DeleteVenue(id), Method.DELETE);
			var response = Client.Execute(request);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				//Return venue object from api call			
				return JsonConvert.DeserializeObject<VistarVenue>(response.Content);
			}
			else
			{
				throw HandleResponseErrors(response);
			}
		}
	}

	public static class VistarExtensions
	{
		public static VistarVenueUpdatePayload ToVenueUpdatePayload(this VistarVenue venue)
		{
			var payload = new VistarVenueUpdatePayload()
			{
				CreativeCategoryRestrictions = venue.CreativeCategoryRestrictions,
				Name = venue.Name,
				VenueType = venue.VenueType,
				ExcludedBuyTypes = venue.ExcludedBuyTypes,
				NetworkId = venue.NetworkId,
				Longitude = venue.Longitude,
				AdvertiserRestrictions = venue.AdvertiserRestrictions,
				OperatingMinutes = venue.OperatingMinutes,
				ActivationDate = venue.ActivationDate,
				CpmFloorCents = venue.CpmFloorCents,
				PartnerVenueId = venue.PartnerVenueId,
				Address = venue.Address,
				Latitude = venue.Latitude,
				Impressions = venue.Impressions,
				LocationId = venue.LocationId,
				Id = venue.Id,
				Gvt = venue.Gvt,

				//Commented out for future api updates 
				//StaticSupported = venue.StaticSupported,
				//VideoSupported = venue.VideoSupported,
				//CortexSupported = venue.CortexSupported,
				//WidthPx = venue.WidthPx,
				//HeightPx = venue.WidthPx,
				//StaticDurationSeconds = venue.StaticDurationSeconds,
				//RegistrationId = venue.RegistrationId
			};

			return payload;
		}

		public static int ToVistarDayOfWeek(this DayOfWeek day)
		{
			return (int)day == 0 ? 6 : (int)day - 1;
		}
	}

}
