namespace API.Models.Dtos;

public class UpdateMatchDto
{
    public int Id { get; set; }
    public int? WinnerId { get; set; }
    public string? News { get; set; }
    public string? ExtraInfo1 { get; set; }
    public string? ExtraInfo2 { get; set; }
    public bool UpdateWinner { get; set; }
}