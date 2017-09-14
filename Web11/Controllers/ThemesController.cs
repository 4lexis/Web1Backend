using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Web11.Models;
using Web11.Models.Core;

namespace Web11.Controllers
{
    [RoutePrefix("api/themes")]
    public class ThemesController : ApiController
    {
        public const string ServerLocalHost = "http://localhost:1172";
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/Themes
        public IQueryable<Theme> GetThemes()
        {
            return db.Themes;
        }

        [EnableQuery]
        // GET: api/Themes/5
        [ResponseType(typeof(Theme))]
        public IHttpActionResult GetTheme(int id)
        {
            Theme theme = db.Themes.Find(id);
            if (theme == null)
            {
                return NotFound();
            }

            return Ok(theme);
        }

        
        // PUT: api/Themes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTheme(int id, Theme theme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theme.Id)
            {
                return BadRequest();
            }

            db.Entry(theme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThemeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Themes
        [HttpPost]
        [Route("posttheme", Name ="TypeApi")]
        [ResponseType(typeof(Theme))]
        public IHttpActionResult PostTheme(Theme theme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Themes.Add(theme);
            db.SaveChanges();

            return CreatedAtRoute("TypeApi", new { id = theme.Id }, theme);
        }

        // DELETE: api/Themes/5
        [ResponseType(typeof(Theme))]
        public IHttpActionResult DeleteTheme(int id)
        {
            Theme theme = db.Themes.Find(id);
            if (theme == null)
            {
                return NotFound();
            }

            db.Themes.Remove(theme);
            db.SaveChanges();

            return Ok(theme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThemeExists(int id)
        {
            return db.Themes.Count(e => e.Id == id) > 0;
        }

        [HttpPost]
        [Route("image/upload/")]        
        public async Task<HttpResponseMessage> PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var myUniqueFileName = Guid.NewGuid().ToString().Substring(0, 8);
                            var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/" + myUniqueFileName + extension);
                            var relativePath = ServerLocalHost + "/Content/Images/" + myUniqueFileName + extension;
                            
                            postedFile.SaveAs(filePath);
                            var message1 = relativePath;
                            return Request.CreateResponse(HttpStatusCode.Created, message1); 
                        }
                    }
                    
                    
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }       
    }
}