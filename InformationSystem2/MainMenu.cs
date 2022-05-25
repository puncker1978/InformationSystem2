using System;
using System.Collections.Generic;
using System.Text;

namespace InformationSystem2
{
    internal class MainMenu
    {
        //Положение курсора
        private int row;
        private int column;

        //Индекс массива заголовков меню
        private int index;

        //Пункты меню
        internal string[] menuItems = new string[]
        {
            "Поиск отдела",
            "Поиск сотрудника",
            "Добавить новый отдел",
            "Добавить нового сотрудника",
            "Переименовать отдел",
            "Изменить число проектов сотрудника",
            "Удалить сотрудника",
            "Удалить отдел со всеми сотрудниками",
            "Сортировка по полю \"Возраст\"",
            "Сортировка по полю \"Фамиля сотрудника\" и \"Заработная плата сотрудника\"",
            "Сортировка по полю \"Фамилия сотрудника\" и \"Имя сотрудника\" и \"Число проектов\"",
            "Сериализация сотрудников и отделов в json",
            "Десериализация сотрудников и отделов из json",
            "Список всех отделов",
            "Список всех сотрудников",
            "Выход"
        };

        //Индекс массива заголовков меню
        internal int Index { get => index; set => index = value; }

        internal MainMenu()
        {
            this.row = Console.CursorTop;
            this.column = Console.CursorLeft;
            this.index = 0;
        }

        internal void DrawMenu()
        {
            Console.SetCursorPosition(column, row);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(menuItems[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
