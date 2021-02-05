using System;
using System.Collections.Generic;

namespace RulesEngine
{
    public class UserEvent
    {
        public UserEvent()
        {
        }

        public IDictionary<string, object> Attributes { get; set; }
    }
}