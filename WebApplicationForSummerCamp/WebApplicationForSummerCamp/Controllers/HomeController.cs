using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationForSummerCamp.Models;

namespace WebApplicationForSummerCamp.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:61005/";
        public async Task<ActionResult> Index()
        {
            List<Announcement> AnnouncementsList = new List<Announcement>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/announcements");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncemtsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Anouncements list  
                    AnnouncementsList = JsonConvert.DeserializeObject<List<Announcement>>(AnnouncemtsResponse);

                }
                //returning the Anouncements list to view  
                return View(AnnouncementsList);
            }
        }
        //comentariu nou
        /*
        //obtinere datalii anunt
        public async Task<ActionResult> Details(int id)
        {
            Detalii AnnouncementsDetails = new Detalii();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/announcements/"+id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncemtsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Anouncements list  
                    AnnouncementsDetails = JsonConvert.DeserializeObject<Detalii>(AnnouncemtsResponse);

                }
                //returning the Anouncements details to view  
                return View(AnnouncementsDetails);
                
            }
        }
        */



        //postare anunt
        //metoda get ce va aduce view-ul in care completam elemenetele unui nou anunt
        public async Task<ActionResult> ADD()
        {
            //construiesc lista de categorii primind lista de categorii existente folosind metoda Categorii()
            List<Category> lista = await Categorii();
            //creez un obiect anunt nou
            NewAnn obiect = new NewAnn();
           
            
            //atribui listei de categorii din model a obiectului obiect lista de categorii existente
            obiect.CategoriesDataSource = lista;
                return View(obiect);
        }

        //metoda care va face post cu anunutul construit in view
        [HttpPost]
        public async Task<ActionResult> ADD(NewAnn nou)
        {
            string url = Baseurl + "api/announcements/NewAnnouncement";
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync(url, nou);
                if (result.IsSuccessStatusCode)
                {
                    nou = await result.Content.ReadAsAsync<NewAnn>();
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();
                    //daca anuntul a fost postat cu succes utilizatorul ajunge din nou la lista de anunturi
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid data";
                }
            }
            return View(nou);

        }


        //pentru a obtine categoriile
        public async Task<List<Category>> Categorii()
        {
            List<Category> CategoriiList = new List<Category>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/categories");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CategoriiResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the category list  
                    CategoriiList = JsonConvert.DeserializeObject<List<Category>>(CategoriiResponse);

                }
                
                return CategoriiList;
            }
        }


       

        //-------------------------------comentarii--------------------------------
        //Postare comentariu, view corespunzator:PostComment, model:Review

        //metoda get ce va aduce view-ul in care completam elemenetele unui nou anunt
        public ActionResult PostComment(int id)
        {
            return View();
        }

        //metoda care va face post cu comentariul construit in view
        [HttpPost]
        public async Task<ActionResult> PostComment(int id,Review nou)
        {
            nou.AnnouncementId = id;
            string url = Baseurl + "api/reviews/NewReview";
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync(url, nou);
                if (result.IsSuccessStatusCode)
                {
                    nou = await result.Content.ReadAsAsync<Review>();
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();
                    //daca commentul a fost postat cu succes utilizatorul ajunge la lista de comentarii
                    return RedirectToAction("Details/"+id);
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid data";
                }
            }
            return View(nou);

        }


    /*

        //obtinere comentarii

        public async Task<ActionResult> Comments(int id)
        {
            List<Review> CommentsList = new List<Review>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/reviews/GetByAnnouncementId?announcementId=" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncemtsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the comments list  
                    CommentsList = JsonConvert.DeserializeObject<List<Review>>(AnnouncemtsResponse);

                }
                //returning the Anouncements details to view  
                return View(CommentsList);

            }
        }

    */

        

        //------------------------------------extindere--------------------------------------------
        //pentru a extinde un anunt
        //metoda get ce va aduce view-ul in care completam emailul pentru extinderea unui nou anunt
        public ActionResult Extend(int id)
        {
            return View();
        }

        //metoda care va face post 
        [HttpPost]
        public async Task<ActionResult> Extend(EmailConfirm nou,int id)
        {
            string url = Baseurl + "api/announcements/ExtendAnnouncement?announcementId="+id;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync(url, nou);
                if (result.IsSuccessStatusCode)
                {
                    nou = await result.Content.ReadAsAsync<EmailConfirm>();
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();
                    //daca anuntul a fost postat cu succes utilizatorul ajunge din nou la lista de anunturi
                    return RedirectToAction("Details/"+id);
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid email";
                }
            }
            return View(nou);

        }


        //--------------------------------close----------------------------------------------------
        //metoda pentru close anouncement
        //metoda get ce va aduce view-ul in care completam emailul pentru extinderea unui nou anunt
        public ActionResult Close(int id)
        {
            return View();
        }

        //metoda care va face post 
        [HttpPost]
        public async Task<ActionResult> Close(EmailConfirm nou, int id)
        {
            string url = Baseurl + "api/announcements/CloseAnnouncement?announcementId="+id;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = await client.PostAsJsonAsync(url, nou);
                if (result.IsSuccessStatusCode)
                {
                    nou = await result.Content.ReadAsAsync<EmailConfirm>();
                    ViewBag.Result = "Succesfully saved!";
                    ModelState.Clear();
                    //daca anuntul a fost postat cu succes utilizatorul ajunge din nou la lista de anunturi
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Result = "Error!Please try again with valid email";
                }
            }
            return View(nou);

        }


        //----------------------------final detalii anunt si comentarii--------------------------------------
        public async Task<ActionResult> Details(int id)
        {
            //detalii
            Detalii AnnouncementsDetails = new Detalii();

            List<Review> commentsList = new List<Review>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/announcements/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AnnouncemtsResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Anouncements list  
                    AnnouncementsDetails = JsonConvert.DeserializeObject<Detalii>(AnnouncemtsResponse);

                }
               


                //comentarii

                    //Sending request to find web api REST service resource GetAllAnouncenents using HttpClient  
                    HttpResponseMessage Res1 = await client.GetAsync("api/reviews/GetByAnnouncementId?announcementId=" + id);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res1.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var AnnouncemtsResponse = Res1.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the comments list  
                        commentsList = JsonConvert.DeserializeObject<List<Review>>(AnnouncemtsResponse);

                    }
                    //returning the Anouncements comments to view  
                    // return View(CommentsList);
                    AnnouncementsDetails.Comentarii = commentsList;
                    //returning the Anouncements details to view  
                    return View(AnnouncementsDetails);
                }
            }
       

    }
}

