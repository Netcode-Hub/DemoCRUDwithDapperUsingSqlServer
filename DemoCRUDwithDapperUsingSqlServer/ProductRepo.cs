using Dapper;
using System.Data;

namespace DemoCRUDwithDapperUsingSqlServer
{
    public class ProductRepo(IDbConnection _dbConnection) : IProduct
    {
        public async Task<int> AddProductAsync(Product product)
        {
            var query = "INSERT INTO Product (Name, Price) VALUES (@Name, @Price)";
            return await _dbConnection.ExecuteAsync(query, product);
        }

        public async  Task<int> DeleteProductAsync(int id)
        {
            var query = "DELETE FROM Product WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = "SELECT * FROM Product";
            return await _dbConnection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var query = "SELECT * FROM Product WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var query = "UPDATE Product SET Name = @Name, Price = @Price WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, product);
        }
    }
}
