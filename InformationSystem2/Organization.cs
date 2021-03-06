using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InformationSystem2
{
    internal class Organization
    {
        #region Приватные коллекции
        /// <summary>
        /// Список отделов
        /// </summary>
        private List<Department> departments = new List<Department>();

        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<Employee> employees = new List<Employee>();
        #endregion

        #region Свойства для коллекций
        /// <summary>
        /// Список отделов
        /// </summary>
        internal List<Department> Departments { get => departments; set => departments = value; }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        internal List<Employee> Employees { get => employees; set => employees = value; }
        #endregion

        #region Конструктры

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        internal Organization()
        { }

        #endregion

        #region Методы для заполнения xml-файлов тестовыми даннми
        /// <summary>
        /// Метод добавления списка сотрудников в xml-файл для заполнения файла тестовыми данными
        /// </summary>
        internal void AddEmployeesToXml()
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            XElement root = xDoc.Element("Employees");
            foreach (Employee employee in Employees)
            {
                root.Add(new XElement("Employee",
                    new XElement("Id", employee.Id),
                    new XElement("Фамилия", employee.SecondName),
                    new XElement("Имя", employee.FirstName),
                    new XElement("Возраст", employee.Age),
                    new XElement("Зарплата", employee.Total),
                    new XElement("Проекты", employee.Projects),
                    new XElement("Отдел", employee.IdDepartment)));
            }
            xDoc.Save("employees.xml");
        }

        /// <summary>
        /// Метод добавления списка отделов в xml-файл для заполнения файла тестовыми данными
        /// </summary>
        internal void AddDepartmentsToXml()
        {
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement root = xDoc.Element("Departments");
            foreach (Department department in Departments)
            {
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id),
                            new XElement("Отдел", department.DepartmentName),
                            new XElement("Создан", department.CreationDate),
                            new XElement("Контингент", department.Contingent)));
            }
            xDoc.Save("departments.xml");
        }

        /// <summary>
        /// Метод добавления нового отдела к списку всех отделов
        /// </summary>
        /// <param name="department">Добавляемый отдел</param>
        internal void AddDepartmentToDepartments(Department department)
        {
            Departments.Add(department);
        }

        /// <summary>
        /// Метод добавления нового сотрудника к списку всех сотрудников
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        internal void AddEmployeeToEmployees(Employee employee)
        {
            employees.Add(employee);
        }

        /// <summary>
        /// Метод присваивает каждому сотруднику отдела
        /// уникальный идентификационный номер отдела
        /// </summary>
        /// <param name="department">Отдел к которому прикрепляется сотрудник</param>
        /// <param name="employee">Прикрепляемый сотрудник</param>
        internal void AddEmployeeToDepartment(Department department, Employee employee)
        {
            employee.IdDepartment = department.Id;
            department.Contingent++;
        }
        #endregion

        #region Рабочие методы

        /// <summary>
        /// Метод добавления одного сотрудника в xml-файл
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        internal void AddEmployeeToXml(Employee employee)
        {
            XDocument xdoc = XDocument.Load("employees.xml");
            XElement root = xdoc.Element("Employees");

            if (root != null)
            {
                // добавляем новый элемент
                root.Add(new XElement("Employee",
                            new XElement("Id", employee.Id),
                            new XElement("Фамилия", employee.SecondName),
                            new XElement("Имя", employee.FirstName),
                            new XElement("Возраст", employee.Age),
                            new XElement("Зарплата", employee.Total),
                            new XElement("Проекты", employee.Projects),
                            new XElement("Отдел", employee.IdDepartment)));

                xdoc.Save("employees.xml");
            }
        }

        /// <summary>
        /// Метод добавления одного отдела в xml-файл
        /// </summary>
        /// <param name="department"></param>
        internal void AddDepartmentToXml(Department department)
        {
            XDocument xdoc = XDocument.Load("departments.xml");
            XElement root = xdoc.Element("Departments");

            if (root != null)
            {
                // добавляем новый элемент
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id),
                            new XElement("Отдел", department.DepartmentName),
                            new XElement("Создан", department.CreationDate),
                            new XElement("Контингент", department.Contingent)));

                xdoc.Save("departments.xml");
            }
        }

        /// <summary>
        /// Метод читает данные обо всех сотрудниках и передаёт их в коллекцию Employees
        /// </summary>
        internal void EmployeesFromXmlToCollection()
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            XElement _employees = xDoc.Element("Employees");
            if(_employees != null)
            {
                foreach (XElement _employee in _employees.Elements("Employee"))
                {
                    Employee employee = new Employee
                    {
                        Id = Guid.Parse(_employee.Element("Id").Value),
                        SecondName = _employee.Element("Фамилия").Value,
                        FirstName = _employee.Element("Имя").Value,
                        Age = int.Parse(_employee.Element("Возраст").Value),
                        Total = int.Parse(_employee.Element("Зарплата").Value),
                        Projects = int.Parse(_employee.Element("Проекты").Value),
                        IdDepartment = Guid.Parse(_employee.Element("Отдел").Value)
                    };
                    Employees.Add(employee);
                }
            }
        }

        /// <summary>
        /// Метод читает данные обо всех отделах и передаёт их в коллекцию Departments
        /// </summary>
        internal void DepartmentsFromXmlToCollection()
        {
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement _departments = xDoc.Element("Departments");
            if (_departments != null)
            {
                foreach (XElement dep in _departments.Elements("Department"))
                {
                    Department department = new Department
                    {
                        Id = Guid.Parse(dep.Element("Id").Value),
                        DepartmentName = dep.Element("Отдел").Value,
                        CreationDate = DateTime.Parse(dep.Element("Создан").Value),
                        Contingent = int.Parse(dep.Element("Контингент").Value)
                    };
                    Departments.Add(department);
                }
            }
        }

        /// <summary>
        /// Метод поиска отдела по названию отдела.
        /// Поскольку название отдела уникально, то
        /// либо отдел существует, и метод возвращает экземпляр класса,
        /// либо отдел не существует, и тогда возвращает NULL.
        /// </summary>
        /// <param name="departmentName">Отдел</param>
        /// <returns></returns>
        internal Department FindDepartment(string departmentName)
        {
            Department department = null;

            foreach(Department dep in Departments)
            {
                if(dep.DepartmentName == departmentName)
                {
                    department = dep;
                    break;
                }
            }
            return department;
        }

        /// <summary>
        /// Метод поиска отдела по сотруднику.
        /// Так как каждый сотрудник числится только в одном отделе,
        /// то найдётся либо только один отдел,
        /// либо отдел вообще не будет найден
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal Department FindDepartment(Employee employee)
        {
            Department department = null;
            
            foreach (Department _department in Departments)
            {
                if (_department.Id == employee.IdDepartment)
                {
                    department= _department;
                    break;
                }
            }
            return department;
        }

        /// <summary>
        /// Метод поиска сотрудника по фамилии и имени.
        /// Поскольку таких сотрудников может быть больше 1, то всех сотрудников,
        /// с такими фамилиями и именами кидаем в коллекцию
        /// </summary>
        /// <param name="secondName">Фамилия для поиска</param>
        /// <param name="firstName">Имя для поиска</param>
        /// <returns>Список всех найденных сотрудников</returns>
        internal List<Employee> FindEmployee(string secondName, string firstName)
        {
            List<Employee> employees = new List<Employee>();

            foreach(Employee emp in Employees)
            {
                if(emp.SecondName == secondName && emp.FirstName == firstName)
                {
                    employees.Add(emp);
                }
            }
            return employees;
        }

        /// <summary>
        /// Метод редактирования сотрудника (меняем число проектов)
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="projects">Новое число проектов</param>
        internal void EditEmployee(Employee employee, int newProjects)
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            XElement root = xDoc.Element("Employees");

            if (root != null)
            {
                foreach (XElement _employee in root.Elements("Employee"))
                {
                    if(_employee.Element("Id").Value == employee.Id.ToString())
                    {
                        _employee.Element("Проекты").Value = newProjects.ToString();
                        Employee emp = new Employee(newProjects);
                        _employee.Element("Зарплата").Value = emp.Total.ToString();
                        break;
                    }
                }
            }
            xDoc.Save("employees.xml");
        }

        /// <summary>
        /// Метод меняет контингент отдела в xml-файле.
        /// При добавлении сотрудника, контингент увеличивается на единицу
        /// При удалении сотрудника, контингент уменьшается на единицу
        /// </summary>
        /// <param name="department">Отдел</param>
        /// <param name="addOrDel">Параметр флаг(add или del)</param>
        internal void EditContingentDepartmentXml(Department department, string addOrDel)
        {
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement root = xDoc.Element("Departments");

            if (root != null)
            {
                foreach (XElement _department in root.Elements("Department"))
                {
                    if (department.DepartmentName == _department.Element("Отдел").Value)
                    {
                        if (addOrDel == "add")
                        {
                            string str = (int.Parse(_department.Element("Контингент").Value) + 1).ToString();
                            _department.Element("Контингент").Value = str;
                            break;
                        }
                        if (addOrDel == "del")
                        {
                            string str = (int.Parse(_department.Element("Контингент").Value) - 1).ToString();
                            _department.Element("Контингент").Value = str;
                            break;
                        }
                    }
                }
                xDoc.Save("departments.xml");
            }
        }

        internal void DeleteEmployeeFromXml(Employee employee)
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            IEnumerable<XElement> _employee = xDoc.Root.Descendants("Employee")
                .Where(emp => emp.Element("Отдел")
                .Value == employee.IdDepartment.ToString());

            _employee.Remove();
            xDoc.Save("employees.xml");
        }

        /// <summary>
        /// Метод удаляет отдел из списка всех отделов и возвращает Id удаляемого отдела
        /// </summary>
        /// <param name="departmentName">Название отдела</param>
        /// <returns>Id удаляемого отдела</returns>
        internal Guid DeleteDepartmentFromXml(string departmentName)
        {
            string idDepartment = "";
            XDocument xDocDepartment = XDocument.Load("departments.xml");
            foreach (XElement _department in xDocDepartment.Element("Departments").Elements("Department"))
            {
                if (_department.Element("Отдел").Value == departmentName)
                {
                    idDepartment = _department.Element("Id").Value;
                    _department.Remove();
                    break;
                }
            }
            xDocDepartment.Save("departments.xml");
            return Guid.Parse(idDepartment);
        }

        /// <summary>
        /// Метод удалает из xml-файла всех сотрудников из удаляемого одела
        /// </summary>
        /// <param name="idDepartment">Id отдела</param>
        internal void DeleteDepartmentEmployees(Guid idDepartment)
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            IEnumerable<XElement> tempEmployees = xDoc.Root.Descendants("Employee")
                .Where(t => t.Element("Отдел")
                .Value == idDepartment.ToString());
            tempEmployees.Remove();
            xDoc.Save("employees.xml");
        }

        /// <summary>
        /// Метод переименования отдела в xml-файле
        /// </summary>
        /// <param name="oldDepartmentName">Старое название отдела</param>
        /// <param name="newDepartmentName">Новое название отдела</param>
        internal void RenameDepartmentXml(string oldDepartmentName, string newDepartmentName)
        {
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement root = xDoc.Element("Departments");

            if (root != null)
            {
                foreach (XElement _department in root.Elements("Department"))
                {
                    if(_department.Element("Отдел").Value == oldDepartmentName)
                    {
                        _department.Element("Отдел").Value = newDepartmentName;
                        break;
                    }
                }
                xDoc.Save("departments.xml");
            }
        }
        #endregion

        #region Сортировки
        /// <summary>
        /// Сортировка сотрудников по полю "Возраст"
        /// </summary>
        /// <returns>Отсортированный список</returns>
        internal List<Employee> SortByAge()
        {
            //List<Employee> employees = new List<Employee>();

            var emp = from _employee in Employees
                        orderby _employee.Age
                        select _employee;
            return emp.ToList();
        }

        internal List<Employee> SortBySecondNameThenTotal()
        {
            var emp = from _employee in Employees
                        orderby _employee.SecondName, _employee.Total
                        select _employee;
            return emp.ToList();
        }

        internal List<Employee> SortBySecondNameThenFirstNameThenProjects()
        {
            var emp = from _employee in Employees
                      orderby _employee.SecondName, _employee.FirstName, _employee.Projects
                      select _employee;
            return emp.ToList();
        }

        #endregion

        #region Методы вывода на экран

        internal void ShowAllEmployees()
        {
            Console.WriteLine($"Фамилия       Имя         Возраст    Проектов    Зарплата");
            foreach (Employee employee in Employees)
            {
                Console.WriteLine($"{employee.SecondName}" +
                    $"{employee.FirstName,10}" +
                    $"{employee.Age,10}" +
                    $"{employee.Projects,10}" +
                    $"{employee.Total,15}");
            }
        }

        #endregion
    }
}
