using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Controllers;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Test.Controller
{
    [TestFixture]
    public class CharacterControllerTest
    {
        private ICharacterRepository _characterRepository;
        private CharacterController _controller;

        [SetUp]
        public void SetUp()
        {
            _characterRepository = A.Fake<ICharacterRepository>();

            _controller = new CharacterController(_characterRepository);
        }

        [Test]
        public void Get_Return_OK()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkObjectResult>());
            });
        }
    }
}