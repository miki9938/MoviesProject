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
    
    public partial class image
    {
        public image()
        {
            this.image_relation = new HashSet<image_relation>();
        }
    
        public int id { get; set; }
        public Nullable<int> movie_id { get; set; }
        public Nullable<int> person_id { get; set; }
    
        public virtual movie movie { get; set; }
        public virtual person person { get; set; }
        public virtual ICollection<image_relation> image_relation { get; set; }
    }
}