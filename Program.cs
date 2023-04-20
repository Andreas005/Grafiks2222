using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;

Raylib.InitWindow(screenwidth, screenheight, "Topdown Game");
Raylib.SetTargetFPS(60);


Texture2D backgroundgamescreen1 = Raylib.LoadTexture("background2.png");
Texture2D background = Raylib.LoadTexture("background2.png");
Texture2D winbackground = Raylib.LoadTexture("background2.png");

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
Rectangle Nextlevelblock2 = new Rectangle(800, 600, 64, 64);







Vector2 enemyMovment = new Vector2(1, 0);
float enemySpeed = 1;

// r  g    b   a  
Color mycolor = new Color(0, 0, 0, 255);


string currentscene = "start";

//   start, game, gameover

string level = "level1";
enemyObject.loadEnemies();


float timer = 0;

bool showNextLevelText = false;

bool pickupp = false;



void coordinates()
{
    enemyrect.x = 500;
    enemyrect.y = 300;
    character.x = 10;
    character.y = 60;
    speed = 9f;
    timer = 0;
    showNextLevelText = false;
}


void level2reset()
{
    timer = 0;
    character.y = 10;
    character.x = 10;
    for (int i = 0; i < enemyObject.enemies.Count; i++)
    {
        enemyEntity enemy = enemyObject.enemies[i];
        enemy.enemyRectangle.y = 760;
    }

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


            enemyMovment = enemydirection * enemySpeed;

            enemyrect.x += enemyMovment.X;
            enemyrect.y += enemyMovment.Y;



            if (Raylib.CheckCollisionRecs(character, enemyrect))
            {

                currentscene = "gameover";
                level = "level1";
            }
            if (Raylib.CheckCollisionRecs(character, Nextlevelblock2) && showNextLevelText)
            {

                currentscene = "congrats";
            }



            if (Raylib.CheckCollisionRecs(character, Nextlevelblock) && showNextLevelText)
            {
                level = "level2";


                // currentscene = "Finallevel";
            }

            // bool pickup  = false; 

            timer += Raylib.GetFrameTime();

            if (timer > 1 && showNextLevelText == false)
            {
                showNextLevelText = true;
            }
        }
        else if (level == "level2")
        {






            if (currentscene == "game")
            {

                if (character.x >= 945 || character.x <= 0 || character.y >= 690 || character.y <= 0)
                {
                    level2reset();
                    currentscene = "gameover";
                    level = "level2";
                }
                // om man går går ut så dör man.


                // om man går ut ur fönstret så dör man      

                if (level == "level2")
                {
                    for (int i = 0; i < enemyObject.enemies.Count; i++)
                    {
                        enemyEntity enemy = enemyObject.enemies[i];
                        if (Raylib.CheckCollisionRecs(enemy.enemyRectangle, character))
                        {
                            currentscene = "gameover";
                            level2reset();
                            break;
                        }

                    }
                    if (Raylib.CheckCollisionRecs(character, Nextlevelblock2) && showNextLevelText)
                    {

                        currentscene = "congrats";
                    }
                    if(Raylib.CheckCollisionRecs(Nextlevelblock2, character)){
                        currentscene = "congrats";
                    }
                }

            }
            if (Raylib.CheckCollisionCircleRec(coinPosition, 20, character))
            {
                speed += 1;

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



            Raylib.ClearBackground(Color.BLACK);
            // (Raylib.CheckCollisionRecs(character, )
            // Raylib.DrawCircle(80, 90, 20,Color.GOLD);




            for (int i = 0; i < enemyObject.enemies.Count; i++)
            {

                enemyEntity enemy = enemyObject.enemies[i];

                enemy.enemyRectangle.y += enemy.speed;

                if (enemy.enemyRectangle.y > screenheight)
                {
                    Console.WriteLine("TURN");
                    enemy.speed = -10;
                }
                if (enemy.enemyRectangle.y < 0)
                {
                    Console.WriteLine("RETURN");
                    enemy.speed = 10;
                }


                Raylib.DrawRectangleRec(enemy.enemyRectangle, Color.RED);
                Raylib.DrawRectangleRec(Nextlevelblock2, Color.PINK);

            }


            if (pickupp == false)
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

       
          Raylib.DrawText("CONGRATS", 100, 300, 128, Color.BLACK);
 
 

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
        Raylib.DrawText("avoid hitting the red rectangle", 250, 600, 30, Color.BLACK);


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




