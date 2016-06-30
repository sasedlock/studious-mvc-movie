using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.DataAccess.Context;
using MvcMovie.Domain.Models;
using MvcMovie.Models;
using MvcMovie.Service.Services;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MvcMovieDbContext db = new MvcMovieDbContext();
        private MovieService _service = new MovieService();

        // GET: Movies
        public ActionResult Index(string movieGenre, string name)
        {
            //var genres = from m in db.Movies
            //             orderby m.Genre
            //             select m.Genre;
            var genres = _service.GetGenres();

            ViewBag.movieGenre = new SelectList(genres.Distinct().ToList());

            //var movies = from m in db.Movies
            //             select m;
            var movies = _service.GetMovies(movieGenre, name);

            //if (!string.IsNullOrEmpty(name))
            //{
            //    movies = movies.Where(s => s.Title.Contains(name));
            //}

            //if (!string.IsNullOrEmpty(movieGenre))
            //{
            //    movies = movies.Where(m => m.Genre == movieGenre);
            //}

            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            var movie = _service.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //    db.Movies.Add(movie);
                //    db.SaveChanges();
                _service.AddMovie(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            var movie = _service.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(movie).State = EntityState.Modified;
                //db.SaveChanges();
                _service.EditMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            var movie = _service.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Movie movie = db.Movies.Find(id);
            //db.Movies.Remove(movie);
            //db.SaveChanges();
            _service.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
