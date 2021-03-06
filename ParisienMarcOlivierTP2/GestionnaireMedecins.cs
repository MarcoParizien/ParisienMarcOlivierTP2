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
                    while (!fichierLecture.EndOfStream)
                    {
                        string ligneLue = fichierLecture.ReadLine();

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
                        string dateCourte;

                        if (m.DateRetraite != default(DateTime))
                        {
                            dateCourte = m.DateRetraite.ToShortDateString();
                            sb.AppendFormat("{0};{1};{2};{3}", m.Identification, m.Prenom, m.Nom, dateCourte);
                            fichierEcriture.WriteLine(sb.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("{0};{1};{2};", m.Identification, m.Prenom, m.Nom);
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
            if (_medecins.Count == 1)
            {
                Console.WriteLine("Impossible de partir à la retraite, il n'y qu'un seul medecin en lsite.");
            }
            else
            {
                Console.WriteLine("-----------------------------");
                Console.Write("Code d'identification : ");
                int id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    foreach (var m in _medecins)
                    {
                        if (id == m.Identification)
                        {
                            if (!m.Retraite)
                            {
                                Console.Write("Entrer la date de retraite (A/M/J) : ");
                                DateTime laDate = Convert.ToDateTime(Console.ReadLine());
                                RepartirPatients(m);
                                _medecins.Remove(m);
                                _medecins.Add(new Medecin(m.Prenom, m.Nom, m.Identification, laDate));
                            }
                            else
                            {
                                Console.WriteLine("Ce medecin est déjà à la retraite, le chanceux!");
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
      
        public void RepartirPatients(Medecin medecinSortant)
        {
            Medecin medecinRemplacant = _medecins[0];
            int nbPatients = medecinSortant.PatientSuivi.Count;

            while (nbPatients> 0)
            {
                foreach (var m in _medecins)
                {
                    if (m.PatientSuivi.Count < medecinRemplacant.PatientSuivi.Count)
                    {
                        medecinRemplacant = m;
                    }
                }

                Patient aBouger = medecinSortant.PatientSuivi[medecinSortant.PatientSuivi.Count - 1];
                aBouger.SonMedecin = medecinRemplacant;

                medecinSortant.PatientSuivi.RemoveAt(medecinSortant.PatientSuivi.Count - 1);
                nbPatients = medecinSortant.PatientSuivi.Count;
            }
        }

        public void AfficherStatistiques()
        {
            int medRetraite = 0;
            int medTotal = _medecins.Count();
            foreach (var m in _medecins)
            {
                if (m.Retraite)
                {
                    medRetraite++;
                }
            }
            if (medRetraite > 0)
            {
                Console.WriteLine("Il y a {0} medecin(s) dont {1} retraité(s).", medTotal, medRetraite);
            }
            else
            {
                Console.WriteLine("Il y a {0} medecin(s).", medTotal);
            }
        }
        public void AfficherListe()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Liste des medecins");

            foreach (var m in _medecins)
            {
                if (m.Retraite)
                {
                    Console.WriteLine("{0} {1} {2} ", m.Identification, m.Nom, m.Prenom + " " + estRetraite);
                }
                else
                {
                    Console.WriteLine("{0} {1} {2} ", m.Identification, m.Nom, m.Prenom);
                }
            }
            Console.ReadKey(true);
        }
        public void AfficherUnique()
        {

            Console.WriteLine("Entrez un identifiant : ");
            int idMedecin = Convert.ToInt32(Console.ReadLine());

            try
            {

                while (idMedecin < IDMIN || idMedecin > IDMAX)
                {
                    Console.WriteLine("L'identifiant doit être entre {0} et {1} ", IDMIN, IDMAX);
                    Console.Write("Entrez un identifiant : ");
                    idMedecin = Convert.ToInt32(Console.ReadLine());
                }

                foreach (var m in _medecins)
                {
                    if (idMedecin == m.Identification)
                    {
                        Console.WriteLine("Code d'identification {0}", m.Identification);
                        Console.WriteLine("Nom : {0} {1}", m.Prenom, m.Nom);

                        if (!m.Retraite)
                        {
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("Liste de patients");
                            foreach (var p in m.PatientSuivi)
                            {
                                Console.WriteLine("{0} {1} {2}", p.IdPatient, p.Prenom, p.Nom);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Retraité le {0}", m.DateRetraite.ToShortDateString());
                        }

                    }
                }
                Console.ReadKey(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        private const int IDMIN = 100;
        private const int IDMAX = 999;
        private const string estRetraite = "Retraité";
        private const string _nomFichierMedecin = "medecins.txt";
        public List<Medecin> _medecins = new List<Medecin>();
    }
}
