using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    // Клас для лікаря, бо лікар це важливо
    public class Doctor
    {
        // Властивості для лікаря
        public int Id { get; set; } // id лікаря
        public string Name { get; set; } // ім'я
        public string Specialization { get; set; } // спеціалізація, наприклад терапевт

        // Конструктор, щоб створити лікаря
        public Doctor(int id, string name, string specialization)
        {
            // присвоюємо значення
            Id = id;
            Name = name;
            Specialization = specialization;
            // все ок
        }
    }
}