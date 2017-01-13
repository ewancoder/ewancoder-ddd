namespace Ewancoder.DDD.EFEventStore.Tests
{
    using Moq;
    using Xunit;
    using System.Runtime.Serialization;

    public sealed class SerializerTests
    {
        [Fact]
        public void ShouldSerializeAndDeserialize()
        {
            var factory = new Mock<ITypeFactory>();
            var sut = new Serializer(factory.Object);
            factory.Setup(f => f.Resolve("testSerializable"))
                .Returns(typeof(TestSerializable));

            var bytes = sut.Serialize(new TestSerializable("name", 10, 22));
            Assert.NotEmpty(bytes);

            var testSerializable = (TestSerializable)sut.Deserialize(
                "testSerializable", bytes);

            Assert.Equal("name", testSerializable.Name);
            Assert.Equal(10, testSerializable.Age);
            Assert.Equal(default(long), testSerializable.NotSerialized);
        }
    }

    [DataContract]
    public class TestSerializable
    {
        private TestSerializable() { }
        public TestSerializable(string name, int age, long notSerialized)
        {
            Name = name;
            Age = age;
            NotSerialized = notSerialized;
        }

        [DataMember(Order = 2)]
        public string Name { get; private set; }

        [DataMember(Order = 5)]
        public int Age { get; private set; }

        public long NotSerialized { get; private set; }
    }
}
