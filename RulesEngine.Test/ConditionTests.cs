using System.Collections.Generic;
using NUnit.Framework;

namespace RulesEngine.Test
{
    public class ConditionTests
    {
        UserEvent _event = null;
        bool _isMatch = false;
        Action _onMatchAction;

        [SetUp]
        public void Setup()
        {
            var attributes = new Dictionary<string, object>
            {
                { "favorite year", 1984 },
                { "pet owner", true },
                { "favorite number", 123 },
                { "first name", "Alice"},
                { "prefers tundra", false },
                { "position", "president" },
                { "favorite color", "green" }
            };
            _event = new UserEvent { Attributes = attributes };
            _isMatch = false;
            // When condition is matched action is to set _isMatch to signal
            // back to test.
            _onMatchAction = (e) => _isMatch = true;
        }

        [TestCase("favorite year", 1984, "first name", "Alice", true)]
        [TestCase("pet owner", false, "first name", "Joe", true)]
        [TestCase("pet owner", true, "first name", "Alice", true)]
        [TestCase("pet owner", false, "first name", "Alice", false)]
        [TestCase("bogus attribute", false, "pet owner", true, false)]
        [TestCase("position", "CFO", "bogus attribute", 17, true)]
        public void OrConditionTest(
            string attr1,
            object value1,
            string attr2,
            object value2,
            bool   expectedMatch)
        {
            var condition1 = new EqualsCondition(attr1, value1);
            var condition2 = new NotEqualsCondition(attr2, value2);
            ICondition[] subConditions = { condition1, condition2 };
            var or = new OrCondition(subConditions);

            var rule = new Rule(or, _onMatchAction);
            rule.Execute(_event);
            Assert.AreEqual(expectedMatch, _isMatch);
        }

        [TestCase("favorite year", 1984, "first name", "Alice", false)]
        [TestCase("pet owner", false, "first name", "Joe", false)]
        [TestCase("pet owner", true, "first name", "doug", true)]
        [TestCase("bogus attribute", false, "pet owner", true, false)]
        [TestCase("position", "president", "bogus attribute", 17, true)]
        public void AndConditionTest(
            string attr1,
            object value1,
            string attr2,
            object value2,
            bool expectedMatch)
        {
            var condition1 = new EqualsCondition(attr1, value1);
            var condition2 = new NotEqualsCondition(attr2, value2);
            ICondition[] subConditions = { condition1, condition2 };
            var or = new AndCondition(subConditions);

            var rule = new Rule(or, _onMatchAction);
            rule.Execute(_event);
            Assert.AreEqual(expectedMatch, _isMatch);
        }

        [TestCase("favorite color", "green", true)]
        [TestCase("favorite color", "blue", false)]
        [TestCase("bogus attribute", "blue", false)]
        [TestCase("favorite color", null, false)]
        [TestCase("favorite number", 123, true)]
        [TestCase("favorite number", "123", false)]
        [TestCase("pet owner", true, true)]
        [TestCase("pet owner", false, false)]
        public void EqualConditionTest(
            string attributeName,
            object expectedValue,
            bool   expectedMatch)
        {
            // test EqualsCondition
            var condition = new EqualsCondition(attributeName, expectedValue);
            var rule = new Rule(condition, _onMatchAction);
            rule.Execute(_event);
            Assert.AreEqual(expectedMatch, _isMatch);

            // now reverse expectations and test NotEqualsCondition
            var oppositeCondition = new NotEqualsCondition(attributeName, expectedValue);
            // reset, just in case
            _isMatch = false;
            rule = new Rule(oppositeCondition, _onMatchAction);
            rule.Execute(_event);
            Assert.AreEqual(!expectedMatch, _isMatch);
        }
    }
}
