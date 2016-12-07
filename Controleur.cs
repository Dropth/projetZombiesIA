using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetIA {

    class Controleur {

        /*
         * Alline Florian
         * Master 1 Informatique
         * Projet IA Zombies
         */

        private const int TIMETOMOVE = 15;
        public Noeud [,] grille { get; set; }
        public bool diagonals { get; set; }

        private Noeud joueur;
        private Noeud fin;

        private int verifTermine = 0; //1 = Joueur gagne | 2 = Zombies gagne | Reste ) la partie continue
        private List<Noeud> zombies;

        public Controleur (Noeud [,] grille,bool isDiagonals) {

            this.grille = grille;
            this.diagonals = isDiagonals;
            zombies = new List<Noeud>();

            for (int i = 0 ; i < grille.GetLength(0) ; i++) {

                for (int j = 0 ; j < grille.GetLength(1) ; j++) {

                    if (grille [i,j].occupant == 'J')
                        joueur = grille [i,j];
                    else if (grille [i,j].occupant == 'F')
                        fin = grille [i,j];
                    else if (grille [i,j].occupant == 'Z')
                        zombies.Add(grille [i,j]);
                }
            }
        }


        public bool jouer () {

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            Console.WriteLine(Program.afficherMap(grille));
            bool mouvementValide = false;
            int timer = 0;

            while (verifTermine == 0) {
                while (!mouvementValide) {
                    while (Console.KeyAvailable == false && mouvementValide == false) {
                        Thread.Sleep(50);
                        timer++;
                        if (timer == TIMETOMOVE)
                            mouvementValide = true;
                    }
                    if (mouvementValide == false) {
                        cki = Console.ReadKey(true);
                        mouvementValide = deplacementFleche(cki);
                    }

                }

                Console.Clear();
                isJeuTermine(joueur);
                if (verifTermine == 1)
                    break;
                mouvementZombie();
                mouvementValide = false;
                timer = 0;
                if (verifTermine != 2)
                    isJeuTermine(joueur);
                Console.WriteLine(Program.afficherMap(grille));
            }
            if (verifTermine == 1) {
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("|                                                                        |");
                Console.WriteLine("|                              ! VICTOIRE !                              |");
                Console.WriteLine("|                                                                        |");
                Console.WriteLine("--------------------------------------------------------------------------");

                Console.WriteLine("\n\nBien joué ! Tu as réussi à t'enfuir de l'université ! \n" +
                                  "Respire un peu d'air frais avant de retourner en cours !\n" +
                                  "Grace à toi, l'Alliance Rebelle des Pauses vient de gagner \n" +
                                  "une victoire importante dans la guerre contre l'université. \n" +
                                  "Veux-tu retenter ta chance ? [y/n]");
                string rejouer = Console.ReadLine();

                if (rejouer.Equals("y")) {

                    Console.Clear();
                    new Program();
                }
                else
                    Environment.Exit(0);
            }
            else if (verifTermine == 2) {

                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("|                                                                        |");
                Console.WriteLine("|                                ! DEFAITE !                             |");
                Console.WriteLine("|                                                                        |");
                Console.WriteLine("--------------------------------------------------------------------------");

                Console.WriteLine("\n\nEchec ! Tu n'as pas réussi à t'enfuir de l'université ! \n"+
                                  "Les profs zombies t'ont attrapé et tu vas maintenant assiter à \n" +
                                  "un cours sans pause ! La guerre n'est pas finie et tu peux retenter \n"+
                                  "ta chance. Es-tu prêt ? [y/n]");
                string rejouer = Console.ReadLine();

                if (rejouer.Equals("y")) {

                    Console.Clear();
                    new Program();
                }
                else
                    Environment.Exit(0);
            }


            return true;
        }

        private void isJeuTermine (Noeud joueur) {
            if (joueur.Equals(fin))
                verifTermine = 1;
            else if (isJoueurBloque())
                verifTermine = 2;
            else
                verifTermine = 0;
        }

        private bool isJoueurBloque () {

            bool isBloque = true;

            if (diagonals) {
                if (verifNoeud(joueur.x + 1,joueur.y + 1))
                    isBloque = false;
                else if (verifNoeud(joueur.x + 1,joueur.y - 1))
                    isBloque = false;
                else if (verifNoeud(joueur.x - 1,joueur.y + 1))
                    isBloque = false;
                else if (verifNoeud(joueur.x - 1,joueur.y - 1))
                    isBloque = false;
            }
            if (verifNoeud(joueur.x + 1,joueur.y))
                isBloque = false;
            else if (verifNoeud(joueur.x,joueur.y + 1))
                isBloque = false;
            else if (verifNoeud(joueur.x,joueur.y - 1))
                isBloque = false;
            else if (verifNoeud(joueur.x - 1,joueur.y))
                isBloque = false;
            return isBloque;
        }

    private void mouvementZombie () {
            Astar astar;
            List<Noeud> temp = new List<Noeud>();
            Noeud suivant;

            foreach (Noeud zombie in zombies) {

                suivant = null;

                astar = new Astar(grille,false);
                List<Noeud> path = astar.FindPath(zombie,joueur);
                List<Noeud> path2 = astar.FindPath(zombie,fin);

                if (path != null) {
                    
                    if (path.Count <= path2.Count)
                        suivant = path [0];
                    else
                        if (path2.Count <= 1)
                            if (path.Count < 4)
                                suivant = path [0];
                            else
                                suivant = path2 [0];
                        else
                            suivant = path2 [0];

                    if (suivant.occupant != 'Z' && suivant.occupant != 'F') {

                        mouvementZombie(zombie,suivant);
                        temp.Add(suivant);

                        if (suivant.Equals(joueur)) {
                            verifTermine = 2;
                        }
                    }
                    else {
                        mouvementZombie(zombie,zombie);
                        temp.Add(zombie);
                    }
                }
            }

            zombies = temp;
        }

        private void mouvementZombie (Noeud depart,Noeud arrive) {
            if (depart.tunnelEnd != null)
                depart.occupant = '5';
            else
                depart.occupant = '_';
            arrive.occupant = 'Z';
        }

        private bool deplacementFleche (ConsoleKeyInfo c) {
            bool verifDeplacement = false;

            if (c.Key.ToString().Equals("NumPad8")) {
                if (verifNoeud(joueur.x - 1,joueur.y)) {
                    mouvementJoueur(grille [joueur.x - 1,joueur.y]);
                    verifDeplacement = true;
                }
            }
            else if (c.Key.ToString().Equals("NumPad6")) {
                if (verifNoeud(joueur.x,joueur.y + 1)) {
                    mouvementJoueur(grille [joueur.x,joueur.y + 1]);
                    verifDeplacement = true;
                }
            }
            else if (c.Key.ToString().Equals("NumPad4")) {
                if (verifNoeud(joueur.x,joueur.y - 1)) {
                    mouvementJoueur(grille [joueur.x,joueur.y - 1]);
                    verifDeplacement = true;
                }

            }
            else if (c.Key.ToString().Equals("NumPad2")) {
                if (verifNoeud(joueur.x + 1,joueur.y)) {
                    mouvementJoueur(grille [joueur.x + 1,joueur.y]);
                    verifDeplacement = true;
                }
            }
            else if (c.Key.ToString().Equals("NumPad5")) {
                if (joueur.tunnelEnd != null)
                    mouvementJoueur(joueur.tunnelEnd);
                verifDeplacement = true;
            }

            return verifDeplacement;
        }

        private void mouvementJoueur (Noeud arrive) {
            if (joueur.tunnelEnd != null)
                joueur.occupant = '5';
            else
                joueur.occupant = '_';
            arrive.occupant = 'J';
            joueur = arrive;
        }

        private bool verifNoeud (int x,int y) {
            bool retour = true;

            if (!grille [x,y].isBloque())
                retour = false;
            if (x > grille.GetLength(0) && x < 0)
                retour = false;
            if (y > grille.GetLength(1) || y < 0)
                retour = false;
            
            return retour;
        }

    }
}
