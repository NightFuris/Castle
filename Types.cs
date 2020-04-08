using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Types
{
    public class Cell
    {
        /// <summary>
        /// Тип
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Здание
            /// </summary>
            Building,
            /// <summary>
            /// Природа
            /// </summary>
            Nature
        }
        
        /// <summary>
        /// Активность (вкл и выкл)
        /// </summary>
        public enum State
        {
            Enable,
            Disable
        }
        
        /// <summary>
        /// Видимость
        /// </summary>
        public static class Visibility
        {
            public enum Types
            {
                /// <summary>
                /// 1
                /// </summary>
                Good,
                /// <summary>
                /// 2
                /// </summary>
                Medium,
                /// <summary>
                /// 3
                /// </summary>
                Poorly,
                /// <summary>
                /// 4
                /// </summary>
                Passed,
                /// <summary>
                /// 5
                /// </summary>
                CanNotSee
            }
            public static Color GetColor(Visibility.Types visibility)
            {
                Color color = Color.black;
                switch (visibility)
                {
                    case Visibility.Types.Good:
                        color = Color.white;
                        break;
                    case Visibility.Types.Medium:
                        color = new Color(0.772549f, 0.772549f, 0.772549f);
                        break;
                    case Visibility.Types.Poorly:
                        color = new Color(0.4352941f, 0.4352941f, 0.4352941f);
                        break;
                    case Visibility.Types.Passed:
                        color = new Color(0.282353f, 0.282353f, 0.282353f);
                        break;
                    case Visibility.Types.CanNotSee:
                        color = Color.black;
                        break;
                }
                return color;
            }
        }
    }

    /// <summary>
    /// Постройки
    /// </summary>
    public class Building 
    {
        /// <summary>
        /// Тип здания
        /// </summary>
        public enum Types
        {
            None,
            /// <summary>
            /// Ферма
            /// </summary>
            Farmhouse,
            /// <summary>
            /// Шахта
            /// </summary>
            Mine,
            /// <summary>
            /// Хижина пастухов
            /// </summary>
            ShepertsHut,
            /// <summary>
            /// Хижина
            /// </summary>
            House,
            /// <summary>
            /// Поле
            /// </summary>
            Fields,
            /// <summary>
            /// Мельница
            /// </summary>
            Mill,
            /// <summary>
            /// Лесопилка
            /// </summary>
            Sawmill,
            /// <summary>
            /// Замок
            /// </summary>
            Castle
        }
    }
    
    /// <summary>
    /// Земля
    /// </summary>
    public class Nature
    {
        public enum Types
        {
            None,
            /// <summary>
            /// Поле
            /// </summary>
            Meadow,
            /// <summary>
            /// Лес
            /// </summary>
            Forest,
            /// <summary>
            /// Горы
            /// </summary>
            Mountains,
        }
        /// <summary>
        /// Метки
        /// </summary>
        public enum PointNature
        {
            None,
            Mine,
            Field,
            Forest,
            Castle
        }
    }

    /// <summary>
    /// GUI
    /// </summary>
    public class GUI
    {
        public enum SwitchButton
        {
            Building,
            Soldiers
        }
    }

    /// <summary>
    /// Ресурсы
    /// </summary>
    public class Resources
    {
        public Sprite icon;
        public Types type = Types.None;
        public int num = 0;
        public string Name
        {
            get
            {
                switch (type)
                {
                    case Types.Money:
                        return "Деньги";
                    case Types.Tree:
                        return "Дерево";
                    case Types.Stone:
                        return "Камень";
                    default:
                        Debug.LogError("ERROR RESOURSE");
                        return "ERROR RESOURSE";
                }
            }
        }
        public enum Types
        {
            None,
            /// <summary>
            /// Деньги
            /// </summary>
            Money,
            /// <summary>
            /// Дерево
            /// </summary>
            Tree,
            /// <summary>
            /// Камень
            /// </summary>
            Stone 
        }
        public Resources(Types type, int num)
        {
            this.type = type;
            this.num = num;
        }
        public Resources(Types type, int num, Sprite icon)
        {
            this.type = type;
            this.num = num;
            this.icon = icon;
        }
    }
    
    /// <summary>
    /// Солдаты
    /// </summary>
    public class Soldier
    {
        public enum Types
        {
            None,
            Knight
        }
    }
}
