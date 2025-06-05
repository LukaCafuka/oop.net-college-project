using Newtonsoft.Json;
using QuickType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.JsonModels;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using Json2 = System.Text.Json;

namespace DataLayer.DataHandling
{
    public class ApiDataHandling
    {
        [JsonProperty("time")]
        [JsonConverter(typeof(TimeEnumConverter))]
        public Time Time { get; set; }
        //Loads countries
        public static Task<HashSet<TeamResults>> LoadJsonTeams()
        {
            if (File.Exists(ResourceLocations.FemaleTeamsPath) && File.Exists(ResourceLocations.MaleTeamsPath))
            {
                //File load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.FemaleTeamsPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json, Converter.Settings) ?? new HashSet<TeamResults>();
                        }
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.MaleTeamsPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json, Converter.Settings) ?? new HashSet<TeamResults>();
                        }
                    });
                }
            }
            else
            {
                //Web load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.FemaleTeamsURL);
                        var response = apiClient.Execute<HashSet<Team>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content, Converter.Settings) ?? new HashSet<TeamResults>();
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleTeamsURL);
                        var response = apiClient.Execute<HashSet<Team>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content, Converter.Settings) ?? new HashSet<TeamResults>();
                    });
                }
            }
        }

        //Loads players
        public static Task<HashSet<Matches>> LoadJsonMatches()
        {
            if (File.Exists(ResourceLocations.FemaleMatchesPath) || File.Exists(ResourceLocations.MaleMatchesPath))
            {
                //File load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.FemaleMatchesPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<Matches>>(json, Converter.Settings) ?? new HashSet<Matches>();
                        }
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.MaleMatchesPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<Matches>>(json, Converter.Settings) ?? new HashSet<Matches>();
                        }
                    });
                }
            }
            else
            {
                //Web load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.FemaleMatchesURL);
                        var response = apiClient.Execute<HashSet<Matches>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<Matches>>(response.Content, Converter.Settings) ?? new HashSet<Matches>();
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleMatchesURL);
                        var response = apiClient.Execute<HashSet<Matches>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<Matches>>(response.Content, Converter.Settings) ?? new HashSet<Matches>();
                    });
                }
            }
        }

        //Loads detailed countries
        public static Task<HashSet<TeamResults>> LoadJsonResults()
        {
            if (File.Exists(ResourceLocations.FemaleTeamsPath) && File.Exists(ResourceLocations.MaleResultsPath))
            {
                //File load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.FemaleResultsPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json, Converter.Settings) ?? new HashSet<TeamResults>();
                        }
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        using (StreamReader reader = new StreamReader(ResourceLocations.MaleResultsPath))
                        {
                            string json = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json, Converter.Settings) ?? new HashSet<TeamResults>();
                        }
                    });
                }
            }
            else
            {
                //Web load
                if (ConfigFile.gender == ConfigFile.Gender.Female)
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.FemaleTeamsURL);
                        var response = apiClient.Execute<HashSet<TeamResults>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content, Converter.Settings) ?? new HashSet<TeamResults>();
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleTeamsURL);
                        var response = apiClient.Execute<HashSet<TeamResults>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content, Converter.Settings) ?? new HashSet<TeamResults>();
                    });
                }
            }
        }
    }
}
