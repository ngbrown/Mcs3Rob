using System;
using System.Collections.Generic;
using System.Linq;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal class AstSeq : IAst
    {
        public IReadOnlyList<IAst> Items { get; }
        public LexLocation LexLocation { get; }

        public AstSeq(LexLocation lexLocation, IAst firstItem)
        {
            if (firstItem == null) throw new ArgumentNullException(nameof(firstItem));

            var itemList = firstItem as AstSeq;
            if (itemList?.Items?.Count == 1)
            {
                firstItem = itemList.Items[0];
            }

            LexLocation = lexLocation;
            Items = new List<IAst>() {firstItem};
        }

        public AstSeq(LexLocation lexLocation, IEnumerable<IAst> itemList)
        {
            if (itemList == null) throw new ArgumentNullException(nameof(itemList));

            Items = new List<IAst>(itemList);
        }

        public AstSeq(LexLocation lexLocation, params IAst[] items)
            : this(lexLocation, (IEnumerable<IAst>)items)
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

            return new AstSeq(LexLocation, Items.Concat(new[] {newItem}));
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }
    }
}