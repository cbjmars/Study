using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core30.WebApi.Sample.Models
{
    // Entitiy Framework로 연결 할 테이블의 컬럼과 유사한 명명 및 데이터타입으로 클래스 생성
    public class TbTestApi
    {
        [Required]
        public int Seq { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime? GreateDt { get; set; }
    }
}
