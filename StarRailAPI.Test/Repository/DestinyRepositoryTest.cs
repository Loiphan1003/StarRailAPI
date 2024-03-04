using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Test.Repository
{
    [TestFixture]
    public class DestinyRepositoryTest
    {
        private StarRailContext _dbContext;
        private DestinyRepository _controller;
        private MyHelpers _helpers = new MyHelpers();
        private readonly Supabase.Client? _client;
        private enum ErrorCode
        {
            NotFound,
            AlreadyName,
        };

        [SetUp]
        public async Task SetUp()
        {
            _dbContext = DBContext.GetDBContext();

            _controller = new DestinyRepository(_dbContext, _helpers, _client!);

            // Add Some Sample Data For Test
            var destinies = new List<Destiny>
            {
                new() { Id = 10, Name = "Abundance", Logo = "", LightCones = {} },
                new() { Id = 11, Name = "Nihility", Logo = "", LightCones = {} },
                new() { Id = 12, Name = "The Hunt", Logo = "", LightCones = {} }
            };

            foreach (var item in destinies)
            {
                var existingDestiny = await _dbContext.Destinies.FindAsync(item.Id);
                if (existingDestiny == null)
                {
                    await _dbContext.AddAsync(item);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private static DestinyAdd[] TestCasesAddTrue = {
            new() { Name = "Destruction" },
            new() { Name = "Erudition" },
        };

        private static DestinyAdd[] TestCasesAddFalse = {
            new() { Name = "Abundance" },
            new() { Name = "Nihility" },
        };


        [Test]
        public void GetAll_Should_Return_Value()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThanOrEqualTo(3));
            });
        }

        [Test]
        [TestCase(10)]
        public void Remove_Should_Return_True(int id)
        {
            // Act
            var result = _controller.Remove(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Result, Is.EqualTo("Remove successful"));
            });
        }

        [Test]
        [TestCase(20)]
        public void Remove_Should_Return_False(int id)
        {
            // Act
            var result = _controller.Remove(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Result, Is.EqualTo("Not Found"));
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCasesAddTrue))]
        public async Task Add_Should_Return_True(DestinyAdd destiny)
        {
            // Act
            var result = await _controller.Add(destiny);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.EqualTo("Add destiny successful"));
            });
        }

        [Test]
        [TestCaseSource(nameof(TestCasesAddFalse))]
        public async Task Add_Should_Return_False(DestinyAdd destiny)
        {
            // Act
            var result = await _controller.Add(destiny);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(ErrorCode.AlreadyName.ToString()));
            });
        }

    }
}