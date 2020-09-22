

namespace HorizonSite.Models
{
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class HomeIndexViewModels
    {
        public IEnumerable<Film> LastThreeFilms { get; set; }
        public IEnumerable<Film> RentedFilms { get; set; }
        public IPagedList<Review> LastTwoReviews { get; set; }
        public Message SendMessage { get; set; }
        public IEnumerable<Genre> listNameGenre { get; set; }
        public IEnumerable<Session> listDateSession { get; set; }
        public IEnumerable<Review> CountReview { get; set; }
        public IEnumerable<Film> AllFilms { get; set; }
        public IEnumerable<FilmsProducer> producersFilm { get; set; }
        public IEnumerable<FilmsActor> actorFilm { get; set; }
    }
}