using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GoogleAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var result = client.GetAsync("https://www.googleapis.com/youtube/v3/search?key=AIzaSyBa053D2Xtk2zt7Vt4rROH20s9ZvR-fpCA&channelId=UChFInZnmH4q2URJ9D5rJc0A&part=snippet,id&order=date&maxResults=20").Result;

            var content = result.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<SearchListResponse>(content);

            //var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            //{
            //    ApplicationName = "TestBigApp",
            //    ApiKey = "AIzaSyBa053D2Xtk2zt7Vt4rROH20s9ZvR-fpCA",
            //});

            //var searchListRequest = youtubeService.Search.List("snippet");
            //searchListRequest.ChannelId = "UChFInZnmH4q2URJ9D5rJc0A";
            //searchListRequest.Type = "youtube#video";
            //var searchListResponse = searchListRequest.ExecuteAsync().Result;

            //var videos = new List<YouTubeVideo>();

            //foreach (var item in searchListResponse.Items)
            //    videos.Add(new YouTubeVideo {
            //        Id = item.Id.VideoId,
            //        Title = item.Snippet.Title,
            //        Description = item.Snippet.Description,
            //        Date = item.Snippet.PublishedAt,
            //        Thumbnail = new Thumbnail {
            //            Low = item.Snippet.Thumbnails.Default__.Url,
            //            Medium = item.Snippet.Thumbnails.Medium.Url,
            //            High = item.Snippet.Thumbnails.High.Url
            //        }
            //    });

            //foreach (var video in videos)
            //{
            //    Console.WriteLine($"{video.Id} - {video.Title} - {video.Description} - {video.Thumbnail.High}");
            //}
            //    var channelsListRequest = youtubeService.Channels.List("contentDetails");
            //    //channelsListRequest.Mine = true;

            //    // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            //    var searchListRequest = youtubeService.Search.List("snippet");
            //    //searchListRequest.Q = "Google"; // Replace with your search term.
            //    searchListRequest.Type = "youtube#video";

            //    searchListRequest.MaxResults = 50;

            //    // Call the search.list method to retrieve results matching the specified query term.
            //    var searchListResponse = searchListRequest.ExecuteAsync().Result;

            //    List<string> videos = new List<string>();
            //    List<string> channels = new List<string>();
            //    List<string> playlists = new List<string>();

            //    // Add each result to the appropriate list, and then display the lists of
            //    // matching videos, channels, and playlists.
            //    foreach (var searchResult in searchListResponse.Items)
            //    {
            //        switch (searchResult.Id.Kind)
            //        {
            //            case "youtube#video":
            //                videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
            //                break;

            //            case "youtube#channel":
            //                channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
            //                break;

            //            case "youtube#playlist":
            //                playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
            //                break;
            //        }
            //    }

            //    Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            //    Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            //    Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));


            //    var stop = "here";
        }
    }

    public class YouTubeVideo
    {
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }

    public sealed class Thumbnail
    {
        public string Low { get; set; }
        public string Medium { get; set; }
        public string High { get; set; }
    }
}
