// HospitalDemo.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    // Демо для показу як працює
    public class HospitalDemo
    {
        // головний метод
        public void Run()
        {
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ЛІКАРНЕЮ ===\n");

            // створюємо лікарню
            Hospital hospital = new Hospital();

            // додаємо лікарів, бо потрібно 2-3
            Doctor doctor1 = new Doctor(1, "Іванов Іван", "Терапевт");
            Doctor doctor2 = new Doctor(2, "Петрова Марія", "Хірург");
            Doctor doctor3 = new Doctor(3, "Сидоренко Петро", "Кардіолог");
            hospital.AddDoctor(doctor1);
            hospital.AddDoctor(doctor2);
            hospital.AddDoctor(doctor3);

            // пацієнти, 3-4
            Patient patient1 = new Patient(1, "Коваленко Тарас", 35);
            Patient patient2 = new Patient(2, "Шевченко Ольга", 28);
            Patient patient3 = new Patient(3, "Бондаренко Андрій", 42);
            Patient patient4 = new Patient(4, "Кравченко Світлана", 31);
            hospital.RegisterPatient(patient1);
            hospital.RegisterPatient(patient2);
            hospital.RegisterPatient(patient3);
            hospital.RegisterPatient(patient4);

            // палати
            HospitalRoom room1 = new HospitalRoom(101, 2);
            HospitalRoom room2 = new HospitalRoom(102, 3);
            HospitalRoom room3 = new HospitalRoom(103, 1);
            hospital.CreateRoom(room1);
            hospital.CreateRoom(room2);
            hospital.CreateRoom(room3);

            // госпіталізуємо
            hospital.HospitalizePatient(1, 101);
            hospital.HospitalizePatient(2, 101);
            hospital.HospitalizePatient(3, 102);
            hospital.HospitalizePatient(4, 102);

            // записи, 2-3
            MedicalRecord record1 = new MedicalRecord(patient1, doctor1, DateTime.Now, "Грип, призначено медикаменти");
            MedicalRecord record2 = new MedicalRecord(patient2, doctor2, DateTime.Now.AddDays(1), "Операція на апендицит");
            MedicalRecord record3 = new MedicalRecord(patient3, doctor3, DateTime.Now.AddDays(2), "Контроль серця");
            hospital.AddMedicalRecord(record1);
            hospital.AddMedicalRecord(record2);
            hospital.AddMedicalRecord(record3);

            // показуємо історію першого пацієнта
            Console.WriteLine("\n--- ІСТОРІЯ ПАЦІЄНТА ---");
            var history = hospital.GetPatientHistory(1);
            // цикл для виведення
            foreach (var record in history)
            {
                Console.WriteLine($"  Дата: {record.Date.ToShortDateString()}");
                Console.WriteLine($"  Лікар: {record.Doctor.Name}");
                Console.WriteLine($"  Опис: {record.Description}\n");
            }

            // статистика
            Console.WriteLine(hospital.GetStatistics());
        }
    }
}