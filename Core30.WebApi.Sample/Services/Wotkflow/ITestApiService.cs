using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core30.WebApi.Sample.Models;

namespace Core30.WebApi.Sample.Services
{
    public interface ITestApiService
    {
        Task<List<TbTestApi>> GetTestApiList();
        Task<TbTestApi> GetTestApiItem(int id);
        Task<int> PutTestApiItem(int id, TbTestApi tbTestApi);
        bool GetTestApiExists(int id);
        Task<int> PostTestApi(TbTestApi tbTestApi);
        Task<TbTestApi> DeleteTestApiItem(int id);
    }
}
