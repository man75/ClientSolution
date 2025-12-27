namespace ClientSi.API.Dtos;

public class AddClientDto
{
    public string Nom { get; set; }=string.Empty;
    public string Prenom { get; set; }= string.Empty;

    public bool IsPro { get; set; } = false;
    public string? Email { get; set; }
}
