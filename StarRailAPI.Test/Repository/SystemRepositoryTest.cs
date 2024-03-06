using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;
using Supabase;

namespace StarRailAPI.Test.Repository
{
    [TestFixture]
    public class SystemRepositoryTest
    {
        private StarRailContext _context;
        private SystemRepository _controller;
        private MyHelpers _helpers;
        private readonly Client? _client;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _context = DBContext.GetDBContext();

            _helpers = new MyHelpers();

            _controller = new SystemRepository(_context, _client!, _helpers);

            // Add Data Sample
            var systems = new List<SystemData>
            {
                new() { Id = 1, Name = "Imaginary"},
                new() { Id = 2, Name = "Fire"},
                new() { Id = 3, Name = "Wind"},
            };

            foreach (var item in systems)
            {
                var existingSystem = await _context.Systems.FindAsync(item.Id);

                if (existingSystem == null)
                {
                    _context.Add(item);
                }

            }

            _context.SaveChanges();
        }

        [Test]
        public void GetAll_Should_Return_Items()
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
        [TestCase("Quantum")]
        public async Task Add_Should_Return_True(string name)
        {
            // Arrange
            SystemAdd add = new()
            {
                Name = name
            };

            // Act
            var result = await _controller.Add(add);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
            });
        }

        [Test]
        [TestCase("Fire")]
        public async Task Add_Duplicate_Name_Should_Return_False(string name)
        {
            // Arrange
            SystemAdd add = new()
            {
                Name = name
            };

            // Act
            var result = await _controller.Add(add);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsFailure, Is.True);
            });
        }

        [Test]
        [TestCase(1)]
        public async Task Remove_Should_Return_True(int id)
        {
            // Act
            var result = await _controller.Remove(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.EqualTo("Remove system successful"));
            });
        }

        [Test]
        [TestCase(9)]
        public async Task Remove_Should_Return_False(int id)
        {
            // Act
            var result = await _controller.Remove(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Error.Code, Is.EqualTo("NotFound"));
                Assert.That(result.Error.Description, Is.EqualTo($"The system {id} not found"));
            });
        }

        private static SystemUpdate[] TestCasesUpdateTrue = {
            new SystemUpdate { Id = 3, Name = "Lightning" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesUpdateTrue))]
        public async Task Update_Should_Return_True(SystemUpdate model)
        {
            // Act
            var result = await _controller.Update(model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.EqualTo("Update Successful"));
            });
        }

        private static SystemUpdate[] TestCasesUpdateFalse = {
            new SystemUpdate { Id = 7, Name = "Lightning" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesUpdateFalse))]
        public async Task Update_Should_Return_False(SystemUpdate model)
        {
            // Act
            var result = await _controller.Update(model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo("NotFound"));
            });
        }
    }
}