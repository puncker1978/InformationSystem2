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
            "Сортировка по полю \"Имя сотрудника\" и \"Фамилия сотрудника\"",
            "Сортировка по полю \"Имя сотрудника\" и \"Фамилия сотрудника\" и \"Число проектов\"",
            "Выход"
        };

        internal int Row { get => row; set => row = value; }
        internal int Column { get => column; set => column = value; }
        internal int Index { get => index; set => index = value; }

        internal MainMenu()
        {
            this.Row = Console.CursorTop;
            this.Column = Console.CursorLeft;
            this.Index = 0;
        }

        internal void DrawMenu()
        {
            Console.SetCursorPosition(Column, Row);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == Index)
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
