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

                Console.WriteLine("Тестовые файлы успешно заполнены");
                Console.Write("Для продолжения нажмите Enter");
                Console.ReadKey();
                Console.Clear();
            }
            #endregion

            #region Основное меню
            {
                Console.WriteLine("Меню");
                Console.WriteLine();

                MainMenu mainMenu = new MainMenu();

                while (true)
                {
                    mainMenu.DrawMenu();
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (mainMenu.Index < mainMenu.menuItems.Length - 1)
                                mainMenu.Index++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (mainMenu.Index > 0)
                                mainMenu.Index--;
                            break;
                        case ConsoleKey.Enter:
                            switch (mainMenu.Index)
                            {
                                case 0:
                                    {   //Поиск отдела
                                        Console.WriteLine("Поиск отдела");
                                        Organization organization = new Organization();
                                        organization.DepartmentsFromXmlToCollection();
                                        Console.WriteLine("Поиск отдела");
                                        Console.Write("Название отдела: ");
                                        string departmentName = Console.ReadLine();
                                        Department department = organization.FindDepartment(departmentName);
                                        if (department != null)
                                        {
                                            Console.WriteLine(department);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Отдел \"{departmentName}\" не найден");
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;

                                case 1:
                                    {   //Поиск сотрудника
                                        Console.WriteLine("Поиск сотрудника");
                                        Organization organization = new Organization();
                                        organization.EmployeesFromXmlToCollection();
                                        Console.WriteLine("Поиск сотрудника по фамилии и имени");
                                        Console.Write("Фамилия: ");
                                        string secondName = Console.ReadLine();
                                        Console.Write("Имя: ");
                                        string firstName = Console.ReadLine();

                                        //Поскольку могут быть разные сотрудники с одинаковыми фамилиями и именоми,
                                        //то будем создавать список всех таких сотрудников
                                        List<Employee> employees = organization.FindEmployee(secondName, firstName);
                                        if (employees != null)
                                        {
                                            Console.WriteLine($"Найденo сотрудников: {employees.Count}");
                                            foreach (Employee employee in employees)
                                            {
                                                //Выводим список всех найденных сотрудников на экран консоли
                                                Console.WriteLine(employee);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Сотрудник {secondName} {firstName} не найден");
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 2:
                                    {   //Создание нового отдела
                                        Console.WriteLine("Добавить новый отдел");
                                        Organization organization = new Organization();
                                        organization.DepartmentsFromXmlToCollection();

                                        Console.WriteLine("Введите данные нового отдела:");
                                        Console.Write("Название отдела: ");
                                        string departmentName = Console.ReadLine();
                                        if (organization.FindDepartment(departmentName) == null)
                                        {
                                            Guid id = Guid.NewGuid();
                                            DateTime creationDate = DateTime.Now;
                                            int contingent = 0;
                                            Department department = new Department(id, departmentName, creationDate, contingent);
                                            organization.AddDepartmentToXml(department);
                                            Console.WriteLine($"Отдел {departmentName} успешно создан");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Отдел \"{departmentName}\" уже существует");
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 3:
                                    {   //Добавление нового сотрудника
                                        Console.WriteLine("Добавить нового сотрудника");
                                        Organization organization = new Organization();

                                        Console.WriteLine("Введите данные нового сотрудника:");
                                        Console.Write("Отдел: ");
                                        string departmentName = Console.ReadLine();
                                        //Находим отдел, в который хотим добавить нового сотрудника
                                        organization.DepartmentsFromXmlToCollection();
                                        Department department = organization.FindDepartment(departmentName);
                                        //Проверяем, существует ли отдел с введенным названием
                                        if (department != null)
                                        {
                                            Console.Write("Фамилия: ");
                                            string secondName = Console.ReadLine();
                                            Console.Write("Имя: ");
                                            string firstName = Console.ReadLine();
                                            Console.Write("Возраст: ");
                                            int age = int.Parse(Console.ReadLine());
                                            Console.Write("Количество проектов: ");
                                            int projects = int.Parse(Console.ReadLine());

                                            //Создаём нового сотрудника
                                            Employee employee = new Employee(secondName, firstName, age, projects);

                                            //Добавляем нового сотрудника в отдел
                                            organization.AddEmployeeToDepartment(department, employee);

                                            //Сохранияем сведения о новом сотруднике в xml-файле
                                            organization.AddEmployeeToXml(employee);

                                            //Увеличиваем контингент того отдела, в который новый сотрудник был добавлен, на единицу
                                            organization.EditContingentDepartmentXml(department, "add");

                                            Console.WriteLine($"Сотрудник {secondName} {firstName} успешно добавлен в отдел {departmentName}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Отдела с названием \"{departmentName}\" не существует");
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 4:
                                    {
                                        Console.WriteLine("Переименовать отдел");
                                        Organization organization = new Organization();
                                        Console.Write("Введите название отдела, который необходимо переименовать: ");
                                        string oldDepartmentName = Console.ReadLine();
                                        organization.DepartmentsFromXmlToCollection();
                                        if (organization.FindDepartment(oldDepartmentName) != null)
                                        {
                                            Console.Write("Введите новое название отдела: ");
                                            string newDepartmentName = Console.ReadLine();
                                            organization.RenameDepartmentXml(oldDepartmentName, newDepartmentName);
                                            Console.WriteLine($"Отдел \"{oldDepartmentName}\" переименован в \"{newDepartmentName}\"");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Отдел \"{oldDepartmentName}\" не найден");
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 5:
                                    {
                                        Console.WriteLine("Изменить число проектов сотрудника");
                                        Organization organization = new Organization();
                                        Console.WriteLine("Данные сотрудника");
                                        Console.Write("Фамилия: ");
                                        string secondName = Console.ReadLine();
                                        Console.Write("Имя: ");
                                        string firstName = Console.ReadLine();

                                        //Читаем из xml-файла список всех сотрудников
                                        organization.EmployeesFromXmlToCollection();

                                        //Формируем список всех сотрудников, удовлетворяющих условиям поиска (таких может оказаться несколько)
                                        List<Employee> employees = organization.FindEmployee(secondName, firstName);

                                        //Количество сотрудников, удовлетворяющих условиям поиска
                                        int count = employees.Count;

                                        if (employees != null)
                                        {
                                            for (int i = 0; i < count; i++)
                                            {
                                                //Выводим на экран сведения обо всех найденных сотрудниках
                                                Console.WriteLine($"№ {i}\n{employees[i]}\n");
                                            }
                                            Console.Write("Номер сотрудника для изменения числа проектов: ");
                                            int number = int.Parse(Console.ReadLine());

                                            //Выбранный сотрудник
                                            Employee employee = employees[number];

                                            //Изменяем число проектов
                                            Console.Write("Новое число проектов: ");

                                            int newProjects = int.Parse(Console.ReadLine());
                                            organization.EditEmployee(employee, newProjects);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Сотрудник с фамилией {secondName} и именем {firstName} не найден");
                                        }
                                        Console.WriteLine("Количество проектов успешно изменено");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 6:
                                    {
                                        Console.WriteLine("Удалить сотрудника");
                                        Organization organization = new Organization();
                                        //Читаем из xml-файла список всех сотрудников
                                        organization.EmployeesFromXmlToCollection();
                                        //Читаем из xml-файла список всех отделов
                                        organization.DepartmentsFromXmlToCollection();

                                        Console.WriteLine("Введите данные сотрудника, которого хотите удалить");
                                        Console.Write("Фамилия: ");
                                        string secondName = Console.ReadLine();
                                        Console.Write("Имя: ");
                                        string firstName = Console.ReadLine();

                                        //Формируем список всех сотрудников, удовлетворяющих условиям поиска (таких может оказаться несколько)
                                        List<Employee> employees = organization.FindEmployee(secondName, firstName);

                                        //Количество сотрудников, удовлетворяющих условиям поиска
                                        int count = employees.Count;
                                        Console.Clear();

                                        if (employees != null)
                                        {
                                            for (int i = 0; i < count; i++)
                                            {
                                                //Выводим на экран сведения обо всех найденных сотрудниках
                                                Console.WriteLine($"№ {i + 1}\n{employees[i]}\n");
                                            }
                                            Console.Write("Номер сотрудника для удаления: ");
                                            int number = int.Parse(Console.ReadLine());

                                            //Выбранный сотрудник
                                            Employee employee = employees[number - 1];

                                            //Отдел, к которому он прикреплён
                                            Department department = organization.FindDepartment(employee);

                                            //Удаляем из xml-файла сотрудника
                                            organization.DeleteEmployeeFromXml(employee);

                                            //Меняем контингент отдела из которого удалили сотрудника
                                            organization.EditContingentDepartmentXml(department, "del");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Сотрудник с фамилией {secondName} и именем {firstName} не найден");
                                        }
                                        Console.WriteLine($"Сотрудник c фамилией {secondName} и именем {firstName} успешно удалён");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 7:
                                    {
                                        Console.WriteLine("Удалить отдел со всеми сотрудниками");
                                        Organization organization = new Organization();
                                        Console.Write("Введите название отдела, в который хотите удалить: ");
                                        string departmentName = Console.ReadLine();
                                        Guid idDepartment = organization.DeleteDepartmentFromXml(departmentName);
                                        organization.DeleteDepartmentEmployees(idDepartment);
                                        Console.WriteLine($"Отдел {departmentName} и все сотрудники данного отдела удалены");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 8:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Сортировка по полю \"Возраст\"");
                                        Organization organization = new Organization();
                                        organization.EmployeesFromXmlToCollection();
                                        organization.Employees = organization.SortByAge();
                                        organization.ShowAllEmployees();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;
                                
                                case 9:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Сортировка по полю \"Фамилия сотрудника\" и \"Заработной плате\"");
                                        Organization organization = new Organization();
                                        organization.EmployeesFromXmlToCollection();
                                        organization.Employees = organization.SortBySecondNameThenTotal();
                                        organization.ShowAllEmployees();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;

                                case 10:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Сортировка по полю \"Фамилия сотрудника\" и \"Имя сотрудника\" и \"Число проектов\"");
                                        Organization organization = new Organization();
                                        organization.EmployeesFromXmlToCollection();
                                        organization.Employees = organization.SortBySecondNameThenFirstNameThenProjects();
                                        organization.ShowAllEmployees();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    break;

                                case 11:
                                    Console.WriteLine($"Выбран пункт \"{mainMenu.menuItems[mainMenu.Index]}\"");
                                    return;
                            }
                            break;
                    }
                }
            }
            #endregion

            #region Поиск отдела
            //{
            //    Organization organization = new Organization();
            //    organization.DepartmentsFromXmlToCollection();
            //    Console.WriteLine("Поиск отдела");
            //    Console.Write("Название отдела: ");
            //    string departmentName = Console.ReadLine();
            //    Department department = organization.FindDepartment(departmentName);
            //    if (department != null)
            //    {
            //        Console.WriteLine(department);
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Отдел \"{departmentName}\" не найден");
            //    }
            //    Console.ReadKey();
            //    Console.Clear();
            //}
            #endregion

            #region Поиск сотрудника
            //{
            //    Organization organization = new Organization();
            //    organization.EmployeesFromXmlToCollection();
            //    Console.WriteLine("Поиск сотрудника по фамилии и имени");
            //    Console.Write("Фамилия: ");
            //    string secondName = Console.ReadLine();
            //    Console.Write("Имя: ");
            //    string firstName = Console.ReadLine();

            //    //Поскольку могут быть разные сотрудники с одинаковыми фамилиями и именоми,
            //    //то будем создавать список всех таких сотрудников
            //    List<Employee> employees = organization.FindEmployee(secondName, firstName);
            //    if(employees != null)
            //    {
            //        Console.WriteLine($"Найденo сотрудников: {employees.Count}");
            //        foreach(Employee employee in employees)
            //        {
            //            //Выводим список всех найденных сотрудников на экран консоли
            //            Console.WriteLine(employee);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Сотрудник {secondName} {firstName} не найден");
            //    }
            //    Console.ReadKey();
            //    Console.Clear();
            //}
            #endregion

            #region Добавление нового отдела
            //{   
            //    Organization organization = new Organization();
            //    organization.DepartmentsFromXmlToCollection();

            //    Console.WriteLine("Введите данные нового отдела:");
            //    Console.Write("Название отдела: ");
            //    string departmentName = Console.ReadLine();
            //    if (organization.FindDepartment(departmentName) == null)
            //    {
            //        Guid id = Guid.NewGuid();
            //        DateTime creationDate = DateTime.Now;
            //        int contingent = 0;
            //        Department department = new Department(id, departmentName, creationDate, contingent);
            //        organization.AddDepartmentToXml(department);
            //        Console.WriteLine($"Отдел {departmentName} успешно создан");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Отдел \"{departmentName}\" уже существует");
            //    }
            //    Console.ReadKey();
            //    Console.Clear();
            //}
            #endregion

            #region Добавление нового сотрудника
            //{   
            //    Organization organization = new Organization();

            //    Console.WriteLine("Введите данные нового сотрудника:");
            //    Console.Write("Отдел: ");
            //    string departmentName = Console.ReadLine();
            //    //Находим отдел, в который хотим добавить нового сотрудника
            //    organization.DepartmentsFromXmlToCollection();
            //    Department department = organization.FindDepartment(departmentName);
            //    //Проверяем, существует ли отдел с введенным названием
            //    if (department != null)
            //    {
            //        Console.Write("Фамилия: ");
            //        string secondName = Console.ReadLine();
            //        Console.Write("Имя: ");
            //        string firstName = Console.ReadLine();
            //        Console.Write("Возраст: ");
            //        int age = int.Parse(Console.ReadLine());
            //        Console.Write("Количество проектов: ");
            //        int projects = int.Parse(Console.ReadLine());

            //        //Создаём нового сотрудника
            //        Employee employee = new Employee(secondName, firstName, age, projects);

            //        //Добавляем нового сотрудника в отдел
            //        organization.AddEmployeeToDepartment(department, employee);

            //        //Сохранияем сведения о новом сотруднике в xml-файле
            //        organization.AddEmployeeToXml(employee);

            //        //Увеличиваем контингент того отдела, в который новый сотрудник был добавлен, на единицу
            //        organization.EditContingentDepartmentXml(department, "add");

            //        Console.WriteLine($"Сотрудник {secondName} {firstName} успешно добавлен в отдел {departmentName}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Отдела с названием \"{departmentName}\" не существует");
            //    }
            //    Console.ReadKey();
            //    Console.Clear();
            //}
            #endregion

            #region Редактирование отдела (переименование)
            {
                //Organization organization = new Organization();
                //Console.Write("Введите название отдела, который необходимо переименовать: ");
                //string oldDepartmentName = Console.ReadLine();
                //organization.DepartmentsFromXmlToCollection();
                //if (organization.FindDepartment(oldDepartmentName) != null)
                //{
                //    Console.Write("Введите новое название отдела: ");
                //    string newDepartmentName = Console.ReadLine();
                //    organization.RenameDepartmentXml(oldDepartmentName, newDepartmentName);
                //    Console.WriteLine($"Отдел \"{oldDepartmentName}\" переименован в \"{newDepartmentName}\"");
                //}
                //else
                //{
                //    Console.WriteLine($"Отдел \"{oldDepartmentName}\" не найден");
                //}
                //Console.ReadKey();
                //Console.Clear();
            }
            #endregion

            #region Редактирование сотрудника (изменение числа проектов)
            {
                //Organization organization = new Organization();
                //Console.WriteLine("Изменение количества проектов сотрудника");
                //Console.Write("Фамилия: ");
                //string secondName = Console.ReadLine();
                //Console.Write("Имя: ");
                //string firstName = Console.ReadLine();

                ////Читаем из xml-файла список всех сотрудников
                //organization.EmployeesFromXmlToCollection();

                ////Формируем список всех сотрудников, удовлетворяющих условиям поиска (таких может оказаться несколько)
                //List<Employee> employees = organization.FindEmployee(secondName, firstName);

                ////Количество сотрудников, удовлетворяющих условиям поиска
                //int count = employees.Count;

                //if (employees != null)
                //{
                //    for (int i = 0; i < count; i++)
                //    {
                //        //Выводим на экран сведения обо всех найденных сотрудниках
                //        Console.WriteLine($"№ {i}\n{employees[i]}\n");
                //    }
                //    Console.Write("Номер сотрудника для изменения числа проектов: ");
                //    int number = int.Parse(Console.ReadLine());

                //    //Выбранный сотрудник
                //    Employee employee = employees[number];

                //    //Изменяем число проектов
                //    Console.Write("Новое число проектов: ");

                //    int newProjects = int.Parse(Console.ReadLine());
                //    organization.EditEmployee(employee, newProjects);
                //}
                //else
                //{
                //    Console.WriteLine($"Сотрудник с фамилией {secondName} и именем {firstName} не найден");
                //}
                //Console.WriteLine("Количество проектов успешно изменено");
                //Console.ReadKey();
                //Console.Clear();
            }
            #endregion

            #region Удаление сотрудника
            {
                //Organization organization = new Organization();
                ////Читаем из xml-файла список всех сотрудников
                //organization.EmployeesFromXmlToCollection();
                ////Читаем из xml-файла список всех отделов
                //organization.DepartmentsFromXmlToCollection();
                
                //Console.WriteLine("Введите данные сотрудника, которого хотите удалить");
                //Console.Write("Фамилия: ");
                //string secondName = Console.ReadLine();
                //Console.Write("Имя: ");
                //string firstName = Console.ReadLine();

                ////Формируем список всех сотрудников, удовлетворяющих условиям поиска (таких может оказаться несколько)
                //List<Employee> employees = organization.FindEmployee(secondName, firstName);

                ////Количество сотрудников, удовлетворяющих условиям поиска
                //int count = employees.Count;
                //Console.Clear();

                //if (employees != null)
                //{
                //    for (int i = 0; i < count; i++)
                //    {
                //        //Выводим на экран сведения обо всех найденных сотрудниках
                //        Console.WriteLine($"№ {i + 1}\n{employees[i]}\n");
                //    }
                //    Console.Write("Номер сотрудника для удаления: ");
                //    int number = int.Parse(Console.ReadLine());

                //    //Выбранный сотрудник
                //    Employee employee = employees[number - 1];
                    
                //    //Отдел, к которому он прикреплён
                //    Department department = organization.FindDepartment(employee);
                    
                //    //Удаляем из xml-файла сотрудника
                //    organization.DeleteEmployeeFromXml(employee);

                //    //Меняем контингент отдела из которого удалили сотрудника
                //    organization.EditContingentDepartmentXml(department, "del");
                //}
                //else
                //{
                //    Console.WriteLine($"Сотрудник с фамилией {secondName} и именем {firstName} не найден");
                //}
                //Console.WriteLine($"Сотрудник c фамилией {secondName} и именем {firstName} успешно удалён");
                //Console.ReadKey();
                //Console.Clear();
            }
            #endregion

            #region Удаление отдела и всех сотрудников данного отдела
            {
                //Organization organization = new Organization();
                //Console.Write("Введите название отдела, в который хотите удалить: ");
                //string departmentName = Console.ReadLine();
                //Guid idDepartment = organization.DeleteDepartmentFromXml(departmentName);
                //organization.DeleteDepartmentEmployees(idDepartment);
                //Console.WriteLine($"Отдел {departmentName} и все сотрудники данного отдела удалены");
                //Console.ReadKey();
                //Console.Clear();
            }
            #endregion
        }
    }
}
