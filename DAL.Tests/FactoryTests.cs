using NUnit.Framework;

namespace DAL.Tests
{
    [TestFixture]
    public class FactoryTests
    {
        private Factory uut;

        [Test]
        public void NoConstructor_GetInstance_DoesNotReturnNull()
        {
            Assert.That(Factory.Instance, Is.Not.Null);
        }

        [Test]
        public void NoConstructor_GetInstanceTwice_ReturnsSameInstance()
        {
            var instance = Factory.Instance;

            Assert.That(Factory.Instance, Is.EqualTo(instance));
        }

        [Test]
        public void Instance_GetUnitOfWork_DoesNotReturnNull()
        {
            Assert.That(Factory.Instance.GetUOF(), Is.Not.Null);
        }

        [Test]
        public void Instance_GetUnitOfWorkTwice_ReturnsDifferentUnits()
        {
            var instance = Factory.Instance.GetUOF();

            Assert.That(Factory.Instance.GetUOF(), Is.Not.EqualTo(instance));
        }
    }
}
