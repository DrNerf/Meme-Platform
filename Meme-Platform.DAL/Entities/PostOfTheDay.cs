using System;
using System.Collections.Generic;
using System.Text;

namespace Meme_Platform.DAL.Entities
{
    public class PostOfTheDay
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual Post Post { get; set; }
    }
}
