using System;
using System.Collections.Generic;

namespace InformationSystem2
{

    internal class Program
    {

        //Статический метод вычисления номера пары неотрицательных
        //целых чисел (Канторовская нумерация пар)
        static int KantorPairs(int x, int y)
        {
            int n = (x + y) * (x + y + 1) / 2 + x;
            return n;
        }

        static void Main(string[] args)
        {
            #region Заполнение xml-файлов тестовыми данными
            {
                //Создали экземпляр класса Organization
                Organization organization = new Organization();

                //Создали экземпляр класа Random
                Random rnd = new Random();
                int i, j = 0;

                //Создадим, для примера, 20 сотрудников и
                //4 отдела. И распределим по 5 сотрудников в
                //каждый отел.
                for (i = 0; i < 4; i++)
                {
                    //Инициализация экземпляра класса Department
                    Department department = new Department($"Отдел {KantorPairs(i, j)}",
                        new DateTime(2021, rnd.Next(1, 12), rnd.Next(1, 28)).Date);

                    //Добавляем инициализированный отдел в список всех отделов
                    organization.AddDepartmentToDepartments(department);

                    for (j = 0; j < 5; j++)
                    {
                        //Инициализация экземпляра класса Employee
                        Employee employee = new Employee($"Фамилия {rnd.Next(1, 10)}",
                            $"Имя {rnd.Next(1, 10)}", rnd.Next(18, 70), rnd.Next(1, 3));

                        //Добавляем сотрудника в список всех сотрудников
                        organization.AddEmployeeToEmployees(employee);

                        //Добавляем сотрудника в отдел
                        organization.AddEmployeeToDepartment(department, employee);
                    }
                }

                //Сохраним тестовые данные о сотрудниках и отделах в xml-файлах
                Console.Clear();
                //Добавляем коллекцию сотрудников в xml-файл
                organization.AddEmployeesToXml();

                //Добавляем коллекцию отделов в xml-файл
                organization.AddDepartmentsToXml();

                Console.WriteLine("Тестовые файлы успешно зполнены");
                Console.ReadKey();
                Console.Clear();
            }
            #endregion

            {
                //Organization organization = new Organization();
                //organization.DepartmentsFromXmlToCollection();

                //Console.Write("Название отдела: ");
                //string departmentName = Console.ReadLine();
                //Department department = organization.FindDepartment(departmentName);
                //Console.WriteLine(department);
                //Console.ReadKey();
                //Console.Clear();
            }
            {
                //Organization organization = new Organization();
                //organization.EmployeesFromXmlToCollection();
                //Console.Write("Фамилия: ");
                //string secondName = Console.ReadLine();
                //Console.Write("Имя: ");
                //string firstName = Console.ReadLine();
                //List<Employee> employees = organization.FindEmployee(secondName, firstName); 
                //Console.ReadKey();
                //Console.Clear();
            }
            {
                Organization organization = new Organization();
                organization.DepartmentsFromXmlToCollection();

                Console.WriteLine("Введите данные нового отдела:");
                Console.Write("Название отдела: ");
                string departmentName = Console.ReadLine();
                Guid id = Guid.NewGuid();
                DateTime creationDate = DateTime.Now;
                int contingent = 0;
                Department department = new Department(id, departmentName, creationDate, contingent);
                organization.AddDepartmentToXml(department);
                Console.ReadKey();
                Console.Clear();
            }
            {
                Organization organization = new Organization();

                Console.WriteLine("Введите данные нового сотрудника:");
                Console.Write("Фамилия: ");
                string secondName = Console.ReadLine();
                Console.Write("Имя: ");
                string firstName = Console.ReadLine();
                Console.Write("Возраст: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Количество проектов: ");
                int projects = int.Parse(Console.ReadLine());
                Console.Write("Отдел: ");
                string departmentName = Console.ReadLine();

                //Находим отдел, в который хотим добавить нового сотрудника
                organization.DepartmentsFromXmlToCollection();
                Department department = organization.FindDepartment(departmentName);

                //Создаём нового сотрудника
                Employee employee = new Employee(secondName, firstName, age, projects);
                //Добавляем нового сотрудника в отдел
                organization.AddEmployeeToDepartment(department, employee);
                //Сохранияем сведения о новом сотруднике в xml-файле
                organization.AddEmployeeToXml(employee);
                //Увеличиваем контингент того отдела, в который новый сотрудник был добавлен
                organization.EditContingentDepartmentXml(department, "add");
            }
        }
    }
}
