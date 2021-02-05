using System;
namespace RulesEngine
{
    public delegate void Action(UserEvent e);

    public class Rule
    {
        private ICondition _condition;
        private Action _action;

        public Rule(ICondition condition, Action action)
        {
            _condition = condition;
            _action = action;
        }

        public void Execute(UserEvent e)
        {
            if (_condition.IsMatch(e))
            {
                _action(e);
            }
        }
    }
}