//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataServiceLayer.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public int UserId { get; set; }
        public Nullable<bool> IsLoaned { get; set; }
    
        public virtual User User { get; set; }
    }
}