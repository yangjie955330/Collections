using System.Data.SqlClient;
using DotNetCore.Collections.Paginable.DbTests.Models;
using Shouldly;
using SqlKata.Compilers;
using SqlKata.Execution;
using Xunit;

namespace DotNetCore.Collections.Paginable.DbTests
{
    public class UnitTest1
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Development\Collections\tests\DotNetCore.Collections.Paginable.DbTests\DataSource\Samples.mdf;Integrated Security=True";

        [Fact]
        public void Test1()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var compiler = new SqlServerCompiler();
                var db = new QueryFactory(connection, compiler);

                var page = db.Query("Int32Samples").GetPage<Int32Sample>(1, 9);
                page.TotalPageCount.ShouldBe(24);
                page.TotalMemberCount.ShouldBe(210);
                page.CurrentPageNumber.ShouldBe(1);
                page.PageSize.ShouldBe(9);
                page.CurrentPageSize.ShouldBe(9);
                page.HasNext.ShouldBeTrue();
                page.HasPrevious.ShouldBeFalse();

                connection.Close();
            }
        }
    }

   
}
