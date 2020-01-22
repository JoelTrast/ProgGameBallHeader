using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BallHeader
{
    static class GameElements
    {
        //static Texture2D menuSprite;
        //static Vector2 menuPos;
        static Texture2D goldCoinSprite;
        //static Menu menu;
        //static Background background;
        //static HighScore highScore;
        static Ball ball;
        static Player player1;
        static Player player2;

        static Texture2D[] leftP1;
        static Texture2D[] rightP1;

        static Texture2D[] leftP2;
        static Texture2D[] rightP2;

        static Goal goal1;
        static Goal goal2;

        static int P1Score;
        static int P2Score;

        static SpriteFont myFont;
        static PrintText printText;

        static Texture2D background;

        public enum State { Menu, Run, HightScore, Quit };
        public static State currentState;

        public static void Initialize()
        {
            //goldCoins = new List<GoldCoin>(); //skapar en ny Lista med goldCoins
            //highScore = new HighScore(10);
            
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //Player1 sprites
            leftP1 = new Texture2D[] { content.Load<Texture2D>("playerLookingLeft1"), content.Load<Texture2D>("playerLookingLeft2"), content.Load<Texture2D>("playerLookingLeft3") };
            rightP1 = new Texture2D[] { content.Load<Texture2D>("playerLookingRight1"), content.Load<Texture2D>("playerLookingRight2"), content.Load<Texture2D>("playerLookingRight3") };

            //Player2 sprites
            leftP2 = new Texture2D[] { content.Load<Texture2D>("player2left1"), content.Load<Texture2D>("player2left2"), content.Load<Texture2D>("player2left3") };
            rightP2 = new Texture2D[] { content.Load<Texture2D>("player2right1"), content.Load<Texture2D>("player2right2"), content.Load<Texture2D>("player2right3") };            

            //Players
            player1 = new Player(leftP1, rightP1, 46 * 2 + 20, window.ClientBounds.Height, 4f, 0f, true);
            player2 = new Player(leftP2, rightP2, window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height, 4f, 0f, false);

            //Goals
            goal1 = new Goal(content.Load<Texture2D>("Goal"), 0, window.ClientBounds.Height - 186, false);
            goal2 = new Goal(content.Load<Texture2D>("Goal"), window.ClientBounds.Width - 94, window.ClientBounds.Height - 186, true);

            //Ball
            ball = new Ball(content.Load<Texture2D>("ball"), window.ClientBounds.Width / 2 - 15, 100, 0, 0);

            printText = new PrintText(content.Load<SpriteFont>("myFont"));

            /*
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;
            */

            //Hämtar font från content
            //printText = new PrintText(content.Load<SpriteFont>("myFont"));

            /*
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.HightScore);
            menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);
            */
            //background = new Background(content.Load<Texture2D>("background"), window);

            //myFont = content.Load<SpriteFont>("myFont");

            //highScore.LoadFromFile("../../../highscore.txt");
            // TODO: use this.Content to load your game content here
        }


        public static State MenuUpdate(GameTime gameTime)
        {
            return 0; //(State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            //background.Draw(spriteBatch);
            //menu.Draw(spriteBatch);
            //spriteBatch.Draw(menuSprite, menuPos, Color.White);
        }

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            // background.Update(window);

            //Update gameElements
            ball.Update(window, gameTime);

            player1.Update(window, gameTime);
            player2.Update(window, gameTime);


            //Ball kollision
            if (ball.CheckCollision(player1))
                ball.Kollision(player1, window);


            if (ball.CheckCollision(player2))
                ball.Kollision(player2, window);

            //Score
            if (ball.CheckCollision(goal1))
            {
                if (ball.X <= goal1.Width)
                {
                    if (ball.Y + ball.Height >= window.ClientBounds.Height - goal1.Height && ball.Y + ball.Height <= window.ClientBounds.Height - goal1.Height + 10)
                    {
                        ball.GoalKollision(goal1);
                    }
                    else if (ball.X <= goal1.Width / 2)
                    {
                        ball.Reset(window.ClientBounds.Width / 2 - 15, 100, 0, 0);
                        player1.Reset(46 * 2 + 20, window.ClientBounds.Height);
                        player2.Reset(window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height);
                        P2Score++;
                    }
                }
            }

            if (ball.CheckCollision(goal2))
            {
                if(ball.X + ball.Width >= window.ClientBounds.Width - goal2.Width)
                {
                    if (ball.Y + ball.Height >= window.ClientBounds.Height - goal2.Height && ball.Y + ball.Height <= window.ClientBounds.Height - goal2.Height +10)
                    {
                        ball.GoalKollision(goal2);
                    }
                    else if (ball.X +ball.Width>=window.ClientBounds.Width - goal2.Width / 2)
                    {
                        ball.Reset(window.ClientBounds.Width / 2 - 15, 100, 0, 0);
                        player1.Reset(46 * 2 + 20, window.ClientBounds.Height);
                        player2.Reset(window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height);
                        P1Score++;
                    }
                }
            }


            //Player kollision
            if (player1.CheckCollision(player2))
            {
                player1.PlayerKollision();
                player2.PlayerKollision();
            }


            /*
            //GAME OVER
            if (P2Score > P1Score)
            {
                Reset(window, content);
                return State.HightScore;
            }
            */

            return State.Run;
            
            
            /*
            if (!player.IsAlive)
            {
                //Reset(window, content);
                return State.HightScore;
            }
            return State.Run;
            */
        }


        public static void RunDraw(SpriteBatch spriteBatch, GameWindow window)
        {
            //background.Draw(spriteBatch);

            ball.Draw(spriteBatch);

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            goal1.Draw(spriteBatch);
            goal2.Draw(spriteBatch);

            printText.Print($"P1 score: {P1Score} | P2 score: {P2Score}", spriteBatch, window.ClientBounds.Width / 2, 100);
        }

        public static State HighScoreUpdate(GameTime gameTime, GameWindow window, ContentManager content)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                //Reset(window, content);
                return State.Menu;
            }

            //highScore.EnterUpdate(gameTime, player.Points);
            return State.HightScore;
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            //highScore.EnterDraw(spriteBatch, myFont);
        }

        /*
        private static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset(380, 400, 2.5f, 4.5f);

            enemies.Clear();

            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width + tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height + tmpSprite.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }

            tmpSprite = content.Load<Texture2D>("tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width + tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height + tmpSprite.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
        }*/

        public static void UnloadSave()
        {
            //highScore.SaveToFile("../../../highscore.txt");
        }

        /*
        public static State MenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
                return State.Run;
            if (keyboardState.IsKeyDown(Keys.H))
                return State.HightScore;
            if (keyboardState.IsKeyDown(Keys.A))
                return State.Quit;

            return State.Menu;
        }*/


    }
}
