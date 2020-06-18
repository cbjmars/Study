using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core30.WebApi.Sample.Models
{
    //DbContext는 데이터모델에 맞게 Entity Framework를 사용하는 주 클래스 입니다.
    // Microsoft.EntityFrameworkCore.DbContext 클래스에서 파생시키는 방식으로 클래스를 생성합니다.
    public class TbTestApiContext : DbContext
    {
        //DbContext를 상속받고, base를 통해서 생성자의 부모 클래스 호출을 위해 base를 사용한다.
        //응용프로그램이 Context를 인스턴스화(객체화) 할 때 DbContext를 전달 받음
        public TbTestApiContext(DbContextOptions<TbTestApiContext> options)
            : base(options)
        { 
        }

        //DB의 테이블에 해당하는 것으로 필요한 만큼 DbSet을 지정할 수 있다. (테이블의 컬럼 정의는 DbSet<T>의 "T" 에 정의 됨)
        public DbSet<TbTestApi> TbTestApis { get; set; }

        //Context에서 OnModelCreating 메서드를 재정의하고 ModelBuilder API를 사용하여 모델을 구성할 수 있습니다.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API(흐름 API)에 의한 Entity 구성 (Fluent API란 함수들을 작성하고 나면, 마치 그 문장이 영어 문장처럼 읽히는 API)
            modelBuilder.Entity<TbTestApi>(entity =>
            {
                // Primary key 적용
                entity.HasKey(e => e.Seq).HasName("PK__TB_TEST_API");
                // entity가 mapping 해야 할 테이블명 지정
                entity.ToTable("TB_TEST_API");
                // Column 및 속성 구성
                entity.Property<int>(e => e.Seq)
                .HasColumnName("SEQ");
                entity.Property(e => e.Subject)
                .HasColumnName("SUBJECT")
                .HasMaxLength(100);
                entity.Property(e => e.Content)
                .HasColumnName("CONTENT")
                .HasMaxLength(1000);
                entity.Property(e => e.GreateDt)
                .HasColumnName("CREATE_DT")
                .HasColumnType("datetime");
            });
        }
    }
}
