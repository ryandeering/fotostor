﻿using System.Threading.Tasks;
using _4thYearProject.Api.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        // GET: api/<controller>
        [HttpGet("{searchText}")]
        public IActionResult GetSearchResults(string searchText)
        {
            if (searchText == null)
                return NotFound();

            return Ok(_searchRepository.GetSearchResults(searchText));
        }
    }
}