//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Movies
{
    using System;
    using System.Collections.Generic;
    
    public partial class person
    {
        public person()
        {
            this.cast = new HashSet<cast>();
            this.comment = new HashSet<comment>();
            this.image = new HashSet<image>();
            this.image_relation = new HashSet<image_relation>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> birth_date { get; set; }
        public string birth_place { get; set; }
    
        public virtual ICollection<cast> cast { get; set; }
        public virtual ICollection<comment> comment { get; set; }
        public virtual ICollection<image> image { get; set; }
        public virtual ICollection<image_relation> image_relation { get; set; }
    }
}