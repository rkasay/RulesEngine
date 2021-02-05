using System;
namespace RulesEngine
{
    public interface ICondition
    {
        bool IsMatch(UserEvent e);
    }
}