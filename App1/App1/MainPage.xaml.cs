using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        static HttpClient client = new HttpClient();
        static AllAsteroids asteroid = new AllAsteroids();
        public MainPage()
        {
            InitializeComponent();

        }
        private async Task But_Clicked(object sender, EventArgs e)
        {
            await Api();
        }
        private async Task Api()
        {
            DateTime edt = DateTime.Now; //end date
            DateTime sdt = edt.AddDays(-7); //start date
            try
            {
                string api = "https://" + $"api.nasa.gov/neo/rest/v1/feed?start_date={sdt.Year}-{sdt.Month}-{sdt.Day}&end_date={edt.Year}-{edt.Month}-{edt.Day}&api_key=hfA4LAmoG8ihFni7mRPJxKO5c4gLbDu5cv1FeI59";
                string response = await client.GetStringAsync(api);
                if (File.Exists("Api"))
                {
                    File.Delete("Api");
                    using (StreamWriter sw = File.CreateText("Api"))
                    {
                        sw.Write(response);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText("Api"))
                    {
                        sw.Write(response);
                    }
                }
                AllAsteroids asteroid = JsonConvert.DeserializeObject<AllAsteroids>(response);
            }
            catch (Exception)
            {
                using (StreamReader sr = File.OpenText("Api"))
                {
                    AllAsteroids asteroid = JsonConvert.DeserializeObject<AllAsteroids>(sr.ReadToEnd());
                }
            }
            Label l = new Label(); // nevím jak to mám lépe udělat, nefunguej mi emulace takže nevím jak to vypadá
            foreach (var item in asteroid.asteroid)
            {
                l.Text += $"Asteroid {item.Value} \n";
            }
        }

        private void but_Clicked_1(object sender, EventArgs e)
        {

        }
    }

    public class Estimated_diameter
    {
        [JsonProperty("kilometers")]
        public Kilometers kilometers { get; set; }
        [JsonProperty("meters")]
        public Meters meters { get; set; }
        [JsonProperty("miles")]
        public Miles miles { get; set; }
        [JsonProperty("feet")]
        public Feet feet { get; set; }
    }
    public class Kilometers
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal estimated_diameter_min { get; set; }
        [JsonProperty("estimated_diameter_max")]
        public decimal estimated_diameter_max { get; set; }
    }
    public class Meters
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal estimated_diameter_min { get; set; }
        [JsonProperty("estimated_diameter_max")]
        public decimal estimated_diameter_max { get; set; }
    }
    public class Miles
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal estimated_diameter_min { get; set; }
        [JsonProperty("estimated_diameter_max")]
        public decimal estimated_diameter_max { get; set; }
    }
    public class Feet
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal estimated_diameter_min { get; set; }
        [JsonProperty("estimated_diameter_max")]
        public decimal estimated_diameter_max { get; set; }
    }
    public class Close_approach_data
    {
        [JsonProperty("close_approach_date")]
        public string close_approach_date { get; set; }
        [JsonProperty("close_approach_date_full")]
        public string close_approach_date_full { get; set; }
        [JsonProperty("epoch_date_close_approach")]
        public string epoch_date_close_approach { get; set; }
        [JsonProperty("relative_velocity")]
        public Relative_velocity relative_velocity { get; set; }
        [JsonProperty("miss_distance")]
        public Miss_distance miss_distance { get; set; }
        [JsonProperty("orbiting_body")]
        public string orbiting_body { get; set; }
    }
    public class Relative_velocity
    {
        [JsonProperty("kilometers_per_second")]
        public decimal kilometers_per_second { get; set; }
        [JsonProperty("kilometers_per_hour")]
        public decimal kilometers_per_hour { get; set; }
        [JsonProperty("miles_per_hour")]
        public decimal miles_per_hour { get; set; }
    }
    public class Miss_distance
    {
        [JsonProperty("astronomical")]
        public decimal astronomical { get; set; }
        [JsonProperty("lunar")]
        public decimal lunar { get; set; }
        [JsonProperty("kilometers")]
        public decimal kilometers { get; set; }
        [JsonProperty("miles")]
        public decimal miles { get; set; }
    }
    public class Asteroid
    {
        [JsonProperty("links")]
        public Links links { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("neo_reference_id")]
        public int neo_reference_id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("nasa_jpl_url")]
        public string nasa_jpl_url { get; set; }
        [JsonProperty("absolute_magnitude_h")]
        public float absolute_magnitude_h { get; set; }
        [JsonProperty("estimated_diameter")]
        public Estimated_diameter estimated_diameter { get; set; }
        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool is_potentially_hazardous_asteroid { get; set; }
        [JsonProperty("close_approach_data")]
        public Close_approach_data close_approach_data { get; set; }
        [JsonProperty("is_sentry_object")]
        public bool is_sentry_object { get; set; }

    }
    public class AllAsteroids
    {
        [JsonProperty("links")]
        public Links links { get; set; }
        [JsonProperty("element_count")]
        public int element_count { get; set; }
        [JsonProperty("near_earth_objects")]
        public Dictionary<string, List<Asteroid>> asteroid { get; set; } //poradil Vojtěch
        //public Near_earth_objects near_earth_objects { get; set; }
    }
    public class Links
    {
        [JsonProperty("next", NullValueHandling = NullValueHandling.Ignore)]
        public string next { get; set; }
        [JsonProperty("prev", NullValueHandling = NullValueHandling.Ignore)]
        public string prev { get; set; }
        [JsonProperty("self")]
        public string self { get; set; }
    }
    //public class Near_earth_objects
    //{
        //[JsonProperty("2021-12-12")]
        //public List<Asteroid> asteroid { get; set; }
    //}
}
