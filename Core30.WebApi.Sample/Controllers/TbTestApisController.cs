using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core30.WebApi.Sample.Models;
using Core30.WebApi.Sample.Services;

namespace Core30.WebApi.Sample.Controllers
{    
    [ApiController] //[ApiController] 특성을 특정 컨트롤러에 적용 - 아래와 [Route("api/[controller]")] 같이 특성 라우팅을 요구
    [Route("api/[controller]")]//컨트롤러 또는 작업의 개별 URL 패턴을 지정합니다
    public class TbTestApisController : ControllerBase
    {
        private readonly ITestApiService _testApiService;

        public TbTestApisController(ITestApiService testApiService)
        {
            _testApiService = testApiService;
        }

        // GET: api/TbTestApis
        // ActionResult<T>는 반환 형식이며 여기서 사용중인 Task<ActionResult<T>>는 비동기 반환 형식이다.
        // IEnumerable<T> 지원된 형식의 컬렉션(Serializer에 의한 동기 컬렉션)을 단순하게 반복할 수 있도록 지원하는 열거자
        // : TbTestApi List를 열거하여 비동기로 반환하는 메서드
        [HttpGet]//HTTP GET 동작 식별
        public async Task<ActionResult<IEnumerable<TbTestApi>>> GetTbTestApis()
        {
            var result = await _testApiService.GetTestApiList();
            return result;
        }

        // GET: api/TbTestApis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbTestApi>> GetTbTestApi(int id)
        {
            var result = await _testApiService.GetTestApiItem(id);

            if (result == null)
            {
                // 404 (요청 결과 찾을 수 없음)
                return NotFound();
            }

            return result;
        }

        // PUT: api/TbTestApis/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        // IActionResult는 반환 형식이 여러개의 작업인 경우 사용하며 수행된 작업의 결과에 따라 서로 다른 HTTP 상태 코드를 반환한다
        // 여기서 사용중인 Task<IActionResult>는 비동기 반환 형식이다.  
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbTestApi(int id, TbTestApi tbTestApi)
        {
            // tbTestApi.Seq에 요청 id가 없는 경우
            if (id != tbTestApi.Seq)
            {
                // 400 (요청사항 인식 오류)
                return BadRequest();
            }
            
            try
            {
                await _testApiService.PutTestApiItem(id, tbTestApi);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_testApiService.GetTestApiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // 202 (요청은 접수했지만 아직 처리하지 않음)
            return NoContent();
        }

        // POST: api/TbTestApis
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TbTestApi>> PostTbTestApi(TbTestApi tbTestApi)
        {
            await _testApiService.PostTestApi(tbTestApi);
            // 201(요청받은 작업을 성공함)
            return CreatedAtAction("GetTbTestApi", new { id = tbTestApi.Seq }, tbTestApi);
        }

        // DELETE: api/TbTestApis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbTestApi>> DeleteTbTestApi(int id)
        {
            var tbTestApi = await _testApiService.DeleteTestApiItem(id);
            if (tbTestApi == null)
            {
                return NotFound();
            }

            return tbTestApi;
        }
    }
}
