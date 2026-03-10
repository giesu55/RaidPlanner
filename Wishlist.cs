using System.Collections.ObjectModel;

namespace RaidPlanner
{
    public class Wishlist
    {
        public string Name { get; set; }

        public ObservableCollection<WishlistItem> Items { get; set; }
            = new ObservableCollection<WishlistItem>();
    }
}