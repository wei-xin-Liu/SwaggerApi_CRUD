using Member.Models.Member;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Xml.Linq;


public class SwaggerApiController : Controller
{
    private readonly ApiService _apiService;

    public SwaggerApiController(ApiService apiService)
    {
        _apiService = apiService;
    }

    // POST SwaggerApi api/Member/GetList 帶入空字串參數 初始頁面載入讀取所有資料
    //                                    搜尋有字串     顯示含有關鍵字資料
    public async Task<IActionResult> Index(string keyword)
    {
        const string baseUrl = "https://exam-api.deta-it.com.tw";

        if (await _apiService.AuthenticateAsync(baseUrl))
        {
            object data = keyword == null ? new { keyword = "" } : new { keyword = $"{keyword}" };
            var result = await _apiService.CallApiAsync($"{baseUrl}/api/Member/GetList", HttpMethod.Post, data);
            return View(JsonConvert.DeserializeObject<List<MemberGetListResp>>(result));
        }
        else
        {
            return View("身分驗證失敗");
        }
    }

    // GET: Member/Create 單純切換換面顯示Create頁面
    public IActionResult Create()
    {
        return View();
    }


    // POST: Member/Create 新增一筆資料 或 更新資料 並跳轉至首頁 
    [HttpPost]
    public async Task<IActionResult> Create(MemberSetReq MemberSetReq)
    {
        const string baseUrl = "https://exam-api.deta-it.com.tw";

        // 原本是每次都去抓取Token，現在因為進到畫面就將token直接存在header中，所以改成去抓token驗證即可
        if (!_apiService.IsTokenValid())
        {
            if (!await _apiService.ValidateTokenAsync(baseUrl))
            {
                if (!await _apiService.AuthenticateAsync(baseUrl))
                {
                    throw new UnauthorizedAccessException("無法驗證身份");
                }
            }
        }
       
        //if (await _apiService.AuthenticateAsync(baseUrl))
        //{
        object data = new
        {
            pk = $"{MemberSetReq.pk}",
            id = $"{MemberSetReq.id}",
            pwd = $"{MemberSetReq.pwd}",
            name = $"{MemberSetReq.name}",
            gender = $"{MemberSetReq.gender}",
            birthday = $"{MemberSetReq.birthday}",
            remark = $"{MemberSetReq.remark}",
            enable = $"{MemberSetReq.enable}",
        };
        await _apiService.CallApiAsync($"{baseUrl}/api/Member/Set", HttpMethod.Post, data);
        return RedirectToAction(nameof(Index));  
        //}
        //else
        //{
        //    return View("身分驗證失敗");
        //}
    }

    // Get: Member/Details 讀取任何一筆指定Member資料 跳轉到Detail 畫面
    public async Task<IActionResult> Details(string? id)
    {
        const string baseUrl = "https://exam-api.deta-it.com.tw";

        // 原本是每次都去抓取Token，現在因為進到畫面就將token直接存在header中，所以改成去抓token驗證即可
        if (!_apiService.IsTokenValid())
        {
            if (!await _apiService.ValidateTokenAsync(baseUrl))
            {
                if (!await _apiService.AuthenticateAsync(baseUrl))
                {
                    throw new UnauthorizedAccessException("無法驗證身份");
                }
            }
        }

        //if (await _apiService.AuthenticateAsync(baseUrl))
        //{
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            object data = new
            {
                pk = $"{id}"
            };
            var result = await _apiService.CallApiAsync($"{baseUrl}/api/Member/Get", HttpMethod.Post, data);
            return View(JsonConvert.DeserializeObject<MemberGetResp>(result));
        }
        //}
        //else
        //{
        //    return View("身分驗證失敗");
        //}
    }

    // Get: Member/Edit 讀取任何一筆指定Member資料 跳轉到 Edit 畫面
    public async Task<IActionResult> Edit(string? id)
    {
        const string baseUrl = "https://exam-api.deta-it.com.tw";

        // 原本是每次都去抓取Token，現在因為進到畫面就將token直接存在header中，所以改成去抓token驗證即可
        if (!_apiService.IsTokenValid())
        {
            if (!await _apiService.ValidateTokenAsync(baseUrl))
            {
                if (!await _apiService.AuthenticateAsync(baseUrl))
                {
                    throw new UnauthorizedAccessException("無法驗證身份");
                }
            }
        }

        //if (await _apiService.AuthenticateAsync(baseUrl))
        //{
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            object data = new
            {
                pk = $"{id}"
            };
            var result = await _apiService.CallApiAsync($"{baseUrl}/api/Member/Get", HttpMethod.Post, data);
            return View(JsonConvert.DeserializeObject<MemberGetResp>(result));
        }
        //}
        //else
        //{
        //    return View("身分驗證失敗");
        //}
    }


    // Get: Member/Delete 刪除一筆指定Member資料
    public async Task<IActionResult> Delete(string? id)
    {
        const string baseUrl = "https://exam-api.deta-it.com.tw";

        // 原本是每次都去抓取Token，現在因為進到畫面就將token直接存在header中，所以改成去抓token驗證即可
        if (!_apiService.IsTokenValid())
        {
            if (!await _apiService.ValidateTokenAsync(baseUrl))
            {
                if (!await _apiService.AuthenticateAsync(baseUrl))
                {
                    throw new UnauthorizedAccessException("無法驗證身份");
                }
            }
        }

        //if (await _apiService.AuthenticateAsync(baseUrl))
        //{
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            object data = new
            {
                pk = $"{id}"
            };
            var result = await _apiService.CallApiAsync($"{baseUrl}/api/Member/Delete", HttpMethod.Post, data);
            return RedirectToAction(nameof(Index));
        }
        //}
        //else
        //{
        //    return View("身分驗證失敗");
        //}
    }
}