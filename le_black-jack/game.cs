using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace le_black_jack
{
    class Game
    {
        private bool play;
        private int nbPlayers = 0;
        List<Playeur> players = new List<Playeur>();
        Playeur banque = new Playeur("la banque", 0);
        public Game()
        {
            main();
        }

        public int getNbPlayers()
        {
            return nbPlayers;
        }

        public List<Playeur> getPlayerList()
        {
            return players;
        }

        private void main()
        {
            introduction();
            if (!play)
                endGame();
            nbPlayer();
            newPlayers();
            Deck deck = new Deck();
            gameloop();
            Console.WriteLine("il n'y as plus de joueur sur la table\nFin du programe dans");
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        private void introduction()
        {
            string continu = "";

            Console.WriteLine("Bonjours bienvenu, je suis une IA qui vous guideras durant la partie " +
                "sur cette table de black jack, voicie les règles de la table\n" +
                "- La table aceuille entre 1 et 8 joueurs\n" +
                "- La mise de départ est de 10 euros et il n'y a pas de limites au mises suivantes\n" +
                "- les autres règles sont communes aux règles du black jack si vous en êtes étrangé" +
                " je vous conseille de vous renseigné avant de jouer\n\n" +
                "continuer ? (o/n)");
            while (continu == "")
            {
                continu = Console.ReadLine().ToUpper();
                if (continu != "O" && continu != "N")
                {
                    Console.WriteLine("Veuillez répondre par o ou n\n" +
                        "continuer ? (o / n)");
                    continu = "";
                }                
            }
            if (continu == "N")
                play = false;
            else
                play = true;
        }

        private void endGame()
        {
            Console.WriteLine("Revenez quand vous serez prêt");
            Thread.Sleep(2000);
            System.Environment.Exit(1);
        }

        private bool stringOrNot()
        {
            try
            {
                Console.WriteLine("Combient il y auras il de joueurs a la tables ?");
                nbPlayers = int.Parse(Console.ReadLine());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private void nbPlayer()
        {
            while (nbPlayers < 1 || nbPlayers > 8)
            {
                while (!stringOrNot())
                {}
                if (nbPlayers < 1 || nbPlayers > 8)
                {
                    Console.WriteLine("Impossible veuillez indiquez un chifren entre 1 et 8");
                }
            }
            Console.WriteLine($"tres bien il y auras donc {nbPlayers} joueur(s) a la table");
        }

        private void newPlayers()
        {
            Playeur playeur;
            string name;
            int money = 0;

            Console.WriteLine($"Création de {nbPlayers} nouveau joueur(s)");
            for (int i = 1; i < nbPlayers + 1; i++)
            {
                Console.Write($"Nom du joueur {i} : ");
                name = Console.ReadLine();
                Console.Write($"Argent total de {name} : ");
                while (money == 0)
                {
                    try
                    {
                        money = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Vous devez entrer un nombre rond superieur a 0");
                    }
                }
                Console.WriteLine($"Bonjours {name} vous commencez avec {money} euros");
                players.Add(playeur = new Playeur(name, money));
                money = 0;
            }
        }

        private void bet()
        {
            Console.WriteLine("premier tour de mise");
            for (int i = 0; i < players.Count(); i++)
            {
                Console.WriteLine($"quel est la mise pour {players[i].getName()} ?");
                do
                {
                    try
                    {
                        players[i].setBet(int.Parse(Console.ReadLine()));
                        if (players[i].getBet() > players[i].getMoney())
                            Console.WriteLine($"Désoler impossible de miser cette somme, vous disposer de {players[i].getMoney()}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (players[i].getBet() > players[i].getMoney() || players[i].getBet() < 1);
                Console.WriteLine($"{players[i].getName()} a misé {players[i].getBet()}");
                players[i].setMoney(players[i].getMoney() - players[i].getBet());
                Console.WriteLine($"il lui reste {players[i].getMoney()} euros");
            }
        }

        private void firstGet()
        {
            Console.WriteLine("Le croupier donne deux cartes aux joueur(s)");
            Console.WriteLine("-------------------------------------");
            for (int i = 0; i <= 1; i++)
            {
                foreach (Playeur item in players)
                {
                    item.take();
                }
            }

            foreach (Playeur item in players)
            {
                Console.WriteLine($"Cartes de {item.getName()} : ");
                foreach (Card card in item.getListCards())
                {
                    card.printInfo();
                }
                Console.WriteLine($"Points du joueur {item.getName()} : {item.getPts()}");
                Console.WriteLine("-------------------------------------");
            }
            Console.WriteLine("la banque pioche une cartes");
            banque.take();
            banque.getListCards()[0].printInfo();
            Console.WriteLine($"Points de la banque : {banque.getPts()}");
        }

        private void gameloop()
        {
            do
            {
                foreach (Playeur item in players)
                {
                    if (item.getListCards().Count() != 0)
                        item.setPts(0);
                    item.getListCards().Clear();
                }
                banque.getListCards().Clear();
                banque.setPts(0);
                bet();
                firstGet();
                foreach (Playeur item in players)
                {
                    playerTurn(item);
                }
                bankTurn();
                foreach (Playeur item in players)
                {
                    final(item);
                }
                outOrNot();
            } while (players.Count() > 0);
        }

        private void playerTurn(Playeur playeur)
        {
            string answer;
            do
            {
                answer = "2";
                if (playeur.getPts() <= 21)
                {
                    Console.WriteLine($"Au tour de {playeur.getName()} avec {playeur.getPts()}");
                    Console.WriteLine("1 pour rester\n2 pour tirer");
                    answer = Console.ReadLine();
                }
                if (answer == "2")
                {
                    Console.Write("Vous avez tiré : ");
                    playeur.take();
                    playeur.getListCards().Last().printInfo();
                    Console.WriteLine($"\nVos points : {playeur.getPts()}");
                }
            } while (answer != "2"  && answer != "1" && playeur.getPts() <= 21);
        }

        //private string choice()
        //{
        //    string answer = "";
        //    do
        //    {
        //        Console.WriteLine("1 pour rester\n2 pour tirer\n 3 pour quitter la table");
        //        answer = Console.ReadLine();
        //        switch (answer)
        //        {
        //            case "1":
        //                break;
        //            case "2":
        //                break;
        //            case "3":
        //                break;
        //            default:
        //                Console.WriteLine("Mauvais choix");
        //                break;
        //        }
        //    } while (true);
        //    return answer;
        //}

        private void bankTurn()
        {
            int size;
            do
            {
                Console.Write($"{banque.getName()} a tiré : ");
                banque.take();
                size = banque.getListCards().Count();
                banque.getListCards().Last().printInfo();
                Console.WriteLine($"Points de la banque: {banque.getPts()}");
            } while (banque.getPts() <= 17);
        }

        private void final(Playeur playeur)
        {
            if (playeur.getPts() == banque.getPts() || playeur.getPts() > 21 && banque.getPts() > 21)
            {
                Console.WriteLine($"{playeur.getName()} égalise avec la banque, le joueur reprend ça mise");
                playeur.setMoney(playeur.getBet() + playeur.getMoney());
            }
            else if (playeur.getPts() <= 21 && banque.getPts() < playeur.getPts() ||
                playeur.getPts() <= 21 && banque.getPts() > 21)
            {
                Console.WriteLine($"{playeur.getName()} gagne et doublez ça mise");
                playeur.setMoney(playeur.getMoney() + (playeur.getBet() + playeur.getBet()));
            }
            else if (banque.getPts() <= 21 && banque.getPts() > playeur.getPts() ||
                banque.getPts() <= 21 && playeur.getPts() > 21)
            {
                Console.WriteLine($"{playeur.getName()} perd ça mise");
            }
        }

        private void outOrNot()
        {
            for (int i = players.Count() - 1; i >= 0; i--)
            {
                if (players[i].getMoney() == 0)
                {
                    Console.WriteLine($"{players[i].getName()} est hors jeux\nAu revoir a bientôt");
                    players.Remove(players[i]);
                }
            }
        }
    }
}
