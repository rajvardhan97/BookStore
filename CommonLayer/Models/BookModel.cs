using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName {  get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float TotalRating { get; set; }
        public float Rating { get; set; }
        public int OriginalPrice { get; set; }
        public int DiscountPrice { get; set; }
        public string BookImage { get; set; }
    }
}
