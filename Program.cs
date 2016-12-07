using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetIA {
    class Program {

        /*
        * Alline Florian
        * Master 1 Informatique
        * Projet IA Zombies
        */

        public Program() {

            Main();
        }

        static void Main () {

            char [,] map = lireMap("./map");
            Noeud [,] grille = genererGrille(map);
            //Console.WriteLine(afficherMap(grille));
            Controleur c = new Controleur(grille,false);

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("|             L'Etudiant contre les professeurs zombies                  |");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("--------------------------------------------------------------------------");

            Console.WriteLine(  "\n\nDans un monde où l'université a décidé d'interdire les pauses, \n" +
                                "vous seul représentez l'espoir de toute une génération. Vous êtes \n" +
                                "retenu prisonnier dans l'université après être sortis de cours pour \n"+
                                "prendre une pause et votre objectif est d'atteindre la sortie de\n" +
                                "l'université. Cependant, la tache ne sera pas aisé.\n" +
                                "Pour éviter que des élèves tels que vous se rebellent, \n" +
                                "l'université a mis en place de redoutables professeurs zombies. Ils \n" +
                                "sont capable de vous poursuivre et bloquer la sortie. Si jamais \n" +
                                "il vous attrape, vous retournerez en cours et la mission de l'Alliance Rebelle \n" +
                                "pour récupérer les pauses échoura. Vous êtes notre seul espoir jeune \n" +
                                "étudiant, nous comptons sur vous !");

            Console.ReadLine();

            Console.WriteLine("\n\n Les consignes : \n\n"+
                              "Vous êtes le J sur la console. Votre objectif est indiqué par le F.\n"+
                              "Les Z représentent les profs Zombies. _ est une case vide et | est un mur \n"+
                              "Les nombres au-dessus de 4 sont des tunnels. Pour l'emprunter, allez dessus et \n"+
                              "appuyez sur 5 (PAV NUM). Pour vous déplacer, utilisez 2,4,6,8 du PAV NUM \n"+
                              "Bonne chance étudiant lambda ! (Si vous êtes prêt, entrez y)\n\n");

            string jouer = Console.ReadLine();

            while(!jouer.Equals("y")) {

                Console.WriteLine("Ecris y et commence ton aventure ! \n" +
                    "Si tu ne sais pas faire ça, on est mal barré pour la mission !");
                jouer = Console.ReadLine();
            }

            c.jouer();
            Console.ReadLine();

            


        }

        public static char [,] lireMap (String nomFichier) {

            TextReader readFile = File.OpenText(nomFichier);
            TextReader readerFile = File.OpenText(nomFichier);

            int lignes = 0;
            int cpt = 0;
            int size = -1;

            string text;
            string [] textSplit;

            List<char> read = new List<char>();

            while (readerFile.ReadLine() != null)
                lignes++;

            while ((text = readFile.ReadLine()) != null) {

                textSplit = text.Split(' ');

                if (size == -1)
                    size = textSplit.Count();

                foreach (string s in textSplit) {
                    read.Add(char.Parse(s));
                }
            }

            char [,] map = new char [lignes, size];

            for (int i = 0 ; i < lignes ; i++) {

                for (int j = 0 ; j < size ; j++) {
                    map [i,j] = read [cpt++];
                }
            }
            return map;
        }

        public static Noeud [,] genererGrille (char [,] map) {

            Noeud [,] grille = new Noeud [map.GetLength(0), map.GetLength(1)];
            Dictionary<int,List<Noeud>> tunnels = new Dictionary<int,List<Noeud>>();
            Noeud n;
            char value;

            for (int i = 0 ; i < map.GetLength(0) ; i++) {
                for (int j = 0 ; j < map.GetLength(1) ; j++) {
                    n = new Noeud(i,j);
                    value = map [i,j];
                    //La Fin
                    if (value == 'F') {
                        n.occupant = 'F';
                        n.isFinish = true;
                    }
                    else { n.occupant = value; }

                    string test = new string(value,1);
                    int val;

                    bool res = Int32.TryParse(test,out val);

                    if(res) {


                        if (val > 4) {

                            if (tunnels.Keys.Contains(val))
                                tunnels [val].Add(n);
                            else {
                                tunnels.Add(val,new List<Noeud>());
                                tunnels [val].Add(n);
                            }
                        }

                    }
                    grille [i,j] = n;
                }
            }

            foreach (List<Noeud> listeN in tunnels.Values) {
                
                listeN [0].tunnelEnd = listeN [1];
                listeN [1].tunnelEnd = listeN [0];
            }
            return grille;
        }

        public static string afficherMap (Noeud [,] map) {
            string str = "";
            for (int i = 0 ; i < map.GetLength(0) ; i++) {
                for (int j = 0 ; j < map.GetLength(1) ; j++) {
                    str += map [i,j].occupant + " ";
                }
                str += "\n";
            }
            return str;
        }
    }
}
