using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using SG.Utilities;

namespace SimTests
{
    public class NotifySetTests
    {
        [Test]
        public void StartsEmpty()
        {
            var set = new NotifySet<int>();

            Assert.That(set, Is.Empty);
        }

        [Test]
        public void AddsSingleItem()
        {
            var set = new NotifySet<int>();
            set.Add(5);

            Assert.That(set, Is.Not.Empty);
        }

        [Test]
        public void UnionWithAddsItems()
        {
            var set = new NotifySet<int>();

            set.UnionWith(new[] { 5, 10, 15 });

            Assert.That(set, Is.Not.Empty);
            Assert.That(set.Count, Is.EqualTo(3));
        }
    }
}
