﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using JustBrewItGoogle;
//
//    var googleRecords = GoogleRecords.FromJson(jsonString);

namespace JustBrewItGoogle
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GoogleRecords
    {
        [JsonProperty("candidates", NullValueHandling = NullValueHandling.Ignore)]
        public List<Candidate> Candidates { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }

    public partial class Candidate
    {
        [JsonProperty("business_status", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessStatus { get; set; }

        [JsonProperty("formatted_address", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Icon { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("opening_hours", NullValueHandling = NullValueHandling.Ignore)]
        public OpeningHours OpeningHours { get; set; }

        [JsonProperty("photos", NullValueHandling = NullValueHandling.Ignore)]
        public List<Photo> Photos { get; set; }

        [JsonProperty("place_id", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaceId { get; set; }

        [JsonProperty("price_level", NullValueHandling = NullValueHandling.Ignore)]
        public long? PriceLevel { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public double? Rating { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }

        [JsonProperty("viewport", NullValueHandling = NullValueHandling.Ignore)]
        public Viewport Viewport { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public double? Lat { get; set; }

        [JsonProperty("lng", NullValueHandling = NullValueHandling.Ignore)]
        public double? Lng { get; set; }
    }

    public partial class Viewport
    {
        [JsonProperty("northeast", NullValueHandling = NullValueHandling.Ignore)]
        public Location Northeast { get; set; }

        [JsonProperty("southwest", NullValueHandling = NullValueHandling.Ignore)]
        public Location Southwest { get; set; }
    }

    public partial class OpeningHours
    {
        [JsonProperty("open_now", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OpenNow { get; set; }
    }

    public partial class Photo
    {
        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("html_attributions", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> HtmlAttributions { get; set; }

        [JsonProperty("photo_reference", NullValueHandling = NullValueHandling.Ignore)]
        public string PhotoReference { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }
    }

    public partial class GoogleRecords
    {
        public static GoogleRecords FromJson(string json)
        {
            return JsonConvert.DeserializeObject<GoogleRecords>(json, JustBrewItGoogle.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this GoogleRecords self)
        {
            return JsonConvert.SerializeObject(self, JustBrewItGoogle.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}