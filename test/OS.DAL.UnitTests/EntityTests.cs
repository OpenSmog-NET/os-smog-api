using Shouldly;
using Xunit;

namespace OS.DAL.UnitTests
{
    public class EntityTests
    {
        public class GivenEntityEquals
        {
            [Fact]
            public void WhenEqualsSameEntity_ShouldBeTrue()
            {
                var entity = TestEntity.Create(5);

                entity.Equals(entity).ShouldBe(true);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithSameId_ShouldBeTrue()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(5);

                e1.Equals(e2).ShouldBe(true);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithDifferentId_ShouldBeFalse()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(6);

                e1.Equals(e2).ShouldBe(false);
            }
        }

        public class GivenEqualityOperator
        {
            [Fact]
            public void WhenEqualsSameEntity_ShouldBeTrue()
            {
                var entity = TestEntity.Create(5);

                (entity == entity).ShouldBe(true);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithSameId_ShouldBeTrue()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(5);

                (e1 == e2).ShouldBe(true);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithDifferentId_ShouldBeFalse()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(6);

                (e1 == e2).ShouldBe(false);
            }
        }

        public class GivenNegativeEqualityOperator
        {
            [Fact]
            public void WhenEqualsSameEntity_ShouldBeFalse()
            {
                var entity = TestEntity.Create(5);

                (entity != entity).ShouldBe(false);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithSameId_ShouldBeFalse()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(5);

                (e1 != e2).ShouldBe(false);
            }

            [Fact]
            public void WhenTwoDifferentEntitiesWithDifferentId_ShouldBeTrue()
            {
                var e1 = TestEntity.Create(5);
                var e2 = TestEntity.Create(6);

                (e1 != e2).ShouldBe(true);
            }
        }
    }

    public class TestEntity : Entity<int>
    {
        public static TestEntity Create(int id)
        {
            return new TestEntity { Id = id };
        }
    }
}