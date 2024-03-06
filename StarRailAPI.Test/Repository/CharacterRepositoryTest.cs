using Microsoft.EntityFrameworkCore;
using StarRailAPI.Data;
using StarRailAPI.Helpers;
using StarRailAPI.Models.Class;
using StarRailAPI.Service.Repositories;
using Supabase;

namespace StarRailAPI.Test.Repository
{
    [TestFixture]
    public class CharacterRepositoryTest
    {
        private StarRailContext _context;
        private CharacterRepository _controller;
        private MyHelpers _helpers;
        private readonly Client? _client;
        private enum ErrorCode
        {
            NotFound,
            AlreadyName,
        };

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _context = DBContext.GetDBContext();

            _helpers = new MyHelpers();

            _controller = new CharacterRepository(_context, _helpers, _client!);

            // Add Some Sample Data For Test
            var characters = new List<Character>
            {
                new() { Id = 1, Name = "Black Swan", Avatar = "", DestinyId  = 11, SystemDataId = 3 },
                new() { Id = 2, Name = "Blade", Avatar = "", DestinyId  = 11, SystemDataId = 3 },
                new() { Id = 3, Name = "Kafka", Avatar = "", DestinyId  = 11, SystemDataId = 3 },
            };

            foreach (var item in characters)
            {
                var existingCharacter = await _context.Characters.FindAsync(item.Id);
                if (existingCharacter == null)
                {
                    _context.Add(item);
                }
            }

            var destiny = new Destiny
            {
                Name = "Harmony",
            };

            var system = new SystemData
            {
                Name = "Ice"
            };

            _context.Add(destiny);
            _context.Add(system);

            _context.SaveChanges();
        }

        [Test]
        public void GetAll_Should_Return_Value()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.GreaterThanOrEqualTo(1));
            });
        }

        #region Test Cases For Add Method
        private static CharacterAdd[] TestCasesAddTrue = {
            new() { Name = "Ruan Mei", DestinyName = "Harmony", SystemName = "Ice" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesAddTrue))]
        public async Task Add_Should_Return_True(CharacterAdd character)
        {
            // Act
            var result = await _controller.Add(character);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
            });
        }

        private static CharacterAdd[] TestCasesForDuplicateName = {
            new() { Name = "Kafka", DestinyName = "Nihility", SystemName = "LightNing" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesForDuplicateName))]
        public async Task Add_Duplicate_Name_Should_Return_Failure(CharacterAdd character)
        {
            // Act
            var result = await _controller.Add(character);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(ErrorCode.AlreadyName.ToString()));
            });
        }

        private static CharacterAdd[] TestCasesForFailureSolution = {
            new() { Name = "Clara", DestinyName = "Nihility", SystemName = "LightNing" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesForFailureSolution))]
        public async Task Add_NotFound_Destiny_Should_Return_True(CharacterAdd character)
        {
            // Act
            var result = await _controller.Add(character);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(ErrorCode.NotFound.ToString()));
                Assert.That(result.Error.Description, Is.EqualTo($"The Destiny {character.DestinyName} not found"));
            });
        }

        #endregion



        [Test]
        [TestCase("Blade")]
        public async Task Remove_Should_Return_True(string name)
        {
            // Act
            var result = await _controller.Remove(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsSuccess, Is.True);
            });
        }

        [Test]
        [TestCase("Lynx")]
        public async Task Remove_Should_Return_NotFound(string name)
        {
            // Act
            var result = await _controller.Remove(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsFailure, Is.True);
            });
        }

        private static CharacterUpdate[] TestCasesUpdateTrue = {
            new() { Id = 1, Name = "Sparkle", DestinyName = "Harmony", SystemName = "Ice" },
        };

        [Test]
        [TestCaseSource(nameof(TestCasesUpdateTrue))]
        public async Task Update_Should_Return_Successful(CharacterUpdate model)
        {
            // Act
            var result = await _controller.Update(model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
            });

        }
    }
}