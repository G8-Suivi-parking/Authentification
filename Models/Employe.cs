
namespace BimaTech.Parking.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Fonction { get; set; } = string.Empty;

        public int EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }

        public ICollection<Vehicule> Vehicules { get; set; } = new List<Vehicule>();
    }
}