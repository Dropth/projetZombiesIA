using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA {
    class Test {
        /*static void Main() {*/
        /*
            int taille = 200;
            String[,] map = new String[taille, taille];
            Astar ast = new Astar(map,false);
            ast.grille[5, 0].isPath = false;
            ast.grille[6, 1].isPath = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var path = ast.FindPath(ast.grille[4, 3], ast.grille[199, 199]);

            sw.Stop();

            foreach (var noeud in path) {
                Console.WriteLine(noeud);
            }

            Console.WriteLine(" solutions en " + sw.ElapsedMilliseconds + " ms");

            string str = "0 0 0 0 0 1 0 0 0";
            string [] tab = str.Split(' ');

            for (int i = 0 ; i < tab.Length ; i++) {
                Console.WriteLine(tab [i] + "\n");
            }

            Console.Read();*/

           /* Heap h = new Heap();

            Noeud n1 = new Noeud(0,0);
            Noeud n2 = new Noeud(1,1);
            Noeud n3 = new Noeud(2,2);
            Noeud n4 = new Noeud(3,3);
            Noeud n5 = new Noeud(4,4);
            Noeud n6 = new Noeud(5,5);

            n1.f = 5;
            n2.f = 4;
            n3.f = 3;
            n4.f = 2;
            n5.f = 1;

            h.Add(n1);
            h.Add(n3);
            h.Add(n4);
            h.Add(n2);
            h.Add(n5);

            h.Remove(n3);

            h.Add(n3);

            

            Console.WriteLine(h.ToString());
            Console.WriteLine(h.Contains(n6));
            Console.Read();*/

//        }
    }
}
