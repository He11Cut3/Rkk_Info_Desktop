//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RkkInfo
{
    using System;
    using System.Collections.Generic;
    
    public partial class RkkInfo_Jobs_Vacancy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RkkInfo_Jobs_Vacancy()
        {
            this.RkkInfo_Main = new HashSet<RkkInfo_Main>();
        }
    
        public int RkkInfo_Jobs_Vacancy_id { get; set; }
        public string RkkInfo_Jobs_Vacancy_Name { get; set; }
        public string RkkInfo_Jobs_Vacancy_First_Name { get; set; }
        public string RkkInfo_Jobs_Vacancy_Last_Name { get; set; }
        public string RkkInfo_Jobs_Vacancy_Position { get; set; }
        public string RkkInfo_Jobs_Vacancy_Date { get; set; }
        public byte[] RkkInfo_Jobs_Vacancy_Files { get; set; }
        public string RkkInfo_Jobs_Vacancy_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RkkInfo_Main> RkkInfo_Main { get; set; }
    }
}
