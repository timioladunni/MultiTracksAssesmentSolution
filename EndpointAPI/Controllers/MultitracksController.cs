using EndpointAPI.Infrastructure;
using EndpointAPI.Interface;
using EndpointAPI.Model;
using EndpointAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EndpointAPI.Service.Responses;

namespace EndpointAPI.Controllers
{
    public class MultitracksController : Controller
    {
        private readonly IMultitracksInterface _interface;
        private IConfiguration _config;
        public MultitracksController(IMultitracksInterface @interface, IConfiguration config)
        {
            this._interface = @interface;
            this._config = config;
        }




        /// <summary>
        /// Endpoint to get artist detail using the artist's name to search
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api.multitracks.com/artist/search")]
        public async Task<IActionResult> GetArtist(string artistName)
        {
            try
            {
                if (!string.IsNullOrEmpty(artistName))
                {
                    var artistdetails =  _interface.GetArtistByName(artistName);
                    if (artistdetails == Status.ServiceFailedAtException.ToString())
                    {
                        return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
                    }
                    if (!string.IsNullOrEmpty(artistdetails))
                    {
                        return  new SuccessResponse().ArtistResponse(artistdetails);
                    }
                    else
                    {
                        return new UnSuccessful().ReturnResponse("Artist does not exist");
                    }
                }
                else
                {
                    return new UnSuccessful().ReturnResponse("Artist name cannot be empty");
                }
            }
            catch (Exception)
            {
                return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
            }
        }




        /// <summary>
        /// Endpoint to add a new artist to the database using the given model
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api.multitracks.com/artist/add")]
        public async Task<IActionResult> AddArtist([FromBody] ArtistInputModel artist) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addArtist = _interface.AddArtist(artist);
                    if (addArtist == Status.ServiceFailedAtException.ToString())
                    {
                        return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
                    }
                    else if (string.IsNullOrEmpty(addArtist))
                    {
                        return new UnSuccessful().ReturnResponse("Input correct values");
                    }
                    else if (addArtist == Status.NewArtistAdded.ToString())
                    {
                        return new SuccessResponse().AddArtist(Status.NewArtistAdded.ToString());
                    }
                    else
                    {
                        return new SuccessResponse().ReturnResponse(Status.NewArtistNotAdded.ToString());
                    }
                }
                catch (Exception)
                {
                    return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
                }
            }
            else
            {
                return new UnSuccessful().ReturnResponse("Model state is Invalid");
            }
        }



        /// <summary>
        /// Endpoint to get the all the songs in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api.multitracks.com/song/list")]
        public async Task<IActionResult> ListSongs(int? pageNumber, int? pageSize)
        {
            try
            {
                var songList = _interface.ListAllSong(pageNumber, pageSize);
                if (songList != null)
                {
                    return new SuccessResponse().ListSongs(songList);
                }
                else
                {
                    return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
                }
                
            }
            catch (Exception)
            {
                return new UnSuccessful().ReturnResponse(Status.ServiceFailedAtException.ToString());
            }
            
        }
        
    }
}
