using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1024, 768, "Topdown Game");
Raylib.SetTargetFPS(60);


Texture2D backgroundgamescreen1 = Raylib.LoadTexture("background2.png");
Texture2D background = Raylib.LoadTexture("background2.png");


float speed = 9f;

Random generator = new();

/*

Om speed är för snabb
  Sänk speeden med litegrann
Flytta positionen med speeden

*/

Texture2D avatarImage = Raylib.LoadTexture("avatar.png");



float bitchx = generator.Next(0, 1025);
float bitchy = generator.Next(0, 769);
Vector2 coinPosition = new Vector2(bitchx, bitchy);


// generera nytt x-värde
// generera nytt y-värde
// coinPosition = new Vector2(x, y)

// Circle goldcoins == new Circle(80,90,20,Color.GOLD);

Rectangle character = new Rectangle(10, 60, avatarImage.width, avatarImage.height);
Rectangle enemyrect = new Rectangle(700, 500, 64, 64);
Rectangle Nextlevelblock = new Rectangle(500, 0, 64, 64);
Rectangle enemyrect2 = new Rectangle(700, 500, 64, 64);
Rectangle enemyrect3 = new Rectangle(900, 500, 64, 64);

List<Rectangle> enemies = new List<Rectangle>();

enemies.Add(enemyrect2);
enemies.Add(enemyrect3);


// r  g    b   a  
Color mycolor = new Color(0, 0, 0, 255);


string currentscene = "start";

//   start, game, gameover

string level = "level1";

Vector2 enemyMovment = new Vector2(1, 0);
float enemyspeed = 4;

float timer = 0;

bool showNextLevelText = false;


bool plockaupp = false;


void coordinates()
{
    enemyrect.x = 500;
    enemyrect.y = 300;
    character.x = 10;
    character.y = 60;
    timer = 0;
    showNextLevelText = false;
}



while (Raylib.WindowShouldClose() == false)
{

    // logik

    if (currentscene == "game")
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))

        {
            character.x += speed;
        }


        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))

        {
            character.x -= speed;
        }



        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))

        {
            character.y += speed;

        }


        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))

        {
            character.y -= speed;

        }


        /*
            om spelaren kommit för långt åt höger
              flytta tillbaka spelaren åt vänster
        */

        if (level == "level1")
        {

            if (currentscene == "game")
            {

                if (character.x >= 945)
                {
                    currentscene = "gameover";
                }
                else if (character.x <= 0)
                {
                    currentscene = "gameover";
                }
                else if (character.y >= 500)
                {
                    currentscene = "gameover";
                }
                else if (character.y <= 0)
                {
                    currentscene = "gameover";
                }

            }

            Vector2 playerpos = new Vector2(character.x, character.y);
            Vector2 enemypos = new Vector2(enemyrect.x, enemyrect.y);

            Vector2 diff = playerpos - enemypos;


            Vector2 enemydirection = Vector2.Normalize(diff);


            enemyMovment = enemydirection * enemyspeed;

            enemyrect.x += enemyMovment.X;
            enemyrect.y += enemyMovment.Y;



            if (Raylib.CheckCollisionRecs(character, enemyrect))
            {

                currentscene = "gameover";

            }

            if (Raylib.CheckCollisionRecs(character, Nextlevelblock))
            {
                level = "level2";
                // currentscene = "Finallevel";
            }

            // bool pickup  = false; 

            timer += Raylib.GetFrameTime();

            if (timer > 5 && showNextLevelText == false)
            {
                showNextLevelText = true;
            }
        }
        else if (level == "level2")
        {




            if (currentscene == "game")
            {

                if (character.x >= 945)
                {
                    currentscene = "gameover";
                }
                else if (character.x <= 0)
                {
                    currentscene = "gameover";
                }
                else if (character.y >= 690)
                {
                    currentscene = "gameover";
                }
                else if (character.y <= 0)

                // om man går går ut så dör man.

                {
                    currentscene = "gameover";
                }
                // om man går ut ur fönstret så dör man            

            }
            if (Raylib.CheckCollisionCircleRec(coinPosition, 20, character))
            {
                speed += 10;

                bitchx = generator.Next(0, 1025);
                bitchy = generator.Next(0, 769);
                coinPosition.X = bitchx;
                coinPosition.Y = bitchy;
                // plockaupp = true;
            }




        }

    }
    else if (currentscene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "game";
        }
    }
    else if (currentscene == "gameover")
    {

        coordinates();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "start";
        }

    }


    // grafik

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);




    if (currentscene == "game")
    {

        if (level == "level1")
        {
            Raylib.DrawTexture(backgroundgamescreen1, 0, 0, Color.WHITE);
            Raylib.DrawRectangleRec(enemyrect, Color.RED);
            if (showNextLevelText == true)
            {
                Raylib.DrawText("By touching the black block you will get to the next level", 100, 200, 32, mycolor);
                Raylib.DrawRectangleRec(Nextlevelblock, Color.BLACK);
            }
        }


        else if (level == "level2")
        {
            // (Raylib.CheckCollisionRecs(character, )
            // Raylib.DrawCircle(80, 90, 20,Color.GOLD);

            if (plockaupp == false)
            {

                Raylib.DrawCircleV(coinPosition, 20, Color.GOLD);
            }


        }



       

        Raylib.DrawTexture(avatarImage,
         (int)character.x,
          (int)character.y,
              Color.WHITE);



    }


    else if (currentscene == "congrats")
    {

        Raylib.DrawTexture(background, 0, 0, Color.WHITE);



        coordinates();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentscene = "start";
        }





    }



    else if (currentscene == "start")

    {

        Raylib.DrawTexture(background, 0, 0, Color.WHITE);

        Raylib.DrawText(" Press ENTER to start", 250, 100, 32, mycolor);
         Raylib.DrawText("dont touch the grass or the walls or = gameover", 250, 300, 30, Color.BLACK);
         Raylib.DrawText("you move by pressing w,a,s,d", 250, 400, 30, Color.BLACK);
         Raylib.DrawText("avoid hitting the red rectangle", 250, 400, 30, Color.BLACK);


    }

    else if (currentscene == "gameover")
    {

        Raylib.DrawText("GAME OVER", 100, 300, 128, Color.BLACK);
    }


    Raylib.EndDrawing();

}

/*
x Begränsa förflyttning

 x rita ut saker för level2

x Mynt
x  Som man kan plocka upp
Flera mynt
Som spawnas då och då


Fiende
Flera fiender
Som rör sig
När man nuddar = dör
*/




