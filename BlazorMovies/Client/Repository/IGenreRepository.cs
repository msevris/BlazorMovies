﻿using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public interface IGenreRepository
    {
        Task CreateGenre(Genre genre);
        Task DeleteGenre(int Id);
        Task<Genre> GetGenre(int Id);
        Task<List<Genre>> GetGenres();
        Task UpdateGenre(Genre genre);
    }
}
