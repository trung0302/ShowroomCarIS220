using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.Auth;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTForm;
using ShowroomCarIS220.DTO.Car;
using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.DTO.New;
using ShowroomCarIS220.Models;
using ShowroomCarIS220.Response;
using ShowroomCarIS220.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ShowroomCarIS220.Controllers
{
    [Route("news")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _db;
        public NewsController(DataContext db)
        {
            _db = db;
        }
        private Authentication _auth = new Authentication();

        //GetAllNews
        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<NewsResponse<List<GetNews>>>> getNews([FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            int pageResults = (pageSize != null) ? (int)pageSize : 2;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;

            var newsResponse = new NewsResponse<List<GetNews>>();
            var listNews = new List<GetNews>();
            try
            {
                if (pageIndex != null)
                {
                    var news = await _db.News
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();

                    foreach (var item in news)
                    {
                        var getNews = new GetNews
                        {
                            id = item.id,
                            author = item.author,
                            title=item.title,
                            image=item.image,
                            description=item.description,
                            dateSource=item.dateSource,
                            detail=item.detail,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt
                        };
                        listNews.Add(getNews);
                    }
                    newsResponse.News = listNews.ToList();
                    newsResponse.totalNews = _db.News.ToList().Count();
                }
                else
                {
                    var news = await _db.News
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();

                    foreach (var item in news)
                    {
                        var getNews = new GetNews
                        {
                            id = item.id,
                            author = item.author,
                            title = item.title,
                            image = item.image,
                            description = item.description,
                            dateSource = item.dateSource,
                            detail = item.detail,
                            createdAt = item.createdAt,
                            updatedAt = item.updatedAt
                        };
                        listNews.Add(getNews);
                    }
                    newsResponse.News = listNews.ToList();
                    newsResponse.totalNews = _db.News.ToList().Count();
                }
                return StatusCode(StatusCodes.Status200OK, newsResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //GetNewsById
        [HttpGet("{id:Guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<NewsResponse<News>>> getNewsById([FromRoute] Guid id)
        {
            var newsResponse = new NewsResponse<News>();
            try
            {
                var news = await _db.News.FindAsync(id);
                if (news != null)
                {
                    newsResponse.News = news;
                    newsResponse.totalNews = _db.Car.ToList().Count();
                    return StatusCode(StatusCodes.Status200OK, newsResponse);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }


        //RemoveNewsByID
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<NewsResponse<List<News>>>> removeNewsById([FromRoute] Guid id, [FromHeader] string Authorization)
        {
            var newsResponse = new NewsResponse<List<News>>();
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var news = await _db.News.FindAsync(id);
                if (news != null)
                {
                    _db.News.Remove(news);
                    await _db.SaveChangesAsync();
                    newsResponse.News = _db.News.ToList();
                    newsResponse.totalNews = _db.News.ToList().Count();

                    return StatusCode(StatusCodes.Status200OK, news);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //AddNews
        [HttpPost]
        public async Task<ActionResult<NewsResponse<News>>> addNews(AddNews addNewsDTO, [FromHeader] string Authorization)
        {
            var newsResponse = new NewsResponse<List<News>>();
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var news = new News()
                {
                    id = Guid.NewGuid(),
                    author = addNewsDTO.author,
                    title = addNewsDTO.title,
                    image = addNewsDTO.image,
                    description = addNewsDTO.description,
                    dateSource = addNewsDTO.dateSource,
                    detail = addNewsDTO.detail,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };

                await _db.News.AddAsync(news);
                await _db.SaveChangesAsync();
                newsResponse.News = _db.News.ToList();
                newsResponse.totalNews = newsResponse.News.Count();
                return StatusCode(StatusCodes.Status200OK, newsResponse.News);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //UpdateNews
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> updateNews([FromRoute] Guid id, UpdateNews updateNewsDTO, [FromHeader] string Authorization)
        {
            try
            {
                var checkToken = _auth.CheckTokenLogout(Authorization.Substring(7), _db);
                if (checkToken == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Please authenticate");
                }
                var news = await _db.News.FindAsync(id);

                if (news != null)
                {
                    news.author = updateNewsDTO.author;
                    news.title = updateNewsDTO.title;
                    news.image = updateNewsDTO.image;
                    news.description = updateNewsDTO.description;
                    news.dateSource = updateNewsDTO.dateSource;
                    news.detail = updateNewsDTO.detail;
                    news.createdAt = DateTime.Now;
                    news.updatedAt = DateTime.Now;

                    await _db.SaveChangesAsync();

                    var getNews = new GetNews
                    {
                        id = news.id,
                        author = updateNewsDTO.author,
                        title = updateNewsDTO.title,
                        image = updateNewsDTO.image,
                        description = updateNewsDTO.description,
                        dateSource = updateNewsDTO.dateSource,
                        detail = updateNewsDTO.detail,
                        createdAt = DateTime.Now,
                        updatedAt = DateTime.Now,
                    };

                    return StatusCode(StatusCodes.Status200OK, getNews);
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, "Not found");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }
    }
}

