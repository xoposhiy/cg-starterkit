using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace bot
{
    [TestFixture]
    public class StateTests
    {
        [TestCase("Some|init|data", "Some input|copy pasted from|error stream")]
        public void Read(string initInput, string stepInput)
        {
            var state = new StateReader().Read(initInput, stepInput);
        }
    }
}