using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
