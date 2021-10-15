using EndpointAPI.Infrastructure;
using EndpointAPI.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointAPI.Interface
{
    public interface IMultitracksInterface
    {
        string GetArtistByName(string input);
        IPagedList<Song>ListAllSong(int? pageNumber, int? pageSize);

        string AddArtist(ArtistInputModel artist);
    }
}
