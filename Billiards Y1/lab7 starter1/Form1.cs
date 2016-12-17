/// <summary>
/// Lab 7 Part 2
/// Name: Sebastian Kruzel
/// finished billiards game
/// Date: 10/12/2015
/// estimated time: 20h
/// worked on: [28/12/15 10:00 - 15:00] [30/12/15 12:30 - 18:00] [03/1/16 14:00 - 15:00] [06/1/16 11:30 - 14:00] [08/1/16 12:00 - 20:00] [16/01/2016 12:00 - 14:00]
/// actual time: 24h
/// Extra: added in boxes to show strength of the shot.
/// </summary>
/// 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab7_starter1
{
    public partial class Form1 : Form
    {
        int score = 0;
        float width = 1000;
        float height = 700;
        float diameter = 30;
        bool drawDirectionLine; // to draw or not
        bool moveBall; // update ball location
        bool playerOneTurn = true;
        bool playerTwoTurn = false;
        bool cueBallHit = false;
        bool yellowBallHit = false;
        bool redBallHit = false;
        bool strengthOneDisplay = false;
        bool strengthTwoDisplay = false;
        bool strengthThreeDisplay = false;
        bool strengthFourDisplay = false;
        Vector3 cueBallCentre = new Vector3(200, 250, 0); //position of the red ball vector
        Vector3 redBallCentre = new Vector3(400, 250, 0); //position of the cue ball vector
        Vector3 yellowBallCentre = new Vector3(600, 250, 0);    //position of the yellow ball
        Vector3 topLeftHoleCentre = new Vector3(15, 15, 0);
        Vector3 topMiddleHoleCentre = new Vector3(400, 15, 0);
        Vector3 topRightHoleCentre = new Vector3(785, 15, 0);
        Vector3 bottomLeftHoleCentre = new Vector3(15, 470, 0);
        Vector3 bottomMiddleHoleCentre = new Vector3(400, 470, 0);
        Vector3 bottomRightHoleCentre = new Vector3(785, 470, 0);
        Vector3 mousePosition = new Vector3(); //position of the mouse vector
        Vector3 redBallVelocity = new Vector3();   //velocity of the cue ball
        Vector3 cueBallVelocity = new Vector3();   //velocity of the red ball
        Vector3 yellowBallVelocity = new Vector3(); //velocity of the yellow ball
        Vector3 topLeft = new Vector3(5, 5, 0); // blue rectangle
        Vector3 radius = new Vector3(); //radius vector
        RectangleF boundingRect;
        /// <summary>
        /// Setup from and variables, place ball at location 200,300
        /// set radius as half diameter
        /// set drawing line to false
        /// sset ball moving to false
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            width = this.Width - 30;
            height = this.Height - 50;
            radius = new Vector3 (diameter / 2, diameter / 2, 0);
            drawDirectionLine = false;
            moveBall = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
        /// <summary>
        /// Draw the game start with a blue box the size of the form (minus our border)
        /// Draw the line (cue) is setting up a shoot
        /// and then draw the ball
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Create pens in blue & black
            Pen bluePen = new Pen(Color.Blue, 2.5f);
            Pen blackPen = new Pen(Color.Black, 4f);
            Brush greenBrush = new SolidBrush(Color.ForestGreen);
            Brush yellowBrush = new SolidBrush(Color.Yellow);
            // Create a rectangle for the sides of the box
            Rectangle cushionRect = new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)width, (int)height);
            //create rectangle for the color of the table
            Rectangle tableRect = new Rectangle((int)0, (int)0, (int)width + 30, (int)height + 50);
            //draw the color of table rectangle
            e.Graphics.FillRectangle(greenBrush, tableRect);
            // Draw the box
            e.Graphics.DrawRectangle(bluePen, cushionRect);
            //draw the shot line
            if (drawDirectionLine == true)
            {//check who's turn it is and draw line accordingly
                if (playerOneTurn == true)
                {
                    e.Graphics.DrawLine(blackPen, mousePosition.X, mousePosition.Y, cueBallCentre.X, cueBallCentre.Y);
                }
                if (playerTwoTurn == true)
                {
                    e.Graphics.DrawLine(blackPen, mousePosition.X, mousePosition.Y, yellowBallCentre.X, yellowBallCentre.Y);
                }
                
            }
            //draw the playing balls
            DrawBall(e, cueBallCentre, Color.PapayaWhip);
            DrawBall(e, redBallCentre, Color.Red);
            DrawBall(e, yellowBallCentre, Color.Yellow);
            //draw all the holes
            DrawBall(e, topLeftHoleCentre, Color.Black);
            DrawBall(e, topMiddleHoleCentre, Color.Black);
            DrawBall(e, topRightHoleCentre, Color.Black);
            DrawBall(e, bottomLeftHoleCentre, Color.Black);
            DrawBall(e, bottomMiddleHoleCentre, Color.Black);
            DrawBall(e, bottomRightHoleCentre, Color.Black);
            //if the strength lines are true draw them
            if (strengthOneDisplay == true)
            {
                Rectangle strengthOneBox = new Rectangle((int)width - 50, (int)height - 60, 20, 20);
                e.Graphics.FillRectangle(yellowBrush, strengthOneBox);
            }
            if (strengthTwoDisplay == true)
            {
                Rectangle strengthTwoBox = new Rectangle((int)width - 80, (int)height - 60, 20, 20);
                e.Graphics.FillRectangle(yellowBrush, strengthTwoBox);
            }
            if (strengthThreeDisplay == true)
            {
                Rectangle strengthThreeBox = new Rectangle((int)width - 110, (int)height - 60, 20, 20);
                e.Graphics.FillRectangle(yellowBrush, strengthThreeBox);
            }
            if (strengthFourDisplay == true)
            {
                Rectangle strengthFourBox = new Rectangle((int)width - 140, (int)height - 60, 20, 20);
                e.Graphics.FillRectangle(yellowBrush, strengthFourBox);
            }
        }
        /// <summary>
        /// Single method for drawing any ball present in the game, passing in the paint event
        /// arguments, a vector as well as a colour
        /// </summary>
        /// <param name="e"></param>
        /// <param name="V"></param>
        /// <param name="color"></param>
        private void DrawBall(PaintEventArgs e, Vector3 V, Color color)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush brush = new SolidBrush(color);

            Vector3 position = new Vector3(V.X - radius.X, V.Y - radius.Y, V.Z - radius.Z);
            boundingRect = new RectangleF(position.X, position.Y, diameter, diameter);
            e.Graphics.DrawEllipse(blackPen, boundingRect);
            e.Graphics.FillEllipse(brush, boundingRect);
        }
        /// <summary>
        /// Start of player interaction 
        /// set moveBall and drawDirection Line Booleans
        /// control the drawing mode.
        /// assign mouse vector 
        /// set balls hit to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (redBallHit && yellowBallHit || redBallHit && cueBallHit)
            {
                //cannon
                if (playerOneTurn == true)
                {
                    score += 2;
                    lblScoreWhite.Text = "White Score = " + score;
                    ResetHits();
                }
                //cannon
                if (playerTwoTurn == true)
                {
                    score += 2;
                    lblScoreYellow.Text = "Yellow Score = " + score;
                    ResetHits();
                }
            }
            //if statements to see how many boxes representing strength of the shot should be displayed
            if (playerOneTurn)  //for turn one
            {
                if (CheckHitStrength(cueBallCentre) > 50)
                {
                    strengthOneDisplay = true;
                }
                if (CheckHitStrength(cueBallCentre) > 100)
                {
                    strengthTwoDisplay = true;
                }
                if (CheckHitStrength(cueBallCentre) > 200)
                {
                    strengthThreeDisplay = true;
                }
                if (CheckHitStrength(cueBallCentre) > 300)
                {
                    strengthFourDisplay = true;
                }
            }
            if (playerTwoTurn)  //for turn two
            {
                if (CheckHitStrength(yellowBallCentre) > 50)
                {
                    strengthOneDisplay = true;
                }
                if (CheckHitStrength(yellowBallCentre) > 100)
                {
                    strengthTwoDisplay = true;
                }
                if (CheckHitStrength(yellowBallCentre) > 200)
                {
                    strengthThreeDisplay = true;
                }
                if (CheckHitStrength(yellowBallCentre) > 200)
                {
                    strengthFourDisplay = true;
                }
            }
            mousePosition = new Vector3(mousePosition.X, mousePosition.Y, 0);
            drawDirectionLine = true;
            moveBall = false;
        }
        /// <summary>
        /// player is dragging mouse so update our mouse co-ords
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
        }
        /// <summary>
        /// When the player releases the mouse workout the
        /// line segment specified by the location of the ball to the last
        /// know sighting of the mouse. Normalise this so it's length is one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //reset the display of strength boxes when mouse is up
            drawDirectionLine = false;
            strengthOneDisplay = false;
            strengthTwoDisplay = false;
            strengthThreeDisplay = false;
            strengthFourDisplay = false;
            //set move ball to true
            moveBall = true;
            //move cue ball if player one's turn
            if (playerOneTurn == true)
            {
                cueBallVelocity = cueBallCentre - mousePosition;
                // normalise the velocity of the cue ball
                cueBallVelocity = 0.1 * cueBallVelocity;
                playerTwoTurn = true;
                playerOneTurn = false;
            }
            //move yellow ball if player two's turn
            else if (playerTwoTurn == true)
            {
                yellowBallVelocity = yellowBallCentre - mousePosition;
                // normalise the velocity of the cue ball
                yellowBallVelocity = 0.1 * yellowBallVelocity;
                playerOneTurn = true;
                playerTwoTurn = false;
            }
        }
        /// <summary>
        /// apply friction to a ball
        /// </summary>
        /// <param name="ballVelocity"></param>
        /// <returns></returns>
        private Vector3 Friction(Vector3 ballVelocity)
        {
            ballVelocity = 0.99 * ballVelocity;
            if (ballVelocity.X * ballVelocity.X + ballVelocity.Y * ballVelocity.Y < 1)
            {//if lower than 1 stop the ball
                ballVelocity = new Vector3(0, 0, 0);
            }
            return ballVelocity;
        }
        /// <summary>
        /// if the ball is moving (not aiming) then update it's location
        /// and check it's at the edge of the box if so bounce by negating
        /// the  direction.X on left right boundrys and they direction.Y on
        /// top and bottom boundries
        /// </summary>
        internal void UpdateWorld()
        {
            //check if the balls are within the boundaries of the table
            CheckBounds(cueBallCentre, cueBallVelocity);
            CheckBounds(redBallCentre, redBallVelocity);
            CheckBounds(yellowBallCentre, yellowBallVelocity);

            if (moveBall == true)    //move the ball
            {
                cueBallCentre += cueBallVelocity;
                redBallCentre += redBallVelocity;
                yellowBallCentre += yellowBallVelocity;
            }
            //applying the friction
            cueBallVelocity = Friction(cueBallVelocity);
            redBallVelocity = Friction(redBallVelocity);
            yellowBallVelocity = Friction(yellowBallVelocity);
            //check if rectangles are overlapping
            RectangleCollisionCheck(cueBallCentre, redBallCentre);
            RectangleCollisionCheck(cueBallCentre, yellowBallCentre);
            RectangleCollisionCheck(redBallCentre, yellowBallCentre);
            //if they are then check for circle collision and if they collide preform the elastic collisions as play a sound
            //set up a temp vector that will switch the ball velocities(INSERT ELASTIC COLLISIONS HERE)
            //set balls hit to true
            if (CheckBallCollision(redBallCentre, cueBallCentre) == true)
            {//between red and cue balls
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"hit.wav");
                player.Play();
                ElasticCollisions(ref cueBallVelocity,ref redBallVelocity ,ref cueBallCentre ,ref redBallCentre);
                redBallHit = true;
                cueBallHit = true;
            }
            if (CheckBallCollision(redBallCentre, yellowBallCentre) == true)
            {//between ted and yellow balls
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"hit.wav");
                player.Play();
                ElasticCollisions(ref redBallVelocity, ref yellowBallVelocity,ref redBallCentre,ref yellowBallCentre);
                redBallHit = true;
                yellowBallHit = true;
            }
            if (CheckBallCollision(cueBallCentre, yellowBallCentre) == true)
            {//between cue and yellow balls
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"hit.wav");
                player.Play();
                ElasticCollisions(ref yellowBallVelocity, ref cueBallVelocity,ref yellowBallCentre,ref cueBallCentre);
                cueBallHit = true;
                yellowBallHit = true;
            }
            //checking for pot of cue ball in each hole (white ball potted)
            if (CheckPot(cueBallCentre, topLeftHoleCentre) == true || CheckPot(cueBallCentre, topMiddleHoleCentre) == true || CheckPot(cueBallCentre, topRightHoleCentre) == true
             || CheckPot(cueBallCentre, bottomLeftHoleCentre) == true || CheckPot(cueBallCentre, bottomMiddleHoleCentre) == true || CheckPot(cueBallCentre, bottomRightHoleCentre) == true)
            {
                //if potted add appropriate score
                //pot
                if (playerTwoTurn == true)
                {
                    score += 2;
                    lblScoreYellow.Text = "Yellow Score = " + score;
                    ResetHits();
                }
                //inoff white off yellow
                if (playerOneTurn == true && yellowBallHit == true)
                {
                    score += 2;
                    lblScoreWhite.Text = "White Score = " + score;
                    ResetHits();
                }
                //inoff white off red
                else if (playerOneTurn == true && redBallHit == true)
                {
                    score += 3;
                    lblScoreWhite.Text = "White Score = " + score;
                    ResetHits();
                }
                cueBallCentre = new Vector3(200, 250, 0);   //reset position
                cueBallVelocity = new Vector3();    //reset velocity
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"drop.wav");
                player.Play();
            }
            //checking for pot of yellow ball in each hole (yellow ball potted)
            if (CheckPot(yellowBallCentre, topLeftHoleCentre) == true || CheckPot(yellowBallCentre, topMiddleHoleCentre) == true || CheckPot(yellowBallCentre, topRightHoleCentre) == true
             || CheckPot(yellowBallCentre, bottomLeftHoleCentre) == true || CheckPot(yellowBallCentre, bottomMiddleHoleCentre) == true || CheckPot(yellowBallCentre, bottomRightHoleCentre) == true)
            {
                //if yellow ball is potted add appropriate score
                //pot
                if (playerOneTurn)
                {
                    score += 2;
                    lblScoreWhite.Text = "White Score = " + score;
                    ResetHits();
                }
                //inoff yellow off white
                if (playerTwoTurn && cueBallHit)
                {
                    score += 2;
                    lblScoreYellow.Text = "Yellow Score = " + score;
                    ResetHits();
                }
                //inoff yellow off red
                else if (playerTwoTurn && redBallHit)
                {
                    score += 3;
                    lblScoreYellow.Text = "Yellow Score = " + score;
                    ResetHits();
                }
                yellowBallCentre = new Vector3(600, 250, 0);
                yellowBallVelocity = new Vector3();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"drop.wav");
                player.Play();
            }
            //checking for pot of red ball in each hole (red ball potted)
            if (CheckPot(redBallCentre, topLeftHoleCentre) == true || CheckPot(redBallCentre, topMiddleHoleCentre) == true || CheckPot(redBallCentre, topRightHoleCentre) == true
             || CheckPot(redBallCentre, bottomLeftHoleCentre) == true || CheckPot(redBallCentre, bottomMiddleHoleCentre) == true || CheckPot(redBallCentre, bottomRightHoleCentre) == true)
            {
                redBallCentre = new Vector3(400, 250, 0);
                redBallVelocity = new Vector3();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"drop.wav");
                player.Play();
            }
        }

        /// <summary>
        /// check the ball location against the edge
        /// swap direction, reset ball location next to cushion so
        /// it can move away next frame freely
        /// do this for each edge
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="velocity"></param>
        private void CheckBounds(Vector3 ball, Vector3 velocity)
        {
            if (ball.X < topLeft.X + radius.X)
            {   //if the ball hits the left flip x direction and reset position of the ball to be inside the table and play sound
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"cushion.wav");
                player.Play();
                velocity.FlipX();
                ball = new Vector3(topLeft.X + radius.X, ball.Y, ball.Z);
            }
            if (ball.X > topLeft.X + width - radius.X)
            {   //if the ball hits the right flip x direction and reset position of the ball to be inside the table and play sound
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"cushion.wav");
                player.Play();
                velocity.FlipX();
                ball = new Vector3(topLeft.X + width - radius.X, ball.Y, ball.Z);
            }
            if (ball.Y < topLeft.Y + radius.Y)
            {//if the ball hits the top flip y direction and reset position of the ball to be inside the table and play sound
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"cushion.wav");
                player.Play();
                velocity.FlipY();
                ball = new Vector3(ball.X, topLeft.Y + radius.Y, ball.Z);
            }
            if (ball.Y > topLeft.Y + height - radius.Y)
            {//if the ball hits the bottom flip y direction and reset position of the ball to be inside the table and play sound
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"cushion.wav");
                player.Play();
                velocity.FlipY();
                ball = new Vector3(ball.X, topLeft.Y + height - radius.Y, ball.Z);
            }
        }
        /// <summary>
        /// Adjust our bounding box to match the from size, take onto account the different borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            width = this.Width - 30;
            height = this.Height - 50;
        }
        /// <summary>
        /// Checks if the bounding boxes of the balls are colliding
        /// </summary>
        /// <returns></returns>
        private bool RectangleCollisionCheck(Vector3 ballOne, Vector3 ballTwo)
        {
            if ((ballOne.X < ballTwo.X - diameter || ballOne.X > ballTwo.X + diameter || (ballOne.Y > ballTwo.Y + diameter || ballOne.Y < ballTwo.Y - diameter)))
            {
                return false;
            }
            else
            {
                return CheckBallCollision(ballOne, ballTwo);
            }
        }//end rectangle collision
        /// <summary>
        /// Ball collision check
        /// </summary>
        /// <returns></returns>
        private Boolean CheckBallCollision(Vector3 ballOne, Vector3 ballTwo)
        {
            if (diameter * diameter > (ballOne.X - ballTwo.X) * (ballOne.X - ballTwo.X) + (ballOne.Y - ballTwo.Y) * (ballOne.Y - ballTwo.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check the distance between the centres of two balls in order to see if potted
        /// instead of using square root, square distance between the balls
        /// square the radius to balance the equation [(x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) = radius * radius]
        /// check if the distance squared is smaller than the radius squared
        /// </summary>
        /// <param name="ballOne"></param>
        /// <param name="ballTwo"></param>
        /// <returns></returns>
        private Boolean CheckPot(Vector3 ballOne, Vector3 ballTwo)
        {
            float distanceSquared = 0;
            distanceSquared = (ballOne.X - ballTwo.X) * (ballOne.X - ballTwo.X) + (ballOne.Y - ballTwo.Y) * (ballOne.Y - ballTwo.Y);
                if ((diameter/2) * (diameter/2) > distanceSquared)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        /// <summary>
        /// reset the balls that were hit this round
        /// </summary>
        private void ResetHits()
        {
            redBallHit = false;
            yellowBallHit = false;
            cueBallHit = false;
        }
        /// <summary>
        /// elastic collisions method, passing in the velocities and positions of two balls by reference
        /// calculate the normal between the two balls by taking away the two positions.
        /// normalise the normal by using the Unit method.
        /// get the parrallel component of the velocity first and second ball
        /// get the perpendicular component of the normal and second velocity
        /// add the parallel component of the second balls velocity to the projection of the first and vice versa to get the actual velocity after collision.
        /// </summary>
        /// <param name="velocityOne"></param>
        /// <param name="velocityTwo"></param>
        /// <param name="ballOne"></param>
        /// <param name="ballTwo"></param>
        private void ElasticCollisions(ref Vector3 velocityOne,ref Vector3 velocityTwo,ref Vector3 ballOne,ref Vector3 ballTwo)
        {
            Vector3 collisionNormal;
            Vector3 parallelComponentOne; 
            Vector3 projectionOne;
            Vector3 parallelComponentTwo;
            Vector3 projectionTwo;

            collisionNormal = ballOne - ballTwo;    //getting the normal plane
            collisionNormal.Unit();     //using unit method to normalise the normal
            parallelComponentOne = collisionNormal.ParralelComponent(velocityOne);       //getting the parallel component of velocity of the first ball
            parallelComponentTwo = collisionNormal.ParralelComponent(velocityTwo);      //getting parallel component of the velocity of the second ball
            projectionOne = collisionNormal.PerpendicularComponent(velocityOne);        //getting the perpendicular component of the first velocity
            projectionTwo = collisionNormal.PerpendicularComponent(velocityTwo);        //getting the perpendicular component of the second velocity
            //adding corresponding components after collision
            velocityOne = parallelComponentTwo + projectionOne;     
            velocityTwo = parallelComponentOne + projectionTwo;
        }
        /// <summary>
        /// a method that uses root of (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) to get the length of the line drawn to determine strength of a shot.
        /// the method takes in a vector of a ball position it then returns the value
        /// </summary>
        /// <param name="ballPosition"></param>
        /// <returns></returns>
        private double CheckHitStrength(Vector3 ballPosition)
        {
            double length = 0;
            length = Math.Sqrt((ballPosition.X - mousePosition.X) * (ballPosition.X - mousePosition.X) + (ballPosition.Y - mousePosition.Y) * (ballPosition.Y - mousePosition.Y));
            return length;
        }
    }//end form1
}//end namespace
