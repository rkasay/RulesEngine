using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine
{
    public class AndCondition : ICondition
    {
        private IEnumerable<ICondition> _subConditions;

        public AndCondition(IEnumerable<ICondition> subConditions)
        {
            _subConditions = subConditions;
        }

        public bool IsMatch(UserEvent e)
        {
            return !(_subConditions.Any(r => !r.IsMatch(e)));
        }
    }
}