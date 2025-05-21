using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentsProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SQL Server bilan bog‘lanish uchun connection string
            string connectionString = "Server=LAPTOP-USUIROAJ;Database=StudentDb;Trusted_Connection=True;";

            // Foydalanuvchidan ism, yosh va kursini olamiz
            Console.WriteLine("Full Name kiriting:");
            string fullName = Console.ReadLine();

            Console.WriteLine("Yoshingizni kiritng:");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Kursini kiritng (masalan: Backend):");
            string course = Console.ReadLine();

            // SQL Serverga ulanamiz
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); // ulanish

                // INSERT query (foydalanuvchi ma'lumotlarini bazaga qo'shamiz)
                string insertQuery = "INSERT INTO Students (FullName, Age, Course) VALUES (@FullName, @Age, @Course)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    // SQL injection xavfsizligi uchun parametrlar
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@Course", course);

                    // Buyruqni bajarish
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} ta student qo'shildi.");
                }

                // Ma'lumotlar chiqarilishini so'rash
                Console.WriteLine("Barcha ma'lumotlar chiqishini xohlaysizmi? (ha/yoq)");
                string userChoice = Console.ReadLine().ToLower();

                if (userChoice == "ha")
                {
                    // Ma'lumotlarni chiqarish uchun SELECT query
                    string selectQuery = "SELECT * FROM Students";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        // Kurs parametrini qo'shish
                        cmd.Parameters.AddWithValue("@Course", course);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Agar ma'lumotlar mavjud bo'lsa, ularni chiqarish
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"FullName: {reader["FullName"]}, Age: {reader["Age"]}, Course: {reader["Course"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Bu kurs uchun hech qanday talabalar yo'q.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ma'lumotlar chiqarilmaydi.");
                }
            }

            Console.ReadLine(); // Dasturni to'xtatib turadi
        }
    }
}

