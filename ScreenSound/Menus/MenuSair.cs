﻿using ScreenSound.db;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}