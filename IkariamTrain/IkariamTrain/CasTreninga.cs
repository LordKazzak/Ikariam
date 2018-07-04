using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkariamTrain
{
    class CasTreninga
    {
        //{min baracks level, base train time}
        //Time = 0,95^(Curr lvl - min lvl) * base train time

        /*                              
         * double[] MetalciPlamena =    {4, 30};
         * double[] ParniRami =         {15, 40};
         * double[] Rami =              {1, 40};
         * double[] LadjeKatapulti =    {3, 50};
         * double[] Baliste =           {3, 50};
         * double[] LadjeMortarji =     {17, 50};
         * double[] Raketne =           {11, 60};
         * double[] Podmornice =        {19, 60};
         * double[] HitriParniki =      {13, 30};
         * double[] NosilciBalonov =    {7, 66};
         * double[] ServisniDoki =      {9, 40};
         * 
         * 
         * double[] Sulicarji =         {4, 5};
         * double[] ParniVelikani =     {12, 15};
         * double[] MetalciKopja =      {1, 1};
         * double[] Mecevalci =         {6, 3};
         * double[] Pracarji =          {2, 1.5};
         * double[] Lokostrelci =       {7, 4};
         * double[] Musketirji =        {13, 10};
         * double[] Ovni =              {3, 10};
         * double[] Katapulti =         {8, 30};
         * double[] Mortarji =          {14, 40};
         * double[] Girokopterji =      {10, 15};
         * double[] Bombniki =          {11, 30};
         * double[] Kuharji =           {5, 20};
         * double[] Zdravniki =         {9, 20};
         */

        public double[,] trainData;
        public double[,] netherData;

        public CasTreninga()
        {
            trainData = new double[,] {
                {9, 40}, //ladje (obratni vrstni red)
                {7, 66},
                {13, 30},
                {19, 60}, //sub not here
                {11, 60},
                {3, 50},
                {17, 50},
                {3, 50},
                {1, 40}, //ram not here
                {15, 40},
                {4, 30}, //metalec
                {9, 20}, //kopenske (obratni vrstni red)
                {5, 20},
                {11, 30},
                {10, 15},
                {14, 40},
                {8, 30},
                {3, 10},
                {13, 10},
                {7, 4},
                {2, 1.5},
                {6, 3},
                {1, 1},
                {12, 15},
                {4, 5}
            };

            netherData = new double[,] {
                {19, 60}, //sub
                {1, 40} //ram
            };
        }
    }
}
