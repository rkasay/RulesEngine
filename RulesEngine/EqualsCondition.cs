using System;
using System.Collections.Generic;

namespace RulesEngine
{
    public class EqualsCondition : ICondition
    {
        private string _name;
        private object _value;

        public EqualsCondition(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public bool IsMatch(UserEvent e)
        {
            return e.Attributes.TryGetValue(_name, out object v) && (v.Equals(_value));
        }
    }
}