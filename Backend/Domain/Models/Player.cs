namespace Domain.Models;

public class Player
{
    public int Id { get; set; }
    public required User User { get; set; }
    public bool IsWinner { get; set; }
    public int Score { get; set; }
    public double Elo { get; set; }
    public virtual int MatchId { get; set; }

    public static implicit operator Player(User user)
    {
        return new Player
        {
            User = user,
            IsWinner = false,
            Score = 0,
            Elo = user.Elo
        };
    }
}