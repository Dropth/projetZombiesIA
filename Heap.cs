using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Heap
    {
        /*
         * Alline Florian
         * Master 1 Informatique
         * Projet IA Zombies
         */

        Noeud [] items;
        int numberOfTime;

        public Heap (){

            items = new Noeud [10000];
            numberOfTime = 0;

        }

        public int Count() {

            return numberOfTime;
        }

        public void Add (Noeud n) {

            if (numberOfTime == 0) {

                items [numberOfTime] = n;
                numberOfTime++;

            }
            else {

                if (numberOfTime == 1) {

                    if(items[0].f > n.f) {

                        Noeud temp = items [0];
                        items [0] = n;
                        items [numberOfTime] = temp;

                    }
                    else {
                        items [numberOfTime] = n;
                    }

                    numberOfTime++;

                }
                else {

                    int indexP=0;                    

                    for(int i=0 ; i < Count() ; i++) {

                        if (items[i].f > n.f) {

                           indexP = i;
                           i = Count();
                        }
                    }

                    Noeud tempo = null;
                    Noeud tempor = null;

                    while(indexP < Count()) {

                        if(tempo == null) {
                            
                            tempo = items [indexP];
                            items [indexP] = n;

                            numberOfTime++;
                        }
                        else {

                            tempor = items [indexP];
                            items [indexP] = tempo;
                            tempo = tempor;
                            
                            tempor = null;

                        }

                        indexP++;

                    }
                }


            }
        }

        public bool Contains(Noeud n) {

            bool isCont = false;

            for(int i=0 ; i < Count() ; i++) {

                if (items [i] == n)
                    isCont = true;
            }

            return isCont;
        }

        public void Remove(Noeud n) {

            int indexZ = 0;

            for (int i = 0 ; i < Count() ; i++) {

                if (items [i] == n) {
                    indexZ = i;
                }
                    
            }
            
            while (indexZ < numberOfTime-1) {

                items [indexZ] = items [indexZ+1];
                indexZ++;

            }

            items [indexZ] = null;
            numberOfTime--;
        }

        public Noeud GetFirst() {

            return items [0];
        }

        public override string ToString() {

            string s = "";

            for(int i=0 ; i < Count() ; i++) {

                s += "f : " + items [i].f + "\n";

            }

            return s;
        }
        


    }

}
