using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services;

public sealed class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;
    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;

        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }

    public async Task<Response<NoContent>> DeleteAsync(int id)
    {
        int deleteStatus = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { id });
        if (deleteStatus > 0)
            return Response<NoContent>.Success(204);
        return Response<NoContent>.Fail("An error occurred while deleting", 500);
    }

    public async Task<Response<List<Models.Discount>>> GetAllAsync()
    {
        IEnumerable<Models.Discount> discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
        return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        Models.Discount discount = await _dbConnection.QueryFirstOrDefaultAsync("select * from discount where code=@Code and userid=@UserId"
            , new { Code = code, UserId = userId });
        if (discount == null)
            return Response<Models.Discount>.Fail("Discount is not found", 404);
        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<Models.Discount>> GetByIdAsync(int id)
    {
        Models.Discount discount = await _dbConnection.QueryFirstOrDefaultAsync<Models.Discount>("select * from discount where id=@Id", new { id });
        if (discount == null)
            return Response<Models.Discount>.Fail("Discount not found", 404);
        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> SaveAsync(Models.Discount discount)
    {
        int saveStatus = await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code)values(@UserId,@Rate,@Code)", discount);
        if (saveStatus > 0)
            return Response<NoContent>.Success(204);
        return Response<NoContent>.Fail("An error occurred while adding", 500);
    }

    public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
    {
        int updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id", discount);
        if (updateStatus > 0)
            return Response<NoContent>.Success(204);
        return Response<NoContent>.Fail("An error occurred while updating", 500);
    }
}
