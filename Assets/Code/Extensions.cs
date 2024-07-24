using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public static class Extensions
    {
      
        public static void AddPool<T>(IEcsSystems ecsSystem, int entity) where T : struct
        {
            if (ecsSystem.GetWorld().GetPool<T>().Has(entity))
            {
               return;
            }
            else
            {
                ref var component = ref ecsSystem
                    .GetWorld()
                    .GetPool<T>().Add(entity);
            }
        }
        
        
        public static int[] GetUniqeRandomArray(int size, int Min, int Max)
        {
            int[] UniqueArray = new int[size];
            var rnd = new System.Random();
            int Random;

            for (int i = 0; i < size; i++)
            {
                Random = rnd.Next(Min, Max);

                for (int j = i; j >= 0; j--)
                {
                    if (UniqueArray[j] == Random)
                    {
                        Random = rnd.Next(Min, Max);
                        j = i;
                    }
                }

                UniqueArray[i] = Random;
            }

            return UniqueArray;
        }
        
        public static int GetRandomInt( int Min, int Max)
        {
            var rnd = new System.Random();
            return rnd.Next(Min, Max);
        }
        
        public static Vector3 GetRandomVector( int XMin, int XMax,int YMin, int YMax)
        {
            var rnd = new System.Random();
            return new Vector3(rnd.Next(XMin, XMax),rnd.Next(YMin, YMax),0) ;
        }
    }
}