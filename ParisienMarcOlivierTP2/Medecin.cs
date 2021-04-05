﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TP2
{
    class Medecin : Personne
    {
        public Medecin(string prenom, string nom, int idMedecin) : base(prenom, nom)
        {
            _idMedecin = idMedecin;
        }
        public Medecin(string prenom, string nom, int idMedecin, DateTime retraite) : base(prenom, nom)
        {
            _retraite = retraite;
        }
        public Medecin()
        {
            try
            {
                Console.Write("Code d'identification: ");
                _idMedecin = Convert.ToInt32(Console.ReadLine());

                while (_idMedecin < IDMIN && _idMedecin > IDMAX)
                {
                    Console.WriteLine("Valeur incorrecte. Doit être un entier entre {0} et {1}", IDMIN, IDMAX);
                    Console.Write("Code d'identification: ");
                    _idMedecin = Convert.ToInt32(Console.ReadLine());
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Le champ ne peut pas contenir de lettre.");
            }
        }
        public bool Retraite
        {
            get { return _retraite != default(DateTime) ? true : false; }
        }
        public DateTime DateRetraite
        {
            get { return _retraite; }
        }
        public int Identification
        {
            get { return _idMedecin; }
        }

        private List<Patient> _patients = new List<Patient>();
        protected DateTime _retraite;
        private readonly int _idMedecin;
        private const int IDMIN = 100;
        private const int IDMAX = 999;
    }
}
