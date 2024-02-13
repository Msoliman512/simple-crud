using Api.DataAccess;
using Api.Test.Wrappers;

namespace Api.Test.DataAccess;

using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

[TestFixture]
public class DbContextTests
{
    private Mock<IConfigurationWrapper> _mockConfigurationWrapper;
    private DbContext _dbContext;
    private IDbConnection _connection;


    [SetUp]
    public void Setup()
    {
        _mockConfigurationWrapper = new Mock<IConfigurationWrapper>();
        _mockConfigurationWrapper.Setup(c => c.GetConnectionString(It.IsAny<string>())).Returns("Data Source=:memory:;Cache=Shared");
        _connection = new SqliteConnection(_mockConfigurationWrapper.Object.GetConnectionString("DefaultConnection"));
        _connection.Open();
       // _dbContext = new DbContext(_mockConfigurationWrapper.Object, _connection);
    }
    
    [TearDown]
    public void TearDown()
    {
        _connection.Close();
    }

    [Test]
    public void CreateConnection_ShouldReturnIDbConnection() //success
    {
        // var result = _dbContext.CreateConnection();
        // Assert.That(result, Is.InstanceOf<IDbConnection>());
    }

    [Test]
    public async Task Init_ShouldCreateDriversTable() // 
    {
        // using (_connection)
        // {
        //     await _dbContext.Init(_connection);
        //
        //     var result = await _connection.QueryFirstOrDefaultAsync<int>(
        //         "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Drivers';");
        //
        //     Assert.That(result, Is.EqualTo(1));
        // }
    }
}