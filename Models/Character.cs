﻿using System.Reflection.Metadata.Ecma335;

namespace Estudos.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string? Name { get; set; } = "'Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public Rpgclass Class { get; set; } = Rpgclass.Knight;

    }
}
