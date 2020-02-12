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
        //Background
        private static Texture2D background;
        private static Vector2 BGpos;

        //Menu
        static Menu menu;

        //ScoreBoard
        static HighScore highScore;
        static Scoreboard scoreboard;

        //Score
        static int P1Score;
        static int P2Score;

        //Objects

        //Ball
        static Ball ball;

        //PLayers
        static Player player1;
        static Player player2;

        //Seagull Items
        static Seagull seagull;

        static Texture2D[] seagullSprite;
        static Texture2D bulletSprite;

        //Goals
        static Goal goal1;
        static Goal goal2;

        //textures for players
        static Texture2D[] leftP1;
        static Texture2D[] rightP1;

        static Texture2D[] leftP2;
        static Texture2D[] rightP2;

        //Fonts
        static SpriteFont myFont;
        static PrintText printText;

        //Games states
        public enum State { Menu, Run, HightScore, Quit };
        public static State currentState;

        /*################################################################################################*/
                                                    /*INITIALIZE*/
        /*################################################################################################*/
        public static void Initialize()
        {
            //highScore = new HighScore(10);
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            //Background
            background = content.Load<Texture2D>("background");
            BGpos = new Vector2(0, -280);

            //Menu
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("start"), (int)State.Run);
            menu.AddItem(content.Load<Texture2D>("highscore"), (int)State.HightScore);
            menu.AddItem(content.Load<Texture2D>("exit"), (int)State.Quit);

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

            //Seagull sprites
            seagullSprite = new Texture2D[] { content.Load<Texture2D>("seagull1"), content.Load<Texture2D>("seagull2"), content.Load<Texture2D>("seagull3"), content.Load<Texture2D>("seagull4") };
            bulletSprite = content.Load<Texture2D>("bajs");

            //Seagull
            seagull = new Seagull(seagullSprite, -50, 50, 4f, 0, bulletSprite);

            //Font
            printText = new PrintText(content.Load<SpriteFont>("myFont"));

            //Scoreboard
            //highScore.LoadFromFile("highscore.txt");
        }

        /*################################################################################################*/
                                                    /*MENU*/
        /*################################################################################################*/
        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            //Background
            spriteBatch.Draw(background, BGpos, Color.White);

            //Menu
            menu.Draw(spriteBatch);

            //spriteBatch.Draw(menuSprite, menuPos, Color.White); OLD
        }

        /*################################################################################################*/
                                                    /*RUNUPDATE*/
        /*################################################################################################*/
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            //Update Objects
            ball.Update(window, gameTime);

            player1.Update(window, gameTime);
            player2.Update(window, gameTime);


            //Ball kollision
            if (ball.CheckCollision(player1))
                ball.Kollision(player1, window);


            if (ball.CheckCollision(player2))
                ball.Kollision(player2, window);

            //Score player 1
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
                        player1.Reset(46 * 2 + 20, window.ClientBounds.Height);
                        player2.Reset(window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height);
                        ball.Reset(window.ClientBounds.Width / 2 - 15, 100, 0, 0);
                        P2Score++;
                    }
                }
            }

            //Score player 2
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
                        player1.Reset(46 * 2 + 20, window.ClientBounds.Height);
                        player2.Reset(window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height);
                        ball.Reset(window.ClientBounds.Width / 2 - 15, 100, 0, 0);
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

            //Spawning seagulls
            seagull.Update(window, gameTime);

            foreach (Bullet b in seagull.Bullets)
                b.Update();


            if (!seagull.IsAlive)
            {
                Random random = new Random();
                int spawnpoint = random.Next(1, 3);

                if (spawnpoint==1)
                    seagull.Reset(-50, 50, 4f);
                else
                    seagull.Reset(window.ClientBounds.Width+50, 50, -4f);
            }
                

                
            //Enemie hit
            foreach (Bullet b in seagull.Bullets)
            {
                if (b.CheckCollision(player1))
                {
                    player1.freez();

                    if (!seagull.IsAlive)
                        player1.speedReset();
                }

                if (b.CheckCollision(player2))
                {
                    player2.freez();

                    if (!seagull.IsAlive)
                        player2.speedReset();
                }
            }



            //Pause to menu
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return State.Menu;
            }

            //GAME OVER
            if (P1Score == 5)
            {
                //Reset(window, content);
                return State.HightScore;

            }
            else if(P2Score == 5)
            {
                //Reset(window, content);
                return State.HightScore;
            }

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch, GameWindow window)
        {
            //Backhground
            spriteBatch.Draw(background, BGpos, Color.White);

            //Enemie
            seagull.Draw(spriteBatch);

            foreach (Bullet b in seagull.Bullets)
                b.Draw(spriteBatch);

            //Ball
            ball.Draw(spriteBatch);
            
            //Players
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            //goals
            goal1.Draw(spriteBatch);
            goal2.Draw(spriteBatch);

            //Text
            printText.Print($"{P1Score} - {P2Score}", spriteBatch, window.ClientBounds.Width / 2, 100);
        }

        /*################################################################################################*/
                                                    /*LEADERBOARD*/
        /*################################################################################################*/
        public static State HighScoreUpdate(GameTime gameTime, GameWindow window, ContentManager content)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                //Reset(window, content);
                return State.Menu;
            }

            //ADD SCORE
            highScore.EnterUpdate(gameTime, P1Score);
            return State.HightScore;
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            //Backhground
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            highScore.EnterDraw(spriteBatch, myFont);
        }

        /*################################################################################################*/
                                                    /*UNLOAD*/
        /*################################################################################################*/
        public static void UnloadSave()
        {
            //highScore.SaveToFile("highscore.txt");
        }

        /*################################################################################################*/
                                                    /*RESET WINDOW*/
        /*################################################################################################*/
        private static void Reset(GameWindow window, ContentManager content)
        {
            ball.Reset(window.ClientBounds.Width / 2 - 15, 100, 0, 0);
            player1.Reset(46 * 2 + 20, window.ClientBounds.Height);
            player2.Reset(window.ClientBounds.Width - 46 * 3 - 20, window.ClientBounds.Height);

            seagull.Reset(-50, 50, 4f);

            P1Score = 0;
            P2Score = 0;

            foreach (Bullet b in seagull.Bullets.ToList())
                seagull.Bullets.Remove(b);

        }
    }
}
