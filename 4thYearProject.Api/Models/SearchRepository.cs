﻿using System.Collections.Generic;
using System.Linq;
using _4thYearProject.Api.Models.Interfaces;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _appDbContext;

        public SearchRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<SearchResult> GetSearchResults(string searchText)
        {

            List<SearchResult> results = new List<SearchResult>();

            var profiles = _appDbContext.Users.Where(u => u.DisplayName.ToLower().Contains(searchText.ToLower()))
                .ToList().Take(5);

            var hashtags = _appDbContext.Hashtags.Where(h => h.Content.ToLower().Contains(searchText.ToLower()))
                .ToList().Take(5);

            foreach (var profile in profiles)
            {
                results.Add(new SearchResult{Type = "Profile", Content = profile.DisplayName});
            }

            foreach (var hashtag in hashtags)
            {
                results.Add(new SearchResult{Type = "Hashtag", Content = hashtag.Content });
            }

            return results;
        }
    }
}