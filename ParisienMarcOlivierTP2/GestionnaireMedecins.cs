using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TP2
{
    class GestionnaireMedecins
    {
        public GestionnaireMedecins()
        {
            try
            {
                using (StreamReader fichierLecture = new StreamReader(_nomFichierMedecin))
                {
                    string ligneLue = fichierLecture.ReadLine();

                    while (ligneLue != null)
                    {
                        string[] elementsLu = ligneLue.Split(';');
                        int idMedecin = Convert.ToInt32(elementsLu[0]);
                        string nom = elementsLu[1];
                        string prenom = elementsLu[2];

                        if (elementsLu[3] != "")
                        {
                            DateTime retraite = Convert.ToDateTime(elementsLu[3]);
                            _medecins.Add(new Medecin(prenom, nom, idMedecin, retraite));
                        }
                        else
                        {
                            _medecins.Add(new Medecin(prenom, nom, idMedecin));
                        }

                        ligneLue = fichierLecture.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        public void Sauvegarder()
        {
            try
            {
                using (StreamWriter fichierEcriture = new StreamWriter(_nomFichierMedecin))
                {
                    foreach (Medecin m in _medecins)
                    {   
                        StringBuilder sb = new StringBuilder();
                        if (m.DateRetraite != default(DateTime))
                        {
                            
                            sb.AppendFormat("{0};{1};{2};{3}", m.Identification, m.Prenom, m.Nom, m.DateRetraite);
                            fichierEcriture.WriteLine(sb.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0};{1};{2};{3}", m.Identification, m.Prenom, m.Nom);
                            fichierEcriture.WriteLine(sb.ToString());
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Ajouter()
        {
            Medecin unMedecin = new Medecin();

            foreach (var m in _medecins)
            {
                if (m.Identification == unMedecin.Identification)
                {
                    Console.WriteLine("Impossible d'ajouter le medecin. Cet identification existe déjà.");
                    Console.ReadKey(true);
                    return;
                }
            }
            _medecins.Add(unMedecin);
            Console.WriteLine("Medecin ajouté!");
            Console.ReadKey(true);
        }
        public void IndiquerRetraite()
        {
            try
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Code d'identification : ");
                int id = Convert.ToInt32(Console.ReadLine());

                foreach (var m in _medecins)
                {
                    if (id == m.Identification && !m.Retraite)
                    {
                        Console.Write("Entrer la date de retraite (A/M/J) : ");
                        DateTime laDate = Convert.ToDateTime(Console.ReadLine());
                        _medecins.Add(new Medecin(m.Prenom, m.Nom, m.Identification, laDate));
                    }
                    else
                    {
                        Console.WriteLine("Ce medecin est déjà à la retraite, le chanceux!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AfficherStatistiques()
        {

        }
        public void AfficherListe()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Liste des medecins");
            foreach (var m in _medecins)
            {
                if (m.Retraite)
                {
                    Console.WriteLine("{0} {2} {3} ", m.Identification, m.Nom, m.Prenom  + estRetraite);
                }
            }
            Console.ReadKey(true);
        }
        public void AfficherUnique()
        {

        }

        private const string estRetraite = "Retraité";
        private const string _nomFichierMedecin = "medecins.txt";
        List<Medecin> _medecins = new List<Medecin>();
    }
}
