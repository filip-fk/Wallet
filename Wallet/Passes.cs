using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wallet
{
    /*
class Pass
{
    #region
    private string organisationT { get; set; }
    private string organisation { get; set; }

    private string gateT { get; set; }
    private string gate { get; set; }

    private string seatT { get; set; }
    private string seat { get; set; }

    private string origin { get; set; }

    private string origin_short { get; set; }

    private string dest { get; set; }

    private string dest_short { get; set; }

    private string middle_info { get; set; }
    private string font_info { get; set; }

    private string flight_numT { get; set; }
    private string flight_num { get; set; }

    private string board_timeT { get; set; }
    private string board_time { get; set; }

    private string clasT { get; set; }
    private string clas { get; set; }

    private string dateT { get; set; }
    private string date { get; set; }

    private string passengerT { get; set; }
    private string passenger { get; set; }

    private string statusT { get; set; }
    private string status { get; set; }

    private bool pgr_r { get; set; }

    private Color c1 { get; set; }
    private Color c2 { get; set; }

    private SolidColorBrush bGround { get; set; }
    private SolidColorBrush fGround { get; set; }

    private string id;
    #endregion

    // to a class to have it serialized by the DataContractSerializer.
    [DataContract(Name = "Pass")]
    class Pass : IExtensibleDataObject
    {
        [DataMember()]
        public string PassTypeIdentifier { get; set; }
        [DataMember()]
        public long FormatVersion { get; set; }
        [DataMember()]
        public string SerialNumber { get; set; }
        [DataMember()]
        public string Description { get; set; }
        [DataMember()]
        public string OrganizationName { get; set; }
        [DataMember()]
        public string TeamIdentifier { get; set; }
        [DataMember()]
        public string RelevantDate { get; set; }
        [DataMember()]
        public string LogoText { get; set; }
        [DataMember()]
        public List<object> Locations { get; set; }
        [DataMember()]
        public string ForegroundColor { get; set; }
        [DataMember()]
        public string BackgroundColor { get; set; }
        [DataMember()]
        public string LabelColor { get; set; }
        [DataMember()]
        public BoardingPass boardingPass { get; set; }
        [DataMember()]
        public Barcode barcode { get; set; }
        [DataMember()]
        public string AuthenticationToken { get; set; }
        [DataMember()]
        public string WebServiceUrl { get; set; }

        public Pass()
        {

        }
        public partial class Barcode
        {
            [DataMember()]
            public string Format { get; set; }

            [DataMember()]
            public string Message { get; set; }

            [DataMember()]
            public string MessageEncoding { get; set; }
        }

        public partial class BoardingPass
        {
            [DataMember()]
            public List<Field> HeaderFields { get; set; }

            [DataMember()]
            public List<Field> PrimaryFields { get; set; }

            [DataMember()]
            public List<Field> SecondaryFields { get; set; }

            [DataMember()]
            public List<Field> AuxiliaryFields { get; set; }

            [DataMember()]
            public List<Field> BackFields { get; set; }

            [DataMember()]
            public string TransitType { get; set; }
        }

        public partial class Field
        {
            [DataMember()]
            public string Key { get; set; }

            [DataMember()]
            public string Label { get; set; }

            [DataMember()]
            public string Value { get; set; }
        }

        public ExtensionDataObject ExtensionData { get; set; }
    }
    */

    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var welcome = Welcome.FromJson(jsonString);
    

        public partial class Welcome
        {
            [JsonProperty("passTypeIdentifier")]
            public string PassTypeIdentifier { get; set; }

            [JsonProperty("formatVersion")]
            public long FormatVersion { get; set; }

            [JsonProperty("serialNumber")]
            public string SerialNumber { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("organizationName")]
            public string OrganizationName { get; set; }

            [JsonProperty("teamIdentifier")]
            public string TeamIdentifier { get; set; }

            [JsonProperty("relevantDate")]
            public string RelevantDate { get; set; }

            [JsonProperty("logoText")]
            public string LogoText { get; set; }

            [JsonProperty("locations")]
            public List<object> Locations { get; set; }

            [JsonProperty("foregroundColor")]
            public string ForegroundColor { get; set; }

            [JsonProperty("backgroundColor")]
            public string BackgroundColor { get; set; }

            [JsonProperty("labelColor")]
            public string LabelColor { get; set; }

            [JsonProperty("boardingPass")]
            public BoardingPass BoardingPass { get; set; }

            [JsonProperty("barcode")]
            public Barcode Barcode { get; set; }

            [JsonProperty("authenticationToken")]
            public string AuthenticationToken { get; set; }

            [JsonProperty("webServiceURL")]
            public string WebServiceUrl { get; set; }
        }

        public partial class Barcode
        {
            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("messageEncoding")]
            public string MessageEncoding { get; set; }
        }

        public partial class BoardingPass
        {
            [JsonProperty("headerFields")]
            public List<Field> HeaderFields { get; set; }

            [JsonProperty("primaryFields")]
            public List<Field> PrimaryFields { get; set; }

            [JsonProperty("secondaryFields")]
            public List<Field> SecondaryFields { get; set; }

            [JsonProperty("auxiliaryFields")]
            public List<Field> AuxiliaryFields { get; set; }

            [JsonProperty("backFields")]
            public List<Field> BackFields { get; set; }

            [JsonProperty("transitType")]
            public string TransitType { get; set; }
        }

        public partial class Field
        {
            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public partial class Welcome
        {
            public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
        }

        public static class Serialize
        {
            public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        internal class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }
    
}

