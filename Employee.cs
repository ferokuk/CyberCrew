//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberCrew
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Money = new HashSet<Money>();
            this.Money1 = new HashSet<Money>();
        }
    
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public bool IsWorking { get; set; }
        public string Gender { get; set; }
        public System.DateTime EmploymentDate { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int PositionId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string INN { get; set; }
    
        public virtual Position Position { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Money> Money { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Money> Money1 { get; set; }
    }
}