using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine
{
    public class OrCondition : ICondition
    {
        private IEnumerable<ICondition> _subConditions;

        public OrCondition(IEnumerable<ICondition> subConditions)
        {
            _subConditions = subConditions;
        }

        public bool IsMatch(UserEvent e)
        {
            return _subConditions.Any(r => r.IsMatch(e));
        }
    }
}