using System;
using Raylib_cs;
using System.Numerics;

class enemyEntity
{
    public Rectangle enemyRectangle;
    public int speed = 10;
}

class enemyObject
{
    
    public static int enemyAmount = 5;
    public static List<enemyEntity> enemies = new();
    public static void loadEnemies()
    {
        enemies.Clear();
        for (int i = 0; i < enemyAmount; i++)
        {
            enemies.Add(new enemyEntity()
            {
                enemyRectangle = new Rectangle(i * 200 + 50, 760, 64, 64)
            });
        }

    }

}

