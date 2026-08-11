namespace BimaTech.Parking.Models
{
    public partial class Entreprise
    {
        public int Id { get; set; }

        public string Adresse { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        
        public List<Employe> Employes { get; set; } = new();
    }
}