using Bertoni_MiniLab.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bertoni_MiniLab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Get json
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/albums");
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string JSONalbums;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JSONalbums = sr.ReadToEnd();
            }

            //Deserialize json to model
            var albums = JsonConvert.DeserializeObject<List<Album>>(JSONalbums);


            return View(albums);
        }

        public PartialViewResult getPhotos(int albumId)
        {
            //Get json
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/photos");
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string JSONphotos;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JSONphotos = sr.ReadToEnd();
            }

            //Deserialize json to model
            var photos = JsonConvert.DeserializeObject<List<Photo>>(JSONphotos);

            return PartialView(photos.Where(x => x.albumId == albumId).ToList());        
        }


        public PartialViewResult getComments(int photoId)
        {
            //Get json
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/comments");
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string JSONcomments;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JSONcomments = sr.ReadToEnd();
            }

            //Deserialize json to model
            var comments = JsonConvert.DeserializeObject<List<Comment>>(JSONcomments);

            return PartialView(comments.Where(x => x.postId == photoId).ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}