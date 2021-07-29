using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core30.WebApi.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace Core30.WebApi.Sample.Services
{
    public class TestApiService : ITestApiService
    {
        private readonly TbTestApiContext _context;

        public TestApiService(TbTestApiContext context)
        {
            _context = context;
        }

        public async Task<List<TbTestApi>> GetTestApiList()
        {
            // ToListAsync는 Linq와 비슷한 비동기 메서드이며 DB에서 쿼리를 실행하지 않기 때문에 Where나 OrderBy 등 연산자를 사용할 수 없음 (ToList 의 비동기 버전)
            // : _context(TbTestApiContext)의 TbTestApis 속성을 읽어 TbTestApi 엔터티 집합에서 목록을 가져옴
            return await _context.TbTestApis.ToListAsync();
        }

        public async Task<TbTestApi> GetTestApiItem(int id)
        {
            // HttpGet Edit 매서드는 ID 매개변수를 받아 EF의 FindAsync를 사용하여 TbTestApi를 검색하고 선택된 레코드를 Edit 보기에 반환한다
            return await _context.TbTestApis.FindAsync(id);
        }

        public async Task<int> PutTestApiItem(int id, TbTestApi tbTestApi)
        {
            // DB에 존재하지만 변경이 이루어진 Entity(tbTestApi)가 있는 경우 _context에 Entity를 첨부하고 수정 할 수 있음
            _context.Entry(tbTestApi).State = EntityState.Modified;

            // 비동기 저장
            return await _context.SaveChangesAsync();
        }

        public bool GetTestApiExists(int id)
        {
            // Any는 TbTestApis의 시퀀스(Seq)에 요소(id)가 존재하는지 확인
            return _context.TbTestApis.Any(e => e.Seq == id);
        }

        public async Task<int> PostTestApi(TbTestApi tbTestApi)
        {
            // Entity(tbTestApi) 새 인스턴스를 추가.
            _context.TbTestApis.Add(tbTestApi);
            return await _context.SaveChangesAsync();
        }

        public async Task<TbTestApi> DeleteTestApiItem(int id)
        {
            var tbTestApi = await _context.TbTestApis.FindAsync(id);
            if (tbTestApi != null)
            {
                // 지정된 Entity 삭제
                _context.TbTestApis.Remove(tbTestApi);
                await _context.SaveChangesAsync();
            }
            return tbTestApi;
        }
    }
}
