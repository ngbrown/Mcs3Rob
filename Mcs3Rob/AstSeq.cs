using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcs3Rob
{
    internal class AstSeq : IAst
    {
        public IReadOnlyList<IAst> Items { get; }

        public AstSeq(IAst firstItem)
        {
            if (firstItem == null) throw new ArgumentNullException(nameof(firstItem));

            var itemList = firstItem as AstSeq;
            if (itemList?.Items?.Count == 1)
            {
                firstItem = itemList.Items[0];
            }

            Items = new List<IAst>() {firstItem};
        }

        public AstSeq(IEnumerable<IAst> itemList)
        {
            if (itemList == null) throw new ArgumentNullException(nameof(itemList));

            Items = new List<IAst>(itemList);
        }

        public AstSeq(params IAst[] items)
            : this((IEnumerable<IAst>)items)
        {
        }

        public AstSeq Append(IAst newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var itemList = newItem as AstSeq;
            if (itemList?.Items?.Count == 1)
            {
                newItem = itemList.Items[0];
            }

            return new AstSeq(Items.Concat(new[] {newItem}));
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }
    }
}