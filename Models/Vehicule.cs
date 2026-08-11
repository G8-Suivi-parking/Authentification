namespace BimaTech.Parking.Models
{
    public enum TypeVehicule
    {
        Voiture,
        Moto,
        Utilitaire
    }

    public enum StatutVehicule
    {
        Actif,
        Inactif,
        EnMaintenance
    }

    public class Vehicule
    {
        public int Id { get; set; }
        public string Immatriculation { get; set; } = string.Empty; // unique
        public string Marque { get; set; } = string.Empty;
        public string Modele { get; set; } = string.Empty;
        public string Couleur { get; set; } = string.Empty;
        public TypeVehicule Type { get; set; }
        public StatutVehicule Statut { get; set; }

        public int EmployeId { get; set; }
        public Employe? Employe { get; set; }
    }
}