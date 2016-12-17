using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 *Lab 7 part 2
 *Name: Sebastian Kruzel
 *Date: 10/12/2015
 */
namespace lab7_starter1
{
    class Vector3
    {
        //get and set of x,y,z
        private float x, y, z;

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        /// <summary>
        /// default constructor, makes null vector
        /// </summary>
        public Vector3()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }

        /// <summary>
        /// Constructor  taking values for x, y and z
        /// </summary
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        public Vector3(float x1, float y1, float z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        /// <summary>
        /// constructor that takes in vector v
        /// </summary>
        /// <param name="v"></param>
        public Vector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        /// <summary>
        /// compute the lenght of a vector by getting the square root of the sum
        /// sum of the squares of the components
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return (Math.Sqrt(x * x + y * y + z * z));
        }

        /// <summary>
        /// return the sum of the product of the corresponding components
        /// dot producct = x1*x2 + y1*y2 + z1*z2
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public double DotProduct(Vector3 v2)
        {
            return (x * v2.x + y * v2.y + z * v2.z);
        }

        /// <summary>
        /// An overloaded operator * to return the product of a scalar by a vector
        /// </summary>
        /// <param name="k"></param>
        /// <param name="V1"></param>
        /// <returns></returns>
        public static Vector3 operator *(double k, Vector3 V1)
        {
            return new Vector3(V1.x * (float)k, V1.y * (float)k, V1.z * (float)k);
        }

        /// <summary>
        /// get a vector of lenght one in same direction
        /// divide vector by the lenght
        /// </summary>
        /// <returns></returns>
        public Vector3 Unit()
        {
            return new Vector3(this.Scale(1 / this.Length()));
        }

        /// <summary>
        /// Projection of vector v2 on the current vector
        ///  = dotproduct(v1,v2) / |v1||v| * v
        ///  same as v1 . v2 /( v1 . v1) *v
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 ParralelComponent(Vector3 v2)
        {
            double lenghtSquared, dotProduct, scale;
            lenghtSquared = DotProduct(this); 
            dotProduct = DotProduct(v2);
            scale = dotProduct / lenghtSquared;
            return new Vector3(this.Scale(scale));
        }

        /// <summary>
        /// method to calculate perpendicular take the vector from its parallel component
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public Vector3 PerpendicularComponent(Vector3 v2)
        {
            return new Vector3(v2 - this.ParralelComponent(v2));
        }

        /// <summary>
        /// casting double to a float using overloaded method
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Vector3 Scale(double scale)
        {
            return Scale((float)scale);
        }

        /// <summary>
        /// multiply all components by a scaler value
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Vector3 Scale(float scale)
        {
            return new Vector3(x * scale, y * scale, z * scale);
        }

        /// <summary>
        /// take in two vectors and calculate the sum of each component
        /// return the sum
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        /// <summary>
        /// take in two vectors and calculate the difference
        /// return the difference
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        /// <summary>
        /// negate each component of a vector
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(-v1.x, -v1.y, -v1.z);
        }

        /// <summary>
        /// flips the X component of the vector by returning a new vector that has just the x component negated
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public void FlipX()
        {
            x = -x;
        }

        /// <summary>
        /// flips the y component of the vector by returning a new vector that has just the y component negated
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public void FlipY()
        {
            y = -y;
        }
    }//end of vector3 class
}//end of namespace
