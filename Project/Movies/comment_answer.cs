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
    
    public partial class comment_answer
    {
        public int id { get; set; }
        public int comment_id { get; set; }
        public int user_id { get; set; }
        public string text { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual comment comment { get; set; }
        public virtual user user { get; set; }
    }
}