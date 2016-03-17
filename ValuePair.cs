using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionHelper
{
    public class ValuePair
    {
        public ValuePair(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public ValuePair()
            : this("", "")
        {

        }

        public string Text
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
