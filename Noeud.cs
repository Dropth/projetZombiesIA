using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA {

    class Noeud {

       /*
        * Alline Florian
        * Master 1 Informatique
        * Projet IA Zombies
        */

        public int f { get; set; } // distance a comparer
        public int g { get; set; } // distance départ - noeud
        public int h { get; set; } // distance noeud - arrivée
        public int x { get; set; } // coordonnée x sur la carte
        public int y { get; set; } // coordonnée y sur la carte
        public Noeud parent { get; set; } //noeud parent
        public bool isP { get; set; } // le noeud est il empruntable?
        public char occupant { get; set; } //_ rien - | mur - J joueur - Z zombie - F fin
        public Noeud tunnelEnd { get; set; }
        public bool isFinish { get; set; }


        public Noeud(int x, int y) {
            this.x = x;
            this.y = y;
            this.isP = true;
            f = 0;
            g = 0;
            h = 0;
            parent = null;
            occupant = '0';
            tunnelEnd = null;
            isFinish = false;

        }

        public bool isBloque () {
            return (occupant != '|' && occupant != 'Z');
        }

        public override string ToString() {
            return "x : " + x + " y : " + y;
        }

        public override bool Equals (System.Object obj) {
            bool ret = false;
            var item = obj as Noeud;

            if (item == null) {
                ret = false;
            }
            else {
                if (item.x == this.x && item.y == this.y)
                    ret = true;
            }

            return ret;
        }

        public bool isPath () {
            return (occupant !=  '|'  /*&& occupant != 3*/);
        }
    }
}
