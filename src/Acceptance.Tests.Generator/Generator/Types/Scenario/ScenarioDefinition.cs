using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    internal class ScenarioDefinition
    {
        public string Name { get; }

        public ScenarioDefinition(string name, Background background, IEnumerable<Test> tests)
        {
            Name = string.Join("", name
                .Split(':', ' ')
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.UpperFirstLetter()));

            Background = background;
            Tests = tests;
        }

        public Background Background { get; }

        public IEnumerable<Test> Tests { get; }
    }
}
