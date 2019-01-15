using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    class GraphicService : IGraphicService
    {
        public void GetGraphic(List<CurrencyRateDTO> rates)
        {
            Bitmap bitmap = new Bitmap(Convert.ToInt32(2160), Convert.ToInt32(2048), PixelFormat.Format32bppArgb);
            Graphics graph = Graphics.FromImage(bitmap);

            DrawAxes(graph);

            //це вся відстань осі Х
            TimeSpan MainSpan = (rates[rates.Count - 1].UpdatedAt - rates[0].UpdatedAt);

            //шукаються точки і рисується дата на осі Х
            //1980 - початок осі y знизу
            //1945 - довжина осі x
            PointF[] points = new PointF[rates.Count];
            int j = 0;
            foreach (var item in rates)
            {
                float a = (item.UpdatedAt - rates[0].UpdatedAt).Days;
                float b = a / MainSpan.Days;
                points[j] = new PointF( b * 1945 + 55, (float)(1980 - item.Rate * 2));

                graph.DrawString(item.UpdatedAt.ToString("dd/MM/yyyy"), new Font("Arial", 20), new SolidBrush(Color.Black), new PointF(b * 1945 + 30, 1990));
                j++;
            }
            graph.DrawLines(new Pen(Color.Black, 2), points);

            CurrencyDTO cur = new CurrencyDTO();
            cur.ImageUrl = @"E:\Рабочий стол\Програмування\Галушко\CurrencyBot\test.png";
            bitmap.Save(cur.ImageUrl, ImageFormat.Png);
        }

        public void DrawAxes(Graphics graph)
        {
            graph.DrawLine(new Pen(Color.Black, 2), 55, 20, 55, 1980);
            graph.DrawLine(new Pen(Color.Black, 2), 55, 1980, 2160, 1980);

            //відмітки на осі Y
            int[] RateArray = new int[33];
            int a = 0;
            for (int i = 0; i < RateArray.Length; i++)
            {
                RateArray[i] = a;
                a += 30;
            }

            //рисуються відмітки
            int RateX = 0;
            int RateY = 1960;
            foreach (var item in RateArray)
            {
                graph.DrawString(item.ToString(), new Font("Arial", 20), new SolidBrush(Color.Black), new Point(RateX, RateY));
                RateY -= 60;
            }
        }
    }
}
