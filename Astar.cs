using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA {
    class Astar {

        /*
         * Alline Florian
         * Master 1 Informatique
         * Projet IA Zombies
         */

        public Noeud [,] grille { get; set; }
        private bool diagonals = false;


        public Astar(Noeud[,] grilleA, bool diag) {
            grille = grilleA;
            diagonals = diag;
        }

        public List<Noeud> FindPathBug(Noeud start, Noeud fin) {
            // List<Noeud> openList = new List<Noeud>();
            Heap openList = new Heap();
            List<Noeud> closedList = new List<Noeud>();
            List<Noeud> path = new List<Noeud>();
            openList.Add(start);
            while (openList.Count() > 0) {
                
               // int indexLowestF = 0;

                Stopwatch sw = new Stopwatch();
                sw.Start();

               /* for (int i = 0; i < openList.Count(); i++)
                    if (openList[i].f < openList[indexLowestF].f) indexLowestF = i;*/

                sw.Stop();
               // Console.WriteLine("BONSOIR : " + sw.ElapsedMilliseconds);
                sw.Reset();

                // Noeud currentNode = openList[indexLowestF];
                Noeud currentNode = openList.GetFirst();
                

                //Cas de fin
                if (currentNode.x == fin.x && currentNode.y == fin.y) {
                    var temp = currentNode;                    

                    while (temp.parent != null) {
                        path.Add(temp);
                        temp = temp.parent;
                    }        

                    path.Reverse();

                    foreach (Noeud n in grille) {
                        n.f = 0;
                        n.g = 0;
                        n.h = 0;
                        n.parent = null;
                    }

                    return path;
                }

                //Cas normal
                
                var voisins = GetVoisins(currentNode);
                

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var voisin in voisins) {
                    if (!closedList.Contains(voisin)) {

                        var gTemp = currentNode.g + 1;
                        var bestG = false;

                        if (!openList.Contains(currentNode)) {
                            //Première rencontre du noeud. On calcule son H et on le met dans l'openList.
                            bestG = true;
                            voisin.h = getHeuristicScore(voisin, fin);
                           // openList.Add(voisin);
                        } else if (gTemp < voisin.g) {
                            //On a trouvé un meilleur chemin vers le noeud. On stock l'information.
                            bestG = true;
                        }

                        if (bestG) {
                            //Si c'est un meilleur chemin, on modifie le noeud.
                            voisin.parent = currentNode;
                            voisin.g = gTemp;
                            voisin.f = voisin.g + voisin.h;
                            if (closedList.Contains(voisin))
                                openList.Remove(voisin);
                            openList.Add(voisin);
                        }
                    }
                }

            }

            foreach (Noeud n in grille) {
                n.f = 0;
                n.g = 0;
                n.h = 0;
                n.parent = null;
            }
            return null;
        }

        public List<Noeud> FindPath (Noeud start,Noeud fin) {
            // List<Noeud> openList = new List<Noeud>();
            List<Noeud> openList = new List<Noeud>();
            List<Noeud> closedList = new List<Noeud>();
            List<Noeud> path = new List<Noeud>();
            openList.Add(start);
            while (openList.Count() > 0) {

                int index = 0;
                int petit = openList [0].f;

                for (int cpt = 0 ; cpt < openList.Count() ; cpt++) {
                    if (openList [cpt].f < petit) {
                        petit = openList [cpt].f;
                        index = cpt;
                    }
                }

                // Noeud currentNode = openList[indexLowestF];
                Noeud currentNode = openList[index];
                openList.Remove(openList [index]);


                //Cas de fin
                if (currentNode.x == fin.x && currentNode.y == fin.y) {
                    var temp = currentNode;

                    while (temp.parent != null) {
                        path.Add(temp);
                        temp = temp.parent;
                    }

                    path.Reverse();

                    foreach (Noeud n in grille) {
                        n.f = 0;
                        n.g = 0;
                        n.h = 0;
                        n.parent = null;
                    }

                    return path;
                }

                //Cas normal

                var voisins = GetVoisins(currentNode);


               // openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var voisin in voisins) {
                    if (!closedList.Contains(voisin)) {

                        var gTemp = currentNode.g + 1;
                        var bestG = false;

                        if (!openList.Contains(currentNode)) {
                            //Première rencontre du noeud. On calcule son H et on le met dans l'openList.
                            bestG = true;
                            voisin.h = getHeuristicScore(voisin,fin);
                            // openList.Add(voisin);
                        }
                        else if (gTemp < voisin.g) {
                            //On a trouvé un meilleur chemin vers le noeud. On stock l'information.
                            bestG = true;
                        }

                        if (bestG) {
                            //Si c'est un meilleur chemin, on modifie le noeud.
                            voisin.parent = currentNode;
                            voisin.g = gTemp;
                            voisin.f = voisin.g + voisin.h;
                            if (closedList.Contains(voisin))
                                openList.Remove(voisin);
                            openList.Add(voisin);
                        }
                    }
                }

            }

            foreach (Noeud n in grille) {
                n.f = 0;
                n.g = 0;
                n.h = 0;
                n.parent = null;
            }
            return null;
        }



        /*private int getHeuristicScore(Noeud voisin, Noeud fin) {
            //Utilisation de la distance Manhattan
            var d1 = Math.Abs(fin.x - voisin.x);
            var d2 = Math.Abs(fin.y - voisin.y);
            return d1 + d2;
        }*/

        private int getHeuristicScore (Noeud voisin,Noeud fin) {
            //Utilisation de la distance Manhattan
            /*if (voisin.otherEnd != null) voisin = voisin.otherEnd;
            var d1 = Math.Abs(fin.x - voisin.x);
            var d2 = Math.Abs(fin.y - voisin.y);
            return d1 + d2;*/

            //Utilisation de la distance euclidienne
            if (voisin.tunnelEnd != null)
                voisin = voisin.tunnelEnd;
            var d1 = Math.Sqrt((Math.Pow(voisin.x - fin.x,2)));
            var d2 = Math.Sqrt((Math.Pow(voisin.y - fin.y,2)));
            return Convert.ToInt32(d1 + d2);
        }

        public List<Noeud> GetVoisins(Noeud n) {
            var ret = new List<Noeud>();


            if (n.x - 1 >= 0 && n.y >= 0 && n.x - 1 < grille.GetLength(0) && n.y < grille.GetLength(1)) {
                if (grille[n.x - 1, n.y].isPath())
                    ret.Add(grille[n.x - 1, n.y]);
            }

            if (n.x >= 0 && n.y - 1 >= 0 && n.x < grille.GetLength(0) && n.y - 1 < grille.GetLength(1)) {
                if (grille[n.x, n.y - 1].isPath())
                    ret.Add(grille[n.x, n.y - 1]);
            }

            if (n.x >= 0 && n.y + 1 >= 0 && n.x < grille.GetLength(0) && n.y + 1 < grille.GetLength(1)) {
                if (grille[n.x, n.y + 1].isPath())
                    ret.Add(grille[n.x, n.y + 1]);
            }

            if (n.x + 1 >= 0 && n.y >= 0 && n.x + 1 < grille.GetLength(0) && n.y < grille.GetLength(1)) {
                if (grille[n.x + 1, n.y].isPath())
                    ret.Add(grille[n.x + 1, n.y]);
            }

            if (diagonals){

                if (n.x - 1 >= 0 && n.y - 1 >= 0 && n.x - 1 < grille.GetLength(0) && n.y - 1 < grille.GetLength(1))
                {
                    if (grille [n.x - 1,n.y - 1].isPath())
                        ret.Add(grille [n.x - 1,n.y - 1]);
                }

                if (n.x + 1 >= 0 && n.y - 1 >= 0 && n.x + 1< grille.GetLength(0) && n.y - 1 < grille.GetLength(1))
                {
                    if (grille [n.x + 1,n.y - 1].isPath())
                        ret.Add(grille [n.x + 1,n.y - 1]);
                }

                if (n.x + 1 >= 0 && n.y + 1 >= 0 && n.x + 1 < grille.GetLength(0) && n.y + 1 < grille.GetLength(1))
                {
                    if (grille [n.x + 1,n.y + 1].isPath())
                        ret.Add(grille [n.x + 1,n.y + 1]);
                }

                if (n.x - 1 >= 0 && n.y + 1 >= 0 && n.x - 1 < grille.GetLength(0) && n.y + 1 < grille.GetLength(1))
                {
                    if (grille [n.x + 1,n.y + 1].isPath())
                        ret.Add(grille [n.x + 1,n.y + 1]);
                }
            }

            
            return ret;
        }

    }
}
