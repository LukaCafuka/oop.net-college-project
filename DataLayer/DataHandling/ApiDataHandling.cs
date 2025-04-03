using Newtonsoft.Json;
using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.JsonModels;
using RestSharp;

namespace DataLayer.DataHandling
{
    class ApiDataHandling
    {
        //Loads countries
        public static Task<HashSet<Team>> LoadJsonTeams()
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
                            return JsonConvert.DeserializeObject<HashSet<Team>>(json);
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
                            return JsonConvert.DeserializeObject<HashSet<Team>>(json);
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
                        return JsonConvert.DeserializeObject<HashSet<Team>>(response.Content);
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleTeamsURL);
                        var response = apiClient.Execute<HashSet<Team>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<Team>>(response.Content);
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
                            return JsonConvert.DeserializeObject<HashSet<Matches>>(json);
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
                            return JsonConvert.DeserializeObject<HashSet<Matches>>(json);
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
                        var apiClient = new RestClient(ResourceLocations.FemaleMatchesDetailedURL);
                        var response = apiClient.Execute<HashSet<Matches>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<Matches>>(response.Content);
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleMatchesDetailedURL);
                        var response = apiClient.Execute<HashSet<Matches>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<Matches>>(response.Content);
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
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json);
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
                            return JsonConvert.DeserializeObject<HashSet<TeamResults>>(json);
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
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content);
                    });
                }
                else
                {
                    return Task.Run(() =>
                    {
                        var apiClient = new RestClient(ResourceLocations.MaleTeamsURL);
                        var response = apiClient.Execute<HashSet<TeamResults>>(new RestRequest());
                        return JsonConvert.DeserializeObject<HashSet<TeamResults>>(response.Content);
                    });
                }
            }
        }
    }
}
