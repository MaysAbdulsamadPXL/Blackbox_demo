namespace CardGames.Domain;

public class Card : ICard
{
    public Card(CardSuit suit, CardRank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public CardSuit Suit { get; }
    public CardRank Rank { get; }
}