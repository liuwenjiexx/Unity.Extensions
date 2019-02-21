using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:src/UnityEngine.Extensions/Texture2D.cs
namespace UnityEngine.Extensions
=======
namespace Core.Unity
>>>>>>> e2db7b97206b38fd85e98182bacbdb5df502aa77:src/Unity.Extensions/Texture2D.cs
{
    public static partial class UnityExtensions
    {

        public static void Fill(this Texture2D img, Color color)
        {
            int width = img.width;
            int height = img.height;
            //note5 400x400 42ms
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        img.SetPixel(i, j, backgroundColor);
            //    }
            //}
            //note5 400x400 18ms
            Color[] colors = new Color[width * height];
            colors.Fill(color);
            img.SetPixels(colors);
            //note5 400x400 20ms
            //Color[] colors = img.GetPixels();
            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {
            //        colors[i * width + j] = backgroundColor;
            //    }
            //}
            //img.SetPixels(colors);
        }



        public static Vector2Int LBToTB(this Texture2D img, Vector2Int point)
        {
            point.y = img.height - point.y;
            return point;
        }

        public static Vector2Int TBToLB(this Texture2D img, Vector2Int point)
        {
            point.y = img.height - point.y;
            return point;
        }

        public static void FillRect(this Texture2D img, int x, int y, int width, int height, Color fillColor)
        {
            int canvasWidth = img.width;
            int canvasHeight = img.height;
            y = canvasHeight - y;
            int xMin = x, xMax = x + width, yMin = y - height, yMax = y;
            xMin = Mathf.Max(xMin, 0);
            xMax = Mathf.Min(xMax, canvasWidth);
            yMin = Mathf.Max(yMin, 0);
            yMax = Mathf.Min(yMax, canvasHeight);

            //for (int i = xMin; i < xMax; i++)
            //{
            //    for (int j = yMin; j < yMax; j++)
            //    {
            //        img.SetPixel(i, j, fillColor);
            //    }
            //}
            //img.filterMode = FilterMode.Trilinear;
            int blockWidth = xMax - xMin;
            int blockHeight = yMax - yMin;
            if (blockWidth <= 0 || blockHeight <= 0)
                return;
            Color[] colors = img.GetPixels(xMin, yMin, blockWidth, blockHeight);
            for (int i = 0; i < blockHeight; i++)
            {
                for (int j = 0; j < blockWidth; j++)
                {
                    colors[i * blockWidth + j] = fillColor;
                }
            }
            img.SetPixels(xMin, yMin, blockWidth, blockHeight, colors);

        }

        //public static void FillRect(this Texture2D img,Color[] colors, int x, int y, int width, int height, Color fillColor)
        //{
        //    int canvasWidth = img.width;
        //    int canvasHeight = img.height;
        //    y = canvasHeight - y;
        //    int xMin = x, xMax = x + width, yMin = y - height, yMax = y;
        //    xMin = Mathf.Max(xMin, 0);
        //    xMax = Mathf.Min(xMax, canvasWidth);
        //    yMin = Mathf.Max(yMin, 0);
        //    yMax = Mathf.Min(yMax, canvasHeight);

        //    for (int i = xMin; i < xMax; i++)
        //    {
        //        for (int j = yMin; j < yMax; j++)
        //        {
        //            colors[ img.SetPixel(i, j, fillColor);
        //        }
        //    }

        //    img.SetPixels(colors);

        //}

        public static RectInt ClampRect(this Texture2D img, RectInt rect)
        {
            int width = img.width;
            int height = img.height;
            Vector2Int min = rect.min, max = rect.max;
            min.x = Mathf.Max(min.x, 0);
            min.y = Mathf.Max(min.y, 0);
            max.x = Mathf.Min(max.x, width);
            max.y = Mathf.Min(max.y, height);
            rect.SetMinMax(min, max);
            return rect;
        }

        public static void FillCircle(this Texture2D img, int x, int y, int radius, Color fillColor)
        {
            int width = img.width;
            int height = img.height;
            y = height - y;
            int xMin = x - radius, xMax = x + radius, yMin = y - radius, yMax = y + radius;


            int sqrRadius = radius * radius;
            //for (int i = xMin; i <= xMax; i++)
            //{
            //    for (int j = yMin; j <= yMax; j++)
            //    {
            //        if (i >= 0 && i < width && j >= 0 && j < height && (i - x) * (i - x) + (j - y) * (j - y) <= sqrRadius)
            //        {
            //            img.SetPixel(i, j, fillColor);
            //        }
            //    }
            //}
            //xMin = Mathf.Max(xMin, 0);
            //xMax = Mathf.Min(xMax, width);
            //yMin = Mathf.Max(yMin, 0);
            //yMax = Mathf.Min(yMax, height);
            RectInt rect = new RectInt(xMin, yMin, xMax - xMin, yMax - yMin);
            rect = img.ClampRect(rect);
            int blockWidth = rect.width;
            int blockHeight = rect.height;
            if (blockWidth <= 0 || blockHeight <= 0)
                return;

            xMin = rect.xMin;
            xMax = rect.xMax;
            yMin = rect.yMin;
            yMax = rect.yMax;

            Color[] colors = img.GetPixels(xMin, yMin, blockWidth, blockHeight);
            int blockX, blockY;
            for (int j = yMin; j <= yMax; j++)
            {
                for (int i = xMin; i <= xMax; i++)
                {
                    blockX = i - xMin;
                    blockY = j - yMin;
                    if (blockX >= 0 && blockX < blockWidth && blockY >= 0 && blockY < blockHeight && (i - x) * (i - x) + (j - y) * (j - y) <= sqrRadius)
                    {
                        colors[blockY * blockWidth + blockX] = fillColor;
                    }
                }
            }
            img.SetPixels(xMin, yMin, blockWidth, blockHeight, colors);

        }

        public static void FillCircle(this Color32[] pixels, int width, int height, int x, int y, int radius, Color32 fillColor)
        {
            int xMin = x - radius, xMax = x + radius, yMin = y - radius, yMax = y + radius;
            int sqrRadius = radius * radius;
            if (xMin < 0)
                xMin = 0;
            if (xMax >= width)
                xMax = width - 1;
            if (yMin < 0)
                yMin = 0;
            if (yMax >= height)
                yMax = height - 1;
            int blockWidth = xMax - xMin;
            int blockHeight = yMax - yMin;

            int yStart;
            for (int j = yMin; j <= yMax; j++)
            {
                yStart = j * width;
                for (int i = xMin; i <= xMax; i++)
                {
                    if ((i - x) * (i - x) + (j - y) * (j - y) <= sqrRadius)
                    {
                        pixels[yStart + i] = fillColor;
                    }
                }
            }
        }


        public static void FillCircle2(this Texture2D img, int x, int y, int radius, Color fillColor)
        {
            int width = img.width;
            int height = img.height;
            y = height - y;
            int xMin = x - radius, xMax = x + radius, yMin = y - radius, yMax = y + radius;


            int sqrRadius = radius * radius;
            for (int i = xMin; i <= xMax; i++)
            {
                for (int j = yMin; j <= yMax; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height && (i - x) * (i - x) + (j - y) * (j - y) <= sqrRadius)
                    {
                        img.SetPixel(i, j, fillColor);
                    }
                }
            }

        }
    }
}